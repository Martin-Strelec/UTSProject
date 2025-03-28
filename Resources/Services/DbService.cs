using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using UTSProject.Resources.Models;
using System.Globalization;
using System.Diagnostics;
using System.Collections.ObjectModel;

public class DbService
{
    private readonly string _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=NTALocalDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

    public async Task<List<StopModel>> GetTripStops(string tripID)
    {
        using (SqlConnection conn = new(_connectionString))
        {
            var stops = new List<StopModel>();
            try
            {
                await conn.OpenAsync();
                using (SqlCommand cmd = new("GetTripStops", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ETripID", tripID); // Add input parameter

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            stops.Add(new StopModel
                            {
                                Name = reader.GetString(1),
                                Latitude = reader.GetDouble(2),
                                Longtitude = reader.GetDouble(3),
                                ArrivalTime = reader.GetTimeSpan(4),
                                DepartureTime = reader.GetTimeSpan(5),
                            });
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Database Error (GetRouteDetails): {e}");
            }
            return stops;
        }
    }

    public async Task<List<TripModel>> GetAllTripsForStop(string stopName)
    {
        using (SqlConnection conn = new(_connectionString))
        {
            var trips = new List<TripModel>();
            try
            {
                await conn.OpenAsync();
                using (SqlCommand cmd = new("GetAllTripsForStopName", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EStopName", stopName); // Add input parameter

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            trips.Add(new TripModel
                            {
                                TripID = reader.GetString(0)
                            });
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Database Error (GetRouteDetails): {e}");
            }
            return trips;
        }
    }

    public async Task<RouteModel> GetRouteDetails(string tripID)
    {
        using (SqlConnection conn = new(_connectionString))
        {
            RouteModel route = new RouteModel();
            try
            {
                await conn.OpenAsync();
                using (SqlCommand cmd = new("GetRouteDetails", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ETripID", tripID); // Add input parameter

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            route = new RouteModel
                            {
                                RouteLongName = reader.GetString(0),
                                RouteShortName = reader.GetString(1),
                                AgencyName = reader.GetString(2),
                                AgencyURL = reader.GetString(3),
                            };
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Database Error (GetRouteDetails): {e}");
            }
            return route;
        }
    }

    public async Task<List<TripModel>> GetTripsByStopAndTime(string stopName, string time)
    {
        using (SqlConnection conn = new(_connectionString))
        {
            var trips = new List<TripModel>();
            try
            {
                await conn.OpenAsync();
                using (SqlCommand cmd = new("GetTripsByStopAndTime", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EStopName", stopName); // Add input parameter
                    cmd.Parameters.AddWithValue("@EStopTime", time); // Add input parameter

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            trips.Add(new TripModel
                            {
                                TripID = reader.GetString(0)
                            });
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Database Error (GetRouteDetails): {e}");
            }
            return trips;
        }
    }

    public async Task<List<TripModel>> GetTripsByStopTimeDay(string day,string stopName, string time)
    {
        using (SqlConnection conn = new(_connectionString))
        {
            var trips = new List<TripModel>();
            try
            {
                await conn.OpenAsync();
                using (SqlCommand cmd = new("GetTripsByStop_Time_Day", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EDay", day.ToLower()); // Add input parameter
                    cmd.Parameters.AddWithValue("@EStopName", stopName); // Add input parameter
                    cmd.Parameters.AddWithValue("@EStopTime", time); // Add input parameter

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            trips.Add(new TripModel
                            {
                                TripID = reader.GetString(0)
                            });
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Database Error (GetRouteDetails): {e}");
            }
            return trips;
        }
    }

    public async Task<List<TripModel>> GetRoutesByStopAndTime(string stopName, string time)
    {
        using (SqlConnection conn = new(_connectionString))
        {
            var trips = new List<TripModel>();
            try
            {
                await conn.OpenAsync();
                using (SqlCommand cmd = new("GetTripsByStopAndTime", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EStopName", stopName); // Add input parameter
                    cmd.Parameters.AddWithValue("@EStopTime", time); // Add input parameter

                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            trips.Add(new TripModel
                            {
                                TripID = reader.GetString(0)
                            });
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Database Error (GetRouteDetails): {e}");
            }
            return trips;
        }
    }
}
