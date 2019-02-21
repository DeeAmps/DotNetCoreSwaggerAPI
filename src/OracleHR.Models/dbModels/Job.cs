using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OracleHR.Models.dbModels
{
    public class Job
    {
        public string JobId { get; set; }
        public string JobTitle { get; set; }
        public int MinSalary { get; set; }
        public int MaxSalary { get; set; }
    }
}
