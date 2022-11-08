using GeoAPI.Geometries;
using NetTopologySuite.Geometries;
using ProjNet;
using ProjNet.CoordinateSystems;
using ProjNet.CoordinateSystems.Transformations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICoordinateSequenceFilter = GeoAPI.Geometries.ICoordinateSequenceFilter;
using Ordinate = NetTopologySuite.Geometries.Ordinate;

namespace TerritoryHelperTestConsole.Extensions;

public static class GeometryExtensions
{
    private static readonly CoordinateSystemServices _coordinateSystemServices
            = new CoordinateSystemServices(
                new Dictionary<int, string>
                {
                    // Coordinate systems:

                    [4326] = GeographicCoordinateSystem.WGS84.WKT,
                    // This coordinate system covers the area of our data.
                    // Different data requires a different coordinate system.

                    //Google Maps and World Map Base Geo Projection
                    [3857] =
                    @"
					PROJCS[""WGS 84 / Pseudo-Mercator"",
						GEOGCS[""WGS 84"",
							DATUM[""WGS_1984"",
								SPHEROID[""WGS 84"",6378137,298.257223563,
									AUTHORITY[""EPSG"",""7030""]],
								AUTHORITY[""EPSG"",""6326""]],
							PRIMEM[""Greenwich"",0,
								AUTHORITY[""EPSG"",""8901""]],
							UNIT[""degree"",0.0174532925199433,
								AUTHORITY[""EPSG"",""9122""]],
							AUTHORITY[""EPSG"",""4326""]],
						PROJECTION[""Mercator_1SP""],
						PARAMETER[""central_meridian"",0],
						PARAMETER[""scale_factor"",1],
						PARAMETER[""false_easting"",0],
						PARAMETER[""false_northing"",0],
						UNIT[""metre"",1,
							AUTHORITY[""EPSG"",""9001""]],
						AXIS[""X"",EAST],
						AXIS[""Y"",NORTH],
						EXTENSION[""PROJ4"",""+proj=merc +a=6378137 +b=6378137 +lat_ts=0.0 +lon_0=0.0 +x_0=0.0 +y_0=0 +k=1.0 +units=m +nadgrids=@null +wktext  +no_defs""],
						AUTHORITY[""EPSG"",""3857""]]
					",

                    //South Carolina state plane coordinates projection for Richland County SC USA
                    [102733] =
                    @"
					PROJCS[""NAD_1983_StatePlane_South_Carolina_FIPS_3900_Feet"",
						GEOGCS[""GCS_North_American_1983"",
							DATUM[""North_American_Datum_1983"",
								SPHEROID[""GRS_1980"",6378137,298.257222101]],
							PRIMEM[""Greenwich"",0],
							UNIT[""Degree"",0.017453292519943295]],
						PROJECTION[""Lambert_Conformal_Conic_2SP""],
						PARAMETER[""False_Easting"",1999996],
						PARAMETER[""False_Northing"",0],
						PARAMETER[""Central_Meridian"",-81],
						PARAMETER[""Standard_Parallel_1"",32.5],
						PARAMETER[""Standard_Parallel_2"",34.83333333333334],
						PARAMETER[""Latitude_Of_Origin"",31.83333333333333],
						UNIT[""Foot_US"",0.30480060960121924],
						AUTHORITY[""EPSG"",""102733""]]
					"

                });

    public static Geometry ProjectTo(this Geometry geometry, int srid)
    {
        var transformation = _coordinateSystemServices.CreateTransformation(geometry.SRID, srid);

        var result = geometry.Copy();
        result.Apply(new MathTransformFilter(transformation.MathTransform));

        return result;
    }

    private class MathTransformFilter : NetTopologySuite.Geometries.ICoordinateSequenceFilter
    {
        private readonly MathTransform _transform;

        public MathTransformFilter(MathTransform transform)
            => _transform = transform;

        public bool Done => false;
        public bool GeometryChanged => true;

        public void Filter(CoordinateSequence seq, int i)
        {
            var x = seq.GetX(i);
            var y = seq.GetY(i);
            var z = seq.GetZ(i);
            _transform.Transform(ref x, ref y, ref z);
            seq.SetX(i, x);
            seq.SetY(i, y);
            seq.SetZ(i, z);
        }
    }
}
