using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TerritoryHelperClassLibrary.Models;
using TerritoryHelperClassLibrary.Models.Configuration;
using TerritoryHelperClassLibrary.Models.UtilityModels;
using TextCopy;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace TerritoryHelperClassLibrary.BaseServices.WebScraping;

public class WebScrapingService
{
    //Fields
    public LowerLeverProgressReportModel lowerReport;

    public WebScrapingService()
    {
        lowerReport = new LowerLeverProgressReportModel();
    }

    public List<ModelRTFRecord> GetExistingTableTerritoryInformation(TerritoryHelperConfiguration config, IProgress<LowerLeverProgressReportModel> lowerProgress)
    {
        //Report
        lowerReport.LowerLevelProcessPercentComplete = 0;
        lowerReport.LowerLevelProcessMessage = $"Retrieving Territory RTF Records...";
        lowerProgress.Report(lowerReport);
        
        new DriverManager().SetUpDriver(new ChromeConfig());
        IWebDriver driver = new ChromeDriver();

        driver.Navigate().GoToUrl(config.LoginUrl);

        var userNameTextBox = driver.FindElement(By.Id("Email"));
        userNameTextBox.SendKeys(config.UserName);

        var passwordTextBox = driver.FindElement(By.Id("Password"));
        passwordTextBox.SendKeys(config.Password);

        var submitButton = driver.FindElement(By.XPath("//*[@id=\"login\"]/div[7]/input"));
        submitButton.Click();

        //Select Congregation
        var selectCongregation = driver.FindElement(By.XPath("//*[@id=\"login\"]/div[2]/div/div/div[1]"));
        selectCongregation.Click();

        //Click on Assignments
        var assignmentsButton = driver.FindElement(By.XPath("//*[@id=\"topbar\"]/div[2]/div[1]/div[7]/a"));
        assignmentsButton.Click();

        //Get list of territory elements
        var territoryElements = driver.FindElements(By.ClassName("territory"))
            .Select(x => x.GetAttribute("id").ToString().Remove(0, 17)).ToList();

        //Get innerText
        var territoryNumbers = driver.FindElements(By.ClassName("number"))
            .Select(x => x.GetAttribute("innerText").Split(' ').Last()).ToList();

        var territoryIdList = new List<string>();
        var rtfRecords = new List<ModelRTFRecord>();

        for (int i = 0; i < territoryElements.Count; i++)
        {
            //Go to the Territory Page
            var territoryUrl = $"{config.TerritoryRecordBaseUrl}/{territoryElements[i]}";
            driver.Navigate().GoToUrl(territoryUrl);
            var tableRows = driver.FindElements(By.XPath("//*[@id=\"territory_notes\"]//tr")).ToList();

            //Report
            int reportCount = i + 1;
            string territoryReport = ScrubHtml(territoryElements[i]);
            lowerReport.LowerLevelProcessPercentComplete = reportCount*100 / territoryElements.Count;
            lowerReport.LowerLevelProcessMessage = $"Processing Territory Id {territoryReport}: Record {reportCount} of {territoryElements.Count}...";
            lowerProgress.Report(lowerReport);

            if (tableRows.Count > 0)
            {
                foreach (var row in tableRows)
                {
                    var tableColumns = row.FindElements(By.XPath("./td")).ToList();

                    string Address = ScrubHtml(tableColumns[0].GetAttribute("innerText")) ?? "";
                    if (Address is not "Dirección" && Address is not "" && Address is not null && tableColumns.Count == config.NumberOfTableRows)
                    {
                        var rtfRecord = new ModelRTFRecord
                        {
                            TerritoryNumber = ScrubHtml(territoryNumbers[i]),
                            Address = Address,
                            NamesList = ScrubHtml(tableColumns[1].GetAttribute("innerText")),
                            PhoneNumbersList = ScrubHtml(tableColumns[2].GetAttribute("innerText")),
                            Notes = ScrubHtml(tableColumns[3].GetAttribute("innerText"))
                        };
                        rtfRecords.Add(rtfRecord);
                        PrintRecordsToConsole(rtfRecord, rtfRecords);
                    }
                }
            }

            territoryIdList.Add(territoryElements[i]);
        }

        //Get territory RTF information
        driver.Quit();

        //Save File
        SaveFile(rtfRecords, "WebScrapedData");
        return rtfRecords;
    }

    public List<AddressMasterRecord> ConvertRTFRecordtoSpanishImportList(List<ModelRTFRecord> rTFRecordList)
    {
        var addressImportList = new List<AddressMasterRecord>();
        if (rTFRecordList.Count > 0)
        {
            foreach (var address in rTFRecordList)
            {
                var addressImport = new AddressMasterRecord
                {
                    CompleteAddress = address.Address,
                    NamesList = address.NamesList,
                    PhoneNumbers = address.PhoneNumbersList,
                    TerritoryNumber = address.TerritoryNumber,
                    Notes = address.Notes
                };

                addressImportList.Add(addressImport);
            }
        }
        return addressImportList;
    }

    private void SaveFile(object modelToSave, string fileName)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
        };
        var jsonText = JsonSerializer.Serialize(modelToSave);
        string fullFileName = $"{fileName}-{DateTime.Now.ToString("MM-dd-yyyy")}.json";
        string fileDirectory = @"D:\Documents\Spritual Documents\Territorios-West Columbia\TerritoryProcessing\TerritoryHelperScripts\TerritoryHelperDataFrameTest\Output\";
        string fileFullPath = Path.Combine(fileDirectory, fullFileName);
        File.WriteAllText(fileFullPath, jsonText);

    }

    private void PrintRecordsToConsole(ModelRTFRecord record, List<ModelRTFRecord> recordList)
    {
        Console.WriteLine("-----------------------------------------------------");
        Console.WriteLine($"Record Count:{recordList.Count}");
        Console.WriteLine($"Territory Number: {record.TerritoryNumber}");
        Console.WriteLine($"Address:{record.Address}");
        Console.WriteLine($"Names:{record.NamesList}");
        Console.WriteLine($"Phone Numbers:{record.PhoneNumbersList}");
    }

    private static string ScrubHtml(string value)
    {
        var step1 = Regex.Replace(value, @"<[^>]+>|&nbsp;", "").Trim();
        var step2 = Regex.Replace(step1, @"\s{2,}", " ");
        return step2;
    }

    public void PasteTerritoryTables(TerritoryHelperConfiguration config, List<AddressMasterRecord> masterRecordList)
    {
        new DriverManager().SetUpDriver(new ChromeConfig());
        IWebDriver driver = new ChromeDriver();

        driver.Navigate().GoToUrl(config.LoginUrl);

        var userNameTextBox = driver.FindElement(By.Id("Email"));
        userNameTextBox.SendKeys(config.UserName);

        var passwordTextBox = driver.FindElement(By.Id("Password"));
        passwordTextBox.SendKeys(config.Password);

        var submitButton = driver.FindElement(By.XPath("//*[@id=\"login\"]/div[7]/input"));
        submitButton.Click();

        //Select Congregation
        var selectCongregation = driver.FindElement(By.XPath("//*[@id=\"login\"]/div[2]/div/div/div[1]"));
        selectCongregation.Click();

        //Click on Assignments
        var assignmentsButton = driver.FindElement(By.XPath("//*[@id=\"topbar\"]/div[2]/div[1]/div[7]/a"));
        assignmentsButton.Click();

        //Get list of territory elements
        var territoryElements = driver.FindElements(By.ClassName("territory"))
            .Select(x => x.GetAttribute("id").ToString().Remove(0, 17)).ToList();

        //Get innerText
        var territoryNumbers = driver.FindElements(By.ClassName("number"))
            .Select(x => x.GetAttribute("innerText").Split(' ')[1]).ToList();

        //Create a string builder to store existing rtf text
        StringBuilder sb = new StringBuilder();
        sb.Append($"--Text File of existing RTF text gathered on {DateTime.Now.ToString("MM/dd/yyyy hh: mm")}\n");

        for (int i = 0; i < territoryElements.Count; i++)
        {
            //Go to the Territory Page
            var territoryUrl = $"{config.TerritoryRecordBaseUrl}/{territoryElements[i]}";
            driver.Navigate().GoToUrl(territoryUrl);

            //Click the edit button
            var editTableButton = driver.FindElement(By.Id("territory_notes_toggle"));
            editTableButton.Click();

            //Wait for page load
            Thread.Sleep(2000);

            //Click the "More" or ... button
            var moreButton = driver.FindElement(By.XPath("//button[@title='More...']"));
            moreButton.Click();

            //Wait for page load
            Thread.Sleep(2000);

            //Click on Source code button
            var sourceCodeButton = driver.FindElement(By.XPath("//button[@title='Source code']"));
            sourceCodeButton.Click();

            //Wait for page load
            Thread.Sleep(1000);

            //select code text box
            var codeMirrorExisting = driver.FindElement(By.ClassName("CodeMirror"));

            //bring code line into focus
            var codeLineExisting = codeMirrorExisting.FindElements(By.ClassName("CodeMirror-line")).FirstOrDefault();
            codeMirrorExisting.Click();

            //Add text into textbox
            var rtfTextElement = codeMirrorExisting.FindElement(By.CssSelector("textarea"));

            //Add text into textbox
            rtfTextElement.SendKeys(Keys.Control + "a");
            rtfTextElement.SendKeys(Keys.Control + "x");

            //var existingRTFText = rtfTextElement.GetAttribute("value");
            sb.Append("--------------------------------------------\n");
            sb.Append($"Territory Number: {territoryNumbers[i]}\n");
            sb.Append($"{ClipboardService.GetText()}\n");

            Console.WriteLine($"{ClipboardService.GetText()}");

            //Clear clipboard
            ClipboardService.SetText("");

            //Get territory addresses for the group 
            var territoryAddressListByTerritoryNumber = masterRecordList.Where(x => x.TerritoryNumber == territoryNumbers[i]).ToList();

            if (territoryAddressListByTerritoryNumber is not null && territoryAddressListByTerritoryNumber.Count() > 0)
            {

                //Create table for input
                var tableInputText = GenerateTextTableForEachTerritory(territoryAddressListByTerritoryNumber);

                //Use clipboard service
                ClipboardService.SetText(tableInputText);

                //select code text box
                var codeMirror = driver.FindElement(By.ClassName("CodeMirror"));

                //bring code line into focus
                var codeLine = codeMirror.FindElement(By.ClassName("CodeMirror-lines"));
                codeLine.Click();

                //Add text into textbox
                var textBox = codeMirror.FindElement(By.CssSelector("textarea"));
                textBox.SendKeys(Keys.Control + "v");

                //Clear clipboard
                ClipboardService.SetText("");
            }

            //Save text
            var saveButton = driver.FindElement(By.XPath("//button[@title='Save']"));
            saveButton.Click();

            //Save rich text formate edits
            var saveRTFEditButton = driver.FindElement(By.Id("territory_notes_save_btn"));
            saveRTFEditButton.Click();

            //Close window if needed
        }

        //existing String
        var finalExistingString = sb.ToString();
        string backupFileName = $"backupTableData-{DateTime.Now.ToString("MM-dd-yyyy")}.txt";
        var fullbackupPath = Path.Combine(config.FileSavedOutputLocation, backupFileName);
        File.WriteAllTextAsync(fullbackupPath, finalExistingString);

        //Get territory RTF information
        driver.Quit();
    }

    private string GenerateTextTableForEachTerritory(List<AddressMasterRecord> territorySpecificAddressList)
    {
        StringBuilder sb = new StringBuilder();

        if (territorySpecificAddressList is not null && territorySpecificAddressList.Count() > 0)
        {

            if (!String.IsNullOrEmpty(territorySpecificAddressList[0].TerritoryName))
            {
                sb.Append($"<p><strong>Territory Name:</strong>  {territorySpecificAddressList[0].TerritoryName}</p>");
            }
            if (!String.IsNullOrEmpty(territorySpecificAddressList[0].TerritorySpecialNotes))
            {
                sb.Append($"<p><strong>Special Notes:</strong>  {territorySpecificAddressList[0].TerritoryName}</p>");
            }
            sb.Append(@"
                <p>&nbsp;</p>
                <p>&nbsp;</p>
                <table dir=""ltr"" border=""1"" cellspacing=""0"" cellpadding=""0""><colgroup><col width=""172"" /><col width=""158"" /><col width=""102"" /><col width=""242"" /></colgroup>
                <tbody>
                <tr>
                <td style=""text-align: center;"" data-sheets-value=""{""><strong>Direcci&oacute;n</strong></td>
                <td style=""text-align: center;"" data-sheets-value=""{""><strong>Nombres</strong></td>
                <td style=""text-align: center;"" data-sheets-value=""{""><strong>N&uacute;mero Telef&oacute;nico</strong></td>
                <td style=""text-align: center;"" data-sheets-value=""{""><strong>Notas</strong></td>
                </tr>
                ");

            foreach (var record in territorySpecificAddressList)
            {
                if (territorySpecificAddressList.Count > 0)
                {
                    if (record.LocationType == "Do not Call" || record.LocationType == "NO VISITAR")
                    {
                        sb.Append($@"
                            <tr>
                            <td data-sheets-value=""{{"">{record.CompleteAddress}</td>
                            <td data-sheets-value=""{{""><span style=""color: #ba372a;"">NO VISITAR</span></td>
                            <td data-sheets-value=""{{""><span style=""color: #ba372a;"">NO VISITAR</span></td>
                            <td data-sheets-value=""{{""><span style=""color: #ba372a;"">NO VISITAR</span></td>
                            </tr>
                            ");
                    }
                    else
                    {
                        sb.Append($@"
                            <tr>
                            <td data-sheets-value=""{{"">{record.CompleteAddress}</td>
                            <td data-sheets-value=""{{"">{record.NamesList}</td>
                            <td data-sheets-value=""{{"">{record.PhoneNumbers}</td>
                            <td data-sheets-value=""{{"">{record.Notes}</td>
                            </tr>
                            ");
                    }
                }
            }

            sb.Append($@"
                </tbody>
                </table>
                <p>&nbsp;</p>
                <p><strong><span style=""font-size: 8pt;"">Reconciliado {DateTime.Now.ToString("MM/dd/yyyy")}</span></strong></p>
                ");
        }
        return sb.ToString();
    }
}
