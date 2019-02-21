using Oracle.ManagedDataAccess.Client;
using OracleHR.Models.dbConnectModels;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace OracleHR.Repository.conn
{
    public class DBConnect
    {
        private static string _dbConnStr;

        public DBConnect(string connStr)
        {
            _dbConnStr = connStr;
        }
        public async Task<Connect> ConnectAndReturnReader(string query)
        {
            try
            {
                OracleCommand cmd = null;
                DbDataReader reader = null;
                OracleConnection con = new OracleConnection(_dbConnStr);
                await con.OpenAsync();
                cmd = new OracleCommand(query, con);
                cmd.CommandType = System.Data.CommandType.Text;
                reader = await cmd.ExecuteReaderAsync();
                return new Connect
                {
                    reader = reader,
                    connection = con
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
