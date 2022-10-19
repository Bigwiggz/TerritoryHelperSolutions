using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerritoryHelperClassLibrary.Models;

public class TerritoryHelperAddress
{
    public string TerritoryType { get; set; }
    public string TerritoryNumber { get; set; }
    public string LocationType { get; set; }
    public string Status { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public string CompleteAddress { get; set; }
    public string Number { get; set; }
    public string Street { get; set; }
    public string Apartment { get; set; }
    public string Floor { get; set; }
    public string City { get; set; }
    public string County { get; set; }
    public string PostalCode { get; set; }
    public string State { get; set; }
    public string CountryCode { get; set; }
}
