using OracleHR.Helpers.queries;
using OracleHR.Models.dbModels;
using OracleHR.Repository.conn;
using OracleHR.Repository.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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

        private async Task<int> GetRegionLastId()
        {
            try
            {
                var RegionLastInsertQuery = String.Format(DBQueries.GET_LAST_REGION_ID);
                var connect = await _conn.ConnectAndReturnReader(RegionLastInsertQuery);
                var reader = connect.reader;
                var con = connect.connection;
                int lastId = await reader.GetFieldValueAsync<int>(0);
                con.Close();
                con.Dispose();
                return lastId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Region> AddNewRegionAsync(Region region)
        {
            try
            {
                int regionLastId = await GetRegionLastId();
                int insertId = regionLastId + 1;
                var RegionInsertQuery = String.Format(DBQueries.INSERT_NEW_REGION, insertId, region.RegionName);
                var connect = await _conn.ConnectAndReturnReader(RegionInsertQuery);
                var con = connect.connection;
                con.Close();
                con.Dispose();
                var confirmInsert = await GetRegionsAsync();
                if (confirmInsert.FirstOrDefault(p => p.RegionId == insertId).RegionId == insertId)
                {
                    return new Region { RegionId = insertId, RegionName = region.RegionName };

                }
                return new Region { RegionId = 0, RegionName = "" };

            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public async Task<int> DeleteRegionAsync(int regionId)
        {
            try
            {
                var confirmRegionExists = await GetRegionsAsync();
                if (confirmRegionExists.FirstOrDefault(p => p.RegionId == regionId) == null)
                {
                    return 0;
                }
                var RegionDeleteQuery = String.Format(DBQueries.REMOVE_REGION, regionId);
                var connect = await _conn.ConnectAndReturnReader(RegionDeleteQuery);
                var con = connect.connection;
                con.Close();
                con.Dispose();
                return 1;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Region>> GetRegionsAsync()
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

        public async Task<Region> UpdateRegionAsync(Region region, int regionId)
        {
            try
            {
                var RegionUpdateQuery = String.Format(DBQueries.UPDATE_REGION, regionId, region.RegionName);
                var connect = await _conn.ConnectAndReturnReader(RegionUpdateQuery);
                var con = connect.connection;
                con.Close();
                con.Dispose();
                var confirmInsert = await GetRegionsAsync();
                if (confirmInsert.FirstOrDefault(p => p.RegionId == regionId).RegionName == region.RegionName)
                {
                    return confirmInsert.FirstOrDefault(p => p.RegionId == regionId);
                }
                return new Region { RegionId = 0, RegionName = "" };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
