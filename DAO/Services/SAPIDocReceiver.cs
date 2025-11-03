using Simpolo_Endpoint.DAO.interfaces;
using Simpolo_Endpoint.DBUtil;
using Simpolo_Endpoint.Models;
using Simpolo_Endpoint.ModelsLibrary;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Simpolo_Endpoint.DAO.Services
{
    public class SAPIDocReceiver : AppDBService, ISAPIDocReceiver
    {

        public SAPIDocReceiver(IOptions<AppSettings> appSettings) : base(appSettings)
        {

        }

        public async Task<string> POSTXML(string request, string Idocname = null, string Client = null)
        {

            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                    { "xml" , request },
                    { "IdocName" , Idocname },
                    { "Client" , Client }
                   
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.IDOCConectionString, "sp_SAP_InsertIDOC", sqlParams).ConfigureAwait(false);
                return "success";
            }
           
            catch (Exception ex)
            {
                return "error: " + ex.Message;
            }
        }
    }
}
