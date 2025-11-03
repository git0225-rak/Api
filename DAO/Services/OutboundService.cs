using Simpolo_Endpoint.DAO.interfaces;
using Simpolo_Endpoint.DBUtil;
using Simpolo_Endpoint.Models;
using Simpolo_Endpoint.ModelsLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Nancy.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Simpolo_Endpoint.DTO;

namespace Simpolo_Endpoint.DAO.Services
{
    public class OutboundService : AppDBService, IOutbound
    {

        private readonly EmailService _emailService;
        private readonly ISAPJsonPostService _jsonPostService;
        private readonly WhatsAppService _whatsappservice;
        public OutboundService(IOptions<AppSettings> appSettings, EmailService emailService, ISAPJsonPostService jsonPostService, WhatsAppService whatsappservice) : base(appSettings)
        {
            _emailService = emailService;
            _jsonPostService = jsonPostService;
            _whatsappservice = whatsappservice;
        }


        public async Task<Payload<string>> GetPendingOBDForVLPDCreation(GetPendingOBDForVLPDCreationModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    { "@OBDNumber", items.OBDNumber },
                    { "@WarehouseIDs", items.WarehouseId },
                    { "@Tenant", items.Tenant },
                    { "@AccountID_New", items.AccountID },
                    { "@UserTypeID_New", items.UserID },
                    { "@TenantID_New", items.TenantID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "sp_OBD_GetVLPDPendingOBDList", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetPick_CheckPendingList(GetPick_CheckPendingListModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    { "@OBDNumber", items.OBDNumber },
                    { "@WarehouseIDs", items.WarehouseId },
                    { "@Tenant", items.Tenant },
                    { "@AccountID_New", items.AccountID },
                    { "@UserTypeID_New", items.UserID },
                    { "@TenantID", items.TenantID },
                    { "@TenantID_New", items.TenantID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "sp_OBD_GetDIPList", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetPGIPendingList(GetPGIPendingListModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    { "@OBDNumber", items.OBDNumber },
                    { "@WarehouseIDs", items.WarehouseId },
                    { "@Tenant", items.Tenant },
                    { "@AccountID_New", items.AccountID },
                    { "@UserTypeID_New", items.UserID },
                    { "@TenantID", items.TenantID },
                    { "@TenantID_New", items.TenantID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "sp_OBD_GetPGIPendingList", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetDeliveriesPendingList(GetDeliveriesPendingListModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    { "@OBDNumber", items.OBDNumber },
                    { "@WarehouseIDs", items.WarehouseId },
                    { "@Tenant", items.Tenant },
                    { "@AccountID_New", items.AccountID },
                    { "@UserTypeID_New", items.UserID },
                    { "@TenantID_New", items.TenantID },
                    { "@TenantID", items.TenantID },
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "sp_OBD_GetDeliveriesPendingList", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetPODPendingList(GetPODPendingListModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    { "@OBDNumber", items.OBDNumber },
                    { "@WarehouseIDs", items.WarehouseId },
                    { "@Tenant", items.Tenant },
                    { "@AccountID_New", items.AccountID },
                    { "@UserTypeID_New", items.UserID },
                    { "@TenantID_New", items.TenantID },
                    { "@TenantID", items.TenantID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "sp_OBD_GetPODPendingList", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetOBDRevertList(GetOBDRevertListModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    { "@TenantId", items.TenantID },
                    { "@WareHouseId", items.WarehouseId },
                    { "@NoofRecords", items.NoofRecords },
                    { "@PageNo", items.PageNo },
                    { "@StatusID", items.StatusId },
                    { "@FromDate", items.FromDate },
                    { "@ToDate", items.ToDate },
                    { "@CategoryID", items.CategoryID },
                    { "@SearchText", items.SearchText }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "USP_OBD_TRN_GetOBDRevertList", sqlParams).ConfigureAwait(false);
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
        //RElease Outbound Get
        public async Task<Payload<string>> GetOBDReleaseList(GetOBDReleaseListModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    { "@OBDNumber", items.OBDNumber },
                    { "@AccountID_New", items.AccountID_New },
                    { "@TenantID_New", items.TenantID},
                    { "@UserID_New", items.UserID_New },
                    { "@UserTypeID_New", items.UserTypeID_New },
                    { "@PageIndex", items.PageIndex },
                    { "@PageSize", items.PageSize },
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "sp_GET_OPENOBDSLIST", sqlParams).ConfigureAwait(false);
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
        //ReleaseOBDItems  - Release OBD
        public async Task<Payload<string>> saveBulkReleaseItemsForOBD(saveBulkReleaseItemsForOBDModel items)
        {
            Payload<string> response = new Payload<string>();
            var Data = "";
            int wareHouseid = GetWarehouseIDByOutbound(items.OutboundID);
            try
            {
                if (wareHouseid > 0)
                {
                    DBFactory factory = new DBFactory();
                    IDBUtility DbUtility = factory.getDBUtility();
                    string finalxml = "";
                    XmlSerializer serializer = new XmlSerializer(typeof(List<data>), new XmlRootAttribute("root"));
                    using (var stream = new StringWriter())
                    {
                        serializer.Serialize(stream, items.items);
                        string xml = stream.ToString();
                        finalxml = xml.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");
                        finalxml = finalxml.Replace("xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", "");
                    }
                    StringBuilder sb = new StringBuilder();
                    sb.Append(" Exec [dbo].[Sp_OBD_UpsertPickingSuggestions] ");
                    sb.Append(" @DataXML='" + (finalxml) + "'");
                    sb.Append(" ,@OutboundID=" + items.OutboundID);
                    sb.Append(" ,@UserID=" + items.UserID);
                    sb.Append(" ,@DockID=" + items.DockID);
                    string XML = sb.ToString();
                    Data = await DbUtility.GetjsonDataFromDatasetINQ(this.ConnectionString, XML);
                    JObject data = JObject.Parse(Data);
                    JArray table = (JArray)data["Table"];
                    int AssgnResult = (int)table[0]["Status"];
                    if (AssgnResult == 1)
                    {
                        string Query_HU = "[dbo].[SP_HU_Upsert_Stock_Reservation] @OutBoundId = " + items.OutboundID + ", @UserId = " + items.UserID + "";
                        //Data = await DbUtility.GetjsonDataFromDatasetINQ(this.ConnectionString, Query_HU);
                        response.Result = "1"; // Success 
                    }
                    if (AssgnResult == -5)
                    {
                        response.Result = "-5"; // Stock not available
                    }
                    else if (AssgnResult == -6)
                    {
                        response.Result = "-6";  //Another User is Processing Release Request
                    }
                    else if (AssgnResult == -1)
                    {
                        response.Result = "-1";  //Problem with Assigning
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

        public async Task<Payload<string>> GetSOsList(GetSOsListModel getSOsList)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    {"@TenantID", getSOsList.TenantID },
                    {"@UserID", getSOsList.UserID }

                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "GET_OPEN_SOLIST_OUTBOUNDCREATION", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetSearchOutboundDetails(GetSearchOutboundDetailsModel getSearchOutboundDetails)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                StringBuilder sCmdupdatelineitem = new StringBuilder();
                sCmdupdatelineitem.Append("EXEC dbo.sp_OBD_SearchOutbound_New ");
                sCmdupdatelineitem.Append("@StartDate=" + (string.IsNullOrEmpty(getSearchOutboundDetails.FromDate) ? "Null" : DBLibrary.SQuote(getSearchOutboundDetails.FromDate)));
                sCmdupdatelineitem.Append(",@EndDate=" + (string.IsNullOrEmpty(getSearchOutboundDetails.ToDate) ? "Null" : DBLibrary.SQuote(getSearchOutboundDetails.ToDate)));
                sCmdupdatelineitem.Append(",@StoreID=" + getSearchOutboundDetails.Warehouseid);
                sCmdupdatelineitem.Append(",@DocumentTypeID=" + getSearchOutboundDetails.DocumentTypeID);
                sCmdupdatelineitem.Append(",@DivisionIDs=" + (string.IsNullOrEmpty(getSearchOutboundDetails.DivisionIDs) ? "Null" : DBLibrary.SQuote(getSearchOutboundDetails.DivisionIDs)));
                sCmdupdatelineitem.Append(",@DeliveryStatusID='" + getSearchOutboundDetails.DeliveryStatusID + "'");
                sCmdupdatelineitem.Append(",@WarehouseID=" + getSearchOutboundDetails.Warehouseid);
                sCmdupdatelineitem.Append(",@SearchText=" + (string.IsNullOrEmpty(getSearchOutboundDetails.SearchText) ? "Null" : DBLibrary.SQuote(getSearchOutboundDetails.SearchText)));
                sCmdupdatelineitem.Append(",@SearchField=" + getSearchOutboundDetails.SearchField);
                sCmdupdatelineitem.Append(",@PageIndex=" + getSearchOutboundDetails.PageIndex);
                sCmdupdatelineitem.Append(",@PageSize=" + getSearchOutboundDetails.PageSize);
                sCmdupdatelineitem.Append(",@TenantID=" + getSearchOutboundDetails.tenantid);
                sCmdupdatelineitem.Append(",@AccountID_New=" + getSearchOutboundDetails.AccountID_New);
                sCmdupdatelineitem.Append(",@UserTypeID_New=" + getSearchOutboundDetails.UserTypeID_New);
                sCmdupdatelineitem.Append(",@TenantID_New=" + getSearchOutboundDetails.TenantID_New);
                sCmdupdatelineitem.Append(",@UserID_New=" + getSearchOutboundDetails.UserID_New);
                sCmdupdatelineitem.Append(",@AWBNo=" + (string.IsNullOrEmpty(getSearchOutboundDetails.AWBNo) ? "Null" : DBLibrary.SQuote(getSearchOutboundDetails.AWBNo)));
                sCmdupdatelineitem.Append(",@DueDate=" + (string.IsNullOrEmpty(getSearchOutboundDetails.DueDate) ? "Null" : DBLibrary.SQuote(getSearchOutboundDetails.DueDate)));
                sCmdupdatelineitem.Append(",@VehicleNo=" +  DBLibrary.SQuote(getSearchOutboundDetails.VehicleNo));

                string AppendQuery = sCmdupdatelineitem.ToString();
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDatasetINQ(this.ConnectionString, AppendQuery);
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

        private int GetWarehouseIDByOutbound(int outboundID)
        {
            try
            {
                int warehouseID;
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("select REF_WH.WarehouseID AS N from OBD_Outbound OBD ");
                sb.Append("JOIN OBD_RefWarehouse_Details REF_WH ON REF_WH.OutboundID = OBD.OutboundID ");
                sb.Append("WHERE OBD.OutboundID = @outboundID");

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    SqlCommand command = new SqlCommand(sb.ToString(), connection);
                    command.Parameters.AddWithValue("@outboundID", outboundID);

                    connection.Open();
                    warehouseID = (int)command.ExecuteScalar();
                    connection.Close();
                }

                return warehouseID;
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                return 0;
            }


        }

        public async Task<Payload<string>> GetOBDwiseItem(GetOBDwiseItemModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    { "@AccountID", items.AccountID },
                    { "@outboundid", items.OBDNumber },
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "sp_GET_OPENOBDWISESLIST", sqlParams).ConfigureAwait(false);
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

        //OBDRevert Page 
        // Revert OBD 
#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<Payload<string>> SetOBDRevert(SetOBDRevertModel items)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            Payload<string> response = new Payload<string>();
            string SAPRefNumber = "";
            string PGIReverseDate = "";
            string SAPError = "";
            PGIRevertResponse pgirevert = new PGIRevertResponse();
            try
            {
                if (items.DeliveryStatusID == 4|| items.DeliveryStatusID == 6|| items.DeliveryStatusID == 7)
                {
                    pgirevert = await _jsonPostService.SendPGIRevertJSONDatatoSAP(items.OutboundID);
                    SAPRefNumber = pgirevert.SAPRefNumber;
                    PGIReverseDate = pgirevert.PGIPostingDate;
                    SAPError = pgirevert.SAPError;

                }
                if(SAPError.Contains("Error"))
                {
                    response.addError(SAPRefNumber);
                }

                else
                {
                    if(SAPRefNumber==""  && (items.DeliveryStatusID==4 || items.DeliveryStatusID == 6 || items.DeliveryStatusID == 7))
                    {
                        response.addError("Error : SAP Delivery Number Should Not Empty");
                    }
                    else
                    {
                        DBFactory factory = new DBFactory();
                        IDBUtility DbUtility = factory.getDBUtility();
                        string OrderType = "SO";
                        string SP = "Exec SP_USP_obd_GetSAPnumber @OutboundID = " + items.OutboundID;
                        string SPAInvoiceNo = DbUtility.GetSqlS(SP, ConnectionString).ToString();
                        string Indicator = "2";
                        int ds = 0;
                        string Query = "EXEC [SP_SET_OBD_Revert_New_Simpolo] @DeliveryStatusId=0,@OutboundId = 0,@OBDNumber ='" + items.OBDNumber + "',@CreatedBy='" + items.UserID + "',@DeliveryStatusTypeID=" + items.DeliveryTypeID + ",@ReversalDocumentNumber=" + DBLibrary.SQuote(SAPRefNumber) + ",@ReversalDate=" + DBLibrary.SQuote(PGIReverseDate);
                        ds = DbUtility.GetSqlN(Query, ConnectionString);
                        response.Result = ds.ToString();
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


        //outbounddetails - Initiate Outbound Delivery>update delivery
        public async Task<Payload<string>> UpsertUpdateDelivery(UpsertUpdateDeliveryModel items)
        {
            Payload<string> response = new Payload<string>();
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                var OutboundID = 0;
                //string GetOBDTrackSQL = "EXEC [dbo].[sp_OBD_GetOutboundDetails] @AccountID_New=" + items.AccountID + ", @OutboundID=" + items.OutboundID;
                //var TableData = await DbUtility.GetjsonDataFromDatasetINQ(this.ConnectionString, GetOBDTrackSQL);
                //JObject data = JObject.Parse(TableData);
                //JArray table = (JArray)data["Table"];
                //int DeliveryStatusID = (int)table[0]["DeliveryStatusID"];
                //string OBDNumber = (string)table[0]["OBDNumber"];
                //int OutboundID = (int)table[0]["OutboundID"];
                if (items.DeliveryStatusID == 4)
                {
                    response.Result = "-1";//Outbound is already delivered
                    return response;
                }
                string Query = "[dbo].[USP_GetOBDBYCustomerPO] @OutboundID = " + items.OutboundID;
                int result = DbUtility.GetSqlN(Query, ConnectionString);
                if (result != 0)
                {
                    response.Result = "-2"; // Once the sales order line items are mapped, cannot modify the header information
                    return response;
                }
                if (items.DeliveryStatusID == 0 || items.DeliveryStatusID == 3 || items.DeliveryStatusID == 10)
                {
                    items.DeliveryStatusID = 1;
                }

                if (items.OutboundID == 0)
                {
                    string EXEQuery = "[dbo].[USP_GetOBDByNumber] @OBDNumber = " + items.OBDNumber;
                    var SQlCheckIfOBDExists = await DbUtility.GetjsonDataFromDatasetINQ(this.ConnectionString, EXEQuery);
                    if (SQlCheckIfOBDExists == "0")
                    {
                        var ResultOBD = await UpsertOutBound(items);
                        var newoutboundid = ResultOBD.Result;
                        OutboundID = Convert.ToInt32(newoutboundid);
                        string OBD = "Exec [dbo].[USP_GetOBDNumberByID] @OutboundID = " + OutboundID;
                        var GetOBD = DbUtility.GetSqlS(OBD, ConnectionString).ToString();
                        if (OutboundID != 0)
                        {

                        }
                        else
                        {
                            response.Result = "3";//redirect to OutDetails Page;
                        }
                    }
                    else
                    {
                        response.Result = "2"; //"'OBD Number' already exists. Please change the 'OBD Number' and try again";
                    }
                }
                else
                {
                    var res = await UpsertOutBound(items);
                    var NewOutboundID = res.Result;
                    OutboundID = Convert.ToInt32(NewOutboundID);
                    response.Result = "1"; //Successfully Updated..!
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

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<Payload<string>> UpsertOutBound(UpsertUpdateDeliveryModel items)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            Payload<string> response = new Payload<string>();
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                StringBuilder OBD = new StringBuilder(2500);
                OBD.Append("DECLARE @NewUpdateOutboundID int;  ");
                OBD.Append("EXEC [sp_OBD_UpsertOutbound] ");
                OBD.Append("@OutboundID=" + items.OutboundID + ",");
                OBD.Append("@OBDNumber=" + "'" + items.OBDNumber + "'" + ",");
                OBD.Append("@DocumentTypeID=" + items.DocumentTypeID + ",");
                OBD.Append("@OBDDate=" + "'" + items.OBDDate + "'" + ",");
                OBD.Append("@CustomerID=" + items.CustomerID + ",");
                OBD.Append("@RequestedBy=" + items.UserID + ",");
                OBD.Append("@DepartmentID=" + items.DepartmentID + ",");
                OBD.Append("@DivisionID=" + items.DivisionID + ",");
                OBD.Append("@RemByIni_OnCreation=" + "'" + items.RemByIni_OnCreation + "'" + ",");
                OBD.Append("@InitiatedBy=" + items.UserID + ",");
                OBD.Append("@DeliveryStatusID=" + items.DeliveryStatusID + ",");
                OBD.Append("@IsReservationDelivery=" + items.IsReservationDelivery + ",");
                OBD.Append("@PriorityLevel=" + items.PriorityLevel + ",");
                OBD.Append("@PriorityDateTime=" + (string.IsNullOrEmpty(items.PriorityDateTime) ? "Null" : DBLibrary.SQuote(items.PriorityDateTime)) + ",");
                OBD.Append("@LastModifiedBy=" + items.UserID + ",");
                OBD.Append("@IsDNPublished=" + items.IsDNPublished + ",");
                OBD.Append("@TenantID=" + items.TenantID + ",");
                OBD.Append("@WarehouseIDs=" + "'" + items.WarehouseIDs + "'" + ",");
                OBD.Append("@CreatedBy=" + items.UserID + ",");
                OBD.Append("  @NewOutboundID=@NewUpdateOutboundID OUTPUT     ;");
                OBD.Append("  select @NewUpdateOutboundID AS N");

                int NewOutboundID = DbUtility.GetSqlN(OBD.ToString(), this.ConnectionString);
                response.Result = NewOutboundID.ToString();
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
        public async Task<Payload<string>> UpsertOBD(UpsertOBDInputModel upsertOBDInput)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    {"@SoNumbers", upsertOBDInput.SoNumbers },
                    {"@AccountID", upsertOBDInput.AccountID },
                    {"@TenantId", upsertOBDInput.TenantId },
                    {"@WareHouseId", upsertOBDInput.WareHouseId  },
                    {"@DeliveryTypeId", upsertOBDInput.DeliveryTypeId },
                    {"@CreatedBy", upsertOBDInput.CreatedBy },
                    {"@PriorityTypeID", upsertOBDInput.PriorityTypeID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "sp_UPSERT_BULK_OBD_CREATION_NEW2", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetPickList(PickListInputModel pickListInput)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    {"@OutboundId", pickListInput.OutboundId },
                    {"@MCode", pickListInput.MCode }

                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "sp_OBD_DeliveryPickNote_NEW", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetPickedItems(PickedItemsInputModel pickedItemsInput)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    {"@MaterialMasterID", pickedItemsInput.MaterialMasterID },
                    {"@CartonCode", pickedItemsInput.CartonCode },
                    {"@AccountID", pickedItemsInput.AccountID },
                    {"@OBD", pickedItemsInput.OBD  },
                    {"@Location", pickedItemsInput.Location },
                    {"@BatchNo", pickedItemsInput.BatchNo },
                    {"@SerialNo", pickedItemsInput.SerialNo },
                    {"@ProjectRefNo", pickedItemsInput.ProjectRefNo },
                    {"@ExpDate", pickedItemsInput.ExpDate },
                    {"@MfgDate", pickedItemsInput.MfgDate },

                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "GEN_OBD_ITEMSWISEPICKEDLIST", sqlParams).ConfigureAwait(false);
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

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<Payload<string>> InsertPickItem(InsertPickItemInputModel insertPickItemInput)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            Payload<string> response = new Payload<string>();
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                //var sqlParams = new Dictionary<string, object>
                //{
                //    {"@OBDNumber", DBLibrary.SQuote(insertPickItemInput.OBDNumber) },
                //    {"@SOHeaderID", insertPickItemInput.SOHeaderID },
                //    {"@AssignedId", insertPickItemInput.AssignedId },
                //    {"@SoDetailsIdnew", insertPickItemInput.SoDetailsIdnew },
                //    {"@AccountID", insertPickItemInput.AccountID },
                //    {"@LineNumber", insertPickItemInput.LineNumber },
                //    {"@Location", DBLibrary.SQuote(insertPickItemInput.Location) },
                //    {"@MCode", DBLibrary.SQuote(insertPickItemInput.MCode) },
                //    {"@Quantity", insertPickItemInput.Quantity },
                //    {"@Mfgdate", DBLibrary.SQuote(insertPickItemInput.Mfgdate) },
                //    {"@ExpDate", DBLibrary.SQuote(insertPickItemInput.ExpDate) },
                //    {"@BatchNo", DBLibrary.SQuote(insertPickItemInput.BatchNo) },
                //    {"@SerialNo", DBLibrary.SQuote(insertPickItemInput.SerialNo) },
                //    {"@CreatedBy", insertPickItemInput.CreatedBy  },
                //    {"@CartonCode", DBLibrary.SQuote(insertPickItemInput.@CartonCode) },
                //    {"@ToCartonCode", DBLibrary.SQuote(insertPickItemInput.ToCartonCode) },
                //    {"@Projrefno", DBLibrary.SQuote(insertPickItemInput.Projrefno) },
                //    {"@MRP", DBLibrary.SQuote(insertPickItemInput.MRP) },

                //};

                //response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "sp_INV_PickItemFromBin", sqlParams).ConfigureAwait(false);

                string InsertPickItemQuery = "EXEC [dbo].[sp_INV_PickItemFromBin] @OBDNumber=" + DBLibrary.SQuote(insertPickItemInput.OBDNumber) + ",@SOHeaderID=" + insertPickItemInput.SOHeaderID + ",@MRP=" + DBLibrary.SQuote(insertPickItemInput.MRP) + ",@Projrefno=" + DBLibrary.SQuote(insertPickItemInput.Projrefno) + ",@ToCartonCode=" + DBLibrary.SQuote(insertPickItemInput.ToCartonCode) + ",@CartonCode=" + DBLibrary.SQuote(insertPickItemInput.CartonCode) + ",@CreatedBy=" + insertPickItemInput.CreatedBy + ",@SerialNo=" + DBLibrary.SQuote(insertPickItemInput.SerialNo) + ",@BatchNo=" + DBLibrary.SQuote(insertPickItemInput.BatchNo) + ",@ExpDate=" + DBLibrary.SQuote(insertPickItemInput.ExpDate) + ",@Mfgdate=" + DBLibrary.SQuote(insertPickItemInput.Mfgdate) + ",@Quantity=" + insertPickItemInput.Quantity + ",@MCode=" + DBLibrary.SQuote(insertPickItemInput.MCode) + ",@Location=" + DBLibrary.SQuote(insertPickItemInput.Location) + ",@LineNumber=" + insertPickItemInput.LineNumber + ",@AccountID=" + insertPickItemInput.AccountID + ",@SoDetailsIdnew=" + insertPickItemInput.SoDetailsIdnew + ",@AssignedId=" + insertPickItemInput.AssignedId + "";
                DataSet DS = DbUtility.GetDS(InsertPickItemQuery, this.ConnectionString);
                var jsonresult = JsonConvert.SerializeObject(DS);

                JObject data = JObject.Parse(jsonresult);
                JArray table = (JArray)data["Table"];
                response.Result = JsonConvert.SerializeObject(table);
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

        public async Task<Payload<string>> DeletePickitem(DeletePickItemsinputModel deletePickItemsinput)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    {"@VLPDPickedID", deletePickItemsinput.VLPDPickedID },
                    {"@CreatedBy", deletePickItemsinput.CreatedBy }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "DELETE_PICKEDITEMS_FOR_VLPD_OUTBOUND", sqlParams).ConfigureAwait(false);
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
        ///Pick N Check Pending--
        // Delivery Document Line ItemS
        public async Task<Payload<string>> GetSOLineItems(GetSOLineItemsInputModel sOLineItemsInput)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    {"@OutboundID", sOLineItemsInput.OutboundID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "sp_OBD_GetSOLineItems", sqlParams).ConfigureAwait(false);
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

        //pick & check and PGI
        public async Task<Payload<string>> UpdateShipmentDetails(UpdateShipmentDetailsModel updateShipmentDetails)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                Response obj = new Response();
                var Result = GetShipperResponse(updateShipmentDetails.OutboundID);
                if (Result.Result == "Error")
                {
                    if (Result.Error == "No data found")
                    {
                        //response.addError(Result.Error);
                        response.Result = "-1";//No data found
                    }
                    else
                    {
                        //response.addError(Result.Error);
                        response.Result = "-2";//Unexpected error from QAD..!An invalid request URI was provided. The request URI must either be an absolute URI or BaseAddress must be set.
                    }
                }
                else
                {
                    var sqlParams = new Dictionary<string, object>
                    {
                        {"@OutboundID", updateShipmentDetails.OutboundID },
                        {"@Result", Result.Result }
                    };
                    DBFactory factory = new DBFactory();
                    IDBUtility DbUtility = factory.getDBUtility();
                    var result = await DbUtility.GetjsonData(this.ConnectionString, "USP_SET_UPDATESHIPMENTNOQAD", sqlParams).ConfigureAwait(false);
                    response.Result = "Shipment Details Updated With" + "'" + Result.Result + "'" + "";
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


        public async Task<Payload<string>> LoadUOMs(DeliveryPackslipModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>{
                {"@prefix",items.prefix},
                {"@LoginAccountId",items.LoginAccountId},
                {"@LoginUserId",items.LoginUserId},
                {"@LoginTanentId",items.LoginTanentId},//LOADING UOMS
            };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_OBD_LoadUOMs", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetPSNMaterialDetails(DeliveryPackslipModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>{
                {"@PSNHeaderId",items.PSNHeaderId},
                //{"LoginAccountId",items.LoginAccountId},
                //{"LoginUserId",items.LoginUserId},
                //{"LoginTanentId",items.LoginTanentId},
            };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_GET_OBD_PackingSlip_Material_Info", sqlParams).ConfigureAwait(false);
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


        public async Task<Payload<string>> LoadPSNMaterialItems(DeliveryPackslipModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>{
                {"@OBDID",items.OutboundID},
                {"@prefix",items.prefix}
                //{"LoginAccountId",items.LoginAccountId},
                //{"LoginUserId",items.LoginUserId},
                //{"LoginTanentId",items.LoginTanentId}
            };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_OutboundMaterialDropDown", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> DeletePSNMaterialItems(DeliveryPackslipModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>{
                {"@PSN",items.IDs}
                //{"@LoginAccountId",items.LoginAccountId},
                //{"@LoginUserId",items.LoginUserId},
                //{"@LoginTanentId",items.LoginTanentId}
            };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_DELETE_PACKING_SLIPS", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> DeletePSNMaterialitemDetail(DeliveryPackslipModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>{
                {"@PSDetailsID",items.PSDetailsID}
                //{"@LoginAccountId",items.LoginAccountId},
                //{"@LoginUserId",items.LoginUserId},
                //{"@LoginTanentId",items.LoginTanentId}
            };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_Delete_PSN_Material_Details", sqlParams).ConfigureAwait(false);
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


        public async Task<Payload<string>> UpsertPackingSlipAddMaterialInfo(UpsertPackingSlipAddMaterialInfo items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                //var sqlParams = new Dictionary<string, object>
                //{
                //{"@PSNHeaderId",items.PSNHeaderId},
                //{"@Material","'"+items.Material+"'"},
                //{"@PickedQty",items.PickedQty},
                //{"@PackedQty",items.PackedQty},
                //{"@CreatedBy",items.CreatedBy},
                //{"@PackedUOM",items.PackedUOM},
                //{"@Itemvolume",items.Itemvolume},
                //{"@ItemWeight",items.ItemWeight},
                ////{"LoginAccountId",items.LoginAccountId},
                ////{"LoginUserId",items.LoginUserId},
                ////{"LoginTanentId",items.LoginTanentId}
                //};
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();


                DataSet ds = DbUtility.GetDS("EXEC sp_OBD_InsertPackingSlipNumberMaterialNumber @PSNHeaderId = " + items.PSNHeaderId + ",@Material='" + items.Material + "',@PickedQty=" + items.PickedQty + ",@PackedQty=" + items.PackedQty + ",@CreatedBy=" + items.CreatedBy + ",@PackedUOM=" + items.PackedUOM + ",@Itemvolume=" + items.Itemvolume + ",@ItemWeight=" + items.ItemWeight, this.ConnectionString);

                // response.Result = JsonConvert.SerializeObject(result.Tables[0].Rows[0][0]);

                response.Result = JsonConvert.SerializeObject(ds, Newtonsoft.Json.Formatting.Indented);
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
        //view pending goods out list
        public async Task<Payload<string>> GetPendingGoodsOutList(GetPendingGoodsOutInputModel getPendingGoodsOutInput)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    {"@OutboundID", getPendingGoodsOutInput.OutboundID },
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "sp_INV_GetPendingGoodsOutList_NEW", sqlParams).ConfigureAwait(false);
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

        // Delivery Pack Slip > SAVE
        public async Task<Payload<string>> UpsertPackingSlipNumber(UpertPackingSlipInputModel UpertPackingSlipInput)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                StringBuilder sCmdPilotCr = new StringBuilder();
                sCmdPilotCr.AppendLine("Exec SP_USP_obd_CheckingDelivarySattusID");
                sCmdPilotCr.AppendLine("@OutBoundId =" + UpertPackingSlipInput.OutBoundId);

                string OutBoundId = sCmdPilotCr.ToString();
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                int DelivarySattusID = DbUtility.GetSqlN(OutBoundId, ConnectionString);
                string lblOutboundStatus = UpertPackingSlipInput.OutboundStatus;
                if (lblOutboundStatus == "Sent to Packing" && DelivarySattusID < 7)
                {
                    //StringBuilder sb = new StringBuilder();
                    //string sp = "exec[dbo].[sp_OBD_InsertPackingSlipNumber] @OutBoundId = " + UpertPackingSlipInput.OutBoundId + ",@HandlingTypeId = " + (UpertPackingSlipInput.HandlingTypeId) + ",@Maxweight = " + (UpertPackingSlipInput.Maxweight) + ",@Maxvolume = " + UpertPackingSlipInput.Maxvolume
                    //+ ",@Remarks = " + UpertPackingSlipInput.Remarks + ",@CreatedBy = " + (UpertPackingSlipInput.CreatedBy) + "";
                    // response.Result;

                    var sqlParams = new Dictionary<string, object>
                    {
                        {"@OutBoundId", UpertPackingSlipInput.OutBoundId },
                        {"@HandlingTypeId", UpertPackingSlipInput.HandlingTypeId },
                        {"@Maxweight", UpertPackingSlipInput.Maxweight },
                        {"@Maxvolume", UpertPackingSlipInput.Maxvolume},
                        {"@Remarks", UpertPackingSlipInput.Remarks},
                        {"@CreatedBy", UpertPackingSlipInput.CreatedBy }
                    };
                    response.Result = await DbUtility.GetjsonData(this.ConnectionString, "sp_OBD_InsertPackingSlipNumber", sqlParams).ConfigureAwait(false);
                    response.Result = "1";
                }
                else
                {
                    //resetError("Delivery challan is already updated. Please check for another store from the 'Packing Store' dropdown", true);
                    response.Result = "-1";
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

        public async Task<Payload<string>> GetDeliveryNoteHeader(GetDeliveryNoteHeaderinputModel getDeliveryNoteHeaderinput)
        {
            Payload<string> response = new Payload<string>();
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                StringBuilder sp = new StringBuilder();
                sp.AppendLine("EXEC [dbo].[USP_LoadTenantDataByUserWH] @prefix=" + "'" + getDeliveryNoteHeaderinput.prefix + "'" + " , @USERID=" + getDeliveryNoteHeaderinput.USERID + " , @AccountID=" + getDeliveryNoteHeaderinput.AccountID + ", @OutboundID = " + getDeliveryNoteHeaderinput.OutboundID + ";"
                + "EXEC [sp_OBD_GetDeliveryNote_Header] @OutboundID =" + getDeliveryNoteHeaderinput.OutboundID + "");
                string output = sp.ToString();
                response.Result = await DbUtility.GetjsonDataFromDatasetINQ(this.ConnectionString, output);
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
        public async Task<Payload<string>> UpdatePackingSlipInformation(UpdatePackingSlipInformationModel updatePackingSlipInformation)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                string OutBoundStatus = "Exec SP_USP_obd_CheckingDelivarySattusID @OutboundID=" + updatePackingSlipInformation.OutboundID;
                int Deliverystatus = DbUtility.GetSqlN(OutBoundStatus, ConnectionString);
                if (Deliverystatus == -1)
                {
                    response.Result = "-1";
                }
                else
                {
                    if ((DbUtility.GetSqlN("Exec SP_USP_obd_CheckingDocumentTypeID @OutboundID=" + updatePackingSlipInformation.OutboundID, this.ConnectionString) == 6))
                    {
                        string query = "Exec SP_USP_obd_ChangeDeliverystatus @OutboundID=" + updatePackingSlipInformation.OutboundID;
                        var Res = await DbUtility.GetjsonDataFromDatasetINQ(this.ConnectionString, query);
                        response.Result = "2"; //Sent to Delivery
                    }
                    else if ((DbUtility.GetSqlN("Exec SP_USP_obd_GetDocumentTypeID @OutboundID=" + updatePackingSlipInformation.OutboundID, this.ConnectionString) == 6))
                    {
                        string query = "Exec SP_USP_obd_UpadteDeliveryStatusID @OutboundID=" + updatePackingSlipInformation.OutboundID;
                        var Res = await DbUtility.GetjsonDataFromDatasetINQ(this.ConnectionString, query);
                        response.Result = "5";//Sent to Delivery
                    }
                    else if ((DbUtility.GetSqlN("Exec SP_USP_obd_CheckingDeliveryStatusID @OutboundID=" + updatePackingSlipInformation.OutboundID, this.ConnectionString) == 7))
                    {
                        response.Result = "6";//Packing is already updated    
                    }
                    else if (Deliverystatus == 4)
                    {
                        response.Result = "1"; //Outbound Is Already Delivered
                    }
                    else if (response.Result == "0")
                    {
                        response.Result = "0"; //"PGI not yet performed
                    }

                    else
                    {
                        response.Result = "8"; // PackingSlip Is Already Generated
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

        //Same for DeliveryPickNote first Grid
        public async Task<Payload<string>> GetInitiateOutboundDelivery(GetInitiateOutboundDeliveryModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();

                StringBuilder GetOBDTrackSQL = new StringBuilder();
                GetOBDTrackSQL.AppendLine("EXEC [dbo].[sp_OBD_GetOutboundDetails] @AccountID_New=" + items.AccountID + ", @OutboundID = " + items.OutboundID + ";"
                + "EXEC [USP_GetDeliveryStatusByOBD] @OutboundID =" + items.OutboundID + ";" + "EXEC [USP_GET_SOTYPEID] @OutboundID =" + items.OutboundID + "");
                string output = GetOBDTrackSQL.ToString();

                response.Result = await DbUtility.GetjsonDataFromDatasetINQ(this.ConnectionString, output);
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

        public async Task<Payload<string>> UpsertDDLineItems(UpsertDDLineItemsModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                StringBuilder SB = new StringBuilder(2500);
                String Query = "EXEC [dbo].[sp_OBD_GetOutboundDetails] @AccountID_New=" + items.AccountID + ", @OutboundID=" + items.OutboundID;
                var TableData = await DbUtility.GetjsonDataFromDatasetINQ(this.ConnectionString, Query);
                JObject data = JObject.Parse(TableData);
                JArray table = (JArray)data["Table"];
                int DeliveryStatusID = (int)table[0]["DeliveryStatusID"];
                if (DeliveryStatusID == 4)
                {
                    response.Result = "-1";//Outbound is already delivered
                    return response;
                }
                SB.Append("DECLARE @NewUpdateoutboundSOID int; ");
                SB.Append("EXEC [dbo].[sp_OBD_UpsertOutbound_CustomerPO_New]  ");
                SB.Append("@OutboundID=" + items.OutboundID + ",");
                SB.Append("@SONumber=" + DBLibrary.SQuote(items.SONumber.ToString()) + ",");
                SB.Append("@CustomerPONumber=" + DBLibrary.SQuote(items.CustPONumber) + ",");
                SB.Append("@InvoiceNumber=" + DBLibrary.SQuote(items.InvoiceNumber) + ",");
                SB.Append("@Outbound_CustomerPOID=" + items.CustomerPOID + ",");
                SB.Append("@CreatedBy=" + items.UserID + ",");
                SB.Append("@TenantID=" + items.TenantID + ",");
                SB.Append("@NewOutbound_CustomerPOID=@NewUpdateoutboundSOID OUTPUT;  ");
                SB.Append("select @NewUpdateoutboundSOID AS N");
                string SBQuery = SB.ToString();
                try
                {
                    int Result = DbUtility.GetSqlN(SBQuery, this.ConnectionString);
                    if (Result == -1)
                    {
                        response.Result = "-1";//SO Number does not exist
                    }
                    else if (Result == -2)
                    {
                        response.Result = "-2";// Error while updating
                    }
                    else if (Result == -3)
                    {
                        response.Result = "-3";  //No SO line item is configured to this customer po
                    }
                    else if (Result == -4)
                    {
                        response.Result = "-4";  //Customer PO Number does not exist
                    }
                    else if (Result == -5)
                    {
                        response.Result = "-5"; //Invoice Number already exists
                    }
                    else if (Result == -6)
                    {
                        response.Result = "-6"; // Duplicate Invoice number
                    }
                    else
                    {
                        response.Result = "1"; // Successfully Updated"
                    }
                }
                catch (Exception Ex)
                {
                    throw Ex;
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

        public async Task<Payload<string>> GetPickCheckPick(GetPickCheckPickModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                int REF_WHID = DbUtility.GetSqlN("Exec [dbo].[USP_GetREF_WHID] @OutboundID=" + items.OutboundID, this.ConnectionString);

                int WHID = DbUtility.GetSqlN("Exec [dbo].[USP_GetWHIDByRefWHID]  @REFWHID = " + REF_WHID, this.ConnectionString);

                String Query = "EXEC [dbo].[sp_OBD_GetOutboundDetails] @AccountID_New=" + items.AccountID + ", @OutboundID=" + items.OutboundID;
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

        public async Task<Payload<string>> UpsertUpdatePGI(UpsertUpdatePGIModel items)
        {
            Payload<string> response = new Payload<string>();

            int documentTypeID = 0;
            string jsonData = "";
            string SAPResult = "";
            string PGIDate = "";
            PGIResponse pgi = new PGIResponse();


            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                int IsOBDpiking = 0;
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    string query = "SELECT isnull(IsVLPDPicking,0) AS TI FROM OBD_Outbound WHERE OutboundID = @OutboundID";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@OutboundID", items.OutboundID);
                        IsOBDpiking = Convert.ToInt32(command.ExecuteScalar());
                    }
                }




                if (IsOBDpiking == 1)
                {
                    if (DbUtility.GetSqlN("Declare  @GoodsOutStatus int; EXEC [sp_INV_CheckGoodsOutStatus] @OutboundID=" + items.OutboundID + ", @Status=@GoodsOutStatus out; select @GoodsOutStatus as N;", this.ConnectionString) == 0)
                    {
                        response.Result = "-1";//  resetError("Goods Out is not yet performed", true);
                        return response;
                    }
                }
                var rsRefDetails = DbUtility.GetRS("Exec USP_OBDRefWarehouseDropDown @WareHouseID=" + items.WarehouseID + " , @OutboundID=" + items.OutboundID, this.ConnectionString);
                DataSet dsDocumentJSONData = DbUtility.GetDS("Exec SP_Get_JSON_Document_By_OBD @OutboundID=" + items.OutboundID + "", this.ConnectionString);
                if (dsDocumentJSONData != null && dsDocumentJSONData.Tables.Count > 0 && dsDocumentJSONData.Tables[0].Rows.Count > 0)
                {
                    DataTable dt = dsDocumentJSONData.Tables[0];
                    foreach (DataRow row in dt.Rows)
                    {
                        documentTypeID = Convert.ToInt32(row["DocumentTypeID"]);
                        jsonData = row["JSONData"].ToString();
                    }
                }

                string ErrorCheck = DbUtility.GetSqlS("Exec SP_Check_OutboundSAP @OutboundID=" + items.OutboundID+",@UserID="+items.UserID,this.ConnectionString);
                await Task.Delay(TimeSpan.FromSeconds(2));
                if (ErrorCheck.Contains("Error"))
                {
                    response.Result = ErrorCheck;
                    return response;
                    
                }

                if (documentTypeID == 5)
                {
                    string Result = DbUtility.GetSqlS("Exec SP_InsertDataAPI @JSONData=" + DBUtil.DBLibrary.SQuote(jsonData), this.ConnectionString);
                    SAPResult = "Success:PLANTTOPLANT";
                }
                else
                {
                    Response obj = new Response();
                    PGIResponse pGIResponse = new PGIResponse();
                    pgi = await _jsonPostService.SendPGIJSONDatatoSAP(items.OutboundID);
                    SAPResult = pgi.SAPRefNumber;
                    items.m_PGIDone_DT = pgi.PGIPostingDate;
                    
                }

                if (SAPResult.Contains("SAP Error:"))
                {

                    response.Result = "-5";
                    response.addError(SAPResult);
                }
                else if (SAPResult.Contains("Error:"))
                {

                    response.Result = "-5";
                    response.addError(SAPResult);


                }

                else if (SAPResult.Contains("Internal Server Error"))
                {
                    throw new Exception(" Error while establishing the connection");
                }

                else if (SAPResult.Contains("Success:"))
                {
                    string Result = "";
                    string[] Sapno = SAPResult.Split(":");
                    Result = Sapno[1];
                    //string Result = SAPResult
                    if (IsOBDpiking == 0)
                    {
                        int NOBD = 0;
                        try
                        {
                            //var dsResult = DbUtility.GetjsonDataFromDatasetINQ(this.ConnectionString, "EXEC [dbo].[sp_OBD_MoveStockOutByPickingData] @OutboundID = " + items.OutboundID + ", @CreatedBy = " + items.UserID + "");
                            //if (dsResult != null)
                            //{

                            int REF_WHID = DbUtility.GetSqlN("Exec[dbo].[USP_GetREF_WHID]  @WarehouseID = " + items.WarehouseID + " ,@OutboundID = " + items.OutboundID, this.ConnectionString);

                            int OBDTrack_WHID = DbUtility.GetSqlN("Exec[dbo].[USP_GetOBDTrack_WHID]  @OutboundID = " + items.OutboundID + " ,@REF_WHID = " + REF_WHID, this.ConnectionString);

                            StringBuilder str = new StringBuilder(2500);
                            str.Append("DECLARE @NewUpdateOutboundID int;  ");
                            str.Append("EXEC [sp_OBD_UpsertOutboundTracking_Warehouse_New] ");
                            str.Append("@OutboundID=" + items.OutboundID + ",");
                            str.Append("@StoreInchargeID=" + items.m_StoreInchargeID.ToString() + ",");
                            str.Append("@OB_RefWarehouse_DetailsID=" + REF_WHID + ",");
                            str.Append("@OBDReceivedOn=" + (items.m_OBDReceivedDT == null ? "NULL" : DBLibrary.SQuote(items.m_OBDReceivedDT)) + ",");
                            str.Append("@TotalQuantity=" + (items.m_TotalQuantity) + ",");
                            str.Append("@NoofLines=" + (items.m_NoofLines) + ",");
                            str.Append("@PickedBy=" + items.m_PickedBy + ",");
                            str.Append("@CheckedBy=" + items.UserID + ",");
                            //str.Append("@RemByStoreIncharge=" + DBLibrary.SQuote(items.m_RemByStoreIncharge) + ",");
                            str.Append("@RemByStoreIncharge=" + (items.m_OBDReceivedDT == null ? "NULL" : DBLibrary.SQuote(items.m_RemByStoreIncharge)) + ",");
                            str.Append("@SentForPGIOn=" + (items.m_PGIDone_DT == null ? "NULL" : DBLibrary.SQuote(items.m_PGIDone_DT)) + ",");
                            str.Append("@TransferedToWarehouseID=" + items.m_TransferedtoStoreID + ",");
                            str.Append("@TenantID=" + items.TenantID + ",");
                            str.Append("@CreatedBy=" + items.UserID + ",");
                            str.Append("@WarehouseID=" + items.WarehouseID + ",");
                            str.Append("@OutboundTracking_WarehouseID=" + OBDTrack_WHID + ",");
                            str.Append("@NewOutboundTracking_WarehouseID=@NewUpdateOutboundID OUTPUT;");
                            str.Append("  select @NewUpdateOutboundID AS N ;");


                            int Output = DbUtility.GetSqlN(str.ToString(), this.ConnectionString);

                            if (Output > 0)
                            {
                                string StockOut = "Exec [Upsert_PGI_Stock_OutBasedonOutbound] @OutboundID=" + items.OutboundID + ",@SAP_RefNumber=" + DBLibrary.SQuote(Result) + ",@PGIDate=" + DBLibrary.SQuote(items.m_PGIDone_DT) + ",@UserID=" + items.UserID;
                                string Resultpgi = DbUtility.GetSqlS(StockOut, this.ConnectionString);
                                if (Resultpgi.Contains("Success"))
                                {
                                    response.Result = Resultpgi;
                                }
                                else
                                {
                                    response.Result = Resultpgi;
                                }

                            }
                            else
                            {
                                response.Result = "Error at the time of PGI";
                            }



                            //if (Output == -2)
                            //{
                            //    response.Result = "-7";
                            //    // resetError("Add atleast one Work Order", true);

                            //    return response;
                            //}
                            //else if (Output == -3)
                            //{
                            //    response.Result = "-8";
                            //    // resetError("Pick N Check is already done for the selected store (" + ddlStores.SelectedItem.Text + ") . Please check another store from the 'Pick Store' dropdown", true);
                            //    return response;
                            //}
                            //else if (Output == -1)
                            //{
                            //    response.Result = "-9";
                            //    // resetError("Error while updating", true);
                            //    return response;
                            //}

                            //var RESOutPut = UpdatePGIDetails(items);

                            //if (RESOutPut == "-2")
                            //{
                            //    response.Result = "-10";
                            //    // resetError("PGI is already updated", true);
                            //    return response;
                            //}
                            //else if (RESOutPut == "-1")
                            //{
                            //    response.Result = "-11";
                            //    // resetError("Error while updating", true);
                            //    return response;
                            //}

                            //string HUQuery = "EXEC [dbo].[SP_Upsert_HU_GoodsOut] @outboundID=" + items.OutboundID + ",@UserID=" + items.UserID;
                            //DataSet dsHu = DbUtility.GetDS(HUQuery, this.ConnectionString);
                            //if (dsHu.Tables[0].Rows[0][0].ToString() == "Done")
                            //{

                            //    string referenceNumberQuery = $"SELECT SAP_PGIRefNo FROM OBD_Outbound WHERE outboundid = {items.OutboundID}";

                            //    DataSet dsReferenceNumber = DbUtility.GetDS(referenceNumberQuery, this.ConnectionString);
                            //    string referenceNumber = dsReferenceNumber.Tables[0].Rows[0]["SAP_PGIRefNo"].ToString();

                            //    string QUERY = "EXEC [dbo].[SP_GetSOType_ForKitting] @outboundID=" + items.OutboundID;
                            //    DataSet DSQ = DbUtility.GetDS(QUERY, this.ConnectionString);
                            //    string subject = "";
                            //    string body = "";
                            //    var ToEmailID = "";
                            //    if (DSQ.Tables[0].Rows[0][0].ToString() == "Kitting")
                            //    {
                            //        int strQuery = DbUtility.GetSqlN("EXEC [dbo].[usp_inbound_kitting]  @SAPRefNo='" + referenceNumber + "', @OutboundID=" + items.OutboundID, this.ConnectionString);
                            //        items.INVReportID = 39;
                            //        var result = await GET_SubjectBody(items.INVReportID);
                            //        subject = result.subject + referenceNumber;
                            //        body = result.body + referenceNumber + " Please proceed with further process";

                            //    }
                            //    else if (DSQ.Tables[0].Rows[0][0].ToString() == "DeKitting")
                            //    {
                            //        items.INVReportID = 40;
                            //        var result = await GET_SubjectBody(items.INVReportID);
                            //        subject = result.subject + referenceNumber;
                            //        body = result.body + referenceNumber + " Please proceed with further process";
                            //    }
                            //    else if (DSQ.Tables[0].Rows[0][0].ToString() == "ServiceOrder")
                            //    {
                            //        items.INVReportID = 41;
                            //        var result = await GET_SubjectBody(items.INVReportID);
                            //        subject = result.subject + referenceNumber;
                            //        body = result.body + referenceNumber + " Please proceed with further process";
                            //    }
                            //    else
                            //    {
                            //        items.INVReportID = 38;  // PGI INVOICE AUTO EMAIL
                            //        var result = await GET_SubjectBody(items.INVReportID);
                            //        subject = result.subject + referenceNumber;
                            //        body = result.body + referenceNumber;
                            //    }
                            //    try
                            //    {
                            //        //ToEmailID = await EMAIL_Generation(items.INVReportID);
                            //        //await _emailService.SendEmailAsync(ToEmailID, subject, body);
                            //        //response.Result = "3";
                            //    }

                            //    catch (Exception ex)
                            //    {
                            //        response.Result = "3"; // resetError("PGI details successfully updated with:" + SAPResult, false);                        
                            //        //string OBDStatus = "UPDATE OBD_Outbound SET DeliveryStatusID = 4 WHERE OutboundID =" + items.OutboundID + "";                    
                            //        //DbUtility.GetSqlN(OBDStatus, ConnectionString);      
                            //    }

                        }
                        catch (Exception ex)
                        {

                        }
                    }

                    else
                    {

                        return response;
                    }

                }
                else
                {
                    throw new Exception("Error while establishing the connection");
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




        public async Task<string> EMAIL_Generation(int INVReportID)
        {
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();

                string sendermails = "EXEC [dbo].[Get_InvoiceEmails] @ReportID=" + INVReportID;
                DataSet dsHem = DbUtility.GetDS(sendermails, this.ConnectionString);
                string ToEmailID = "";
                if (dsHem.Tables[0].Rows.Count > 0)
                {
                    for (var i = 0; i < dsHem.Tables[0].Rows.Count; i++)
                    {
                        ToEmailID += dsHem.Tables[0].Rows[i][0].ToString() + ";";
                    }
                }
                return ToEmailID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<(string subject, string body)> GET_SubjectBody(int INVReportID)
        {
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();

                string sendermails = "EXEC [dbo].[SP_Get_SubjectBody] @ReportNameID=" + INVReportID;
                DataSet dsHem = DbUtility.GetDS(sendermails, this.ConnectionString);

                string subject = dsHem.Tables[0].Rows[0]["SUBJECT"].ToString();
                string body = dsHem.Tables[0].Rows[0]["BODY"].ToString();

                return (subject, body);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public async Task<string> SAPPGIPOSTING(int OutboundID, int UserID, string OrderType)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(1200);
                    string sapurl = this.ServiceURL;
                    string URL = $"{ServiceURL}/SAPIntegration/PostPGIDatatoSAP";
                    string urlParameters = $"?OutboundID={Uri.EscapeDataString(OutboundID.ToString())}&UserID={Uri.EscapeDataString(UserID.ToString())}";

                    HttpResponseMessage result = await client.PostAsync(URL + urlParameters, null);

                    if (result.IsSuccessStatusCode)
                    {
                        string saprefNumber = await result.Content.ReadAsStringAsync();
                        return saprefNumber;
                    }
                    else
                    {
                        //  return $"SAP Connector Failed: { await result.Content.ReadAsStringAsync() }";
                        return result.ReasonPhrase;
                    }
                }
            }
            catch (Exception ex)
            {
                return $"SAP Connector Failed: {ex.Message}";

            }
        }


        public async Task<Payload<string>> GeneratePGIInvoiceData(GeneratePGIInvoiceModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                string SAPResult = await PGIInvoiceDataPOSTING(items.OBDRefNUmber, items.OutboundID);
                response.Result = SAPResult;
                if (SAPResult.Contains("SAP Error:"))
                {
                    response.Result = SAPResult;
                    //response.addError(SAPResult);
                }
                if (SAPResult.Contains("Error:"))
                {
                    response.Result = SAPResult;
                    //response.addError(SAPResult);
                }
                else if (SAPResult.Contains("Internal Server Error"))
                {
                    response.Result = "Error While establishing Connection";
                }
                else
                {
                    response.Result = SAPResult;
                }
            }
            catch (Exception ex)
            {
                response.ResponseCode = ex.Message;
            }
            return response;
        }




        public async Task<string> PGIInvoiceDataPOSTING(string OBDRefNUmber, int OutboundID)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(1200);
                    string sapurl = this.ServiceURL;
                    string URL = $"{ServiceURL}/SAPIntegration/PostPGIInvoiceDatatoSAP";
                    string urlParameters = $"?OBDRefNUmber={OBDRefNUmber}&OutboundID={OutboundID}";
                    HttpResponseMessage result = await client.PostAsync(URL + urlParameters, null);
                    if (result.IsSuccessStatusCode)
                    {
                        string resultString = result.ToString(); // Convert object to string
                        if (resultString.Contains("Error:"))
                        {
                            return resultString;
                        }
                        else
                        {
                            string invoiceData = await result.Content.ReadAsStringAsync();
                            return invoiceData;
                        }
                    }
                    else
                    {
                        return result.ReasonPhrase;
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }



        public Response SOPGIPosting(int OutboundID)
        {
            Response obj = new Response();
            try
            {
                string qadurl = this.APIURL;
                string URL = "" + qadurl + "/Inbound/QADSOPGI";
                string urlParameters = "?OutboundID=" + OutboundID;
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(URL);
                    client.DefaultRequestHeaders.Accept.Add(
                           new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage result = client.GetAsync(urlParameters).Result;

                    if (result.IsSuccessStatusCode)
                    {
                        string jsonString = result.Content.ReadAsStringAsync().Result;
                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                        dynamic item = serializer.Deserialize<object>(jsonString);
                        obj.Result = item["result"];
                        obj.Error = item["errorcode"];

                        return obj;
                    }
                    else
                    {
                        obj.Result = "Error";
                        obj.Error = "Connectiion Error";
                        return obj;
                    }
                }
            }
            catch (Exception ex)
            {
                obj.Result = "Error";
                obj.Error = ex.Message;
                return obj;
            }
        }


        public Response GetShipperResponse(int OutboundID)
        {
            Response obj = new Response();
            try
            {
                string qadurl = this.APIURL;
                string URL = "" + qadurl + "/Inbound/QADShipmentRequest";
                string urlParameters = "?OutboundID=" + OutboundID + "";
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(URL);
                    client.DefaultRequestHeaders.Accept.Add(
                     new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage result = client.GetAsync(urlParameters).Result;

                    if (result.IsSuccessStatusCode)
                    {
                        string jsonString = result.Content.ReadAsStringAsync().Result;
                        JavaScriptSerializer serializer = new JavaScriptSerializer();
                        dynamic item = serializer.Deserialize<object>(jsonString);
                        obj.Result = item["result"];
                        obj.Error = item["errorcode"];
                        return obj;
                    }
                    else
                    {
                        obj.Result = "Error";
                        obj.Error = "Connectiion Error";
                        return obj;
                    }
                }
            }
            catch (Exception ex)
            {
                obj.Result = "Error";
                obj.Error = ex.Message;
                return obj;
            }
        }

        //commented by Prasanna.ch

        //public Response SupplierReturnPGIPosting(int OutboundID)
        //{
        //    Response obj = new Response();
        //    try
        //    {
        //        string qadurl = this.APIURL;
        //        string URL = "" + qadurl + "/Inbound/QADGRNRevert";
        //        string urlParameters = "?grnHeaderID=" + OutboundID + "&&isSupplierRtn=1";
        //        using (HttpClient client = new HttpClient())
        //        {
        //            client.BaseAddress = new Uri(URL);
        //            client.DefaultRequestHeaders.Accept.Add(
        //            new MediaTypeWithQualityHeaderValue("application/json"));
        //            HttpResponseMessage response = client.GetAsync(urlParameters).Result;
        //            if (response.IsSuccessStatusCode)
        //            {
        //                string jsonString = response.Content.ReadAsStringAsync().Result;
        //                JavaScriptSerializer serializer = new JavaScriptSerializer();
        //                dynamic item = serializer.Deserialize<object>(jsonString);
        //                obj.Result = item["result"];
        //                obj.Error = item["errorcode"];
        //                return obj;
        //            }
        //            else
        //            {
        //                obj.Result = "Error";
        //                obj.Error = "Connectiion Error";
        //                return obj;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        obj.Result = "Error";
        //        obj.Error = ex.Message;
        //        return obj;
        //    }
        //}

        public string UpdatePGIDetails(UpsertUpdatePGIModel items)
        {
            Payload<string> response = new Payload<string>();
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            int Result = 0;
            int REF_WHID = DbUtility.GetSqlN("Exec[dbo].[USP_GetREF_WHID]  @WarehouseID = " + items.WarehouseID + " ,@OutboundID = " + items.OutboundID, this.ConnectionString);
            try
            {
                Response obj = new Response();
                StringBuilder strUpdateOBDTracking = new StringBuilder(2500);
                strUpdateOBDTracking.Append("DECLARE @NewUpdateOutboundID int;  ");
                strUpdateOBDTracking.Append("EXEC [sp_OBD_UpsertPGI] ");
                strUpdateOBDTracking.Append("  @OutboundID=" + items.OutboundID + ",");
                //strUpdateOBDTracking.Append("@RemByIni_AfterPGI=" + (items.m_RemByIni_AfterPGI == "" ? "NULL" : DBLibrary.SQuote(items.m_RemByIni_AfterPGI.ToString())) + ",");
                strUpdateOBDTracking.Append("@PGIDoneBy=" + items.m_PGIDoneBy + ",");
                strUpdateOBDTracking.Append("@PGIDoneOn=" + DBLibrary.SQuote(items.m_SentForPGI_DT) + ",");
                strUpdateOBDTracking.Append("@DocumentTypeID=" + items.DocumentTypeID + ",");
                strUpdateOBDTracking.Append("@OB_RefWarehouse_DetailsID=" + REF_WHID + ",");
                strUpdateOBDTracking.Append("@UserID=" + items.UserID + ",");
                strUpdateOBDTracking.Append("@UpdatedBy=" + items.UserID + ",");
                strUpdateOBDTracking.Append("@NewOutboundID=@NewUpdateOutboundID OUTPUT ;");
                strUpdateOBDTracking.Append("select @NewUpdateOutboundID AS N ;");
                Result = DbUtility.GetSqlN(strUpdateOBDTracking.ToString(), this.ConnectionString);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result.ToString();
        }

        public async Task<Payload<string>> GetPackingSlipData(GetPackingSlipDataModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    {"@OutboubdID", items.OutboundID },
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "SP_USP_obd_GetPackingSlipData", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetRouteCode(GetRouteCodeModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    {"@OutboundID", items.OutboundID },
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "USP_GetRouteCodeForOutbound", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetItemMasterLoad(GetItemMasterLoadModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    {"@AccountID" , items.AccountID},
                    {"@TenantID" , items.TenantID}

                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_MMT_MasterDataList", sqlParams).ConfigureAwait(false);
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


        public async Task<Payload<string>> GetPackingSlipNumberData(GetPackingSlipNumberDataModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    {"@OutBoundId", items.OutboundID },
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "USP_GET_OBD_PackingSlip_Header_Info", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetPickMaterial(GetPickMaterialModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    {"@OutBoundId", items.OutboundID },
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "USP_OutboundMaterialDropDown1", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> UpdateDeliveryDetails(UpdateDeliveryDetailsModel items)
        {
            Payload<string> response = new Payload<string>();
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                string Query = "EXEC [dbo].[sp_OBD_GetOutboundDetails] @OutboundID = " + "'" + items.OutboundID + "'" + "";
                var TableData = await DbUtility.GetjsonDataFromDatasetINQ(this.ConnectionString, Query);
                JObject data = JObject.Parse(TableData);
                JArray table = (JArray)data["Table"];
                int DeliveryStatusID = (int)table[0]["DeliveryStatusID"];

                if (DeliveryStatusID == 4)
                {
                    response.Result = "-1"; //"Delivery is already made. Please attach the Proof of Delivery (POD) to process this request"
                    return response;
                }

                string query = "EXEC [dbo].[USP_GetREF_WHID] @OutboundID = " + "'" + items.OutboundID + "'" + "";
                var jsonresult = await DbUtility.GetjsonDataFromDatasetINQ(this.ConnectionString, query);
                JObject jdata = JObject.Parse(jsonresult);
                JArray table1 = (JArray)jdata["Table"];
                var OB_RefWarehouse_DetailsID = (int)table1[0]["N"];

                string StatusQuery = "EXEC [USP_GetDeliveryStatusByOBD] @OutboundID =" + items.OutboundID + "";
                var DeliveryStatus = DbUtility.GetSqlS(StatusQuery, ConnectionString);

                StringBuilder strUpdateOBDTracking = new StringBuilder();
                strUpdateOBDTracking.Append("DECLARE @NewUpdateOutboundID int;  ");
                strUpdateOBDTracking.Append("EXEC [sp_OBD_UpsertDelivery] ");
                strUpdateOBDTracking.Append("  @OutboundID=" + items.OutboundID + ",");
                strUpdateOBDTracking.Append("@TransferedToWarehouseID=" + (items.TransferedToWarehouseID == 0 ? "Null" : items.TransferedToWarehouseID) + ",");
                strUpdateOBDTracking.Append("@InstructionModeID=" + (items.InstructionModeID == 0 ? "Null" : items.InstructionModeID) + ",");
                strUpdateOBDTracking.Append("@Requester=" + (string.IsNullOrEmpty(items.Requester) ? "Null" : DBLibrary.SQuote(items.Requester)) + ",");
                strUpdateOBDTracking.Append("@DocumentNumber=" + (string.IsNullOrEmpty(items.DocumentNumber) ? "Null" : DBLibrary.SQuote(items.DocumentNumber)) + ",");
                strUpdateOBDTracking.Append("@DocumentReceivedDate=" + (string.IsNullOrEmpty(items.DocumentReceivedDate) ? "Null" : DBLibrary.SQuote(items.DocumentReceivedDate)) + ",");
                strUpdateOBDTracking.Append("@DeliveryDate=" + (string.IsNullOrEmpty(items.DeliveryDate) ? "Null" : DBLibrary.SQuote(items.DeliveryDate)) + ",");
                strUpdateOBDTracking.Append("@DeliveredBy=" + items.DeliveredBy + ",");
                strUpdateOBDTracking.Append("@DriverName=" + (string.IsNullOrEmpty(items.DriverName) ? "Null" : DBLibrary.SQuote(items.DriverName)) + ",");
                strUpdateOBDTracking.Append("@ReceivedBy=" + (string.IsNullOrEmpty(items.ReceivedBy) ? "Null" : DBLibrary.SQuote(items.ReceivedBy)) + ",");
                strUpdateOBDTracking.Append("@RemByDeliveryIncharge=" + (string.IsNullOrEmpty(items.RemByDeliveryIncharge) ? "Null" : DBLibrary.SQuote(items.RemByDeliveryIncharge)) + ",");
                strUpdateOBDTracking.Append("@IsPODReceived=" + items.IsPODReceived + ",");
                strUpdateOBDTracking.Append("@OB_RefWarehouse_DetailsID=" + OB_RefWarehouse_DetailsID + ",");
                strUpdateOBDTracking.Append("@DeliveryStatusID=" + DeliveryStatusID + ",");
                strUpdateOBDTracking.Append("@NewOutboundID=@NewUpdateOutboundID OUTPUT,");
                strUpdateOBDTracking.Append("@UpdatedBy=" + items.UserID);
                strUpdateOBDTracking.Append("  select @NewUpdateOutboundID AS N ;");

                int Result = DbUtility.GetSqlN(strUpdateOBDTracking.ToString(), this.ConnectionString);

                if (Result == -1)
                {
                    response.Result = "-3";// "Error while updating delivery details"
                    return response;
                }
                else if (Result == -3)
                {
                    response.Result = "-4"; //"The selected store already delivered.Please check for another store from the 'Receiving Store' dropdown"
                    return response;
                }
                else if (Result == 1)
                {
                    response.Result = "1"; //Delivery details successfully updated
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

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<Payload<string>> WOComponentIssue_GetList(WOComponentIssue_GetListModel items)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            Payload<string> response = new Payload<string>();
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                string Query = " EXEC [USP_GetDataforWOPGI] @OutboundID = " + items.OutboundID + "";
                var DS = DbUtility.GetDS(Query, ConnectionString);

                var DS1 = DS.Tables[0].Clone();
                var DS2 = DS.Tables[0].Clone();

                foreach (DataRow row in DS.Tables[0].Rows)
                {
                    if (row["Quantity"] != DBNull.Value)
                    {
                        var QuantityValue = Convert.ToDecimal(row["Quantity"]);
                        if (QuantityValue > 0)
                        {
                            DS1.ImportRow(row);
                        }
                        else if (QuantityValue < 0)
                        {
                            DS2.ImportRow(row);
                        }
                    }
                }
                var result = new
                {
                    Table = DS1,
                    Table1 = DS2
                };

                response.Result = JsonConvert.SerializeObject(result);
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

        public async Task<Payload<string>> WOComponent_Initiate(QADRequestObj items)
        {
            Payload<string> response = new Payload<string>();
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
#pragma warning disable CS0219 // The variable 'ErrorCode' is assigned but its value is never used
                string ErrorCode = "";
#pragma warning restore CS0219 // The variable 'ErrorCode' is assigned but its value is never used
#pragma warning disable CS0168 // The variable 'urlParameters' is declared but never used
                string urlParameters;
#pragma warning restore CS0168 // The variable 'urlParameters' is declared but never used
                string qadurl = this.APIURL;
                string URL = "" + qadurl + "/Inbound/QADItemLevelWOPGI";
                var json = JsonConvert.SerializeObject(items);
                var result = await GetResponseFromQAD(URL, json);

                if (result != null && result.IsSuccessStatusCode)
                {
                    var jsonString = await result.Content.ReadAsStringAsync();
                    var item = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(jsonString);
                    var test = item.GetProperty("result").GetString();
                    var errorCode = item.GetProperty("error").GetString();

                    if (test == "success")
                    {
                        response.Result = "1"; //Initiated Successfully
                        return response;
                    }
                    else
                    {
                        //response.addError(ErrorCode);
                        response.Result = "-2";
                        return response; //Unexpected error from QAD..!An error occurred while sending the request.
                    }
                }
                else
                {
                    //response.addError(ErrorCode);
                    response.Result = "-3";
                    return response; // Error While Adjusting Stock
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

        public static async Task<HttpResponseMessage> GetResponseFromQAD(string URL, string json)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(URL);

                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(client.BaseAddress, content);

                return response;
            }
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<Payload<string>> AddDelvDocLineItem_Click(AddDelvDocLineItem_ClickModel items)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            Payload<string> response = new Payload<string>();
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                if (items.DeliveryStatusID == 4)
                {
                    response.Result = "-1";//"Outbound is already delivered"
                    return response;
                }

                string Query = "EXEC [dbo].[USP_GetCountGoodsMovementdata_OBDID] @OutboundID=" + items.OutboundID + "";
                int result = DbUtility.GetSqlN(Query, ConnectionString);

                if (result > 0)
                {
                    response.Result = "-2";//"Cannot modify the delivery doc details, as 'Goods-Out' process is started"
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

        public async Task<Payload<string>> Delete_DeliveryDocLineItems(Delete_DeliveryDocLineItemsModel items)
        {
            Payload<string> response = new Payload<string>();
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                if (items.DeliveryStatusID == 4)
                {
                    response.Result = "-1"; //"Outbound is already delivered"
                    return response;
                }

                if (items.DeliveryStatusID >= 2)
                {
                    response.Result = "-1"; //Outbound is already released
                    return response;
                }

                string Query = "EXEC [dbo].[USP_GetGoodsMovementdata_OBDID] @OutboundID = " + items.OutboundID + "";
                DataSet DS = DbUtility.GetDS(Query, this.ConnectionString);

                if (DS.Tables[0].Rows.Count > 0)
                {
                    response.Result = "-3"; //Cannot modify the delivery doc. details, as 'Goods-OUT' process is started
                    return response;
                }

                var sqlParams = new Dictionary<string, object>
                {
                    {"@SOHeaderID", items.SOHeaderID }
                };

                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "SP_USP_obd_updateSoheaderID", sqlParams).ConfigureAwait(false);
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


        public async Task<Payload<string>> GetPickingRevertData(SetOBDRevertNewModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    {"@OutboundId",items.OutboundId},
                    //{"@LoginAccountId",items.LoginAccountId},
                   // {"@LoginUserId",items.LoginUserId},
                    //{"@LoginTanentId",items.LoginTanentId},
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_OBD_TRN_GetOBDDetailsRevert", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> OBDLinePickRevert(SetOBDRevertNewModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    //{"AssignedID",items.AssignedID},
                    {"@AssignedID",items.AssignedID},
                    //{"sodetailsid",items.SODetailsID},
                    //{"husize",items.HUSize},
                    //{"batchno",items.BatchNo},
                    //{"mfgdate",items.MfgDate},
                    //{"expdate",items.ExpDate},
                    //{"serialno",items.SerialNo},
                    //{"@RevertTypeID",items.RevertTypeID},
                    {"@Qty_Revert",items.RevertQty},
                    {"@CreatedBy",items.CreatedBy},
                    //{"@outbound_customerpoid",items.SODetailsID},
                    //{"@qty",Convert.ToDecimal(items.Quantity)}
                    //{"isfullrevert",0},
                    //{"refcur","Ref_Cursor"},
            };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "SP_OBD_Revert_LineItems", sqlParams, true).ConfigureAwait(false);
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


        public async Task<Payload<string>> GenerateDeliveryPackSlip(GetPackingSlipDataModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    {"@OUTBOUNDID",items.OutboundID},
                    {"@PACKSLIPID",items.PackslipHeaderID}
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "Usp_Get_PackslipPrint", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> UpsertDockManagementData(ReceivingDockManagementDataModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {

                    StringBuilder UpsertReceivingDockManagement = new StringBuilder();
                    UpsertReceivingDockManagement.AppendLine("Exec USP_OBD_Upsert_Outbounddock");
                    UpsertReceivingDockManagement.AppendLine("@OutboundID = " + items.OutboundID + ",");
                    UpsertReceivingDockManagement.AppendLine("@DockID =" + items.OutboundDockId + ",");
                    UpsertReceivingDockManagement.AppendLine("@VehicleNo =" + DBLibrary.SQuote(items.VehicleRegNo) + ",");
                    UpsertReceivingDockManagement.AppendLine("@DriverName =" + DBLibrary.SQuote(items.DriverName) + ",");
                    UpsertReceivingDockManagement.AppendLine("@CreatedBy = " + items.LoginUserId + ",");
                    UpsertReceivingDockManagement.AppendLine("@LoginAccountId =" +items.LoginAccountId+ ",");
                    UpsertReceivingDockManagement.AppendLine("@LoginUserId =" +items.LoginUserId+ ",");
                    UpsertReceivingDockManagement.AppendLine("@LoginTanentId =" +items.LoginTanentId+ ",");
                    UpsertReceivingDockManagement.AppendLine("@VehicleTypeID =" +items.VehicleTypeID+ ",");
                    UpsertReceivingDockManagement.AppendLine("@FreightCompanyID =" +items.FreightCompanyID+ ",");
                    UpsertReceivingDockManagement.AppendLine("@drivermobileno =" + DBLibrary.SQuote(items.Drivercontactno) + ",");
                    UpsertReceivingDockManagement.AppendLine("@ReceivingStatus =" + items.ReceivingStatus + ",");
                    UpsertReceivingDockManagement.AppendLine("@VehicleWeight =" + DBLibrary.SQuote(items.VehicleWeight) + ",");
                    UpsertReceivingDockManagement.AppendLine("@LoadingPointID =" +items.LoadingPointID+ "");

                    DBFactory factory = new DBFactory();
                    IDBUtility DbUtility = factory.getDBUtility();
                    var DockCreated  = DbUtility.GetDS(UpsertReceivingDockManagement.ToString(), ConnectionString);
                    response.Result = DockCreated.Tables[0].Rows[0][0].ToString();
                    if (response.Result== "Success : Vehicle Gate In Successfully")
                    {
                        try
                        {
                            WhatAppNotes notes = new();
                            notes.OutboundID = items.OutboundID;
                            notes.ScenarioID = 1;
                             notes.VechileNumber = items.VehicleRegNo;
                            string result = await _whatsappservice.SendWAMBasedOnActivity(notes);
                        }
                        catch (Exception)
                        {

                            throw;
                        }

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
        public async Task<Payload<string>> GetDockManagementData(DashBoardInputModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>{
                  {"@OutboundId", items.OutboundId},
                  {"@DockId",items.DockId},
                  {"@LoginAccountId",items.LoginAccountId},
                  {"@LoginUserId",items.LoginUserId},
                  {"@LoginTanentId",items.LoginTanentId},
            };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "SP_OBD_GetOBDDock", sqlParams).ConfigureAwait(false);
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
    }
}




