using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TerritoryHelperClassLibrary.BaseServices.GeoMapping;
using TerritoryHelperClassLibrary.Models.AtoZDatabaseModels;
using TerritoryHelperClassLibrary.Models.Configuration;
using TerritoryHelperTestClassLibrary.Models;
using TerritoryHelperTestConsole.Extensions;

namespace TerritoryHelperTestConsole.TopLevelServices;

public class TerritoryGeoProcessing
{
    public CongregationTerritories CreateNTSGeometryPerTerritory(TerritoryHelperConfiguration config)
    {
        var congregationTerritoriesText = File.ReadAllText(config.TerritoriesFilePath);
        var congregationTerritoriesList = JsonSerializer.Deserialize<CongregationTerritories>(congregationTerritoriesText);

        var GeoFileProcessing = new GeoServices();

        foreach (var territory in congregationTerritoriesList.features)
        {
            var congGeometryObject = GeoFileProcessing.ConvertObjecttoJSONString(territory.geometry);
            territory.properties.TerritoryGeometry = GeoFileProcessing.ConvertGeoJSONstringToGeometry(congGeometryObject);
            territory.properties.TerritoryGeometry.SRID = 4326;
        }

        return congregationTerritoriesList;
    }

    public List<TerritoryInformation> CreateTerritoryInformationList(CongregationTerritories congregationTerritories, Point kingdomHallLocation)
    {
        List<TerritoryInformation> territoryInformationList = new();

        foreach (var territory in congregationTerritories.features)
        {
            var territoryArea = territory.properties.TerritoryGeometry.ProjectTo(102733).Area/ 27878400;
            var territoryPerimeter = territory.properties.TerritoryGeometry.ProjectTo(102733).Length / 5280;
            var territoryCentroid = territory.properties.TerritoryGeometry.Centroid;
            var territoryDistance = territoryCentroid.ProjectTo(102733).Distance(kingdomHallLocation.ProjectTo(102733))/5280;
            var territoryWKT = territory.properties.TerritoryGeometry.ToText();

            TerritoryInformation territoryInfo = new TerritoryInformation
            {
                TerritoryAreaSqMiles = territoryArea,
                TerritoryCenterLatitude=territoryCentroid.Y,
                TerritoryCenterLongitude=territoryCentroid.X,   
                TerritoryPerimeterMiles=territoryPerimeter,
                DistanceFromKingdomHall=territoryDistance,
                TerritoryWKT=territoryWKT,  

                name = territory.properties.name,
                description=territory.properties.description,
                TerritoryNotes=territory.properties.TerritoryNotes,
                TerritoryType=territory.properties.TerritoryType,
                TerritoryNumber=territory.properties.TerritoryNumber,
                TerritoryTypeCode=territory.properties.TerritoryTypeCode,
                TerritoryTypeColor=territory.properties.TerritoryTypeColor,
            };

            territoryInformationList.Add(territoryInfo);
        }

        return territoryInformationList;
    }
}
