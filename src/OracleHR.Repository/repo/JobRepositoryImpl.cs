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
    public class JobRepositoryImpl : IJobRepository
    {
        private static string _connStr;
        private DBConnect _conn;

        public JobRepositoryImpl(string connnectionString)
        {
            _connStr = connnectionString;
            _conn = new DBConnect(_connStr);
        }

        public async Task<List<Job>> GetJobsAsync()
        {
            try
            {
                var results = new List<Job>();
                var getAllJobs = DBQueries.GET_JOBS;
                var connect = await _conn.ConnectAndReturnReader(getAllJobs);
                var reader = connect.reader;
                var con = connect.connection;
                while (await reader.ReadAsync())
                {
                    results.Add(new Job
                    {
                        JobId = reader.GetValue(0).ToString(),
                        JobTitle = reader.GetValue(1).ToString(),
                        MinSalary = Convert.ToInt32(reader.GetValue(2)),
                        MaxSalary = Convert.ToInt32(reader.GetValue(3))
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
