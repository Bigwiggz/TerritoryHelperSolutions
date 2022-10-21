using NetTopologySuite.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerritoryHelperClassLibrary.Models.AtoZDatabaseModels;

public class CongregationTerritories
{
    public string type { get; set; }
    public Feature[] features { get; set; }
}

public class Feature
{
    public string type { get; set; }
    public Properties properties { get; set; }
    public Geometry geometry { get; set; }
}

public class Properties
{
    public string name { get; set; }
    public string description { get; set; }
    public string TerritoryType { get; set; }
    public object TerritoryTypeCode { get; set; }
    public string TerritoryTypeColor { get; set; }
    public string TerritoryNumber { get; set; }
    public string TerritoryNotes { get; set; }
    public NetTopologySuite.Geometries.Geometry TerritoryGeometry { get; set; }
}

public class Geometry
{
    public string type { get; set; }
    public float[][][] coordinates { get; set; }
}
