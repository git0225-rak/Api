using FWMSC21Core.Entities;
using Simpolo_Endpoint.DAO.interfaces;
using Simpolo_Endpoint.DBUtil;
using Simpolo_Endpoint.ModelsLibrary;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Simpolo_Endpoint.DAO.Services
{
    public class ShipperIDIntegrationService : AppDBService, IShipperIDIntegration
    {
        public ShipperIDIntegrationService(IOptions<AppSettings> appSettings) : base(appSettings)
        {

        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<string> POSTXML(string request)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                string SQLQuery = "EXEC [dbo].[sp_SAP_InsertIDOC] xml = @xml";

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(SQLQuery, connection))
                    {
                        command.Parameters.AddWithValue("@xml", DBLibrary.SQuote(request));
                        command.ExecuteNonQuery();
                    }
                }
                
                return "success";
            }
            catch (Exception ex)
            {
                return "error:" + ex.Message + "";
            }
        }
    }
}
