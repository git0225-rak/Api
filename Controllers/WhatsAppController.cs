using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Simpolo_Endpoint.DAO.interfaces;
using Simpolo_Endpoint.DTO;
using System.Threading.Tasks;

namespace Simpolo_Endpoint.Controllers
{
    [Route("WhatUpIntegration")]
    [ApiController]
    public class WhatsAppController : ControllerBase
    {
        private readonly IWhatappInterface _whatsappService;
        public WhatsAppController(IWhatappInterface whatsappService)
        {
            _whatsappService = whatsappService;
        }



        [HttpPost("sendmessage")]
        public async Task<IActionResult> SendMessage(WhatAppNotes wap)
        {
            WhatAppNotes was = new WhatAppNotes
            {
                OutboundID = wap.OutboundID,
                VechileNumber=wap.VechileNumber,
                ScenarioID = wap.ScenarioID
            };
         

            var response = await _whatsappService.SendWAMBasedOnActivity(was);
            return Ok(response);
        }
    }
}
