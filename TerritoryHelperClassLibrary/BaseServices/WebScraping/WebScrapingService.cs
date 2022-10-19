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
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace TerritoryHelperClassLibrary.BaseServices.WebScraping;

public class WebScrapingService
{
    public List<ModelRTFRecord> GetExistingTableTerritoryInformation(TerritoryHelperConfiguration territoryHelperConfiguration)
    {

        new DriverManager().SetUpDriver(new ChromeConfig());
        IWebDriver driver = new ChromeDriver();

        driver.Navigate().GoToUrl(territoryHelperConfiguration.LoginUrl);

        var userNameTextBox = driver.FindElement(By.Id("Email"));
        userNameTextBox.SendKeys(territoryHelperConfiguration.UserName);

        var passwordTextBox = driver.FindElement(By.Id("Password"));
        passwordTextBox.SendKeys(territoryHelperConfiguration.Password);

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
            var territoryUrl = $"{territoryHelperConfiguration.TerritoryRecordBaseUrl}/{territoryElements[i]}";
            driver.Navigate().GoToUrl(territoryUrl);
            var tableRows = driver.FindElements(By.XPath("//*[@id=\"territory_notes\"]//tr")).ToList();

            //TODO: Remove Table Headers with "Dirección, Nombres, Número Telefónico, Notas"
            if (tableRows.Count > 0)
            {
                foreach (var row in tableRows)
                {
                    var tableColumns = row.FindElements(By.XPath("./td")).ToList();

                    string Address = ScrubHtml(tableColumns[0].GetAttribute("innerText")) ?? "";
                    if (Address is not "Dirección" && Address is not "" && Address is not null && tableColumns.Count == territoryHelperConfiguration.NumberOfTableRows)
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
}
