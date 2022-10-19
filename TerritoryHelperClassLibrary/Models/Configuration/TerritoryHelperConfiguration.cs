using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerritoryHelperClassLibrary.Models.Configuration;

public class TerritoryHelperConfiguration
{
    //Territory Helper Information
    public string UserName { get; set; }
    public string Password { get; set; }
    public string TerritoryRecordBaseUrl { get; set; }
    public string LoginUrl { get; set; }

    //Web Scraper Location
    public string SeleniumWebBrowserLocation { get; set; }
    public int NumberOfTableRows { get; set; }

    //File Directory
    public string FileSavedOutputLocation { get; set; } 
    public string FileInputLocation { get; set; }

    //Address Verification Information
    public string AddressVerificationUserId { get; set; }
    public string APIType { get; set; }
    public string BatchID { get; set; }
    public string USPSAPISite { get; set; }
    public int APICallDelayinMiliseconds { get; set; }
}
