using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Simpolo_Endpoint.DAO.interfaces;
using Simpolo_Endpoint.Entities;
using Simpolo_Endpoint.Models;
using static Simpolo_Endpoint.Models.InventoryModel;
using System.Threading.Tasks;
using Simpolo_Endpoint.DTO;
using System;

namespace Simpolo_Endpoint.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class GroupOBDController : ControllerBase
    {

        private readonly IGroupOBD _GroupOBD;
        public GroupOBDController(IGroupOBD groupOBD)
        {
            _GroupOBD = groupOBD;
        }
       

        [Route("GetGroupOutboundDetails"), HttpPost]
        public async Task<IActionResult> GetGroupOutboundDetails(GroupOBDDTO groupobd)
        {
            Payload<string> response = await _GroupOBD.GetGroupOutboundDetails(groupobd);
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



        [Route("GetOBDDetailsCustomerWise"), HttpPost]
        public async Task<IActionResult> GetOBDDetailsCustomerWise(GroupOBDDTO groupobd)
        {
            Payload<string> response = await _GroupOBD.GetOBDDetailsCustomerWise(groupobd);
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





        [Route("GetSOType"), HttpPost]
        public async Task<IActionResult> GetSOType(SOTypeModelItems sotype)
        {
            Payload<string> response = await _GroupOBD.GetSOType(sotype);
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

        [Route("GetDeliverySites"), HttpPost]
        public async Task<IActionResult> GetDeliverySites(DeliverySitesModelItems DeliverySites)
        {
            Payload<string> response = await _GroupOBD.GetDeliverySites(DeliverySites);
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



        [Route("GroupOBDCreation"), HttpPost]
        public async Task<IActionResult> GroupOBDCreation(GroupOutboundCreationItemsModel GroupOutbound)
        {
            Payload<string> response = await _GroupOBD.GroupOBDCreation(GroupOutbound);
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

        [Route("VLPDDeliveryNoteDetails"), HttpPost]
        public async Task<IActionResult> VLPDDeliveryNoteDetails(VLPDDeliveryNoteModelItems VLPDDelivery)
        {
            Payload<string> response = await _GroupOBD.VLPDDeliveryNoteDetails(VLPDDelivery);
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


        [Route("GetGroupOBDPopupDetails"), HttpPost]
        public async Task<IActionResult> GetGroupOBDPopupDetails(GroupOBDPopupModelitems GroupOBDPopup)
        {
            Payload<string> response = await _GroupOBD.GetGroupOBDPopupDetails(GroupOBDPopup);
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


        [Route("GetCartonDetails"), HttpPost]
        public async Task<IActionResult> GetCartonDetails(CartonModelItems cartonmodel)
        {
            Payload<string> response = await _GroupOBD.GetCartonDetails(cartonmodel);
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



        [Route("GetVLPDPickingItem"), HttpPost]
        public async Task<IActionResult> GetVLPDPickingItem(vlpdpickitemfrombin vlpdpickitem)
        {
            Payload<string> response = await _GroupOBD.GetVLPDPickingItem(vlpdpickitem);
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

        [Route("GetVLPDPickingByGroupOBDNumber"), HttpPost]
        public async Task<IActionResult> GetVLPDPickingByGroupOBDNumber(vlpddeliverypicknote vlpdpickitem)
        {
            Payload<string> response = await _GroupOBD.GetVLPDPickingByGroupOBDNumber(vlpdpickitem);
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

        [Route("UpdateOBDQty"), HttpPost]
        public async Task<IActionResult> UpdateOBDQty(UpdateOBDQtyinViewPopup UpdateOBDQty)
        {
            Payload<string> response = await _GroupOBD.UpdateOBDQty(UpdateOBDQty);
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


        [Route("VerifyPopUp"), HttpPost]
        public async Task<IActionResult> VerifyPopUp(Vlpdverifyopup Vlpdverify)
        {
            Payload<string> response = await _GroupOBD.VerifyPopUp(Vlpdverify);
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

        [Route("GetPendingSKUList"), HttpPost]
        public async Task<IActionResult> GetPendingSKUList(Pendingreleaselist Pendingrelease)
        {
            Payload<string> response = await _GroupOBD.GetPendingSKUList(Pendingrelease);
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



        [Route("GetReservedSKUList"), HttpPost]
        public async Task<IActionResult> GetReservedSKUList(Pendingreleaselist Pendingrelease)
        {
            Payload<string> response = await _GroupOBD.GetReservedSKUList(Pendingrelease);
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




        [Route("VLPDViewPickList"), HttpPost]
        public async Task<IActionResult> VLPDViewPickList(VLPDViewPickList vlpdViewPickList)
        {
            Payload<string> response = await _GroupOBD.VLPDViewPickList(vlpdViewPickList);
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


        [Route("VLPDViewPickListSummarize"), HttpPost]
        public async Task<IActionResult> VLPDViewPickListSummarize(VLPDViewPickList vlpdViewPickList)
        {
            Payload<string> response = await _GroupOBD.VLPDViewPickListSummarize(vlpdViewPickList);
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




        [Route("VLPDReleaseItems"), HttpPost]
        public async Task<IActionResult> VLPDReleaseItems(VLPDReleaseModelItems VLPDReleaseModel)
        {
            Payload<string> response = await _GroupOBD.VLPDReleaseItems(VLPDReleaseModel);
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

        [Route("VLPDRegenerateReleaseItems"), HttpPost]
        public async Task<IActionResult> VLPDRegenerateReleaseItems(VLPDReleaseModelItems VLPDReleaseModel)
        {
            Payload<string> response = await _GroupOBD.VLPDRegenerateReleaseItems(VLPDReleaseModel);
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









        [Route("UpsertOBDLoadPointData"), HttpPost]
        public async Task<IActionResult> UpsertOBDLoadPointData(GetLoadingPoints items)
        {
            Payload<string> response = await _GroupOBD.UpsertOBDLoadPointData(items);
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


        [Route("UpdateOBDLoadPointData"), HttpPost]
        public async Task<IActionResult> UpdateOBDLoadPointData(GetLoadingPoints items)
        {
            Payload<string> response = await _GroupOBD.UpdateOBDLoadPointData(items);
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




        [Route("GetPickingListDetails"), HttpPost]
        public async Task<IActionResult> GetPickingListDetails(PickingListItemsModel PickingListItems)
        {
            Payload<string> response = await _GroupOBD.GetPickingListDetails(PickingListItems);
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

        [Route("GetGroupOBDNumber"), HttpPost]
        public async Task<IActionResult> GetGroupOBDNumber(GroupOBDNumber GroupOBD)
        {
            Payload<string> response = await _GroupOBD.GetGroupOBDNumber(GroupOBD);
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


        [Route("PostPGIDataToSAP"), HttpPost]
        public async Task<IActionResult> PostPGIDataToSAP(PgipostingDTO pgi)
        {
            
            try
            {
                bool response = await _GroupOBD.PostPGIDataToSAP(pgi);
                if (response)
                {
                    return Ok(new { Message = "PGI Posting successful." });
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Failed to post." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Failed to post.", Error = ex.Message });
            }
        }








    }
}

