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
    public class HouseKeepingController : ControllerBase
    {
        private readonly IHouseKeeping _IHouseKeeping;
        public HouseKeepingController(IHouseKeeping HouseKeeping)
        {
            _IHouseKeeping = HouseKeeping;
        }
        [Route("GetFastMovingLocsTransferList"), HttpPost]
        public async Task<IActionResult> GetFastMovingLocsTransferList(GetFastMovingLocsTransferListModel items)
        {
            Payload<string> response = await _IHouseKeeping.GetFastMovingLocsTransferList(items);
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




        [Route("GetTransferRequestNumbers_HHT"), HttpPost]
        public async Task<IActionResult> GetTransferRequestNumbers_HHT(TransferRequestModel items)
        {
            Payload<string> response = await _IHouseKeeping.GetTransferRequestNumbers_HHT(items);
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



        [Route("GetTransferRequestNumbers_HHT_PalletConsolidate"), HttpPost]
        public async Task<IActionResult> GetTransferRequestNumbers_HHT_PalletConsolidate(TransferRequestModel items)
        {
            Payload<string> response = await _IHouseKeeping.GetTransferRequestNumbers_HHT_PalletConsolidate(items);
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




        [Route("DeleteFastMovingLocsTransfer"), HttpPost]
        public async Task<IActionResult> DeleteFastMovingLocsTransfer(DeleteFastMovingLocsTransferModel items)
        {
            Payload<string> response = await _IHouseKeeping.DeleteFastMovingLocsTransfer(items);
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



        [Route("ApprovedBatchGradeTransfers"), HttpPost]
        public async Task<IActionResult> ApprovedBatchGradeTransfers(GetInternalTransferHeaderModel items)
        {
            Payload<string> response = await _IHouseKeeping.ApprovedBatchGradeTransfer(items);
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



        [Route("RepostMaterialDetails_SAP"), HttpPost]
        public async Task<IActionResult> RepostMaterialDetails_SAP(InitiateToProcessModel items)
        {
            Payload<string> response = await _IHouseKeeping.RepostMaterialDetails_SAP(items);
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




        [Route("GetInternalTransferHeader"), HttpPost]
        public async Task<IActionResult> GetInternalTransferHeader(GetInternalTransferHeaderModel items)
        {
            Payload<AuthResponce> response = await _IHouseKeeping.GetInternalTransferHeader(items);
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

        [Route("UpsertInternalTransferHeader"), HttpPost]
        public async Task<IActionResult> UpsertInternalTransferHeader(UpsertInternalTransferHeaderModel items)
        {
            Payload<string> response = await _IHouseKeeping.UpsertInternalTransferHeader(items);
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

        [Route("GetInternalTransferDetails"), HttpPost]
        public async Task<IActionResult> GetInternalTransferDetails(GetInternalTransferDetailsModel items)
        {
            Payload<string> response = await _IHouseKeeping.GetInternalTransferDetails(items);
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


        [Route("GetInternalTransferDetails_VLPD"), HttpPost]
        public async Task<IActionResult> GetInternalTransferDetails_VLPD(GetInternalTransferDetailsModel items)
        {
            Payload<string> response = await _IHouseKeeping.GetInternalTransferDetails_VLPD(items);
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

        [Route("GetAvailableQty"), HttpPost]
        public async Task<IActionResult> GetAvailableQty(GetAvailableQtyModel getAvailableQtyModel)
        {
            Payload<string> response = await _IHouseKeeping.GetAvailableQty(getAvailableQtyModel);
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

        [Route("UpsertInternalTransferDetails"), HttpPost]
        public async Task<IActionResult> UpsertInternalTransferDetails(UpsertInternalTransferDetailsModel items)
        {
            Payload<string> response = await _IHouseKeeping.UpsertInternalTransferDetails(items);
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

        [Route("UpsertExpiryDateTransferRequestDetails"), HttpPost]
        public async Task<IActionResult> UpsertExpiryDateTransferRequestDetails(UpsertExpiryDateTransferRequestDetailsModel items)
        {
            Payload<string> response = await _IHouseKeeping.UpsertExpiryDateTransferRequestDetails(items);
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


        [Route("DeleteInternalTransferDetails"), HttpPost]
        public async Task<IActionResult> DeleteInternalTransferDetails(DeleteInternalTransferDetailsModel items)
        {

            Payload<string> response = await _IHouseKeeping.DeleteInternalTransferDetails(items);
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

        [Route("GetCycleCountList"), HttpPost]
        public async Task<IActionResult> GetCycleCountList(GetCycleCountListModel items)
        {
            Payload<string> response = await _IHouseKeeping.GetCycleCountList(items);
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

        [Route("DeleteCyclecountlist"), HttpPost]
        public async Task<IActionResult> DeleteCyclecountlist(DeleteccOrderModel obj)
        {
            Payload<string> response = await _IHouseKeeping.DeleteCyclecountlist(obj);
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

        [Route("UpsertCycleCountHeader"), HttpPost]
        public async Task<IActionResult> UpsertCycleCountHeader(UpsertCycleCountHeaderModel items)
        {
            Payload<string> response = await _IHouseKeeping.UpsertCycleCountHeader(items);
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

        [Route("GetCycleCountEntityConfiguration"), HttpPost]
        public async Task<IActionResult> GetCycleCountEntityConfiguration(GetCycleCountEntityConfigurationModel items)
        {
            Payload<string> response = await _IHouseKeeping.GetCycleCountEntityConfiguration(items);
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


        [Route("GetCycleCountTransactionList"), HttpPost]
        public async Task<IActionResult> GetCycleCountTransactionList(GetCycleCountlistModel items)
        {
            Payload<string> response = await _IHouseKeeping.GetCycleCountTransactionList(items);
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

        [Route("GetCycleCountTransactionListByStatus"), HttpPost]
        public async Task<IActionResult> GetCycleCountTransactionListByStatus(GetCycleCountTransactionListModel getCycleCountTransaction)
        {
            Payload<string> response = await _IHouseKeeping.GetCycleCountTransactionListByStatus(getCycleCountTransaction);
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

        [Route("GetCCBlockedLocations"), HttpPost]
        public async Task<IActionResult> GetCCBlockedLocations(GetCCBlockedLocationsModel getCCBlockedLocationsModel)
        {
            Payload<string> response = await _IHouseKeeping.GetCCBlockedLocations(getCCBlockedLocationsModel);
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
        [Route("GetStockAadjustmentNewList"), HttpPost]
        public async Task<IActionResult> GetStockAadjustmentNewList(GetStockAadjustmentNewListModel getStockAadjustmentNewListModel)
        {
            Payload<string> response = await _IHouseKeeping.GetStockAadjustmentNewList(getStockAadjustmentNewListModel);
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


        [Route("UpsertBinToBinTransferItem"), HttpPost]
        public async Task<IActionResult> UpsertBinToBinTransferItem(UpsertBinToBinTransferModel obj)
        {
            Payload<string> response = await _IHouseKeeping.UpsertBinToBinTransferItem(obj);
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



        [Route("DeleteCyclecountDetails"), HttpPost]
        public async Task<IActionResult> DeleteCyclecountDetails(DeleteccOrderModel obj)
        {
            Payload<string> response = await _IHouseKeeping.DeleteCyclecountDetails(obj);
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


        [Route("CreateCC_EntityConfiguration"), HttpPost]
        public async Task<IActionResult> CreateCC_EntityConfiguration(CreateCC_EntityConfigurationModel items)
        {
            Payload<string> response = await _IHouseKeeping.CreateCC_EntityConfiguration(items);
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
        [Route("GetCC_ShipmentVerificationDetails"), HttpPost]
        public async Task<IActionResult> GetCC_ShipmentVerificationDetails(GetCC_ShipmentDetailsModel items)
        {
            Payload<string> response = await _IHouseKeeping.GetCC_ShipmentVerificationDetails(items);
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
        [Route("SaveCC_ShipmentVerificationDetails"), HttpPost]
        public async Task<IActionResult> SaveCC_ShipmentVerificationDetails(SaveCC_ShipmentDetailsModel items)
        {
            Payload<string> response = await _IHouseKeeping.SaveCC_ShipmentVerificationDetails(items);
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

        [Route("UpsertCCtransactionInitiate"), HttpPost]
        public async Task<IActionResult> UpsertCCtransactionInitiate(UpsertCCtransactionModel items)
        {
            Payload<string> response = await _IHouseKeeping.UpsertCCtransactionInitiate(items);
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

        [Route("GetUnmappedSKUList"), HttpPost]
        public async Task<IActionResult> GetUnmappedSKUList(GetUnmappedSKUListModel items)
        {
            Payload<string> response = await _IHouseKeeping.GetUnmappedSKUList(items);
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

        [Route("SaveUnmappedZoneSKUData"), HttpPost]
        public async Task<IActionResult> SaveUnmappedZoneSKUData(GetUnmappedSKUListModel items)
        {
            Payload<string> response = await _IHouseKeeping.SaveUnmappedZoneSKUData(items);
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
