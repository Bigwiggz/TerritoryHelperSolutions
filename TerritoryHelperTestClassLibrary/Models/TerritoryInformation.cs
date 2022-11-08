using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerritoryHelperTestClassLibrary.Models;

public class TerritoryInformation
{
    //properties
    public string name { get; set; }
    public string description { get; set; }
    public string TerritoryType { get; set; }
    public object TerritoryTypeCode { get; set; }
    public string TerritoryTypeColor { get; set; }
    public string TerritoryNumber { get; set; }
    public string TerritoryNotes { get; set; }

    //geometry properties
    public double TerritoryAreaSqMiles { get; set; }
    public double TerritoryCenterLatitude { get; set; }
    public double TerritoryCenterLongitude { get; set; }
    public double DistanceFromKingdomHall { get; set; }
    public double TerritoryPerimeterMiles { get; set; }
    public string TerritoryWKT { get; set; }
}
