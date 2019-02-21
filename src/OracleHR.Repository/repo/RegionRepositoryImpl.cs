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
    public class RegionRepositoryImpl : IRegionRepository
    {
        private static string _connStr;
        private DBConnect _conn;

        public RegionRepositoryImpl(string connnectionString)
        {
            _connStr = connnectionString;
            _conn = new DBConnect(_connStr); 
        }

        public Task<Region> AddNewRegion(Region region)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteRegion(int regionId)
        {
            throw new NotImplementedException();
        }

        public Task<Region> GetRegion(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Region>> GetRegions()
        {
            try
            {
                var results = new List<Region>();
                var getAllRegionsQuery = DBQueries.GET_REGIONS;
                var connect = await _conn.ConnectAndReturnReader(getAllRegionsQuery);
                var reader = connect.reader;
                var con = connect.connection;
                while (await reader.ReadAsync())
                {
                    results.Add(new Region
                    {
                        RegionId = Convert.ToInt32(reader.GetValue(0)),
                        RegionName = reader.GetValue(1).ToString()
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

        public Task<Region> UpdateRegion(Region region, int regionId)
        {
            throw new NotImplementedException();
        }
    }
}
