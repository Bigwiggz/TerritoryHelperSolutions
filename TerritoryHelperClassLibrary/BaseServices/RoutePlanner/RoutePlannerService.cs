using Azure.Maps.Routing;
using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerritoryHelperClassLibrary.Models;
using TerritoryHelperClassLibrary.Models.Configuration;
using Azure.Core.GeoJson;
using Azure.Maps.Routing.Models;
using TerritoryHelperClassLibrary.Models.AtoZDatabaseModels.RoutePlannerGeoJsonExport;
using OpenQA.Selenium.DevTools;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using System.Text.Json;

namespace TerritoryHelperClassLibrary.BaseServices.RoutePlanner;

public class RoutePlannerService
{

    public async Task<(List<AddressMasterRecord>,RoutePlannerGeoJSON)> GetRouteDirectionsPerTerritory(List<AddressMasterRecord> masterRecordsList, TerritoryHelperConfiguration config)
    {
        AzureKeyCredential credential = new AzureKeyCredential(config.MicrosoftAzureMapsPrimarySecurityKey);
        MapsRoutingClient packageClient = new MapsRoutingClient(credential);

        RouteDirectionOptions options = new RouteDirectionOptions
        {
            //TODO: Check all possible route options and read the "ComputeBestWaypointOrder" to determine how to add a start and finish destination
            ComputeBestWaypointOrder = true,
            RouteType = RouteType.Shortest,
            RouteRepresentationForBestOrder = RouteRepresentationForBestOrder.Polyline,
            TravelMode = TravelMode.Car,
            Language = RoutingLanguage.SpanishMexico,
            InstructionsType = RouteInstructionsType.Text,
            VehicleWeightInKilograms= 1621,
            IsCommercialVehicle= false,
            VehicleEngineType= VehicleEngineType.Combustion,
            ConstantSpeedConsumptionInLitersPerHundredKilometer= "50,6.3:130,11.5",
            CurrentFuelInLiters=60,
            AuxiliaryPowerInLitersPerHour=0.2,
            FuelEnergyDensityInMegajoulesPerLiter= 34.2,
            AccelerationEfficiency=0.33,
            DecelerationEfficiency=0.83,
            UphillEfficiency= 0.27,
            DownhillEfficiency= 0.51

            //TODO: Enter Car/Vehicle information for good fuel approximation and costs.
        };

        var territoriesList=masterRecordsList.Select(x=>x.TerritoryNumber).Distinct().ToList();

        var routePlannerGeoJSON = new RoutePlannerGeoJSON();

        foreach (var territory in territoriesList)
        {
            var addressList = masterRecordsList.Where(x => x.TerritoryNumber == territory && x.Status!="NO VISITAR").ToList();
            
            bool addressListIsAllTrailersOrApartments = true;
            if (addressList is not null && addressList.Count>1)
            {
                addressListIsAllTrailersOrApartments = addressList.All(x => x.CompleteAddress.Contains("Apt") || x.CompleteAddress.Contains("Lot"));
            }    

            if (addressList.Count >= 3 && addressList is not null && addressListIsAllTrailersOrApartments==false)
            {
                List<GeoPosition> routePoints = new List<GeoPosition>();
                routePoints.Add(new GeoPosition(config.KingdomHallLocationLongitude, config.KingdomHallLocationLatitude));
                
                foreach (var address in addressList)
                {
                    routePoints.Add(new GeoPosition((double)address.Longitude, (double)address.Latitude));
                }

                routePoints.Add(new GeoPosition(config.KingdomHallLocationLongitude, config.KingdomHallLocationLatitude));

                RouteDirectionQuery query = new RouteDirectionQuery(routePoints, options);
                Response<RouteDirections> responseResult =await packageClient.GetDirectionsAsync(query);

                //Test
                var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
                var jsonTextResult = System.Text.Json.JsonSerializer.Serialize(responseResult, jsonOptions);
                await File.WriteAllTextAsync(Path.Combine(config.FileSavedOutputLocation, "testFileResponseResult.json"), jsonTextResult);
                //End Test

                //Points List & Route Planning
                var routeLegList = responseResult.Value.Routes[0].Legs.ToList();
                routeLegList.RemoveAt(0);
                routeLegList.RemoveAt(routeLegList.Count - 1);

                var lineCoordinatesList = new List<object>();
                var routePlannerSummaryResults = new RoutePlannerSummaryResults();

                foreach (var leg in routeLegList)
                {
                    foreach(var point in leg.Points)
                    {
                        lineCoordinatesList.Add(new double[] { point.Longitude,point.Latitude });
                    }
                }

                var lengthInMeters=routeLegList[routeLegList.Count - 1].Summary.LengthInMeters.Value - routeLegList[0].Summary.LengthInMeters.Value;
                var fuelConsumptionInLiters= routeLegList[routeLegList.Count - 1].Summary.FuelConsumptionInLiters.Value - routeLegList[0].Summary.FuelConsumptionInLiters.Value;
                var timeSpanofTravel= routeLegList[routeLegList.Count - 1].Summary.TravelTimeInSeconds.Value - routeLegList[0].Summary.TravelTimeInSeconds.Value;

                routePlannerSummaryResults.RouteDistanceInMiles = lengthInMeters * 0.000621371;
                routePlannerSummaryResults.FuelConsumptionInGallons = fuelConsumptionInLiters * 0.264172;
                routePlannerSummaryResults.RouteTravelTime = TimeSpan.FromSeconds(timeSpanofTravel);

                var routePlannerGeometry = new RoutePlannerGeometry
                {
                    type = "LineString",
                    coordinates = lineCoordinatesList.ToArray()
                };

                //TurnByTurnDirections
                var instructionsGroupList = responseResult.Value.Routes[0].Guidance.InstructionGroups.ToList();
                var instructionGroupStartIndex = instructionsGroupList[0].LastInstructionIndex;
                var instructionGroupEndIndex = instructionsGroupList.Last().FirstInstructionIndex;
                var endPortionStartValue = instructionGroupEndIndex.Value - instructionGroupStartIndex.Value;
                var removalRange= instructionsGroupList.Last().LastInstructionIndex- instructionGroupEndIndex;
                var instructionsList = responseResult.Value.Routes[0].Guidance.Instructions.ToList();
                instructionsList.RemoveRange(0, instructionGroupStartIndex.Value);
                instructionsList.RemoveRange(endPortionStartValue, removalRange.Value);
                List<string> turnByTurnDirectionsList=new List<string>();
                
                foreach (var instruction in instructionsList)
                {
                    turnByTurnDirectionsList.Add(instruction.Message);
                }

                //Address List
                var optimalIndexList = responseResult.Value.OptimizedWaypoints.ToArray();
                var routePlannerAddressList=new List<RoutePlannerAddress>();
                int i = 0;
                foreach (var address in addressList)
                {
                    
                    var routePlannerAddress = new RoutePlannerAddress();
                    routePlannerAddress.CompleteAddress = address.CompleteAddress;
                    routePlannerAddress.Latitude = address.Latitude;
                    routePlannerAddress.Longitude = address.Longitude;
                    routePlannerAddress.LocationType= address.LocationType;
                    routePlannerAddress.Id = address.Id;
                    routePlannerAddress.MasterRecordRoutePlanOrder= optimalIndexList.Where(x=>x.ProvidedIndex==i).Select(y=>y.OptimizedIndex).FirstOrDefault().Value; 

                    address.MasterRecordRoutePlanOrder = optimalIndexList.Where(x => x.ProvidedIndex == i).Select(y => y.OptimizedIndex).FirstOrDefault().Value;
                    routePlannerAddressList.Add(routePlannerAddress);
                    i++;
                }
                var sortedAddressesList = routePlannerAddressList.OrderBy(x => x.MasterRecordRoutePlanOrder).ToList();
                var routePlannerProperties = new RoutePlannerProperties
                {
                    TerritoryNumber = addressList[0].TerritoryNumber,
                    TerritoryType= addressList[0].TerritoryType,
                    LineColor= "a83232",
                    NumberOfAddresses=addressList.Count,
                    AddressesList= sortedAddressesList,
                    TurnByTurnDirections= turnByTurnDirectionsList,
                    SummaryResults = routePlannerSummaryResults
                };

                var routePlannerFeature = new RoutePlannerFeature
                {
                    properties = routePlannerProperties,
                    geometry = routePlannerGeometry,
                    type = "Feature"
                };

                //Test
                var jsonText = System.Text.Json.JsonSerializer.Serialize(routePlannerFeature, jsonOptions);
                await File.WriteAllTextAsync(Path.Combine(config.FileSavedOutputLocation,"testFileJsonFeature.json"),jsonText);
                //End Test

                routePlannerGeoJSON.features.Add(routePlannerFeature);
            }
        }
        return (masterRecordsList, routePlannerGeoJSON);
    }
}
