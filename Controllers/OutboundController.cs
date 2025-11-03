using Simpolo_Endpoint.DAO.interfaces;
using Simpolo_Endpoint.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Simpolo_Endpoint.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class OutboundController : ControllerBase
    {
        private readonly IOutbound _Outbound;
        public OutboundController(IOutbound outbound)
        {
            _Outbound = outbound;
        }


        [Route("GetPendingOBDForVLPDCreation"), HttpPost]
        public async Task<IActionResult> GetPendingOBDForVLPDCreation(GetPendingOBDForVLPDCreationModel items)
        {
            Payload<string> response = await _Outbound.GetPendingOBDForVLPDCreation(items);
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


        [Route("GetPick_CheckPendingList"), HttpPost]
        public async Task<IActionResult> GetPick_CheckPendingList(GetPick_CheckPendingListModel items)
        {
            Payload<string> response = await _Outbound.GetPick_CheckPendingList(items);
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


        [Route("GetPGIPendingList"), HttpPost]
        public async Task<IActionResult> GetPGIPendingList(GetPGIPendingListModel items)
        {
            Payload<string> response = await _Outbound.GetPGIPendingList(items);
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

        [Route("GetDeliveriesPendingList"), HttpPost]
        public async Task<IActionResult> GetDeliveriesPendingList(GetDeliveriesPendingListModel items)
        {
            Payload<string> response = await _Outbound.GetDeliveriesPendingList(items);
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


        [Route("GetPODPendingList"), HttpPost]
        public async Task<IActionResult> GetPODPendingList(GetPODPendingListModel items)
        {
            Payload<string> response = await _Outbound.GetPODPendingList(items);
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


        [Route("GetOBDRevertList"), HttpPost]
        public async Task<IActionResult> GetOBDRevertList(GetOBDRevertListModel items)
        {
            Payload<string> response = await _Outbound.GetOBDRevertList(items);
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

        [Route("GetOBDReleaseList"), HttpPost]
        public async Task<IActionResult> GetOBDReleaseList(GetOBDReleaseListModel items)
        {
            Payload<string> response = await _Outbound.GetOBDReleaseList(items);
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
        [Route("saveBulkReleaseItemsForOBD"), HttpPost]
        public async Task<IActionResult> saveBulkReleaseItemsForOBD(saveBulkReleaseItemsForOBDModel items)
        {
            Payload<string> response = await _Outbound.saveBulkReleaseItemsForOBD(items);
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
        [Route("GetSOsList"), HttpPost]
        public async Task<IActionResult> GetSOsList(GetSOsListModel getSOsList)
        {
            Payload<string> response = await _Outbound.GetSOsList(getSOsList);
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
        //09/03/2023
        [Route("GetOBDwiseItem"), HttpPost]
        public async Task<IActionResult> GetOBDwiseItem(GetOBDwiseItemModel getSOsList)
        {
            Payload<string> response = await _Outbound.GetOBDwiseItem(getSOsList);
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

        [Route("SetOBDRevert"), HttpPost]
        public async Task<IActionResult> SetOBDRevert(SetOBDRevertModel getSOsList)
        {
            Payload<string> response = await _Outbound.SetOBDRevert(getSOsList);
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


        //outbounddetails - Initiate Outbound Delivery>update delivery
        [Route("UpsertUpdateDelivery"), HttpPost]
        public async Task<IActionResult> UpsertUpdateDelivery(UpsertUpdateDeliveryModel items)
        {
            Payload<string> response = await _Outbound.UpsertUpdateDelivery(items);
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
        [Route("UpsertOBD"), HttpPost]
        public async Task<IActionResult> UpsertOBD(UpsertOBDInputModel upsertOBDInput)
        {
            Payload<string> response = await _Outbound.UpsertOBD(upsertOBDInput);
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

        [Route("GetPickList"), HttpPost]
        public async Task<IActionResult> GetPickList(PickListInputModel pickListInput)
        {
            Payload<string> response = await _Outbound.GetPickList(pickListInput);
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

        [Route("GetPickedItems"), HttpPost]
        public async Task<IActionResult> GetPickedItems(PickedItemsInputModel pickedItemsInput)
        {
            Payload<string> response = await _Outbound.GetPickedItems(pickedItemsInput);
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

        [Route("InsertPickItem"), HttpPost]
        public async Task<IActionResult> InsertPickItem(InsertPickItemInputModel insertPickItemInput)
        {
            Payload<string> response = await _Outbound.InsertPickItem(insertPickItemInput);
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

        [Route("DeletePickitem"), HttpPost]
        public async Task<IActionResult> DeletePickitem(DeletePickItemsinputModel deletePickItemsinput)
        {
            Payload<string> response = await _Outbound.DeletePickitem(deletePickItemsinput);
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

        [Route("GetSOLineItems"), HttpPost]
        public async Task<IActionResult> GetSOLineItems(GetSOLineItemsInputModel getSOLineItemsInput)
        {
            Payload<string> response = await _Outbound.GetSOLineItems(getSOLineItemsInput);
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
        [Route("GetSearchOutboundDetails"), HttpPost]
        public async Task<IActionResult> GetSearchOutboundDetails(GetSearchOutboundDetailsModel getSearchOutboundDetails)
        {
            Payload<string> response = await _Outbound.GetSearchOutboundDetails(getSearchOutboundDetails);
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

        [Route("UpdateShipmentDetails"), HttpPost]
        public async Task<IActionResult> UpdateShipmentDetails(UpdateShipmentDetailsModel updateShipmentDetails)
        {
            Payload<string> response = await _Outbound.UpdateShipmentDetails(updateShipmentDetails);
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

        [Route("GetPendingGoodsOutList"), HttpPost]
        public async Task<IActionResult> GetPendingGoodsOutList(GetPendingGoodsOutInputModel getPendingGoodsOutInput)
        {
            Payload<string> response = await _Outbound.GetPendingGoodsOutList(getPendingGoodsOutInput);
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

        [Route("GetPSNMaterialDetails"), HttpPost]
        public async Task<IActionResult> GetPSNMaterialDetails(DeliveryPackslipModel items)
        {
            Payload<string> response = await _Outbound.GetPSNMaterialDetails(items);
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

        [Route("UpsertPackingSlipAddMaterialInfo"), HttpPost]
        public async Task<IActionResult> UpsertPackingSlipAddMaterialInfo(UpsertPackingSlipAddMaterialInfo items)
        {
            Payload<string> response = await _Outbound.UpsertPackingSlipAddMaterialInfo(items);
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

        [Route("LoadUOMs"), HttpPost]
        public async Task<IActionResult> LoadUOMs(DeliveryPackslipModel items)
        {
            Payload<string> response = await _Outbound.LoadUOMs(items);
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

        [Route("LoadPSNMaterialItems"), HttpPost]
        public async Task<IActionResult> LoadPSNMaterialItems(DeliveryPackslipModel items)
        {
            Payload<string> response = await _Outbound.LoadPSNMaterialItems(items);
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

        [Route("DeletePSNMaterialitemDetail"), HttpPost]
        public async Task<IActionResult> DeletePSNMaterialitemDetail(DeliveryPackslipModel items)
        {
            Payload<string> response = await _Outbound.DeletePSNMaterialitemDetail(items);
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
        [Route("DeletePSNMaterialItems"), HttpPost]
        public async Task<IActionResult> DeletePSNMaterialItems(DeliveryPackslipModel items)
        {
            Payload<string> response = await _Outbound.DeletePSNMaterialItems(items);
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

        [Route("UpsertPackingSlipNumber"), HttpPost]
        public async Task<IActionResult> UpsertPackingSlipNumber(UpertPackingSlipInputModel UpertPackingSlipInput)
        {
            Payload<string> response = await _Outbound.UpsertPackingSlipNumber(UpertPackingSlipInput);
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

        //[Route("UpdatePackingSlipInformation"), HttpPost]
        //public async Task<IActionResult> UpdatePackingSlipInformation(UpdatePackingSlipInformationModel updatePackingSlipInformation)
        //{
        //    Payload<string> response = await _Outbound.UpdatePackingSlipInformation(updatePackingSlipInformation);
        //    if (response != null)
        //    {
        //        if (!response.HasErrors && !response.HasWarnings)
        //        {
        //            return Ok(response);
        //        }
        //        else if (response.Errors.Count > 0)
        //        {
        //            return StatusCode(StatusCodes.Status500InternalServerError, response.GetErrors());
        //        }
        //        else
        //        {
        //            return StatusCode(StatusCodes.Status202Accepted, response.GetWarnings());
        //        }
        //    }
        //    else
        //    {
        //        return BadRequest("Failed to retrieve data");
        //    }
        //}
        [Route("GetDeliveryNoteHeader"), HttpPost]
        public async Task<IActionResult> GetDeliveryNoteHeader(GetDeliveryNoteHeaderinputModel getDeliveryNoteHeaderinput)
        {
            Payload<string> response = await _Outbound.GetDeliveryNoteHeader(getDeliveryNoteHeaderinput);
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

        [Route("UpdatePackingSlipInformation"), HttpPost]
        public async Task<IActionResult> UpdatePackingSlipInformation(UpdatePackingSlipInformationModel updatePackingSlipInformation)
        {
            Payload<string> response = await _Outbound.UpdatePackingSlipInformation(updatePackingSlipInformation);
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

        [Route("GetInitiateOutboundDelivery"), HttpPost]
        public async Task<IActionResult> GetInitiateOutboundDelivery(GetInitiateOutboundDeliveryModel items)
        {
            Payload<string> response = await _Outbound.GetInitiateOutboundDelivery(items);
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

        [Route("UpsertDDLineItems"), HttpPost]
        public async Task<IActionResult> UpsertDDLineItems(UpsertDDLineItemsModel items)
        {
            Payload<string> response = await _Outbound.UpsertDDLineItems(items);
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

        [Route("GetPickCheckPick"), HttpPost]
        public async Task<IActionResult> GetPickCheckPick(GetPickCheckPickModel items)
        {
            Payload<string> response = await _Outbound.GetPickCheckPick(items);
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

        [Route("UpsertUpdatePGI"), HttpPost]
        public async Task<IActionResult> UpsertUpdatePGI(UpsertUpdatePGIModel items)
        {
            Payload<string> response = await _Outbound.UpsertUpdatePGI(items);
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

        [Route("GetPackingSlipData"), HttpPost]
        public async Task<IActionResult> GetPackingSlipData(GetPackingSlipDataModel items)
        {
            Payload<string> response = await _Outbound.GetPackingSlipData(items);
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


        [Route("GetRouteCode"), HttpPost]
        public async Task<IActionResult> GetRouteCode(GetRouteCodeModel items)
        {
            Payload<string> response = await _Outbound.GetRouteCode(items);
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

        [Route("GetItemMasterLoad"), HttpPost]
        public async Task<IActionResult> GetItemMasterLoad(GetItemMasterLoadModel getItemMasterLoadModel)
        {
            Payload<string> response = await _Outbound.GetItemMasterLoad(getItemMasterLoadModel);
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

        [Route("GetPackingSlipNumberData"), HttpPost]
        public async Task<IActionResult> GetPackingSlipNumberData(GetPackingSlipNumberDataModel items)
        {
            Payload<string> response = await _Outbound.GetPackingSlipNumberData(items);
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


        [Route("GetPickMaterial"), HttpPost]
        public async Task<IActionResult> GetPickMaterial(GetPickMaterialModel items)
        {
            Payload<string> response = await _Outbound.GetPickMaterial(items);
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

        [Route("UpdateDeliveryDetails"), HttpPost]
        public async Task<IActionResult> UpdateDeliveryDetails(UpdateDeliveryDetailsModel items)
        {
            Payload<string> response = await _Outbound.UpdateDeliveryDetails(items);
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


        [Route("WOComponentIssue_GetList"), HttpPost]
        public async Task<IActionResult> WOComponentIssue_GetList(WOComponentIssue_GetListModel items)
        {
            Payload<string> response = await _Outbound.WOComponentIssue_GetList(items);
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


        [Route("WOComponent_Initiate"), HttpPost]
        public async Task<IActionResult> WOComponent_Initiate(QADRequestObj items)
        {
            Payload<string> response = await _Outbound.WOComponent_Initiate(items);
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

        //<Add Sale Order Button in Delivery Document Line Items>

        [Route("Delete_DeliveryDocLineItems"), HttpPost]
        public async Task<IActionResult> Delete_DeliveryDocLineItems(Delete_DeliveryDocLineItemsModel items)
        {
            Payload<string> response = await _Outbound.Delete_DeliveryDocLineItems(items);
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


        [Route("AddDelvDocLineItem_Click"), HttpPost]
        public async Task<IActionResult> AddDelvDocLineItem_Click(AddDelvDocLineItem_ClickModel items)
        {
            Payload<string> response = await _Outbound.AddDelvDocLineItem_Click(items);
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

        [Route("GeneratePGIInvoice"), HttpPost]
        public async Task<IActionResult> GeneratePGIInvoice(GeneratePGIInvoiceModel items)
        {
            Payload<string> response = await _Outbound.GeneratePGIInvoiceData(items);
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


        [Route("GetPickingRevertData"), HttpPost]
        public async Task<IActionResult> GetPickingRevertData(SetOBDRevertNewModel items)
        {
            Payload<string> response = await _Outbound.GetPickingRevertData(items);
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


        [Route("OBDLinePickRevert"), HttpPost]
        public async Task<IActionResult> OBDLinePickRevert(SetOBDRevertNewModel items)
        {
            Payload<string> response = await _Outbound.OBDLinePickRevert(items);
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


        [Route("GenerateDeliveryPackSlip"), HttpPost]
        public async Task<IActionResult> GenerateDeliveryPackSlip(GetPackingSlipDataModel items)
        {
            Payload<string> response = await _Outbound.GenerateDeliveryPackSlip(items);
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
        [Route("UpsertDockManagementData"), HttpPost]
        public async Task<IActionResult> UpsertDockManagementData(ReceivingDockManagementDataModel items)
        {
            Payload<string> response = await _Outbound.UpsertDockManagementData(items);
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
        [Route("GetDockManagementData"), HttpPost]
        public async Task<IActionResult> GetDockManagementData(DashBoardInputModel items)
        {
            Payload<string> response = await _Outbound.GetDockManagementData(items);
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

