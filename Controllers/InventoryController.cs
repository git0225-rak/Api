using Simpolo_Endpoint.DAO.interfaces;
using Simpolo_Endpoint.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Simpolo_Endpoint.Models.InventoryModel;

namespace Simpolo_Endpoint.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventory _inventory;
        public InventoryController(IInventory inventory)
        {
            _inventory = inventory;
        }

        [Route("GetCurrentStock"), HttpPost]
        public async Task<IActionResult> GetCurrentStock(GetCurrentStockInputModel getCurrentStockInput)
        {
            Payload<string> response = await _inventory.GetCurrentStock(getCurrentStockInput);
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



        [Route("GetMovementStock"), HttpPost]
        public async Task<IActionResult> GetMovementStock(GetCurrentStockInputModel getCurrentStockInput)
        {
            Payload<string> response = await _inventory.GetMovementStock(getCurrentStockInput);
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


        [Route("UpsertMovementStock"), HttpPost]
        public async Task<IActionResult> UpsertMovementStock(GetCurrentStockInputModel getCurrentStockInput)
        {
            Payload<string> response = await _inventory.UpsertMovementStock(getCurrentStockInput);
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



        [Route("GetMiscellaneousReceipt"), HttpPost]
        public async Task<IActionResult> GetMiscellaneousReceipt(GetMiscellaneousReceiptModel getMiscellaneousReceipt)
        {
            Payload<string> response = await _inventory.GetMiscellaneousReceipt(getMiscellaneousReceipt);
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

        [Route("GetMiscellaneousReceiptTableData"), HttpPost]
        public async Task<IActionResult> GetMiscellaneousReceiptTableData(GetMiscellaneousReceiptTableDataModel obj)
        {
            Payload<string> response = await _inventory.GetMiscellaneousReceiptTableData(obj);
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


        [Route("UpdateProjectstockTransferQty"), HttpPost]
        public async Task<IActionResult> UpdateProjectstockTransferQty([FromBody] UpdateProjectstockTransferQtyModel obj)
        {
            Payload<string> response = await _inventory.UpdateProjectstockTransferQty(obj);
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

        [Route("GetProjectStockList"), HttpPost]
        public async Task<IActionResult> GetProjectStockList(GetProjectStockListModel items)
        {
            Payload<string> response = await _inventory.GetProjectStockList(items);
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

        [Route("UpdateMisslleniousReceipt"), HttpPost]
        public async Task<IActionResult> UpdateMisslleniousReceipt(UpdateMisslleniousReceiptIputModel updateMisslleniousReceipt)
        {
            Payload<string> response = await _inventory.UpdateMisslleniousReceipt(updateMisslleniousReceipt);
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


        [Route("GetMiscellaneousIssue"), HttpPost]
        public async Task<IActionResult> GetMiscellaneousIssue(GetMiscellaneousIssueModel getMiscellaneousIssue)
        {
            Payload<string> response = await _inventory.GetMiscellaneousIssue(getMiscellaneousIssue);
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

        [Route("GetColumns"), HttpPost]
        public async Task<IActionResult> GetColumns(GetColumnsInputModel getColumnsInput)
        {
            Payload<string> response = await _inventory.GetColumns(getColumnsInput);
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

        [Route("UpsertColumns"), HttpPost]
        public async Task<IActionResult> UpsertColumns([FromBody] UpsertColumnsInputModel upsertColumnsInput)
        {
            Payload<string> response = await _inventory.UpsertColumns(upsertColumnsInput);
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
        [Route("PickingItem"), HttpPost]
        public async Task<IActionResult> PickingItem(PickingItemModel items)
        {
            Payload<string> response = await _inventory.PickingItem(items);
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


        [Route("GetCurrentStockReportBySearch"), HttpPost]
        public async Task<IActionResult> GetCurrentStockReportBySearch(GetCurrentStockReportInputModel items)
        {
            Payload<string> response = await _inventory.GetCurrentStockReportBySearch(items);
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


        [Route("GetCurrentStockReport"), HttpPost]
        public async Task<IActionResult> GetCurrentStockReport(GetCurrentStockInputModel getCurrentStockInput)
        {
            Payload<string> response = await _inventory.GetCurrentStockReport(getCurrentStockInput);
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

        [Route("GetReserveStockReport"), HttpPost]
        public async Task<IActionResult> GetReserveStockReport(ReserveStockModelItems StockItems)
        {
            Payload<string> response = await _inventory.GetReserveStockReport(StockItems);
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
