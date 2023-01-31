using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerritoryHelperClassLibrary.Models.Configuration;
using TerritoryHelperClassLibrary.Models.UtilityModels;
using TerritoryHelperClassLibrary.Models;
using TerritoryHelperClassLibrary.Models.AtoZDatabaseModels;
using System.Text.Json;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using OpenQA.Selenium;
using TextCopy;
using AngleSharp.Html.Dom;
using HtmlAgilityPack;
using System.Net;

namespace TerritoryHelperClassLibrary.BaseServices.Parsing;

public class XMLParsingService
{
    //Fields
    public LowerLeverProgressReportModel lowerReport;

    public XMLParsingService()
    {
        lowerReport = new LowerLeverProgressReportModel();
    }

    public async Task<List<ModelRTFRecord>> GetExistingTableTerritoryInformation(TerritoryHelperConfiguration config, IProgress<LowerLeverProgressReportModel> lowerProgress)
    {
        var congregationTerritoriesText = await File.ReadAllTextAsync(config.TerritoriesFilePath);
        var congregationTerritoriesList = JsonSerializer.Deserialize<CongregationTerritories>(congregationTerritoriesText);

        var territoryProperties = congregationTerritoriesList.features;

        var rtfRecords=new List<ModelRTFRecord>();

        int i = 0;
        foreach(var territory in territoryProperties)
        {

            //Report
            lowerReport.LowerLevelProcessPercentComplete = i*100/territoryProperties.Count();
            lowerReport.LowerLevelProcessMessage = $"Retrieving RTF addresses in Territory {territory.properties.TerritoryNumber.Trim()}: Record {i} of {territoryProperties.Count()}";
            lowerProgress.Report(lowerReport);

            var territoryAddressListXML = territory.properties.TerritoryNotes;

            if (string.IsNullOrWhiteSpace(territoryAddressListXML) ==false)
            {
                //Load in Html
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(territoryAddressListXML);

                var tableNode=htmlDoc.DocumentNode.SelectSingleNode("//tbody");
                if(tableNode is not null)
                {
                    var tablRows = tableNode.Elements("tr");

                    foreach (var row in tablRows)
                    {
                        var rowColumns = row.Elements("td").ToList();
                        var addressTest = rowColumns[0].InnerText;
                        if (addressTest is not "Direcci&oacute;n" && addressTest is not "" && addressTest is not null && rowColumns.Count == config.NumberOfTableRows)
                        {
                            var rtfRecord = new ModelRTFRecord
                            {
                                TerritoryNumber = territory.properties.TerritoryNumber.Trim(),
                                Address = ScrubHtml(rowColumns[0].InnerText),
                                NamesList = ScrubHtml(rowColumns[1].InnerText),
                                PhoneNumbersList = ScrubHtml(rowColumns[2].InnerText),
                                Notes = ScrubHtml(rowColumns[3].InnerText),
                            };
                            rtfRecords.Add(rtfRecord);
                        }
                        
                    }
                }
            }
            i++;
        }
        return rtfRecords;
    }

    public async Task<CongregationTerritories> PasteTerritoryTables(TerritoryHelperConfiguration config, List<AddressMasterRecord> masterRecordList, IProgress<LowerLeverProgressReportModel> lowerProgress)
    {
        var congregationTerritoriesText = await File.ReadAllTextAsync(config.TerritoriesFilePath);
        var congregationTerritoriesList = JsonSerializer.Deserialize<CongregationTerritories>(congregationTerritoriesText);

        var territoryProperties = congregationTerritoriesList.features;
        int i = 0;
        foreach (var territory in territoryProperties)
        {
            //Report 
            lowerReport.LowerLevelProcessPercentComplete = i * 100 / territoryProperties.Count();
            lowerReport.LowerLevelProcessMessage = $"Pasting RTF addresses in Territory {territory.properties.TerritoryNumber.Trim()}: Record {i} of {territoryProperties.Count()}";
            lowerProgress.Report(lowerReport);

            //Get territory addresses for the group 
            var territoryAddressListByTerritoryNumber = masterRecordList.Where(x => x.TerritoryNumber == territory.properties.TerritoryNumber.Trim()).ToList();

            //TODO: Input for Territory Notes here
            if (territoryAddressListByTerritoryNumber is not null && territoryAddressListByTerritoryNumber.Count() > 0)
            {

                //Create table for input
                var tableInputText = GenerateTextTableForEachTerritory(territoryAddressListByTerritoryNumber);
                territory.properties.TerritoryNotes = tableInputText;
            }
            i++;
        }

        return congregationTerritoriesList;
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

    private string ScrubHtml(string value)
    {
        var step1 = Regex.Replace(value, @"<[^>]+>|&nbsp;", "").Trim();
        var step2 = Regex.Replace(step1, @"\s{2,}", " ");
        return step2;
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
