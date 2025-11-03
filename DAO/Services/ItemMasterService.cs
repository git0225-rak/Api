using Simpolo_Endpoint.DAO.interfaces;
using Simpolo_Endpoint.DBUtil;
using Simpolo_Endpoint.Models;
using Simpolo_Endpoint.ModelsLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Simpolo_Endpoint.DAO.Services
{
    public class ItemMasterService: AppDBService, IItemMasterData
    {
        public ItemMasterService(IOptions<AppSettings> appSettings) : base(appSettings)
        {
        }
        public async Task<Payload<string>> GetMaterialList(GetMaterialListModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                    { "OEMPartNumber" , items.OEMPartNumber },
                    { "SupplierID" , items.SupplierID },
                    { "Supplier" , items.Supplier },
                    { "MCode" , items.MCode },
                    { "Description" , items.Description },
                    { "IsMMAdminApproved" , items.IsMMAdminApproved },
                    { "MTypeID" , items.MTypeID },
                    { "TenantName" , items.TenantName },
                    { "AccountID_New" , items.AccountID_New },
                    { "TenantID_New" , items.TenantID_New },
                    { "PageIndex" , items.PageIndex },
                    { "PageSize" , items.PageSize },
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "sp_TPL_MaterialMasterList", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetItemMasterAutocompletes(GetItemMasterAutocompletesModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                    { "TenantID" , items.TenantID },
                    { "AccountID" , items.AccountID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "USP_MMT_MasterDataList", sqlParams).ConfigureAwait(false);
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
      

        public async Task<Payload<string>> GetMaterailParametersInfo(GetMaterailParametersInfoModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                    { "@MaterialMasterID" , items.MaterialMasterID },
                    { "@TenantID_New" , items.TenantID },
                    { "@AccountID_New" , items.AccountID_New }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "USP_MMT_GETMaterailParametersInfo ", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> UpsertItemMasterBasicDetails([FromBody] UpsertItemMasterBasicDetailsModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                if (items.MaterialMasterID ==0)
                {
                    String sql = "EXEC[USP_MST_IM_IL_UpsertBasicDetails]@TenantID = " + items.TenantID + ",@AccountID = " + items.AccountID + ",@Mcode = '" + items.Mcode+"'";
                    int itemcount = DbUtility.GetSqlN(sql, ConnectionString);
                    if (itemcount != 0)
                    {
                        response.Result = "-1"; // Mcode Exists 
                        return response;
                    }                   
                }
                string json = JsonConvert.SerializeObject(items.XmlData);
                string xml = JsonConvert.DeserializeXmlNode(json, "root").InnerXml;
                var sqlParams = new Dictionary<string, object> {
                    { "@DataXml", xml },
                    { "@LoggedInUserID", items.LoggedInUserID },
                    { "@TenantID", items.TenantID },
                    { "@AccountID", items.AccountID },
                };
                var TableData = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "USP_UPSERT_MMTMasterXMLData", sqlParams).ConfigureAwait(false);
                JObject data = JObject.Parse(TableData);
                JArray table = (JArray)data["Table"];
                int MaterialID = (int)table[0]["MaterialID"];
                response.Result = MaterialID.ToString();
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

        public async Task<Payload<string>> UpsertUoMInfo(UpsertUoMInfoModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                string Query = "EXEC [dbo].[sp_MMT_CheckIsUoMConfigured] @MaterialMasterUoMID=" + items.MaterialMaster_UoMID;
                int UoMCheck = DbUtility.GetSqlN(Query, ConnectionString);
                if (UoMCheck != 0)
                {
                    var CheckUoM = UoMCheck.ToString();

                }
                StringBuilder sqlUoMDetails = new StringBuilder(2500);
                sqlUoMDetails.Append("EXEC  [dbo].[sp_MMT_UpsertMaterialMaster_GEN_UoM]    @MaterialMaster_UoMID=" + items.MaterialMaster_UoMID + ",@TenantID=" + items.TenantID + ",@MaterialMasterID=" + items.MaterialMasterID + ",@UoMTypeID=" + items.UoMTypeID + ",@UoMID=" + items.UoMID + ",@UoMQty=" + items.UoMQty + ",@CreatedBy=" + items.CreatedBy);

                string UOM = sqlUoMDetails.ToString();
                var Result = await DbUtility.GetjsonDataFromDatasetINQ(this.ConnectionString, UOM).ConfigureAwait(false);
                if (Result == "{}")
                {
                    response.Result = "1";//Success
                }
                else
                {
                    response.Result = "-1";//Fail
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

        //added by Prasanna
        public async Task<Payload<string>> SaveUpdateSupplierDetails(SaveUpdateSupplierDetailsModel SaveUpdateSupplier)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                StringBuilder sCmdPilotCr = new StringBuilder();

                sCmdPilotCr.AppendLine("EXEC [dbo].[sp_MMT_UpsertMaterialMaster_Supplier]");
                sCmdPilotCr.AppendLine("@MaterialMaster_SupplierID =" + (SaveUpdateSupplier.MaterialMaster_SupplierID) + ",");
                sCmdPilotCr.AppendLine("@MaterialMasterID =" + (SaveUpdateSupplier.MaterialMasterID) + ",");
                sCmdPilotCr.AppendLine("@SupplierPartNo =" + (SaveUpdateSupplier.SupplierPartNo) + ",");
                sCmdPilotCr.AppendLine("@SupplierID =" + (SaveUpdateSupplier.SupplierID) + ",");
                sCmdPilotCr.AppendLine("@TenantID =" + (SaveUpdateSupplier.TenantID) + ",");
                sCmdPilotCr.AppendLine("@InitialOrderQuantity =" + (SaveUpdateSupplier.InitialOrderQuantity) + ",");
                sCmdPilotCr.AppendLine("@ExpectedUnitCost =" + (SaveUpdateSupplier.ExpectedUnitCost) + ",");
                sCmdPilotCr.AppendLine("@PlannedDeliveryTime =" + (SaveUpdateSupplier.PlannedDeliveryTime) + ",");
                sCmdPilotCr.AppendLine("@CurrencyID =" + (SaveUpdateSupplier.CurrencyID) + ",");
                sCmdPilotCr.AppendLine("@CreatedBy =" + (SaveUpdateSupplier.CreatedBy) + ";");
                string saveUpdateSupplierDetails = sCmdPilotCr.ToString();
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                int NewUpdateSupplierID = DbUtility.GetSqlN(saveUpdateSupplierDetails, ConnectionString);
                if (NewUpdateSupplierID != 1)  //to Check Supplier Name duplication while in Edit Mode OR New Record
                {
                    if (NewUpdateSupplierID != 2) 
                    {
                        response.Result = "error";//Supplier Name is already existing.
                    }
                    else
                    {
                        response.Result = "2";// updating existing supplier
                    }
                    
                }
                else if (NewUpdateSupplierID == 1) //If insertion or updation is successful,
                {
                    response.Result = "1";     //Successfully Saved
                }
            }
            catch (SqlException ex)
            {
                if (ex.Message.IndexOf(" Cannot insert duplicate key in object 'dbo.MMT_Supplier'.") > -1)
                {
                    response.Result = ("3");   //Supplier code already exists under this Tenant
                }
                else
                {
                    response.Result = ("4");  //Error while submitting the data
                }
            }

            catch (Exception ex)

            {
                response.Result = ("Error while submitting the data");
            }
            return response;
        }

        public async Task<Payload<string>> SetMsps(SetMspsModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                XmlSerializer serializer = new XmlSerializer(typeof(List<XMLService>), new XmlRootAttribute("root"));
                string OutputXML = "";
                using (var stream = new StringWriter())
                {
                    serializer.Serialize(stream, items.data);
                    string xml = stream.ToString();
                    OutputXML = xml.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");
                    OutputXML = OutputXML.Replace("xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\"", "");
                }
                XDocument xmlDoc = XDocument.Parse(OutputXML);
                foreach (var element in xmlDoc.Descendants("XMLService").ToList())
                {
                    element.Name = "data";
                }
                string XmlString = xmlDoc.ToString();
                StringBuilder sb = new StringBuilder();
                sb.Append(" Exec [dbo].[USP_UPSERT_MMTMSPXMLData] ");
                sb.Append(" @DataXml='" + XmlString + "'");
                sb.Append(" ,@LoggedInUserID=" + items.UserID);
                var result = await DbUtility.GetjsonDataFromDatasetINQ(this.ConnectionString, sb.ToString());
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

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<Payload<string>> Check_MCodeExists(GetMCodeCheck items)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            Payload<string> response = new Payload<string>();
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                string sql = "Exec USP_MST_IM_IL_PartCheck @TenantID=" + items.TenantID + ",@MCode=" + DBUtil.DBLibrary.SQuote(items.MCode);
                int PCheck = DbUtility.GetSqlN(sql, ConnectionString);
                if (PCheck != 0)
                {
                    response.Result = "1"; //Exists
                }
                else
                {
                    response.Result = "-1";//Not Exist
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

        public async Task<Payload<AuthResponce>> GetItemPictureDetails(GetItemPictureDetailsModel items)
        {
            Payload<AuthResponce> response = new Payload<AuthResponce>();
            try
            {
                AuthResponce authResponce = new AuthResponce();
                string encodedFileAsBase64 = "";
                var sqlParams = new Dictionary<string, object> 
                {
                    { "@MaterialMasterID", items.MaterialMasterID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                var tableData = await DbUtility.GetjsonData(this.ConnectionString, "GET_MaterialMaster_PictureDetails", sqlParams).ConfigureAwait(false);
                JArray dataArray = JArray.Parse(tableData);
                JObject dataObject = (JObject)dataArray[0];
                string logo = dataObject["FilePath"]?.ToString();
                if (logo != "")
                {
                    //string logo = (string)dataObject["FilePath"];
                    //string imageName = "20230428165046512.png";
                    //string FolderPath = @"\\192.168.1.20\GSK_Endpoint_Images\";
                    //string url = Path.Combine(this.FolderPath, logo);
                    string imageURL = Path.Combine(this.FolderPath, logo);
                    string encodedUrl = Convert.ToBase64String(Encoding.Default.GetBytes(imageURL));
                    using (var client = new WebClient())
                    {
                        byte[] dataBytes = client.DownloadData(new Uri(imageURL));
                        encodedFileAsBase64 = Convert.ToBase64String(dataBytes);
                    }
                    authResponce.Base64Image = encodedFileAsBase64;
                    authResponce.UserInfo = tableData;
                    response.Result = authResponce;
                }
                else
                {           
                    response.Result = authResponce;
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

        public async Task<Payload<string>> UpsertItemPictureDetails(UpsertItemPictureDetailsModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                string fileName = "";
                string filePath = "";
                string Filespace = this.FolderPath;
                //string Filespace = @"\\192.168.1.20\GSK_Endpoint_Images\";
                //items.imageurl = @"\\192.168.1.20\GSK_Endpoint_Images\";
                if (items.imageurl != "")
                {
                    string base64String = items.imageurl;
                    byte[] fileBytes = Convert.FromBase64String(base64String);
                    string extension = items.Type;
                    fileName = $"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.{extension}";
                    filePath = Path.Combine(Filespace, fileName);
                    File.WriteAllBytes(filePath, fileBytes);
                }
                else
                {
                    filePath = items.fileName;
                }
                var result = "";
#pragma warning disable CS0219 // The variable 'result1' is assigned but its value is never used
                string result1 = "";
#pragma warning restore CS0219 // The variable 'result1' is assigned but its value is never used
#pragma warning disable CS0219 // The variable 'NewUserID' is assigned but its value is never used
                int NewUserID = 0;
#pragma warning restore CS0219 // The variable 'NewUserID' is assigned but its value is never used
                if (items.MaterialMasterID != 0)
                {
                    DBFactory factory = new DBFactory();
                    IDBUtility DbUtility = factory.getDBUtility();
                    //flag=0
                    if (items.MaterialMasterID != 0)
                    {
                        var sqlParams = new Dictionary<string, object> {
                            { "@MaterialMasterID", items.MaterialMasterID },
                            { "@FilePath", fileName },
                        };

                        result = await DbUtility.GetjsonData(this.ConnectionString, "Save_MaterialPicture", sqlParams).ConfigureAwait(false);
                        if (result.Equals("[]") && result.Length == 2)
                        {
                            response.Result = "1";                        
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
    }
}