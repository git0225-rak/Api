using Simpolo_Endpoint.DAO.interfaces;
using Simpolo_Endpoint.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Simpolo_Endpoint.Models.POSOModel;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using System.Text;
using System.Net.Http;
using Simpolo_Endpoint.DAO.HHTServices;
using Simpolo_Endpoint.DAO.Services;
namespace Simpolo_Endpoint.Controllers
{

    [Route("SAPJSON")]
    [ApiController]
    public class SAPJsonPostServiceController : ControllerBase
    {
        private readonly ISAPJsonPostService _sapJsonPostService;
        public SAPJsonPostServiceController(ISAPJsonPostService sapJsonPostService)
        {
            _sapJsonPostService = sapJsonPostService;
        }

        [HttpPost("PGIPosting")]
        public async Task<IActionResult> PGIPosting()
        {
            PGIResponse status = await  _sapJsonPostService.SendPGIJSONDatatoSAP(1);  

            if (!string.IsNullOrEmpty(status.SAPRefNumber))
            {
                return Ok(new { Message = status });  
            }

            return BadRequest(status);
        }

        [HttpPost("PGIRevertPosting")]
        public async Task<IActionResult> PGIRevertPosting()
        {
            PGIRevertResponse status = await _sapJsonPostService.SendPGIRevertJSONDatatoSAP(3);

            if (!string.IsNullOrEmpty(status.SAPRefNumber))
            {
                return Ok(new { Message = status });
            }

            return BadRequest("SAP Json Error..");
        }


        [HttpPost("MaterialTransfersPosting")]
        public async Task<IActionResult> MaterialTransfersPosting()
        {
            string status = await _sapJsonPostService.SendMaterialTransferJSONDatatoSAP(5);

            if (!string.IsNullOrEmpty(status))
            {
                return Ok(new { Message = status });
            }

            return BadRequest("SAP Json Error..");
        }
    }

}



