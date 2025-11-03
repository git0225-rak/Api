using Simpolo_Endpoint.DAO.interfaces;
using Simpolo_Endpoint.DBUtil;
using Simpolo_Endpoint.Models;
using Simpolo_Endpoint.ModelsLibrary;
using Microsoft.AspNetCore.Mvc;
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
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using static Simpolo_Endpoint.Models.InventoryModel;

namespace Simpolo_Endpoint.DAO.Services
{
    public class InventoryServices : AppDBService, IInventory
    {

        Encryption objE = new Encryption();
        public InventoryServices(IOptions<AppSettings> appSettings) : base(appSettings)
        {
        }
        //current stock
#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<Payload<string>> GetCurrentStock(GetCurrentStockInputModel getCurrentStockInput)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            Payload<string> response = new Payload<string>();
            try
            {

                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();

                string Query = " EXEC [sp_INV_GetCurrentStockReportDynamic] @SearchType = " + getCurrentStockInput.SearchType + " ," +
                    " @searchText = " + "'" + getCurrentStockInput.searchText + "'" + " , @TenantID = " + "'" + getCurrentStockInput.TenantID + "'" + " ," +
                    " @MaterialMasterID = " + "'" + getCurrentStockInput.MaterialMasterID + "'" + " , @MTypeID = " + getCurrentStockInput.MTypeID + " , @BatchNo = " + "'" + getCurrentStockInput.BatchNo + "'" + " ," +
                    " @LocationID = " + getCurrentStockInput.LocationID + " , @KitID = " + getCurrentStockInput.KitID + " , @AccountID_New = " + getCurrentStockInput.AccountID_New + " , " +
                    "@TenantID_New = " + getCurrentStockInput.TenantID_New + " , @UserID_New = " + getCurrentStockInput.UserID_New + " , " +
                    "@IndustryXML = " + "'" + getCurrentStockInput.IndustryXML + "'" + ", @GEN_MST_Industry_ID = " + getCurrentStockInput.GEN_MST_Industry_ID + ", @WarehouseID = " + getCurrentStockInput.WarehouseID + "," +
                    "@OEMPartNo = " + "'" + getCurrentStockInput.OEMPartNo + "'" + ",@MenuID = " + getCurrentStockInput.MenuID + ",@Rownumber = " + getCurrentStockInput.Rownumber + ", " +
                    "@NofRecordsPerPage = " + getCurrentStockInput.NofRecordsPerPage + ",@slocid = " + getCurrentStockInput.slocid + ",@DrawTypeID = " + getCurrentStockInput.DrawTypeID + ",@CartonID = " + getCurrentStockInput.CartonID + "," +
                    "@GradeID=" + getCurrentStockInput.GradeID + "";

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



        public async Task<Payload<string>> GetMovementStock(GetCurrentStockInputModel getCurrentStockInput)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            Payload<string> response = new Payload<string>();
            try
            {

                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();

                string Query = " EXEC [sp_INV_GetMovementStockReportDynamic] @SearchType = " + getCurrentStockInput.SearchType + " ," +
                    " @searchText = " + "'" + getCurrentStockInput.searchText + "'" + " , @TenantID = " + "'" + getCurrentStockInput.TenantID + "'" + " ," +
                    " @MaterialMasterID = " + "'" + getCurrentStockInput.MaterialMasterID + "'" + " , @MTypeID = " + getCurrentStockInput.MTypeID + " , @BatchNo = " + "'" + getCurrentStockInput.BatchNo + "'" + " ," +
                    " @LocationID = " + getCurrentStockInput.LocationID + " , @KitID = " + getCurrentStockInput.KitID + " , @AccountID_New = " + getCurrentStockInput.AccountID_New + " , " +
                    "@TenantID_New = " + getCurrentStockInput.TenantID_New + " , @UserID_New = " + getCurrentStockInput.UserID_New + " , " +
                    "@IndustryXML = " + "'" + getCurrentStockInput.IndustryXML + "'" + ", @GEN_MST_Industry_ID = " + getCurrentStockInput.GEN_MST_Industry_ID + ", @WarehouseID = " + getCurrentStockInput.WarehouseID + "," +
                    "@OEMPartNo = " + "'" + getCurrentStockInput.OEMPartNo + "'" + ",@MenuID = " + getCurrentStockInput.MenuID + ",@Rownumber = " + getCurrentStockInput.Rownumber + ", " +
                    "@NofRecordsPerPage = " + getCurrentStockInput.NofRecordsPerPage + ",@slocid = " + getCurrentStockInput.slocid + ",@DrawTypeID = " + getCurrentStockInput.DrawTypeID + ",@CartonID = " + getCurrentStockInput.CartonID + "," +
                    "@GradeID=" + getCurrentStockInput.GradeID + "";

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


        public async Task<Payload<string>> UpsertMovementStock(GetCurrentStockInputModel getCurrentStockInput)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            Payload<string> response = new Payload<string>();
            try
            {

                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();

                string Query = " EXEC [sp_INV_UpsertMovementStock]  @TenantID = " + "'" + getCurrentStockInput.TenantID + "'" + " ," +
                    " @MaterialMasterID = " + "'" + getCurrentStockInput.MaterialMasterID + "'" + " , @BatchNo = " + "'" + getCurrentStockInput.BatchNo + "'" + " ," +
                    " @LocationID = " + getCurrentStockInput.LocationID + " , " +
                    "@slocid = " + getCurrentStockInput.slocid + ",@CartonID = " + getCurrentStockInput.CartonID + "," +
                    "@GradeID=" + getCurrentStockInput.GradeID + ","+ "@ActiveStockDetailsID="+ getCurrentStockInput.ActivestockDetailsID;

                    var DS = DbUtility.GetSqlS(Query, ConnectionString);
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





        public async Task<Payload<string>> GetMiscellaneousReceipt(GetMiscellaneousReceiptModel getMiscellaneousReceipt)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                  {"@MaterialMasterID",getMiscellaneousReceipt.MaterialMasterID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_INV_M_IL_Load_MaterialDetails", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetMiscellaneousReceiptTableData(GetMiscellaneousReceiptTableDataModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                  {"@MaterialMasterID",obj.MaterialMasterID },
                  {"@WarehouseID",obj.WarehouseID},
                  {"@TenantID",obj.TenantID },

                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "sp_INV_GetAvailbleDataForMisslleniousReceipt", sqlParams).ConfigureAwait(false);
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


        public async Task<Payload<string>> GetProjectStockList(GetProjectStockListModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {

                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                string query = "EXEC [dbo].[SP_GetProjstocklist] @TenantID =" + items.TenantID + ",@MaterialMasterID = " + items.MaterialMasterID + ",@WarehouseID = " + items.WareHouseId + ",@Projectrefno = " + "'" + items.ProjectRefNo +"'"+ "";
                DataSet ds = DbUtility.GetDS(query, this.ConnectionString);
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


        public async Task<Payload<string>> UpdateProjectstockTransferQty(UpdateProjectstockTransferQtyModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();

                XmlSerializer serializer = new XmlSerializer(typeof(List<Materialdata>), new XmlRootAttribute("root"));
                string OutputXML = "";
                using (var stream = new StringWriter())
                {
                    serializer.Serialize(stream, items.OBJ);
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
                //var sqlParams = new Dictionary<string, object> {
                //  {"@DynamicXml", "'"+updatedXmlString+"'" },
                //  {"@ProjectRefNo","'"+items.ProjectRefNo+"'"  },
                //  {"@TenantID",items.TenantID },
                //  {"@UserID",items.UserID },
                //  {"@WareHouseId",items.WareHouseId }
                //};

                string InsertPickItemQuery = "EXEC [dbo].[SP_UpsertProjectStockTransfer] @DynamicXml=" + DBLibrary.SQuote(updatedXmlString) + ",@ProjectRefNo=" + DBLibrary.SQuote(items.ProjectRefNo) + ",@TenantID=" + items.TenantID + ",@UserID=" + items.UserID + ",@WareHouseId=" + items.WareHouseId + "";
                DataSet DS = DbUtility.GetDS(InsertPickItemQuery, this.ConnectionString);
                var jsonresult = JsonConvert.SerializeObject(DS);
                response.Result = jsonresult;
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

        public async Task<Payload<string>> UpdateMisslleniousReceipt(UpdateMisslleniousReceiptIputModel updateMisslleniousReceipt)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();

                string UniqueID = "MR" + DateTime.Now.ToString("ddhhmmss");
                // string qadurl = this.APIURL;
                //string URL = "" + qadurl + "/Inbound/GetMiscRecptXMLData";
                //string urlParameters = "?batchNo=" + updateMisslleniousReceipt.BatchNo + "&&projectRefNo=" + updateMisslleniousReceipt.ProjectRefNo + "&&Qty=" + updateMisslleniousReceipt.POQunatity + "&&remks=" + updateMisslleniousReceipt.Remarks + "&&part=" + updateMisslleniousReceipt.MCode + "&&um=" + updateMisslleniousReceipt.ltBaseUOM + "&&val=" + 1 + "&&qadAccount=" + updateMisslleniousReceipt.qadAccount + "&&QADLocation=Holding&&uniqueID=" + UniqueID + "";
                // using (HttpClient client = new HttpClient())
                //{
                //client.BaseAddress = new Uri(URL);

                //client.DefaultRequestHeaders.Accept.Add(
                //      new MediaTypeWithQualityHeaderValue("application/json"));

                //HttpResponseMessage result = client.GetAsync(urlParameters).Result;

                //if (result.IsSuccessStatusCode)
                //{
                //string jsonString = result.Content.ReadAsStringAsync().Result;
                //JavaScriptSerializer serializer = new JavaScriptSerializer();
                //dynamic item = serializer.Deserialize<object>(jsonString);
                //string test = item["result"];
                //string ErrorCode = item["errorcode"];
                string test = "success";
                if (test == "success")
                {
                    if (!string.IsNullOrEmpty(updateMisslleniousReceipt.SerialNO))
                    {
                        if (updateMisslleniousReceipt.POQunatity != "1")
                        {
                            response.Result = "-1"; //Quantity should be 1 for serial no., cannot receive
                            return response;
                        }
                        string Query = "[dbo].[sp_GET_SERIAL_COUNT] @SerialNo = " + DBLibrary.SQuote(updateMisslleniousReceipt.SerialNO) + ", @MCode= " + DBLibrary.SQuote(updateMisslleniousReceipt.MCode) + "";
                        int Count = DbUtility.GetSqlN(Query, this.ConnectionString);
                        if (Count != 0)
                        {
                            response.Result = "-2";
                            // resetError("Duplicate serial no", true);
                            return response;
                        }
                    }

                    var MfgDate = string.IsNullOrEmpty(updateMisslleniousReceipt.MfgDate) ? "NULL" : DBLibrary.SQuote(Convert.ToDateTime(updateMisslleniousReceipt.MfgDate).ToString("dd-MMM-yyyy"));
                    var ExpDate = string.IsNullOrEmpty(updateMisslleniousReceipt.ExpDate) ? "NULL" : DBLibrary.SQuote(Convert.ToDateTime(updateMisslleniousReceipt.ExpDate).ToString("dd-MMM-yyyy"));

                    string MisslleniousReceiptQuery = "EXEC [dbo].[sp_INV_MisslleniousReceiptUpdate] @TenantID =" + updateMisslleniousReceipt.TenantID + ",@MaterialMasterID = " + updateMisslleniousReceipt.MaterialMasterID + ",@POQunatity = " + "'" + updateMisslleniousReceipt.POQunatity + "'" + ",@LOCATION = " + DBLibrary.SQuote(updateMisslleniousReceipt.LOCATION) + ",@SerialNo = " + DBLibrary.SQuote(updateMisslleniousReceipt.SerialNO) + ",@MfgDate = " + MfgDate + ",@ExpDate = " + ExpDate + ",@ProjectRefNo = " + DBLibrary.SQuote(updateMisslleniousReceipt.ProjectRefNo) + ",@MRP = " + DBLibrary.SQuote(updateMisslleniousReceipt.MRP) + ",@UpdatedBy = " + updateMisslleniousReceipt.UpdatedBy + ",@CartonID = " + updateMisslleniousReceipt.CartonID + ",@WareHouseId = " + updateMisslleniousReceipt.WareHouseId + ",@UniqueID = " + DBLibrary.SQuote(UniqueID) + ",@IsDamaged = " + updateMisslleniousReceipt.IsDamaged + ",@HasDiscipency = " + updateMisslleniousReceipt.HasDiscipency + ",@Remarks = " + DBLibrary.SQuote(updateMisslleniousReceipt.Remarks) + ",@StorageLocationID = " + updateMisslleniousReceipt.StorageLocationID + ",@BatchNo = " + DBLibrary.SQuote(updateMisslleniousReceipt.BatchNo) + ",@Grade="+DBLibrary.SQuote(updateMisslleniousReceipt.Grade)+"";
                    DataSet ds = DbUtility.GetDS(MisslleniousReceiptQuery, this.ConnectionString);
                    response.Result = JsonConvert.SerializeObject(ds);
                    response.Result = "1";
                }
                else
                {
                    //response.addError(ErrorCode);
                    response.Result = "-3";//QAD Error
                }
                //  }
                //else
                //{
                //    response.Result = "-1";//connection Error 
                //}
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
        public async Task<Payload<string>> GetMiscellaneousIssue(GetMiscellaneousIssueModel getMiscellaneousIssue)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                StringBuilder sp = new StringBuilder();
                sp.AppendLine("Exec USP_INV_CS_IL_getMiscellaneousList @MaterialMasterID =" + getMiscellaneousIssue.MaterialMasterID + ";" + "EXEC[dbo].[sp_INV_GetAvailbleDataForMisslleniousIssues] @MaterialMasterID = " + getMiscellaneousIssue.MaterialMasterID + ", @WarehouseID = " + getMiscellaneousIssue.WarehouseID+",@TenantID ="+getMiscellaneousIssue.TenantID+"");
                string output = sp.ToString();
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
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
        //cplumns
        public async Task<Payload<string>> GetColumns(GetColumnsInputModel getColumnsInput)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                  {"@AccountID",getColumnsInput.AccountID },
                  {"@TenantID",getColumnsInput.TenantID },
                  {"@UserID",getColumnsInput.UserID },
                  {"@MenuID",getColumnsInput.MenuID },
                 // {"@MaterialMasterID",getColumnsInput.MaterialMasterID },

                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "USP_GetConfigurationColmuns", sqlParams).ConfigureAwait(false);
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
        //current stock columns
        public async Task<Payload<string>> UpsertColumns(UpsertColumnsInputModel getColumnsInput)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();

                XmlSerializer serializer = new XmlSerializer(typeof(List<XMLFormat>), new XmlRootAttribute("root"));
                string OutputXML = "";
                using (var stream = new StringWriter())
                {
                    serializer.Serialize(stream, getColumnsInput.data);
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
                  {"@DynamicXml", "'"+updatedXmlString+"'" },
                  {"@AccountID",getColumnsInput.AccountID },
                  {"@TenantID",getColumnsInput.TenantID },
                  {"@UserID",getColumnsInput.UserID },
                  {"@MenuID",getColumnsInput.MenuID },
                };

                var Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "Usp_UpsertGEN_CNF_DynamicColumns", sqlParams).ConfigureAwait(false);
                response.Result = "1";
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


        public async Task<Payload<string>> PickingItem(PickingItemModel items)

        {
            Payload<string> response = new Payload<string>();
            string uniqueID = "MI" + DateTime.Now.ToString("ddhhmmss");
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                string Query = "EXEC [dbo].[USP_INV_M_IL_Load_MaterialDetails] @MaterialMasterID = " + items.MaterialMasterID + "";
                DataSet ds = DbUtility.GetDS(Query, this.ConnectionString);

                var jsonresult1 = JsonConvert.SerializeObject(ds);

                JObject data = JObject.Parse(jsonresult1);
                JArray table2 = (JArray)data["Table"];
                var Uom = (string)table2[0]["UoM"];
                var MCode = (string)table2[0]["MCode"];

                //string qadurl = this.APIURL;
                //string URL = "" + qadurl + "/Inbound/GetMiscRecptXMLData";
                //string urlParameters = "?batchNo=" + items.BatchNo + "&&projectRefNo=" + items.ProjectRefNo + "&&Qty=" + items.SOQuantity + "&&remks=" + items.Remarks + "&&part=" + MCode + "&&um=" + Uom + "&&val=" + -1 + "&&qadAccount=" + items.qadAccount + "&&QADLocation=Holding&&uniqueID=" + uniqueID + "";
                //using (HttpClient client = new HttpClient())
                //{
                //    client.BaseAddress = new Uri(URL);
                //    client.DefaultRequestHeaders.Accept.Add(
                //   new MediaTypeWithQualityHeaderValue("application/json"));
                //    HttpResponseMessage result = client.GetAsync(urlParameters).Result;
                //    if (result.IsSuccessStatusCode)
                //    {
                //        string jsonString = result.Content.ReadAsStringAsync().Result;
                //        JavaScriptSerializer serializer = new JavaScriptSerializer();
                //        dynamic item = serializer.Deserialize<object>(jsonString);
                //        string test = item["result"];
                //        string ErrorCode = item["errorcode"];
                string test = "success";
                if (test == "success")
                {
                    StringBuilder query = new StringBuilder();
                    query.Append(" DECLARE @NewResult int ");
                    query.Append(" Exec [sp_INV_MisslleniousIssueUpdate] @MaterialMasterID=" + items.MaterialMasterID);
                    query.Append(" ,@SOQunatity='" + items.SOQunatity+ "'");
                    query.Append(" ,@LOCATION =" + DBUtil.DBLibrary.SQuote(items.Location));
                    query.Append(" ,@ActiveStockDetailsID=" + items.ActiveStockDetailsID);
                    query.Append(" ,@Remarks=" + DBUtil.DBLibrary.SQuote(items.Remarks));
                    query.Append(" ,@StorageLocationID =" + items.SLOCID);
                    query.Append(",@WarehouseID=" + Convert.ToInt32(items.WarehouseID));
                    query.Append(",@TenantID=" + items.TenantID);
                    query.Append(",@AccountID=" + items.AccountID);
                    query.Append(" ,@UpdatedBy =" + items.UserID.ToString());
                    query.Append(",@UniqueID='" + uniqueID + "'");
                    query.Append(" ,@Result=@NewResult output select @NewResult as N");
                    string Querystring = query.ToString();

                    var DS = DbUtility.GetDS(Querystring, ConnectionString);
                    var jsonresult = JsonConvert.SerializeObject(DS);

                    JObject dataresult = JObject.Parse(jsonresult);
                    JArray table1 = (JArray)dataresult["Table"];
                    var Result = (int)table1[0]["N"];

                    response.Result = JsonConvert.SerializeObject(Result);

                    int OBDID = 0;
                    if (DS.Tables[0].Rows.Count > 0)
                    {
                        OBDID = Convert.ToInt32(DS.Tables[0].Rows[0][0]);
                    }
                    if (OBDID != 0)
                    {
                        response.Result = "1";//Success
                    }
                    else
                    {
                        response.Result = "-1";//Error While Picking
                    }
                }
                //else
                //{
                //    //response.addError(ErrorCode);
                //    response.Result = "-2"; //Error Code
                //}
                //}
                else
                {
                    response.Result = "-3";  //"Connection Error"
                }
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


        public async Task<Payload<string>> GetCurrentStockReportBySearch(GetCurrentStockReportInputModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {

                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                string decryptedData = objE.Decryptword(obj.DataJson);

                // Deserialize decrypted data to a list of MaterialAttribute objects
                List<MaterialAttribute> materialAttributes = JsonConvert.DeserializeObject<List<MaterialAttribute>>(decryptedData);

                // Serialize the list to XML
                XmlSerializer serializer = new XmlSerializer(typeof(List<MaterialAttribute>), new XmlRootAttribute("root"));
                string outputXml = "";
                using (var stream = new StringWriter())
                {
                    serializer.Serialize(stream, materialAttributes);
                    string xml = stream.ToString();
                    outputXml = xml.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");
                    outputXml = outputXml.Replace("xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", "");
                }

                XDocument xmlDoc = XDocument.Parse(outputXml);
                foreach (var element in xmlDoc.Descendants("XMLFormat").ToList())
                {
                    element.Name = "data";
                }
                string updatedXmlString = xmlDoc.ToString();

                var sqlParams = new Dictionary<string, object> {
                        {"@TenantID",obj.TenantID },
                        {"@MaterialMasterID",obj.MaterialMasterID },
                        {"@MTypeID",obj.MTypeID },
                        {"@DrawTypeID",obj.DrawTypeID },
                        {"@BatchNo",obj.BatchNo },
                        {"@LocationID",obj.LocationID },
                        {"@KitID",obj.KitID },
                        {"@AccountID_New",obj.AccountID },
                        {"@UserTypeID_New",obj.UserTypeID },
                        {"@TenantID_New",obj.TenantID },
                        {"@UserID_New",obj.UserID },
                        {"@IndustryXML",updatedXmlString},
                        {"@GEN_MST_Industry_ID",obj.IndustryID },
                        {"@WarehouseID",obj.WarehouseID },
                        {"@OEMPartNo",obj.OEMPartNo },
                        {"@MenuID",obj.MenuID },
                        {"@Rownumber",obj.Rownumber },
                        {"@NofRecordsPerPage",obj.NofRecordsPerPage },
                        {"@SearchType",obj.SearchType },
                        {"@searchText",obj.searchText },
                        {"@IsExcel",obj.IsExcel },
                        {"@CartonID",obj.CartonID },
                        {"@slocid",obj.SlocID}
        };

                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "USP_INV_GetCurrentStockReportDynamic", sqlParams).ConfigureAwait(false);
            }
            catch (SqlException sqlex)
            {
                response.addError(sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }



        public async Task<Payload<string>> GetCurrentStockReport(GetCurrentStockInputModel getCurrentStockInput)
        {
            Payload<string> response = new Payload<string>();
            try
            {

                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();

                string Query = " EXEC [usp_inv_getcurrentstockreportdynamic_new] @SearchType = " + getCurrentStockInput.SearchType + " ," +
                    " @searchText = " + "'" + getCurrentStockInput.searchText + "'" + " , @TenantID = " + "'" + getCurrentStockInput.TenantID + "'" + " ," +
                    " @MaterialMasterID = " + "'" + getCurrentStockInput.MaterialMasterID + "'" + " , @MTypeID = " + getCurrentStockInput.MTypeID + " , @BatchNo = " + "'" + getCurrentStockInput.BatchNo + "'" + " ," +
                    " @LocationID = " + getCurrentStockInput.LocationID + " , @KitID = " + getCurrentStockInput.KitID + " , @AccountID_New = " + getCurrentStockInput.AccountID_New + " , " + 
                    " @UserTypeID_New = " + getCurrentStockInput.UserTypeID_New + " , " + "@IsExcel = " + getCurrentStockInput.IsExcel + " , " +
                    "@TenantID_New = " + getCurrentStockInput.TenantID_New + " , @UserID_New = " + getCurrentStockInput.UserID_New + " , " +
                    "@IndustryXML = " + "'" + getCurrentStockInput.IndustryXML + "'" + ", @GEN_MST_Industry_ID = " + getCurrentStockInput.GEN_MST_Industry_ID + ", @WarehouseID = " + getCurrentStockInput.WarehouseID + "," +
                    "@OEMPartNo = " + "'" + getCurrentStockInput.OEMPartNo + "'" + ",@MenuID = " + getCurrentStockInput.MenuID + ",@Rownumber = " + getCurrentStockInput.Rownumber + ", " +
                    "@NofRecordsPerPage = " + getCurrentStockInput.NofRecordsPerPage + ",@slocid = " + getCurrentStockInput.slocid + ",@DrawTypeID = " + getCurrentStockInput.DrawTypeID + ",@CartonID = " + getCurrentStockInput.CartonID + "";

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

        public async Task<Payload<string>> GetReserveStockReport(ReserveStockModelItems StockItems)
        {

            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
            {
                {"@tenantid",StockItems.tenantid},
                {"@materialmasterid",StockItems.materialmasterid},
                {"@warehouseid",StockItems.warehouseid},
                {"@rownumber",StockItems.rownumber},
                {"@nofrecordsperpage",StockItems.nofrecordsperpage},
                {"@isexcel",StockItems.isexcel},
                {"@loginaccountid",StockItems.loginaccountid},
                {"@loginuserid",StockItems.loginuserid},
                {"@logintanentid",StockItems.logintanentid}
            };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                //response.Result = await DbUtility.GetjsonData(this.ConnectionString, "usp_inv_reservestockreport", sqlParams).ConfigureAwait(false);
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "usp_inv_reservestockreport", sqlParams).ConfigureAwait(false);
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
