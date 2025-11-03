using Simpolo_Endpoint.DAO.interfaces;
using Simpolo_Endpoint.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Simpolo_Endpoint.Models.ReportsModel;
using Newtonsoft.Json;
using static Azure.Core.HttpHeader;
using Simpolo_Endpoint.Entities;

namespace Simpolo_Endpoint.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReports _reports;
        public ReportsController(IReports reports)
        {
            _reports = reports;
        }

        [Route("GetInBoundSummary"), HttpPost]
        public async Task<IActionResult> GetInBoundSummary(InboundSummaryModel inboundSummary)
        {
            Payload<string> response = await _reports.GetInBoundSummary(inboundSummary);
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

        [Route("GetReceiptPendingReport"), HttpPost]
        public async Task<IActionResult> GetReceiptPendingReport(ReceiptPendingReportModel receiptPendingReport)
        {
            Payload<string> response = await _reports.GetReceiptPendingReport(receiptPendingReport);
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

        [Route("GetOBDTransactionList"), HttpPost]
        public async Task<IActionResult> GetOBDTransactionList(OBDTransactionListModel oBDTransactionList)
        {
            Payload<string> response = await _reports.GetOBDTransactionList(oBDTransactionList);
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

        [Route("GetOutboundSummaryReport"), HttpPost]
        public async Task<IActionResult> GetOutboundSummaryReport(OutboundSummaryReportModel outboundSummaryReport)
        {
            Payload<string> response = await _reports.GetOutboundSummaryReport(outboundSummaryReport);
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

        [Route("GetSOInfo"), HttpPost]
        public async Task<IActionResult> GetSOInfo(GetSOInfoModel getSOInfo)
        {
            Payload<string> response = await _reports.GetSOInfo(getSOInfo);
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

        [Route("GetMaterialAgeingReport"), HttpPost]
        public async Task<IActionResult> GetMaterialAgeingReport(GetMaterialAgeingReportModel getMaterialAgeingReport)
        {
            Payload<string> response = await _reports.GetMaterialAgeingReport(getMaterialAgeingReport);
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

        [Route("GetMaterialTracking"), HttpPost]
        public async Task<IActionResult> GetMaterialTracking(GetMaterialTrackingModel getMaterialTracking)
        {
            Payload<string> response = await _reports.GetMaterialTracking(getMaterialTracking);
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

        [Route("GetBinReplenishment"), HttpPost]
        public async Task<IActionResult> GetBinReplenishment(GetBinReplenishmentModel getBinReplenishment)
        {
            Payload<string> response = await _reports.GetBinReplenishment(getBinReplenishment);
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

        [Route("GetGRNPendingList"), HttpPost]
        public async Task<IActionResult> GetGRNPendingList(GetGRNPendingListModel getGRNPendingList)
        {
            Payload<string> response = await _reports.GetGRNPendingList(getGRNPendingList);
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

        [Route("GetWarehouseOperatorActivity"), HttpPost]
        public async Task<IActionResult> GetWarehouseOperatorActivity(GetWarehouseOperatorActivityModel getWarehouseOperatorActivity)
        {
            Payload<string> response = await _reports.GetWarehouseOperatorActivity(getWarehouseOperatorActivity);
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

        [Route("GetExpiryDateTransferData"), HttpPost]
        public async Task<IActionResult> GetExpiryDateTransferData(GetExpiryDateTransferDataModel getExpiryDateTransferData)
        {
            Payload<string> response = await _reports.GetExpiryDateTransferData(getExpiryDateTransferData);
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

        [Route("UpdateExpiryDateTrasferLog"), HttpPost]
        public async Task<IActionResult> UpdateExpiryDateTrasferLog([FromBody] UpdateExpiryDateTrasferLogModel updateExpiryDateTrasferLog)
        {
            Payload<string> response = await _reports.UpdateExpiryDateTrasferLog(updateExpiryDateTrasferLog);
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

        [Route("GetRefnoDropdown"), HttpPost]
        public async Task<IActionResult> GetRefnoDropdown(GetIDocFailedListReportModel items)
        {
            Payload<string> response = await _reports.GetRefnoDropdown(items);
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


        [Route("GetJSONAPITypes"), HttpPost]
        public async Task<IActionResult> GetJSONAPITypes(GetIDocFailedListReportModel items)
        {
            Payload<string> response = await _reports.GetJSONAPITypes(items);
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



        [Route("GetOperatorSummaryReport"), HttpPost]
        public async Task<IActionResult> GetOperatorSummaryReport(GetOperatorSummaryReportModel items)
        {
            Payload<string> response = await _reports.GetOperatorSummaryReport(items);
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


        //Warehouse > ExpiredMaterialReport GET
        [Route("GetExpiredMaterialReport"), HttpPost]
        public async Task<IActionResult> GetExpiredMaterialReport(GetExpiredMaterialReportModel items)
        {
            Payload<string> response = await _reports.GetExpiredMaterialReport(items);
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

        //Warehouse > SupplierReturns GET
        [Route("GetSupplierReturnsReport"), HttpPost]
        public async Task<IActionResult> GetSupplierReturnsReport(GetSupplierReturnsReportModel items)
        {
            Payload<string> response = await _reports.GetSupplierReturnsReport(items);
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

        //Audits> SkipLog
        [Route("GetSkipLogDataReport"), HttpPost]
        public async Task<IActionResult> GetSkipLogDataReport(GetSkipLogDataReportModel items)
        {
            Payload<string> response = await _reports.GetSkipLogDataReport(items);
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

        //Audits > CycleCountReport get
        [Route("GetCycleCountReport"), HttpPost]
        public async Task<IActionResult> GetCycleCountReport(GetCycleCountReportModel items)
        {
            Payload<string> response = await _reports.GetCycleCountReport(items);
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

        //get reference numbers
        [Route("GetReferenceNumbers"), HttpPost]
        public async Task<IActionResult> GetReferenceNumbers(GetReferenceNumbersModel items)
        {
            Payload<string> response = await _reports.GetReferenceNumbers(items);
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

        //get AuditLogReport
        [Route("GetTableAuditReport"), HttpPost]
        public async Task<IActionResult> GetTableAuditReport(GetTableAuditReportModel items)
        {
            Payload<string> response = await _reports.GetTableAuditReport(items);
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


        // Failed QDoc List page
        [Route("GetIDocFailedListReport"), HttpPost]
        public async Task<IActionResult> GetIDocFailedListReport(GetIDocFailedListReportModel items)
        {
            Payload<string> response = await _reports.GetIDocFailedListReport(items);
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

        //Audits > SkipLog > Clear Log button
        [Route("ClearAuditSkipLog"), HttpPost]
        public async Task<IActionResult> ClearAuditSkipLog(ClearAuditSkipLogModel items)
        {
            Payload<string> response = await _reports.ClearAuditSkipLog(items);
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

        [Route("GetInboundDetailsReport"), HttpPost]
        public async Task<IActionResult> GetInboundDetailsReport(GetInboundDetailsReportModel items)
        {
            Payload<string> response = await _reports.GetInboundDetailsReport(items);
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

        [Route("GetOutboundDetailsReport"), HttpPost]
        public async Task<IActionResult> GetOutboundDetailsReport(GetOutboundDetailsReportModel items)
        {
            Payload<string> response = await _reports.GetOutboundDetailsReport(items);
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

        [Route("GetWareHouse_StockInformation_Report"), HttpPost]
        public async Task<IActionResult> GetWareHouse_StockInformation_Report(GetWareHouse_StockInformation_ReportModel items)
        {
            Payload<string> response = await _reports.GetWareHouse_StockInformation_Report(items);
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

        [Route("GetTotalIdocReport"), HttpPost]
        public async Task<IActionResult> GetTotalIdocReport()
        {
            Payload<string> response = await _reports.GetTotalIdocReport();
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


        [Route("ActiveFailedIdoc"), HttpPost]
        public async Task<IActionResult> ActiveFailedIdoc(GetIDocFailedListReportModel items)
        {
            Payload<string> response = await _reports.ActiveFailedIdoc(items);
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
        [Route("GetVehicleManagementData"), HttpPost]
        public async Task<IActionResult> GetVehicleManagementData(DashBoardInputModel obj)
        {
            Payload<string> response = await _reports.GetVehicleManagementData(obj);
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
        [Route("LoadingScanReport"), HttpPost]
        public async Task<IActionResult> LoadingScanReportData(DashBoardInputModel obj)
        {
            Payload<string> response = await _reports.LoadingScanReportData(obj);
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
        [Route("VehicleEntryAndExitReport"), HttpPost]
        public async Task<IActionResult> VehicleEntryAndExitReport(DashBoardInputModel obj)
        {
            Payload<string> response = await _reports.VehicleEntryAndExitReport(obj);
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
        [Route("StockAdjustmentReport"), HttpPost]
        public async Task<IActionResult> StockAdjustmentReport(DashBoardInputModel obj)
        {
            Payload<string> response = await _reports.StockAdjustmentReport(obj);
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
        [Route("GetPalletConsolidationReport"),HttpPost]
        public async Task<IActionResult> PalletConsolidationReport(DashBoardInputModel obj)
        {
           
            Payload<string> reponse =await _reports.PalletConsolidationReport(obj);
            if (reponse != null)
            {
                if (!reponse.HasErrors && !reponse.HasWarnings)
                {
                    return Ok(reponse);
                }
                else if (reponse.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, reponse.GetErrors());
                }
                else
                {
                    return StatusCode(StatusCodes.Status202Accepted, reponse.GetWarnings());
                }
            }
            else
            {
                return BadRequest("Failed To Retrieve Data");
            }
        }
        [Route("GetPalletUtilizationReport"),HttpPost]
        public async Task<IActionResult> PalletUtilizationReport(DashBoardInputModel obj)
        {
            Payload<string> response = await _reports.PalletUtilizationReport(obj);
            if (response != null)
            {
                if(!response.HasErrors && !response.HasWarnings)
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
                return BadRequest("Failed To Retrieve Data");
            }
        }
        [Route("GetLogAuditData"), HttpPost]
        public async Task<IActionResult> GetLogAuditData(GetLogAuditDataModel obj)
        {
            Payload<string> response = await _reports.GetLogAuditData(obj);
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
                return BadRequest("Failed To Retrieve Data");
            }
        }

        [Route("ClearLogAuditData"), HttpPost]
        public async Task<IActionResult> ClearLogAuditData(ClearLogAuditDataModel obj)
        {
            Payload<string> response = await _reports.ClearLogAuditData(obj);
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
                return BadRequest("Failed To Retrieve Data");
            }
        }
    }    
}
