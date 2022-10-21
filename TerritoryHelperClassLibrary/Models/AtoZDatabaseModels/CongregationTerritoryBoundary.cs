using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerritoryHelperClassLibrary.Models.AtoZDatabaseModels;

public class CongregationTerritoryBoundary
{
    public string type { get; set; }
    public FeatureBoundary[] features { get; set; }
}

public class FeatureBoundary
{
    public string type { get; set; }
    public Properties properties { get; set; }
    public GeometryBoundary geometry { get; set; }
}

public class GeometryBoundary
{
    public string type { get; set; }
    public float[][][] coordinates { get; set; }
}
