using OracleHR.Helpers.queries;
using OracleHR.Models.dbModels;
using OracleHR.Repository.conn;
using OracleHR.Repository.interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OracleHR.Repository.repo
{
    public class LocationRepositoryImpl : ILocationRepository
    {
        private static string _connStr;
        private DBConnect _conn;

        public LocationRepositoryImpl(string connnectionString)
        {
            _connStr = connnectionString;
            _conn = new DBConnect(_connStr);
        }

        public async Task<List<Location>> GetLocationsAsync()
        {
            try
            {
                var results = new List<Location>();
                var getAllLocations = DBQueries.GET_LOCATIONS;
                var connect = await _conn.ConnectAndReturnReader(getAllLocations);
                var reader = connect.reader;
                var con = connect.connection;
                while (await reader.ReadAsync())
                {
                    results.Add(new Location
                    {
                        LocationId = Convert.ToInt32(reader.GetValue(0)),
                        StreetAddress = reader.GetValue(1).ToString(),
                        PostalCode = reader.GetValue(2).ToString(),
                        City = reader.GetValue(3).ToString(),
                        State = reader.GetValue(1).ToString(),
                        CountryId = reader.GetValue(2).ToString(),
                    });
                }
                con.Close();
                con.Dispose();
                return results;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
