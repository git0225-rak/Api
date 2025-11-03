using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simpolo_Endpoint.Models
{
    public class ReportsModel
    {
        public class InboundSummaryModel
        {
            public int InboundId { get; set; }
            public int tenantid { get; set; }
            public int Warehouseid { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public int PageSize { get; set; }
            public int PageIndex { get; set; }
            public int IsForExcel { get; set; }
        }
        public class ReceiptPendingReportModel
        {
            public int TenantID { get; set; }
            public int WHID { get; set; }
            public int InboundID { get; set; }
            public int IsExport { get; set; }
            public int NoofRecords { get; set; }
            public int PageNo { get; set; }
        }
        public class OBDTransactionListModel
        {
            public int AccountID_New { get; set; }
            public int MaterialMasterId { get; set; }
            public int Warehouseid { get; set; }
            public int NofRecordsPerPage { get; set; }
            public int Rownumber { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public int TenantID { get; set; }
        }
        public class OutboundSummaryReportModel
        {
            public int TenantID { get; set; }
            public int WarehouseID { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public int CustomerID { get; set; }
            public int OutboundID { get; set; }
            public int PageSize { get; set; }
            public int PageIndex { get; set; }
            public int SOHeaderId { get; set; }
            public int LoadSheetID { get; set; }
            public int isExport { get; set; }
        }
        public class GetSOInfoModel
        {
            public int TenantId { get; set; }
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
            public int IsExcel { get; set; }
            public int SOHID { get; set; }
        }
        public class GetMaterialAgeingReportModel
        {
            public int AgeIndays { get; set; }
            public string ZoneCode { get; set; }
            public int MaterialMasterID { get; set; }
            public int AccountID_New { get; set; }
            public int WarehouseID { get; set; }
            public int TenantID_New { get; set; }
            public int IsExport { get; set; }
            public int NofRecordsPerPage { get; set; }
            public int Rownumber { get; set; }
            public int ExpDays { get; set; }
        }
        public class GetMaterialTrackingModel
        {
            public int MaterialMasterID { get; set; }
            public int IsExport { get; set; }
            public int TenantID { get; set; }
            public int WareHouseId { get; set; }
        }
        public class GetBinReplenishmentModel
        {
            public int MaterialMasterID { get; set; }
            public int AccountID_New { get; set; }
            public int TenantID_New { get; set; }
            public int WareHouseId { get; set; }
        }
        public class GetGRNPendingListModel
        {
            public int AccountID { get; set; }
            public int TenantID { get; set; }
            public int InboundID { get; set; }
            public int MaterialMasterID { get; set; }
            public int PoHeaderID { get; set; }
            public string SupplierInvoiceID { get; set; }
            public int Warehouseid { get; set; }
            public int IsExport { get; set; }
            public int NofRecordsPerPage { get; set; }
            public int Rownumber { get; set; }
            public int UserID { get; set; }
        }
        public class GetWarehouseOperatorActivityModel
        {
            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public int AccountID_New { get; set; }
            public int TenantID_New { get; set; }
            public int WareHouseId { get; set; }
            public int UserID_New { get; set; }
        }
        public class GetExpiryDateTransferDataModel
        {
            public int TenantID { get; set; }
            public int MaterialID { get; set; }
            public string Fromdate { get; set; }
            public string Todate { get; set; }
        }
        public class UpdateExpiryDateTrasferLogModel
        {
            public List<TransferRequestData> transferRequestData { get; set; }
        }
        public class TransferRequestData
        {
            public int TransferRequestCaptureID { get; set; }
        }

        public class GetSkipLogDataReportModel
        {
            public int MaterialMasterID { get; set; }
            public int TenantID { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public string Type { get; set; }
        }

        public class GetReferenceNumbersModel
        {
            public string prefix { get; set; }
            public int CategoryID { get; set; }
        }

        public class GetTableAuditReportModel
        {
            public int CategoryID { get; set; }
            public int ReferenceId { get; set; }
        }

        public class GetIDocFailedListReportModel
        {
            public string date { get; set; }
            public string prefix { get; set; }
            public string Referencenumber { get; set; }
            public string IdocName { get; set; }
            public string DocumentType { get; set; }
            public int AccountID { get; set; }
            public int UserID { get; set; }
            public int Id { get; set; }

        }

        public class GetCycleCountReportModel
        {
            public int CycleCount_ID { get; set; }
            public int CCM_CNF_AccountCycleCount_ID { get; set; }
            public int AccountID { get; set; }
            public int MaterialMasterID { get; set; }
            public int LocationID { get; set; }
            public int WarehouseID { get; set; }
            public int NofRecordsPerPage { get; set; }
            public int Rownumber { get; set; }
            public int IsBincomplete { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public int IsExcel { get; set; }
        }
        public class GetOperatorSummaryReportModel
        {
            public int TenantID { get; set; }
            public int WarehouseId { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public int UserID { get; set; }
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
            public int OutboundID { get; set; }
            public int SOHeaderID { get; set; }
            public int LoadSheetID { get; set; }
        }

        public class GetExpiredMaterialReportModel
        {
            public int AccountID { get; set; }
            public int TenantID { get; set; }
            public int MaterialMasterID { get; set; }
            public int NoofRecords { get; set; }
            public int PageNo { get; set; }
            public int WarehouseId { get; set; }
            public int IsExport { get; set; }
        }

        public class GetSupplierReturnsReportModel
        {
            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public int UserID { get; set; }
            public int AccountID { get; set; }
            public int TenantID { get; set; }
            public int NofRecordsPerPage { get; set; }
            public int Rownumber { get; set; }
            public int WarehouseId { get; set; }
        }


        public class ClearAuditSkipLogModel
        {
            public string Type { get; set; }
            public List<ClearItems> items { get; set; }
        }

        public class ClearItems
        {
            public int MaterialID { get; set; }
            public int LocationId { get; set; }
            public decimal SkipQuantity { get; set; }
        }
        public class GetInboundDetailsReportModel
        {
            public int InboundId { get; set; }
            public int @MaterialMasterID { get; set; }
            public int tenantid { get; set; }
            public int Warehouseid { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public int PageSize { get; set; }
            public int PageIndex { get; set; }
            public int IsForExcel { get; set; }
            public int InboundStatusID { get; set; }
        }
        public class GetOutboundDetailsReportModel
        {
            public int TenantID { get; set; }
            public int WarehouseID { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public int CustomerID { get; set; }
            public int OutboundID { get; set; }
            public int PageSize { get; set; }
            public int PageIndex { get; set; }
            public int SOHeaderId { get; set; }
            public int LoadSheetID { get; set; }
            public int IsExcel { get; set; }
            public int OrderTypeID { get; set; }
            public string VehicleNumber { get; set; }

            public string VLPDNumber { get; set; }
        }

        public class GetWareHouse_StockInformation_ReportModel
        {
            public int TenantID { get; set; }
            public int WarehouseID { get; set; }
            public int MaterialMasterID { get; set; }
            //public string ToDate { get; set; }
            public int IsExcel { get; set; }
            public int PageSize { get; set; }
            public int PageIndex { get; set; }
        }
        public class GetLogAuditDataModel
        {
            public int UserId { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public string IPAddress { get; set; }
        }

        public class ClearLogAuditDataModel
        {
            public int UserId { get; set; }
            public int LoginUserID { get; set; }
            public int AccountID_New { get; set; }
            public string IPAddress { get; set; }
        }
    }
}

