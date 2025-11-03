using Simpolo_Endpoint.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Simpolo_Endpoint.Models.ReportsModel;

namespace Simpolo_Endpoint.DAO.interfaces
{
    public interface IReports
    {
        Task<Payload<string>> GetInBoundSummary(InboundSummaryModel inboundSummary);
        Task<Payload<string>> GetReceiptPendingReport(ReceiptPendingReportModel receiptPendingReport);
        Task<Payload<string>> GetOBDTransactionList(OBDTransactionListModel oBDTransactionList);
        Task<Payload<string>> GetOutboundSummaryReport(OutboundSummaryReportModel outboundSummaryReport);
        Task<Payload<string>> GetSOInfo(GetSOInfoModel getSOInfo);
        Task<Payload<string>> GetMaterialAgeingReport(GetMaterialAgeingReportModel getMaterialAgeingReport);
        Task<Payload<string>> GetMaterialTracking(GetMaterialTrackingModel getMaterialTracking);
        Task<Payload<string>> GetBinReplenishment(GetBinReplenishmentModel getBinReplenishment);
        Task<Payload<string>> GetGRNPendingList(GetGRNPendingListModel getGRNPending);
        Task<Payload<string>> GetWarehouseOperatorActivity(GetWarehouseOperatorActivityModel getWarehouseOperatorActivity);
        Task<Payload<string>> GetExpiryDateTransferData(GetExpiryDateTransferDataModel getExpiryDateTransferData);
        Task<Payload<string>> UpdateExpiryDateTrasferLog([FromBody] UpdateExpiryDateTrasferLogModel updateExpiryDateTrasferLog);
        Task<Payload<string>> GetCycleCountReport(GetCycleCountReportModel items);
        Task<Payload<string>> GetSkipLogDataReport(GetSkipLogDataReportModel items);
        Task<Payload<string>> GetReferenceNumbers(GetReferenceNumbersModel items);
        Task<Payload<string>> GetTableAuditReport(GetTableAuditReportModel items);
        Task<Payload<string>> GetIDocFailedListReport(GetIDocFailedListReportModel items);
        Task<Payload<string>> GetOperatorSummaryReport(GetOperatorSummaryReportModel items);
        Task<Payload<string>> GetExpiredMaterialReport(GetExpiredMaterialReportModel items);
        Task<Payload<string>> GetSupplierReturnsReport(GetSupplierReturnsReportModel items);
        Task<Payload<string>> ClearAuditSkipLog(ClearAuditSkipLogModel items);
        Task<Payload<string>> GetInboundDetailsReport(GetInboundDetailsReportModel items);
        Task<Payload<string>> GetOutboundDetailsReport(GetOutboundDetailsReportModel items);
        Task<Payload<string>> GetWareHouse_StockInformation_Report(GetWareHouse_StockInformation_ReportModel items);
        Task<Payload<string>> GetRefnoDropdown(GetIDocFailedListReportModel items);
        Task<Payload<string>> GetJSONAPITypes(GetIDocFailedListReportModel items);
        Task<Payload<string>> GetTotalIdocReport();
        Task<Payload<string>> ActiveFailedIdoc(GetIDocFailedListReportModel items);
        Task<Payload<string>> GetVehicleManagementData(DashBoardInputModel obj);
        Task<Payload<string>> LoadingScanReportData(DashBoardInputModel obj);
        Task<Payload<string>> VehicleEntryAndExitReport(DashBoardInputModel obj);
        Task<Payload<string>> StockAdjustmentReport(DashBoardInputModel obj);
        Task<Payload<string>> PalletConsolidationReport(DashBoardInputModel obj);
        Task<Payload<string>> PalletUtilizationReport(DashBoardInputModel obj);
        Task<Payload<string>> GetLogAuditData(GetLogAuditDataModel obj);
        Task<Payload<string>> ClearLogAuditData(ClearLogAuditDataModel obj);
    }
}
