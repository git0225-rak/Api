using Simpolo_Endpoint.DAO.interfaces;
using Simpolo_Endpoint.DBUtil;
using Simpolo_Endpoint.Models;
using Simpolo_Endpoint.ModelsLibrary;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;



namespace Simpolo_Endpoint.DAO.Services
{
    public class OrdersService : AppDBService, IOrders
    {
        private readonly ISAPJsonPostService _jsonPostService;
        public OrdersService(IOptions<AppSettings> appSettings, ISAPJsonPostService jsonPostService) : base(appSettings)
        {
            _jsonPostService = jsonPostService;
        }
        public async Task<Payload<string>> GetStockPosting(GetStockPostingModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {

                    { "POHeaderID" , obj.POHeaderID },
                    { "MMID" , obj.MMID },

                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "sp_INV_GetStockPosting", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetCurrentStockDynamicData(GetCurrentStockDynamicDataModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {

                    { "POHeaderID" , obj.POHeaderID },
                    { "MMID" , obj.MMID },

                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "sp_INV_GetStockPosting", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetSuppliersReturns(GetSuppliersReturnsModel obj)
        {

            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {

                { "POHeaderID" , obj.POHeaderID },
                { "SupplierInvoiceID" , obj.SupplierInvoiceID },
                { "WarehouseID" , obj.WarehouseID },

                };

                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "sp_INV_GetSupplierReturnMaterial_NEW", sqlParams).ConfigureAwait(false);
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
                var sqlParams = new Dictionary<string, object>  
                {
                    { "CCM_CNF_AccountCycleCount_ID" , obj.CCM_CNF_AccountCycleCount_ID },
                    { "AM_MST_Account_ID" , obj.AM_MST_Account_ID },
                    { "CCM_TRN_CycleCount_ID" , obj.CCM_TRN_CycleCount_ID },
                    { "Prefix" , obj.prefix },
                    { "LocationID" , obj.LocationID },
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

                { "CCM_TRN_CycleCount_ID" , obj.CCM_TRN_CycleCount_ID },
                { "Rownumber" , obj.Rownumber },
                { "NofRecordsPerPage" , obj.NofRecordsPerPage },
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
        public async Task<Payload<string>> GetStockPostingDetails(GetStockPostingInputModel getStockPostingInput)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                    {"@POHeaderID",getStockPostingInput.POHeaderID },
                    {"@MMID",getStockPostingInput.MMID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "sp_INV_GetStockPosting", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> UpdateEmployeeHeaderData(UpdateEmployeeHeaderDataModel updateEmployee)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                    {"@RequestHeaderID",updateEmployee.RequestHeaderID },
                    {"@RequestTypeID",updateEmployee.RequestTypeID },
                    {"@SAPIssuePosting_MDNo",updateEmployee.@SAPIssuePosting_MDNo },
                    {"@SupplierID",updateEmployee.SupplierID },
                    {"@DockLocationID",updateEmployee.DockLocationID },
                    {"@EmpRequestStatusID",updateEmployee.EmpRequestStatusID },
                    {"@AccountID",updateEmployee.AccountID },
                    {"@TenantID",updateEmployee.TenantID },
                    {"@CreatedBy",updateEmployee.CreatedBy },
                    {"@PONumber",updateEmployee.PONumber }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "SP_Upsert_EmployeeRequestHeader", sqlParams).ConfigureAwait(false);
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                response.Result = ("Error while submitting the data");
            }
            return response;
        }
        public async Task<Payload<string>> SaveTransferRequest(SaveTransferRequestModel updateEmployee)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    {"UserID",updateEmployee.UserID },
                    {"PONumber",updateEmployee.PONumber },
                    {"XML",updateEmployee.XML },
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_UpserStockPostingTransferRequest", sqlParams).ConfigureAwait(false);
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                response.Result = ("Error while submitting the data");
            }
            return response;
        }


        public async Task<Payload<string>> GetSupplierReturnlist(GetSupplierReturnlistModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    {"@POHeaderID",items.POHeaderID },
                    {"@SupplierInvoiceID",items.SupplierInvoiceID },
                    {"@WareHouseID",items.WareHouseID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "sp_INV_GetSupplierReturnMaterial_NEW", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> StockPosting(StockPostingModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();

                var emptyNamepsaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
                emptyNamepsaces.Add("", "");
                XmlSerializer serializer = new XmlSerializer(typeof(List<XMLposting>), new XmlRootAttribute("xml"));
                string OutputXML = "";
                using (var stream = new StringWriter())
                {
                    serializer.Serialize(stream, items.xmlpost, emptyNamepsaces);
                    string xml = stream.ToString();
                    OutputXML = xml.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");
                    OutputXML = OutputXML.Replace("xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", "");
                }
                XDocument xmlDoc = XDocument.Parse(OutputXML);
                foreach (var element in xmlDoc.Descendants("XMLposting").ToList())
                {
                    element.Name = "Item";
                }
                string updatedXmlString = xmlDoc.ToString();

                var sqlParams = new Dictionary<string, object>
                {
                    { "@UserID" , items.UserID },
                    { "@XML" , "'"+updatedXmlString+"'" },
                    { "@PONumber" , "'"+items.PONumber+"'" }
                };

                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_UpserStockPostingTransferRequest", sqlParams).ConfigureAwait(false);
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
        //MaterialDetails//Actions
        public async Task<Payload<string>> MaterialQtyUpdate(MaterialQtyUpdateModel obj)
        {
            Payload<string> response = new Payload<string>();
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                string Query = "Exec [dbo].[USP_ORD_INL_ORD_ORD_EmployeeRequestHeader] @RequestHeaderID=" + obj.HeaderID + "";
                string result = await DbUtility.GetjsonDataFromDatasetINQ(this.ConnectionString, Query);
                JObject data = JObject.Parse(result);
                JArray table = (JArray)data["Table"];
                int EmpRequestStatusID = (int)table[0]["EmpRequestStatusID"];
                if (EmpRequestStatusID == 1)
                {
                    if (obj.RequestedQuantity > obj.ReceivedQuantity)
                    {
                        response.Result = "-1";//Requested Qty exceeded received Qty
                        return response;
                    }
                }

                var sqlParams = new Dictionary<string, object>
                {
                    { "Quantity" , obj.RequestedQuantity },
                    { "HeaderID" , obj.HeaderID },
                    { "LineNumber" , obj.LineNumber },
                    { "MaterialMasterID" , obj.MaterialMasterID },
                    { "BatchNo" , obj.BatchNo },
                    { "FromStorageLocationID" , obj.FromStorageLocationID },
                    { "RequestDetailsID" , obj.RequestDetailsID },
                    { "UserID" , obj.UserID },
                    { "ProjectRefNo" , obj.ProjectRefNo },
                };

                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "SP_UPDATE_EMPDETAILSQTY", sqlParams).ConfigureAwait(false);
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


        //MaterialDetails for confirm button
        public async Task<Payload<string>> EmployeeRequestConfirmation(EmployeeRequestConfirmationModel obj)
        {
            Payload<string> response = new Payload<string>();
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            string qadaccount = obj.QADAccount;
            string ErrorCode = "", test = "";
            DataSet DS = null;
            string urlParameters;
            try
            {
                int EmpRequestStatusID = 0;
                string sp = "SELECT EmpRequestStatusID FROM ORD_EmployeeRequestHeader WHERE RequestHeaderID = @RequestHeaderID";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sp, connection))
                    {
                        command.Parameters.AddWithValue("@RequestHeaderID", obj.RequestHeaderID);
                        EmpRequestStatusID = (int)command.ExecuteScalar();
                    }
                }

                if (EmpRequestStatusID > 1)
                {
                    string Query = "EXEC [dbo].[sp_INV_GET_SamplingQADDataForMiscOut] @RequestHeaderID = " + obj.RequestHeaderID + ",@QONumber = " + DBLibrary.SQuote(obj.QONumber) + ",@QADAccount=" + DBLibrary.SQuote(obj.QADAccount) + ",@Remarks=" + DBLibrary.SQuote(obj.Remarks) + "";
                    DS = DbUtility.GetDS(Query, this.ConnectionString);
                }

                if (DS.Tables[0].Rows.Count > 0)
                {
                    var IsWMSError = Convert.ToInt32(DS.Tables[0].Rows[0]["IsWMSError"]);
                    var SODetailsID = Convert.ToInt32(DS.Tables[0].Rows[0]["SODetailsID"]);
                    var RequestDetailsID = Convert.ToInt32(DS.Tables[0].Rows[0]["RequestDetailsID"]);
                    var SOHeaderID = Convert.ToInt32(DS.Tables[0].Rows[0]["SOHeaderID"]);
                    var VLPDAssignID = Convert.ToInt32(DS.Tables[0].Rows[0]["VLPDAssignID"]);
                    var BatchNo = Convert.ToString(DS.Tables[0].Rows[0]["BatchNo"]);
                    var projectRefNo = Convert.ToString(DS.Tables[0].Rows[0]["ProjectRefNo"]);
                    var Quantity = Convert.ToDecimal(DS.Tables[0].Rows[0]["Quantity"]);
                    var remarks = Convert.ToString(DS.Tables[0].Rows[0]["remarks"]);
                    var Mcode = Convert.ToString(DS.Tables[0].Rows[0]["Mcode"]);
                    var UoM = Convert.ToString(DS.Tables[0].Rows[0]["UoM"]);
                    if (IsWMSError == 0)
                    {
                        string qadurl = this.APIURL;
                        string URL = "" + qadurl + "/Inbound/GetMiscRecptXMLData";
                        urlParameters = "?batchNo=" + BatchNo + "&&projectRefNo=" + projectRefNo + "&&Qty=" + Quantity + "&&remks=" + remarks + "&&part=" + Mcode + "&&um=" + UoM + "&&val=" + -1 + "&&qadAccount=" + qadaccount + "&&QADLocation=Holding";
                        var result = GetResponseFromQAD(URL, urlParameters);
                        if (result.IsSuccessStatusCode)
                        {
                            var jsonString = await result.Content.ReadAsStringAsync();
                            var item = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(jsonString);
                            test = item.GetProperty("result").GetString();
                            ErrorCode = item.GetProperty("errorcode").GetString();

                            if (test == "success")
                            {
                                response.Result = "1";
                                //return response;
                            }
                            else
                            {
                                response.Result = "-1";//Error Code
                                return response;
                            }
                        }
                        else
                        {
                            response.Result = "-3";
                            return response; //Connection Error                                              
                        }
                    }
                    else
                    {
                        test = "success";
                        ErrorCode = "";
                    }

                    int IsQADSuccess = test == "success" ? 1 : 0;

                    string queryresult = "EXEC [dbo].[sp_INV_SET_SamplingOutStock] @SoDetailsID=" + SODetailsID + ",@RequestDetailsID=" + RequestDetailsID + ",@SOHeaderID=" + SOHeaderID + ",@BatchNo='" + BatchNo + "',@Quantity='" + Quantity + "'" +
                    ",@CreatedBy=" + obj.UserID + ",@AssignedId=" + VLPDAssignID + ",@IsQADSuccess=" + IsQADSuccess + ",@Error='" + ErrorCode + "'";

                    var Dataset = DbUtility.GetDS(queryresult, this.ConnectionString);
                }
                else
                {
                    //test = "error";
                    //ErrorCode = "No data found";
                    response.ResponseCode = "-4";//No data found
                }
                response.Result = "2";
                return response;// "The Request has been processing..!";
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
        public async Task<Payload<string>> GetEmployeeRequestForm(EmployeeRequestVerificationModel obj)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            Payload<string> response = new Payload<string>();
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {               
                string Query = "EXEC [dbo].[sp_Get_ORD_EmployeeRequestFormDetails_New] @RequestHeaderID = " + obj.RequestHeaderID + ",@Rownumber = " + obj.Rownumber + ",@NofRecordsPerPage=" + obj.NofRecordsPerPage + "";
                var DS = DbUtility.GetDS(Query, this.ConnectionString);

                DataColumn rowNumberColumn = new DataColumn("RowNumber", typeof(int));
                DS.Tables[2].Columns.Add(rowNumberColumn);

                int rowCount = 1; 
                foreach (DataRow row in DS.Tables[2].Rows)
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


        public async Task<Payload<string>> InitiateStock(InitiateStockModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                string finalResult = "";

                var serializer = new XmlSerializer(typeof(List<Dataitems>), new XmlRootAttribute("Materials"));
                using (var stream = new StringWriter())
                {
                    serializer.Serialize(stream, obj.DataJson);
                    string xmlResult = stream.ToString();

                    finalResult = xmlResult.ToString().Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n", " ");
                }
                var sqlParams = new Dictionary<string, object> {
                    {"@CCID",+obj.CCM_TRN_CycleCount_ID },
                    {"@UserID",+obj.UserID },
                  {"@XMLData", "'"+finalResult+"'" }
                };
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "SP_Upsert_MiscTrnascations", sqlParams).ConfigureAwait(false);
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


        public async Task<Payload<string>> InitiateToInProcess(InitiateToProcessModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                string Query = "Exec USP_ORD_INL_InitiateToInProcess @TransferRequestID=" + items.TransferRequestedID;
                var ds =  DbUtility.GetDS( Query,this.ConnectionString);
                int StatusID = 0;
                int TransferTypeID = 0;
                string uniqueID = "";
                int SAPCall = 0;
                if (ds.Tables.Count >0)
                {
                     StatusID =  Convert.ToInt32(ds.Tables[0].Rows[0]["StatusID"]);
                     TransferTypeID = Convert.ToInt32(ds.Tables[0].Rows[0]["TransferTypeID"]);
                     uniqueID = DateTime.Now.ToString("ddhhmmss");
                     SAPCall = Convert.ToInt32(ds.Tables[0].Rows[0]["SAPCALL"]);
                }
                if(StatusID==10)
                {
                    Task<string> RefNumber = _jsonPostService.SendMaterialTransferJSONDatatoSAP_Unblock(items.TransferRequestedID);
                    if (RefNumber.Result.Contains("Success"))
                    {
                        int startIndex = RefNumber.Result.IndexOf(":") + 1;
                        int endIndex = RefNumber.Result.IndexOf("-");
                        string Result = RefNumber.Result.Substring(startIndex, endIndex - startIndex);
                        string SQL = DbUtility.GetSqlS("Exec SP_Upsert_UnblockItems @TransferRequestID=" + items.TransferRequestedID + ",@UserID=" + items.UserID+",@UnblockNumber="+DBLibrary.SQuote(Result), this.ConnectionString);
                        response.Result = SQL;
                        return response;

                    }
                    else
                    {
                        response.addError(RefNumber.Result);
                    }
                }

                if (TransferTypeID == 5)
                {
                    if (ds.Tables.Count > 1)
                    {


                        int strTransferRequestID = 0;
                        strTransferRequestID += items.TransferRequestedID;

                        string result = await PostDataToSAPMaterialTransfer(items.TransferRequestedID, items.UserID);

                        if (result.Contains("SAP Error:"))
                        {

                            response.Result = "-1";
                            response.addError(result);
                        }
                        else if (result.Contains("Error:"))
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
                            string saprefno = "";
                            string[] Sapno = result.Split(":");

                            saprefno = Sapno[1];

                            items.SapMaterialRefno = saprefno;

                            await ReserveStock(items);
                            return response;

                            response.Result = "1"; //"sloc to sloc  initiated  successfully"

                        }
                        else
                        {
                            throw new Exception("Error while establishing the connection");
                        }
                    }

                    else
                    {
                        await ReserveStock(items);
                        return response;
                    }

                }
                else if (TransferTypeID == 4)
                {

                    if (StatusID == 1)
                    {
                        await ReserveStock(items);
                        return response;
                    }
                    else
                    {
                        return response;
                    }
                }
                else if(TransferTypeID==13)
                {
                    int IsSuccess = 0;
                    string Result = "";

                    if (TransferTypeID == 13)
                    {
                        Payload<string> Respone = await ReserveStockBatchandGrade(items);
                        Task<string> RefNumber = _jsonPostService.SendMaterialTransferJSONDatatoSAP(items.TransferRequestedID);
                        if (RefNumber.Result.Contains("Success"))
                        {
                            IsSuccess = 1;
                            int startIndex = RefNumber.Result.IndexOf(":") + 1;
                            int endIndex = RefNumber.Result.IndexOf("-");
                            Result=RefNumber.Result.Substring(startIndex, endIndex - startIndex);
                            response.Result = "Successfully Initiated";
                            
                        }
                        else
                        {
                            IsSuccess = 0;
                            Result = RefNumber.Result;
                            response.Result = Result;
                        }

                        string Sql = DbUtility.GetSqlS("Exec SP_Update_TransferStaus @TransferRequestID=" + items.TransferRequestedID + ",@IsSuccess=" + IsSuccess + ",@SAPRefNumber=" + DBLibrary.SQuote(Result), this.ConnectionString);
                        return response;
                    }

                }
                else if(TransferTypeID==15)
                {
                    string SQL = DbUtility.GetSqlS("Exec SP_Upsert_AssignedItems_TransferRequest @TransferRequestID=" + items.TransferRequestedID + ",@UserID=" + items.UserID, this.ConnectionString);
                    response.Result = SQL;
                        return response;
                }
                else
                {
                    if (StatusID == 2)
                    {
                        await ReserveStockBatchandGrade(items);
                        return response;
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


        public async Task<string> PostDataToSAPMaterialTransfer(int TransferRequestID, int UserID)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(1200);
                    string sapurl = this.ServiceURL;
                    string URL = $"{ServiceURL}/SAPIntegration/GenerateInventoryMovementSLocToSLoc";
                    string urlParameters = $"?TransferRequestedID={Uri.EscapeDataString(TransferRequestID.ToString())}&UserID={Uri.EscapeDataString(UserID.ToString())}";

                    //string urlParameters = $"?TransferRequestedID={TransferRequestID}";

                    HttpResponseMessage result = await client.PostAsync(URL + urlParameters, null);

                    if (result.IsSuccessStatusCode)
                    {

                        string SAP_MaterialNo = await result.Content.ReadAsStringAsync();
                        return SAP_MaterialNo;
                    }
                    else
                    {
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


        public async Task<Payload<string>> QADInternalTransferPosting(int TransferRequestID, int TransferTypeID, string uniqueID)
        {
            Payload<string> response = new Payload<string>();
            string errorCode = "";
            try
            {
                string urlParameters;
                string qadurl = this.APIURL;
                string URL = "" + qadurl + "/Inbound/QADUpdateInventorystatus";
                //URL = "" + " http://localhost/FWMSC21_GSK_API/ " + "/Inbound/QADUpdateInventorystatus";
                urlParameters = "?TransferRequestID=" + TransferRequestID + "&&TransferTypeID=" + TransferTypeID + "&&uniqueID=" + uniqueID + "";
                var result = GetResponseFromQAD(URL, urlParameters);
                if (result.IsSuccessStatusCode)
                {
                    var jsonString = await result.Content.ReadAsStringAsync();
                    var item = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(jsonString);
                    var test = item.GetProperty("result").GetString();
                    errorCode = item.GetProperty("errorcode").GetString();
                    if (test == "success")
                    {
                        response.Result = "1";
                        return response;
                    }
                    else
                    {
                        response.Result = errorCode;
                        return response; //Error Code
                    }
                }
                else
                {
                    response.Result = "-2";
                    return response; //Connection Error
                }            
            }
#pragma warning disable CS0168 // The variable 'Ex' is declared but never used
            catch (Exception Ex)
#pragma warning restore CS0168 // The variable 'Ex' is declared but never used
            {
                response.addError(errorCode);
                response.Result = "-3";//QAD Error
            }
            return response;
        }

        public static HttpResponseMessage GetResponseFromQAD(string URL, string urlParameters)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);


            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = client.GetAsync(urlParameters).Result;
            return response;
        }
        public async Task<Payload<string>> ReserveStock(InitiateToProcessModel items)
        {
            string uniqueID = DateTime.Now.ToString("ddhhmmss");
            Payload<string> response = new Payload<string>();

            string Query = "EXEC [dbo].[INTERNALSTOCK_RESERVE] @TransferRequestedID = " + items.TransferRequestedID + " , @CreatedBy = " + items.UserID + " , @SAPRefno = " + (items.SapMaterialRefno != null ? "'" + items.SapMaterialRefno + "'" : "''")+"";
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            int Result = DbUtility.GetSqlN(Query, ConnectionString);
            response.Result = Result.ToString();
            return response;
        }

        public async Task<Payload<string>> ReserveStockBatchandGrade(InitiateToProcessModel items)
        {
            string uniqueID = DateTime.Now.ToString("ddhhmmss");
            Payload<string> response = new Payload<string>();

            string Query = "EXEC [dbo].[INTERNALSTOCK_RESERVE_BatchGrade] @TransferRequestedID = " + items.TransferRequestedID + " , @CreatedBy = " + items.UserID + " , @SAPRefno = " + (items.SapMaterialRefno != null ? "'" + items.SapMaterialRefno + "'" : "''") + "";
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            string Result = DbUtility.GetSqlS(Query, ConnectionString);
            response.Result = Result.ToString();
            return response;
        }

        public async Task<Payload<string>> CompleteMasterDetailsSetLFO(MasterDetailsSetLFOModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                string Sp_Set = "[dbo].[USP_CCM_LostAndFoundOperations]";
                var sqlParams = new Dictionary<string, object>  {

                    { "@CCM_TRN_CycleCount_ID" , items.CCM_TRN_CycleCount_ID },
                    { "@OperationFlag" , items.OperationFlag },
                    { "@UserLoggedId" , items.UserLoggedId }

                };
                var obj = new Dictionary<string, object>();
                foreach (var item in sqlParams)
                {
                    obj.Add(item.Key, item.Value);
                }
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
                string Append = "EXEC " + Sp_Set + " " + sb.ToString();
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
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

        public async Task<Payload<string>> GetSuccessInfoCapture(GetSuccessInfoCaptureModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                string Sp_Set = "[dbo].[USP_GET_CCM_TRN_CycleCounts_Capture]";
                var sqlParams = new Dictionary<string, object>  {

                    { "@CID" , items.CID },
                    { "@AccountID" , items.AccountID },
                    { "@CCM_CNF_AccountCycleCount_ID" , items.CCM_CNF_AccountCycleCount_ID }
                };
                var obj = new Dictionary<string, object>();
                foreach (var item in sqlParams)
                {
                    obj.Add(item.Key, item.Value);
                }
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
                string Append = "EXEC " + Sp_Set + " " + sb.ToString();
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                var data = await DbUtility.GetjsonDataFromDatasetINQ(this.ConnectionString, Append);
                if (data != null || data != "{}")
                {
                    response.Result = data;//Success
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
        public async Task<Payload<string>> UpsertQualityVerification(UpsertQualityModel items)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            Payload<string> response = new Payload<string>();
            string Query = "EXEC USP_ORD_INL_ORD_UPD_EmployeeRequestHeader @RequestHeaderID = '" + items.RequestHeaderID + "' ";
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            int Result = DbUtility.GetSqlN(Query, ConnectionString);
            if (Result == 0)
            {
                response.Result = "1";//Shipment Verified Successfully
            }
            return response;
        }


        public async Task<Payload<string>> LabSampleRequest_InitiatePick(LabSampleRequest_InitiatePickModel items)
        {
            int IsAllowed = 0;
            Payload<string> response = new Payload<string>();
            try
            {
                string Roles = items.Roles;
                for (int i = 0; i < Roles.Length; i++)
                {
                    if (Roles[i] == '3' || Roles[i] == '5')
                    {
                        IsAllowed = 1;
                        break;
                    }
                }
                if (IsAllowed == 0)
                {
                    //response.Result = "{\r\n  \"Table\": [\r\n    {\r\n    \"N\":\"-99\"\r\n}\r\n]\r\n}";
                    response.Result = "{\"Table\":[{\"N\":\"-99\"}]}";
                }
                else
                {
                    string Query = "EXEC [dbo].[sp_ORD_Employee_Issue] @TenantID = " + items.TenantID + ",@RequestHeaderID =" + items.RequestHeaderID + ",@UserID=" + items.UserID;
                    DBFactory factory = new DBFactory();
                    IDBUtility DbUtility = factory.getDBUtility();
                    var result = await DbUtility.GetjsonDataFromDatasetINQ(this.ConnectionString, Query);
                    response.Result = result.ToString();
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

        public async Task<Payload<string>> MaterialPickQtyUpdate(MaterialPickQtyUpdateModel items)
        {
            Payload<string> response = new Payload<string>();
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                string Query = "Exec [dbo].[USP_ORD_INL_ORD_ORD_EmployeeRequestHeader] @RequestHeaderID=" + items.HeaderID + "";
                string result = await DbUtility.GetjsonDataFromDatasetINQ(this.ConnectionString, Query);
                JObject data = JObject.Parse(result);
                JArray table = (JArray)data["Table"];
                int EmpRequestStatusID = (int)table[0]["EmpRequestStatusID"];
                if (EmpRequestStatusID != 1)
                {
                    if (items.PickedQuantity > items.ReceivedQuantity)
                    {
                        response.Result = "-1";//Picked Qty exceeded received Qty
                        return response;
                    }
                }

                string ResultQuery = "EXEC [dbo].[SP_UPDATE_EMPDETAILS_PICKQTY] @RequestDetailsID = " + items.RequestDetailsID + ",@Quantity = " + items.PickedQuantity + ",@UserID=" + items.UserID + "";
                var DS = DbUtility.GetDS(ResultQuery, this.ConnectionString);

#pragma warning disable CS0252 // Possible unintended reference comparison; to get a value comparison, cast the left hand side to type 'string'
                if (DS.Tables[0].Rows[0]["N"] == "1")
#pragma warning restore CS0252 // Possible unintended reference comparison; to get a value comparison, cast the left hand side to type 'string'
                {
                    response.ResponseCode = "1"; //Successfully Updated..!
                    response.Result = JsonConvert.SerializeObject(DS);
                }
                else
                {
                    response.ResponseCode = "2";//Stock not available at the Location..!
                    response.Result = JsonConvert.SerializeObject(DS);
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
        public async Task<Payload<string>> TransferSupplierReturns(TransferSupplierReturnsModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                Random generateNo = new Random();
                var CustomerPONum = "DCUSPO" + generateNo.Next(1000, 10000);

                var OBDNumber = await GetOBDForSupplierReturns(items);
                var SONumber = await GetSONumberForSupplierReturns(items);

                XmlSerializer serializer = new XmlSerializer(typeof(List<SupplierRt>));
                string finalxml = "";
                using (var stream = new StringWriter())
                {
                    serializer.Serialize(stream, items.SupplierRt);
                    string xml = stream.ToString();
                    finalxml = xml.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");
                    finalxml = finalxml.Replace(" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", "");
                }
                var sqlParams = new Dictionary<string, object>
                {
                    { "@XML" , "'" + finalxml + "'" },
                    { "@SONumber" , SONumber.Result },
                    { "@CustomerPO" , CustomerPONum },
                    { "@OBDNUmber" , OBDNumber.Result  },
                    { "@POHeaderID" , items.POHeaderID },
                    { "@SupplierInvoiceID" , items.SupplierInvoiceID },
                    { "@WareHouseID" , items.WareHouseID },
                    { "@TenantID" , items.TenantID },
                    { "@DockID" , items.DockID },
                    { "@AccountID" , items.AccountID },
                    { "@CreatedBy" , items.UserID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                var jsonresult = await DbUtility.GetjsonData(this.ConnectionString, "DummyOutbound_SupplierReturns", sqlParams).ConfigureAwait(false);
                JArray jsonArray = JArray.Parse(jsonresult);
                int OutBoundID = jsonArray[0]["N"].Value<int>();

                if (OutBoundID != -1)
                {
                    response.Result = "Return Order created successfully with Outbound Ref.:" + OBDNumber.Result;
                    return response;
                }
                else
                {
                    response.Result = "Error while returns creations";
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
        public async Task<Payload<string>> GetOBDForSupplierReturns(TransferSupplierReturnsModel items)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            Payload<string> response = new Payload<string>();
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                String OBDNumber = "";
                String newvalue = "";
                string Query = "EXEC [sp_SYS_GetSystemConfigValue] @SysConfigKey='OutboundDetails.aspx' , @TenantID =" + items.TenantID + "";
                int Length = Convert.ToInt32(DbUtility.GetSqlS(Query, ConnectionString));

                string NewOBDQuery = "EXEC [sp_SYS_GetSystemConfigValue] @SysConfigKey='dummyoutboundforsupplierreturn_prefix' , @TenantID =" + items.TenantID + "";
                string NewOBDNumber = DbUtility.GetSqlS(NewOBDQuery, ConnectionString);
                NewOBDNumber = NewOBDNumber + "" + (Convert.ToInt16(DateTime.Now.Year) % 100) + "";

                String OldOBDQuery = "Exec USP_ORD_INL_GETOBDNumber @AccountID=" + items.AccountID + " , @NewOBDNumber= " + "'" + NewOBDNumber + "'" + "";
                String OldOBDNumber = DbUtility.GetSqlS(OldOBDQuery, ConnectionString);

                int power = (Int32)Math.Pow((double)10, (double)(Length - 1)); //getting minvalue of prifix length

                if (OldOBDNumber != "" && NewOBDNumber.Equals(OldOBDNumber.Substring(0, NewOBDNumber.Length))) //if ponumber is existed and same year ponumber  enter
                {
                    String temp = OldOBDNumber.Substring(NewOBDNumber.Length, Length); //getting number of last prifix
                    Int32 number = Convert.ToInt32(temp);
                    number++;

                    while (power > 1) //add '0' to number at left side for get 
                    {
                        if (number / power > 0)
                        {
                            break;
                        }
                        newvalue += "0";
                        power /= 10;
                    }
                    newvalue += number;
                }
                else
                {                                                                                           //other wise generate first number 
                    for (int i = 0; i < Length - 1; i++)
                        newvalue += "0";
                    newvalue += "1";
                }
                NewOBDNumber += newvalue;
                OBDNumber = NewOBDNumber;

                response.Result = OBDNumber;
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
        public async Task<Payload<string>> GetSONumberForSupplierReturns(TransferSupplierReturnsModel items)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            Payload<string> response = new Payload<string>();
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                String SONumber = "";
                String newvalue = "";

                string Query = "EXEC [sp_SYS_GetSystemConfigValue] @SysConfigKey='salesorder.aspx.cs.SO_Length' , @TenantID =" + items.TenantID + "";
                int Length = Convert.ToInt32(DbUtility.GetSqlS(Query, ConnectionString));

                string NewSOQuery = "EXEC [sp_SYS_GetSystemConfigValue] @SysConfigKey='dummysoheaderforsupplierreturn_prefix' , @TenantID =" + items.TenantID + "";
                string NewSONumber = DbUtility.GetSqlS(NewSOQuery, ConnectionString);
                NewSONumber = NewSONumber + "" + (Convert.ToInt16(DateTime.Now.Year) % 100) + "";

                String OldSOQuery = "Exec USP_ORD_INL_ORD_SOHeaderData @AccountID=" + items.AccountID + " , @NewOBDNumber= " + "'" + NewSONumber + "'" + "";
                String OldSONumber = DbUtility.GetSqlS(OldSOQuery, ConnectionString);

                int power = (Int32)Math.Pow((double)10, (double)(Length - 1)); //getting minvalue of prifix length

                if (OldSONumber != "" && NewSONumber.Equals(OldSONumber.Substring(0, NewSONumber.Length))) //if ponumber is existed and same year ponumber  enter
                {
                    String temp = OldSONumber.Substring(NewSONumber.Length, Length); //getting number of last prifix
                    Int32 number = Convert.ToInt32(temp);
                    number++;

                    while (power > 1) //add '0' to number at left side for get 
                    {
                        if (number / power > 0)
                        {
                            break;
                        }
                        newvalue += "0";
                        power /= 10;
                    }
                    newvalue += number;
                }
                else
                {                                                                                           //other wise generate first number 
                    for (int i = 0; i < Length - 1; i++)
                        newvalue += "0";
                    newvalue += "1";
                }
                NewSONumber += newvalue;
                SONumber = NewSONumber;

                response.Result = SONumber;
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

        //public async Task<Payload<string>> CurrentStock_PrintLabel(CurrentStock_PrintLabelModel printobj)
        //{
        //    Payload<string> response = new Payload<string>();
        //    DBFactory factory = new DBFactory();
        //    IDBUtility DbUtility = factory.getDBUtility();
        //    int Port = 0;
        //    try
        //    {
        //        string ZPL = "";
        //        await Task.Run(() =>
        //        {
        //            try
        //            {
        //                List<CurrentStock_Report> lst = new List<CurrentStock_Report>();
        //                lst = printobj.currentStock_report;
        //                Print_MLabelModel Mlabel = new Print_MLabelModel();

        //                for (var i = 0; i < printobj.currentStock_report.Count; i++)
        //                {
        //                    Mlabel.MCode = printobj.currentStock_report[i].MCode;
        //                    string Query = "SELECT MDescription AS S FROM MMT_MaterialMaster WHERE IsActive=1 AND IsDeleted=0 AND MCode='" + printobj.currentStock_report[i].MCode + "'";
        //                    string MDescription = DbUtility.GetSqlS(Query, ConnectionString);
        //                    Mlabel.Description = MDescription;
        //                    //Mlabel.SerialNo = printobj.currentStock_report[i].SerialNo;
        //                    Mlabel.BatchNo = printobj.currentStock_report[i].BatchNo;
        //                    Mlabel.MfgDate = printobj.currentStock_report[i].MfgDate == null || printobj.currentStock_report[i].MfgDate == "" ? "" : printobj.currentStock_report[i].MfgDate;
        //                    Mlabel.ExpDate = printobj.currentStock_report[i].ExpDate == null || printobj.currentStock_report[i].ExpDate == "" ? "" : printobj.currentStock_report[i].ExpDate;
        //                    Mlabel.ProjectNo = printobj.currentStock_report[i].ProjectRefNo;
        //                    Mlabel.Lineno = printobj.currentStock_report[i].LineNo == null || printobj.currentStock_report[i].LineNo == "" ? "" : printobj.currentStock_report[i].LineNo;
        //                    Mlabel.Mrp = printobj.currentStock_report[i].MRP == null || printobj.currentStock_report[i].MRP == "" ? "" : printobj.currentStock_report[i].MRP;
        //                    Mlabel.KitCode = printobj.currentStock_report[i].KitCode == null || printobj.currentStock_report[i].KitCode == "" ? "" : printobj.currentStock_report[i].KitCode;
        //                    //Mlabel.HUNo = printobj.currentStock_report[i].HUNo == null || printobj.currentStock_report[i].HUNo == "" ? "1" : printobj.currentStock_report[i].HUNo;
        //                    //Mlabel.HUSize = printobj.currentStock_report[i].HUSize == null || printobj.currentStock_report[i].HUSize == "" ? "1" : printobj.currentStock_report[i].HUSize;
        //                    Mlabel.SupplierLot = printobj.currentStock_report[i].SupplierLot;

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
        //                    Mlabel.PrintQty = printobj.currentStock_report[i].PrintQty;
        //                    Mlabel.LabelType = LabelType;

        //                    ZPL += PrintBarcodeLabel(Mlabel).Result.Result;

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

        //                    Helper.PrintHelper.PrintUsingIP(printobj.ipaddress, Port , ZPL);
        //                }
        //                lst.Clear();
        //                var result = ZPL;
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



        public async Task<Payload<string>> CurrentStock_PrintLabel(CurrentStock_PrintLabelModel printobj)
        {
            Payload<string> response = new Payload<string>();
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            int Port = 0;
            try
            {
                string ZPL = "";
                await Task.Run(() =>
                {
                    try
                    {
                        List<CurrentStock_Report> lst = new List<CurrentStock_Report>();
                        lst = printobj.currentStock_report;
                        Print_MLabelModel Mlabel = new Print_MLabelModel();

                        for (var i = 0; i < printobj.currentStock_report.Count; i++)
                        {

                            string Serial_Nos = "exec SP_Item_print @mcode='" + printobj.currentStock_report[i].MCode + "',@count=" + printobj.currentStock_report[i].PrintQty + "";
                            var ds = DbUtility.GetDS(Serial_Nos, this.ConnectionString);

                            for (int j = 0; j < ds.Tables[0].Rows.Count; j++)
                            {
                            Mlabel.SerialNo = ds.Tables[0].Rows[j]["BoxSerialNo"].ToString();
                            Mlabel.BoxSerialNo = ds.Tables[0].Rows[j]["BoxSerialNo"].ToString();
                            Mlabel.MCode = printobj.currentStock_report[i].MCode;
                            string Query = "SELECT MDescription AS S FROM MMT_MaterialMaster WHERE IsActive=1 AND IsDeleted=0 AND MCode='" + printobj.currentStock_report[i].MCode + "'";
                            string MDescription = DbUtility.GetSqlS(Query, ConnectionString);
                            Mlabel.Description = MDescription;
                            //Mlabel.SerialNo = printobj.currentStock_report[i].SerialNo;
                            Mlabel.BatchNo = printobj.currentStock_report[i].BatchNo;
                            Mlabel.MfgDate = printobj.currentStock_report[i].MfgDate == null || printobj.currentStock_report[i].MfgDate == "" ? "" : printobj.currentStock_report[i].MfgDate;
                            Mlabel.ExpDate = printobj.currentStock_report[i].ExpDate == null || printobj.currentStock_report[i].ExpDate == "" ? "" : printobj.currentStock_report[i].ExpDate;
                            Mlabel.ProjectNo = printobj.currentStock_report[i].ProjectRefNo;
                            Mlabel.Lineno = printobj.currentStock_report[i].LineNo == null || printobj.currentStock_report[i].LineNo == "" ? "" : printobj.currentStock_report[i].LineNo;
                            Mlabel.Mrp = printobj.currentStock_report[i].MRP == null || printobj.currentStock_report[i].MRP == "" ? "" : printobj.currentStock_report[i].MRP;
                            Mlabel.KitCode = printobj.currentStock_report[i].KitCode == null || printobj.currentStock_report[i].KitCode == "" ? "" : printobj.currentStock_report[i].KitCode;
                            //Mlabel.HUNo = printobj.currentStock_report[i].HUNo == null || printobj.currentStock_report[i].HUNo == "" ? "1" : printobj.currentStock_report[i].HUNo;
                            //Mlabel.HUSize = printobj.currentStock_report[i].HUSize == null || printobj.currentStock_report[i].HUSize == "" ? "1" : printobj.currentStock_report[i].HUSize;
                            Mlabel.SupplierLot = printobj.currentStock_report[i].SupplierLot;
                            Mlabel.Grade= printobj.currentStock_report[i].Grade;

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
                            Mlabel.PrintQty = printobj.currentStock_report[i].PrintQty;
                            Mlabel.LabelType = LabelType;
                            Mlabel.DesignName = printobj.Currentstocksecondlabelprint.DesignName;
                            Mlabel.Matt = printobj.Currentstocksecondlabelprint.Series;
                            Mlabel.BoxQty = printobj.Currentstocksecondlabelprint.BoxQty;
                            Mlabel.Rectified = printobj.Currentstocksecondlabelprint.Rectified;
                            Mlabel.UnPolished = printobj.Currentstocksecondlabelprint.UnPolished;
                            Mlabel.Glazed = printobj.Currentstocksecondlabelprint.Glazed;
                            Mlabel.ShiftTime = printobj.Currentstocksecondlabelprint.Shift;
                            Mlabel.LineNo = printobj.Currentstocksecondlabelprint.LineNumber;
                            Mlabel.SorterId = printobj.Currentstocksecondlabelprint.SorterID;
                            Mlabel.Wapis = printobj.Currentstocksecondlabelprint.WAPIS;
                            Mlabel.Size = printobj.currentStock_report[i].Size;
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
                        else
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



#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<Payload<string>> PrintBarcodeLabel(Print_MLabelModel Mlabel)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
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

                //var query = "EXEC [dbo].[sp_GetZPLString] @Dpi=" + Mlabel.Dpi + " , @Length=" + Mlabel.Length + ", @Width = " + Mlabel.Width + ", @LabelType = " + "'" + Mlabel.LabelType + "'";
                var query = "EXEC [dbo].[sp_GetZPLString] @Dpi=" + Mlabel.Dpi + " , @Length=" + Mlabel.Length + ", @Width = " + Mlabel.Width + ", @LabelType = " + "'" + Mlabel.LabelType + "',@IsSingleLabel="+ Mlabel.IsSecondaryLabelprint + "";//Added By Ramsai
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
                        result = result.Replace("@BatchNo", " " + "" + "");   
                    }

                    if (Mlabel.MfgDate.ToString() != "")
                    {

                        //result = result.Replace("@Mfg.Date", " " + " " + String.Format("{0:dd-MM-yy}", Mlabel.MfgDate));
                        result = result.Replace("@Mfg.Date", "" + " " + Convert.ToDateTime(Mlabel.MfgDate).ToString("dd.MM.yyyy"));

                    }
                    else
                    {
                        result = result.Replace("@Mfg.Date", " " + "" + "");
                    }

                    if (Mlabel.ExpDate.ToString() != "")
                    {
                        result = result.Replace("@Exp.Date", " " + " " + String.Format("{0:dd-MM-yy}", Mlabel.ExpDate));
                    }
                    else
                    {
                        result = result.Replace("@Exp.Date", " " + "" + "");
                    }

                    if (Mlabel.SerialNo != "")
                    {
                        result = result.Replace("@Serial No.", " " + "" + Mlabel.SerialNo);
                    }
                    else
                    {
                        result = result.Replace("@Serial No.", " " + "" + "");
                    }

                    if (Mlabel.ProjectNo != "")
                    {
                        result = result.Replace("@Project Ref No.", " " + "" + Mlabel.ProjectNo);
                    }
                    else
                    {
                        result = result.Replace("@Project Ref No.", " " + "" + "");
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
                    if (Mlabel.Mrp != "")
                    {
                        result = result.Replace("@MRP", " " + "" + Mlabel.Mrp + " /-");
                    }
                    else
                    {
                        result = result.Replace("@MRP", " " + "" + "");
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
                    if (Mlabel.Size != "")
                    {
                        result = result.Replace("@Size", "" + "Size : " + Mlabel.Size);
                    }
                    else
                    {
                        result = result.Replace("@Size", "" + "" + "");
                    }
                    if (Mlabel.Grade != "")
                    {
                        result = result.Replace("@Grade", "" + " - " + Mlabel.Grade);
                    }
                    else
                    {
                        result = result.Replace("@Grade", "" + "" + "");
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

        public async Task<Payload<string>> UpsertCycleCountDetails(UpsertCycleCountDetailsModel items)
        {
            Payload<string> response = new Payload<string>();
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                int CycleCountID = 0;
                int AccountCycleCountID = 0;
                int MSTCycleCountID = 0;
                int CycleCountEntityID = 0;
                int EntityID = 0;
                string CycleCountCode = "";

                string Query = " EXEC [dbo].[Get_CCDetails_HHT] @CCName = " + "'" + items.CCName + "'" + "";
                var DS = DbUtility.GetDS(Query, ConnectionString);

                if (DS != null && DS.Tables.Count != 0 && DS.Tables[0].Rows.Count != 0)
                {
                    foreach (DataRow dr in DS.Tables[0].Rows)
                    {
                        CycleCountID = Convert.ToInt32(DS.Tables[0].Rows[0]["CCM_TRN_CycleCount_ID"]);
                        AccountCycleCountID = Convert.ToInt32(DS.Tables[0].Rows[0]["CCM_CNF_AccountCycleCount_ID"]);
                        MSTCycleCountID = Convert.ToInt32(DS.Tables[0].Rows[0]["CCM_MST_CycleCount_ID"]);
                        CycleCountEntityID = Convert.ToInt32(DS.Tables[0].Rows[0]["CCM_MST_CycleCountEntity_ID"]);
                        EntityID = Convert.ToInt32(DS.Tables[0].Rows[0]["Entity_ID"]);
                    }
                }
                else
                {
                    items.Result = "Error while inserting";
                    response.ResponseCode = items.Result;
                    return response;
                }

                string strQuery = "EXEC [dbo].[UpsertCycleCountDetails_HHT] @CCM_TRN_CycleCount_ID=" + CycleCountID;
                strQuery += ",@CCM_CNF_AccountCycleCount_ID=" + AccountCycleCountID;
                strQuery += ",@CCM_MST_CycleCount_ID=" + MSTCycleCountID;
                strQuery += ",@CCM_MST_CycleCountEntity_ID=" + CycleCountEntityID;
                strQuery += ",@Entity_ID=" + EntityID;
                strQuery += ",@Location=" + DBLibrary.SQuote(items.Location);
                strQuery += ",@Container=" + DBLibrary.SQuote(items.Container);
                strQuery += ",@Mcode=" + DBLibrary.SQuote(items.SKU);
                strQuery += ",@Qty=" + items.CCQty;
                strQuery += ",@UserId =" + items.UserID;
                strQuery += ",@TenantID =" + items.TenantID;
                strQuery += ",@Mfgdate =" + (string.IsNullOrEmpty(items.MfgDate) ? "NULL" : DBLibrary.SQuote(items.MfgDate));
                strQuery += ",@Expdate =" + (string.IsNullOrEmpty(items.ExpDate) ? "NULL" : DBLibrary.SQuote(items.ExpDate));
                strQuery += ",@StorageLocation =" + (string.IsNullOrEmpty(items.StorageLocation) ? "NULL" : DBLibrary.SQuote(items.StorageLocation));
                strQuery += ",@SerialNo =" + (string.IsNullOrEmpty(items.SerialNo) ? "NULL" : DBLibrary.SQuote(items.SerialNo));
                strQuery += ",@BatchNo =" + (string.IsNullOrEmpty(items.BatchNo) ? "NULL" : DBLibrary.SQuote(items.BatchNo));
                strQuery += ",@ProjectRefNo =" + (string.IsNullOrEmpty(items.ProjectRefNo) ? "NULL" : DBLibrary.SQuote(items.ProjectRefNo));
                strQuery += ",@MRP =" + (string.IsNullOrEmpty(items.MRP) ? "NULL" : DBLibrary.SQuote(items.MRP));
                strQuery += ",@WarehouseID =" + items.WarehouseID;

                int result = DbUtility.GetSqlN(strQuery, ConnectionString);

                string sp = "SELECT CycleCountCode FROM CCM_TRN_CycleCounts WHERE CCM_TRN_CycleCount_ID = @CCM_TRN_CycleCount_ID AND IsActive=1 AND IsDeleted=0";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sp, connection))
                    {
                        command.Parameters.AddWithValue("@CCM_TRN_CycleCount_ID", items.CCM_TRN_CycleCount_ID);
                        CycleCountCode = (string)command.ExecuteScalar();
                    }
                }
          
                string BinCompleteQuery = " EXEC [dbo].[USP_API_CCM_ConsolidateInventoryAfterCycleCount] @CycleCountCode = " + "'" + CycleCountCode + "'" + ",@LocationCode = " + "'" + items.Location + "'" + ",@ActivityByUser = " + "'" + items.UserID + "'" + ",@AccountID = " + "'" + items.AccountID + "'" + "";
                var DataSet = DbUtility.GetDS(BinCompleteQuery, ConnectionString);


                if (result == 0)
                {
                    response.Result = "0"; //Error while inserting
                }
                else if (result == -1)
                {
                    response.Result = "-1"; //Invalid Location
                }
                else
                {
                    var resultBin = await GetBinCount(items);
                    //response.Result = resultBin.Result; //Confirmed successfully
                    response.Result = "1"; //Confirmed successfully
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

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<Payload<string>> GetBinCount(UpsertCycleCountDetailsModel items)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            Payload<string> response = new Payload<string>();
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                StringBuilder sbSqlString = new StringBuilder();
                sbSqlString.Append(" EXEC [dbo].[GetBincount_HHT]");
                sbSqlString.Append(" @Location=" + DBLibrary.SQuote(items.Location));
                sbSqlString.Append(" ,@CycleCountName=" + DBLibrary.SQuote(items.CCName));
                sbSqlString.Append(" ,@WarehouseID=" + items.WarehouseID);
                int result = DbUtility.GetSqlN(sbSqlString.ToString(),ConnectionString);
                if (result != 0)
                {
                    items.Result = "";
                    items.Count = result.ToString();
                    response.Result = items.Count;
                }
                else
                {
                    items.Count = "0";
                    response.Result = items.Count;//Bin Count
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
      

       



        






    }
}




