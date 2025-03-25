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
}
