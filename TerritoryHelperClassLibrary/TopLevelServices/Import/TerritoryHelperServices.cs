using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerritoryHelperClassLibrary.BaseServices.AddressVerification;
using TerritoryHelperClassLibrary.BaseServices.Excel;
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


}
