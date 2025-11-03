using Simpolo_Endpoint.DAO.interfaces;
using Simpolo_Endpoint.DBUtil;
using Simpolo_Endpoint.Models;
using Simpolo_Endpoint.ModelsLibrary;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using SixLabors.ImageSharp;
using static Simpolo_Endpoint.DBUtil.DBLibrary;
using System.Drawing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Processors.Transforms;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace Simpolo_Endpoint.DAO.Services
{
    public class AccountService : AppDBService, IAccount
    {

        private readonly IWebHostEnvironment _webHostEnvironment;
        

        public AccountService(IOptions<AppSettings> appSettings, IWebHostEnvironment webHostEnvironment)
        : base(appSettings)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        
       
        public async Task<Payload<string>> GetAccountList(GetAccountListModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                    { "UserTypeID_New" , items.UserTypeID_New },
                    { "TenantID_New" , items.TenantID_New },
                    { "UserID_New" , items.UserID_New },
                    { "AccountID_New" , items.AccountID_New },
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_GEN_GetAccountInfo", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<AuthResponce>> GetAccountDetails(GetAccountDetailsModel items)
        {
            Payload<AuthResponce> response = new Payload<AuthResponce>();
            try
            {
                AuthResponce authResponce = new AuthResponce();
                string encodedFileAsBase64 = "";

                var sqlParams = new Dictionary<string, object>
                {
                    { "UserID_New", items.UserID_New },
                    { "AccountID_New", items.AccountID_New },
                    { "AccountID", items.AccountID },
                };

                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();

                var tableData = await DbUtility.GetjsonData(this.ConnectionString, "USP_GEN_GetAccountInfo", sqlParams).ConfigureAwait(false);
                JArray dataArray = JArray.Parse(tableData);
                JObject dataObject = (JObject)dataArray[0];

                string logo = (string)dataObject["React_LogoPath"];

                if (!string.IsNullOrEmpty(logo))
                {
                    string imageURL = Path.Combine(this.FolderPath, logo);
                    string encodedUrl = Convert.ToBase64String(Encoding.Default.GetBytes(imageURL));

                    using (var client = new WebClient())
                    {
                        byte[] dataBytes = client.DownloadData(new Uri(imageURL));
                        encodedFileAsBase64 = Convert.ToBase64String(dataBytes);
                    }
                }
                else
                {
                    authResponce.Base64Image = "null";
                }

                authResponce.Base64Image = encodedFileAsBase64;
                authResponce.UserInfo = tableData;
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


        public async Task<Payload<string>> UpsertAccount(UpsertAccountModel items)
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
                    filePath = items.LogoPath;
                }

                var result = "";
#pragma warning disable CS0219 // The variable 'result1' is assigned but its value is never used
                string result1 = "";
#pragma warning restore CS0219 // The variable 'result1' is assigned but its value is never used
                int NewUserID = 0;
                if (items.AccountID != 0)
                {
                    DBFactory factory = new DBFactory();
                    IDBUtility DbUtility = factory.getDBUtility();
                    //flag=0
                    if (items.AccountID != 0)
                    {
                        var sqlParams = new Dictionary<string, object> {
                            { "AccountName", "'"+items.AccountName+"'" },
                            { "AccountID", items.AccountID },
                            { "flag", 0}
                        };

                        result = await DbUtility.GetjsonData(this.ConnectionString, "USP_MST_CheckExistAccount", sqlParams).ConfigureAwait(false);
                        if (result.Equals("[]") && result.Length == 2)
                        {
                            result1 = "0";
                        }
                        else
                        {
                            response.Result = "5";                     
                            return response;
                        }
                    }
                    else  //flag=1
                    {
                        var sqlParams = new Dictionary<string, object> {
                            { "AccountName", "'"+items.AccountName+"'"  },
                            { "AccountID", items.AccountID },
                            { "flag", 1}
                        };
                        result = await DbUtility.GetjsonData(this.ConnectionString, "USP_MST_CheckExistAccount", sqlParams).ConfigureAwait(false);
                        if (result.Equals("[]"))
                        {
                            result1 = "0";
                        }
                        else
                        {
                            response.Result = "5";
                            //response.addError("Account Name Already Exists");
                            return response;
                        }
                    }
                    //flag=2
                    if (items.AccountID != 0)
                    {
                        var sqlParams = new Dictionary<string, object> {
                            { "AccountCode", "'"+items.accountCode+"'" },
                            { "AccountID", items.AccountID },
                            { "flag", 2}
                        };
                        result = await DbUtility.GetjsonData(this.ConnectionString, "USP_MST_CheckExistAccount", sqlParams).ConfigureAwait(false);
                        JArray jsonArray = JArray.Parse(result);
                        int nValue = jsonArray[0]["N"].Value<int>();
                        if (nValue == 0)
                        {
                            result1 = "0";
                        }
                        else
                        {
                            response.Result = "6";
                            //response.addError("Account Code Already Exists");
                            return response;
                        }
                    }
                    else  //flag=3
                    {
                        var sqlParams = new Dictionary<string, object> {
                            { "AccountCode", "'"+items.accountCode+"'" },
                            { "AccountID", items.AccountID },
                            { "flag", 3}
                        };
                        result = await DbUtility.GetjsonData(this.ConnectionString, "USP_MST_CheckExistAccount", sqlParams).ConfigureAwait(false);
                        if (result.Equals("[]") && result.Length == 2)
                        {
                            result1 = "0";
                        }
                        else
                        {
                            response.Result = "6";
                            //response.addError("Account Code Already Exists");
                            return response;
                        }
                    }
                    string data;
                    if (items.AccountID != 0)
                    {
                        data = items.AccountID.ToString();
                    }
                    else
                    {
                        data = null;
                    }
                    StringBuilder sCmdPilotCr = new StringBuilder();
                    sCmdPilotCr.AppendLine("DECLARE @UpdateAccountID int EXEC ");
                    sCmdPilotCr.AppendLine("[dbo].[USP_GEN_UpsertAccount_React] ");
                    sCmdPilotCr.AppendLine("@AccountID = " + items.AccountID + ",");
                    sCmdPilotCr.AppendLine("@Account = " + "'"+items.AccountName+"'" + ",");
                    sCmdPilotCr.AppendLine("@accountCode =" + "'"+items.accountCode+"'" + ",");
                    sCmdPilotCr.AppendLine("@CompanyLegalName =" + "'"+items.CompanyLegalName+"'" + ",");
                    sCmdPilotCr.AppendLine("@CreatedBy =" + items.UserID + ",");
                    sCmdPilotCr.AppendLine("@React_LogoPath = " + DBUtil.DBLibrary.SQuote(fileName) + ",");
                    sCmdPilotCr.AppendLine("@ZohoAccountId =" + "'"+items.ZohoAccountId+"'" + ",");
                    sCmdPilotCr.AppendLine("@SSOAccountID =" + items.SSOAccountID + ",");
                    sCmdPilotCr.AppendLine("@NewAccountID = @UpdateAccountID OUTPUT;");
                    sCmdPilotCr.AppendLine("SELECT @UpdateAccountID as N;");
                    string Account = sCmdPilotCr.ToString();
                    NewUserID = DbUtility.GetSqlN(Account, ConnectionString);
                    if (data != null)
                    {
                        //Response.Redirect("AccountList.aspx?statid=Updatesuccess");
                        //resetError("Successfully updated", false);
                        response.Result = "1";
                        return response;
                    }
                    else
                    {
                        //Response.Redirect("AccountList.aspx?statid=Createsuccess");
                        //resetError("Successfully created", false);
                        response.Result = "0";
                        return response;
                    }
                }
                else
                {
                    response.Result = "2";
                    return response;
                    //resetError("Error while updating Account", true);
                }
#pragma warning disable CS0162 // Unreachable code detected
                if (NewUserID != 0)
#pragma warning restore CS0162 // Unreachable code detected
                {
                    response.Result = "3";
                    return response;
                    //Response.Redirect("AccountList.aspx?statid=success");
                }
                else
                {
                    response.Result = "4";
                    return response;
                    //resetError("Error while updating Account", true);
                    //return;
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

        public async Task<Payload<string>> GetUserList(GetUserListModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                    { "AccountID", items.AccountID },
                    { "UserIDNew", items.UserIDNew },
                    { "UserID", items.UserID },
                    { "TenantId", items.TenantId },
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "sp_GetUserList", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> UpsertUser(UpsertUserModel items)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            Payload<string> response = new Payload<string>();
            try
            {
                string password = items.Password;
                byte[] encData_byte = Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                StringBuilder sCmdPilotCr = new StringBuilder();
                sCmdPilotCr.AppendLine("DECLARE @UpdateUserID int EXEC ");
                sCmdPilotCr.AppendLine("[dbo].[sp_GEN_UpsertUsers] ");
                sCmdPilotCr.AppendLine("@UserID = " + items.UserID + ",");
                sCmdPilotCr.AppendLine("@AccountID = " + items.AccountID + ",");
                sCmdPilotCr.AppendLine("@TenantID = " + items.TenantID + ",");
                sCmdPilotCr.AppendLine("@FirstName =" + DBUtil.DBLibrary.SQuote(items.FirstName) + ",");
                sCmdPilotCr.AppendLine("@LastName =" + DBUtil.DBLibrary.SQuote(items.LastName) + ",");
                sCmdPilotCr.AppendLine("@MiddleName =" + DBUtil.DBLibrary.SQuote(items.MiddleName) + ",");
                sCmdPilotCr.AppendLine("@Email = " + DBUtil.DBLibrary.SQuote(items.Email) + ",");
                sCmdPilotCr.AppendLine("@Sex =" + items.Sex + ",");
                sCmdPilotCr.AppendLine("@AlternateEmail1 =" + DBUtil.DBLibrary.SQuote(items.AlternateEmail1) + ",");
                sCmdPilotCr.AppendLine("@AlternateEmail2 = " + DBUtil.DBLibrary.SQuote(items.AlternateEmail2) + ",");
                sCmdPilotCr.AppendLine("@Password =" + DBUtil.DBLibrary.SQuote(items.Password) + ",");
                sCmdPilotCr.AppendLine("@UserRoleIDs =" + DBUtil.DBLibrary.SQuote(items.UserRoleIDs) + ",");
                sCmdPilotCr.AppendLine("@WarehouseIDs =" + DBUtil.DBLibrary.SQuote(items.WarehouseIDs) + ",");
                sCmdPilotCr.AppendLine("@IsActive = " + items.IsActive + ",");
                sCmdPilotCr.AppendLine("@EnPassword =" + DBUtil.DBLibrary.SQuote(encodedData) + ",");
                sCmdPilotCr.AppendLine("@Mobile =" + DBUtil.DBLibrary.SQuote(items.Mobile) + ",");
                sCmdPilotCr.AppendLine("@CreatedBy =" + items.CreatedBy + ",");
                sCmdPilotCr.AppendLine("@EmployeeCode =" + DBUtil.DBLibrary.SQuote(items.EmployeeCode) + ",");
                sCmdPilotCr.AppendLine("@UserTypeID = " + items.UserTypeID + ",");
                sCmdPilotCr.AppendLine("@HashPWD =" + 0 + ",");
                sCmdPilotCr.AppendLine("@SSOUserID =" + items.SSOUserID + ",");
                sCmdPilotCr.AppendLine("@NewUserID =@UpdateUserID OUTPUT;");
                sCmdPilotCr.AppendLine("SELECT @UpdateUserID as N;");
                string Account = sCmdPilotCr.ToString();
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                int NewUserID = DbUtility.GetSqlN(Account, ConnectionString);
                if (NewUserID > 0)
                {
                    response.Result = "4";//Successfully created 
                }
                if (NewUserID == -333)
                {
                    response.Result = "1";//DO not have permission to create/update the data  
                }
                if (NewUserID == -111)
                {
                    response.Result = "2"; // Employee Code Already exists
                }
                if (NewUserID == -222)
                {
                    response.Result = "3"; //Email Already Exists
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

        public async Task<Payload<string>> GetWareHouseList(GetWarehouseListModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                     { "UserID", items.UserID },
                     { "Accountid", items.Accountid },
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "SP_GetWareHouseList", sqlParams).ConfigureAwait(false);
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

      
        public async Task<Payload<string>> DeleteWarehouse(DeleteWarehouseModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                string WHcountdock = "";
                int WareHousedock = 0;
                int WareHousecode = 0;
                StringBuilder res = new StringBuilder();
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                if (items.WarehouseID != "0")
                {
                    var sqlParams = new Dictionary<string, object> 
                    {
                        { "flag",0 },
                        { "WarehouseID", items.WarehouseID },
                    };
                    response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_MST_GetLocationORDockCount_WH", sqlParams).ConfigureAwait(false);
                    WHcountdock = response.Result;
                    JArray jsonArray = JArray.Parse(WHcountdock);
                    WareHousecode = (int)jsonArray[0]["N"];
                }

                if (items.WarehouseID != "0")
                {
                    var sqlParams = new Dictionary<string, object> 
                    {
                        { "WarehouseID", items.WarehouseID },
                        { "flag",1 },
                    };
                    response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_MST_GetLocationORDockCount_WH", sqlParams).ConfigureAwait(false);
                    WHcountdock = (response.Result);
                    JArray jsonArray1 = JArray.Parse(WHcountdock);
                    WareHousedock = (int)jsonArray1[0]["N"];
                }

                if (WareHousecode == 0 && WareHousedock == 0)
                {
                    var sqlParams = new Dictionary<string, object> {
                        { "WarehouseID", items.WarehouseID },
                    };
                    string sp = "Exec sp_Delete_Warehouse @WarehouseID=" + items.WarehouseID + "";
                    //  response.Result = await DbUtility.GetjsonData(this.ConnectionString, "sp_Delete_Warehouse", sqlParams).ConfigureAwait(false);
                    int value = DbUtility.GetSqlN(sp, ConnectionString);
                    // string value = response.Result;
                    if (value == 0)
                    {
                        response.Result = "1";//Success
                    }
                    else
                    {
                        response.Result = "-1"; // Unable to delete the WH as it contains stock
                    }
                }
                else
                {
                    response.Result = "2";  //Could not delete since Zone/Dock is mapped
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

        //New Warehouse Creation 
#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<Payload<string>> UpsertWarehouse(UpsertWarehouseModel items)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            Payload<string> response = new Payload<string>();
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                string sp = "exec[dbo].[UpsertNewWareHouse] @Warehouseid = " + items.Warehouseid + ",@WHName = " + DBUtil.DBLibrary.SQuote(items.WHName) + ",@WHCode = " + DBUtil.DBLibrary.SQuote(items.WHCode) + ",@WHGroupcode = " + items.WHGroupcode +
                ",@Location=" + DBUtil.DBLibrary.SQuote(items.Location) + ",@RackingRType=" + items.RackingRType + ",@WHtype=" + items.WHtype + ",@WHAddress=" + DBUtil.DBLibrary.SQuote(items.WHAddress) + ",@FloorSpace=" + DBUtil.DBLibrary.SQuote(items.FloorSpace) +
                ",@Measurements=" + DBUtil.DBLibrary.SQuote(items.Measurements) + ",@PIN=" + items.PIN + ",@GeoLocation=" + DBUtil.DBLibrary.SQuote(items.GeoLocation) + ",@CountryId=" + items.CountryId + ",@CurrencyId=" + items.CurrencyId +
                ",@InoutId=" + items.InoutId + ",@pName=" + DBUtil.DBLibrary.SQuote(items.pName) + ",@Pmobile=" + DBUtil.DBLibrary.SQuote(items.Pmobile) + ",@pEmail=" + DBUtil.DBLibrary.SQuote(items.pEmail) +
                ",@PAddress=" + DBUtil.DBLibrary.SQuote(items.PAddress) + ",@sname=" + DBUtil.DBLibrary.SQuote(items.sname) + ",@SMobile=" + DBUtil.DBLibrary.SQuote(items.SMobile) + ",@SEmail=" + DBUtil.DBLibrary.SQuote(items.SEmail) +
                ",@SAddress=" + DBUtil.DBLibrary.SQuote(items.SAddress) + ",@UserID=" + items.UserID + ",@AccountId=" + items.AccountId + ",@length=" + items.length + ",@Width=" + items.Width +
                ",@height=" + items.height + ",@Latitude=" + DBUtil.DBLibrary.SQuote(items.Latitude) + ",@Langitude=" + DBUtil.DBLibrary.SQuote(items.Langitude) + ",@stateid=" + items.stateid + ",@cityid=" + items.cityid + ",@TimeZoneId=" + items.TimeZoneId + ",@IsActive=" + items.IsActive + ",@IsDeleted=" + items.IsDeleted + "";
                int Output = DbUtility.GetSqlN(sp, ConnectionString);
                if (Output > 0)
                {
                    response.Result = Output.ToString(); //Successfully Saved
                }
                else
                {
                    response.Result = "2";//Successfully created 
                }
            }
            catch (SqlException Sqlex)
            {
                if (Sqlex.Message.IndexOf("Cannot insert duplicate key row in object 'dbo.GEN_Warehouse' with unique index 'UK_GEN_WHCode") > -1)
                {
                    response.Result = ("-1");   //wh code already exists under this Tenant
                }
                else
                {
                    response.Result = ("-4");    //Error while submitting the data
                }
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }
        public async Task<Payload<string>> UpsertDock(UpsertDockModel dockInputModel)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                    {"@DockNo",dockInputModel.DockNo },
                    {"@DockName",dockInputModel.DockName },
                    {"@WarehouseID",dockInputModel.WarehouseID },
                    {"@DockID",dockInputModel.DockID },
                    {"@DockTypeID",dockInputModel.DockTypeID },
                };
                bool status = DBLibrary.isAlphaNumeric(dockInputModel.DockNo);
                if (status == false)
                {
                    response.Result = "2"; //please enter alpha numeric
                }
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_UpsertGEN_Dock", sqlParams).ConfigureAwait(false);
                if (response.Result == "" || response.Result == "[]")
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

        public async Task<Payload<string>> UpsertZone(UpsertZoneModel zoneInputModel)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                    {"@ZoneCode",zoneInputModel.ZoneCode },
                    {"@ZoneDesc",zoneInputModel.ZoneDesc },
                    {"@WarehouseID",zoneInputModel.WarehouseID },
                    {"@ZoneID",zoneInputModel.ZoneID },
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_UpsertGEN_Zone", sqlParams).ConfigureAwait(false);
                if (response.Result == "[]" || zoneInputModel.ZoneCode != "")
                {
                    response.Result = "2";//zone code must not be empty
                }
                else 
                {
                    response.Result = "1";  //Success
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

        public async Task<Payload<string>> DeleteZone(DeleteZoneModel deleteZoneInput)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                    {"@Warehouseid",deleteZoneInput.WarehouseID },
                    {"@ZoneId",deleteZoneInput.ZoneID },
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                var ZoneID = await DbUtility.GetjsonData(this.ConnectionString, "DeleteZone", sqlParams).ConfigureAwait(false);
                JArray jsonArray = JArray.Parse(ZoneID);
                int nValue = jsonArray[0]["N"].Value<int>();
                if (nValue == 0)
                {
                    response.Result = "1";  //successfully delete
                }
                else
                {
                    //Could not Delete since ISDockzone / location is mapped
                    response.Result = "2";  //check input data
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