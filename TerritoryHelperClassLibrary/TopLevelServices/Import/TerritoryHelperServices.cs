using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerritoryHelperClassLibrary.BaseServices.AddressVerification;
using TerritoryHelperClassLibrary.BaseServices.Excel;
using TerritoryHelperClassLibrary.BaseServices.GeoMapping;
using TerritoryHelperClassLibrary.BaseServices.RecordCleanup;
using TerritoryHelperClassLibrary.BaseServices.WebScraping;
using TerritoryHelperClassLibrary.Models;
using TerritoryHelperClassLibrary.Models.AtoZDatabaseModels;
using TerritoryHelperClassLibrary.Models.Configuration;
using TerritoryHelperClassLibrary.BaseServices.RoutePlanner;

namespace TerritoryHelperClassLibrary.TopLevelServices.Import;

public class TerritoryHelperServices
{
    public TerritoryHelperServices()
    {
        
    }

    public async Task ImportDataFromTerritoryHelper(TerritoryHelperConfiguration config)
    {
        //1) Import territory helper information
        Console.WriteLine("Importing Territory Helper Information");

        var excelService = new ExcelBaseService();
        FileInfo fileInfo=new FileInfo(config.ExistingSpanishAddressesFilePath);
        var territoryHelperAddressList = await excelService.LoadExistingSpanishAddreessesExcelFile(fileInfo);

        //2) Web scrape territory helper for notes
        Console.WriteLine("Webscraping for Territory Notes");
        var webScraper = new WebScrapingService();
        var territoryHelperNotesList = webScraper.GetExistingTableTerritoryInformation(config);
        var territoryHelperNotesRecordsMaster = webScraper.ConvertRTFRecordtoSpanishImportList(territoryHelperNotesList);

        //3) Do Record cleanup
        Console.WriteLine("Cleaning up Territory Records");
        var recordCleanup = new RecordCleanupService();
        recordCleanup.CreateUniqueIdentifierFromList(territoryHelperAddressList);
        recordCleanup.CreateUniqueIdentifierFromList(territoryHelperNotesRecordsMaster);

        recordCleanup.ReplaceNullsWithEmpty<AddressMasterRecord>(territoryHelperAddressList);
        recordCleanup.ReplaceNullsWithEmpty<AddressMasterRecord>(territoryHelperNotesRecordsMaster);

        //3A) Add other properties
        recordCleanup.AddOtherMasterRecordProperties(territoryHelperAddressList);
        recordCleanup.AddOtherMasterRecordProperties(territoryHelperNotesRecordsMaster);

        //A) In TerritoryHelper but NOT in Territory Notes
        Console.WriteLine("Splitting Territory Records i 3 categories");
        var territoryHelperNOTTerritoryNotes = territoryHelperAddressList
            .Where(x => !territoryHelperNotesRecordsMaster
            .Any(y => y.UniqueIdentifierCreation == x.UniqueIdentifierCreation)).ToList();

        //B) In Both TerritoryHelper AND Territory Notes
        var territoryHelperANDTerritoryNotes = territoryHelperAddressList
            .Where(x => territoryHelperNotesRecordsMaster
            .Any(y => y.UniqueIdentifierCreation == x.UniqueIdentifierCreation))
            .ToList();

        territoryHelperANDTerritoryNotes.ForEach(x => 
        {
            x.NamesList = territoryHelperNotesRecordsMaster.FirstOrDefault(y => y.UniqueIdentifierCreation == x.UniqueIdentifierCreation).NamesList;
            x.PhoneNumbers = territoryHelperNotesRecordsMaster.FirstOrDefault(y => y.UniqueIdentifierCreation == x.UniqueIdentifierCreation).PhoneNumbers;
            x.Notes = territoryHelperNotesRecordsMaster.FirstOrDefault(y => y.UniqueIdentifierCreation == x.UniqueIdentifierCreation).Notes;

        });

        //C) In Territory Notes but NOT in Territory Helper
        var territoryNotesNOTTerritoryHelper = territoryHelperNotesRecordsMaster
            .Where(x => !territoryHelperAddressList
            .Any(y => y.UniqueIdentifierCreation == x.UniqueIdentifierCreation)).ToList();

        //4) Verify all addresses
        Console.WriteLine("Verifying all addresses");
        var addressVerificationService = new AddressVerificationService();
        await addressVerificationService.VerifyAddress(territoryHelperNOTTerritoryNotes,config);
        await addressVerificationService.VerifyAddress(territoryHelperANDTerritoryNotes, config);
        await addressVerificationService.VerifyAddress(territoryNotesNOTTerritoryHelper, config);

        //5) Export all the data to an excel file
        Console.WriteLine("Exporting all data to Excel");
        var excelOutputFileName = $"MasterExcelOutput{DateTime.Now.ToString("MM-dd-yyyy")}.xlsx";
        var excelOutputFilePath = Path.Combine(config.FileSavedOutputLocation, excelOutputFileName);
        FileInfo fileInfoExcelOutput = new FileInfo(excelOutputFilePath);

        await excelService.SaveExcelFileReconciliation(
            territoryHelperAddressList,
            territoryHelperNotesRecordsMaster,
            territoryNotesNOTTerritoryHelper,
            territoryHelperANDTerritoryNotes,
            territoryHelperNOTTerritoryNotes,
            fileInfoExcelOutput);
    }

    public async Task UpdateTerritoryHelperUsingMasterRecord(TerritoryHelperConfiguration config)
    {
        //1) Verify all addresses are in
        Console.WriteLine("RUN CHECK");
        Console.WriteLine("1) Did you make all edits to the territory database information/hard file information? (Y/N)");
        Console.WriteLine("2) Are you sure the order of the territory database information/hard file has not changed and was downloaded in ENGLISH? (Y/N)");
        Console.WriteLine("3) Is everything in the correct file with the correct name? (Y/N)");
        Console.WriteLine("4) Did you export the territory helper addresses and put it in the folder Input/CurrentTerritoryHelperAddresses with the name \"TerritoryHelperAddresses.xlsx\"? (Y/N)");
        Console.WriteLine("5) Did you check to make sure all existing territory notes are");

        //2) Import territory helper information for order of addresses
        Console.WriteLine("Importing Territory Helper Information");
        var excelService = new ExcelBaseService();
        FileInfo fileInfo = new FileInfo(config.ExistingSpanishAddressesFilePath);
        var territoryHelperAddressList = await excelService.LoadExistingSpanishAddreessesExcelFile(fileInfo);

        //2a) Create Unique Identifier for eacy record
        var recordCleanup = new RecordCleanupService();
        recordCleanup.CreateUniqueIdentifierFromList(territoryHelperAddressList);

        //3) Import Territory Edited Records
        FileInfo editedterritoryRecordsFileInfo = new FileInfo(config.EditedTerritoryHelperMasterAddressForImportFilePath);
        var editedTerritoryRecordsListForImport = await excelService.LoadEditedAndUpdatedMasterFileForTerritoryHelperImport(editedterritoryRecordsFileInfo);

        //4) Import Territory Notes
        var territoryNotesText = await File.ReadAllTextAsync(config.TerritoryNotesPath);
        var territoryNotesList= JsonSerializer.Deserialize<List<TerritorySpecificNote>>(territoryNotesText);

        //5a) Import Territory Special Notes and Reorder the territory list
        foreach (var record in editedTerritoryRecordsListForImport)
        {
            record.Order = 0;

            foreach (var territory in territoryNotesList)
            {
                if (record.TerritoryNumber==territory.TerritoryNumbers)
                {
                    record.TerritorySpecialNotes = territory.TerritorySpecialNotes;
                    record.TerritoryName = territory.TerritoryDescription;
                }
            }

            foreach (var orderItem in territoryHelperAddressList)
            {
                if (record.UniqueIdentifierCreation == orderItem.UniqueIdentifierCreation)
                {
                    record.Order = orderItem.Order;
                }
            }

        }

        //5b) Order by descending and keep only records that are from the original territory List
        var orderedTerritoryRecordsListForImport = editedTerritoryRecordsListForImport.Where(x => x.Order > 0).OrderBy(x => x.Order).ToList();

        //5c) Get Excluded Records
        var excludedRecords = editedTerritoryRecordsListForImport.Where(x => x.Order == 0).ToList();

        //5d) Make records list of all records added and excluded
        string outputFileName = $"ExportedListToTerritoryHelper-{DateTime.Now.ToString("MM-dd-yyyy")}.xlsx";

        var outputExcelFile = new FileInfo(Path.Combine(config.FileSavedOutputLocation, outputFileName));

        var excelFileProcessing = new ExcelBaseService();
        
        await excelFileProcessing.ExportAddressMasterRecordToExcel(orderedTerritoryRecordsListForImport, excludedRecords, outputExcelFile);
        /*
        //TEST
        var routePlannerService = new RoutePlannerService();
        var testResult = await routePlannerService.GetRouteDirectionsPerTerritory(orderedTerritoryRecordsListForImport, config);
        var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        var geoJSONText = JsonSerializer.Serialize(testResult.Item2, jsonOptions);
        var testFilePath = Path.Combine(config.FileSavedOutputLocation, "testGeoJSONPaths.json");
        await File.WriteAllTextAsync(testFilePath, geoJSONText);
        */

        //6) Paste Information in Territory Tables
        var webScraperService = new WebScrapingService();
        webScraperService.PasteTerritoryTables(config, orderedTerritoryRecordsListForImport);
    }

    public async Task ImportAtoZDatabaseAddresses(TerritoryHelperConfiguration config)
    {
        Console.WriteLine("Starting Address Parsing...");

        var FileServices = new GeoFileProcessing();

        //Extract all records from all files into one single object
        var allxlsxFiles = FileServices.GetFiles(config.AtoZXLSXFilesPath);

        var allImportedRecords = await FileServices.AggregateListOfAllRecordsPerFile(allxlsxFiles);

        FileServices.AddIdEnumeration(allImportedRecords);

        //Check for Spanish Last Names
        var excelFileProcessing = new ExcelBaseService();

        var spanishLastNamesFile = new FileInfo(config.SpanishLastNamesPath);

        var listOfSpanishLastNames = await excelFileProcessing.LoadSpanishLastNames(spanishLastNamesFile);

        FileServices.AddIsSpanish(allImportedRecords, listOfSpanishLastNames);

        //Filter on Spanish Names only
        var spanishOnlyList = FileServices.FilterOnSpanishNamesOnly(allImportedRecords);

        //Filter out existing addresses
        FileInfo existingSpanishAddressFile = new FileInfo(config.ExistingSpanishAddressesFilePath);

        var newSpanishAddressList = await FileServices.FilterOnlyNewSpanishAddresses(spanishOnlyList, existingSpanishAddressFile);

        //Create master Record List
        var masterRecordList = FileServices.CreateMasterRecordsList(allImportedRecords, newSpanishAddressList);

        //Filter on whole of territory boundary
        var boundaryFilteredMasterList = FileServices.FilterTerritoriesbyBoundary(masterRecordList, config.CongregationCurrentTerritoryBoundariesFilePath);

        //Find out which territory each address is in
        FileServices.FindTerritoryLocationPerAddress(boundaryFilteredMasterList, config.TerritoriesFilePath);

        boundaryFilteredMasterList = boundaryFilteredMasterList.Where(x => x.TerritoryType != "G0").ToList();

        //Add Address Verification
        var finalModelList = FileServices.CreateFinalModelList(boundaryFilteredMasterList);

        var addressVerifierService = new AddressVerificationService();

        await addressVerifierService.VerifyAddress(finalModelList,config);

        //Add in Territory Notes
        FileServices.AddTerritoryNotes(finalModelList, config);

        //Save address list to spreadsheet
        string outputFileName = $"NewAddressImport{DateTime.Now.ToString("MM-dd-yyyy")}.xlsx";

        var outputExcelFile = new FileInfo(Path.Combine(config.FileSavedOutputLocation, outputFileName));

        var territoryHelperImportRecordsList=FileServices.CreateTerritoryHelperImportAddressRecordList(finalModelList);

        await excelFileProcessing.SaveExcelMasterFile(finalModelList, territoryHelperImportRecordsList, outputExcelFile);

        //Save all results
        FileInfo congregationTerritoriesFile = new FileInfo(config.TerritoriesFilePath);

        var existingSpanishAddressGeoJSON = await FileServices.CreateExistingAddressGeoJSON(existingSpanishAddressFile);
        var newSpanishAddressGeoJSON = FileServices.CreateNewAddressGeoJSON(boundaryFilteredMasterList);
        await FileServices.CreateJavascriptTerritoriesAndLocationsFiles(config, newSpanishAddressGeoJSON, existingSpanishAddressGeoJSON, congregationTerritoriesFile);


        Console.WriteLine("End Program");
    }
}
