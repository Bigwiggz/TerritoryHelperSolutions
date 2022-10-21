// See https://aka.ms/new-console-template for more information
using TerritoryHelperClassLibrary.Models.Configuration;
using TerritoryHelperClassLibrary.TopLevelServices.Import;

Console.WriteLine("Hello, World!");


var westColumbiaTerritoryHelperConfig = new TerritoryHelperConfiguration
{
    //Territory Helper Information
    UserName = "bigwiggz@live.com",
    Password = "SpanishTerr1914",
    TerritoryRecordBaseUrl = "https://www.territoryhelper.com/en/View/Territory",
    LoginUrl = "https://www.territoryhelper.com/",

    //Web Scraper Location
    SeleniumWebBrowserLocation = @"D:\Documents\Programming\Web Programs\TerritoryHelperSolutions\TerritoryHelperClassLibrary\BaseServices\WebScraping\WebDriver\",
    NumberOfTableRows = 4,

    //File Directory
    FileSavedOutputLocation = @"D:\Documents\Programming\Web Programs\TerritoryHelperSolutions\TerritoryHelperConsole\Output\",
    FileInputLocation = @"D:\Documents\Programming\Web Programs\TerritoryHelperSolutions\TerritoryHelperConsole\Input\",

    //Address Verification Information
    AddressVerificationUserId = "388PALME4313",
    APIType = "Verify",
    BatchID = "1",
    USPSAPISite = "http://production.shippingapis.com/ShippingAPI.dll?API=Verify&XML=",
    APICallDelayinMiliseconds = 0,

    //Imported AtoZ File Paths
    AtoZDatbaseFilesPath = @"D:\Documents\Programming\Web Programs\TerritoryHelperSolutions\TerritoryHelperConsole\Input\AtoZDatabase\AddressImportFiles\",
    AtoZXLSXFilesPath= @"D:\Documents\Programming\Web Programs\TerritoryHelperSolutions\TerritoryHelperConsole\Input\AtoZDatabase\AddressXLSXFiles\",
    SpanishLastNamesPath= @"D:\Documents\Programming\Web Programs\TerritoryHelperSolutions\TerritoryHelperConsole\Input\AtoZDatabase\SpanishLastNames\SpanishLastNames.xlsx",
    ExistingSpanishAddressesFilePath= @"D:\Documents\Programming\Web Programs\TerritoryHelperSolutions\TerritoryHelperConsole\Input\AtoZDatabase\CurrentTerritoryHelperAddresses\TerritoryHelperAddresses.xlsx",
    TerritoryBoundaryFilePath= @"D:\Documents\Programming\Web Programs\TerritoryHelperSolutions\TerritoryHelperConsole\Input\AtoZDatabase\CurrentTerritories\SpanishWestColumbiaTerritory.json",
    CongregationCurrentTerritoryBoundariesFilePath= @"D:\Documents\Programming\Web Programs\TerritoryHelperSolutions\TerritoryHelperConsole\Input\AtoZDatabase\TerritoryBoundary\CongregationTerritoryBoundary.json",

    //Imported AtoZ Database JS file paths
    AtoZTerritoriesJSFilePath = @"D:\Documents\Programming\Web Programs\TerritoryHelperSolutions\TerritoryHelperConsole\Output\Map\js\information\territories.js",
    AtoZExistingAddressesJSFilePath = @"D:\Documents\Programming\Web Programs\TerritoryHelperSolutions\TerritoryHelperConsole\Output\Map\js\information\Addresses\existingAddresses.js",
    AtoZNewAddressesJSFilePath = @"D:\Documents\Programming\Web Programs\TerritoryHelperSolutions\TerritoryHelperConsole\Output\Map\js\information\Addresses\newAddresses.js"
};

//Call your Top level services here
var territoryHelperService = new TerritoryHelperServices();

//Call this service to Import all records from territory helper
//await territoryHelperService.ImportDataFromTerritoryHelper(westColumbiaTerritoryHelperConfig, "TerritoryHelperAddresses.xlsx");

//Call this Service to 
await territoryHelperService.ImportAtoZDatabaseAddresses(westColumbiaTerritoryHelperConfig);

