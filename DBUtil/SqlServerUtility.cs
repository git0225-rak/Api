using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Web;

namespace Simpolo_Endpoint.DBUtil
{
    public class SqlServerUtility : IDBUtility
    {
        public Task<string> GetjsonData(string connectionString, string sp, Dictionary<string, object> sqlparems, bool IsSP = false)
        {
            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    sqlConnection.Open();
                    try
                    {

                        SqlCommand sqlCommand = new SqlCommand(sp, sqlConnection)
                        {
                            CommandType = CommandType.StoredProcedure,
                            CommandTimeout = sqlConnection.ConnectionTimeout
                        };
                        foreach (KeyValuePair<string, object> parametar in sqlparems)
                            sqlCommand.Parameters.AddWithValue(parametar.Key, parametar.Value);

                        StringBuilder executedQuery = new StringBuilder(sp);
                        executedQuery.Append(" ");

                        foreach (SqlParameter parameter in sqlCommand.Parameters)
                        {
                            executedQuery.Append(parameter.ParameterName + " = " + parameter.Value + ", ");
                        }
                        executedQuery.Remove(executedQuery.Length - 2, 2);
                        DataTable dt = new DataTable();
                        (new SqlDataAdapter(sqlCommand)).Fill(dt);
                        // string executedQuery = sqlCommand.CommandText;
                        sqlCommand.Parameters.Clear();
                        return Task.FromResult(JsonConvert.SerializeObject(dt));

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        sqlConnection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
       
        public int GetSqlN(String Sql, string connectionString)
        {
            int N = 0;
            IDataReader rs;
            rs = GetRS(Sql, connectionString);
            if (rs.Read())
            {
                N = RSFieldInt(rs, "N");
            }
            rs.Close();
            return N;
        }
        public int RSFieldInt(IDataReader rs, String fieldname)
        {

            int idx = rs.GetOrdinal(fieldname);
            if (rs.IsDBNull(idx))
            {
                return 0;
            }
            return rs.GetInt32(idx);

        }

        public decimal GetDSqlN(string Sql, string connectionString)
        {
            decimal N = 0;
            IDataReader rs;
            rs = GetRS(Sql, connectionString);
            if (rs.Read())
            {
                N = RSFieldDecimal(rs, "N");
            }
            rs.Close();
            return N;
        }

        public decimal RSFieldDecimal(IDataReader rs, string FieldName)
        {
            decimal value = 0;
            int index = rs.GetOrdinal(FieldName);
            if (!rs.IsDBNull(index))
            {
                value = rs.GetDecimal(index);
            }
            return value;
        }

        public String SQuote(String s)
        {
            try
            {

                return "N'" + s.Replace("'", "''") + "'";

            }
            catch (Exception)
            {

                return "''";
            }
        }

        public IDataReader GetRS(String Sql, string conn)
        {
            SqlConnection dbconn = new SqlConnection
            {
                ConnectionString = conn
            };
            dbconn.Open();
            SqlCommand cmd = new SqlCommand(Sql, dbconn);
            return cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }
        public static String RSFieldByLocale(IDataReader rs, String fieldname, String LocaleSetting)
        {
            String tmpS = String.Empty;
            int idx = rs.GetOrdinal(fieldname);
            if (rs.IsDBNull(idx))
            {
                tmpS = String.Empty;
            }
            else
            {
                tmpS = rs.GetString(idx);
            }
            return tmpS;

        }
        public int GetSqllN(SqlCommand command, string connectionString)
        {
            int N = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                command.Connection = connection;
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        N = reader.GetInt32(reader.GetOrdinal("N"));
                    }
                }
            }
            return N;
        }


        public DataSet GetDS(String Sql, string connectionString)
        {
            DataSet ds = new DataSet();
            SqlConnection dbconn = new SqlConnection
            {
                ConnectionString = connectionString
            };
            dbconn.Open();
            SqlDataAdapter da = new SqlDataAdapter(Sql, dbconn);
            da.SelectCommand.CommandTimeout = 90; // Will allow the Data set to be filled in 90 Secs
            da.Fill(ds, "Table");
            dbconn.Close();

            return ds;
        }


#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<string> GetjsonDataFromDataset(string connectionString, string sp, Dictionary<string, object> sqlparems)

        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                try
                {
                    SqlCommand sqlCommand = new SqlCommand(sp, sqlConnection)
                    {
                        CommandType = CommandType.StoredProcedure,
                        CommandTimeout = sqlConnection.ConnectionTimeout
                    };
                    foreach (KeyValuePair<string, object> parametar in sqlparems)
                        sqlCommand.Parameters.AddWithValue(parametar.Key, parametar.Value);
                    StringBuilder executedQuery = new StringBuilder(sp);
                    executedQuery.Append(" ");

                    foreach (SqlParameter parameter in sqlCommand.Parameters)
                    {
                        executedQuery.Append(parameter.ParameterName + " = " + parameter.Value + ", ");
                    }
                        executedQuery.Remove(executedQuery.Length - 2, 2);
                    DataSet ds = new DataSet();
                    (new SqlDataAdapter(sqlCommand)).Fill(ds);
                    sqlCommand.Parameters.Clear();
                    string str = JsonConvert.SerializeObject(ds, Formatting.Indented);
                    return str;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<DataTable> GetDataTable(string connectionString, string sp, Dictionary<string, object> sqlparems, bool IsSP = false)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                try
                {
                    SqlCommand sqlCommand = new SqlCommand(sp, sqlConnection)
                    {
                        CommandType = CommandType.StoredProcedure,
                        CommandTimeout = sqlConnection.ConnectionTimeout
                    };
                    foreach (KeyValuePair<string, object> parametar in sqlparems)
                        sqlCommand.Parameters.AddWithValue(parametar.Key, parametar.Value);

                    DataTable dt = new DataTable();
                    (new SqlDataAdapter(sqlCommand)).Fill(dt);
                    sqlCommand.Parameters.Clear();

                    return dt;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
        }
#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<string> GetjsonDataFromDatasetINQ(string connectionString, string Query)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                try
                {
                    SqlCommand sqlCommand = new SqlCommand(Query, sqlConnection);


                    DataSet ds = new DataSet();
                    (new SqlDataAdapter(sqlCommand)).Fill(ds);

                    string str = JsonConvert.SerializeObject(ds, Formatting.Indented);
                    return str;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<string> GetJsonDataFromCommand(string connectionString, SqlCommand command)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                command.Connection = sqlConnection;
                sqlConnection.Open();
                try
                {
                    DataSet ds = new DataSet();
                    (new SqlDataAdapter(command)).Fill(ds); 

                    string str = JsonConvert.SerializeObject(ds, Formatting.Indented);
                    return str;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
        }


        public async Task ExecuteNonQuery(string connectionString, string procedureName, Dictionary<string, object> parameters)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                try
                {
                    using (SqlCommand sqlCommand = new SqlCommand())
                    {
                        sqlCommand.Connection = sqlConnection;
                        sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                        sqlCommand.CommandText = procedureName;

                        if (parameters.Count > 0)
                        {
                            sqlCommand.Parameters.AddRange(GetSqlParameters(parameters).ToArray());
                        }
                        await sqlCommand.ExecuteNonQueryAsync();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    sqlConnection.Close();
                }
            }
        }
        private static List<SqlParameter> GetSqlParameters(Dictionary<string, object> sqlParameter)
        {
            return sqlParameter.Select(p => new SqlParameter(p.Key, p.Value)).ToList();
        }
        public string GetSqlS(string Sql, string connectionstring)
        {
            String S = String.Empty;
            IDataReader rs = GetRS(Sql, connectionstring);
            if (rs.Read())
            {
                S = RSFieldByLocale(rs, "S", System.Threading.Thread.CurrentThread.CurrentUICulture.Name);
                if (S.Equals(DBNull.Value))
                {
                    S = String.Empty;
                }
            }
            rs.Close();
            return S;
        }
    }
}