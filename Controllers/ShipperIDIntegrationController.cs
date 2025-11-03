using Simpolo_Endpoint.DAO.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Simpolo_Endpoint.Controllers
{
    [Route("ShipperAPI")]
    [ApiController]
    public class ShipperIDIntegrationController : ControllerBase
    {
        private readonly IShipperIDIntegration _ShipperIDIntegration;
        public ShipperIDIntegrationController(IShipperIDIntegration shipperIDIntegration)
        {
            _ShipperIDIntegration = shipperIDIntegration;
        }


        [HttpPost]
        [Route("PostIDocToFalcon")]
        public async Task<string> POSTXML([FromBody]string request)
        {
            try
            {
                string result = "";
                string xml = request.ToString();
                result = _ShipperIDIntegration.POSTXML(xml).Result;
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }


    }
}
