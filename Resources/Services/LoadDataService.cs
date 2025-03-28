using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure;
using Microsoft.EntityFrameworkCore;
using UTSProject.Resources.Models;

namespace UTSProject.Resources.Services
{
    public class LoadDataService
    {
        private NTAService _nta;
        private DbService _db;
        private int _paginationStart;
        private int _paginationStep;
        public LoadDataService(NTAService nta, DbService db)
        {
            _nta = nta;
            _db = db;
            _paginationStart = 0;
            _paginationStep = 25;
        }
        public async Task<ObservableCollection<ConnectionDetailsModel>> LoadDataFromAPI(UserInput uInput)
        {
            //Showing the loading indicator
            //IndicatorIsVisible = true;

            //Initializing Collection
            ObservableCollection<ConnectionDetailsModel> Connections = new ObservableCollection<ConnectionDetailsModel>();

            //Using NTAService method for storing entities
            await _nta.ReadDataAsync(uInput.Time);

            // Add data to List

            return await AddToConnections(Connections);

            //Hiding the loading indicator
            //IndicatorIsVisible = false;

            //Setting the view to true
            //ConnectionsIsVisible = true;

            //Setting the Button to visible
            //ButtonIsVisible = true;
        }

        public async Task<ObservableCollection<ConnectionDetailsModel>> AddToConnections(ObservableCollection<ConnectionDetailsModel> connections)
        {
            await foreach (var entity in _nta.InitializeData())
            {
                string tripID = entity.TripUpdate.Trip.TripID;

                if (tripID == null)
                {
                    Console.WriteLine("Skipping due to tripID being null");
                    continue;
                }

                try
                {
                    var routeTask = _db.GetRouteDetails(tripID);
                    var stopsTask = _db.GetTripStops(tripID);

                    await Task.WhenAll(routeTask, stopsTask); // Run both tasks in parallel

                    var dbRouteDetails = routeTask.Result;
                    var dbStops = stopsTask.Result;

                    if (dbRouteDetails == null || dbStops.Count == 0)
                    {
                        Console.WriteLine($"Missing data for TripID: {entity.TripUpdate.Trip.TripID}");
                        continue; // Skip this iteration
                    }

                    connections.Add(new ConnectionDetailsModel
                    {
                        Date = entity.TripUpdate.Trip.StartDate,
                        Time = entity.TripUpdate.Trip.StartTime,
                        RouteShortName = dbRouteDetails.RouteShortName,
                        RouteLongName = dbRouteDetails.RouteLongName,
                        StartPoint = dbStops[0],
                        EndPoint = dbStops[dbStops.Count - 1],
                        Stops = dbStops
                    });
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error with adding Connections: {e}");
                }
            }
            return connections;
        }

        public async Task<ObservableCollection<ConnectionDetailsModel>> AddConnectionsFromDatabase(ObservableCollection<ConnectionDetailsModel> connections, UserInput input)
        {
            string stopName = "ATU Sligo";

            List<TripModel> trips = await _db.GetTripsByStopTimeDay(input.Date.DayOfWeek.ToString(),stopName, input.Time.ToString());

            //List<TripModel> test = new List<TripModel>();
            //test.Add(trips[0]);
            //test.Add(trips[1]);


            foreach (var t in trips.Skip(_paginationStart).Take(_paginationStep))
            {
                string tripID = t.TripID;

                if (tripID == null)
                {
                    Console.WriteLine("Skipping due to tripID being null");
                    continue;
                }

                try
                {
                    var routeTask = _db.GetRouteDetails(tripID);
                    var stopsTask = _db.GetTripStops(tripID);

                    await Task.WhenAll(routeTask, stopsTask); // Run both tasks in parallel

                    var dbRouteDetails = routeTask.Result;
                    var dbStops = stopsTask.Result;

                    var result = dbStops.FirstOrDefault(t => t.Name == stopName);

                    if (dbRouteDetails == null || dbStops.Count == 0)
                    {
                        Console.WriteLine($"Missing data for TripID: {t.TripID}");
                        continue; // Skip this iteration
                    }

                    connections.Add(new ConnectionDetailsModel
                    {
                        RouteShortName = dbRouteDetails.RouteShortName,
                        RouteLongName = dbRouteDetails.RouteLongName,
                        StartPoint = dbStops[0],
                        SearchedPoint = result,
                        EndPoint = dbStops[dbStops.Count - 1],
                        Stops = dbStops
                    });
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error with adding Connections: {e}");
                }
            }
            // Incrementing pagination
            _paginationStart += _paginationStep;
            return connections;
        }
    }
}
