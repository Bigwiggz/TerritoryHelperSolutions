// See https://aka.ms/new-console-template for more information
using ExcelMigration.ExcelInterop;
using System.Diagnostics;
using TerritoryHelperClassLibrary.BaseServices.GeoMapping;
using TerritoryHelperClassLibrary.Models.Configuration;
using TerritoryHelperClassLibrary.Models.UtilityModels;
using TerritoryHelperClassLibrary.TopLevelServices.Import;

Console.WriteLine("Hello, World!");


var westColumbiaTerritoryHelperConfig = new TerritoryHelperConfiguration
{
    //Territory Helper Information
    UserName = "[TerritoryHelperUserNameHere]",
    Password = "[TerritoryHelperPasswordHere]",
    TerritoryRecordBaseUrl = "https://www.territoryhelper.com/en/View/Territory",
    LoginUrl = "https://www.territoryhelper.com/",

    //Web Scraper Location
    SeleniumWebBrowserLocation = @"D:\Documents\Programming\Web Programs\TerritoryHelperSolutions\TerritoryHelperClassLibrary\BaseServices\WebScraping\WebDriver\",
    NumberOfTableRows = 4,

    //File Directory
    FileSavedOutputLocation = @"D:\Documents\Programming\Web Programs\TerritoryHelperSolutions\TerritoryHelperConsole\Output\",
    FileInputLocation = @"D:\Documents\Programming\Web Programs\TerritoryHelperSolutions\TerritoryHelperConsole\Input\",

    //Address Verification Information
    AddressVerificationUserId = "[AddressVerificationUserIdHere]",
    APIType = "Verify",
    BatchID = "1",
    USPSAPISite = "http://production.shippingapis.com/ShippingAPI.dll?API=Verify&XML=",
    APICallDelayinMiliseconds = 100,

    //Imported AtoZ File Paths
    AtoZDatbaseFilesPath = @"D:\Documents\Programming\Web Programs\TerritoryHelperSolutions\TerritoryHelperConsole\Input\AtoZDatabase\AddressImportFiles\",
    AtoZXLSXFilesPath = @"D:\Documents\Programming\Web Programs\TerritoryHelperSolutions\TerritoryHelperConsole\Input\AtoZDatabase\AddressXLSXFiles\",
    SpanishLastNamesPath = @"D:\Documents\Programming\Web Programs\TerritoryHelperSolutions\TerritoryHelperConsole\Input\AtoZDatabase\SpanishLastNames\SpanishLastNames.xlsx",
    ExistingSpanishAddressesFilePath = @"D:\Documents\Programming\Web Programs\TerritoryHelperSolutions\TerritoryHelperConsole\Input\AtoZDatabase\CurrentTerritoryHelperAddresses\TerritoryHelperAddresses.xlsx",
    TerritoriesFilePath = @"D:\Documents\Programming\Web Programs\TerritoryHelperSolutions\TerritoryHelperConsole\Input\AtoZDatabase\CurrentTerritories\SpanishWestColumbiaTerritory.json",
    CongregationCurrentTerritoryBoundariesFilePath = @"D:\Documents\Programming\Web Programs\TerritoryHelperSolutions\TerritoryHelperConsole\Input\AtoZDatabase\TerritoryBoundary\CongregationTerritoryBoundary.json",
    EditedTerritoryHelperMasterAddressForImportFilePath = @"D:\Documents\Programming\Web Programs\TerritoryHelperSolutions\TerritoryHelperConsole\Input\AtoZDatabase\EditedTerritoryHelperMasterAddressForImport\TerritoryInformationForImport.xlsx",

    //Imported AtoZ Database JS file paths
    AtoZTerritoriesJSFilePath = @"D:\Documents\Programming\Web Programs\TerritoryHelperSolutions\TerritoryHelperConsole\Output\Map\js\information\Territories\territories.js",
    AtoZExistingAddressesJSFilePath = @"D:\Documents\Programming\Web Programs\TerritoryHelperSolutions\TerritoryHelperConsole\Output\Map\js\information\Addresses\existingAddresses.js",
    AtoZNewAddressesJSFilePath = @"D:\Documents\Programming\Web Programs\TerritoryHelperSolutions\TerritoryHelperConsole\Output\Map\js\information\Addresses\newAddresses.js",

    //Territory Notes
    TerritoryNotesPath= @"D:\Documents\Programming\Web Programs\TerritoryHelperSolutions\TerritoryHelperConsole\Input\AtoZDatabase\TerritorySpecificInformation\TerritorySpecialNotes.json",

    //Kingdom Hall Location Information
    KindgomHallAddress="3679 Leaphart Rd, West Columbia SC, 29169",
    KingdomHallLocationLatitude= 34.00261402946795,
    KingdomHallLocationLongitude=-81.13513616286949,

    //Azure Maps Primary Security Key
    MicrosoftAzureMapsPrimarySecurityKey= "[MS Azure Maps Key Here]"


};

//Call Progress
var progress = new Progress<ProgressReportModel>();
var lowerProgress = new Progress<LowerLeverProgressReportModel>();

//Call your Top level services here
var territoryHelperService = new TerritoryHelperServices();

/*
 IMPORT ALL RECORDS FROM TERRITORY HELPER
 */
//Call this service to Import all records from territory helper
//await territoryHelperService.ImportDataFromTerritoryHelper(westColumbiaTerritoryHelperConfig);

/*
 * EXPORT ALL RECORDS TO TERRITORY HELPER
 */
//Call this service to export all changes to Territory Helper
//await territoryHelperService.UpdateTerritoryHelperUsingMasterRecord(westColumbiaTerritoryHelperConfig);

/*
 * EXPORT ALL RECORDS TO TERRITORY HELPER TO CENSO
 */
//Call this service to export all changes to Territory Helper CENSO
await territoryHelperService.UpdateCENSOTerritoryHelperUsingMasterRecord(westColumbiaTerritoryHelperConfig, progress, lowerProgress);

/*
 * IMPORT ALL RECORDS FROM ATOZ DATABASE
 */

/*
Stopwatch stopWatch = new Stopwatch();
stopWatch.Start();

if(Directory.GetFiles(westColumbiaTerritoryHelperConfig.AtoZDatbaseFilesPath).Length!= Directory.GetFiles(westColumbiaTerritoryHelperConfig.AtoZXLSXFilesPath).Length 
    && Directory.GetFiles(westColumbiaTerritoryHelperConfig.AtoZXLSXFilesPath).Length>0)
{
    var excelConverterService = new ExcelInteropConverterService();

    var FileServices = new GeoFileProcessing();

    var allFiles = FileServices.GetFiles(westColumbiaTerritoryHelperConfig.AtoZDatbaseFilesPath);

    foreach (var file in allFiles)
    {
        if (file.Extension == ".xls" && allFiles.Length > 0)
        {
            excelConverterService.ConvertXLStoXLSX(file, westColumbiaTerritoryHelperConfig.AtoZXLSXFilesPath);
        }
    }
}
await territoryHelperService.ImportAtoZDatabaseAddresses(westColumbiaTerritoryHelperConfig);
stopWatch.Stop();
TimeSpan ts = stopWatch.Elapsed;
string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",ts.Hours, ts.Minutes, ts.Seconds,ts.Milliseconds / 10);
Console.WriteLine("RunTime " + elapsedTime);
*/
