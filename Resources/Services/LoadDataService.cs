using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Maui.Graphics.Text;
using UTSProject.Resources.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace UTSProject.Resources.Services
{
    public class LoadDataService
    {
        // Private properties & DI objects
        private NTAService _nta;
        private DbService _db;
        private List<TripModel> _trips;
        private UserInput _userInput;
        private int _paginationStart;
        private int _paginationStep;
        private string _stopName;
        // Public properties
        public ObservableCollection<ConnectionDetailsModel> Connections;
        public LoadDataService(NTAService nta, DbService db)
        {
            _nta = nta; //Assigning DI services to private properties
            _db = db;
            Connections = new ObservableCollection<ConnectionDetailsModel>(); // Initializing Collection of Connections
            _stopName = "ATU Sligo"; //TEMP VARIABLE -> Will be removed in the future
            // Initialization of Pagination
            _paginationStart = 0;
            _paginationStep = 25;
        }
        public async Task<ObservableCollection<ConnectionDetailsModel>> LoadDataFromAPI(UserInput uInput) // Method Used to load data from an API (USING NTAService)
        {
            //Initializing Collection
            ObservableCollection<ConnectionDetailsModel> Connections = new ObservableCollection<ConnectionDetailsModel>();

            //Using NTAService method for storing entities
            await _nta.ReadDataAsync(uInput.Time);

            return await AddToConnections(Connections);
        }

        public async Task<ObservableCollection<ConnectionDetailsModel>> AddToConnections(ObservableCollection<ConnectionDetailsModel> connections) // Used for adding data to public property Connections when loading from an API 
        {
            await foreach (var entity in _nta.InitializeData())
            {
                string tripID = entity.TripUpdate.Trip.TripID; // Dynamic variable

                //Checks if the TripID is not null
                if (tripID == null)
                {
                    Console.WriteLine("Skipping due to tripID being null");
                    continue;
                }

                try
                {
                    // Database queries/tasks
                    var routeTask = _db.GetRouteDetails(tripID);
                    var stopsTask = _db.GetTripStops(tripID);

                    // Run both tasks in parallel
                    await Task.WhenAll(routeTask, stopsTask);

                    // Save the results
                    var dbRouteDetails = routeTask.Result;
                    var dbStops = stopsTask.Result;

                    // Checks if the results are not null
                    if (dbRouteDetails == null || dbStops.Count == 0)
                    {
                        Console.WriteLine($"Missing data for TripID: {entity.TripUpdate.Trip.TripID}");
                        continue; // Skip this iteration
                    }

                    connections.Add(new ConnectionDetailsModel
                    {
                        Date = DateTime.Parse(entity.TripUpdate.Trip.StartDate),
                        Time = TimeSpan.Parse(entity.TripUpdate.Trip.StartTime),
                        RouteShortName = dbRouteDetails.RouteShortName,
                        RouteLongName = dbRouteDetails.RouteLongName,
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

        public async Task<bool> AddConnectionsFromDatabase(UserInput input) // Handles adding new connections based on the user input to public property Connections
        {

            _paginationStart = 0; // Reset pagination
            Connections.Clear(); // Clear the Connections
            // Clear the List
            if (_trips != null)
            {
                _trips.Clear();
            }

            _userInput = input; // Store the user input
            _trips = await _db.GetTripsByStopTimeDay(input.Date.DayOfWeek.ToString(), _stopName, input.Time.ToString());  // Load the trips from database into private property
            Connections = await InitializeConnections(_stopName, Connections); // Load the connections into public property

            return true;
        }

        private async Task<ObservableCollection<ConnectionDetailsModel>> InitializeConnections(string stopName, ObservableCollection<ConnectionDetailsModel> connections) // Filters connections and saves them to a new list
        {
            foreach (var t in _trips.Skip(_paginationStart).Take(_paginationStep))
            {
                string tripID = t.TripID; // Dynamic Variable

                if (tripID == null)
                {
                    Console.WriteLine("Skipping due to tripID being null");
                    continue;
                }

                try
                {
                    var routeTask = _db.GetRouteDetails(tripID);
                    var stopsTask = _db.GetTripStops(tripID);

                    await Task.WhenAll(routeTask, stopsTask);

                    var dbRouteDetails = routeTask.Result;
                    var dbStops = stopsTask.Result;

                    var result = dbStops.FirstOrDefault(t => t.Name == stopName); // Searching for searched stop in the stops list

                    if (dbRouteDetails == null || 
                        dbStops.Count == 0 || 
                        dbStops[dbStops.Count - 1].Name == stopName
                        )
                    {
                        Console.WriteLine($"Missing data for TripID: {t.TripID}");
                        continue; // Skip this iteration
                    }

                    connections.Add(new ConnectionDetailsModel
                    {
                        Date = _userInput.Date,
                        Time = _userInput.Date.TimeOfDay,
                        IsNotSaved = true,
                        RouteShortName = dbRouteDetails.RouteShortName,
                        RouteLongName = dbRouteDetails.RouteLongName,
                        SearchedStop = result,
                        EndStop = dbStops[dbStops.Count - 1],
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

        public async Task<ObservableCollection<ConnectionDetailsModel>> LoadAnother() // Loads another set of connections into Connections property
        {
            return await InitializeConnections(_stopName, Connections);
        }
    }
}
