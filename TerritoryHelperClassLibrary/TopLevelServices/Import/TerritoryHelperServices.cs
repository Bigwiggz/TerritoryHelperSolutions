using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerritoryHelperClassLibrary.BaseServices.AddressVerification;
using TerritoryHelperClassLibrary.BaseServices.Excel;
using TerritoryHelperClassLibrary.BaseServices.FileTransformations;
using TerritoryHelperClassLibrary.BaseServices.GeoMapping;
using TerritoryHelperClassLibrary.BaseServices.RecordCleanup;
using TerritoryHelperClassLibrary.BaseServices.WebScraping;
using TerritoryHelperClassLibrary.Models;
using TerritoryHelperClassLibrary.Models.Configuration;

namespace TerritoryHelperClassLibrary.TopLevelServices.Import;

public class TerritoryHelperServices
{
    public TerritoryHelperServices()
    {
        
    }

    public async Task ImportDataFromTerritoryHelper(TerritoryHelperConfiguration config, string territoryAddressImportFileName)
    {
        //1) Import territory helper information
        Console.WriteLine("Importing Territory Helper Information");

        var excelService = new ExcelBaseService();
        var importTerritoryHelperInformationPath = Path.Combine(config.FileInputLocation, territoryAddressImportFileName);
        FileInfo fileInfo=new FileInfo(importTerritoryHelperInformationPath);
        var territoryHelperAddressList = await excelService.LoadTerritoryHelperAddresses(fileInfo);

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
        addressVerificationService.VerifyAddress(territoryHelperNOTTerritoryNotes,config);
        addressVerificationService.VerifyAddress(territoryHelperANDTerritoryNotes, config);
        addressVerificationService.VerifyAddress(territoryNotesNOTTerritoryHelper, config);

        //5)Finish Address Master Record information
        recordCleanup.AddOtherMasterRecordProperties(territoryHelperNOTTerritoryNotes);
        recordCleanup.AddOtherMasterRecordProperties(territoryHelperANDTerritoryNotes);
        recordCleanup.AddOtherMasterRecordProperties(territoryNotesNOTTerritoryHelper);

        //6) Export all the data to an excel file
        Console.WriteLine("Exporting all data to Excel");
        var excelOutputFileName = $"MasterExcelOutput{DateTime.Now.ToString("MM-dd-yyyy")}.xlsx";
        var excelOutputFilePath = Path.Combine(config.FileSavedOutputLocation, excelOutputFileName);
        FileInfo fileInfoExcelOutput = new FileInfo(importTerritoryHelperInformationPath);

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
        throw new NotImplementedException();
    }

    public async Task ImportAtoZDatabaseAddresses(TerritoryHelperConfiguration config)
    {
        Console.WriteLine("Starting Address Parsing...");

        var FileServices = new GeoFileProcessing();

        var allFiles = FileServices.GetFiles(config.AtoZDatbaseFilesPath);

        //Convert excel files from xls to xlsx
        var excelConverter = new ExcelConverterService();

        foreach (var file in allFiles)
        {
            if (file.Extension == ".xls" && allFiles.Length > 0)
            {
                excelConverter.FreeSpireConvertXLStoXLSX(file, config.AtoZXLSXFilesPath);
            }

        }

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
        FileServices.FindTerritoryLocationPerAddress(boundaryFilteredMasterList, config.TerritoryBoundaryFilePath);

        boundaryFilteredMasterList = boundaryFilteredMasterList.Where(x => x.TerritoryType != "G0").ToList();

        //Add Address Verification
        var finalModelList = FileServices.CreateFinalModelList(boundaryFilteredMasterList);

        var addressVerifierService = new AddressVerificationService();

        addressVerifierService.VerifyAddress(finalModelList,config);

        //Save address list to spreadsheet
        string outputFileName = $"NewAddressImport{DateTime.Now.ToString("MM-dd-yyyy")}.xlsx";

        var outputExcelFile = new FileInfo(Path.Combine(config.FileSavedOutputLocation, outputFileName));

        await excelFileProcessing.SaveExcelMasterFile(finalModelList, outputExcelFile);

        //Save all results
        FileInfo congregationTerritoriesFile = new FileInfo(config.FileSavedOutputLocation);

        var existingSpanishAddressGeoJSON = await FileServices.CreateExistingAddressGeoJSON(existingSpanishAddressFile);
        var newSpanishAddressGeoJSON = FileServices.CreateNewAddressGeoJSON(boundaryFilteredMasterList);
        await FileServices.CreateJavascriptTerritoriesAndLocationsFiles(config, newSpanishAddressGeoJSON, existingSpanishAddressGeoJSON, congregationTerritoriesFile);


        Console.WriteLine("End Program");
    }
}
