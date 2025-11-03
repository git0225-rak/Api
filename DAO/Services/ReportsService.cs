using Simpolo_Endpoint.DAO.interfaces;
using Simpolo_Endpoint.DBUtil;
using Simpolo_Endpoint.Models;
using Simpolo_Endpoint.ModelsLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using static Simpolo_Endpoint.Models.ReportsModel;

namespace Simpolo_Endpoint.DAO.Services
{
    public class ReportsService : AppDBService, IReports
    {
        public ReportsService(IOptions<AppSettings> appSettings) : base(appSettings)
        {

        }

        //inbound/InBoundSummary
        public async Task<Payload<string>> GetInBoundSummary(InboundSummaryModel inboundSummary)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                string fromDate = inboundSummary.FromDate;
                string toDate = inboundSummary.ToDate;
                string FromDate = "";
                string ToDate = "";
                DateTime fromDateValue;
                DateTime ToDateValue;
                if (!string.IsNullOrEmpty(fromDate) && DateTime.TryParse(fromDate, out fromDateValue))
                {
                    FromDate = "'" + fromDate + "'";
                }
                else
                {
                    FromDate = string.IsNullOrEmpty(inboundSummary.FromDate) ? "null" : "" + inboundSummary.FromDate + "";
                }
                if (!string.IsNullOrEmpty(toDate) && DateTime.TryParse(toDate, out ToDateValue))
                {

                    ToDate = "'" + toDate + "'";
                }
                else
                {
                    ToDate = string.IsNullOrEmpty(inboundSummary.ToDate) ? "null" : "" + inboundSummary.ToDate + "";
                }
                string Query = "Exec[dbo].[USP_RPT_InboundSummaryReport_New] @InboundId = " + inboundSummary.InboundId + ",@tenantid = " + inboundSummary.tenantid + ",@Warehouseid = " + inboundSummary.Warehouseid + ",@FromDate = " + FromDate + ",@ToDate = " + ToDate + ",@PageSize = " + inboundSummary.PageSize + ",@PageIndex = " + inboundSummary.PageIndex+ ",@IsForExcel="+inboundSummary.IsForExcel + "";
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDatasetINQ(this.ConnectionString, Query);
            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }

        //InBound/ReceiptPendingReport
        public async Task<Payload<string>> GetReceiptPendingReport(ReceiptPendingReportModel receiptPendingReport)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                    {"@TenantID",receiptPendingReport.TenantID },
                    {"@WHID",receiptPendingReport.WHID },
                    {"@InboundID",receiptPendingReport.InboundID },
                    {"@IsExport",receiptPendingReport.IsExport },
                    {"@NoofRecords",receiptPendingReport.NoofRecords },
                    {"@PageNo",receiptPendingReport.PageNo }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "sp_RPT_INV_GetPendingGoodsInList", sqlParams).ConfigureAwait(false);
            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }

        //Outbound Transactions History
        public async Task<Payload<string>> GetOBDTransactionList(OBDTransactionListModel oBDTransactionList)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    {"@AccountID_New",oBDTransactionList.AccountID_New },
                    {"@TenantID_New",oBDTransactionList.TenantID },
                    {"@MaterialMasterId",oBDTransactionList.MaterialMasterId },
                    {"@Warehouseid",oBDTransactionList.Warehouseid },
                    {"@NofRecordsPerPage",oBDTransactionList.NofRecordsPerPage },
                    {"@Rownumber",oBDTransactionList.Rownumber },
                    {"@StartDate",oBDTransactionList.FromDate },
                    {"@EndDate",oBDTransactionList.ToDate },
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "sp_RPT_GetOutboundTransactionsHistory", sqlParams).ConfigureAwait(false);
            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }

        //Outbound Outbound Summary
        public async Task<Payload<string>> GetOutboundSummaryReport(OutboundSummaryReportModel outboundSummaryReport)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                //var sqlParams = new Dictionary<string, object> {
                //    {"@TenantID",outboundSummaryReport.TenantID },
                //    {"@WarehouseID",outboundSummaryReport.WarehouseID },
                //    {"@FromDate",outboundSummaryReport.FromDate },
                //    {"@ToDate",outboundSummaryReport.ToDate },
                //    {"@CustomerID",outboundSummaryReport.CustomerID },
                //    {"@OutboundID",outboundSummaryReport.OutboundID },
                //    {"@PageSize",outboundSummaryReport.PageSize },
                //    {"@PageIndex",outboundSummaryReport.PageIndex },
                //    {"@SOHeaderId",outboundSummaryReport.SOHeaderId },
                //    {"@LoadSheetID",outboundSummaryReport.LoadSheetID },
                //    {"@IsExcel",outboundSummaryReport.isExport }

                //};
                //DBFactory factory = new DBFactory();
                //IDBUtility DbUtility = factory.getDBUtility();
                //response.Result = await DbUtility.GetjsonDataFromDatasetINQ(this.ConnectionString, "sp_RPT_GetOutboundSummaryReport", sqlParams).ConfigureAwait(false);
                string Query = "Exec[dbo].[sp_RPT_GetOutboundSummaryReport] @OutboundID = " + outboundSummaryReport.OutboundID + ",@TenantID = " + outboundSummaryReport.TenantID + ",@WarehouseID = " + outboundSummaryReport.WarehouseID + ",@FromDate = '" + outboundSummaryReport.FromDate + "',@ToDate = '" + outboundSummaryReport.ToDate + "',@PageSize = " + outboundSummaryReport.PageSize + ",@PageIndex = " + outboundSummaryReport.PageIndex + ",@IsExcel=" + outboundSummaryReport.isExport + ",@CustomerID="+outboundSummaryReport.CustomerID+ ",@SOHeaderId="+outboundSummaryReport.SOHeaderId+ ",@LoadSheetID="+outboundSummaryReport.LoadSheetID+"";
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDatasetINQ(this.ConnectionString, Query);
            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }

        //GetSOInfoModel /reports/ outbound
        public async Task<Payload<string>> GetSOInfo(GetSOInfoModel getSOInfo)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                    {"@TenantId",getSOInfo.TenantId },
                    {"@PageIndex",getSOInfo.PageIndex },
                    {"@PageSize",getSOInfo.PageSize },
                    {"@IsExcel",getSOInfo.IsExcel },
                    {"@SOHID",getSOInfo.SOHID },

                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_RPT_Get_SAP_CancelInvoiceNumberInfo", sqlParams).ConfigureAwait(false);
            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }

        //MaterialAgeingReport
        public async Task<Payload<string>> GetMaterialAgeingReport(GetMaterialAgeingReportModel getMaterialAgeingReport)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                    {"@AgeIndays",getMaterialAgeingReport.AgeIndays },
                    {"@ZoneCode",getMaterialAgeingReport.ZoneCode },
                    {"@MaterialMasterID",getMaterialAgeingReport.MaterialMasterID },
                    {"@WarehouseID",getMaterialAgeingReport.WarehouseID },
                    {"@TenantID_New",getMaterialAgeingReport.TenantID_New },
                    {"@AccountID_New",getMaterialAgeingReport.AccountID_New },
                    {"@IsExport",getMaterialAgeingReport.IsExport },
                    {"@NofRecordsPerPage",getMaterialAgeingReport.NofRecordsPerPage },
                    {"@Rownumber",getMaterialAgeingReport.Rownumber },
                    {"@ExpDays",getMaterialAgeingReport.ExpDays },

                 };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "sp_RPT_GetMaterialAgeingReport", sqlParams).ConfigureAwait(false);
            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }

        //Material Traceability Report
        public async Task<Payload<string>> GetMaterialTracking(GetMaterialTrackingModel getMaterialTracking)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                StringBuilder sp = new StringBuilder();
                sp.AppendLine("EXEC [sp_RPT_GetMaterialTracking_In] @MaterialMasterID =" + getMaterialTracking.MaterialMasterID + ",@IsExport=" + getMaterialTracking.IsExport + ",@TenantID=" + getMaterialTracking.TenantID + ",@WareHouseId=" + getMaterialTracking.WareHouseId + ";"
                + "EXEC [sp_RPT_GetMaterialTracking_Out] @MaterialMasterID =" + getMaterialTracking.MaterialMasterID + ",@IsExport=" + getMaterialTracking.IsExport + ",@TenantID=" + getMaterialTracking.TenantID + ",@WareHouseId=" + getMaterialTracking.WareHouseId);
                string output = sp.ToString();
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                var jsonData = await DbUtility.GetjsonDataFromDatasetINQ(this.ConnectionString, output);
                DataSet dataSet = JsonConvert.DeserializeObject<DataSet>(jsonData);

                if (dataSet.Tables[0].Rows.Count != 0)
                {
                    dataSet.Tables[0].Columns["PONumber"].ColumnName = "PO No.";
                    dataSet.Tables[0].Columns["PODate"].ColumnName = "PO Date";
                    dataSet.Tables[0].Columns["InvoiceNumber"].ColumnName = "Invoice Number";
                    dataSet.Tables[0].Columns["InvoiceDate"].ColumnName = "Invoice Date";
                    dataSet.Tables[0].Columns["TenantName"].ColumnName = "Tenant";
                    dataSet.Tables[0].Columns["SupplierName"].ColumnName = "Supplier";
                    dataSet.Tables[0].Columns["StoreRefNo"].ColumnName = "Store Ref. No.";
                    dataSet.Tables[0].Columns["ProjectRefNo"].ColumnName = "Project Ref. No.";
                    dataSet.Tables[0].Columns["MCode"].ColumnName = "SKU";
                    dataSet.Tables[0].Columns["Quantity"].ColumnName = "Received Qty.";

                    dataSet.Tables[0].Columns["PO No."].SetOrdinal(0);
                    dataSet.Tables[0].Columns["PO Date"].SetOrdinal(1);
                    dataSet.Tables[0].Columns["Invoice Number"].SetOrdinal(2);
                    dataSet.Tables[0].Columns["Invoice Date"].SetOrdinal(3);
                    dataSet.Tables[0].Columns["Tenant"].SetOrdinal(4);
                    dataSet.Tables[0].Columns["Supplier"].SetOrdinal(5);
                    dataSet.Tables[0].Columns["Store Ref. No."].SetOrdinal(6);
                    dataSet.Tables[0].Columns["Project Ref. No."].SetOrdinal(7);
                    dataSet.Tables[0].Columns["SKU"].SetOrdinal(8);
                    dataSet.Tables[0].Columns["Received Qty."].SetOrdinal(9);
                }

                if (dataSet.Tables[1].Rows.Count != 0)
                {
                    dataSet.Tables[1].Columns["SONumber"].ColumnName = "SO No.";
                    dataSet.Tables[1].Columns["SODate"].ColumnName = "SO Date";
                    dataSet.Tables[1].Columns["CustPONumber"].ColumnName = "Customer PO No.";
                    dataSet.Tables[1].Columns["CustomerName"].ColumnName = "Customer";
                    dataSet.Tables[1].Columns["OBDNumber"].ColumnName = "OBD No.";
                    dataSet.Tables[1].Columns["ProjectRefNo"].ColumnName = "Project Ref. No.";
                    dataSet.Tables[1].Columns["MCode"].ColumnName = "SKU";
                    dataSet.Tables[1].Columns["Quantity"].ColumnName = "Picked Qty.";

                    dataSet.Tables[1].Columns["SO No."].SetOrdinal(0);
                    dataSet.Tables[1].Columns["SO Date"].SetOrdinal(1);
                    dataSet.Tables[1].Columns["Customer PO No."].SetOrdinal(2);
                    dataSet.Tables[1].Columns["Customer"].SetOrdinal(3);
                    dataSet.Tables[1].Columns["OBD No."].SetOrdinal(4);
                    dataSet.Tables[1].Columns["Project Ref. No."].SetOrdinal(5);
                    dataSet.Tables[1].Columns["SKU"].SetOrdinal(6);
                    dataSet.Tables[1].Columns["Picked Qty."].SetOrdinal(7);
                }

                response.Result = JsonConvert.SerializeObject(dataSet);
            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }

        // Bin Replenishment
        public async Task<Payload<string>> GetBinReplenishment(GetBinReplenishmentModel getBinReplenishment)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                    {"@MaterialMasterID",getBinReplenishment.MaterialMasterID },
                    {"@TenantID_New",getBinReplenishment.TenantID_New },
                    {"@WareHouseId",getBinReplenishment.WareHouseId },
                    {"@AccountID_New",getBinReplenishment.AccountID_New },

                 };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "sp_BinReplenishmentReport", sqlParams).ConfigureAwait(false);
            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }

        //Grn pending
        public async Task<Payload<string>> GetGRNPendingList(GetGRNPendingListModel getGRNPending)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    {"@MaterialMasterID",getGRNPending.MaterialMasterID },
                    {"@AccountID",getGRNPending.AccountID },
                    {"@TenantID",getGRNPending.TenantID },
                    {"@InboundID",getGRNPending.InboundID },
                    {"@PoHeaderID",getGRNPending.PoHeaderID },
                    {"@SupplierInvoiceID",getGRNPending.SupplierInvoiceID },
                    {"@Warehouseid",getGRNPending.Warehouseid },
                    {"@IsExport",getGRNPending.IsExport },
                    {"@NofRecordsPerPage",getGRNPending.NofRecordsPerPage },
                    {"@Rownumber",getGRNPending.Rownumber },
                    {"@UserID",getGRNPending.UserID }
                 };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "sp_RPT_GRNPendingList", sqlParams).ConfigureAwait(false);
            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }
        // Warehouse / Operator Activity Report
        public async Task<Payload<string>> GetWarehouseOperatorActivity(GetWarehouseOperatorActivityModel getWarehouseOperatorActivity)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                string fromDate = getWarehouseOperatorActivity.FromDate;
                string toDate = getWarehouseOperatorActivity.ToDate;
                string FromDate = "";
                string ToDate = "";
                DateTime fromDateValue;
                DateTime ToDateValue;
                if (!string.IsNullOrEmpty(fromDate) && DateTime.TryParse(fromDate, out fromDateValue))
                {
                    FromDate = "'" + fromDate + "'";
                }
                else
                {
                    FromDate = string.IsNullOrEmpty(getWarehouseOperatorActivity.FromDate) ? "null" : "" + getWarehouseOperatorActivity.FromDate + "";
                }
                if (!string.IsNullOrEmpty(toDate) && DateTime.TryParse(toDate, out ToDateValue))
                {

                    ToDate = "'" + toDate + "'";
                }
                else
                {
                    ToDate = string.IsNullOrEmpty(getWarehouseOperatorActivity.ToDate) ? "null" : "" + getWarehouseOperatorActivity.ToDate + "";
                }

                string sp = "EXEC [sp_RPT_GetWarehouseOperatorActivity] @AccountID_New = " + getWarehouseOperatorActivity.AccountID_New + ",@TenantID_New = " + getWarehouseOperatorActivity.TenantID_New
                + ",@WareHouseId = " + getWarehouseOperatorActivity.WareHouseId + ",@UserID_New = " + getWarehouseOperatorActivity.UserID_New
                + ",@FromDate = " + FromDate + ", @ToDate = " + ToDate + "";

                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDatasetINQ(this.ConnectionString, sp).ConfigureAwait(false);
            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }
        // Expiry Date Report
        public async Task<Payload<string>> GetExpiryDateTransferData(GetExpiryDateTransferDataModel getExpiryDateTransferData)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                string fromDate = getExpiryDateTransferData.Fromdate;
                string toDate = getExpiryDateTransferData.Todate;
                string FromDate = "";
                string ToDate = "";
                DateTime fromDateValue;
                DateTime ToDateValue;
                if (!string.IsNullOrEmpty(fromDate) && DateTime.TryParse(fromDate, out fromDateValue))
                {
                    FromDate = "'" + fromDate + "'";
                }
                else
                {
                    FromDate = string.IsNullOrEmpty(getExpiryDateTransferData.Fromdate) ? "null" : "" + getExpiryDateTransferData.Fromdate + "";
                }
                if (!string.IsNullOrEmpty(toDate) && DateTime.TryParse(toDate, out ToDateValue))
                {

                    ToDate = "'" + toDate + "'";
                }
                else
                {
                    ToDate = string.IsNullOrEmpty(getExpiryDateTransferData.Todate) ? "null" : "" + getExpiryDateTransferData.Todate + "";
                }
                StringBuilder sCmdPilotCr = new StringBuilder();
                string sp = "EXEC USP_GetExpiryDateTransferData @TenantID = " + getExpiryDateTransferData.TenantID + ",@MaterialID = " + getExpiryDateTransferData.MaterialID
                + ",@From = " + DBUtil.DBLibrary.SQuote(getExpiryDateTransferData.Fromdate) + ", @ToDate = " + DBUtil.DBLibrary.SQuote(getExpiryDateTransferData.Todate) + "";
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDatasetINQ(this.ConnectionString, sp).ConfigureAwait(false);
            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }

        public async Task<Payload<string>> UpdateExpiryDateTrasferLog(UpdateExpiryDateTrasferLogModel updateExpiryDateTrasferLog)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                string json = JsonConvert.SerializeObject(updateExpiryDateTrasferLog.transferRequestData);
                XmlDocument xml1 = JsonConvert.DeserializeXmlNode("{\"TransferRequestData\":" + json + "}", "ArrayOfTransferRequestData");
                var sqlParams = new Dictionary<string, object>
                {
                  {"@Xml", xml1.InnerXml }
                };

                var result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "SP_UpdateExpiryDateTrasferLog", sqlParams).ConfigureAwait(false);
                JObject data = JObject.Parse(result);
                JArray table = (JArray)data["Table"];
                int n = (int)table[0]["S"];
                if (n == 1)
                {
                    response.Result = "1"; //Successfully Cleared from Expiry Date Transfer data
                    return response;
                }
                else
                {
                    response.Result = "0"; //Error while clearing Expiry Date Transfer data
                    return response;
                }
            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }


        public async Task<Payload<string>> GetRefnoDropdown(GetIDocFailedListReportModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();

                string resQuery = "EXEC [SP_GetRefnoDropDown] @AccountID = " + items.AccountID + ",@IdocName='" + items.IdocName + "',@prefix='" + items.prefix + "'";

                var DS = DbUtility.GetDS(resQuery, ConnectionString);
                response.Result = JsonConvert.SerializeObject(DS);
            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }

        public async Task<Payload<string>> GetJSONAPITypes(GetIDocFailedListReportModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();

                string resQuery = "EXEC [SP_GetJSONAPITypes] @AccountID = " + items.AccountID + ",@prefix='" + items.prefix + "'";

                var DS = DbUtility.GetDS(resQuery, ConnectionString);
                response.Result = JsonConvert.SerializeObject(DS);
            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }



        //OperatorSummary report
#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<Payload<string>> GetOperatorSummaryReport(GetOperatorSummaryReportModel items)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            Payload<string> response = new Payload<string>();
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();

                string Query = " EXEC [sp_RPT_GetOperatorSummaryReport] @TenantID = " + items.TenantID + " , @WarehouseID = " + items.WarehouseId + " , @FromDate = " + "'" + items.FromDate + "'" + " , @ToDate = " + "'" + items.ToDate + "'" + " , @UserID = " + items.UserID + " , @OutboundID = " + items.OutboundID + " , @PageSize = " + items.PageSize + " , @PageIndex = " + items.PageIndex + " , @SOHeaderId = " + items.SOHeaderID + " , @LoadSheetID = " + items.LoadSheetID + "";

                var DS = DbUtility.GetDS(Query, ConnectionString);
                response.Result = JsonConvert.SerializeObject(DS);
            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }

        // Inventory / Expired Material Report
        public async Task<Payload<string>> GetExpiredMaterialReport(GetExpiredMaterialReportModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    { "@AccountID_New", items.AccountID },
                    { "@TenantID_New", items.TenantID },
                    { "@mmiD", items.MaterialMasterID },
                    { "@WareHouseId", items.WarehouseId },
                    { "@IsExport", items.IsExport },
                    { "@NoofRecords", items.NoofRecords },
                    { "@PageNo", items.PageNo }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "sp_RPT_GetExpiredMaterialList", sqlParams).ConfigureAwait(false);
            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }

        // Inventory /Returns / Supplier Returns Report
        public async Task<Payload<string>> GetSupplierReturnsReport(GetSupplierReturnsReportModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    { "@FromDate", String.IsNullOrEmpty(items.FromDate) ? (object)DBNull.Value : items.FromDate },
                    { "@ToDate", String.IsNullOrEmpty(items.ToDate) ? (object)DBNull.Value : items.ToDate },
                    { "@AccountID_New", items.AccountID },
                    { "@TenantID_New", items.TenantID },
                    { "@WareHouseId", items.WarehouseId },
                    { "@NofRecordsPerPage", items.NofRecordsPerPage },
                    { "@Rownumber", items.Rownumber }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "sp_RPT_GetSupplierReturns", sqlParams).ConfigureAwait(false);
            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }


        //Audit / Skip Log Report
#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<Payload<string>> GetSkipLogDataReport(GetSkipLogDataReportModel items)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            Payload<string> response = new Payload<string>();
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                string Query = "";
                if (items.Type == "obd")
                {
                    Query = "EXEC [OBD_RPT_SkipLog] @TenantID = " + items.TenantID + " , @MaterialID = " + items.MaterialMasterID + " , @From = " + DBLibrary.SQuote(items.FromDate) + " , @Todate = " + DBLibrary.SQuote(items.ToDate) + "";
                }
                else
                {
                    Query = "EXEC [INB_RPT_SkipLog] @TenantID = " + items.TenantID + " , @MaterialID = " + items.MaterialMasterID + " , @From = " + DBLibrary.SQuote(items.FromDate) + " , @Todate = " + DBLibrary.SQuote(items.ToDate) + "";
                }
                var DS = DbUtility.GetDS(Query, ConnectionString);
                response.Result = JsonConvert.SerializeObject(DS);
            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }

        // Cycle Count Report
        public async Task<Payload<string>> GetCycleCountReport(GetCycleCountReportModel items)
        {
            Payload<string> response = new Payload<string>();
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    {"@CCM_TRN_CycleCount_ID", items.CycleCount_ID },
                    {"@CCM_CNF_AccountCycleCount_ID", items.CCM_CNF_AccountCycleCount_ID },
                    {"@AccountID", items.AccountID },
                    {"@Rownumber", items.Rownumber },
                    {"@NofRecordsPerPage", items.NofRecordsPerPage },
                    {"@LocationID", items.LocationID },
                    {"@MaterialMasterID", items.MaterialMasterID },
                    {"@WarehouseID", items.WarehouseID },
                    {"@IsBincomplete", items.IsBincomplete },
                    {"@StartDate", items.FromDate }, 
                    {"@EndDate", items.ToDate },
                    {"@IsExcel", items.IsExcel }

                };

                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "sp_CCM_CycleCountReport_Audit", sqlParams).ConfigureAwait(false);
            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }

        // Audit / Table Audit Report
#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<Payload<string>> GetReferenceNumbers(GetReferenceNumbersModel items)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            Payload<string> response = new Payload<string>();
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                string Query = "";

                if (items.CategoryID == 1)  //Inbound      
                {
                    Query = " EXEC [SP_USP_FalconServices_GetInboundID] @prefix = " + "'" + items.prefix + "'" + "";
                }
                else if (items.CategoryID == 2) //Outbound
                {
                    Query = " EXEC [SP_USP_FalconServices_GetOutboundID] @prefix = " + "'" + items.prefix + "'" + "";
                }
                else if (items.CategoryID == 3) // Internal Trransfer
                {
                    Query = " EXEC [SP_USP_FalconServices_GetTransferRequestIDDeatils] @prefix = " + "'" + items.prefix + "'" + "";
                }
                else if (items.CategoryID == 4) //Cycle counts
                {
                    Query = " EXEC [SP_USP_FalconServices_GetCCM_CNF_AccountCycleCount_ID] @prefix = " + "'" + items.prefix + "'" + "";
                }

                var DS = DbUtility.GetDS(Query, ConnectionString);
                response.Result = JsonConvert.SerializeObject(DS);
            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }

        // Audit/Table Audit Report
        public async Task<Payload<string>> GetTableAuditReport(GetTableAuditReportModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                      {"@OrderTypeID",items.CategoryID},
                      {"@OrderID",items.ReferenceId}
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "USP_HIST_FetchAuditTrail", sqlParams).ConfigureAwait(false);
            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }

        //IDOC Failed Status Report
        public async Task<Payload<string>> GetIDocFailedListReport(GetIDocFailedListReportModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();

                string date = string.IsNullOrEmpty(items.date) ? "null" : "'" + Convert.ToDateTime(items.date) + "'";
                string resQuery = "EXEC [sp_IDocFailedList] @DocumentType = " + DBLibrary.SQuote(items.DocumentType) +
                                   " , @Referencenumber = " + DBLibrary.SQuote(items.Referencenumber) +
                                   " , @DATE = " + date;

                var DS = DbUtility.GetDS(resQuery, ConnectionString);
                response.Result = JsonConvert.SerializeObject(DS);
            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }
        public async Task<Payload<string>> ClearAuditSkipLog(ClearAuditSkipLogModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                string json = JsonConvert.SerializeObject(items.items);
                XmlDocument xmlData = JsonConvert.DeserializeXmlNode("{\"SkipClearData\":" + json + "}", "ArrayOfSkipClearData");
                var sqlParams = new Dictionary<string, object>
                {
                    { "@Type" , items.Type },
                    { "@XML" , "'"+xmlData.InnerXml+"'" }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                var result = await DbUtility.GetjsonData(this.ConnectionString, "SP_UpdateSkipItemLog", sqlParams).ConfigureAwait(false);
                JArray jArray = JArray.Parse(result);
                int n = (int)jArray[0]["S"];
                if (n == 1)
                {
                    response.Result = "1"; //Successfully Cleared from Skip Log
                }
                else
                {
                    response.Result = "0"; //Error while clearing Skip Log
                }
            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }

        public async Task<Payload<string>> GetInboundDetailsReport(GetInboundDetailsReportModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                string fromDate = items.FromDate;
                string toDate = items.ToDate;
                string FromDate = "";
                string ToDate = "";
                DateTime fromDateValue;
                DateTime ToDateValue;
                if (!string.IsNullOrEmpty(fromDate) && DateTime.TryParse(fromDate, out fromDateValue))
                {
                    FromDate = "'" + fromDate + "'";
                }
                else
                {
                    FromDate = string.IsNullOrEmpty(items.FromDate) ? "null" : "" + items.FromDate + "";
                }
                if (!string.IsNullOrEmpty(toDate) && DateTime.TryParse(toDate, out ToDateValue))
                {

                    ToDate = "'" + toDate + "'";
                }
                else
                {
                    ToDate = string.IsNullOrEmpty(items.ToDate) ? "null" : "" + items.ToDate + "";
                }
                string Query = "Exec[dbo].[SP_RPT_InboundDetailsReport] @InboundId = " + items.InboundId + ",@MaterialMasterID = " + items.MaterialMasterID + ",@tenantid = " + items.tenantid + ",@Warehouseid = " + items.Warehouseid + ",@FromDate = " + FromDate + ",@ToDate = " + ToDate + ",@PageSize = " + items.PageSize + ",@PageIndex = " + items.PageIndex + ",@IsForExcel = " + items.IsForExcel + ",@InboundStatusID="+items.InboundStatusID+"";
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDatasetINQ(this.ConnectionString, Query);
            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }

        public async Task<Payload<string>> GetOutboundDetailsReport(GetOutboundDetailsReportModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    {"@TenantID",items.TenantID },
                    {"@WarehouseID",items.WarehouseID },
                    {"@StartDate",items.FromDate},
                    {"@EndDate",items.ToDate},
                    {"@CustomerID",items.CustomerID },
                    {"@OutboundID",items.OutboundID },
                    {"@PageSize",items.PageSize },
                    {"@PageIndex",items.PageIndex },
                    {"@SOHeaderId",items.SOHeaderId },
                    {"@LoadSheetID",items.LoadSheetID },
                    {"@SOTypeID",items.OrderTypeID },
                    {"@IsExcel",items.IsExcel },
                    { "@VehicleNumber",items.VehicleNumber},
                    { "@VLPDNumber",items.VLPDNumber},
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "SP_GET_OutboundDetailsReport", sqlParams).ConfigureAwait(false);
            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }

        public async Task<Payload<string>> GetWareHouse_StockInformation_Report(GetWareHouse_StockInformation_ReportModel items)
        {
            Payload<string> response = new Payload<string>();
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                //string toDate = string.IsNullOrEmpty(items.ToDate) ? "NULL" : "'" + items.ToDate + "'";

                StringBuilder sp = new StringBuilder();
                sp.AppendLine("EXEC [SP_RPT_WarehouseStockInformation] @MaterialMasterID = " + items.MaterialMasterID + " , @TenantID = " + items.TenantID + " , @WarehouseID = " + items.WarehouseID + " , @IsExcel = " + items.IsExcel + " , @RowNumber = " + items.PageIndex + " , @NofRecordsPerPage = " + items.PageSize + ";"
                + "EXEC [sp_RPT_GetTenantDetails] @TenantID =" + items.@TenantID + "");
                string output = sp.ToString();

                var jsonData = await DbUtility.GetjsonDataFromDatasetINQ(this.ConnectionString, output);
                DataSet dataSet = JsonConvert.DeserializeObject<DataSet>(jsonData);

                response.Result = JsonConvert.SerializeObject(dataSet);
            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }


        public async Task<Payload<string>> GetTotalIdocReport()
        {
            Payload<string> response = new Payload<string>();
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();

                //string date = string.IsNullOrEmpty(items.date) ? "null" : "'" + Convert.ToDateTime(items.date) + "'";
                string resQuery = "EXEC [USP_GETIDOC_List] ";

                var DS = DbUtility.GetDS(resQuery, ConnectionString);
                response.Result = JsonConvert.SerializeObject(DS);
            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }

        public async Task<Payload<string>> ActiveFailedIdoc(GetIDocFailedListReportModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                string resQuery = "EXEC [SP_ActiveFailedIdoc] @ID =" + items.Id+",@RefNumber="+ DBUtil.DBLibrary.SQuote (items.Referencenumber);
                int N = DbUtility.GetSqlN(resQuery, ConnectionString);
                response.Result = JsonConvert.SerializeObject(N);
            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }
        public async Task<Payload<string>> GetVehicleManagementData(DashBoardInputModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                string resQuery = "EXEC [USP_Vehicle_Management_Details] @WareHouseId =" + obj.WareHouseid + ",@LoginAccountId=" + obj.LoginAccountId + ",@LoginUserId=" + obj.LoginUserId + ",@LoginTenantId=" + obj.LoginTanentId + ",@DockId="+obj.DockId+",@VehicleNo='"+obj.VehicleNo+"',@LoadingTypeID="+obj.LoadingPointID+"";
                var ds = DbUtility.GetDS(resQuery, ConnectionString);
                response.Result = JsonConvert.SerializeObject(ds);
            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch(Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }
        public async Task<Payload<string>> LoadingScanReportData(DashBoardInputModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                string fromDate;
                if (string.IsNullOrEmpty(obj.fromdate))
                {
                    fromDate = "NULL";
                }
                else
                {
                    fromDate = $"'{obj.fromdate}'";
                }

                string toDate;
                if (string.IsNullOrEmpty(obj.todate))
                {
                    toDate = "NULL";
                }
                else
                {
                    toDate = $"'{obj.todate}'";
                }

                string resQuery = "EXEC [SP_RPT_LoadingScanReport] " +
                                  "@TenantId = " + obj.tenantID + ", " +
                                  "@WarehouseId = " + obj.WareHouseid + ", " +
                                  "@FromDate = " + fromDate + ", " +
                                  "@ToDate = " + toDate + "," +
                                  "@IsExcel = " + obj.IsExcel + "," +
                                  "@RowNumber = " + obj.PageIndex + "," +
                                  "@NofRecordsPerPage = " + obj.PageSize;
                var ds = DbUtility.GetDS(resQuery, ConnectionString);
                response.Result = JsonConvert.SerializeObject(ds);
            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }
        public async Task<Payload<string>> VehicleEntryAndExitReport(DashBoardInputModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
               
                string resQuery = "EXEC [SP_RPT_VehicleEntryAndExitReport] " +
                                  "@WarehouseId = " + obj.WareHouseid + ", " +
                                  "@DockId = " + obj.DockId + ", " +
                                  "@VehicleNo = '" + obj.VehicleNo + "'," +
                                  "@IsExcel = " + obj.IsExcel + "," +
                                  "@RowNumber = " + obj.PageIndex + "," +
                                  "@NofRecordsPerPage = " + obj.PageSize;
                var ds = DbUtility.GetDS(resQuery, ConnectionString);
                response.Result = JsonConvert.SerializeObject(ds);
            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }
        public async Task<Payload<string>> StockAdjustmentReport(DashBoardInputModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                string fromDate;
                if (string.IsNullOrEmpty(obj.fromdate))
                {
                    fromDate = "NULL";
                }
                else
                {
                    fromDate = $"'{obj.fromdate}'";
                }

                string toDate;
                if (string.IsNullOrEmpty(obj.todate))
                {
                    toDate = "NULL";
                }
                else
                {
                    toDate = $"'{obj.todate}'";
                }
                string resQuery = "EXEC [SP_RPT_StockAdjustmentReport] " +
                                  "@WarehouseId = " + obj.WareHouseid + ", " +
                                  "@TenantId = " + obj.tenantID + ", " +
                                  "@FromDate = " + fromDate + "," +
                                  "@ToDate = " + toDate + "," +
                                  "@IsExcel = " + obj.IsExcel + "," +
                                  "@RowNumber = " + obj.PageIndex + "," +
                                  "@NofRecordsPerPage = " + obj.PageSize;
                var ds = DbUtility.GetDS(resQuery, ConnectionString);
                response.Result = JsonConvert.SerializeObject(ds);
            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }
        public async Task<Payload<string>> PalletConsolidationReport(DashBoardInputModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                string fromDate;
                if (string.IsNullOrEmpty(obj.fromdate))
                {
                    fromDate = "NULL";
                }
                else
                {
                    fromDate = $"'{obj.fromdate}'";
                }

                string toDate;
                if (string.IsNullOrEmpty(obj.todate))
                {
                    toDate = "NULL";
                }
                else
                {
                    toDate = $"'{obj.todate}'";
                }

                string resQuery = "EXEC [SP_RPT_PalletConsolidationReport] " +
                                  "@WarehouseId = " + obj.WareHouseid + ", " +
                                  "@TenantId = " + obj.tenantID + ", " +
                                  "@FromDate = " + fromDate + "," +
                                  "@ToDate = " + toDate + "," +
                                  "@MaterialMasterId = " + obj.MMID + "," +
                                  "@LocationId = " + obj.LocationId + "," +
                                  "@PalletId = " + obj.CartonId + "," +
                                  "@IsExcel = " + obj.IsExcel + "," +
                                  "@RowNumber = " + obj.PageIndex + "," +
                                  "@NofRecordsPerPage = " + obj.PageSize;
                var ds = DbUtility.GetDS(resQuery, ConnectionString);
                response.Result = JsonConvert.SerializeObject(ds);
            }
            catch(SqlException sql)
            {
                response.addError(sql.Message);
            }
            catch (Exception ex)
            {
               response.addError(ex.Message);
            }
            return response;
        }
        public async Task<Payload<string>> PalletUtilizationReport(DashBoardInputModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                string resQuery = "EXEC [SP_RPT_PalletUtilizationReport] " +
                                  "@WarehouseId = " + obj.WareHouseid + ", " +
                                  "@TenantId = " + obj.tenantID + ", " +
                                  "@MaterialMasterId = " + obj.MMID + "," +
                                  "@LocationId = " + obj.LocationId + "," +
                                  "@CartonId = " + obj.CartonId + ", " +
                                  "@IsExcel = " + obj.IsExcel + "," +
                                  "@RowNumber = " + obj.PageIndex + "," +
                                  "@NofRecordsPerPage = " + obj.PageSize;
                var ds = DbUtility.GetDS(resQuery, ConnectionString);
                response.Result = JsonConvert.SerializeObject(ds);
            }
            catch(SqlException sql)
            {
                response.addError(sql.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }

        public async Task<Payload<string>> GetLogAuditData(GetLogAuditDataModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                string fromDate;
                if (string.IsNullOrEmpty(obj.FromDate))
                {
                    fromDate = "NULL";
                }
                else
                {
                    fromDate = $"'{obj.FromDate}'";
                }

                string toDate;
                if (string.IsNullOrEmpty(obj.ToDate))
                {
                    toDate = "NULL";
                }
                else
                {
                    toDate = $"'{obj.ToDate}'";
                }
                string resQuery = "EXEC [dbo].[Sp_GetUser_AuditLogDetails] @UserId =" + obj.UserId + ",@FromDate="+ fromDate + ",@ToDate="+toDate+ ",@IPAddress="+DBLibrary.SQuote(obj.IPAddress)+"";
                var ds = DbUtility.GetDS(resQuery, ConnectionString);
                response.Result = JsonConvert.SerializeObject(ds);
            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }

        public async Task<Payload<string>> ClearLogAuditData(ClearLogAuditDataModel obj)
        {
            Payload<string> response = new Payload<string>();
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                string Query = "Exec [dbo].[Sp_ClearLogAuditData] @UserId =" + obj.UserId + ",@LoginUserId="+obj.LoginUserID+ ",@AccountId="+obj.AccountID_New+ ",@IPAddress="+DBLibrary.SQuote(obj.IPAddress)+"";
                DataSet dsResult = DbUtility.GetDS(Query, this.ConnectionString);
                response.Result = JsonConvert.SerializeObject(dsResult);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }
    }
}
