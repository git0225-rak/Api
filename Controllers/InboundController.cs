using Simpolo_Endpoint.DAO.interfaces;
using Simpolo_Endpoint.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simpolo_Endpoint.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class InboundController : ControllerBase
    {
        private readonly IInbound _Inbound;
        public InboundController(IInbound inbound)
        {
            _Inbound = inbound;
        }

        //Get Inbound Details
        [Route("GetInboundDetails"), HttpPost]
        public async Task<IActionResult> GetInboundDetails(GetInboundDetailsModel items)
        {
            Payload<string> response = await _Inbound.GetInboundDetails(items);
            if (response != null)
            {
                if (!response.HasErrors && !response.HasWarnings)
                {
                    return Ok(response);
                }
                else if (response.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response.GetErrors());
                }
                else
                {
                    return StatusCode(StatusCodes.Status202Accepted, response.GetWarnings());
                }
            }
            else
            {
                return BadRequest("Failed to retrieve data");
            }
        }


        [Route("GetInboundTracking_ShipmentTransit"), HttpPost]
        public async Task<IActionResult> GetInboundTracking_ShipmentTransit(InboundTracking_ShipmentTransitModel items)
        {
            Payload<string> response = await _Inbound.GetInboundTracking_ShipmentTransit(items);
            if (response != null)
            {
                if (!response.HasErrors && !response.HasWarnings)
                {
                    return Ok(response);
                }
                else if (response.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response.GetErrors());
                }
                else
                {
                    return StatusCode(StatusCodes.Status202Accepted, response.GetWarnings());
                }
            }
            else
            {
                return BadRequest("Failed to retrieve data");
            }
        }


        [Route("GetInboundTracking_ShipmentExpected"), HttpPost]
        public async Task<IActionResult> GetInboundTracking_ShipmentExpected(InboundTracking_ShipmentExpectedModel items)
        {
            Payload<string> response = await _Inbound.GetInboundTracking_ShipmentExpected(items);
            if (response != null)
            {
                if (!response.HasErrors && !response.HasWarnings)
                {
                    return Ok(response);
                }
                else if (response.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response.GetErrors());
                }
                else
                {
                    return StatusCode(StatusCodes.Status202Accepted, response.GetWarnings());
                }
            }
            else
            {
                return BadRequest("Failed to retrieve data");
            }
        }


        [Route("GetInboundTracking_ShipmentInProcess"), HttpPost]
        public async Task<IActionResult> GetInboundTracking_ShipmentInProcess(InboundTracking_ShipmentInProcessModel items)
        {
            Payload<string> response = await _Inbound.GetInboundTracking_ShipmentInProcess(items);
            if (response != null)
            {
                if (!response.HasErrors && !response.HasWarnings)
                {
                    return Ok(response);
                }
                else if (response.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response.GetErrors());
                }
                else
                {
                    return StatusCode(StatusCodes.Status202Accepted, response.GetWarnings());
                }
            }
            else
            {
                return BadRequest("Failed to retrieve data");
            }
        }


        [Route("GetRevertInboundList"), HttpPost]
        public async Task<IActionResult> GetRevertInboundList(GetRevertInboundListModel items)
        {
            Payload<string> response = await _Inbound.GetRevertInboundList(items);
            if (response != null)
            {
                if (!response.HasErrors && !response.HasWarnings)
                {
                    return Ok(response);
                }
                else if (response.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response.GetErrors());
                }
                else
                {
                    return StatusCode(StatusCodes.Status202Accepted, response.GetWarnings());
                }
            }
            else
            {
                return BadRequest("Failed to retrieve data");
            }
        }



        //basicdata>Save/update

        [Route("UpsertInboundBasicData"), HttpPost]
        public async Task<IActionResult> UpsertInboundBasicData(UpsertInboundBasicDataModel items)
        {
            Payload<string> response = await _Inbound.UpsertInboundBasicData(items);
            if (response != null)
            {
                if (!response.HasErrors && !response.HasWarnings)
                {
                    return Ok(response);
                }
                else if (response.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response.GetErrors());
                }
                else
                {
                    return StatusCode(StatusCodes.Status202Accepted, response.GetWarnings());
                }
            }
            else
            {
                return BadRequest("Failed to retrieve data");
            }
        }


        // GET Inward Order / Invoice Details
        [Route("GetInBoundPOInvoiceDetails"), HttpPost]
        public async Task<IActionResult> GetInBoundPOInvoiceDetails(GetInBoundPOInvoiceDetailsModel items)
        {
            Payload<string> response = await _Inbound.GetInBoundPOInvoiceDetails(items);
            if (response != null)
            {
                if (!response.HasErrors && !response.HasWarnings)
                {
                    return Ok(response);
                }
                else if (response.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response.GetErrors());
                }
                else
                {
                    return StatusCode(StatusCodes.Status202Accepted, response.GetWarnings());
                }
            }
            else
            {
                return BadRequest("Failed to retrieve data");
            }
        }


        //Inward Order / Invoice Details > PopUp Conditions
        [Route("AddOrderOrInvoiceItems"), HttpPost]
        public async Task<IActionResult> AddOrderOrInvoiceItems(AddOrderOrInvoiceItemsModel items)
        {
            Payload<string> response = await _Inbound.AddOrderOrInvoiceItems(items);
            if (response != null)
            {
                if (!response.HasErrors && !response.HasWarnings)
                {
                    return Ok(response);
                }
                else if (response.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response.GetErrors());
                }
                else
                {
                    return StatusCode(StatusCodes.Status202Accepted, response.GetWarnings());
                }
            }
            else
            {
                return BadRequest("Failed to retrieve data");
            }
        }



        //Inward Order / Invoice Details
        [Route("UpsertInBoundPOInvoiceDetails"), HttpPost]
        public async Task<IActionResult> UpsertInBoundPOInvoiceDetails(UpsertInBoundPOInvoiceDetailsModel items)
        {
            Payload<string> response = await _Inbound.UpsertInBoundPOInvoiceDetails(items);
            if (response != null)
            {
                if (!response.HasErrors && !response.HasWarnings)
                {
                    return Ok(response);
                }
                else if (response.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response.GetErrors());
                }
                else
                {
                    return StatusCode(StatusCodes.Status202Accepted, response.GetWarnings());
                }
            }
            else
            {
                return BadRequest("Failed to retrieve data");
            }
        }

        //Get ASN Deatils
        [Route("GetASNDetails"), HttpPost]
        public async Task<IActionResult> GetASNDetails(GetASNDetailsModel items)
        {
            Payload<string> response = await _Inbound.GetASNDetails(items);
            if (response != null)
            {
                if (!response.HasErrors && !response.HasWarnings)
                {
                    return Ok(response);
                }
                else if (response.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response.GetErrors());
                }
                else
                {
                    return StatusCode(StatusCodes.Status202Accepted, response.GetWarnings());
                }
            }
            else
            {
                return BadRequest("Failed to retrieve data");
            }
        }



        //Inbound Search

        [Route("GetSearchInboundDetails"), HttpPost]
        public async Task<IActionResult> GetSearchInboundDetails(GetSearchInboundDetailsModel items)
        {
            Payload<string> response = await _Inbound.GetSearchInboundDetails(items);
            if (response != null)
            {
                if (!response.HasErrors && !response.HasWarnings)
                {
                    return Ok(response);
                }
                else if (response.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response.GetErrors());
                }
                else
                {
                    return StatusCode(StatusCodes.Status202Accepted, response.GetWarnings());
                }
            }
            else
            {
                return BadRequest("Failed to retrieve data");
            }
        }

        //ReceivingTallyReport RTR
        [Route("GetRTRDetails"), HttpPost]
        public async Task<IActionResult> GetRTRDetails(GetRTRDetailsModel items)
        {
            Payload<string> response = await _Inbound.GetRTRDetails(items);
            if (response != null)
            {
                if (!response.HasErrors && !response.HasWarnings)
                {
                    return Ok(response);
                }
                else if (response.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response.GetErrors());
                }
                else
                {
                    return StatusCode(StatusCodes.Status202Accepted, response.GetWarnings());
                }
            }
            else
            {
                return BadRequest("Failed to retrieve data");
            }
        }


        //RTR > Receive Button

        [Route("GetGoodsInSuggestedPutAwayList"), HttpPost]
        public async Task<IActionResult> GetGoodsInSuggestedPutAwayList(GetGoodsInSuggestedPutAwayListModel items)
        {
            Payload<string> response = await _Inbound.GetGoodsInSuggestedPutAwayList(items);
            if (response != null)
            {
                if (!response.HasErrors && !response.HasWarnings)
                {
                    return Ok(response);
                }
                else if (response.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response.GetErrors());
                }
                else
                {
                    return StatusCode(StatusCodes.Status202Accepted, response.GetWarnings());
                }
            }
            else
            {
                return BadRequest("Failed to retrieve data");
            }
        }




        //Put Away List > Get
        [Route("Get_ReceiveMSPsPutawayList"), HttpPost]
        public async Task<IActionResult> Get_ReceiveMSPsPutawayList(Get_ReceiveMSPsPutawayListModel items)
        {
            Payload<string> response = await _Inbound.Get_ReceiveMSPsPutawayList(items);
            if (response != null)
            {
                if (!response.HasErrors && !response.HasWarnings)
                {
                    return Ok(response);
                }
                else if (response.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response.GetErrors());
                }
                else
                {
                    return StatusCode(StatusCodes.Status202Accepted, response.GetWarnings());
                }
            }
            else
            {
                return BadRequest("Failed to retrieve data");
            }
        }




        //Put Away List > Receive
        [Route("ReceiveMSPsPutawayList"), HttpPost]
        public async Task<IActionResult> ReceiveMSPsPutawayList(ReceiveMSPsPutawayListModel items)
        {
            Payload<string> response = await _Inbound.ReceiveMSPsPutawayList(items);
            if (response != null)
            {
                if (!response.HasErrors && !response.HasWarnings)
                {
                    return Ok(response);
                }
                else if (response.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response.GetErrors());
                }
                else
                {
                    return StatusCode(StatusCodes.Status202Accepted, response.GetWarnings());
                }
            }
            else
            {
                return BadRequest("Failed to retrieve data");
            }
        }


        //Put Away List > DELETE
        [Route("DeleteGoodsInRecieveddetails"), HttpPost]
        public async Task<IActionResult> DeleteGoodsInRecieveddetails(DeleteGoodsInRecieveddetailsModel items)
        {
            Payload<string> response = await _Inbound.DeleteGoodsInRecieveddetails(items);
            if (response != null)
            {
                if (!response.HasErrors && !response.HasWarnings)
                {
                    return Ok(response);
                }
                else if (response.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response.GetErrors());
                }
                else
                {
                    return StatusCode(StatusCodes.Status202Accepted, response.GetWarnings());
                }
            }
            else
            {
                return BadRequest("Failed to retrieve data");
            }
        }


        //ASN update
        [Route("UpdateASNDetails"), HttpPost]
        public async Task<IActionResult> UpdateASNDetails(UpdateASNDetailsModel items)
        {
            Payload<string> response = await _Inbound.UpdateASNDetails(items);
            if (response != null)
            {
                if (!response.HasErrors && !response.HasWarnings)
                {
                    return Ok(response);
                }
                else if (response.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response.GetErrors());
                }
                else
                {
                    return StatusCode(StatusCodes.Status202Accepted, response.GetWarnings());
                }
            }
            else
            {
                return BadRequest("Failed to retrieve data");
            }
        }

        //Shipment Expected Details
        [Route("UpdateShipmentExpectedDetails"), HttpPost]
        public async Task<IActionResult> UpdateShipmentExpectedDetails(UpdateShipmentExpectedDetailsModel items)
        {
            Payload<string> response = await _Inbound.UpdateShipmentExpectedDetails(items);
            if (response != null)
            {
                if (!response.HasErrors && !response.HasWarnings)
                {
                    return Ok(response);
                }
                else if (response.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response.GetErrors());
                }
                else
                {
                    return StatusCode(StatusCodes.Status202Accepted, response.GetWarnings());
                }
            }
            else
            {
                return BadRequest("Failed to retrieve data");
            }
        }


        //Receiving Dock Management > Get
        [Route("GetReceivingDockManagementDetails"), HttpPost]
        public async Task<IActionResult> GetReceivingDockManagementDetails(GetReceivingDockManagementDetailsModel items)
        {
            Payload<string> response = await _Inbound.GetReceivingDockManagementDetails(items);
            if (response != null)
            {
                if (!response.HasErrors && !response.HasWarnings)
                {
                    return Ok(response);
                }
                else if (response.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response.GetErrors());
                }
                else
                {
                    return StatusCode(StatusCodes.Status202Accepted, response.GetWarnings());
                }
            }
            else
            {
                return BadRequest("Failed to retrieve data");
            }
        }



        [Route("UNBLOCKPGRInbound"), HttpPost]
        public async Task<IActionResult> UNBLOCKPGRInbound(PGRUnblockModel items)
        {
            Payload<string> response = await _Inbound.UNBLOCKPGRInbound(items);
            if (response != null)
            {
                if (!response.HasErrors && !response.HasWarnings)
                {
                    return Ok(response);
                }
                else if (response.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response.GetErrors());
                }
                else
                {
                    return StatusCode(StatusCodes.Status202Accepted, response.GetWarnings());
                }
            }
            else
            {
                return BadRequest("Failed to retrieve data");
            }
        }





        //Receiving Dock Management > SAVE/UPDATE
        [Route("UpsertReceivingDockManagement"), HttpPost]
        public async Task<IActionResult> UpsertReceivingDockManagement(UpsertReceivingDockManagementModel items)
        {
            Payload<string> response = await _Inbound.UpsertReceivingDockManagement(items);
            if (response != null)
            {
                if (!response.HasErrors && !response.HasWarnings)
                {
                    return Ok(response);
                }
                else if (response.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response.GetErrors());
                }
                else
                {
                    return StatusCode(StatusCodes.Status202Accepted, response.GetWarnings());
                }
            }
            else
            {
                return BadRequest("Failed to retrieve data");
            }
        }


        //Receiving Dock Management > Delete
        [Route("DeleteReceivingDockManagementDetails"), HttpPost]
        public async Task<IActionResult> DeleteReceivingDockManagementDetails(DeleteReceivingDockManagementDetailsModel items)
        {
            Payload<string> response = await _Inbound.DeleteReceivingDockManagementDetails(items);
            if (response != null)
            {
                if (!response.HasErrors && !response.HasWarnings)
                {
                    return Ok(response);
                }
                else if (response.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response.GetErrors());
                }
                else
                {
                    return StatusCode(StatusCodes.Status202Accepted, response.GetWarnings());
                }
            }
            else
            {
                return BadRequest("Failed to retrieve data");
            }
        }


        //Shipment REceived Details > GET
        [Route("Get_ShipmentReceivedDetails"), HttpPost]
        public async Task<IActionResult> Get_ShipmentReceivedDetails(Get_ShipmentReceivedDetailsModel items)
        {
            Payload<string> response = await _Inbound.Get_ShipmentReceivedDetails(items);
            if (response != null)
            {
                if (!response.HasErrors && !response.HasWarnings)
                {
                    return Ok(response);
                }
                else if (response.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response.GetErrors());
                }
                else
                {
                    return StatusCode(StatusCodes.Status202Accepted, response.GetWarnings());
                }
            }
            else
            {
                return BadRequest("Failed to retrieve data");
            }
        }


        //Shipment REceived Details > Receive Button
        [Route("ShipmentReceivedDetails"), HttpPost]
        public async Task<IActionResult> ShipmentReceivedDetails(ShipmentReceivedDetailsModel items)
        {
            Payload<string> response = await _Inbound.ShipmentReceivedDetails(items);
            if (response != null)
            {
                if (!response.HasErrors && !response.HasWarnings)
                {
                    return Ok(response);
                }
                else if (response.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response.GetErrors());
                }
                else
                {
                    return StatusCode(StatusCodes.Status202Accepted, response.GetWarnings());
                }
            }
            else
            {
                return BadRequest("Failed to retrieve data");
            }
        }




        [Route("UpsertShipmentDetailsBasedonInwardType"), HttpPost]
        public async Task<IActionResult> UpsertShipmentDetailsBasedonInwardType(ShipmentReceivedDetailsModel items)
        {
            Payload<string> response = await _Inbound.UpsertShipmentDetailsBasedonInwardType(items);
            if (response != null)
            {
                if (!response.HasErrors && !response.HasWarnings)
                {
                    return Ok(response);
                }
                else if (response.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response.GetErrors());
                }
                else
                {
                    return StatusCode(StatusCodes.Status202Accepted, response.GetWarnings());
                }
            }
            else
            {
                return BadRequest("Failed to retrieve data");
            }
        }






        //GRN Get Details
        [Route("GetGRNUpdateDetails"), HttpPost]
        public async Task<IActionResult> GetGRNUpdateDetails(GetGRNUpdateDetailsModel items)
        {
            Payload<string> response = await _Inbound.GetGRNUpdateDetails(items);
            if (response != null)
            {
                if (!response.HasErrors && !response.HasWarnings)
                {
                    return Ok(response);
                }
                else if (response.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response.GetErrors());
                }
                else
                {
                    return StatusCode(StatusCodes.Status202Accepted, response.GetWarnings());
                }
            }
            else
            {
                return BadRequest("Failed to retrieve data");
            }
        }


       
        [Route("FetchGRNDataForInbound"), HttpPost]
        public async Task<IActionResult> FetchGRNDataForInbound(FetchGRNDataForInboundModel items)
        {
            Payload<string> response = await _Inbound.FetchGRNDataForInbound(items);
            if (response != null)
            {
                if (!response.HasErrors && !response.HasWarnings)
                {
                    return Ok(response);
                }
                else if (response.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response.GetErrors());
                }
                else
                {
                    return StatusCode(StatusCodes.Status202Accepted, response.GetWarnings());
                }
            }
            else
            {
                return BadRequest("Failed to retrieve data");
            }
        }


        //GRN > Post GRN

        [Route("CheckIsShortGRN"), HttpPost]
        public async Task<IActionResult> CheckIsShortGRN(CheckIsShortGRNModel items)
        {
            Payload<string> response = await _Inbound.CheckIsShortGRN(items);
            if (response != null)
            {
                if (!response.HasErrors && !response.HasWarnings)
                {
                    return Ok(response);
                }
                else if (response.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response.GetErrors());
                }
                else
                {
                    return StatusCode(StatusCodes.Status202Accepted, response.GetWarnings());
                }
            }
            else
            {
                return BadRequest("Failed to retrieve data");
            }
        }


        // Discrepancy Details
        [Route("GetDiscrepancyDetails"), HttpPost]
        public async Task<IActionResult> GetDiscrepancyDetails(GetDiscrepancyDetailsModel items)
        {
            Payload<string> response = await _Inbound.GetDiscrepancyDetails(items);
            if (response != null)
            {
                if (!response.HasErrors && !response.HasWarnings)
                {
                    return Ok(response);
                }
                else if (response.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response.GetErrors());
                }
                else
                {
                    return StatusCode(StatusCodes.Status202Accepted, response.GetWarnings());
                }
            }
            else
            {
                return BadRequest("Failed to retrieve data");
            }
        }

        // Discrepancy List > POPUP > SAVE
        [Route("GetDiscrepancyLineItems_PageLoad"), HttpPost]
        public async Task<IActionResult> GetDiscrepancyLineItems_PageLoad(GetDiscrepancyLineItems_PageLoadModel items)
        {
            Payload<string> response = await _Inbound.GetDiscrepancyLineItems_PageLoad(items);
            if (response != null)
            {
                if (!response.HasErrors && !response.HasWarnings)
                {
                    return Ok(response);
                }
                else if (response.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response.GetErrors());
                }
                else
                {
                    return StatusCode(StatusCodes.Status202Accepted, response.GetWarnings());
                }
            }
            else
            {
                return BadRequest("Failed to retrieve data");
            }
        }


        //Save Discrepancy
        [Route("SaveDiscrepancyDetails"), HttpPost]
        public async Task<IActionResult> SaveDiscrepancyDetails(SaveDiscrepancyDetailsModel items)
        {
            Payload<string> response = await _Inbound.SaveDiscrepancyDetails(items);
            if (response != null)
            {
                if (!response.HasErrors && !response.HasWarnings)
                {
                    return Ok(response);
                }
                else if (response.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response.GetErrors());
                }
                else
                {
                    return StatusCode(StatusCodes.Status202Accepted, response.GetWarnings());
                }
            }
            else
            {
                return BadRequest("Failed to retrieve data");
            }
        }

        //upsert Discrepancy
        [Route("UpsertDiscrepancyDetails"), HttpPost]
        public async Task<IActionResult> UpsertDiscrepancyDetails(UpsertDiscrepancyDetailsModel items)
        {
            Payload<string> response = await _Inbound.UpsertDiscrepancyDetails(items);
            if (response != null)
            {
                if (!response.HasErrors && !response.HasWarnings)
                {
                    return Ok(response);
                }
                else if (response.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response.GetErrors());
                }
                else
                {
                    return StatusCode(StatusCodes.Status202Accepted, response.GetWarnings());
                }
            }
            else
            {
                return BadRequest("Failed to retrieve data");
            }
        }


        //Delete Discrepancy
        [Route("DeleteDiscrepancyDetails"), HttpPost]
        public async Task<IActionResult> DeleteDiscrepancyDetails(DeleteDiscrepancyDetailsModel items)
        {
            Payload<string> response = await _Inbound.DeleteDiscrepancyDetails(items);
            if (response != null)
            {
                if (!response.HasErrors && !response.HasWarnings)
                {
                    return Ok(response);
                }
                else if (response.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response.GetErrors());
                }
                else
                {
                    return StatusCode(StatusCodes.Status202Accepted, response.GetWarnings());
                }
            }
            else
            {
                return BadRequest("Failed to retrieve data");
            }
        }

        // check has Discrepancy at pageload
        [Route("CheckDiscrepency_OnPageLoad"), HttpPost]
        public async Task<IActionResult> CheckDiscrepency_OnPageLoad(CheckDiscrepency_OnPageLoadModel items)
        {
            Payload<string> response = await _Inbound.CheckDiscrepency_OnPageLoad(items);
            if (response != null)
            {
                if (!response.HasErrors && !response.HasWarnings)
                {
                    return Ok(response);
                }
                else if (response.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response.GetErrors());
                }
                else
                {
                    return StatusCode(StatusCodes.Status202Accepted, response.GetWarnings());
                }
            }
            else
            {
                return BadRequest("Failed to retrieve data");
            }
        }

        //Shipment Verification > Verify Button
        [Route("UpdateShipmentVerificationDetails"), HttpPost]
        public async Task<IActionResult> ShipmentVerificationDetails(UpdateShipmentVerificationDetailsModel items)
        {
            Payload<string> response = await _Inbound.UpdateShipmentVerificationDetails(items);
            if (response != null)
            {
                if (!response.HasErrors && !response.HasWarnings)
                {
                    return Ok(response);
                }
                else if (response.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response.GetErrors());
                }
                else
                {
                    return StatusCode(StatusCodes.Status202Accepted, response.GetWarnings());
                }
            }
            else
            {
                return BadRequest("Failed to retrieve data");
            }
        }

        //Shipment Verification > GET
        [Route("GetShipmentVerificationDetails"), HttpPost]
        public async Task<IActionResult> GetShipmentVerificationDetails(GetShipmentVerificationDetailsModel items)
        {
            Payload<string> response = await _Inbound.GetShipmentVerificationDetails(items);
            if (response != null)
            {
                if (!response.HasErrors && !response.HasWarnings)
                {
                    return Ok(response);
                }
                else if (response.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response.GetErrors());
                }
                else
                {
                    return StatusCode(StatusCodes.Status202Accepted, response.GetWarnings());
                }
            }
            else
            {
                return BadRequest("Failed to retrieve data");
            }
        }

        //INBOUND REVERT > RevertGRN

        [Route("RevertGRNDetails"), HttpPost] 
        public async Task<IActionResult> RevertGRNDetails(RevertGRNDetailsModel items)
        {
            Payload<string> response = await _Inbound.RevertGRNDetails(items);
            if (response != null)
            {
                if (!response.HasErrors && !response.HasWarnings)
                {
                    return Ok(response);
                }
                else if (response.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response.GetErrors());
                }
                else
                {
                    return StatusCode(StatusCodes.Status202Accepted, response.GetWarnings());
                }
            }
            else
            {
                return BadRequest("Failed to retrieve data");
            }
        }


        //INBOUND REVERT > CLOSE

        [Route("ShipmentCloseDetails"), HttpPost]
        public async Task<IActionResult> ShipmentCloseDetails(ShipmentCloseDetailsModel items)
        {
            Payload<string> response = await _Inbound.ShipmentCloseDetails(items);
            if (response != null)
            {
                if (!response.HasErrors && !response.HasWarnings)
                {
                    return Ok(response);
                }
                else if (response.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response.GetErrors());
                }
                else
                {
                    return StatusCode(StatusCodes.Status202Accepted, response.GetWarnings());
                }
            }
            else
            {
                return BadRequest("Failed to retrieve data");
            }
        }

        //INBOUND REVERT > GET

        [Route("GetInboundRevertDetails"), HttpPost]
        public async Task<IActionResult> GetInboundRevertDetails(GetInboundRevertDetailsModel items)
        {
            Payload<string> response = await _Inbound.GetInboundRevertDetails(items);
            if (response != null)
            {
                if (!response.HasErrors && !response.HasWarnings)
                {
                    return Ok(response);
                }
                else if (response.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response.GetErrors());
                }
                else
                {
                    return StatusCode(StatusCodes.Status202Accepted, response.GetWarnings());
                }
            }
            else
            {
                return BadRequest("Failed to retrieve data");
            }
        }


  [Route("GetRevertGRNDetails"), HttpPost]
        public async Task<IActionResult> GetRevertGRNDetails(GetRevertGRNDetailsModel items)
        {
            Payload<string> response = await _Inbound.GetRevertGRNDetails(items);
            if (response != null)
            {
                if (!response.HasErrors && !response.HasWarnings)
                {
                    return Ok(response);
                }
                else if (response.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response.GetErrors());
                }
                else
                {
                    return StatusCode(StatusCodes.Status202Accepted, response.GetWarnings());
                }
            }
            else
            {
                return BadRequest("Failed to retrieve data");
            }
        }

        [Route("RevertShipmmentExpected"), HttpPost]
        public async Task<IActionResult> RevertShipmmentExpected(RevertShipmmentExpectedModel items)
        {
            Payload<string> response = await _Inbound.RevertShipmmentExpected(items);
            if (response != null)
            {
                if (!response.HasErrors && !response.HasWarnings)
                {
                    return Ok(response);
                }
                else if (response.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response.GetErrors());
                }
                else
                {
                    return StatusCode(StatusCodes.Status202Accepted, response.GetWarnings());
                }
            }
            else
            {
                return BadRequest("Failed to retrieve data");
            }
        }

        [Route("RevertShipmmentReceived"), HttpPost]
        public async Task<IActionResult> RevertShipmmentReceived(RevertShipmmentReceivedModel items)
        {
            Payload<string> response = await _Inbound.RevertShipmmentReceived(items);
            if (response != null)
            {
                if (!response.HasErrors && !response.HasWarnings)
                {
                    return Ok(response);
                }
                else if (response.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response.GetErrors());
                }
                else
                {
                    return StatusCode(StatusCodes.Status202Accepted, response.GetWarnings());
                }
            }
            else
            {
                return BadRequest("Failed to retrieve data");
            }
        }

        [Route("CreateGRNEntryAndPostDatatoSAP"), HttpPost]
        public async Task<IActionResult> CreateGRNEntryAndPostDatatoSAP(CreateGRNEntryAndPostDatatoSAPModel items)
        {
            Payload<string> response = await _Inbound.CreateGRNEntryAndPostDatatoSAP(items);

            if (response != null)
            {
                if (!response.HasErrors && !response.HasWarnings && response != null)
                {
                    return Ok(response);
                }
                else if (response.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response.GetErrors());
                }
                else
                {
                    return StatusCode(StatusCodes.Status202Accepted, response.GetWarnings());
                }
            }
            else
            {
                return BadRequest("Failed to retrieve data");
            }
        }

        [Route("RTR_PrintLabels"), HttpPost]
        public async Task<IActionResult> RTR_PrintLabels(RTR_PrintLabelModel items)
        {
            Payload<string> response = await _Inbound.RTR_PrintLabels(items);
            if (response != null)
            {
                if (!response.HasErrors && !response.HasWarnings)
                {
                    return Ok(response);
                }
                else if (response.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response.GetErrors());
                }
                else
                {
                    return StatusCode(StatusCodes.Status202Accepted, response.GetWarnings());
                }
            }
            else
            {
                return BadRequest("Failed to retrieve data");
            }
        }

    }
}

