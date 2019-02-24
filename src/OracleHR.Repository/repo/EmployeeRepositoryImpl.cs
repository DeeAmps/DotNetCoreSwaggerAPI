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
    public class EmployeeRepositoryImpl : IEmployeeRepository
    {

        private static string _connStr;
        private DBConnect _conn;

        public EmployeeRepositoryImpl(string connnectionString)
        {
            _connStr = connnectionString;
            _conn = new DBConnect(_connStr);
        }

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            try
            {
                var results = new List<Employee>();
                var getAllEmployees = DBQueries.GET_EMPLOYEES;
                var connect = await _conn.ConnectAndReturnReader(getAllEmployees);
                var reader = connect.reader;
                var con = connect.connection;
                while (await reader.ReadAsync())
                {
                    results.Add(new Employee
                    {
                        EmployeeId = Convert.ToInt32(reader.GetValue(0)),
                        FirstName = reader.GetValue(1).ToString(),
                        LastName = reader.GetValue(2).ToString(),
                        Email = reader.GetValue(3).ToString(),
                        PhoneNumber = reader.GetValue(4).ToString(),
                        HireDate = Convert.ToDateTime(reader.GetValue(5)).ToShortDateString(),
                        JobId = reader.GetValue(6).ToString(),
                        Salary = Convert.ToInt64(reader.GetValue(7)),
                        CommissionPct = Convert.ToInt32(reader.GetValue(8)),
                        ManagerId = Convert.ToInt32(reader.GetValue(9)),
                        DepartmentId = Convert.ToInt32(reader.GetValue(10))
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
