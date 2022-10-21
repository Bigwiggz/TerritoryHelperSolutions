using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerritoryHelperClassLibrary.Models.AtoZDatabaseModels.GeoJSONExport;

public class AllAddressesGeoJSON
{
    public string type { get; set; } = "FeatureCollection";
    public Feature[] features { get; set; }
}

public class Feature
{
    public string type { get; set; } = "Feature";
    public Properties properties { get; set; }
    public Geometry geometry { get; set; }
}

public class Properties
{
    public string Id { get; set; }
    public string TerritoryType { get; set; } = "G0";
    public string TerritoryNumber { get; set; } = "00";
    public string FullAddress { get; set; }
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
    public string LocationCoordinates { get; set; }
    public string PhoneNumbers { get; set; }
    public string NamesList { get; set; }
    public string AddressType { get; set; }
}
public class Geometry
{
    public string type { get; set; } = "Point";
    public decimal[] coordinates { get; set; }
}
