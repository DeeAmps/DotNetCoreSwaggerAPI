using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OracleHR.Models.dbModels
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string HireDate { get; set; }
        public string JobId { get; set; }
        public long Salary { get; set; }
        public int CommissionPct { get; set; }
        public int ManagerId { get; set; }
        public int DepartmentId { get; set; }
    }
}
