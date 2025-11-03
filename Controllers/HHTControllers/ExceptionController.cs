using Simpolo_Endpoint.DAO.HHTInterface;
using Simpolo_Endpoint.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simpolo_Endpoint.Controllers.HHTControllers
{
    [Route("[controller]")]
    //[Authorize]
    [ApiController]
    public class ExceptionController : ControllerBase
    {
        JsonSettings jsonSettings = new JsonSettings();
        private readonly iException _Exception;
        private string _ClassCode = string.Empty;
        public ExceptionController(iException exception)
        {
            _Exception = exception;
            _ClassCode = ExceptionHandling.GetClassExceptionCode(ExceptionHandling.ExcpConstants_API_Enpoint.ExceptionController);
        }

        [HttpPost]
        [Route("LogException")]
        public string LogException(WMSCoreMessage oRequest)
        {
            try
            {
                bool Result = false;
                InventoryDTO _oResponse = new InventoryDTO();
                List<InventoryDTO> lstReponse = new List<InventoryDTO>();
                if (oRequest != null)
                {
                    dynamic obj = oRequest.EntityObject;
                    WMSExceptionMessage _oWMSException = JsonConvert.DeserializeObject<WMSExceptionMessage>(obj.ToString());

                    if ("" != null)
                    {
                        Result = _Exception.LogException(_oWMSException);
                        if (Result == true)
                        {
                            _oResponse.Result = "1";
                        }
                        else
                        {
                            _oResponse.Result = "0";
                        }
                        return JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Inventory, _oResponse),jsonSettings.JsonSerializerSettings));
                    }
                }
                return null;
            }
            catch (WMSExceptionMessage ex)
            {
                List<WMSExceptionMessage> _lstwMSExceptionMessage = new List<WMSExceptionMessage>();
                _lstwMSExceptionMessage.Add(ex);
                return JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Exception, _lstwMSExceptionMessage),jsonSettings.JsonSerializerSettings));
            }
            catch (System.Exception excp)
            {
                ExceptionHandling.LogException(excp, _ClassCode + "001");
                return null;
            }
        }

    }
}
