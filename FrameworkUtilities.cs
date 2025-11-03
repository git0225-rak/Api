using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace Simpolo_Endpoint
{
    public static class FrameworkUtilities
    {
        private static string _ClassCode = "WMSCore_Lib_001_";
        public static string ReadApplicationKey(string Key)
        {
            try
            {
                return ConfigurationManager.AppSettings[Key];
            }
            catch (Exception ex)
            {
                ExceptionHandling.LogException(ex, _ClassCode + "001");

                return null;
            }
        }
    }
}
