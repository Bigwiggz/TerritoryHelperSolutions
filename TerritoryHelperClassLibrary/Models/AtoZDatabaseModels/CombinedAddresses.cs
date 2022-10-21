using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerritoryHelperClassLibrary.Models.AtoZDatabaseModels;

public class CombinedAddresses
{
    public string UniqueLocationIndex { get; set; }
    public string Street { get; set; }
    public string Number { get; set; }
    public string City { get; set; }
    public string County { get; set; }
    public string ZipCode { get; set; }
    public string State { get; set; }
    public string Unit { get; set; }
    public string Floor { get; set; }
    public string CountryCode { get; set; }
    public string StatusCode { get; set; }
    public string LocationType { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public bool IsSpanish { get; set; }
}
