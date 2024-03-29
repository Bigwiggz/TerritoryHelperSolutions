﻿using System.Text.Json;
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
using TerritoryHelperClassLibrary.Models.UtilityModels;
using TerritoryHelperClassLibrary.BaseServices.Parsing;
using TerritoryHelperClassLibrary.Extensions;
using TerritoryHelperClassLibrary.BaseServices.AddressScanner;

namespace TerritoryHelperClassLibrary.TopLevelServices.Import;

public class TerritoryHelperServices
{
    //Fields
    public ProgressReportModel report;
    public LowerLeverProgressReportModel lowerReport;

    public TerritoryHelperServices()
    {
        report = new ProgressReportModel();
        lowerReport= new LowerLeverProgressReportModel();
    }

    public async Task ImportDataFromTerritoryHelper(TerritoryHelperConfiguration config, IProgress<ProgressReportModel> progress, IProgress<LowerLeverProgressReportModel> lowerProgress)
    {

        //1) Import territory helper information
        Console.WriteLine("Importing Territory Helper Information");

        var excelService = new ExcelBaseService();
        FileInfo fileInfo=new FileInfo(config.ExistingSpanishAddressesFilePath);
        var territoryHelperAddressList = await excelService.LoadExistingSpanishAddreessesExcelFile(fileInfo);
        
        //Report
        report.TopLevelProgressMessage = "STAGE 1: Loading Existing Spanish Address Excel File...";
        report.TopLevelPercentComplete = 2;
        progress.Report(report);

        //2) Web scrape territory helper for notes
        Console.WriteLine("Webscraping for Territory Notes");

        //Report
        report.TopLevelProgressMessage = "STAGE 2: Initializing RTF Note Service.  This task will take time to complete...";
        report.TopLevelPercentComplete = 5;
        progress.Report(report);

        var territoryHelperNotesRecordsMaster = new List<AddressMasterRecord>();
        var xmlParser = new XMLParsingService();
        var territoryHelperNotesList=await xmlParser.GetExistingTableTerritoryInformation(config, lowerProgress);
        territoryHelperNotesRecordsMaster = xmlParser.ConvertRTFRecordtoSpanishImportList(territoryHelperNotesList);

        //Report
        report.TopLevelProgressMessage = "STAGE 3: Converting Territory RTF/imported records to Master List...";
        report.TopLevelPercentComplete = 62;
        progress.Report(report);

        //3) Do Record cleanup
        Console.WriteLine("Cleaning up Territory Records");

        //Report
        report.TopLevelProgressMessage = "STAGE 4: Cleaning Territory Records and Creating unique identifiers";
        report.TopLevelPercentComplete = 67;
        progress.Report(report);

        var recordCleanup = new RecordCleanupService();
        recordCleanup.CreateUniqueIdentifierFromList(territoryHelperAddressList);
        recordCleanup.CreateUniqueIdentifierFromList(territoryHelperNotesRecordsMaster);

        //Report
        report.TopLevelProgressMessage = "Cleaning Territory Record null and empty information...";
        report.TopLevelPercentComplete = 70;
        progress.Report(report);

        recordCleanup.ReplaceNullsWithEmpty<AddressMasterRecord>(territoryHelperAddressList);
        recordCleanup.ReplaceNullsWithEmpty<AddressMasterRecord>(territoryHelperNotesRecordsMaster);

        //3A) Add other properties
        recordCleanup.AddOtherMasterRecordProperties(territoryHelperAddressList);
        recordCleanup.AddOtherMasterRecordProperties(territoryHelperNotesRecordsMaster);

        //Report
        report.TopLevelProgressMessage = "STAGE 5: Combining Territory Records and creating difference lists...";
        report.TopLevelPercentComplete = 72;
        progress.Report(report);

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

        //Report
        report.TopLevelProgressMessage = "STAGE 6: Starting Territory Address Verification (This may take some time)...";
        report.TopLevelPercentComplete = 75;
        progress.Report(report);

        //4) Verify all addresses
        Console.WriteLine("Verifying all addresses");
        var addressVerificationService = new AddressVerificationService();
        await addressVerificationService.VerifyAddress(territoryHelperNOTTerritoryNotes,config, lowerProgress);
        await addressVerificationService.VerifyAddress(territoryHelperANDTerritoryNotes, config, lowerProgress);
        await addressVerificationService.VerifyAddress(territoryNotesNOTTerritoryHelper, config, lowerProgress);

        //Report
        report.TopLevelProgressMessage = "STAGE 7: Writing Results To Excel File...";
        report.TopLevelPercentComplete = 95;
        progress.Report(report);

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

        //Report
        report.TopLevelProgressMessage = "Script Completed";
        report.TopLevelPercentComplete = 100;
        progress.Report(report);
    }

    //TODO: finish territory helper master record updating functionality
    public async Task UpdateTerritoryHelperUsingMasterRecord(TerritoryHelperConfiguration config, IProgress<ProgressReportModel> progress, IProgress<LowerLeverProgressReportModel> lowerProgress)
    {
        //1) Verify all addresses are in
        Console.WriteLine("RUN CHECK");
        Console.WriteLine("1) Did you make all edits to the territory database information/hard file information? (Y/N)");
        Console.WriteLine("2) Are you sure the order of the territory database information/hard file has not changed and was downloaded in ENGLISH? (Y/N)");
        Console.WriteLine("3) Is everything in the correct file with the correct name? (Y/N)");
        Console.WriteLine("4) Did you export the territory helper addresses and put it in the folder Input/CurrentTerritoryHelperAddresses with the name \"TerritoryHelperAddresses.xlsx\"? (Y/N)");
        Console.WriteLine("5) Did you check to make sure all existing territory notes are correct");

        //Report
        report.TopLevelPercentComplete = 5;
        report.TopLevelProgressMessage = "Importing Existing Spanish Address Excel File";
        progress.Report(report);

        //2) Import territory helper information for order of addresses
        Console.WriteLine("Importing Territory Helper Information");
        var excelService = new ExcelBaseService();
        FileInfo fileInfo = new FileInfo(config.ExistingSpanishAddressesFilePath);
        var territoryHelperAddressList = await excelService.LoadExistingSpanishAddreessesExcelFile(fileInfo);

        //Report
        report.TopLevelPercentComplete = 15;
        report.TopLevelProgressMessage = "Creating Unique Identifier Records...";
        progress.Report(report);

        //2a) Create Unique Identifier for eacy record
        var recordCleanup = new RecordCleanupService();
        recordCleanup.CreateUniqueIdentifierFromList(territoryHelperAddressList);

        //Report
        report.TopLevelPercentComplete = 20;
        report.TopLevelProgressMessage = "Cleaning record files...";
        progress.Report(report);

        //3) Import Territory Edited Records
        FileInfo editedterritoryRecordsFileInfo = new FileInfo(config.EditedTerritoryHelperMasterAddressForImportFilePath);
        var editedTerritoryRecordsListForImport = await excelService.LoadEditedAndUpdatedMasterFileForTerritoryHelperImport(editedterritoryRecordsFileInfo);

        //Report
        report.TopLevelPercentComplete = 30;
        report.TopLevelProgressMessage = "Importing Territory Records...";
        progress.Report(report);

        //4) Import Territory Notes
        var territoryNotesText = await File.ReadAllTextAsync(config.TerritoryNotesPath);
        var territoryNotesList= JsonSerializer.Deserialize<List<TerritorySpecificNote>>(territoryNotesText);

        //Report
        report.TopLevelPercentComplete = 50;
        report.TopLevelProgressMessage = "Importing Territory Special Notes and Reordering list...";
        progress.Report(report);

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

        //Report
        report.TopLevelPercentComplete = 75;
        report.TopLevelProgressMessage = "Pasting Territory Information Notes Tables...";
        progress.Report(report);

        //6) Paste Information in Territory Tables
        var xmlParser = new XMLParsingService();
        var congregationTerritoriesList=xmlParser.PasteTerritoryTables(config, orderedTerritoryRecordsListForImport, lowerProgress);
        var congregationTerritoriesText = JsonSerializer.Serialize(congregationTerritoriesList, new JsonSerializerOptions{WriteIndented=true });
        string territoriesFilePath = Path.Combine(config.FileSavedOutputLocation, $"CongregationTerritoriesUpdated-{DateTime.Now.ToString("MM-dd-yyyy")}.json");
        await File.WriteAllTextAsync(territoriesFilePath,congregationTerritoriesText);

    }

    //TODO: Add auto sorting based on addresses
    public async Task UpdateCENSOTerritoryHelperUsingMasterRecord(TerritoryHelperConfiguration config, IProgress<ProgressReportModel> progress, IProgress<LowerLeverProgressReportModel> lowerProgress)
    {
        //1) Verify all addresses are in
        Console.WriteLine("RUN CHECK");
        Console.WriteLine("1) Did you make all edits to the territory database information/hard file information? (Y/N)");
        Console.WriteLine("2) Are you sure the order of the territory database information/hard file has not changed and was downloaded in ENGLISH? (Y/N)");
        Console.WriteLine("3) Is everything in the correct file with the correct name? (Y/N)");
        Console.WriteLine("4) Did you export the territory helper addresses and put it in the folder Input/CurrentTerritoryHelperAddresses with the name \"TerritoryHelperAddresses.xlsx\"? (Y/N)");
        Console.WriteLine("5) Did you check to make sure all existing territory notes are");

        //Report
        report.TopLevelPercentComplete = 5;
        report.TopLevelProgressMessage = "Importing Existing Spanish Address Excel File";
        progress.Report(report);

        //2) Import territory helper information for order of addresses
        Console.WriteLine("Importing Territory Helper Information");
        var excelService = new ExcelBaseService();
        FileInfo fileInfo = new FileInfo(config.ExistingSpanishAddressesFilePath);
        var territoryHelperAddressList = await excelService.LoadExistingSpanishAddreessesExcelFile(fileInfo);

        //Report
        report.TopLevelPercentComplete = 15;
        report.TopLevelProgressMessage = "Creating Unique Identifier Records...";
        progress.Report(report);

        //2a) Create Unique Identifier for eacy record
        var recordCleanup = new RecordCleanupService();
        recordCleanup.CreateUniqueIdentifierFromList(territoryHelperAddressList);

        //Report
        report.TopLevelPercentComplete = 20;
        report.TopLevelProgressMessage = "Cleaning record files...";
        progress.Report(report);

        //3) Import Territory Edited Records
        FileInfo editedterritoryRecordsFileInfo = new FileInfo(config.EditedTerritoryHelperMasterAddressForImportFilePath);
        var editedTerritoryRecordsListForImport = await excelService.LoadEditedAndUpdatedMasterFileForTerritoryHelperImport(editedterritoryRecordsFileInfo);

        //Report
        report.TopLevelPercentComplete = 30;
        report.TopLevelProgressMessage = "Importing Territory Records...";
        progress.Report(report);

        //4) Import Territory Notes
        var territoryNotesText = await File.ReadAllTextAsync(config.TerritoryNotesPath);
        var territoryNotesList = JsonSerializer.Deserialize<List<TerritorySpecificNote>>(territoryNotesText);

        //Report
        report.TopLevelPercentComplete = 50;
        report.TopLevelProgressMessage = "Importing Territory Special Notes and Reordering list...";
        progress.Report(report);

        //5a) Import Territory Special Notes and Reorder the territory list
        int i = 1;
        foreach (var record in editedTerritoryRecordsListForImport)
        {
            record.Order = i;

            foreach (var territory in territoryNotesList)
            {
                if (record.TerritoryNumber == territory.TerritoryNumbers)
                {
                    record.TerritorySpecialNotes = territory.TerritorySpecialNotes;
                    record.TerritoryName = territory.TerritoryDescription;
                }
            }
            i++;
        }

        //5b) Order by descending and keep only records that are from the original territory List
        var orderedTerritoryRecordsListForImport = editedTerritoryRecordsListForImport.Where(x => x.Order >= 0).OrderBy(x => x.Order).ToList();

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

        //Report
        report.TopLevelPercentComplete = 75;
        report.TopLevelProgressMessage = "Pasting Territory Information Notes Tables...";
        progress.Report(report);

        //6) Paste Information in Territory Tables
        var xmlParser = new XMLParsingService();
        var congregationTerritoriesList = xmlParser.PasteTerritoryTables(config, orderedTerritoryRecordsListForImport, lowerProgress);
        var congregationTerritoriesText = JsonSerializer.Serialize(congregationTerritoriesList, new JsonSerializerOptions { WriteIndented = true });
        string territoriesFilePath = Path.Combine(config.FileSavedOutputLocation, $"CongregationTerritoriesUpdated-{DateTime.Now.ToString("MM-dd-yyyy")}.json");
        await File.WriteAllTextAsync(territoriesFilePath, congregationTerritoriesText);
    }

    public async Task ImportAtoZDatabaseAddresses(TerritoryHelperConfiguration config, IProgress<ProgressReportModel> progress, IProgress<LowerLeverProgressReportModel> lowerProgress)
    {
        Console.WriteLine("Starting Address Parsing...");



        var FileServices = new GeoFileProcessing();

        //Extract all records from all files into one single object
        var allxlsxFiles = FileServices.GetFiles(config.AtoZXLSXFilesPath);

        var allImportedRecords = await FileServices.AggregateListOfAllRecordsPerFile(allxlsxFiles);

        FileServices.AddIdEnumeration(allImportedRecords);

        //Report
        report.TopLevelPercentComplete = 55;
        report.TopLevelProgressMessage = "Filtering by spanish last names list....";
        progress.Report(report);

        //Check for Spanish Last Names
        var excelFileProcessing = new ExcelBaseService();

        var spanishLastNamesFile = new FileInfo(config.SpanishLastNamesPath);

        var listOfSpanishLastNames = await excelFileProcessing.LoadSpanishLastNames(spanishLastNamesFile);

        FileServices.AddIsSpanish(allImportedRecords, listOfSpanishLastNames);

        //Filter on Spanish Names only
        var spanishOnlyList = FileServices.FilterOnSpanishNamesOnly(allImportedRecords);

        //Report
        report.TopLevelPercentComplete = 60;
        report.TopLevelProgressMessage = "Filtering by existing addresses....";
        progress.Report(report);

        //Filter out existing addresses
        FileInfo existingSpanishAddressFile = new FileInfo(config.ExistingSpanishAddressesFilePath);

        var newSpanishAddressList = await FileServices.FilterOnlyNewSpanishAddresses(spanishOnlyList, existingSpanishAddressFile);

        //Report
        report.TopLevelPercentComplete = 65;
        report.TopLevelProgressMessage = "Filtering by existing CENSO records....";
        progress.Report(report);

        //Filter out CENSO addresses
        FileInfo censoSpanishAddressFile = new FileInfo(config.CensoTerritoryAddressPath);
        var censoFilteredSpanishAddressList = await FileServices.FilterOnlyNewSpanishAddresses(newSpanishAddressList, censoSpanishAddressFile);

        //Report
        report.TopLevelPercentComplete = 63;
        report.TopLevelProgressMessage = "Creating master list....";
        progress.Report(report);

        //Create master Record List
        var masterRecordList = FileServices.CreateMasterRecordsList(allImportedRecords, censoFilteredSpanishAddressList);

        //Report
        report.TopLevelPercentComplete = 65;
        report.TopLevelProgressMessage = "Filtering by congregation territory boundary....";
        progress.Report(report);

        //Filter on whole of territory boundary
        var boundaryFilteredMasterList = FileServices.FilterTerritoriesbyBoundary(masterRecordList, config.CongregationCurrentTerritoryBoundariesFilePath);

        //Report
        report.TopLevelPercentComplete = 65;
        report.TopLevelProgressMessage = "Matching addresses with the correct territory....";
        progress.Report(report);

        //Find out which territory each address is in
        FileServices.FindTerritoryLocationPerAddress(boundaryFilteredMasterList, config.TerritoriesFilePath);

        boundaryFilteredMasterList = boundaryFilteredMasterList.Where(x => x.TerritoryType != "G0").ToList();

        //Report
        report.TopLevelPercentComplete = 70;
        report.TopLevelProgressMessage = "Starting Territory Address Verification (This may take some time)....";
        progress.Report(report);

        //Add Address Verification
        var finalModelList = FileServices.CreateFinalModelList(boundaryFilteredMasterList);

        var addressVerifierService = new AddressVerificationService();

        await addressVerifierService.VerifyAddress(finalModelList,config,lowerProgress);

        //Report
        report.TopLevelPercentComplete = 90;
        report.TopLevelProgressMessage = "Adding Territory Notes....";
        progress.Report(report);

        //Add in Territory Notes
        FileServices.AddTerritoryNotes(finalModelList, config);

        //Report
        report.TopLevelPercentComplete = 90;
        report.TopLevelProgressMessage = "Creating Excel Spreadsheets....";
        progress.Report(report);

        //Save address list to spreadsheet
        string outputFileName = $"NewAddressImport{DateTime.Now.ToString("MM-dd-yyyy")}.xlsx";

        var outputExcelFile = new FileInfo(Path.Combine(config.FileSavedOutputLocation, outputFileName));

        var territoryHelperImportRecordsList=FileServices.CreateTerritoryHelperImportAddressRecordList(finalModelList);

        await excelFileProcessing.SaveExcelMasterFile(finalModelList, territoryHelperImportRecordsList, outputExcelFile);

        //Report
        report.TopLevelPercentComplete = 95;
        report.TopLevelProgressMessage = "Creating interactive maps....";
        progress.Report(report);

        //Save all results
        FileInfo congregationTerritoriesFile = new FileInfo(config.TerritoriesFilePath);

        var existingSpanishAddressGeoJSON = await FileServices.CreateExistingAddressGeoJSON(existingSpanishAddressFile);
        var newSpanishAddressGeoJSON = FileServices.CreateNewAddressGeoJSON(boundaryFilteredMasterList);
        await FileServices.CreateJavascriptTerritoriesAndLocationsFiles(config, newSpanishAddressGeoJSON, existingSpanishAddressGeoJSON, congregationTerritoriesFile);

        //Report
        report.TopLevelPercentComplete = 100;
        report.TopLevelProgressMessage = "Complete";
        progress.Report(report);

        Console.WriteLine("End Program");
    }

    //TODO: Add Address Scanner Service
    public async Task RunAddressScanner(TerritoryHelperConfiguration config, IProgress<ProgressReportModel> progress, IProgress<LowerLeverProgressReportModel> lowerProgress)
    {
        var excelService = new ExcelBaseService();
        FileInfo fileInfo = new FileInfo(config.ExistingSpanishAddressesFilePath);

        //Report
        report.TopLevelPercentComplete = 10;
        report.TopLevelProgressMessage = "Loading Existing Spanish Address File";
        progress.Report(report);

        var masterAddressList = await excelService.LoadExistingSpanishAddreessesExcelFile(fileInfo);

        var territoryHelperAddressList = new List<TerritoryHelperAddress>();

        foreach (var masterTerritory in masterAddressList)
        {
            var territoryAddress = new TerritoryHelperAddress();
            PropertyCopyService.CopyProperties(masterTerritory, territoryAddress);

            territoryHelperAddressList.Add(territoryAddress);
        }

        var addressScrubberService = new AddressScannerService();
        var addressErrorList = addressScrubberService.GetListOfAddressesWithErrors(territoryHelperAddressList, lowerProgress);

        addressErrorList = addressErrorList.Where(x => x.HasError == true).ToList();

        var archiveDirectory = config.FileSavedOutputLocation;

        var excelFilePath = Path.Combine(archiveDirectory, @$"AddressErrorRecords-{DateTime.Now.ToString("MM-dd-yyyy")}.xlsx");

        FileInfo addressErrorListFile = new FileInfo(excelFilePath);

        await excelService.ExportCompleteAddressErrorListToExcel(addressErrorList, addressErrorListFile);

        //Report
        report.TopLevelPercentComplete = 100;
        report.TopLevelProgressMessage = "Complete";
        progress.Report(report);
    }
}
