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
    public class OrdersController : ControllerBase
    {
        private readonly IOrders _iOrders;

        public OrdersController(IOrders Orders)
        {
            _iOrders = Orders;
        }
        [Route("GetStockPosting"), HttpPost]
        public async Task<IActionResult> GetStockPosting(GetStockPostingModel getStockPostingModel)
        {
            Payload<string> response = await _iOrders.GetStockPosting(getStockPostingModel);
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
        [Route("GetCurrentStockDynamicData"), HttpPost]
        public async Task<IActionResult> GetCurrentStockDynamicData(GetCurrentStockDynamicDataModel getCurrentStockDynamicDataModel)
        {
            Payload<string> response = await _iOrders.GetCurrentStockDynamicData(getCurrentStockDynamicDataModel);
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
        [Route("GetSuppliersReturns"), HttpPost]
        public async Task<IActionResult> GetSuppliersReturns(GetSuppliersReturnsModel getSuppliersReturnsModel)
        {
            Payload<string> response = await _iOrders.GetSuppliersReturns(getSuppliersReturnsModel);
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
       
        [Route("GetStockPostingDetails"), HttpPost]
        public async Task<IActionResult> GetStockPostingDetails(GetStockPostingInputModel getStockPostingInput)
        {
            Payload<string> response = await _iOrders.GetStockPostingDetails(getStockPostingInput);
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
        [Route("UpdateEmployeeHeaderData"), HttpPost]
        public async Task<IActionResult> UpdateEmployeeHeaderData(UpdateEmployeeHeaderDataModel updateEmployeeModel)
        {
            Payload<string> response = await _iOrders.UpdateEmployeeHeaderData(updateEmployeeModel);
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
        [Route("SaveTransferRequest"), HttpPost]
        public async Task<IActionResult> SaveTransferRequest(SaveTransferRequestModel saveTransferRequestModel)
        {
            Payload<string> response = await _iOrders.SaveTransferRequest(saveTransferRequestModel);
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

        [Route("GetSupplierReturnlist"), HttpPost]
        public async Task<IActionResult> GetSupplierReturnlist(GetSupplierReturnlistModel items)
        {
            Payload<string> response = await _iOrders.GetSupplierReturnlist(items);
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

        [Route("StockPosting"), HttpPost]
        public async Task<IActionResult> StockPosting(StockPostingModel items)
        {
            Payload<string> response = await _iOrders.StockPosting(items);
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

        [Route("InitiateStock"), HttpPost]
        public async Task<IActionResult> InitiateStock(InitiateStockModel items)
        {
            Payload<string> response = await _iOrders.InitiateStock(items);
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
        [Route("MaterialQtyUpdate"), HttpPost]
        public async Task<IActionResult> MaterialQtyUpdate(MaterialQtyUpdateModel materialQtyUpdateModel)
        {
            Payload<string> response = await _iOrders.MaterialQtyUpdate(materialQtyUpdateModel);
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
        [Route("EmployeeRequestConfirmation"), HttpPost]
        public async Task<IActionResult> EmployeeRequestConfirmation(EmployeeRequestConfirmationModel employeeRequestConfirmationModel)
        {
            Payload<string> response = await _iOrders.EmployeeRequestConfirmation(employeeRequestConfirmationModel);
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
      
        [Route("GetEmployeeRequestForm"), HttpPost]
        public async Task<IActionResult> GetEmployeeRequestForm(EmployeeRequestVerificationModel employeeRequestVerificationModel)
        {
            Payload<string> response = await _iOrders.GetEmployeeRequestForm(employeeRequestVerificationModel);
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

        [Route("InitiateToInProcess"), HttpPost]
        public async Task<IActionResult> InitiateToInProcess(InitiateToProcessModel items)
        {
            Payload<string> response = await _iOrders.InitiateToInProcess(items);
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

        [Route("CompleteMasterDetailsSetLFO"), HttpPost]
        public async Task<IActionResult> CompleteMasterDetailsSetLFO(MasterDetailsSetLFOModel items)
        {
            Payload<string> response = await _iOrders.CompleteMasterDetailsSetLFO(items);
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

        [Route("GetSuccessInfoCapture"), HttpPost]
        public async Task<IActionResult> GetSuccessInfoCapture(GetSuccessInfoCaptureModel getSuccessInfoCaptureModel)
        {
            Payload<string> response = await _iOrders.GetSuccessInfoCapture(getSuccessInfoCaptureModel);
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

        [Route("UpsertQualityVerification"), HttpPost]
        public async Task<IActionResult> UpsertQualityVerification(UpsertQualityModel items)
        {
            Payload<string> response = await _iOrders.UpsertQualityVerification(items);
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

        [Route("LabSampleRequest_InitiatePick"), HttpPost]
        public async Task<IActionResult> LabSampleRequest_InitiatePick(LabSampleRequest_InitiatePickModel items)
        {
            Payload<string> response = await _iOrders.LabSampleRequest_InitiatePick(items);
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

        [Route("MaterialPickQtyUpdate"), HttpPost]
        public async Task<IActionResult> MaterialPickQtyUpdate(MaterialPickQtyUpdateModel MaterialPickQtyUpdateModel)
        {
            Payload<string> response = await _iOrders.MaterialPickQtyUpdate(MaterialPickQtyUpdateModel);
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


        [Route("TransferSupplierReturns"), HttpPost]
        public async Task<IActionResult> TransferSupplierReturns(TransferSupplierReturnsModel items)
        {
            Payload<string> response = await _iOrders.TransferSupplierReturns(items);
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

        [Route("CurrentStock_PrintLabel"), HttpPost]
        public async Task<IActionResult> CurrentStock_PrintLabel(CurrentStock_PrintLabelModel printobj)
        {
            Payload<string> response = await _iOrders.CurrentStock_PrintLabel(printobj);
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

        [Route("UpsertCycleCountDetails"), HttpPost]
        public async Task<IActionResult> UpsertCycleCountDetails(UpsertCycleCountDetailsModel items)
        {
            Payload<string> response = await _iOrders.UpsertCycleCountDetails(items);
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

        
    
        