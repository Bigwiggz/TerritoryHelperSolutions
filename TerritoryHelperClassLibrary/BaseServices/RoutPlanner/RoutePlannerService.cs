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

namespace TerritoryHelperClassLibrary.BaseServices.RoutPlanner;

public class RoutePlannerService
{
    //TODO: Implement Route Planning using Azure Maps
    public async Task GetRouteDirectionsPerTerritory(List<AddressMasterRecord> masterRecordsList, TerritoryHelperConfiguration config)
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
            InstructionsType = RouteInstructionsType.Text

            //TODO: Enter Car/Vehicle information for good fuel approximation and costs.
        };

        var territoriesList=masterRecordsList.Select(x=>x.TerritoryNumber).Distinct().ToList();

        var routePlannerGeoJSON = new RoutePlannerGeoJSON();
        var routePlannerFeatureForLineString=new RoutePlannerFeature();

        foreach (var territory in territoriesList)
        {
            var addressList = masterRecordsList.Where(x => x.TerritoryNumber == territory).ToList();
            if (addressList.Count >= 3 && addressList is not null)
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

                //Remove first and last Route
                responseResult.Value.Routes[0].Legs.RemoveAt(0);
                responseResult.Value.Routes[0].Legs.RemoveAt(responseResult.Value.Routes.ToList().Count - 1);

                //TurnByTurnDirections
                var instructionsGroupList = responseResult.Value.Routes[0].Guidance.InstructionGroups.ToList();
                var instructionGroupStartIndex = instructionsGroupList[0].LastInstructionIndex;
                var instructionGroupEndIndex = instructionsGroupList.Last().FirstInstructionIndex;

                var instructionsList = responseResult.Value.Routes[0].Guidance.Instructions.ToList();
                instructionsList.RemoveRange(0, instructionGroupStartIndex.Value);
                instructionsList.RemoveRange(instructionGroupEndIndex.Value, instructionsList.Count);
                List<string> turnByTurnDirectionsList=new List<string>();
                
                foreach (var instruction in instructionsList)
                {
                    turnByTurnDirectionsList.Add(instruction.Message);
                }

                //Address List
                var optimalIndexList = responseResult.Value.OptimizedWaypoints.ToArray();
                var routePlannerAddressList=new List<RoutePlannerAddress>();
                foreach (var address in addressList)
                {
                    for (int i = 0; i < optimalIndexList.Length;i++)
                    {
                        var routePlannerAddress = new RoutePlannerAddress();
                        routePlannerAddress.CompleteAddress = address.CompleteAddress;
                        routePlannerAddress.Latitude = address.Latitude;
                        routePlannerAddress.Longitude = address.Longitude;
                        routePlannerAddress.LocationType= address.LocationType;
                        routePlannerAddress.Id = address.Id;
                        routePlannerAddress.MasterRecordRoutePlanOrder= optimalIndexList[i].OptimizedIndex.Value;

                        address.MasterRecordRoutePlanOrder = optimalIndexList[i].OptimizedIndex.Value;

                        routePlannerAddressList.Add(routePlannerAddress);
                    }
                    
                }

                var routePlannerProperties = new RoutePlannerProperties
                {
                    TerritoryNumber = addressList[0].TerritoryNumber,
                    TerritoryType= addressList[0].TerritoryType,
                    LineColor= "#a83232",
                    NumberOfAddresses=addressList.Count,
                    AddressesList= routePlannerAddressList.OrderBy(x=>x.MasterRecordRoutePlanOrder).ToList(),
                    TurnByTurnDirections= turnByTurnDirectionsList
                };

                var routePlannerGeometry = new RoutePlannerGeometry();
            }
        }
        routePlannerGeoJSON.features.Add(routePlannerFeatureForLineString);
    }
}
