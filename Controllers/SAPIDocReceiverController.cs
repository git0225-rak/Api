using Simpolo_Endpoint.DAO.interfaces;
using Simpolo_Endpoint.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simpolo_Endpoint.Controllers
{
    [Route("WMSAPI")]
    [ApiController]
    public class SAPIDocReceiverController : ControllerBase
    {
        private readonly ISAPIDocReceiver _ISAPIDocReceiver;
        public SAPIDocReceiverController(ISAPIDocReceiver ISAPIDocReceiver)
        {
            _ISAPIDocReceiver = ISAPIDocReceiver;
        }


        [HttpPost]
        [Route("PostIDocToFalcon")]
        public async Task<string> POSTXML(string idocName = null, string client = null)
        {
            try
            {

                using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
                {
                    string xml = reader.ReadToEndAsync().Result;
                    xml = xml.Remove(0, 38);
                    string result = _ISAPIDocReceiver.POSTXML(xml, idocName, client).Result;
                    return result;

                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
