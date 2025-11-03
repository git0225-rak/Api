using Simpolo_Endpoint.DAO.interfaces;
using Simpolo_Endpoint.DBUtil;
using Simpolo_Endpoint.Models;
using Simpolo_Endpoint.ModelsLibrary;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Microsoft.AspNetCore.SignalR.Protocol;

namespace Simpolo_Endpoint.DAO.Services
{
    public class HouseKeepingService : AppDBService, IHouseKeeping
    {
        Encryption objE = new Encryption();
        private readonly ISAPJsonPostService _jsonPostService;
        public HouseKeepingService(IOptions<AppSettings> appSettings, ISAPJsonPostService jsonPostService) : base(appSettings)
        {
            _jsonPostService = jsonPostService;
        }

        public async Task<Payload<string>> GetFastMovingLocsTransferList(GetFastMovingLocsTransferListModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                string Query = "EXEC [dbo].[sp_INV_GetFastMovementLocsFulfillOrders] @TransferTypeID=" + items.TransferTypeId + ", @AccountID=" + items.AccountID + ",@FulfillStatusID =" + items.StatusId + " , @UserID = " + items.UserID + " , @WareHouseID=" + items.WarehouseID + " , @Sapref=" + DBUtil.DBLibrary.SQuote(items.SAPReferenceNumber) + "";
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDatasetINQ(this.ConnectionString, Query).ConfigureAwait(false);
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



        public async Task<Payload<string>> GetTransferRequestNumbers_HHT(TransferRequestModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                string Query = "EXEC [dbo].[GetTransferRequestNumbers_HHT] @TenantID="+items.TenantID+",@UserID="+items.UserID+",@WarehouseID="+items.WarehouseId;
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDatasetINQ(this.ConnectionString, Query).ConfigureAwait(false);
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


        public async Task<Payload<string>> GetTransferRequestNumbers_HHT_PalletConsolidate(TransferRequestModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                string Query = "EXEC [dbo].[GetTransferRequestNumbers_HHT] @TenantID=" + items.TenantID + ",@UserID=" + items.UserID + ",@WarehouseID=" + items.WarehouseId;
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDatasetINQ(this.ConnectionString, Query).ConfigureAwait(false);
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
        public async Task<Payload<string>> DeleteFastMovingLocsTransfer(DeleteFastMovingLocsTransferModel items)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            Payload<string> response = new Payload<string>();
            try
            {
                string Query = "EXEC USP_ORD_UPD_INV_TransferRequest @TransferRequestID=" + items.TransferRequestID + ";EXEC USP_ORD_UPD_INV_TransferRequestDetails @TransferRequestID=" + items.TransferRequestID;
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                int ID = DbUtility.GetSqlN(Query, ConnectionString);
                if (ID == 0)
                {
                    response.Result = "1";
                }
                else
                {
                    response.Result = "2";
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


        public async Task<Payload<string>> ApprovedBatchGradeTransfer(GetInternalTransferHeaderModel items)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            Payload<string> response = new Payload<string>();
            string Result = "";
            string Message = "";
            int IsSuccess = 0;
            try
            {
                
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
         

                if(items.IsApproved==1)
                {
                    Task<string> RefNumber = _jsonPostService.SendMaterialTransferJSONDatatoSAP(items.TransferReqID);
                    if(RefNumber.Result.Contains("Success"))
                    {
                        IsSuccess = 1;
                        int startIndex = RefNumber.Result.IndexOf(":") + 1;
                        int endIndex = RefNumber.Result.IndexOf("-");
                        Result = RefNumber.Result.Substring(startIndex, endIndex - startIndex);
                    }
                    else
                    {
                        IsSuccess = 0;
                        Result = RefNumber.Result;
                    }
   
                    Message = ReserveStockBatchGrade(items.TransferReqID, items.UserID, Result,IsSuccess);
                    
                    
                }
                else
                {
                    string Query = "EXEC SP_Approver_ItemTransferBatchandGrade @UserID=" + items.UserID + ",@TransferID=" + items.TransferReqID + ",@IsApproved=" + items.IsApproved;
                     Message = DbUtility.GetSqlS(Query, ConnectionString);
                }

                response.Result = Message;

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


        public async Task<Payload<string>> RepostMaterialDetails_SAP(InitiateToProcessModel items)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            Payload<string> response = new Payload<string>();
            string Result = "";
            string Message = "";
            int IsSuccess = 0;
            try
            {

                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                Task<string> RefNumber = _jsonPostService.SendMaterialTransferJSONDatatoSAP(items.TransferRequestedID);
                if (RefNumber.Result.Contains("Success"))
                {
                    IsSuccess = 1;
                    int startIndex = RefNumber.Result.IndexOf(":") + 1;
                    int endIndex = RefNumber.Result.IndexOf("-");
                    Result = RefNumber.Result.Substring(startIndex, endIndex - startIndex);
                }
                else
                {
                    IsSuccess = 0;
                    Result = RefNumber.Result;
                }

               
                Message = ReserveStockBatchGrade(items.TransferRequestedID, items.UserID, Result, IsSuccess);
                response.Result = Message;

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


        public string ReserveStockBatchGrade(int TransferReqID,int UserID,string SapMaterialRefno,int IsSucess)
        {
            string uniqueID = DateTime.Now.ToString("ddhhmmss");
            
            string Query = "EXEC [dbo].[INTERNALSTOCK_RESERVE] @TransferRequestedID = " + TransferReqID + " , @CreatedBy = " + UserID + " ,@IsSAPSuccess="+IsSucess+" ,@SAPRefno = " + (SapMaterialRefno != null ? "'" + SapMaterialRefno + "'" : "''") + "";
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            int Result = DbUtility.GetSqlN(Query, ConnectionString);
            return Result.ToString();
        }






        public async Task<Payload<AuthResponce>> GetInternalTransferHeader(GetInternalTransferHeaderModel items)
        {
            Payload<AuthResponce> response = new Payload<AuthResponce>();
            try
            {
                AuthResponce authResponce = new AuthResponce();
                string Query = "EXEC [dbo].[GET_TRANSFERREQUEST_HEADER_INFO] @TransferReqID=" + items.TransferReqID;
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                var Data = await DbUtility.GetjsonDataFromDatasetINQ(this.ConnectionString, Query).ConfigureAwait(false);
                JObject data = JObject.Parse(Data);
                JArray table = (JArray)data["Table"];
                int TransferTypeID = (int)table[0]["TransferTypeID"];
                string GetTransferType = "Select TransferType from INV_TransferType where TransferTypeID= @TransferTypeID";
                SqlCommand command = new SqlCommand(GetTransferType);
                command.Parameters.AddWithValue("@TransferTypeID", TransferTypeID);
                var Output = await DbUtility.GetJsonDataFromCommand(this.ConnectionString, command);
                string TransferType = JsonConvert.SerializeObject(Output);
                authResponce.UserInfo = Data.ToString();
                authResponce.JsonToken = Output;
                response.Result = authResponce;
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
        public async Task<Payload<string>> UpsertInternalTransferHeader(UpsertInternalTransferHeaderModel items)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            Payload<string> response = new Payload<string>();
            try
            {
                string Query = "EXEC[dbo].[UPSERT_TRANSFERREQUEST] @TenatID = " + items.TenantID + ",@WarehouseId = " + items.WarehouseId + ", @TransferType = " + items.TransferTypeID + ", @Remarks = " + DBUtil.DBLibrary.SQuote(items.Remarks) + ", @UserID = " + items.UserID + ", @IsSuggestedReg = " + items.IsSuggestedReg + "";
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                int TransferRequestID = DbUtility.GetSqlN(Query, ConnectionString);
                if (TransferRequestID != 0)
                {
                    response.Result = TransferRequestID.ToString();
                }
                else
                {
                    response.Result = "-1";//Error While Creating   //Navigate to InternalTransferRequest.aspx
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

        public async Task<Payload<string>> GetInternalTransferDetails(GetInternalTransferDetailsModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                string Query = "EXEC [dbo].[GET_TRANSFER_REQUEST_DETAILS] @TransferRequestID=" + items.TransferRequestID;
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDatasetINQ(this.ConnectionString, Query).ConfigureAwait(false);
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



        public async Task<Payload<string>> GetInternalTransferDetails_VLPD(GetInternalTransferDetailsModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                string Query = "EXEC [dbo].[GET_TRANSFER_REQUEST_DETAILS_PickList] @TransferRequestID=" + items.TransferRequestID;
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDatasetINQ(this.ConnectionString, Query).ConfigureAwait(false);
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




        public async Task<Payload<string>> GetAvailableQty(GetAvailableQtyModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                    { "@TenantID" , items.TenantID },
                    { "@CartionID" , items.CartonID },
                    { "@LocationID" , items.LocationID },
                    { "@MaterialMasterID" , items.MaterialMasterID },
                    { "@BatchNo" , items.BatchNo },
                    { "@StorageLocationID" , items.StorageLocationID },
                    { "@ExpDate" , items.ExpDate },
                    { "@PojectRefNo" , items.PojectRefNo },
                    { "@IsProdOrder",items.IsProdOrder},

                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "SP_TRS_MATERIALQTY", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> UpsertInternalTransferDetails(UpsertInternalTransferDetailsModel items)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            Payload<string> response = new Payload<string>();
            try
            {
                DateTime? FromDateValue = null;
                DateTime? ToDateValue = null;
                string query = "EXEC [dbo].[UPSERT_TRANSFER_REQUEST_DETAILS] @TransferID = " + items.TransferRequestID
                + ", @CartonID  =" + items.cartonID + " , @LocationID = " + items.LocationID +
                " ,@MaterialMaterID =" + items.MaterialMasterID + " , @Quantity = " + items.Quantity
                + ",@BatchNo = " + DBUtil.DBLibrary.SQuote(items.BatchNo) + " ,@FromSL =" + items.FromSLID + "  , @ToSL = " + items.ToSL
                + ", @ToLocationID = " + items.ToLocationID + ",@ToMaterialMasterID=" + items.TOMMID + ",@ToBatchNo=" + DBUtil.DBLibrary.SQuote(items.TOBatchNo)
                + ", @ToGrade = " + DBUtil.DBLibrary.SQuote(items.ToGrade) + ",@FromGrade=" + DBUtil.DBLibrary.SQuote(items.FromGrade)
                + ",@CreatedBy=" + items.UserID+",@IsProdOrder="+items.IsProdOrder+",@ToCartonID="+items.ToCartonID;
                if (FromDateValue == null)
                {
                    query += ",@FromExpDate=NULL";
                }
                else
                {
                    query += ",@FromExpDate='" + FromDateValue + "'";
                }
                if (ToDateValue == null)
                {
                    query += ",@ToExpDate=NULL";
                }
                else
                {
                    query += ",@ToExpDate='" + ToDateValue + "'";
                }
                query += ",@ProjectRefNo= '" + items.ProjectRefNo + "'";

                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                string Result = DbUtility.GetSqlS(query, ConnectionString);
                //if (Result > 0)
                //{
                //    response.Result = "1";//Saved successfully
                //    return response;
                //}
                //if (Result == -2)
                //{
                //    response.Result = "-2";//Duplicate Record found
                //    return response;
                //}
                //if (Result == -3)
                //{
                //    response.Result = "-3";//Records should not be more than 10
                //    return response;
                //}
                //if (Result == -1)
                //{
                //    response.Result = "-1";//Quantity is not available for select Part Number
                //    return response;
                //}

                response.Result = Result;
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




#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<Payload<string>> UpsertExpiryDateTransferRequestDetails(UpsertExpiryDateTransferRequestDetailsModel items)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            Payload<string> response = new Payload<string>();
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                string Query = "EXEC [dbo].[UPSERT_EXPIRY_DATE_TRANSFER_REQUEST_DETAILS] @TransferID = " + items.TransferRequestID + ",@MaterialMaterID =" + items.MaterialMasterID + "," +
                               "@BatchNo=" + "'" + items.BatchNo + "'" + ",@FromSL=" + items.FromSLID + ",@FromExpDate=" + (string.IsNullOrEmpty(items.FromExpDate) ? "null" : "'" + items.FromExpDate + "'") + ",@ToExpDate=" + (string.IsNullOrEmpty(items.ToExpDate) ? "null" : "'" + items.ToExpDate + "'") + "," +
                               "@ProjectRefNo = " + (string.IsNullOrEmpty(items.ProjectRefNo) ? "null" : "'" + items.ProjectRefNo + "'") + "";

                int Result = DbUtility.GetSqlN(Query, ConnectionString);
                if (Result > 0)
                {
                    response.Result = "1";//Saved successfully
                    return response;
                }
                if (Result == -2)
                {
                    response.Result = "-2";//Duplicate Record found
                    return response;
                }
                if (Result == -3)
                {
                    response.Result = "-3";// Records should not be more than 10
                    return response;
                }
                if (Result == -1)
                {
                    response.Result = "-1";//Quantity is not available for select Part Number
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

        public async Task<Payload<string>> DeleteInternalTransferDetails(DeleteInternalTransferDetailsModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                    {"@TransferRequestID",items.TransferRequestDetailsID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                var count = await DbUtility.GetjsonData(this.ConnectionString, "USP_ORD_UPD_DeleteOrder", sqlParams).ConfigureAwait(false);
                if (count.Equals("[]"))
                {
                    response.Result = "1";
                    return response;
                }
                else
                {
                    response.Result = "2";
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

        public async Task<Payload<string>> GetCycleCountList(GetCycleCountListModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                    { "@AM_MST_Account_ID" , items.AccountID },
                    { "@UserId" , items.UserID },
                    { "@ZoneID" , items.ZoneID },
                    { "@CCM_CNF_AccountCycleCount_ID" , items.CCM_CNF_AccountCycleCount_ID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "SP_GET_CCM_CNF_AccountCycleCounts_List", sqlParams).ConfigureAwait(false);
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

        //DeleteCyclecountlist
        public async Task<Payload<string>> DeleteCyclecountlist(DeleteccOrderModel obj)
        {

            Payload<string> response = new Payload<string>();
            try
            {
                StringBuilder sb1 = new StringBuilder();
                sb1.Append("Exec USP_ORD_CCM_TRN_CycleCounts @CCID=" + obj.CCID + ";");
                sb1.Append("Exec USP_ORD_UPD_CCM_TRN_CycleCounts @CCID=" + obj.CCID);
                string Query = sb1.ToString();
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                var count = await DbUtility.GetjsonDataFromDatasetINQ(this.ConnectionString, Query).ConfigureAwait(false);
                JObject data = JObject.Parse(count);
                JArray table = (JArray)data["Table"];
                JArray table1 = (JArray)data["Table1"];
                int tranCount = (int)table[0]["TranCount"];
                int CompleteCount = (int)table1[0]["Completed"];
                if (tranCount > 0 || CompleteCount > 0)
                {
                    response.Result = "2"; //Cannot delete this Cycle Count, as it is already Initiated
                }
                else
                {
                    response = await DeleteCyclecountlistByID(obj);
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

        // Delete Verify 
        public async Task<Payload<string>> DeleteCyclecountlistByID(DeleteccOrderModel obj)
        {

            Payload<string> response = new Payload<string>();
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                StringBuilder sb = new StringBuilder();

                sb.Append("[dbo].USP_Delete_CCM_CNF_AccountCycleCounts");
                sb.Append(" @PK=" + obj.CCID + ", @LoggedInUserID=" + obj.UserID + ", @UTCTimestamp='" + (DateTime.UtcNow.ToString("dd-MMM-yyyy hh:mm:ss") + "'"));
                var Result = sb.ToString();
                var count = await DbUtility.GetjsonDataFromDatasetINQ(this.ConnectionString, Result).ConfigureAwait(false);
                if (count == "{}" || count == null)
                {
                    response.Result = "1";
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

        public async Task<Payload<string>> UpsertCycleCountHeader(UpsertCycleCountHeaderModel items)
        {
            Payload<string> response = new Payload<string>();
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                string json = JsonConvert.SerializeObject(items.XML);
                //string xml = JsonConvert.DeserializeXmlNode(json, "root").InnerXml;
                XmlDocument xmlData = JsonConvert.DeserializeXmlNode("{\"data\":" + json + "}", "root");

                string accountCycleCountName = xmlData.SelectSingleNode("//AccountCycleCountName")?.InnerText;

                string sqlQuery = $"SELECT COUNT(*) AS N FROM CCM_CNF_AccountCycleCounts WHERE CONVERT(NVARCHAR(MAX), AccountCycleCountName) LIKE N'%{accountCycleCountName}%';";

                int existingCount = DbUtility.GetSqlN(sqlQuery, ConnectionString);

                if (existingCount > 0)
                {
                    string checkUpdateQuery = $"SELECT COUNT(*) AS N FROM CCM_CNF_AccountCycleCounts WHERE CONVERT(NVARCHAR(MAX), AccountCycleCountName) LIKE N'%{accountCycleCountName}%' AND CCM_CNF_AccountCycleCount_ID = {items.CCM_CNF_AccountCycleCount_ID};";
                    int matchingCount = DbUtility.GetSqlN(checkUpdateQuery, ConnectionString);

                    if (matchingCount == 0)
                    {
                        response.Result = "-5"; // CycleCount name already exists and is not the record being updated
                        return response;
                    }
                }


                //var sqlParams = new Dictionary<string, object>
                //{
                //    { "@inputDataXml", xmlData.InnerXml },
                //    { "@LanguageType", items.LanguageType },
                //    { "@UpdatedBy", items.UpdatedBy },
                //    { "@CCM_CNF_AccountCycleCount_ID", items.CCM_CNF_AccountCycleCount_ID },
                //    { "@TenantId", items.TenantId }
                //};

                string Query = "EXEC [dbo].[USP_SET_CCM_CNF_AccountCycleCounts] @inputDataXml='" + (xmlData.InnerXml) + "',@LanguageType= '" + items.LanguageType + "' , @UpdatedBy = " + items.UpdatedBy + ",@CCM_CNF_AccountCycleCount_ID=" + items.CCM_CNF_AccountCycleCount_ID + ",@TenantId=" + items.TenantId + "";
                //var Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "USP_SET_CCM_CNF_AccountCycleCounts", sqlParams).ConfigureAwait(false);
                var DS = DbUtility.GetDS(Query, this.ConnectionString);
                response.Result = JsonConvert.SerializeObject(DS);
                //if (tranCount == 1)
                //{
                //    response.Result = "1";//User can update only Frequency And Valid To, As CC Is Already Initiated
                //    return response;
                //}
                //else if (tranCount == 2)
                //{
                //    response.Result = "2"; //User can update only Valid To,  As CC Is Already Completed
                //    return response;
                //}
                //else if (tranCount == 3)
                //{
                //    response.Result = "3";//Cannot update this Cycle Count, as there is no Sequence.
                //    return response;
                //}
                //else
                //{
                //    response.Result = "0";
                //    return response;// Created Successfully 
                //}
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

        public async Task<Payload<string>> GetCycleCountEntityConfiguration(GetCycleCountEntityConfigurationModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                    { "AM_MST_Account_ID" , items.AM_MST_Account_ID },
                    { "CCM_CNF_AccountCycleCount_ID" , items.CCM_CNF_AccountCycleCount_ID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "SP_GET_CCM_CNF_AccountCycleCounts", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetCycleCountTransactionList(GetCycleCountlistModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                    { "AccountID", items.AccountID },
                    { "UserId", items.UserId }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                var Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "USP_GET_CCM_TRN_CycleCounts", sqlParams).ConfigureAwait(false);
                response.Result = Result;
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

        public async Task<Payload<string>> GetCycleCountTransactionListByStatus(GetCycleCountTransactionListModel getCycleCountTransaction)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                    {"@AccountID",getCycleCountTransaction.AccountID },
                    {"@UserId",getCycleCountTransaction.UserId }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "USP_GET_CCM_TRN_CycleCounts_Ascending", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetCCBlockedLocations(GetCCBlockedLocationsModel obj)
        {

            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "CCM_CNF_AccountCycleCount_ID" , obj.CCM_CNF_AccountCycleCount_ID },
                    { "AM_MST_Account_ID" , obj.AM_MST_Account_ID },
                    { "CCM_TRN_CycleCount_ID" , obj.CCM_TRN_CycleCount_ID },
                    { "LocationID" , obj.LocationID },
                    { "Prefix" , obj.prefix }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "SP_GET_CycleCount_BlockedLocations", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetStockAadjustmentNewList(GetStockAadjustmentNewListModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "@CCM_TRN_CycleCount_ID" , obj.CCM_TRN_CycleCount_ID },
                    { "@Rownumber" , obj.Rownumber },
                    { "@NofRecordsPerPage" , obj.NofRecordsPerPage }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "sp_CCM_CycleCountReport", sqlParams).ConfigureAwait(false);
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



        // Cycle Count Details - Entity Config
        public async Task<Payload<string>> DeleteCyclecountDetails(DeleteccOrderModel obj)
        {

            Payload<string> response = new Payload<string>();
            try
            {
                StringBuilder sb1 = new StringBuilder();
                sb1.Append("Exec USP_ORD_CCM_TRN_CycleCounts @CCID=" + obj.CCID + ";");
                sb1.Append("Exec USP_ORD_UPD_CCM_TRN_CycleCounts @CCID=" + obj.CCID);
                string Query = sb1.ToString();
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                var count = await DbUtility.GetjsonDataFromDatasetINQ(this.ConnectionString, Query).ConfigureAwait(false);
                JObject data = JObject.Parse(count);
                JArray table = (JArray)data["Table"];
                JArray table1 = (JArray)data["Table1"];
                int tranCount = (int)table[0]["TranCount"];
                int CompleteCount = (int)table1[0]["Completed"];
                if (tranCount > 0 || CompleteCount > 0)
                {
                    response.Result = "1"; //Cannot delete this Cycle Count, as it is already Initiated
                }
                else
                {
                    response = await DeleteCyclecountDetailsByID(obj);
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
        // Cycle Count Details verify
        public async Task<Payload<string>> DeleteCyclecountDetailsByID(DeleteccOrderModel obj)
        {

            Payload<string> response = new Payload<string>();
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                StringBuilder sb = new StringBuilder();

                sb.Append("[dbo].USP_Delete_CCM_CNF_AccountCycleCountDetails");
                sb.Append(" @PK=" + obj.ID + ", @LoggedInUserID=" + obj.UserID);
                var Result = sb.ToString();
                var count = await DbUtility.GetjsonDataFromDatasetINQ(this.ConnectionString, Result).ConfigureAwait(false);
                if (count == "{}" || count == null)
                {
                    response.Result = "2";
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


        public async Task<Payload<string>> CreateCC_EntityConfiguration(CreateCC_EntityConfigurationModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                CCXMLData entitydata = new CCXMLData();
                entitydata.ccxmldata = items.data;
                XmlSerializer serializer = new XmlSerializer(typeof(CCXMLData));
                string finalxml = "";
                using (var stream = new StringWriter())
                {
                    serializer.Serialize(stream, entitydata);
                    string xml = stream.ToString();
                    finalxml = xml.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");
                    finalxml = finalxml.Replace(" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", "");
                }
                var sqlParams = new Dictionary<string, object>
                {
                    { "@inputDataXml" , finalxml },
                    { "@CCM_CNF_AccountCycleCountDetail_ID" , items.CycleCount_ID },
                    { "@UpdatedBy" , items.UserID },
                    { "@MMID" , items.MaterialMasterID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_SET_CCM_CNF_AccountCycleCountsDetails", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetCC_ShipmentVerificationDetails(GetCC_ShipmentDetailsModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    { "@CCM_CNF_AccountCycleCount_ID" , items.CycleCount_ID },
                    { "@AM_MST_Account_ID" , items.AccountID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "SP_GET_CCM_CNF_AccountCycleCounts", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> SaveCC_ShipmentVerificationDetails(SaveCC_ShipmentDetailsModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    { "@ccid" , items.ccid },
                    { "@Remarks" , items.Remarks },
                    { "@ShipmentVerifiedDate" , items.ShipmentVerifiedDate },
                    { "@filename" , items.filename }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                var Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "USP_INL_UPD_CCM_CNF_AccountCycleCounts", sqlParams).ConfigureAwait(false);
                if (Result == "{}" || Result == null)
                {
                    response.Result = "1"; //Save
                }
                else
                {
                    response.Result = "2"; //Error
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

        public async Task<Payload<string>> UpsertCCtransactionInitiate(UpsertCCtransactionModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                string SP_Name = "[dbo].[USP_GET_CCM_TRN_LocationFilterQueryWithActiveStock]";
                var obj = new Dictionary<string, object>();
                //var time = (DateTime.UtcNow.ToString("dd-MMM-yyyy hh:mm:ss"));
                DateTime now = DateTime.Now;
                string time = now.ToString("MMM dd, yyyy HH:mm:ss");
                var sqlParams = new Dictionary<string, object>  {

                    { "@CCM_TRN_CycleCount_ID" , items.CCM_TRN_CycleCount_ID },
                    { "@CCM_CNF_AccountCycleCount_ID" , items.CCM_CNF_AccountCycleCount_ID },
                    { "@LoggedInUserID" , items.LoggedInUserID },
                    { "@InitiatedTimestamp" ,  time }

                };
                foreach (var item in sqlParams)
                {
                    obj.Add(item.Key, item.Value);
                }
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                var json = JsonConvert.SerializeObject(obj);
                Dictionary<dynamic, dynamic> values = JsonConvert.DeserializeObject<Dictionary<dynamic, dynamic>>(JsonConvert.DeserializeObject(json).ToString());
                StringBuilder sb = new StringBuilder();
                int count = 0;
                foreach (KeyValuePair<dynamic, dynamic> pair in values)
                {
                    if (count != 0)
                        sb.Append(",");
                    sb.Append(pair.Key + "=" + DBUtil.DBLibrary.SQuote(pair.Value.ToString()));
                    count++;
                }
                string AP = sb.ToString();
                string Append = "EXEC " + SP_Name + " " + AP;

                var data = await DbUtility.GetjsonDataFromDatasetINQ(this.ConnectionString, Append);
                if (data == null || data == "{}")
                {
                    response.Result = "1";//Success
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


            public async Task<Payload<string>> UpsertBinToBinTransferItem(UpsertBinToBinTransferModel items)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
            {
            Payload<string> response = new Payload<string>();
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                string Result1 = DbUtility.GetSqlS("EXEC Upsert_BinToBinTransfer_HHT @MaterialMasterID =" + items.MaterialMasterID + ",@ToLocationID =" + items.ToLocationID + " , @FromLocation ='" + items.FromLocation + "',  @Quantity = " + items.TransferQty + ", @BatchNo ='" + items.BatchNo + "', @ExpDate ='" + items.ExpDate + "',@MfgDate = '" + items.MfgDate + "', @SerialNo = '" + items.SerialNo + "', @MRP = '" + items.MRP + "', @ProjectRefNo = '" + items.ProjectNo + "', @CreatedBy =" + items.UserId + ", @FromCartonID =" + items.FromCartonID + ", @ToCartonID =" + items.ToCartonID + ", @MCODE = '" + items.MCode + "', @TLoc = '" + items.ToLocation + "', @FromCarton =' " + items.FromCartonNo + "', @ToCarton = '" + items.ToCartonNo + "', @FromSLOC = '" + items.FromSLoc + "', @ToSLOC = '" + items.ToSLoc + "', @TenantID =" + items.TenantID + ", @WarehouseID = " + items.WarehouseID + ", @IsWorkOrder =" + items.IsWorkOrder + ", @EmpreqNumber = '" + items.EmpreqNumber + "',@TransferID=" + items.TransferRequestID, this.ConnectionString);


                //int tranCount = (int)Result.Tables[0].Rows[0]["N"];
                //if (tranCount == -2)
                //{
                //    response.Result = "Error :Mis Matched container Configuration";
                //    return response;
                //}
                //else if (tranCount == -3)
                //{
                //    response.Result = "Error :Container is Configured to different Loc.";
                //    return response;
                //}
                //else if (tranCount == -4)
                //{
                //    response.Result = "Error : No stock available";
                //    return response;
                //}
                //else if (tranCount == -5)
                //{
                //    response.Result = "Error : Quantity exceeded";
                //    return response;
                //}
                //else if (tranCount == -6)
                //{
                //    response.Result = "Error : Another item already exist in the bin";
                //    return response;
                //}

                   response.Result = Result1;
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

        public async Task<Payload<string>> GetUnmappedSKUList(GetUnmappedSKUListModel items)
        {

            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
            {
                {"@TenantID",items.TenantID},
                {"@MaterialID",items.MaterialMasterID},
                {"@WarehouseID",items.WarehouseID},
                {"@UserID",items.UserID}
            };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "USP_GET_SKUMAPPINGLIST", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> SaveUnmappedZoneSKUData(GetUnmappedSKUListModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();

                XmlSerializer serializer = new XmlSerializer(typeof(List<SKUDetails>), new XmlRootAttribute("root"));
                string OutputXML = "";
                using (var stream = new StringWriter())
                {
                    serializer.Serialize(stream, obj.DataJson);
                    string xml = stream.ToString();
                    OutputXML = xml.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");
                    OutputXML = OutputXML.Replace("xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", "");
                }
                XDocument xmlDoc = XDocument.Parse(OutputXML);
                foreach (var element in xmlDoc.Descendants("XMLFormat").ToList())
                {
                    element.Name = "data";
                }
                string updatedXmlString = xmlDoc.ToString();
                var sqlParams = new Dictionary<string, object> {
                {"@inputDataXml", "'"+updatedXmlString+"'" },
                {"@TenantID",obj.TenantID },
                {"@WarehouseID",obj.WarehouseID },
                {"@UserID", obj.UserID}
                };
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "USP_UPSERT_SKUMAPPING", sqlParams).ConfigureAwait(false);
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