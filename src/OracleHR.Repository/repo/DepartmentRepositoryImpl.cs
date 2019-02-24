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
    public class DepartmentRepositoryImpl : IDepartmentRepository
    {
        private static string _connStr;
        private DBConnect _conn;

        public DepartmentRepositoryImpl(string connnectionString)
        {
            _connStr = connnectionString;
            _conn = new DBConnect(_connStr);
        }

        public async Task<List<Department>> GetDepartmentsAsync()
        {
            try
            {
                var results = new List<Department>();
                var getAllDepartments = DBQueries.GET_DEPARTMENTS;
                var connect = await _conn.ConnectAndReturnReader(getAllDepartments);
                var reader = connect.reader;
                var con = connect.connection;
                while (await reader.ReadAsync())
                {
                    results.Add(new Department
                    {
                        DepartmentId = Convert.ToInt32(reader.GetValue(0)),
                        DepartmentName = reader.GetValue(1).ToString(),
                        ManagerId = Convert.ToInt32(reader.GetValue(2)),
                        LocationId = Convert.ToInt32(reader.GetValue(3))
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
