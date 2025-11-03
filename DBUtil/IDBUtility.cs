using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Simpolo_Endpoint.DBUtil
{
    public interface  IDBUtility
    {

        Task<string> GetjsonData(string connectionString, string sp, Dictionary<string, object> sqlparems, bool IsSP = false);
        int GetSqlN(String Sql, string connectionstring);
        DataSet GetDS(string sP, string connectionString);
        Task<string> GetjsonDataFromDataset(string connectionString, string sp, Dictionary<string, object> sqlparems);
        Task<string> GetjsonDataFromDatasetINQ(string connectionString, string Query);
        Task ExecuteNonQuery(string connectionString, string procedureName, Dictionary<string, object> parameters);
        string GetSqlS(string Sql, string connectionstring);
        IDataReader GetRS(String Sql, string connectionstring);
        Task<DataTable> GetDataTable(string connectionString, string sp, Dictionary<string, object> sqlparems, bool IsSP = false);
        Task<string> GetJsonDataFromCommand(string connectionString, SqlCommand command);
        decimal GetDSqlN(string Sql, string connectionString);
    }
}