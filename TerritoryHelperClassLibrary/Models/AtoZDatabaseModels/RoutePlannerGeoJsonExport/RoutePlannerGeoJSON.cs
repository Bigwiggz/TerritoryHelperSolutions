using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerritoryHelperClassLibrary.Models.AtoZDatabaseModels.RoutePlannerGeoJsonExport;

public class RoutePlannerGeoJSON
{
    public string type { get; set; } = "FeatureCollection";
    public List<RoutePlannerFeature> features { get; set; } = new List<RoutePlannerFeature>();
}

public class RoutePlannerFeature
{
    public string type { get; set; } = "Feature";
    public RoutePlannerProperties properties { get; set; }
    public RoutePlannerGeometry geometry { get; set; }
}

public class RoutePlannerProperties
{
    public string TerritoryType { get; set; } = "G0";
    public string TerritoryNumber { get; set; } = "00";
    public string LineColor { get; set; } = "#0000cc";
    public int NumberOfAddresses { get; set; }  
    public List<RoutePlannerAddress> AddressesList { get; set; }= new List<RoutePlannerAddress>();
    public RoutePlannerSummaryResults SummaryResults { get; set; }
    public List<string> TurnByTurnDirections { get; set; }
}

public class RoutePlannerGeometry
{
    public string type { get; set; } = "LineString";
    public object[] coordinates { get; set; }
}

public class RoutePlannerAddress
{
    //Order
    public Guid Id { get; set; }

    //Territory Helper Info
    public string LocationType { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public string CompleteAddress { get; set; }

    //Route Plan Order
    public int MasterRecordRoutePlanOrder { get; set; }

}

public class RoutePlannerSummaryResults
{
    public double RouteDistanceInMiles { get; set; }
    public TimeSpan RouteTravelTime { get; set; }   
    public double FuelConsumptionInGallons { get; set; }

}