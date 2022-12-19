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
            //Where is this?
            RouteRepresentationForBestOrder = RouteRepresentationForBestOrder.Polyline,
            TravelMode = TravelMode.Car,
            Language = RoutingLanguage.SpanishMexico,
            InstructionsType = RouteInstructionsType.Text

            //TODO: Enter Car/Vehicle information for good fuel approximation and costs.
        };

        var territoriesList=masterRecordsList.Select(x=>x.TerritoryNumber).Distinct().ToList();

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

                //TODO: get responseResult and put it into custom object
            }
        }
    }
}
