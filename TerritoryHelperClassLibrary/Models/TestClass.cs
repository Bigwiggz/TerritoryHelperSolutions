using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerritoryHelperClassLibrary.Models;


public class Rootobject
{
    public Feature[] features { get; set; }
    public string type { get; set; }
}

public class Feature
{
    public Geometry geometry { get; set; }
    public Properties properties { get; set; }
    public string type { get; set; }
}

public class Geometry
{
    public float[][] coordinates { get; set; }
    public string type { get; set; }
}

public class Properties
{
    public string name { get; set; }
}

