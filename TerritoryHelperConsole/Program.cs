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
    NumberOfTableRows =4,

    //File Directory
    FileSavedOutputLocation = @"D:\Documents\Programming\Web Programs\TerritoryHelperSolutions\TerritoryHelperConsole\Output\",
    FileInputLocation = @"D:\Documents\Programming\Web Programs\TerritoryHelperSolutions\TerritoryHelperConsole\Input\",

    //Address Verification Information
    AddressVerificationUserId ="388PALME4313",
    APIType = "Verify",
    BatchID = "1",
    USPSAPISite = "http://production.shippingapis.com/ShippingAPI.dll?API=Verify&XML=",
    APICallDelayinMiliseconds =0
};

//Call your Top level services here
var territoryHelperService = new TerritoryHelperServices();
await territoryHelperService.ImportDataFromTerritoryHelper(westColumbiaTerritoryHelperConfig, "TerritoryHelperAddresses.xlsx");