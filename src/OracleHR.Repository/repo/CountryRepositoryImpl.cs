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
    public class CountryRepositoryImpl : ICountryRepository
    {
        private static string _connStr;
        private DBConnect _conn;

        public CountryRepositoryImpl(string connnectionString)
        {
            _connStr = connnectionString;
            _conn = new DBConnect(_connStr);
        }

        public async Task<List<Country>> GetCountriesAsync()
        {
            try
            {
                var results = new List<Country>();
                var getAllCountries = DBQueries.GET_COUNTRIES;
                var connect = await _conn.ConnectAndReturnReader(getAllCountries);
                var reader = connect.reader;
                var con = connect.connection;
                while (await reader.ReadAsync())
                {
                    results.Add(new Country
                    {
                        CountryId = reader.GetValue(0).ToString(),
                        CountryName = reader.GetValue(1).ToString(),
                        RegionId = Convert.ToInt32(reader.GetValue(2))
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
