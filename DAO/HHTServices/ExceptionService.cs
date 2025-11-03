using Simpolo_Endpoint.DAO.HHTInterface;
using Simpolo_Endpoint.DAO.Services;
using Simpolo_Endpoint.ModelsLibrary;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simpolo_Endpoint.DAO.HHTServices
{
    public class ExceptionService : AppDBService, iException
    {
        private string _ClassCode = string.Empty;
        public ExceptionService(IOptions<AppSettings> appSettings) : base(appSettings)
        {

        }
        public bool LogException(WMSExceptionMessage oWMSExcp)
        {
            try
            {
                ExceptionData oExcpData = new ExceptionData();
                oExcpData.AddInputs("oWMSExcp", oWMSExcp);

                ExceptionHandling.LogException(oWMSExcp, _ClassCode + "001", oExcpData);

                return true;
            }
            catch (WMSExceptionMessage excp)
            {
                throw excp;
            }
            catch (Exception excp)
            {
                ExceptionData oExcpData = new ExceptionData();
                oExcpData.AddInputs("oWMSExcp", oWMSExcp);

                ExceptionHandling.LogException(excp, _ClassCode + "001", oExcpData);
                throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = ErrorMessages.WMSExceptionMessage, ShowAsCriticalError = true };
            }
        }

    }
}
