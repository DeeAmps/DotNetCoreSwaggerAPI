using Oracle.ManagedDataAccess.Client;
using System.Data.Common;

namespace OracleHR.Models.dbConnectModels
{
    public class Connect
    {
        public DbDataReader reader { get; set; }
        public OracleConnection connection { get; set; }
    }
}
