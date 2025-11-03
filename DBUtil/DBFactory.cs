using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static Simpolo_Endpoint.DBUtil.DBLibrary;

namespace Simpolo_Endpoint.DBUtil
{
    public class DBFactory
    {
        private string dbName = "SqlServer";
        public IDBUtility getDBUtility()
        {
            if (dbName == "SqlServer")
            {

              return new SqlServerUtility();
            }
           
            else
            {
                throw new Exception("DB Configuration not avaialble");
            }
        }
    }
}