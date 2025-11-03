using Simpolo_Endpoint.DAO.interfaces;
using Simpolo_Endpoint.DBUtil;
using Simpolo_Endpoint.Models;
using Simpolo_Endpoint.ModelsLibrary;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols;
using Nancy.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Simpolo_Endpoint.DAO.Services
{
    public class InboundService : AppDBService, IInbound
    {
            
        private readonly ISAPJsonPostService _jsonPostService;
        public InboundService(IOptions<AppSettings> appSettings, ISAPJsonPostService jsonPostService) : base(appSettings)
        {
            _jsonPostService = jsonPostService;
        }
        public async Task<Payload<string>> GetInboundDetails(GetInboundDetailsModel items)
        {
            Payload<string> response = new Payload<string>();
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                string DiscrepancyQuery = "EXEC [dbo].[INB_ASN_IMPORT_DATA] @InboundID = " + items.InboundID + "";
                var DS = DbUtility.GetDS(DiscrepancyQuery, this.ConnectionString);
               
                var sqlParams = new Dictionary<string, object>
                {
                    { "@InboundID", items.InboundID },
                    { "@AccountID_New", items.AccountID },
                    { "@TenantID_New", items.TenantID }
                };

                if (DS.Tables[0].Rows.Count == 0)
                {
                    response.ResponseCode = "0";
                }
                else
                {
                    response.ResponseCode = "1";
                }
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "sp_INB_GetInBoundReceivedDetails", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetInboundTracking_ShipmentTransit(InboundTracking_ShipmentTransitModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    { "@WarehouseIDs", items.WarehouseId },
                    { "@StoreRefNo", items.StoreRefNo },
                    { "@AccountID_New", items.AccountID },
                    { "@TenantID", items.TenantID },
                    { "@Tenant", items.Tenant }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "sp_INB_GetSITList", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetInboundTracking_ShipmentExpected(InboundTracking_ShipmentExpectedModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    { "@WarehouseIDs", items.WarehouseId },
                    { "@StoreRefNo", items.StoreRefNo },
                    { "@AccountID_New", items.AccountID },
                    { "@TenantID", items.TenantID },
                    { "@Tenant", items.Tenant }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "sp_INB_GetShipmentExpectedList", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetInboundTracking_ShipmentInProcess(InboundTracking_ShipmentInProcessModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    { "@WarehouseIDs", items.WarehouseId },
                    { "@StoreRefNo", items.StoreRefNo },
                    { "@AccountID_New", items.AccountID },
                    { "@TenantID", items.TenantID },
                    { "@Tenant", items.Tenant }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "sp_INB_GetSIPList", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetRevertInboundList(GetRevertInboundListModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    { "@StoreRefNo", items.StoreRefNo },
                    { "@AccountID_New", items.AccountID },
                    { "@TenantID", items.TenantID },
                    { "@UserID_New", items.UserID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "SP_INB_RevertInbound", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> UpsertInboundBasicData(UpsertInboundBasicDataModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                StringBuilder sCmdUpsertInboundBasicData = new StringBuilder();
                sCmdUpsertInboundBasicData.AppendLine("DECLARE @NewUpdateInboundID nvarchar(50);  ");
                sCmdUpsertInboundBasicData.AppendLine("EXEC [dbo].[sp_INB_UpsertInbound]    ");
                sCmdUpsertInboundBasicData.AppendLine("@StoreRefNo = " + items.StoreRefNo + ",");
                sCmdUpsertInboundBasicData.AppendLine("@DocReceivedDate = " + "'" + items.DocReceivedDate + "'" + ",");
                sCmdUpsertInboundBasicData.AppendLine("@ShipmentTypeID =" + items.ShipmentTypeID + ",");
                sCmdUpsertInboundBasicData.AppendLine("@IsChargesRequired =" + items.IsChargesRequired + ",");
                sCmdUpsertInboundBasicData.AppendLine("@SupplierID =" + items.SupplierID + ",");
                sCmdUpsertInboundBasicData.AppendLine("@ConsignmentNoteTypeID =" + (String.IsNullOrEmpty(items.ConsignmentNoteTypeID) ? "null" : items.ConsignmentNoteTypeID) + ",");
                sCmdUpsertInboundBasicData.AppendLine("@ConsignmentNoteTypeValue =" + (String.IsNullOrEmpty(items.ConsignmentNoteTypeValue) ? "null" : items.ConsignmentNoteTypeValue) + ",");
                sCmdUpsertInboundBasicData.AppendLine("@ConsignmentNoteTypeDate =" + (String.IsNullOrEmpty(items.ConsignmentNoteTypeDate) ? "null" : items.ConsignmentNoteTypeDate) + ",");
                sCmdUpsertInboundBasicData.AppendLine("@NoofPackagesInDocument =" + items.NoofPackagesInDocument + ",");
                sCmdUpsertInboundBasicData.AppendLine("@GrossWeight =" + items.GrossWeight + ",");
                sCmdUpsertInboundBasicData.AppendLine("@CBM =" + items.CBM + ",");
                sCmdUpsertInboundBasicData.AppendLine("@ClearanceCompanyID =" + (String.IsNullOrEmpty(items.ClearanceCompanyID) ? "null" : items.ClearanceCompanyID) + ",");
                sCmdUpsertInboundBasicData.AppendLine("@ClearanceInvoiceNo =" + (String.IsNullOrEmpty(items.ClearanceInvoiceNo) ? "null" : items.ClearanceInvoiceNo) + ",");
                sCmdUpsertInboundBasicData.AppendLine("@ClearanceInvoiceDate =" + (String.IsNullOrEmpty(items.ClearanceInvoiceDate) ? "null" : items.ClearanceInvoiceDate) + ",");
                sCmdUpsertInboundBasicData.AppendLine("@ClearanceAmount =" + items.ClearanceAmount + ",");
                sCmdUpsertInboundBasicData.AppendLine("@FreightCompanyID =" + (String.IsNullOrEmpty(items.FreightCompanyID) ? "null" : items.FreightCompanyID) + ",");
                sCmdUpsertInboundBasicData.AppendLine("@FreightInvoiceNo =" + (String.IsNullOrEmpty(items.FreightInvoiceNo) ? "null" : items.FreightInvoiceNo) + ",");
                sCmdUpsertInboundBasicData.AppendLine("@FreightInvoiceDate =" + (String.IsNullOrEmpty(items.FreightInvoiceDate) ? "null" : items.FreightInvoiceDate) + ",");
                sCmdUpsertInboundBasicData.AppendLine("@FreightAmount =" + items.FreightAmount + ",");
                sCmdUpsertInboundBasicData.AppendLine("@PriorityLevel =" + (String.IsNullOrEmpty(items.PriorityLevel) ? "null" : items.PriorityLevel) + ",");
                sCmdUpsertInboundBasicData.AppendLine("@PriorityDateTime =" + "null" + ",");
                sCmdUpsertInboundBasicData.AppendLine("@RemarksBy_Ini =" + (String.IsNullOrEmpty(items.RemarksBy_Ini) ? "null" : items.RemarksBy_Ini) + ",");
                sCmdUpsertInboundBasicData.AppendLine("@CreatedBy =" + items.CreatedBy + ",");
                sCmdUpsertInboundBasicData.AppendLine("@UpdatedOn =" + DBLibrary.SQuote(DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss")) + ",");
                sCmdUpsertInboundBasicData.AppendLine("@UpdatedBy =" + items.UpdatedBy + ",");
                sCmdUpsertInboundBasicData.AppendLine("@AccountID =" + items.AccountID + ",");
                sCmdUpsertInboundBasicData.AppendLine("@TenantID =" + items.TenantID + ",");
                sCmdUpsertInboundBasicData.AppendLine("@WarehouseIDs =" + items.WarehouseIDs + ",");
                sCmdUpsertInboundBasicData.AppendLine("@InboundID =" + items.InboundID + ",");
                sCmdUpsertInboundBasicData.AppendLine("@InboundStatusID =" + 1 + ",");
                sCmdUpsertInboundBasicData.AppendLine("@NewInboundID=@NewUpdateInboundID OUTPUT select @NewUpdateInboundID AS S");
                response.Result = DbUtility.GetSqlS(sCmdUpsertInboundBasicData.ToString(), ConnectionString);
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

        public async Task<Payload<string>> GetInBoundPOInvoiceDetails(GetInBoundPOInvoiceDetailsModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    { "@TenantID", items.TenantID },
                    { "@InboundID", items.InboundID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "sp_ORD_GetPOInvoiceDetails", sqlParams).ConfigureAwait(false);
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


        public async Task<Payload<string>> AddOrderOrInvoiceItems(AddOrderOrInvoiceItemsModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                int InboundStatusID = 0;
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                string sp = "select InboundStatusID AS N from INB_Inbound where IsActive=1 and IsDeleted=0  and InboundID=@InboundID";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sp, connection))
                    {
                        command.Parameters.AddWithValue("@InboundID", items.InboundID);
                         InboundStatusID = (int)command.ExecuteScalar();                 
                    }
                }
                if (InboundStatusID == 3)
                {
                    response.Result = "-1"; //Cannot change the shipment details, as shipment is received
                    return response;
                }
                if (InboundStatusID == 2)
                {
                    response.Result = "-2"; //Cannot change the shipment details, as shipment is Expected
                    return response;
                }
                if (InboundStatusID == 6)
                {
                    response.Result = "-3"; //Cannot change the shipment details, as GRN Updated
                    return response;
                }
                if (InboundStatusID == 4)
                {
                    response.Result = "-4"; //Cannot change the shipment details, as shipment is Verified
                    return response;
                }


                string Query = "EXEC dbo.[USP_INB_INL_GetinvoiceInboundTrack] @InboundID= " + items.InboundID + "";
                int InboundTracking_WarehouseID = DbUtility.GetSqlN(Query, ConnectionString);

                if (InboundTracking_WarehouseID != 0)
                {
                    response.Result = "-5"; //Cannot change the shipment details, as shipment is received
                    return response;
                }

                string ResultQuery = "EXEC dbo.[USP_INB_INL_GetinvoiceSupplier] @SupplierID =  " + items.SupplierID + " , @TenantID = " + items.TenantID + "";
                int POHeaderID = DbUtility.GetSqlN(ResultQuery, ConnectionString);

                if (POHeaderID == 0)
                {
                    response.Result = "-6"; //No PO's are configured to this supplier
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




        public async Task<Payload<string>> UpsertInBoundPOInvoiceDetails(UpsertInBoundPOInvoiceDetailsModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                StringBuilder sCmdPOInvDetails = new StringBuilder();
                sCmdPOInvDetails.AppendLine("DECLARE @NewUpdateInboundPOInvoiceID int;");
                sCmdPOInvDetails.AppendLine("EXEC [dbo].[sp_INB_UpsertPOInvoiceDetails]");
                sCmdPOInvDetails.AppendLine("@Inbound_SupplierInvoiceID = " + items.Inbound_SupplierInvoiceID + ",");
                sCmdPOInvDetails.AppendLine("@POHeaderID = " + items.POHeaderID + ",");
                sCmdPOInvDetails.AppendLine("@InboundID =" + items.InboundID + ",");
                sCmdPOInvDetails.AppendLine("@SupplierInvoiceID =" + items.SupplierInvoiceID + ",");
                sCmdPOInvDetails.AppendLine("@CreatedBy =" + items.UserID + ",");
                sCmdPOInvDetails.AppendLine("@UpdatedOn = " + DBLibrary.SQuote(DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss")) + ",");
                sCmdPOInvDetails.AppendLine("@NewInbound_SupplierInvoiceID=@NewUpdateInboundPOInvoiceID OUTPUT; ");
                sCmdPOInvDetails.AppendLine("select @NewUpdateInboundPOInvoiceID AS N");

                int Inbound_SupplierInvoiceID = DbUtility.GetSqlN(sCmdPOInvDetails.ToString(), ConnectionString);
                if (Inbound_SupplierInvoiceID != 0)
                {
                    response.Result = Inbound_SupplierInvoiceID.ToString();//Successfully Updated
                }
                else
                {
                    response.Result = "-2"; //Error while updating PO/Invoice details
                }
            }
            catch (SqlException Sqlex)
            {
                if (Sqlex.Message.StartsWith("Cannot insert duplicate key in object 'dbo.INB_Inbound_ORD_SupplierInvoice'"))
                {
                    response.Result = ("-1");   //This invoice is already updated
                }
                else
                {
                    response.Result = ("-2"); //Error while updating PO/Invoice details
                }
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }

        public async Task<Payload<string>> GetASNDetails(GetASNDetailsModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    { "@InboundID", items.InboundID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "INB_ASN_IMPORT_DATA", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetSearchInboundDetails(GetSearchInboundDetailsModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                string StartDate = items.StartDate;
                string EndDate = items.EndDate;
              
                DateTime fromDateValue;
                DateTime ToDateValue;
                if (!string.IsNullOrEmpty(StartDate) && DateTime.TryParseExact(StartDate, "MM-dd-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out fromDateValue))
                {
                    StartDate = "'" + fromDateValue.ToString("MM-dd-yyyy") + "'";
                }
                else
                {
                    StartDate = string.IsNullOrEmpty(items.StartDate) ? "null" : "'" + items.StartDate + "'";
                }

                if (!string.IsNullOrEmpty(EndDate) && DateTime.TryParseExact(EndDate, "MM-dd-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out ToDateValue))
                {
                    EndDate = "'" + ToDateValue.ToString("MM-dd-yyyy") + "'";
                }
                else
                {
                    EndDate = string.IsNullOrEmpty(items.EndDate) ? "null" : "'" + items.EndDate + "'";
                }
                string Query = "Exec[dbo].[sp_INB_SearchInbound_New] @TenantID = " + items.TenantID + ",@UserID = " + items.UserID
                + ",@AccountID_New = " + items.AccountID + ",@StoreID = " + items.WarehouseId + ",@ShipmentTypeID = " + items.ShipmentTypeID + ",@ShipmentStatusID = " + items.ShipmentStatusID
                + ",@ClearenceCompanyID = " + items.ClearenceCompanyID + ",@PageIndex = " + items.PageIndex
                + ",@PageSize = " + items.PageSize + ",@WarehouseID = " + items.WarehouseId + ",@SearchText = " +DBLibrary.SQuote(items.SearchText)
                + ",@SearchField = " + items.SearchField + ",@Qty = " + items.Quantity + ",@GrossWeight = " + items.GrossWeight
                + ",@StartDate=" + StartDate + ",@EndDate=" + EndDate +",@OrderTypeID = "+ items.OrderTypeID +"";
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


        public async Task<Payload<string>> GetRTRDetails(GetRTRDetailsModel items)
        {
            Payload<string> response = new Payload<string>();
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                int InboundStatusID = 0;
                string sp = "select InboundStatusID AS N from INB_Inbound where IsActive=1 and IsDeleted=0 and InboundID=@InboundID";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(sp, connection))
                    {
                        command.Parameters.AddWithValue("@InboundID", items.InboundID);
                        connection.Open();
                        InboundStatusID = (int)command.ExecuteScalar();
                    }
                }
                if (InboundStatusID < 3)
                {
                    response.ResponseCode = "-1"; //Disable receive button
                    return response;
                }

                var sqlParams = new Dictionary<string, object>
                {
                    { "@MCode", items.MCode },
                    { "@InboundID", items.InboundID }
                };
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "sp_INB_ReceivingTallyReport", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetGoodsInSuggestedPutAwayList(GetGoodsInSuggestedPutAwayListModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                      {"@InboundID",items.InboundID},
                      {"@POHeaderID",items.POHeaderID},
                      {"@LineNumber",items.LineNumber},
                      {"@MaterialMasterID",items.MaterialMasterID},
                      {"@SupplierInvoiceID",items.SupplierInvoiceID},
                      {"@SupplierInvoiceDetailsID",items.SupplierInvoiceDetailsID},
                      {"@AccountID",items.AccountID}
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "GET_SUGGESTEDPUTAWAYLIST", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> Get_ReceiveMSPsPutawayList(Get_ReceiveMSPsPutawayListModel items)
        {
            Payload<string> response = new Payload<string>();
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                string Query = "EXEC [dbo].[sp_INV_GET_GoodsIn_List] @InboundID = " + items.InboundID + ",@POHeaderID = " + items.POHeaderID + ",@LineNumber=" + items.LineNumber + ",@MaterialMasterID = " + items.MaterialMasterID + ",@SupplierInvoiceID = " + items.SupplierInvoiceID + ",@SupplierInvocieDetailsID = " + items.SupplierInvoiceDetailsID + "";
                var DS = DbUtility.GetDS(Query, this.ConnectionString);

                DataColumn rowNumberColumn = new DataColumn("RowNumber", typeof(int));
                DS.Tables[0].Columns.Add(rowNumberColumn);

                int rowCount = 1;
                foreach (DataRow row in DS.Tables[0].Rows)
                {
                    row["RowNumber"] = rowCount;
                    rowCount++;
                }

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

        public async Task<Payload<string>> ReceiveMSPsPutawayList(ReceiveMSPsPutawayListModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                string[] MCode = items.MCode.Split('_');
                string[] Location = items.Location.Split('_');

                if (items.SerialNo != "")
                {
                    string Query = "EXEC dbo.[sp_GET_SERIAL_COUNT]  @SerialNo = " + DBLibrary.SQuote(items.SerialNo) + " , @MCode = " + DBLibrary.SQuote(items.MCode) + "";
                    int SerialCount = DbUtility.GetSqlN(Query, ConnectionString);

                    if (SerialCount != 0)
                    {
                        response.Result = "-1"; //"Serial no is already received";
                        return response;
                    }
                }

                        string SQL = "Exec sp_INV_ReceiveItem @TransactionID="+ items.TransactionID
                         + ",@LineNumber = " + items.LineNumber + " , @POHeaderID = " + items.POHeaderID
                         + " , @Quantity = " + (items.Quantity) + " , @DocQty = " + items.DocQty
                         + ",@ProjectRefNo = " + (string.IsNullOrEmpty(items.ProjectRefNo) ? "''" : DBLibrary.SQuote(items.ProjectRefNo))
                         + ",@ExpDate = " + (string.IsNullOrEmpty(items.ExpDate) ? "''" : DBLibrary.SQuote(items.ExpDate))
                         + ",@MfgDate = " + (string.IsNullOrEmpty(items.MfgDate) ? "''" : DBLibrary.SQuote(items.MfgDate))
                         + " , @SerialNo = " + (string.IsNullOrEmpty(items.SerialNo) ? "''" : "'" + items.SerialNo + "'")
                         + " , @BatchNo = " + (string.IsNullOrEmpty(items.BatchNo) ? "''" : "'" + items.BatchNo + "'")
                         + " , @StorageLocation = " + items.StorageLocation
                         + " , @Loction = " + DBLibrary.SQuote(items.Location) + " , @CartonCode = " + DBLibrary.SQuote(items.CartonCode)
                         + ",@GoodsMovementTypeID = " + items.GoodsMovementTypeID + " , @CreatedBy = " + items.UserID
                         + " , @SupplierInvoiceID = " + items.SupplierInvoiceID + " , @HUNo = " + 1
                         + " , @IsRequestFromPC = " + items.IsRequestFromPC + " , @MRP = " + items.MRP  +" , @Mcode = " + DBLibrary.SQuote(MCode[0]) + ""
                         +" , @SuggestedPutawayID = " + items.SuggestedPutawayID;
                var result = await DbUtility.GetjsonDataFromDatasetINQ(this.ConnectionString, SQL);
                using (JsonDocument doc = JsonDocument.Parse(result))
                {
                   
                    if (doc.RootElement.TryGetProperty("Table", out JsonElement table) && table.ValueKind == JsonValueKind.Array && table.GetArrayLength() > 0)
                    {
                       
                        JsonElement element = table[0];
                        if (element.TryGetProperty("ErrorCode", out JsonElement errorCode) && errorCode.GetString() == "Err")
                        {
                           
                            if (element.TryGetProperty("ErrorMessage", out JsonElement errorMessage))
                            {
                                string error = errorMessage.ToString();
                                response.Result = error;
                                return response;
                            }
                        }
                        else
                        {
                          
                            if (element.TryGetProperty("N", out JsonElement n))
                            {
                                response.Result = "1";
                            }
                        }
                    }
                   
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



        public async Task<Payload<string>> DeleteGoodsInRecieveddetails(DeleteGoodsInRecieveddetailsModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                string json = JsonConvert.SerializeObject(items.items);
                XmlDocument xmlData = JsonConvert.DeserializeXmlNode("{\"QCPending\":" + json + "}", "ArrayOfQCPending");
                var sqlParams = new Dictionary<string, object>
                {
                    { "@CreatedBy" , items.UserID },
                    { "@GoodsData" , xmlData.InnerXml }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "sp_GMD_Revert_Receiving", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> UpdateASNDetails(UpdateASNDetailsModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                      {"@SupplierInvoiceDetailsID",items.SupplierInvoiceDetailsID},
                      {"@InvoiceQty",items.InvoiceQuantity},
                      {"@StorageLocation",items.StorageLocationID},
                      {"@UpdatedBy",items.UserID}
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                var result = await DbUtility.GetjsonData(this.ConnectionString, "USP_ORD_Update__LineitemWisePODetails", sqlParams).ConfigureAwait(false);
                JArray jArray = JArray.Parse(result);
                int n = (int)jArray[0]["N"];
                if (n == 1)
                {
                    response.Result = "1";//Successfully updated
                }
                else if (n == -1)
                {
                    response.Result = "-1";//Shipment is already Received
                }
                else
                {
                    response.Result = "-2";//Error while updating projected vehicle details
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

        public async Task<Payload<string>> UpdateShipmentExpectedDetails(UpdateShipmentExpectedDetailsModel items)
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            Payload<string> response = new Payload<string>();
            try
            {
                int InboundStatusID = 0;
                string sp = "select InboundStatusID AS N from INB_Inbound where IsActive=1 and IsDeleted=0 and InboundID=@InboundID";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(sp, connection))
                    {
                        command.Parameters.AddWithValue("@InboundID", items.InboundID);
                        connection.Open();
                        InboundStatusID = (int)command.ExecuteScalar();                       
                    }
                }

                if (InboundStatusID >= 3)
                {
                    response.Result = "-1"; //Cannot change the expected date, as shipment is received
                    return response;
                }

                string Query = "EXEC dbo.[USP_INB_INL_GetShipmentTop1] @InboundID = " + items.InboundID + "";
                int result = DbUtility.GetSqlN(Query, ConnectionString);
                if (result == 0)
                {
                    response.Result = "-2"; //Add atleast one PO/Invoice line item
                    return response;
                }

                string SP = "EXEC dbo.[USP_INB_INL_GetShipmentCount] @InboundID =" + items.InboundID + "";
                int Count = DbUtility.GetSqlN(SP, ConnectionString);
                if (Count != 0)
                {
                    response.Result = "-3"; //Storage Location Cannot be empty
                    return response;
                }

                string query = "Update INB_Inbound SET ShipmentExpectedDate = " + "'" + items.ShipmentExpectedDate + "'" + " , InboundStatusID = 2 WHERE InboundID = " + items.InboundID + "";
                int resultinfo = DbUtility.GetSqlN(query, ConnectionString);
                if (resultinfo == 0)
                {
                    response.Result = "1"; //Shipment expected details successfully updated
                }
                else
                {
                    response.Result = "-4"; //Error while updating shipment expected details
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


        public async Task<Payload<string>> GetReceivingDockManagementDetails(GetReceivingDockManagementDetailsModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    { "@Inboundid", items.InboundID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "sp_INB_GETInbDocks", sqlParams).ConfigureAwait(false);
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


        public async Task<Payload<string>>  UNBLOCKPGRInbound(PGRUnblockModel items)
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            Payload<string> response = new Payload<string>();
            try
            {
                string Result = "";
                Task<string> RefNumber = _jsonPostService.SendMaterialTransferJSONDatatoSAP_ReturnPGR(items.InboundID);
                if (RefNumber.Result.Contains("Success"))
                {
                    int startIndex = RefNumber.Result.IndexOf(":") + 1;
                    int endIndex = RefNumber.Result.IndexOf("-");
                    Result = RefNumber.Result.Substring(startIndex, endIndex - startIndex);
                    string Sql = "Exec SP_PGR_Unblock_Inbound @InboundID=" + items.InboundID + ",@UserID=" + items.UserID+",@UnblockNumber="+ DBLibrary.SQuote(Result);
                    string Message = DbUtility.GetSqlS(Sql, this.ConnectionString);
                    if (Message.Contains("Success"))
                    {
                        response.Result = Message;
                    }
                    else
                    {
                        response.addError(Message);
                    }
                    
                }
                else
                {
                    Result = RefNumber.Result;
                    response.addError(Result);
                }

                return response;

            }
            catch(Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }

        public async Task<Payload<string>> UpsertReceivingDockManagement(UpsertReceivingDockManagementModel items)
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            Payload<string> response = new Payload<string>();
            try
            {
                string sp = "select InboundStatusID AS N from INB_Inbound where IsActive=1 and IsDeleted=0  and InboundID=@InboundID";
                int InboundStatusID = 0;
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(sp, conn))
                    {
                        cmd.Parameters.AddWithValue("@InboundID", items.InboundID);
                        conn.Open();
                        InboundStatusID = (int)cmd.ExecuteScalar();
                    }
                }

                if (InboundStatusID >= 6)
                {
                    response.Result = "-1"; //Unable to update the Dock
                    return response;
                }
                if (InboundStatusID == 1)
                {
                    response.Result = "11"; //Unable to update the Dock
                    return response;
                }
                //string DockInfo = "EXEC USP_INB_INL_GetInboundDock @InboundID = " + "'" + items.InboundID + "'" + ", @DockID =" + "'" + items.DockID + "'" + "";
                //var InboundDockID =  DbUtility.GetSqlN( DockInfo, this.ConnectionString);
               
                if (items.InboundID != 0)
                {
                    StringBuilder UpsertReceivingDockManagement = new StringBuilder();
                    UpsertReceivingDockManagement.AppendLine("Exec sp_INB_Upsert_inbounddock");
                    UpsertReceivingDockManagement.AppendLine("@InboundDockID = " + items.InboundDockID + ",");
                    UpsertReceivingDockManagement.AppendLine("@InboundID = " + items.InboundID + ",");
                    UpsertReceivingDockManagement.AppendLine("@DockID =" + items.DockID + ",");
                    UpsertReceivingDockManagement.AppendLine("@VehicleRegNo =" + DBLibrary.SQuote(items.VehicleRegNo) + ",");
                    UpsertReceivingDockManagement.AppendLine("@DriverName =" + DBLibrary.SQuote(items.DriverName) + ",");
                    UpsertReceivingDockManagement.AppendLine("@CreatedBy = " + items.UserID + ",");
                    UpsertReceivingDockManagement.AppendLine("@CreatedOn =" + DBLibrary.SQuote(DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss")) + ",");
                    UpsertReceivingDockManagement.AppendLine("@ReceivingStatus =" + DBLibrary.SQuote(items.RecievingStatus )+ ",");
                    UpsertReceivingDockManagement.AppendLine("@DriverContactNo =" + DBLibrary.SQuote(items.DriverContactNo) + ",");
                    UpsertReceivingDockManagement.AppendLine("@VehicleWeight =" + DBLibrary.SQuote(items.VehicleWeight) + "");
                    var DockCreated = DbUtility.GetDS(UpsertReceivingDockManagement.ToString(), ConnectionString);
                    response.Result = DockCreated.Tables[0].Rows[0]["Success"].ToString();
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

        public async Task<Payload<string>> DeleteReceivingDockManagementDetails(DeleteReceivingDockManagementDetailsModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                 {
                         { "@InboundDockID", items.InboundDockID },
                         { "@UserID", items.UserID }
                 };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                var result = await DbUtility.GetjsonData(this.ConnectionString, "sp_INB_DeleteInbDocks", sqlParams).ConfigureAwait(false);
                JArray jArray = JArray.Parse(result);
                int n = (int)jArray[0]["N"];
                if (n == 1)
                {
                    response.Result = "1";//Successfully removed...
                }
                else
                {
                    response.Result = "-1";//Dock can not be removed at this stage
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



        public async Task<Payload<string>> Get_ShipmentReceivedDetails(Get_ShipmentReceivedDetailsModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                         { "@InboundID", items.InboundID },
                         { "@UserID", items.UserID },

                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                var result = await DbUtility.GetjsonData(this.ConnectionString, "USP_INB_INL_ShipmentReceivedOn", sqlParams).ConfigureAwait(false);
                if (result != "")
                {
                    response.Result = result;
                }
                else
                {
                    response.Result = "-1";//No Data Found
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



        public async Task<Payload<string>> ShipmentReceivedDetails(ShipmentReceivedDetailsModel items)
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            Payload<string> response = new Payload<string>();
            try
            {
                string PGRQCCheckExists = "EXEC dbo.[SP_Check_PGR_QCCheck] @InboundID=" + items.InboundID;
                string Result = DbUtility.GetSqlS(PGRQCCheckExists, ConnectionString);
                if(Result.Contains("Error"))
                {
                    response.addError(Result);
                    return response;
                }

                string DockInfo = "EXEC dbo.[USP_INB_INL_ShipmentDock] @InboundID =" + items.InboundID;
                int Dock = DbUtility.GetSqlN(DockInfo, ConnectionString);
                if (Dock == 0)
                {
                    response.Result = "-2"; // Provide Doc info...
                    return response;
                }

                if (items.INBTypeID == 1)
                {
                    string IsVirtualStockIN = "Exec dbo.[SP_Check_Virtual_DO_Exists] @InboundID=" + items.InboundID + ",@UserID=" + items.UserID;
                    string MissingNumbers = DbUtility.GetSqlS(IsVirtualStockIN, ConnectionString);
                    if(MissingNumbers.Contains("Error"))
                    {
                        response.addError(MissingNumbers);
                        return response;
                    }

                }


                if (items.INBTypeID == 2)
                {
                    string IsDockStockIN = "Exec dbo.[SP_Check_Cross_DO_Exists] @InboundID=" + items.InboundID + ",@UserID=" + items.UserID;
                    string MissingNumbers = DbUtility.GetSqlS(IsDockStockIN, ConnectionString);
                    if (MissingNumbers.Contains("Error"))
                    {
                        response.addError(MissingNumbers);
                        return response;
                    }

                }

                string SuppInfo = "EXEC dbo.[USP_INB_INL_ShipmentSupplierInvoice] @InboundID =" + items.InboundID + "";
                int SUP = DbUtility.GetSqlN(SuppInfo, ConnectionString);
                if (SUP == 0)
                {
                    response.Result = "-3"; // No PO's are configured to this supplier
                    return response;
                }


                string Query = "EXEC dbo.[USP_INB_INL_ShipmentCancelReceive] @InboundID =" + items.InboundID + " , @WarehouseID = " + items.IB_RefWarehouse_DetailsID + "";
                int IB_RefWarehouse_DetailsID = DbUtility.GetSqlN(Query, ConnectionString);


                string sp = "EXEC dbo.[USP_INB_INL_ShipmentRefwarehouse] @IB_RefWarehouse_DetailsID = " + IB_RefWarehouse_DetailsID + "";
                int result = DbUtility.GetSqlN(sp, ConnectionString);
                if (result > 0)
                {
                    response.Result = "-1";//This shipment is already received
                    return response;
                }

                StringBuilder InboundTracking_WarehouseID = new StringBuilder();
                InboundTracking_WarehouseID.AppendLine("DECLARE @NewUpdateInboundTracking_WarehouseID int;  ");
                InboundTracking_WarehouseID.AppendLine("EXEC [sp_INB_UpsertInboundTracking_Warehouse]  @InboundTracking_WarehouseID=0");
                InboundTracking_WarehouseID.AppendLine(",@InboundID=" + items.InboundID);
                InboundTracking_WarehouseID.AppendLine(",@IB_RefWarehouse_DetailsID=" + IB_RefWarehouse_DetailsID);
                InboundTracking_WarehouseID.AppendLine(",@UserID=" + items.UserID);
                InboundTracking_WarehouseID.AppendLine(",@ShipmentReceivedOn=" + (String.IsNullOrEmpty(items.ShipmentReceivedOn) ? "NULL" : "'" + items.ShipmentReceivedOn + "'"));
                InboundTracking_WarehouseID.AppendLine(",@Offloadtime=" + (String.IsNullOrEmpty(items.Offloadtime) ? "NULL" : "'" + items.Offloadtime + "'"));
                InboundTracking_WarehouseID.AppendLine(",@ProcessedOn=NULL");
                InboundTracking_WarehouseID.AppendLine(",@ShipmentVerifiedOn=NULL");
                InboundTracking_WarehouseID.AppendLine(",@Disc_PackagesReceived=NULL");
                InboundTracking_WarehouseID.AppendLine(",@Disc_Remarks=NULL");
                InboundTracking_WarehouseID.AppendLine(",@Disc_CheckedBy=NULL");
                InboundTracking_WarehouseID.AppendLine(",@Disc_CheckedDate=NULL");
                InboundTracking_WarehouseID.AppendLine(",@Disc_VerifiedBy=NULL");
                InboundTracking_WarehouseID.AppendLine(",@Disc_VerifiedDate=NULL");
                InboundTracking_WarehouseID.AppendLine(",@DiscrepancyStatusID=NULL");
                InboundTracking_WarehouseID.AppendLine(",@HasDiscrepancy=0");
                InboundTracking_WarehouseID.AppendLine(",@Remarks=NULL");
                InboundTracking_WarehouseID.AppendLine(",@CreatedBy=" + items.UserID);
                InboundTracking_WarehouseID.AppendLine(",@INBTypeid=" + items.INBTypeID);
                InboundTracking_WarehouseID.AppendLine(",@UpdatedOn=" + DBLibrary.SQuote(DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss")));
                InboundTracking_WarehouseID.AppendLine(",@NewInboundTracking_WarehouseID=@NewUpdateInboundTracking_WarehouseID OUTPUT  Select @NewUpdateInboundTracking_WarehouseID AS N;");

                var Output = await DbUtility.GetjsonDataFromDatasetINQ(this.ConnectionString, InboundTracking_WarehouseID.ToString());
                JObject data = JObject.Parse(Output);
                JArray table = (JArray)data["Table"];
                int AssgnResult = (int)table[0]["N"];

                if (AssgnResult > 0)
                {
                    string ShipmentVerificationStatus = "EXEC dbo.[USP_INB_INL_SuggestedPutaway] @InboundID = " + items.InboundID;
                    var ResOutput = await DbUtility.GetjsonDataFromDatasetINQ(this.ConnectionString, ShipmentVerificationStatus);
                    JObject jdata = JObject.Parse(ResOutput);
                    JArray jtable = (JArray)jdata["Table"];
                    int Count = (int)jtable[0]["Column1"];

                    if (Count == 0)
                    {
                        string PutawaySuggestionsQuery = "EXEC dbo.[USP_TRN_GeneratePutawaySuggestions] @InboundID = " + items.InboundID;
                        DbUtility.GetDS(PutawaySuggestionsQuery, this.ConnectionString);
                    }

                    string ChangeInboundStatus = "EXEC dbo.[USP_INB_INL_ShipmentRefwarehouseDetails] @InboundID = " + items.InboundID;
                    var OutputResult = await DbUtility.GetjsonDataFromDatasetINQ(this.ConnectionString, ChangeInboundStatus);
                    JObject jdata1 = JObject.Parse(OutputResult);
                    JArray jtable1 = (JArray)jdata1["Table"];
                    int CountOf_IB_RefWarehouse_DetailsID = (int)jtable1[0]["N"];

                    if (CountOf_IB_RefWarehouse_DetailsID != 0)
                    {
                        string InbStatus = "Update INB_Inbound SET InboundStatusID = 3 WHERE InboundID = " + items.InboundID + "";
                        int StatusID = DbUtility.GetSqlN(InbStatus, ConnectionString);
                    }
                    if(items.INBTypeID == 1)
                    {
                        string IsVirtualStockIN = "Exec dbo.[SP_Upsert_Virtual_StockIn_INB] @InboundID=" + items.InboundID + ",@UserID=" + items.UserID;
                        string StockIN = DbUtility.GetSqlS(IsVirtualStockIN, this.ConnectionString);

                    }



                    response.Result = AssgnResult.ToString();//Shipment received details successfully updated                  
                }
                else
                {
                    response.Result = "-2";//Error while updating shipment received details
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


        public async Task<Payload<string>> UpsertShipmentDetailsBasedonInwardType(ShipmentReceivedDetailsModel items)
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            Payload<string> response = new Payload<string>();
            try
            {
               
                if (items.INBTypeID == 1)
                {
                    string IsVirtualStockIN = "Exec dbo.[SP_Check_Virtual_DO_Exists] @InboundID=" + items.InboundID + ",@UserID=" + items.UserID;
                    string MissingNumbers = DbUtility.GetSqlS(IsVirtualStockIN, ConnectionString);
                    if (MissingNumbers.Contains("Error"))
                    {
                        response.addError(MissingNumbers);
                        return response;
                    }
                    else
                    {
                        string Upsertdock = DbUtility.GetSqlS("Exec dbo.[SP_Upsert_Dock_Details] @InboundID=" + items.InboundID + ",@UserID=" + items.InboundID,ConnectionString);
                        response.Result = Upsertdock;
                    }

                }


                if (items.INBTypeID == 2 || items.INBTypeID==3)
                {
                    string IsDockStockIN = "Exec dbo.[SP_Check_Cross_DO_Exists] @InboundID=" + items.InboundID + ",@UserID=" + items.UserID+",@InbTypeID="+items.INBTypeID;
                    string MissingNumbers = DbUtility.GetSqlS(IsDockStockIN, ConnectionString);
                    if (MissingNumbers.Contains("Error"))
                    {
                        response.addError(MissingNumbers);
                        return response;
                    }
                    else
                    {
                        response.Result = MissingNumbers;
                        return response;
                    }

                }

                if (items.INBTypeID == 1)
                {
                    string IsVirtualStockIN = "Exec dbo.[SP_Upsert_Virtual_StockIn_INB] @InboundID=" + items.InboundID + ",@UserID=" + items.UserID;
                    string StockIN = DbUtility.GetSqlS(IsVirtualStockIN, this.ConnectionString);
                    await Task.Delay(TimeSpan.FromSeconds(5));
                    string IsVirtualDOStockOut= "Exec dbo.[SP_Upsert_Virtual_StockOut_OBD] @InboundID=" + items.InboundID + ",@UserID=" + items.UserID;
                    string StockOut = DbUtility.GetSqlS(IsVirtualDOStockOut, this.ConnectionString);
                    response.Result = StockIN;

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




        public async Task<Payload<string>> GetGRNUpdateDetails(GetGRNUpdateDetailsModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    { "@inboundID" , items.InboundID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "sp_INB_GetGRNUpdateDetails", sqlParams).ConfigureAwait(false);
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


        public async Task<Payload<string>> FetchGRNDataForInbound(FetchGRNDataForInboundModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    { "@inboundID" , items.InboundID },
                    { "@PoheaderId" , items.POHeaderID },
                    { "@SupplierInvoiceID" , items.SupplierInvoiceID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "SP_GetInboundDataForGRN", sqlParams).ConfigureAwait(false);
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
       

        public async Task<Payload<string>> GetGRNXMLData(int InboundId, string PONumber, string InvoiceNumber, int InboundType, string Remarks)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                string ErrorCode = "";
                string urlParameters;
                string qadurl = this.APIURL;
                string URL = "" + qadurl + "/Inbound/GetGRNXMLData";
                //URL = "" + " http://localhost/FWMSC21_GSK_API/ " + "/Inbound/QADUpdateInventorystatus";
                urlParameters = "?InboundId=" + InboundId + "&&PONumber=" + PONumber + "&&InvoiceNumber=" + InvoiceNumber + "&&InboundType=" + InboundType + "&&Remarks=" + Remarks + "";
                var result = GetResponseFromSAP(URL, urlParameters);
                if (result.IsSuccessStatusCode)
                {
                    var jsonString = await result.Content.ReadAsStringAsync();
                    var item = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(jsonString);
                    var test = item.GetProperty("result").GetString();
                    var errorCode = item.GetProperty("errorcode").GetString();
                    if (test == "success")
                    {
                        response.Result = "1";
                        return response;
                    }
                    else
                    {
                        //response.addError(errorCode);
                        //response.Result = "2";
                        return response; //Error Code
                    }
                }
                else
                {
                    response.Result = "-3";
                    return response; //Connection Error
                }
                return response;
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

        public static HttpResponseMessage GetResponseFromSAP(string URL, string urlParameters)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);


            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync(urlParameters).Result;
            return response;
        }

        public async Task<Payload<string>> GetDiscrepancyDetails(GetDiscrepancyDetailsModel items)
        {
            Payload<string> response = new Payload<string>();
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                string Disc_VerifiedBy = "";
                string Disc_CheckedBy = "";

                string Query = "EXEC dbo.[USP_INB_INL_ShipmentCancelReceive] @InboundID =" + items.InboundID + " , @WarehouseID = " + items.WarehouseId + "";
                int IB_RefWarehouse_DetailsID = DbUtility.GetSqlN(Query, ConnectionString);

                string DiscrepancyQuery = "EXEC [dbo].[USP_INB_INL_ShipmentVerificationRef] @InboundID = " + items.InboundID + ",@IB_RefWarehouse_DetailsID = " + IB_RefWarehouse_DetailsID + "";
                var DS = DbUtility.GetDS(DiscrepancyQuery, this.ConnectionString);

                if (DS.Tables[0].Rows.Count > 0)
                {
                    var CheckedBy = DS.Tables[0].Rows[0]["Disc_CheckedBy"] != DBNull.Value ? Convert.ToInt32(DS.Tables[0].Rows[0]["Disc_CheckedBy"]) : 0;
                    var CheckedByID = DS.Tables[0].Rows[0]["Disc_CheckedBy"] != DBNull.Value ? Convert.ToInt32(DS.Tables[0].Rows[0]["Disc_CheckedBy"]) : 0;
                    var Disc_CheckedDate = string.IsNullOrEmpty(Convert.ToString(DS.Tables[0].Rows[0]["Disc_CheckedDate"])) ? "NULL" : Convert.ToDateTime(DS.Tables[0].Rows[0]["Disc_CheckedDate"]).ToString("dd-MMM-yyyy");
                    var VerifiedBy = DS.Tables[0].Rows[0]["Disc_VerifiedBy"] != DBNull.Value ? Convert.ToInt32(DS.Tables[0].Rows[0]["Disc_VerifiedBy"]) : 0;
                    var VerifiedByID = DS.Tables[0].Rows[0]["Disc_VerifiedBy"] != DBNull.Value ? Convert.ToInt32(DS.Tables[0].Rows[0]["Disc_VerifiedBy"]) : 0;
                    var Disc_VerifiedDate = string.IsNullOrEmpty(Convert.ToString(DS.Tables[0].Rows[0]["Disc_VerifiedDate"])) ? "NULL" : Convert.ToDateTime(DS.Tables[0].Rows[0]["Disc_VerifiedDate"]).ToString("dd-MMM-yyyy");
                    var Disc_Remarks = string.IsNullOrEmpty(Convert.ToString(DS.Tables[0].Rows[0]["Disc_Remarks"])) ? "" : Convert.ToString(DS.Tables[0].Rows[0]["Disc_Remarks"]);

                    string sp = "SELECT DISTINCT FirstName AS Disc_CheckedBy FROM GEN_User WHERE UserID=@UserID AND IsActive=1 AND IsDeleted=0";
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand(sp, connection))
                        {
                            command.Parameters.AddWithValue("@UserID", CheckedBy);
                            Disc_CheckedBy = (string)command.ExecuteScalar();
                        }
                    }

                    string sp2 = "SELECT DISTINCT FirstName AS Disc_VerifiedBy FROM GEN_User WHERE UserID=@UserID AND IsActive=1 AND IsDeleted=0";
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand(sp2, connection))
                        {
                            command.Parameters.AddWithValue("@UserID", VerifiedBy);
                            Disc_VerifiedBy = (string)command.ExecuteScalar();
                        }
                    }

                    var resultTable = new Dictionary<string, object>
                    {
                        { "Disc_Remarks", Disc_Remarks },
                        { "Disc_VerifiedDate", Disc_VerifiedDate },
                        { "VerifiedBy_FirstName", Disc_VerifiedBy },
                        { "Disc_CheckedDate", Disc_CheckedDate },
                        { "FirstName", Disc_CheckedBy },
                        { "CheckedByID", CheckedByID },
                        { "VerifiedByID", VerifiedByID }
                    };

                    var result = new Dictionary<string, object>
                    {
                        { "Table", new List<Dictionary<string, object>> { resultTable } }
                    };
                    response.Result = JsonConvert.SerializeObject(result);
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

        public async Task<Payload<string>> GetDiscrepancyLineItems_PageLoad(GetDiscrepancyLineItems_PageLoadModel items)
        {
            Payload<string> response = new Payload<string>();
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                int IB_RefWarehouse_DetailsID = 0;
                string sp = "SELECT IB_RefWarehouse_DetailsID FROM INB_RefWarehouse_Details WHERE InboundID=@InboundID AND IsActive=1 AND IsDeleted=0";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sp, connection))
                    {
                        command.Parameters.AddWithValue("@InboundID", items.InboundID);
                        IB_RefWarehouse_DetailsID = (int)command.ExecuteScalar();
                    }
                }

                string Query = "EXEC [dbo].[sp_INB_DiscrepancyDetails] @IBRefered_WHID=" + IB_RefWarehouse_DetailsID + "";
                var DS = DbUtility.GetDS(Query, this.ConnectionString);
                response.Result = JsonConvert.SerializeObject(DS);

                //string SQLQuery = "EXEC [dbo].[sp_INB_DiscrepancyList] @InboundID = " + items.InboundID;
                //var Output = await DbUtility.GetjsonDataFromDatasetINQ(this.ConnectionString, SQLQuery);
                //JObject data = JObject.Parse(Output);
                //JArray table = (JArray)data["Table"];
                //int POHeaderID = (int)table[0]["POHeaderID"];

                //StringBuilder sp = new StringBuilder();
                //sp.AppendLine("EXEC [dbo].[sp_INB_DiscrepancyList] @InboundID=" + items.InboundID + "" + ";"
                //+ "EXEC [sp_INB_POWISE_Discrepancy] @POHeaderID =" + POHeaderID + " , @InboundID =" + items.InboundID + "");
                //string output = sp.ToString();
                //response.Result = await DbUtility.GetjsonDataFromDatasetINQ(this.ConnectionString, output);
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

        public async Task<Payload<string>> SaveDiscrepancyDetails(SaveDiscrepancyDetailsModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                StringBuilder SaveDiscrepancy = new StringBuilder();
                SaveDiscrepancy.AppendLine("EXEC [USP_INB_INL_InboundTracking] ");
                SaveDiscrepancy.AppendLine("@InboundID = " + items.InboundID + ",");
                SaveDiscrepancy.AppendLine("@Disc_Remarks = " + DBLibrary.SQuote(items.Disc_Remarks) + ",");
                SaveDiscrepancy.AppendLine("@Disc_CheckedBy = " + DBLibrary.SQuote(items.Disc_CheckedBy) + ",");
                SaveDiscrepancy.AppendLine("@Disc_CheckedDate =" + DBLibrary.SQuote(items.Disc_CheckedDate) + ",");
                SaveDiscrepancy.AppendLine("@Disc_VerifiedBy =" + DBLibrary.SQuote(items.Disc_VerifiedBy) + ",");
                SaveDiscrepancy.AppendLine("@Disc_VerifiedDate =" + DBLibrary.SQuote(items.Disc_VerifiedDate) + "");

                var result = DbUtility.GetSqlN(SaveDiscrepancy.ToString(), ConnectionString);
                if (result == 0)
                {
                    response.Result = "1";//Succesfully Saved
                }
                else
                {
                    response.Result = "-1";//Error
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

        public async Task<Payload<string>> UpsertDiscrepancyDetails(UpsertDiscrepancyDetailsModel items)
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            Payload<string> response = new Payload<string>();
            try
            {
                string SP = "EXEC dbo.[USP_INB_INL_ShipmentVerificationRefDetails] @WarehouseID =" + "'" + items.WarehouseId + "'" + " , @InboundID = " + items.InboundID + "";
                int IB_RefWarehouse_DetailsID = DbUtility.GetSqlN(SP, ConnectionString);

                string Query = "EXEC dbo.[USP_INB_INL_ShipmentVerificationTrack] @IB_RefWarehouse_DetailsID =" + IB_RefWarehouse_DetailsID + "";
                string ShipmentVerifiedOn = DbUtility.GetSqlS(Query, ConnectionString);

                if (ShipmentVerifiedOn != "")
                {
                    response.Result = "-1"; //Cannot change the shipment details, as shipment is verified
                    return response;
                }

                string query = "EXEC dbo.[USP_INB_INL_ShipmentVerificationPO] @TenantID =" + items.TenantID + " , @PONumber = " + "'" + items.PONumber + "'" + "";
                int POHeaderID = DbUtility.GetSqlN(query, ConnectionString);

                if (POHeaderID == 0)
                {
                    response.Result = "-2"; //PO Number does not exist
                    return response;
                }


                string sp = "EXEC dbo.[USP_INB_INL_ShipmentVerificationInvoice] @SupplierID =" + items.SupplierID + " , @POHeaderID = " + items.POHeaderID + " , @InvoiceNumber = " + items.InvoiceNumber + "";
                int SupplierInvoiceID = DbUtility.GetSqlN(sp, ConnectionString);

                if (SupplierInvoiceID == 0)
                {
                    response.Result = "-3"; //Invoice Number does not exist
                    return response;
                }


                string resultQuery = "EXEC dbo.[USP_INB_INL_ShipmentVerificationMaterial] @MCode =" + "'" + items.MCode + "'" + " , @TenantID = " + items.TenantID + "";
                int MaterialMasterID = DbUtility.GetSqlN(resultQuery, ConnectionString);

                string LineNumberQuery = "EXEC dbo.[USP_INB_INL_ShipmentVerificationLineNumber] @LineNumber =" + items.LineNumber + " , @InboundID = " + items.InboundID + " , @MaterialMasterID = " + MaterialMasterID + " , @SupplierInvoiceID = " + SupplierInvoiceID + " , @POHeaderID = " + POHeaderID + "";
                int LineNumber = DbUtility.GetSqlN(LineNumberQuery, ConnectionString);

                if (LineNumber == 0)
                {
                    response.Result = "-4"; //Invalid PO line number
                }

                //StringBuilder UpsertDiscrepancy = new StringBuilder();
                //UpsertDiscrepancy.AppendLine("DECLARE @NewUpdateInboundDiscID int;  EXEC [sp_INB_UpsertDiscrepancyDetails] ");
                //UpsertDiscrepancy.AppendLine("@IB_RefWarehouse_DetailsID = " + IB_RefWarehouse_DetailsID + ",");
                //UpsertDiscrepancy.AppendLine("@MaterialMasterID = " + MaterialMasterID + ",");
                //UpsertDiscrepancy.AppendLine("@DiscrepancyID = " + items.DiscrepancyID + ",");
                //UpsertDiscrepancy.AppendLine("@SupplierInvoiceID =" + SupplierInvoiceID + ",");
                //UpsertDiscrepancy.AppendLine("@LineNumber =" + items.LineNumber + ",");
                //UpsertDiscrepancy.AppendLine("@ReceivedQuantity =" + items.ReceivedQuantity + ",");
                //UpsertDiscrepancy.AppendLine("@DiscrepancyDescription = " + "'" + items.DiscrepancyDescription + "'" + ",");
                //UpsertDiscrepancy.AppendLine("@CreatedBy = " + items.UserID + ",");
                //UpsertDiscrepancy.AppendLine("@NewInbound_DiscrepancyID=@NewUpdateInboundDiscID OUTPUT;  select @NewUpdateInboundDiscID AS N");

                //DbUtility.GetSqlN(UpsertDiscrepancy.ToString(), ConnectionString);

                var sqlParams = new Dictionary<string, object>
                {
                      {"@DiscrepancyID",items.DiscrepancyID},
                      {"@MaterialMasterID",MaterialMasterID},
                      {"@IB_RefWarehouse_DetailsID",IB_RefWarehouse_DetailsID},
                      {"@SupplierInvoiceID",SupplierInvoiceID},
                      {"@LineNumber",items.LineNumber},
                      {"@ReceivedQuantity",items.ReceivedQuantity},
                      {"@DiscrepancyDescription",items.DiscrepancyDescription},
                      {"@CreatedBy",items.UserID}
                };

                var result = await DbUtility.GetjsonData(this.ConnectionString, "sp_INB_UpsertDiscrepancy", sqlParams).ConfigureAwait(false);
                JArray jArray = JArray.Parse(result);
                int n = (int)jArray[0]["N"];

                if (n == 1)
                {
                    response.Result = "1";//"Successfully Updated"
                }
                else if (n == -1)
                {
                    response.Result = "-5"; //"No excess and shortage for this Item"
                    return response;
                }
                else if (n == -2)
                {
                    response.Result = "-6"; //"Total Qty. is more than Invoice Qty."
                    return response;
                }
                else if (n == -3)
                {
                    response.Result = "-7"; //"Quantity is more than excess received qty."
                    return response;
                }
            }
            catch (SqlException Sqlex)
            {
                //response.addError(Sqlex.Message); 
                if (Sqlex.ErrorCode == -2146232060)
                {
                    response.Result = ("-8");   //This line item is already updated
                }
                else
                {
                    response.Result = ("-9");  //Error while updating discrepancy details
                }
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }

        public async Task<Payload<string>> DeleteDiscrepancyDetails(DeleteDiscrepancyDetailsModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();

                string SP = "EXEC dbo.[USP_INB_INL_ShipmentVerificationDiscrepancy] @DiscrepancyID =" + items.DiscrepancyID + "";
                int n = DbUtility.GetSqlN(SP, ConnectionString);

                if (n == 0)
                {
                    response.Result = "1"; //"Successfully deleted the selected line items"  
                }
                else
                {
                    response.Result = "-1"; //"Error while deleting discrepancy line items"
                }
                return response;
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

        public async Task<Payload<string>> CheckDiscrepency_OnPageLoad(CheckDiscrepency_OnPageLoadModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();

                string SP = "EXEC dbo.[USP_CheckDiscrepency_Inbound] @InboundID =" + items.InboundID + "";
                int n = DbUtility.GetSqlN(SP, ConnectionString);

                if (n == 0)
                {
                    response.Result = "0"; //"No Discrepency"  
                }
                else
                {
                    response.Result = "1"; //"Has Discrepency"
                }
                return response;
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

        public async Task<Payload<string>> UpdateShipmentVerificationDetails(UpdateShipmentVerificationDetailsModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();

                string SP = "EXEC dbo.[USP_INB_INL_ShipmentVerificationAvailableStock] @InboundID =" + items.InboundID + "";
                int Result_GRN = DbUtility.GetSqlN(SP, ConnectionString);

                if (Result_GRN == 0)
                {
                    response.Result = "-1"; //"GRN is not at done for received items"  
                    return response;
                }

                string Query = "EXEC dbo.[USP_INB_INL_ShipmentCancelReceive] @InboundID =" + items.InboundID + " , @WarehouseID = " + items.WarehouseId + "";
                int IB_RefWarehouse_DetailsID = DbUtility.GetSqlN(Query, ConnectionString);


                string sp = "EXEC dbo.[USP_INB_INL_ShipmentVerificationReceivedOn] @IB_RefWarehouse_DetailsID =" + IB_RefWarehouse_DetailsID + "";
                string ShipmentReceivedOn = DbUtility.GetSqlS(sp, ConnectionString);

                if (ShipmentReceivedOn == "")
                {
                    response.Result = "-2"; //Shipment not received yet
                    return response;
                }

                string ResQuery = "EXEC dbo.[usp_inb_updateshipmentverified] @inboundid =" + items.InboundID + ", @warehouseid=" + items.WarehouseId + ", @shipmentverifieddate='" + ShipmentReceivedOn + "', @shipmenttypeid=" + items.ShipmentTypeID + ", @remarks='" + items.Remarks + "'";

                int Resultqry = DbUtility.GetSqlN(ResQuery, ConnectionString);

                if(Resultqry==10)
                {
                    response.Result = "10";
                    return response;

                }

                else if (Resultqry == 9)
                {
                    response.Result = "9";
                    return response;

                }

                else if (Resultqry == 8)
                {
                    response.Result = "8";
                    return response;

                }

                else if (Resultqry == 7)
                {
                    response.Result = "7";
                    return response;

                }
                else if (Resultqry == -7)
                {
                    response.Result = "-7";
                    return response;

                }

                else if (Resultqry == 6)
                {
                    response.Result = "6";
                    return response;

                }

                else if (Resultqry == 5)
                {
                    response.Result = "5";
                    return response;

                }

                else if (Resultqry == 4)
                {
                    response.Result = "4";
                    return response;

                }

                else if (Resultqry == 3)
                {
                    response.Result = "3";
                    return response;

                }

                else if (Resultqry == 2)
                {
                    response.Result = "2";
                    return response;

                }

                else if (Resultqry == 1)
                {
                    response.Result = "1";
                    return response;

                }




                //    string ResQuery = "EXEC dbo.[sp_CheckPalletPutaway] @TransactionDocId =" + items.InboundID + "";
                //    int Carton_LocationId = DbUtility.GetSqlN(ResQuery, ConnectionString);

                //    if (Carton_LocationId == 0)
                //    {
                //        response.Result = "-3"; //Please put Away the Container
                //        return response;
                //    }

                //    string QueryResult = "EXEC dbo.[USP_INB_INL_GetShipmentVerifiedOn] @IB_RefWarehouse_DetailsID =" + IB_RefWarehouse_DetailsID + "";
                //    string ShipmentVerifiedOn = DbUtility.GetSqlS(QueryResult, ConnectionString);

                //    if (ShipmentVerifiedOn != "")
                //    {
                //        response.Result = "-4"; //This shipment is already verified
                //        return response;
                //    }

                //    string ResultQuery = "EXEC dbo.[USP_INB_INL_ShipmentVerificationGoodMovement] @TransactionDocID =" + items.InboundID + "";
                //    int GoodsMovementDetailsID = DbUtility.GetSqlN(ResultQuery, ConnectionString);

                //    if (GoodsMovementDetailsID == 0)
                //    {
                //        response.Result = "-5"; //Goods-IN is not yet performed
                //        return response;
                //    }

                //    //commented by ch. prasanna --- we are not using this structure in Falcon WMS
                //    //string InwardQCCheck = "EXEC dbo.[sp_INV_InwardQCCheck] @InboundID =" + items.InboundID + " , @SupplierInvoiceID = " + items.SupplierInvoiceID + "";
                //    //int N = DbUtility.GetSqlN(InwardQCCheck, ConnectionString);

                //    //if (N == 0)
                //    //{
                //    //    response.Result = "-6"; //QC yet to be captured
                //    //    return response;
                //    //}

                //    string ShipmentQuery = "select ShipmentTypeID from GEN_ShipmentType where ShipmentTypeID = @ShipmentTypeID";
                //    int ShipmentTypeID = 0;

                //    using (SqlConnection connection = new SqlConnection(ConnectionString))
                //    {
                //        using (SqlCommand command = new SqlCommand(ShipmentQuery, connection))
                //        {
                //            command.Parameters.AddWithValue("@ShipmentTypeID", items.ShipmentTypeID);
                //            connection.Open();
                //            object result = command.ExecuteScalar();
                //            if (result != null)
                //            {
                //                ShipmentTypeID = Convert.ToInt32(result);
                //            }
                //        }
                //    }

                //    //if (ShipmentTypeID == 11)
                //    //{
                //    //    string ShipmentInboundQuery = "EXEC [dbo].[USP_INB_INL_ShipmentInbound] @InboundID = " + items.InboundID + "";
                //    //    int JobOrderStatusID = DbUtility.GetSqlN(ShipmentInboundQuery, ConnectionString);
                //    //} 

                //    string ShipmentVerifiedQuery = "EXEC [dbo].[USP_Close_PO_DuringShipmentVerification] @InboundID = " + items.InboundID + " , @UserID = " + items.UserID + "";
                //    int Verified = DbUtility.GetSqlN(ShipmentVerifiedQuery, ConnectionString);

                //    if (Verified == 0)
                //    {
                //        response.Result = "1"; //Shipment verification details successfully updated
                //        return response;
                //    }




                //    string InboundTrackQuery = "EXEC dbo.[USP_INB_INL_ShipmentInboundTrack] @IB_RefWarehouse_DetailsID =" + IB_RefWarehouse_DetailsID + "";
                //    string ShipmentReceivedON = DbUtility.GetSqlS(InboundTrackQuery, ConnectionString);

                //    StringBuilder sCmdShipmentVerificationDetails = new StringBuilder();
                //    // sCmdShipmentVerificationDetails.AppendLine("Update INB_InboundTracking_Warehouse set ShipmentVerifiedOn = " + DBLibrary.SQuote(DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss")) + ",");
                //    //sCmdShipmentVerificationDetails.AppendLine("Update INB_InboundTracking_Warehouse set ShipmentVerifiedOn = " + DBLibrary.SQuote(DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss")) + ",");
                //    ////  sCmdShipmentVerificationDetails.AppendLine("Remarks = " + DBLibrary.SQuote(string.IsNullOrEmpty(items.Remarks) ? "''" : items.Remarks + ","));
                //    //sCmdShipmentVerificationDetails.AppendLine("Remarks = " + DBLibrary.SQuote(string.IsNullOrEmpty(items.Remarks) ? "''" : items.Remarks + ","));
                //    ////  sCmdShipmentVerificationDetails.AppendLine(" where IB_RefWarehouse_DetailsID = " + IB_RefWarehouse_DetailsID + ",");
                //    //sCmdShipmentVerificationDetails.AppendLine(" where IB_RefWarehouse_DetailsID = " + IB_RefWarehouse_DetailsID);
                //    //DbUtility.GetSqlN(sCmdShipmentVerificationDetails.ToString(), ConnectionString);
                //    //DbUtility.GetSqlN(sCmdShipmentVerificationDetails.ToString(), ConnectionString);


                //    DbUtility.GetSqlN(sCmdShipmentVerificationDetails.ToString(), ConnectionString);



                //    string ShipmentSP = "EXEC dbo.[USP_INB_INL_ShipmentInboundTrackVerifiedOn] @InboundID = " + items.InboundID + "";
                //    int ShipmentVerifiedCount = DbUtility.GetSqlN(ShipmentSP, ConnectionString);
                //    string WHID = items.WarehouseId.Substring(0, items.WarehouseId.Length - 1);

                //    if (ShipmentVerifiedCount == WHID.Length)
                //    {
                //        string ShipmentUpdateQuery = "EXEC dbo.[USP_INB_INL_ShipmentInboundTrackUpdate] @InboundID = " + items.InboundID + "";
                //        int ShipmentUpdated = DbUtility.GetSqlN(ShipmentUpdateQuery, ConnectionString);

                //        if (ShipmentUpdated == 1)
                //        {
                //            response.Result = "26";//Shipment Closed
                //        }
                //        else
                //        {
                //            response.Result = "-7";//Error while updating verification details"
                //        }
                //        return response;
                //    }
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

        public async Task<Payload<string>> GetShipmentVerificationDetails(GetShipmentVerificationDetailsModel items)
        {
            Payload<string> response = new Payload<string>();
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                string Query = "EXEC [dbo].[sp_INB_GetShipmentVerifiedDetails] @InboundID=" + items.InboundID + "";
                var DS = DbUtility.GetDS(Query, this.ConnectionString);
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


        public async Task<Payload<string>> RevertGRNDetails(RevertGRNDetailsModel items)
        {
            ////string sqlGrnRevert = "2";
            //Payload<string> response = new Payload<string>();
            //try
            //{
            //    DBFactory factory = new DBFactory();
            //    IDBUtility DbUtility = factory.getDBUtility();
            //    List<string> DT = items.GRNHeaderIDs.Split(',').ToList();

            //    foreach (var row in DT)
            //    {
            //        var InboundID = items.InboundID;
            //        var GRNUpdateID = row;
            //        string qadurl = ConfigurationManager.AppSettings["urlQAD"];
            //        string URL = "" + qadurl + "/Inbound/QADGRNRevert";
            //        //string URL = "/http://localhost/FWMSC21_GSK_API/Inbound/QADGRNRevert";

            //        string urlParameters = "?grnHeaderID=" + GRNUpdateID + "&&isSupplierRtn=0";
            //        HttpClient client = new HttpClient();
            //        client.BaseAddress = new Uri(URL);

            //        client.DefaultRequestHeaders.Accept.Add(
            //        new MediaTypeWithQualityHeaderValue("application/json"));

            //        HttpResponseMessage result = client.GetAsync(urlParameters).Result;

            //        if (result.IsSuccessStatusCode)
            //        {
            //            string jsonString = result.Content.ReadAsStringAsync().Result;
            //            JavaScriptSerializer serializer = new JavaScriptSerializer();
            //            dynamic item = serializer.Deserialize<object>(jsonString);
            //            string CheckResult = item["result"];
            //            string ErrorCode = item["errorcode"];
            //            if (CheckResult == "success")
            //            {
            //                string ShipmentSP = "EXEC[dbo].[sp_INV_RevertGRNConfirmedStock] @GRNHeaderID = " + row + " , @CreatedBy = " + items.UserID + "";
            //                int GrnRevert = DbUtility.GetSqlN(ShipmentSP, ConnectionString);
            //                if (GrnRevert != 1)
            //                {
            //                    response.Result = "-1"; //"Once Goods-Out process is started, cannot revert the shipment details"
            //                    return response;
            //                }
            //            }
            //            else
            //            {
            //                response.Result = "-2"; //Error Code from QAD System
            //                return response;
            //            }
            //        }
            //        else
            //        {
            //            response.Result = "-3"; //Connection Error
            //            return response;
            //        }
            //        client.Dispose();
            //    }
            //    try
            //    {
            //        string ShipmentCloseSP = "EXEC [dbo].[sp_INB_ClosePOStatus] @InboundID = " + items.InboundID + " , @POStatusID = 1 , @Status = 1";
            //        int ClosePOStatus = DbUtility.GetSqlN(ShipmentCloseSP, ConnectionString);

            //        string Revertquery = "select Count(*) AS [N] from INB_GRNUpdate where InboundID=@InboundID AND IsActive=1 AND IsDeleted=0 AND IsCancelled=0";
            //        int InboundID = items.InboundID;
            //        int count;
            //        using (SqlConnection connection = new SqlConnection(ConnectionString))
            //        {
            //            using (SqlCommand command = new SqlCommand(Revertquery, connection))
            //            {
            //                command.Parameters.Add("@InboundID", SqlDbType.Int).Value = InboundID;
            //                connection.Open();
            //                count = (int)command.ExecuteScalar();
            //            }
            //        }

            //        if (count == 0)
            //        {
            //            string query = "update INB_Inbound set InboundStatusID=3 where InboundID = @InboundID";
            //            using (SqlConnection connection = new SqlConnection(ConnectionString))
            //            {
            //                using (SqlCommand command = new SqlCommand(query, connection))
            //                {
            //                    command.Parameters.AddWithValue("@InboundID", items.InboundID);
            //                    connection.Open();
            //                    command.ExecuteNonQuery();
            //                }
            //            }
            //        }
            //        response.Result = "1"; // Successfully deleted the selected line items
            //    }
            //    catch (Exception ex)
            //    {
            //        response.Result = "-4"; //Error while deleting selected line items
            //    }
            //}
            //catch (SqlException Sqlex)
            //{
            //    response.addError(Sqlex.Message);
            //}
            //catch (Exception ex)
            //{
            //    response.addError(ex.Message);
            //}
            //return response;
       
            Payload<string> response = new Payload<string>();
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                string sp = "EXEC[dbo].[sp_INV_RevertGRNConfirmedStock] @GRNHeaderID = " + "'" + items.GRNHeaderIDs + " ', @CreatedBy = " + items.UserID + "";
                var DS = DbUtility.GetDS(sp, this.ConnectionString);
                string GRNRevert = Convert.ToString(DS.Tables[0].Rows[0]["N"]);
                if (GRNRevert != "Success")
                {
                    response.Result = GRNRevert;//"Once Goods-Out process is started, cannot revert the shipment details"
                    return response;                 
                }

                try
                {
                    string ShipmentCloseSP = "EXEC [dbo].[sp_INB_ClosePOStatus] @InboundID = " + items.InboundID + " , @POStatusID = 1 , @Status = 1";
                    int ClosePOStatus = DbUtility.GetSqlN(ShipmentCloseSP, ConnectionString);

                    string Revertquery = "select Count(*) AS [N] from INB_GRNUpdate where InboundID=@InboundID AND IsActive=1 AND IsDeleted=0 AND IsCancelled=0";
                    int InboundID = items.InboundID;
                    int count;
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        using (SqlCommand command = new SqlCommand(Revertquery, connection))
                        {
                            command.Parameters.Add("@InboundID", SqlDbType.Int).Value = InboundID;
                            connection.Open();
                            count = (int)command.ExecuteScalar();
                        }
                    }

                    if (count == 0)
                    {
                        string query = "update INB_Inbound set InboundStatusID=3 where InboundID = @InboundID";
                        using (SqlConnection connection = new SqlConnection(ConnectionString))
                        {
                            using (SqlCommand command = new SqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@InboundID", items.InboundID);
                                connection.Open();
                                command.ExecuteNonQuery();
                            }
                        }
                    }
                   // response.Result = "1"; // Successfully deleted the selected line items
                    response.Result = "GRN Reverted Successfully"; // Successfully deleted the selected line items
                }
                catch (Exception ex)
                {
                    //response.Result = "-4"; //Error while deleting selected line items
                    response.Result = "Error while deleting selected line items"; //Error while deleting selected line items
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

        public async Task<Payload<string>> ShipmentCloseDetails(ShipmentCloseDetailsModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                string Query = "EXEC dbo.[USP_INB_INL_GetInBoundIDCount] @InboundID = " + items.InboundID + "";
                int InboundCount = DbUtility.GetSqlN(Query, ConnectionString);
                if (InboundCount > 0)
                {
                    response.Result = "-1"; //Sorry, you cannot close this shipment until you revert to 'Shipment Initiated' status
                    return response;
                }

                string SP = "EXEC dbo.[USP_INB_INL_UpdateInbound] @InboundID = " + items.InboundID + "";
                int result = DbUtility.GetSqlN(SP, ConnectionString);

                string sp = "EXEC dbo.[USP_INB_INL_GetWarehouseDetails] @IB_RefWarehouse_DetailsID = " + items.IB_RefWarehouse_DetailsID + "";
                int Warehouseid = DbUtility.GetSqlN(sp, ConnectionString);

                if (Warehouseid > 0)
                {
                    response.Result = "1"; //Successfully Closed
                    return response;
                }
                else
                {
                    response.Result = "-2"; //Error while closing this shipment
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

        public async Task<Payload<string>> GetInboundRevertDetails(GetInboundRevertDetailsModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                      {"@StoreRefNo","'" + items.StoreRefNo + "'"},
                      {"@TenantID",items.TenantID},
                      {"@AccountID_New",items.AccountID},
                      {"@UserID_New",items.UserID}
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "SP_INB_RevertInbound", sqlParams).ConfigureAwait(false);
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


        public async Task<Payload<string>> GetRevertGRNDetails(GetRevertGRNDetailsModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                string SQL = "EXEC sp_INB_GetGRNUpdateDetails @InboundID = " + items.InboundID;
                var Result = await DbUtility.GetjsonDataFromDatasetINQ(this.ConnectionString, SQL);
                var jsonObject = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(Result);
                var tableValue = jsonObject.GetProperty("Table");
                if (tableValue.ValueKind == JsonValueKind.Array && !tableValue.EnumerateArray().Any())
                {
                    response.Result = "-1";
                    return response;// No Data found
                }
                else
                {
                    response.Result = Result;   
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

        public async Task<Payload<string>> RevertShipmmentExpected(RevertShipmmentExpectedModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                StringBuilder sb = new StringBuilder();
                string confirmationResult = await CheckReceiptConfirmation(items.RefWHID);
                if (confirmationResult=="1")
                {
                    response.Result = "-2";// First revert Shipment Verification Details
                    return response;
                }
                sb.Append("EXEC  [dbo].[sp_INB_RevertShipmmentExpected]  @InboundID=" + items.vInboundTrackingID + ", @UpdatedBy = " + items.UserID + "");
                string sbAppend = sb.ToString();
                try
                {
                    var Result = await DbUtility.GetjsonDataFromDatasetINQ(this.ConnectionString, sbAppend);
                    response.Result = "1";//Shipment expected details successfully reverted 
                    return response;
                }
                catch (Exception Ex )
                {
                    response.Result = "-5";// resetError("Error while reverting Shipment expected details", true);
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
        public async Task<string> CheckReceiptConfirmation(int RefWHID)
        {
            int status = 0;
            string ShipmentVerifiedOn = "", ShipmentReceivedOn = "";
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            string CheckReceivedStatus = "EXEC dbo.[USP_INB_INL_GetInboundShipmentVerifiedOn] @IB_RefWarehouse_DetailsID = " + RefWHID;
            var CheckReceived = await DbUtility.GetjsonDataFromDatasetINQ(this.ConnectionString, CheckReceivedStatus);
            JObject obj = JObject.Parse(CheckReceived);
            JArray rows = (JArray)obj["Table"];
            foreach (JObject row in rows)
            {
                ShipmentVerifiedOn = row["S"].ToString();
                ShipmentReceivedOn = row["R"].ToString();
            }
            if ((ShipmentVerifiedOn != null && ShipmentVerifiedOn != "") || (ShipmentReceivedOn != null && ShipmentReceivedOn != ""))
            {
                status = 1;
                return status.ToString();
            }
            else
            {
                status = 2;
            }
            return status.ToString();
        }
        public async Task<string> CheckReceivedStatus(int RefWHID)
        {
            int status = 0;
            string ShipmentVerifiedOn = "";
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            string CheckReceivedStatus = "EXEC dbo.[USP_INB_INL_GetInboundShipmentVerifiedOn] @IB_RefWarehouse_DetailsID = " + RefWHID;
            var CheckReceived = await DbUtility.GetjsonDataFromDatasetINQ(this.ConnectionString, CheckReceivedStatus);
            JObject obj = JObject.Parse(CheckReceived);
            JArray rows = (JArray)obj["Table"];
            foreach (JObject row in rows)
            {
                ShipmentVerifiedOn = row["S"].ToString();
            }
            if (ShipmentVerifiedOn != null && ShipmentVerifiedOn != "")
            {
                status = 1;
                return status.ToString();
            }
            else
            {
                status = 2;
            }
            return status.ToString();
        }
        public async Task<Payload<string>> RevertShipmmentReceived(RevertShipmmentReceivedModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("EXEC  [dbo].[sp_INB_RevertShipmmentReceived]  @InboundID=" + items.vInboundTrackingID + ", @UpdatedBy = " + items.UserID + "");
                string sbAppend = sb.ToString();
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                string confirmationResult = await CheckReceivedStatus(items.RefWHID);
                if (confirmationResult == "1")
                {
                    response.Result = "-2";// First revert Shipment Verification Details
                    return response;
                }
                string Query = "EXEC  [dbo].[USP_INB_INL_GetInboundGRNSelect] @InboundID= " + items.vInboundTrackingID;
                var Result = await DbUtility.GetjsonDataFromDatasetINQ(this.ConnectionString, Query);
                var jsonObject = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(Result);
                var tableValue = jsonObject.GetProperty("Table");
                if (tableValue.ValueKind != JsonValueKind.Array && tableValue.EnumerateArray().Any())
                {
                    response.Result = "-1";
                    return response; //Unable to Revert Shipment Received Until Revert GRN updated
                }
                string GetOBDTrackSQL = "";
                if (items.vInboundTrackingID != 0)
                    GetOBDTrackSQL = "EXEC [dbo].[sp_INB_GetInBoundReceivedDetails]  @AccountID_New=" + items.AccountID + ",@TenantID_New=" + items.TenantID + ", @InboundID =" + items.vInboundTrackingID;
                else
                    GetOBDTrackSQL = "EXEC [dbo].[sp_INB_GetInBoundReceivedDetails]   @InboundID =" + items.vInboundTrackingID;
                var Output = await DbUtility.GetjsonDataFromDatasetINQ(this.ConnectionString, GetOBDTrackSQL);
                JObject data = JObject.Parse(Output);
                JArray table = (JArray)data["Table"];
                int statusid = (int)table[0]["InboundStatusID"];
                if(statusid == 3 || statusid==7 || statusid==27 || statusid==28)
                {
                    try
                    {
                        var RevertReceived = await DbUtility.GetjsonDataFromDatasetINQ(this.ConnectionString, sbAppend);
                        response.Result = "1"; //success
                    }
                    catch(Exception ex)
                    {
                        response.Result = "-3";//"Error while reverting Shipment received details", true);
                    }
                   
                }
                else
                {
                    response.Result = "-5"; //Error While reverting
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

        //public async Task<Payload<string>> CreateGRNEntryAndPostDatatoSAP(int InboundId,string PONumber,string InvoiceNumber,int InboundType,string Remarks)
        //{
        //    Payload<string> response = new Payload<string>();
        //    try
        //    {
        //        DBFactory factory = new DBFactory();
        //        IDBUtility DbUtility = factory.getDBUtility();


        //        //string strMaterialTransactionID = "";
        //        //string materialTransactionID = "";

        //        //foreach (GRNDetails grn in items.GRNdetails)
        //        //{
        //        //    strMaterialTransactionID += grn.MaterialTransactionID + ",";
        //        //}

        //        //if (!string.IsNullOrEmpty(strMaterialTransactionID) && strMaterialTransactionID.EndsWith(","))
        //        //{
        //        //    materialTransactionID = strMaterialTransactionID.TrimEnd(',');
        //        //}

        //        String QueryData = "EXEC [dbo].[sp_INB_GetGRNUpdateDetails] @InboundID= " + InboundId + "";

        //        var TableData = await DbUtility.GetjsonDataFromDatasetINQ(this.ConnectionString, QueryData);
        //        JObject data = JObject.Parse(TableData);
        //        JArray table = (JArray)data["Table"];
        //         InboundId = (int)table[0]["InboundId"];
        //         PONumber = (string)table[0]["PONumber"];
        //         InvoiceNumber = (string)table[0]["InvoiceNumber"];
        //         InboundType = (int)table[0]["InboundType"];
        //         Remarks = (string)table[0]["Remarks"];
        //        //string result = PostGRNDataToSAP(InboundId, PONumber, InvoiceNumber, InboundType, Remarks);
        //        string Result = PostGRNDataToSAP(InboundId, PONumber, InvoiceNumber, InboundType, Remarks);

        //        //if (result=="1")
        //        //{

        //        //}

        //        //string Query = "EXEC [dbo].[INB_CreateGRNAndConfirmStock] @MaterilaTransactionID = " + "'" + materialTransactionID + "'" + " , @InboundID = " + items.InboundID + " , @POHeaderID = " + items.POHeaderID + " ,@SupplierInvoiceID = " + items.SupplierInvoiceID + " ,@Remarks = " + "'" + items.Remarks + "'" + " , @CreatedBy = " + items.UserID + " , @IsShortSTO = " + 0 + " , @GRNUpdatedID = " + items.GRNHeaderID + "";
        //        //int New_GRNHeaderID = DbUtility.GetSqlN(Query, ConnectionString);
        //        if (Result!="")
        //        {
        //            response.Result = "1"; //"GRN Created successfully"
        //            string InbStatus = "Update INB_Inbound SET InboundStatusID = 6 WHERE InboundID = " + InboundId + "";
        //            DbUtility.GetSqlN(InbStatus, ConnectionString);


        //        }
        //        else
        //        {
        //            response.Result = "-1"; //"Error While Posting GRN"
        //        }
        //    }
        //    catch (SqlException Sqlex)
        //    {
        //        response.addError(Sqlex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        response.addError(ex.Message);
        //    }
        //    return response;
        //}
        public async Task<Payload<string>> CheckIsShortGRN(CheckIsShortGRNModel items)
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            Payload<string> response = new Payload<string>();
            try
            {
                if (items.flag == 0)
                {
                    var sqlParams = new Dictionary<string, object>
                    {
                          {"@InboundId",items.InboundId},
                          {"@PoNumber",items.PONumber}
                    };
                    var result = await DbUtility.GetjsonData(this.ConnectionString, "SP_CheckIsShortGRN", sqlParams).ConfigureAwait(false);
                    JArray jArray = JArray.Parse(result);
                    int n = (int)jArray[0]["Column1"];
                    if (n == 1)
                    {
                        response.Result = "-1";//This OBD is not recieved completely .Do you want to continue with Short GRN?
                    }
                    else if (n == 0)
                    {
                        response.ResponseCode = "-2";
                        //Payload<string> resultPayload = await GetGRNXMLData(items.InboundId, items.PONumber, items.InvoiceNumber, items.InboundType, items.Remarks);
                        //string qadresult = resultPayload.Result;
                        string qadresult = "1";
                        if (qadresult == "1")
                        {
                            response.Result = "1";//Success and call CreateGRNEntryAndPostDatatoSAP
                        }
                        else
                        {
                            //response.addError(resultPayload.Errors[0]);
                            response.Result = "2";//"Unexpected error from QAD..!";
                        }
                    }
                }
                else if (items.flag == 1)
                {
                    response.ResponseCode = "-2";
                    //Payload<string> resultPayload = await GetGRNXMLData(items.InboundId, items.PONumber, items.InvoiceNumber, items.InboundType, items.Remarks);
                    string qadresult = "1";
                    if (qadresult == "1")
                    {
                        response.Result = "1";//Success and call CreateGRNEntryAndPostDatatoSAP
                    }
                    else
                    {
                        response.Result = "2";//"Unexpected error from QAD..!";
                    }
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



        public async Task<Payload<string>> CreateGRNEntryAndPostDatatoSAP(CreateGRNEntryAndPostDatatoSAPModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();

                string strMaterialTransactionID = "";

                foreach (GRNDetails grn in items.GRNdetails)
                {
                    strMaterialTransactionID += grn.MaterialTransactionID + ",";
                }

                string CHECKQuery = "EXEC [dbo].[SP_CheckInboundReceivedStock]  @InboundID = " + items.InboundID + "";
                int check = DbUtility.GetSqlN(CHECKQuery, ConnectionString);

                if (check == 1)
                {
                    string result = "Success:";
                    if (result.Contains("SAP Error:"))
                    {
                        response.Result = "-1";
                        response.addError(result);
                    }
                   else  if (result.Contains("Error:"))
                    {
                        response.Result = "-1";
                        response.addError(result);
                    }

                    else if (result.Contains("Internal Server Error"))
                    {
                        response.Result = "-1";
                        throw new Exception(" Error while establishing the connection");
                    }
                    else if (result.Contains("Success:"))
                    {
                        string sapgrnno = "";
                        string[] Sapno = result.Split(":");

                        sapgrnno = Sapno[1];

                        string Query = "EXEC [dbo].[INB_CreateGRNAndConfirmStock] @MaterilaTransactionID = " + "'" + strMaterialTransactionID + "'" + " , @InboundID = " + items.InboundID + " , @POHeaderID = " + items.POHeaderID + " ,@SupplierInvoiceID = " + items.SupplierInvoiceID + " ,@Remarks = " + "'" + items.Remarks + "' ,  @SAPMIGONumber = '" + sapgrnno + "', @IsShortSTO = " + 0 + "";
                        int New_GRNHeaderID = DbUtility.GetSqlN(Query, ConnectionString);

                        int InwardTypeID = DbUtility.GetSqlN("Exec GetInwardType @InboundID=" + items.InboundID,this.ConnectionString);
                        if(InwardTypeID==2)
                        {
                            string AssignStockOBD = DbUtility.GetSqlS("Exec SP_AutoRelease_Cross_Inward @InboundID=" + items.InboundID+ ",@UserID="+items.UserID, this.ConnectionString);
                        }

                        response.Result = "1"; 

                    }
                    else
                    {
                        throw new Exception("Error while establishing the connection");
                    }
                }
                //}

                else if (check == -1)
                {
                    string Result = "Please receive all line items  in Inbound";
                    response.addError(Result);
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

        public async Task<string> PostDataToSAPAndConfirmStock(int InboundID, int UserID)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(1200);
                    string sapurl = this.ServiceURL;
                    string URL = $"{ServiceURL}/SAPIntegration/PostGRNDatatoSAP";
                    string urlParameters = $"?InboundId={Uri.EscapeDataString(InboundID.ToString())}&UserID={Uri.EscapeDataString(UserID.ToString())}";

                    HttpResponseMessage result = await client.PostAsync(URL + urlParameters, null);

                    if (result.IsSuccessStatusCode)
                    {
                       
                        string saprefNumber = await result.Content.ReadAsStringAsync();

                        return saprefNumber;
                    }
                    else
                    {
                       // result.ReasonPhrase = saprefNumber;
                        return result.ReasonPhrase;
                        //return $"SAP Connector Failed: { await result.Content.ReadAsStringAsync() }";
                    }
                }
            }
            catch (Exception ex)
            {
                return $"SAP Connector Failed: { ex.Message }";

            }
        }


        //public async Task<string> PostDataToSAPAndConfirmStock(int GRNHeaderID, int InboundID)
        //{
        //    try
        //    {
        //        using (HttpClient client = new HttpClient())
        //        {
        //            string sapurl = this.ServiceURL;
        //            string URL = $"{ServiceURL}/SAPIntegration/PostGRNDatatoSAP";


        //            string urlParameters = $"?InboundId={InboundID}&GRNHeaderID={GRNHeaderID}";


        //            HttpResponseMessage result = await client.PostAsync(URL + urlParameters, null);

        //            if (result.IsSuccessStatusCode)
        //            {
        //                string jsonString = await result.Content.ReadAsStringAsync();
        //                var item = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(jsonString);
        //                var test = item.GetProperty("result").GetString();
        //                var errorCode = item.GetProperty("errorcode").GetString();

        //                if (test == "success")
        //                {
        //                    return "Success";
        //                }
        //                else
        //                {
        //                    return "Failed"; 
        //                }
        //            }
        //            else
        //            {
        //                return "Connection Error"; 
        //            }
        //        }
        //    }
        //    catch (SqlException Sqlex)
        //    {

        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //    return ""; 
        //}




        //public string PostGRNDataToSAP(int InboundId, string PONumber, string InvoiceNumber, int InboundType, string Remarks)
        //{
        //    Payload<string> response = new Payload<string>();
        //    try
        //    {
        //        string ErrorCode = "";
        //        string urlParameters;
        //        string qadurl = this.APIURL;
        //        //string URL = "" + qadurl + "/Inbound/GetGRNXMLData";
        //        string URL = "" + " http://localhost/FWMSC21_GSK_API/ " + "/SAPIntegrationAPI/PostGRNDatatoSAP";
        //        urlParameters = "?InboundId=" + InboundId + "&&PONumber=" + PONumber + "&&InvoiceNumber=" + InvoiceNumber + "&&InboundType=" + InboundType + "&&Remarks=" + Remarks + "";
        //        var result = GetResponseFromSAP(URL, urlParameters);
        //        if (result.IsSuccessStatusCode)
        //        {
        //            var jsonString =  result.Content.ReadAsStringAsync();
        //            var item = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(jsonString.ToString());
        //            var test = item.GetProperty("result").GetString();
        //            var errorCode = item.GetProperty("errorcode").GetString();
        //            if (test == "success")
        //            {
        //                response.Result = "1";
        //                return "Success";
        //            }
        //            else
        //            {
        //                //response.addError(errorCode);
        //                //response.Result = "2";
        //                return "Failed"; //Error Code
        //            }
        //        }
        //        else
        //        {
        //            response.Result = "-3";
        //            return "Connection Error"; //Connection Error
        //        }

        //    }
        //    catch (SqlException Sqlex)
        //    {
        //        response.addError(Sqlex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        response.addError(ex.Message);
        //    }
        //    return "";
        //}


        //public async Task<Payload<string>> RTR_PrintLabels(RTR_PrintLabelModel printobj)
        //{
        //    Payload<string> response = new Payload<string>();
        //    DBFactory factory = new DBFactory();
        //    IDBUtility DbUtility = factory.getDBUtility();
        //    try
        //    {
        //        string ZPL = "";
        //        int Port = 0;
        //        await Task.Run(() =>
        //        {
        //            try
        //            {
        //                List<RTR_LabelPrint> lst = new List<RTR_LabelPrint>();
        //                lst = printobj.RTR_labelprint;
        //                Print_RTRMLabelModel Mlabel = new Print_RTRMLabelModel();

        //                for (var i = 0; i < printobj.RTR_labelprint.Count; i++)
        //                {
        //                    Mlabel.MCode = printobj.RTR_labelprint[i].MCode;
        //                    string Query = "SELECT MDescription AS S FROM MMT_MaterialMaster WHERE IsActive=1 AND IsDeleted=0 AND MCode='" + printobj.RTR_labelprint[i].MCode + "'";
        //                    string MDescription = DbUtility.GetSqlS(Query, ConnectionString);
        //                    Mlabel.Description = MDescription;
        //                    //Mlabel.SerialNo = printobj.RTR_labelprint[i].SerialNo;
        //                    Mlabel.BatchNo = printobj.RTR_labelprint[i].BatchNo;
        //                    Mlabel.MfgDate = printobj.RTR_labelprint[i].MfgDate == null || printobj.RTR_labelprint[i].MfgDate == "" ? "" : printobj.RTR_labelprint[i].MfgDate;
        //                    Mlabel.ExpDate = printobj.RTR_labelprint[i].ExpDate == null || printobj.RTR_labelprint[i].ExpDate == "" ? "" : printobj.RTR_labelprint[i].ExpDate;
        //                    Mlabel.ProjectNo = printobj.RTR_labelprint[i].ProjectRefNo;
        //                    Mlabel.Lineno = printobj.RTR_labelprint[i].LineNo == null || printobj.RTR_labelprint[i].LineNo == "" ? "" : printobj.RTR_labelprint[i].LineNo;
        //                    Mlabel.Mrp = printobj.RTR_labelprint[i].MRP == null || printobj.RTR_labelprint[i].MRP == "" ? "" : printobj.RTR_labelprint[i].MRP;
        //                    Mlabel.KitCode = printobj.RTR_labelprint[i].KitCode == null || printobj.RTR_labelprint[i].KitCode == "" ? "" : printobj.RTR_labelprint[i].KitCode;
        //                    //Mlabel.HUNo = printobj.RTR_labelprint[i].HUNo == null || printobj.RTR_labelprint[i].HUNo == "" ? "1" : printobj.RTR_labelprint[i].HUNo;
        //                    //Mlabel.HUSize = printobj.RTR_labelprint[i].HUSize == null || printobj.RTR_labelprint[i].HUSize == "" ? "1" : printobj.RTR_labelprint[i].HUSize;
        //                    Mlabel.SupplierLot = printobj.RTR_labelprint[i].SupplierLot;

        //                    string LabelQuery = "EXEC USP_INV_CS_IL_GetPrint @TenantBarcodeTypeID = " + printobj.LabelID + "";
        //                    var DS = DbUtility.GetDS(LabelQuery, this.ConnectionString);
        //                    string length = Convert.ToString(DS.Tables[0].Rows[0]["Length"]);
        //                    string width = Convert.ToString(DS.Tables[0].Rows[0]["Width"]);
        //                    string LabelType = Convert.ToString(DS.Tables[0].Rows[0]["LabelType"]);

        //                    Mlabel.Length = length;
        //                    Mlabel.Width = width;
        //                    Mlabel.LabelType = LabelType;

        //                    Mlabel.PrinterIP = "0";
        //                    Mlabel.IsBoxLabelReq = false;
        //                    Mlabel.Length = length;
        //                    Mlabel.Width = width;

        //                    Mlabel.Dpi = 203; //dpi;
        //                    Mlabel.PrintQty = printobj.RTR_labelprint[i].PrintQty;
        //                    Mlabel.LabelType = LabelType;

        //                    ZPL += PrintBarcodeLabel(Mlabel).Result.Result;
        //                }
        //                lst.Clear();
        //                var result = ZPL;

        //                string sp = "SELECT Port FROM GEN_ClientResource WHERE IsActive=1 and IsDeleted=0 and DeviceIP=@DeviceIP";
        //                using (SqlConnection connection = new SqlConnection(ConnectionString))
        //                {
        //                    connection.Open();
        //                    using (SqlCommand command = new SqlCommand(sp, connection))
        //                    {
        //                        command.Parameters.AddWithValue("@DeviceIP", printobj.ipaddress);
        //                        Port = (int)command.ExecuteScalar();
        //                    }
        //                }



        //                Helper.PrintHelper.PrintUsingIP(IPAddress, Port, ZPL);
        //                response.Result = result;
        //                return response;
        //            }
        //            catch (Exception ex)
        //            {
        //                throw new Exception(ex.Message);
        //            }
        //        });
        //        return response;
        //    }
        //    catch (SqlException Sqlex)
        //    {
        //        response.addError(Sqlex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        response.addError(ex.Message);
        //    }
        //    return response;
        //}


        //public async Task<Payload<string>> RTR_PrintLabels_Network(RTR_PrintLabelModel printobj)
        //{
        //    Payload<string> response = new Payload<string>();
        //    DBFactory factory = new DBFactory();
        //    IDBUtility DbUtility = factory.getDBUtility();
        //    try
        //    {
        //        string ZPL = "";
        //        int Port = 0;
        //        await Task.Run(() =>
        //        {
        //            try
        //            {
        //                List<RTR_LabelPrint> lst = new List<RTR_LabelPrint>();
        //                lst = printobj.RTR_labelprint;
        //                Print_RTRMLabelModel Mlabel = new Print_RTRMLabelModel();

        //                for (var i = 0; i < printobj.RTR_labelprint.Count; i++)
        //                {
        //                    Mlabel.MCode = printobj.RTR_labelprint[i].MCode;
        //                    //string Query = "SELECT MDescription AS S FROM MMT_MaterialMaster WHERE IsActive=1 AND IsDeleted=0 AND MCode='" + printobj.RTR_labelprint[i].MCode + "'";
        //                    //string MDescription = DbUtility.GetSqlS(Query, ConnectionString);
        //                    Mlabel.Description = printobj.RTR_labelprint[i].Description;
        //                    //Mlabel.SerialNo = printobj.RTR_labelprint[i].SerialNo;
        //                    Mlabel.BatchNo = printobj.RTR_labelprint[i].BatchNo;
        //                    Mlabel.MfgDate = printobj.RTR_labelprint[i].MfgDate == null || printobj.RTR_labelprint[i].MfgDate == "" ? "" : printobj.RTR_labelprint[i].MfgDate;
        //                    Mlabel.ExpDate = printobj.RTR_labelprint[i].ExpDate == null || printobj.RTR_labelprint[i].ExpDate == "" ? "" : printobj.RTR_labelprint[i].ExpDate;
        //                    Mlabel.ProjectNo = printobj.RTR_labelprint[i].ProjectRefNo;
        //                    Mlabel.Lineno = printobj.RTR_labelprint[i].LineNo == null || printobj.RTR_labelprint[i].LineNo == "" ? "" : printobj.RTR_labelprint[i].LineNo;
        //                    Mlabel.Mrp = printobj.RTR_labelprint[i].MRP == null || printobj.RTR_labelprint[i].MRP == "" ? "" : printobj.RTR_labelprint[i].MRP;
        //                    Mlabel.KitCode = printobj.RTR_labelprint[i].KitCode == null || printobj.RTR_labelprint[i].KitCode == "" ? "" : printobj.RTR_labelprint[i].KitCode;
        //                    //Mlabel.HUNo = printobj.RTR_labelprint[i].HUNo == null || printobj.RTR_labelprint[i].HUNo == "" ? "1" : printobj.RTR_labelprint[i].HUNo;
        //                    //Mlabel.HUSize = printobj.RTR_labelprint[i].HUSize == null || printobj.RTR_labelprint[i].HUSize == "" ? "1" : printobj.RTR_labelprint[i].HUSize;
        //                    Mlabel.SupplierLot = printobj.RTR_labelprint[i].SupplierLot;

        //                    string LabelQuery = "EXEC USP_INV_CS_IL_GetPrint @TenantBarcodeTypeID = " + printobj.LabelID + "";
        //                    var DS = DbUtility.GetDS(LabelQuery, this.ConnectionString);
        //                    string length = Convert.ToString(DS.Tables[0].Rows[0]["Length"]);
        //                    string width = Convert.ToString(DS.Tables[0].Rows[0]["Width"]);
        //                    string LabelType = Convert.ToString(DS.Tables[0].Rows[0]["LabelType"]);

        //                    Mlabel.Length = length;
        //                    Mlabel.Width = width;
        //                    Mlabel.LabelType = LabelType;

        //                    Mlabel.PrinterIP = "0";
        //                    Mlabel.IsBoxLabelReq = false;
        //                    Mlabel.Length = length;
        //                    Mlabel.Width = width;

        //                    Mlabel.Dpi = 203; //dpi;
        //                    Mlabel.PrintQty = printobj.RTR_labelprint[i].PrintQty;
        //                    Mlabel.LabelType = LabelType;

        //                    ZPL += PrintBarcodeLabel(Mlabel).Result.Result;
        //                }
        //                lst.Clear();
        //                var result = ZPL;

        //                if (printobj.PrinterType == 1 || printobj.PrinterType == 0)
        //                {
        //                    Helper.PrintHelper.PrintUsingIP(this.ipaddress, this.port, ZPL);
        //                }
        //                else if (printobj.PrinterType == 2)
        //                {

        //                    string sp = "SELECT Port FROM GEN_ClientResource WHERE IsActive=1 and IsDeleted=0 and DeviceIP=@DeviceIP";
        //                    using (SqlConnection connection = new SqlConnection(ConnectionString))
        //                    {
        //                        connection.Open();
        //                        using (SqlCommand command = new SqlCommand(sp, connection))
        //                        {
        //                            command.Parameters.AddWithValue("@DeviceIP", printobj.ipaddress);
        //                            Port = (int)command.ExecuteScalar();
        //                        }
        //                    }

        //                    Helper.PrintHelper.PrintUsingIP(printobj.ipaddress, Port, ZPL);
        //                }
        //                else
        //                {
        //                    Helper.PrintHelper.PrintUsingIP(printobj.ipaddress, printobj.port, ZPL);
        //                }

        //                response.Result = result;
        //                return response;
        //            }
        //            catch (Exception ex)
        //            {
        //                throw new Exception(ex.Message);
        //            }
        //        });
        //        return response;
        //    }
        //    catch (SqlException Sqlex)
        //    {
        //        response.addError(Sqlex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        response.addError(ex.Message);
        //    }
        //    return response;
        //}



        //public async Task<Payload<string>> RTR_PrintLabels(RTR_PrintLabelModel printobj)
        //{
        //    Payload<string> response = new Payload<string>();
        //    DBFactory factory = new DBFactory();
        //    IDBUtility DbUtility = factory.getDBUtility();
        //    try
        //    {
        //        string ZPL = "";
        //        int Port = 0;
        //        await Task.Run(() =>
        //        {
        //            try
        //            {

        //                List<RTR_LabelPrint> lst = new List<RTR_LabelPrint>();
        //                lst = printobj.RTR_labelprint;
        //                Print_RTRMLabelModel Mlabel = new Print_RTRMLabelModel();


        //                for (var i = 0; i < printobj.RTR_labelprint.Count; i++)
        //                {
        //                    string Serial_Nos = "exec SP_Item_print @mcode='" + printobj.RTR_labelprint[i].MCode + "',@count=" + printobj.RTR_labelprint[i].PrintQty + "";
        //                    var ds = DbUtility.GetDS(Serial_Nos, this.ConnectionString);

        //                    for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
        //                    {

        //                        Mlabel.MCode = printobj.RTR_labelprint[i].MCode;
        //                        string Query = "SELECT MDescription AS S FROM MMT_MaterialMaster WHERE IsActive=1 AND IsDeleted=0 AND MCode='" + printobj.RTR_labelprint[i].MCode + "'";
        //                        string MDescription = DbUtility.GetSqlS(Query, ConnectionString);
        //                        Mlabel.Description = MDescription;
        //                        Mlabel.SerialNo = ds.Tables[0].Rows[j]["BoxSerialNo"].ToString();
        //                        Mlabel.BoxSerialNo = ds.Tables[0].Rows[j]["BoxSerialNo"].ToString();
        //                        Mlabel.BatchNo = printobj.RTR_labelprint[i].BatchNo;
        //                        Mlabel.MfgDate = printobj.RTR_labelprint[i].MfgDate == null || printobj.RTR_labelprint[i].MfgDate == "" ? "" : printobj.RTR_labelprint[i].MfgDate;
        //                        //DateTime currentDate = DateTime.Now;
        //                        //string formattedDate = currentDate.ToString("yyyy-MM-dd");

        //                        //Mlabel.MfgDate = printobj.RTR_labelprint[i].MfgDate == null || printobj.RTR_labelprint[i].MfgDate == "" ? formattedDate : printobj.RTR_labelprint[i].MfgDate; //added by Chinni.K on 11/01/24


        //                        Mlabel.ExpDate = printobj.RTR_labelprint[i].ExpDate == null || printobj.RTR_labelprint[i].ExpDate == "" ? "" : printobj.RTR_labelprint[i].ExpDate;
        //                        Mlabel.ProjectNo = printobj.RTR_labelprint[i].ProjectRefNo;
        //                        Mlabel.Lineno = printobj.RTR_labelprint[i].LineNo == null || printobj.RTR_labelprint[i].LineNo == "" ? "" : printobj.RTR_labelprint[i].LineNo;

        //                        Mlabel.Grade = printobj.RTR_labelprint[i].Grade;
        //                        //Mlabel.Mrp = printobj.RTR_labelprint[i].MRP == null || printobj.RTR_labelprint[i].MRP == "" ? "" : printobj.RTR_labelprint[i].MRP;
        //                        Mlabel.KitCode = printobj.RTR_labelprint[i].KitCode == null || printobj.RTR_labelprint[i].KitCode == "" ? "" : printobj.RTR_labelprint[i].KitCode;
        //                        //Mlabel.HUNo = printobj.RTR_labelprint[i].HUNo == null || printobj.RTR_labelprint[i].HUNo == "" ? "1" : printobj.RTR_labelprint[i].HUNo;
        //                        //Mlabel.HUSize = printobj.RTR_labelprint[i].HUSize == null || printobj.RTR_labelprint[i].HUSize == "" ? "1" : printobj.RTR_labelprint[i].HUSize;
        //                        Mlabel.SupplierLot = printobj.RTR_labelprint[i].SupplierLot;
        //                        Mlabel.WarehouseID = printobj.WarehouseID;
        //                        //Mlabel.Grade = printobj.RTR_labelprint[i].Grade;



        //                        string LabelQuery = "EXEC USP_INV_CS_IL_GetPrint @TenantBarcodeTypeID = " + printobj.LabelID + "";
        //                        var DS = DbUtility.GetDS(LabelQuery, this.ConnectionString);
        //                        string length = Convert.ToString(DS.Tables[0].Rows[0]["Length"]);
        //                        string width = Convert.ToString(DS.Tables[0].Rows[0]["Width"]);
        //                        string LabelType = Convert.ToString(DS.Tables[0].Rows[0]["LabelType"]);

        //                        Mlabel.Length = length;
        //                        Mlabel.Width = width;
        //                        Mlabel.LabelType = LabelType;

        //                        Mlabel.PrinterIP = "0";
        //                        Mlabel.IsBoxLabelReq = false;
        //                        Mlabel.Length = length;
        //                        Mlabel.Width = width;

        //                        Mlabel.Dpi = 203; //dpi;
        //                        Mlabel.PrintQty = "1";
        //                        Mlabel.LabelType = LabelType;

        //                        ZPL += PrintBarcodeLabel(Mlabel).Result.Result;
        //                    }
        //                    lst.Clear();
        //                    var result = ZPL;
        //                    if (printobj.PrinterType == 1 || printobj.PrinterType == 0)
        //                    {
        //                        Helper.PrintHelper.PrintUsingIP(this.ipaddress, this.port, ZPL);
        //                    }
        //                    else if (printobj.PrinterType == 2)
        //                    {
        //                        string sp = "SELECT Port FROM GEN_ClientResource WHERE IsActive=1 and IsDeleted=0 and DeviceIP=@DeviceIP";
        //                        using (SqlConnection connection = new SqlConnection(ConnectionString))
        //                        {
        //                            connection.Open();
        //                            using (SqlCommand command = new SqlCommand(sp, connection))
        //                            {
        //                                command.Parameters.AddWithValue("@DeviceIP", printobj.ipaddress);
        //                                Port = (int)command.ExecuteScalar();
        //                            }
        //                        }
        //                        Helper.PrintHelper.PrintUsingIP(printobj.ipaddress, Port, ZPL);
        //                    }
        //                    else
        //                    {
        //                        //string IPAddress = "0";
        //                        // int Port = 9100;

        //                        Helper.PrintHelper.PrintUsingIP(printobj.ipaddress, printobj.port, ZPL);
        //                    }
        //                }
        //                response.Result = ZPL;
        //                return response;
        //            }
        //            catch (Exception ex)
        //            {
        //                throw new Exception(ex.Message);
        //            }
        //       });
        //       return response;
        //    }
        //    catch (SqlException Sqlex)
        //    {
        //        response.addError(Sqlex.Message);
        //    }
        //    catch (Exception ex)
        //    {
        //        response.addError(ex.Message);
        //    }
        //    return response;
        //}



        public async Task<Payload<string>> RTR_PrintLabels(RTR_PrintLabelModel printobj)
        {
            Payload<string> response = new Payload<string>();
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                string ZPL = "";
                int Port = 0;
                await Task.Run(() =>
                {
                    try
                    {
                        List<RTR_LabelPrint> lst = new List<RTR_LabelPrint>();
                        lst = printobj.RTR_labelprint;
                        Print_RTRMLabelModel Mlabel = new Print_RTRMLabelModel();

                        for (var i = 0; i < printobj.RTR_labelprint.Count; i++)
                        {

                            string Serial_Nos = "exec SP_Item_print @mcode='" + printobj.RTR_labelprint[i].MCode + "',@count=" + printobj.RTR_labelprint[i].PrintQty + "";
                            var ds = DbUtility.GetDS(Serial_Nos, this.ConnectionString);

                            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                            {
                                Mlabel.SerialNo = ds.Tables[0].Rows[j]["BoxSerialNo"].ToString();
                                Mlabel.BoxSerialNo = ds.Tables[0].Rows[j]["BoxSerialNo"].ToString();
                                Mlabel.MCode = printobj.RTR_labelprint[i].MCode;
                                string Query = "SELECT MDescription AS S FROM MMT_MaterialMaster WHERE IsActive=1 AND IsDeleted=0 AND MCode='" + printobj.RTR_labelprint[i].MCode + "'";
                                string MDescription = DbUtility.GetSqlS(Query, ConnectionString);
                                Mlabel.Description = MDescription;

                                Mlabel.BatchNo = printobj.RTR_labelprint[i].BatchNo;
                                Mlabel.MfgDate = printobj.RTR_labelprint[i].MfgDate == null || printobj.RTR_labelprint[i].MfgDate == "" ? "" : printobj.RTR_labelprint[i].MfgDate;
                                //DateTime currentDate = DateTime.Now;
                                //string formattedDate = currentDate.ToString("yyyy-MM-dd");

                                //Mlabel.MfgDate = printobj.RTR_labelprint[i].MfgDate == null || printobj.RTR_labelprint[i].MfgDate == "" ? formattedDate : printobj.RTR_labelprint[i].MfgDate; //added by Chinni.K on 11/01/24


                                Mlabel.ExpDate = printobj.RTR_labelprint[i].ExpDate == null || printobj.RTR_labelprint[i].ExpDate == "" ? "" : printobj.RTR_labelprint[i].ExpDate;
                                Mlabel.ProjectNo = printobj.RTR_labelprint[i].ProjectRefNo;
                                Mlabel.Lineno = printobj.RTR_labelprint[i].LineNo == null || printobj.RTR_labelprint[i].LineNo == "" ? "" : printobj.RTR_labelprint[i].LineNo;

                                Mlabel.Grade = printobj.RTR_labelprint[i].Grade;
                                //Mlabel.Mrp = printobj.RTR_labelprint[i].MRP == null || printobj.RTR_labelprint[i].MRP == "" ? "" : printobj.RTR_labelprint[i].MRP;
                                Mlabel.KitCode = printobj.RTR_labelprint[i].KitCode == null || printobj.RTR_labelprint[i].KitCode == "" ? "" : printobj.RTR_labelprint[i].KitCode;
                                //Mlabel.HUNo = printobj.RTR_labelprint[i].HUNo == null || printobj.RTR_labelprint[i].HUNo == "" ? "1" : printobj.RTR_labelprint[i].HUNo;
                                //Mlabel.HUSize = printobj.RTR_labelprint[i].HUSize == null || printobj.RTR_labelprint[i].HUSize == "" ? "1" : printobj.RTR_labelprint[i].HUSize;
                                Mlabel.SupplierLot = printobj.RTR_labelprint[i].SupplierLot;
                                Mlabel.WarehouseID = printobj.WarehouseID;
                                //Mlabel.Grade = printobj.RTR_labelprint[i].Grade;


                                string LabelQuery = "EXEC USP_INV_CS_IL_GetPrint @TenantBarcodeTypeID = " + printobj.LabelID + "";
                                var DS = DbUtility.GetDS(LabelQuery, this.ConnectionString);
                                string length = Convert.ToString(DS.Tables[0].Rows[0]["Length"]);
                                string width = Convert.ToString(DS.Tables[0].Rows[0]["Width"]);
                                string LabelType = Convert.ToString(DS.Tables[0].Rows[0]["LabelType"]);

                                Mlabel.Length = length;
                                Mlabel.Width = width;
                                Mlabel.LabelType = LabelType;

                                Mlabel.PrinterIP = "0";
                                Mlabel.IsBoxLabelReq = false;
                                Mlabel.Length = length;
                                Mlabel.Width = width;

                                Mlabel.Dpi = 203; //dpi;
                                Mlabel.PrintQty = printobj.RTR_labelprint[i].PrintQty;
                                Mlabel.LabelType = LabelType;
                                Mlabel.DesignName = printobj.RtrSecondLabelPrint.DesignName;
                                Mlabel.Matt = printobj.RtrSecondLabelPrint.Series;
                                Mlabel.BoxQty = printobj.RtrSecondLabelPrint.BoxQty;
                                Mlabel.Rectified = printobj.RtrSecondLabelPrint.Rectified;
                                Mlabel.UnPolished = printobj.RtrSecondLabelPrint.UnPolished;
                                Mlabel.Glazed = printobj.RtrSecondLabelPrint.Glazed;
                                Mlabel.ShiftTime = printobj.RtrSecondLabelPrint.Shift;
                                Mlabel.LineNo = printobj.RtrSecondLabelPrint.LineNumber;
                                Mlabel.SorterId = printobj.RtrSecondLabelPrint.SorterID;
                                Mlabel.Wapis = printobj.RtrSecondLabelPrint.WAPIS;
                                Mlabel.Size = printobj.RTR_labelprint[i].Size;
                                Mlabel.IsSecondaryLabelprint = printobj.IsSecondaryLabelprint;//Added By Ramsai
                                ZPL += PrintBarcodeLabel(Mlabel).Result.Result;
                            }
                            
                        }
                        lst.Clear();
                        var result = ZPL;

                        if (printobj.PrinterType == 1 || printobj.PrinterType == 0)
                        {
                            Helper.PrintHelper.PrintUsingIP(this.ipaddress, this.port, ZPL);
                        }
                        else if (printobj.PrinterType == 2)
                        {

                            string sp = "SELECT Port FROM GEN_ClientResource WHERE IsActive=1 and IsDeleted=0 and DeviceIP=@DeviceIP";
                            using (SqlConnection connection = new SqlConnection(ConnectionString))
                            {
                                connection.Open();
                                using (SqlCommand command = new SqlCommand(sp, connection))
                                {
                                    command.Parameters.AddWithValue("@DeviceIP", printobj.ipaddress);
                                    Port = (int)command.ExecuteScalar();
                                }
                            }

                            Helper.PrintHelper.PrintUsingIP(printobj.ipaddress, Port, ZPL);
                        }
                        else if (printobj.PrinterType == 3)
                        {
                            Helper.PrintHelper.PrintUsingIP(printobj.ipaddress, printobj.port, ZPL);
                        }

                        response.Result = result;
                        return response;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                });
                return response;
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


        public async Task<Payload<string>> PrintBarcodeLabel(Print_RTRMLabelModel Mlabel)
        {
            Payload<string> response = new Payload<string>();
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                ZPL zplString = new ZPL();
                string barcodestring = null;

                Mlabel.HUSize = Mlabel.HUSize == "" || Mlabel.HUSize == null || Mlabel.HUSize == "0" ? "1" : Mlabel.HUSize;
                Mlabel.HUNo = Mlabel.HUNo == "" || Mlabel.HUNo == null || Mlabel.HUNo == "0" ? "1" : Mlabel.HUNo;

                barcodestring = Mlabel.MCode + "|" + Mlabel.BatchNo + "|" + Mlabel.BoxSerialNo + "|" + String.Format("{0:dd-MM-yy}", Mlabel.MfgDate) + "|" + String.Format("{0:dd-MM-yy}", Mlabel.ExpDate) + "|" + Mlabel.ProjectNo + "|" + Mlabel.KitCode.ToString() + "|" + Mlabel.Grade + "|" + Mlabel.Lineno + "|" + Mlabel.HUNo + "|" + Mlabel.HUSize;

                string sp = "Exec Get_Zonebymcode_print @Mcode = '" + Mlabel.MCode + "' , @WarehouseID= "+Mlabel.WarehouseID+"";
                var DS = DbUtility.GetDS(sp, ConnectionString);

                if (DS.Tables[0].Rows.Count > 0)
                {
                    Mlabel.Zone = DS.Tables[0].Rows[0]["Zone"].ToString();
                }
                else
                {
                    Mlabel.Zone = "";
                }

                //var query = "EXEC [dbo].[sp_GetZPLString] @Dpi=" + Mlabel.Dpi + " , @Length=" + Mlabel.Length + ", @Width = " + Mlabel.Width + ", @LabelType = " + "'" + Mlabel.LabelType + "'";
                var query = "EXEC [dbo].[sp_GetZPLString] @Dpi=" + Mlabel.Dpi + " , @Length=" + Mlabel.Length + ", @Width = " + Mlabel.Width + ", @LabelType = " + "'" + Mlabel.LabelType + "',@IsSingleLabel=" + Mlabel.IsSecondaryLabelprint + "";//Added By Ramsai
                string result = DbUtility.GetSqlS(query, ConnectionString);
                if (result != "" && result != null)
                {
                    result = result.Replace("barcodegeneratorcodewithmfgandexp", barcodestring);
                    result = result.Replace("@SKU", "" + " " + Mlabel.MCode);
                    result = result.Replace("@Desc.", "" + " " + Mlabel.Description);

                    if (Mlabel.BatchNo != "")
                    {
                        result = result.Replace("@BatchNo", "" + " " + Mlabel.BatchNo);
                    }
                    else
                    {
                        result = result.Replace("@BatchNo", "" + "" + "");
                    }
                    if (Mlabel.MfgDate.ToString() != "")
                    {
                        result = result.Replace("@Mfg.Date", "" + " " + Convert.ToDateTime(Mlabel.MfgDate).ToString("dd.MM.yyyy"));
                        //result = result.Replace("@Mfg.Date", "" + " " + String.Format("{0:dd.MMM.yyyy}", Mlabel.MfgDate));
                    }
                    else
                    {
                        result = result.Replace("@Mfg.Date", "" + "" + "");
                    }
                    if (Mlabel.ExpDate.ToString() != "")
                    {
                        result = result.Replace("@Exp.Date", "" + " " + String.Format("{0:dd-MM-yy}", Mlabel.ExpDate));
                    }
                    else
                    {
                        result = result.Replace("@Exp.Date", "" + "" + "");
                    }
                    if (Mlabel.BoxSerialNo != "")
                    {
                        result = result.Replace("@Serial No.", "" + " " + Mlabel.BoxSerialNo);
                    }
                    else
                    {
                        result = result.Replace("@Serial No.", "" + " " + "");
                    }
                    if (Mlabel.ProjectNo != "")
                    {
                        result = result.Replace("@Project Ref No.", "" + "" + Mlabel.ProjectNo);
                    }
                    else
                    {
                        result = result.Replace("@Project Ref No.", "" + "" + "");
                    }
                    if (Mlabel.Size != "")
                    {
                        result = result.Replace("@Size", "Size : " + " " + Mlabel.Size);
                    }
                    else
                    {
                        result = result.Replace("@Size", "" + "" + "");
                    }
                    if (Mlabel.Zone == "" || Mlabel.Zone == null)
                    {
                        result = result.Replace("@Zone", "");//" Zone # :" + "" +
                        result = result.Replace("^GB170,85,5^FS", "");
                    }
                    else
                    {
                        result = result.Replace("@Zone", Mlabel.Zone);// "Zone # :" + "" + 
                    }
                    if (Mlabel.Grade != "")
                    {
                        result = result.Replace("@Grade", "" + " - " + Mlabel.Grade);
                    }
                    else
                    {
                        result = result.Replace("@Grade", "" + "" + "");
                    }
                    if (Mlabel.KitPlannerID != 0)
                    {
                        result = result.Replace("@Kit Id", "Kit ID :" + "" + Mlabel.KitPlannerID.ToString());
                    }
                    else
                    {
                        result = result.Replace("@Kit Id", "Kit ID  :" + "" + "");
                    }
                    if (Mlabel.GRNDate != Convert.ToString(DateTime.MinValue))
                    {
                        if (!string.IsNullOrEmpty(Mlabel.GRNDate))
                        {
                            result = result.Replace("@GRN date", "GRN Date: " + Mlabel.GRNDate);
                        }
                        else
                        {
                            result = result.Replace("@GRN date", "GRN Date: ");
                        }
                    }
                    else
                    {
                        result = result.Replace("@GRN date", "GRN Date: ");
                    }
                    if (Mlabel.Location != "")
                    {
                        result = result.Replace("@Location", "Location : " + Mlabel.Location);
                    }
                    else
                    {
                        result = result.Replace("@Location", "Location : " + "" + "");
                    }

                    if (Mlabel.HUSize != "")
                    {
                        result = result.Replace("@HUSize", "HU : " + Mlabel.HUNo + "/" + Mlabel.HUSize);
                    }
                    else
                    {
                        result = result.Replace("@HUSize", "HU : " + "" + "");
                    }

                    if (Mlabel.SupplierLot != "")
                    {
                        result = result.Replace("@Supplier Lot", "SupplierLot : " + Mlabel.SupplierLot);
                    }
                    else
                    {
                        result = result.Replace("@Supplier Lot", "SupplierLot : " + "" + "");
                    }
                    if (Mlabel.DesignName != "")
                    {
                        result = result.Replace("@DesignName", " " + Mlabel.DesignName);
                    }
                    else
                    {
                        result = result.Replace("@DesignName", "" + "" + "");
                    }
                    if (Mlabel.Matt != "")
                    {
                        result = result.Replace("@Matt", "" + Mlabel.Matt);
                    }
                    else
                    {
                        result = result.Replace("@Matt", "" + "" + "");
                    }
                    if (Mlabel.BoxQty != "")
                    {
                        result = result.Replace("@Qty.Box", "" + Mlabel.BoxQty);
                    }
                    else
                    {
                        result = result.Replace("@Qty.Box", "" + "" + "");
                    }
                    if (Mlabel.LineNo != "")
                    {
                        result = result.Replace("@LineNo", "" + Mlabel.LineNo);
                    }
                    else
                    {
                        result = result.Replace("@LineNo", "" + "" + "");
                    }
                    if (Mlabel.ShiftTime != "")
                    {
                        result = result.Replace("@Shift", "" + Mlabel.ShiftTime);
                    }
                    else
                    {
                        result = result.Replace("@Shift", "" + "" + "");
                    }
                    if (Mlabel.SorterId != "")
                    {
                        result = result.Replace("@SorterId", "SORT - " + Mlabel.SorterId);
                    }
                    else
                    {
                        result = result.Replace("@SorterId", "" + "" + "");
                    }
                    if (Mlabel.Wapis != "")
                    {
                        result = result.Replace("@Wapis", " " + Mlabel.Wapis);
                    }
                    else
                    {
                        result = result.Replace("@Wapis", "" + "" + "");
                    }
                    if (Mlabel.Glazed != "")
                    {
                        result = result.Replace("@Gl", "" + Mlabel.Glazed);
                    }
                    else
                    {
                        result = result.Replace("@Gl", "" + "" + "");
                    }
                    if (Mlabel.UnPolished != "")
                    {
                        result = result.Replace("@Up", "" + Mlabel.UnPolished);
                    }
                    else
                    {
                        result = result.Replace("@Up", "" + "" + "");
                    }
                    if (Mlabel.Rectified != "")
                    {
                        result = result.Replace("@R", "" + Mlabel.Rectified);
                    }
                    else
                    {
                        result = result.Replace("@R", "" + "" + "");
                    }

                }
                if (Mlabel.IsBoxLabelReq != true)
                {
                    result = result.Replace("@NoofLabels", Mlabel.PrintQty);
                    Mlabel.Duplicateprints = "0";
                    result = result.Replace("@DuplicatePrints", Mlabel.Duplicateprints);
                    response.Result = result;
                    return response;
                }
                else
                {
                    result = result.Replace("@NoofLabels", (Convert.ToInt16(Mlabel.PrintQty) + 1).ToString());
                    Mlabel.Duplicateprints = "1";
                    result = result.Replace("@DuplicatePrints", Mlabel.Duplicateprints);
                    response.Result = result;
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








        public GRNDetails ParseSoapResponse(string response, string responseType)
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(response);  //loading soap message as string
            XmlNamespaceManager manager = new XmlNamespaceManager(document.NameTable);
            string Status = "";
            manager.AddNamespace("d", "http://schemas.xmlsoap.org/soap/envelope/");
            manager.AddNamespace("bhr", "urn:schemas-qad-com:xml-services");

            XmlNodeList xnList = document.SelectNodes("//bhr:" + responseType, manager);
            int nodes = xnList.Count;
            GRNDetails gRNDetails = new GRNDetails();
            if (nodes == 0)
            {
                gRNDetails.result = "Error";
                gRNDetails.errorcode = "No Response from QAD!";
            }
            else
            {
                foreach (XmlNode xn in xnList)
                {
                    Status = xn["ns1:result"].InnerText;
                    gRNDetails.result = Status;
                    if (Status != "success")
                    {
                        //gRNDetails.ErrorCode = xn["ns3:tt_msg_data"] == null ? "" : xn["ns3:tt_msg_data"].InnerText + " " + xn["ns3:tt_msg_desc"] == null ? "" : xn["ns3:tt_msg_desc"].InnerText + " " + xn["ns3:tt_msg_context"] == null ? "" : xn["ns3:tt_msg_context"].InnerText;
                        //gRNDetails.ErrorCode = xn.InnerText;
                        gRNDetails.errorcode = "QAD Error" + xn["ns3:dsExceptions"]?["ns3:temp_err_msg"]?["ns3:tt_msg_desc"] == null ? xn.InnerText : xn["ns3:dsExceptions"]?["ns3:temp_err_msg"]?["ns3:tt_msg_desc"]?.InnerText;
                        gRNDetails.result = gRNDetails.errorcode?.Length > 0 && gRNDetails.errorcode.Contains("WARNING: Shipper not printed.") ? "success" : gRNDetails.result;

                    }
                }
            }
            return gRNDetails;
        }
       
    }
}
