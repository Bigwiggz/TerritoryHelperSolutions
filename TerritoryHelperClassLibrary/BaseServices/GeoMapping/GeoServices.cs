using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TerritoryHelperClassLibrary.BaseServices.GeoMapping;

public class GeoServices
{
    public string ConvertObjecttoJSONString<T>(T geometryObject)
    {
        string geoJsonString = System.Text.Json.JsonSerializer.Serialize(geometryObject);

        return geoJsonString;
    }

    public NetTopologySuite.Geometries.Geometry ConvertGeoJSONstringToGeometry(string geoJSONstring)
    {
        var geoJsonOptions = new JsonSerializerOptions();
        geoJsonOptions.Converters.Add(new NetTopologySuite.IO.Converters.GeoJsonConverterFactory());
        var serializer = GeoJsonSerializer.Create();
        NetTopologySuite.Geometries.Geometry geoShape;
        using (var stringReader = new StringReader(geoJSONstring))
        {
            using (var jsonReader = new JsonTextReader(stringReader))
            {
                geoShape = serializer.Deserialize<NetTopologySuite.Geometries.Geometry>(jsonReader);
            }
        }
        return geoShape;
    }
    public bool IsLocationInTerritory(NetTopologySuite.Geometries.Geometry territoryShape, decimal latitude, decimal longitude)
    {
        var coordinate = new Coordinate();
        coordinate.X = (double)longitude;
        coordinate.Y = (double)latitude;
        var pointLocation = new Point(coordinate);

        bool IsLocationInTerritory = territoryShape.Contains(pointLocation);

        return IsLocationInTerritory;
    }
}
