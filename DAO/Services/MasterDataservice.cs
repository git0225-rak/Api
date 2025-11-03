using Simpolo_Endpoint.DAO.interfaces;
using Simpolo_Endpoint.DBUtil;
using Simpolo_Endpoint.Models;
using Simpolo_Endpoint.ModelsLibrary;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Text.Json;
using static Simpolo_Endpoint.DBUtil.DBLibrary;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Xml.Linq;
using SixLabors.ImageSharp;

namespace Simpolo_Endpoint.DAO.Services
{
    public class MasterDataservice : AppDBService, IMasterData
    {
        Encryption objE = new Encryption();
        public MasterDataservice(IOptions<AppSettings> appSettings) : base(appSettings)
        {

        }

        public async Task<Payload<string>> GetSupplierList(GetSupplierlistModel supplierlist)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                int? UserRoleID = null;
                var sqlParams = new Dictionary<string, object> {
                    {"@SupplierCode",supplierlist.SupplierCode },
                    {"@SupplierName",supplierlist.SupplierName },
                    {"@UserRoleID", UserRoleID ?? (object)DBNull.Value },
                    {"@TenantName",supplierlist.TenantName },
                    {"@AccountID_New",supplierlist.AccountID_New },
                    {"@UserTypeID",supplierlist.UserTypeID },
                    {"@TenantID_New",supplierlist.TenantID_New },
                    {"@TenantID",supplierlist.TenantID}
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "sp_TPL_GetSupplierList", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> DeleteSupplier(SaveSupplierDetailsInputModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>{
                {"@supplierid",obj.SupplierID },
                {"@loggedinuserid",obj.UpdatedBy},
                {"@loginaccountid",obj.LoginAccountId},
                {"@logintanentid",obj.LoginTanentId},
              };

                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "GEN_DELETE_Supplier", sqlParams).ConfigureAwait(false);

                if (response.Result == "[{\"N\":1}]")
                {
                    response.Result = "1";   //Success
                }
                else if (response.Result == "[{\"N\":-1}]")
                {
                    response.Result = "-1";   //can't be deleted  supplier is mapped with that orders
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


        public async Task<Payload<string>> DeleteCustomerInfo(SaveCustomerDetailsInputModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>{
                {"@CustomerID",obj.CustomerID},
                {"@LoginAccountId",obj.LoginAccountId},
                {"@LoggedInUserID",obj.LoginUserId},
                {"@LoginTanentId",obj.LoginTanentId}
              };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "GEN_DELETE_Customer", sqlParams).ConfigureAwait(false);
                if (response.Result == "[{\"N\":1}]")
                {
                    response.Result = "1";   //Success
                }
                else if (response.Result == "[{\"N\":-1}]")
                {
                    response.Result = "-1";   //Could not delete this customer , because customer mapped to 
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
        public async Task<Payload<string>> GetSupplierDetails(GetSupplierDetailsModel supplierDetails)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                    {"@SupplierID",supplierDetails.SupplierID },
                    {"@AccountID_New",supplierDetails.AccountID_New },
                    {"@TenantID_New",supplierDetails.TenantID_New },
                    {"@TenantID",supplierDetails.TenantID },
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "sp_MMT_SupplierDetails ", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> UpsertSupplierDetails(UpsertSupplierDetailsModel updateSupplier)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            Payload<string> response = new Payload<string>();
            try
            {
                StringBuilder sCmdPilotCr = new StringBuilder();
                sCmdPilotCr.AppendLine("DECLARE @NewUpdateSupplierID int EXEC ");
                sCmdPilotCr.AppendLine("[dbo].[sp_MMT_UpsertSupplier]");
                sCmdPilotCr.AppendLine("@SupplierName =" + DBUtil.DBLibrary.SQuote(updateSupplier.SupplierName) + ",");
                sCmdPilotCr.AppendLine("@Address1 =" + DBUtil.DBLibrary.SQuote(updateSupplier.Address1) + ",");
                sCmdPilotCr.AppendLine("@Address2 =" + DBUtil.DBLibrary.SQuote(updateSupplier.Address2) + ",");
                sCmdPilotCr.AppendLine("@Phone1 =" + DBUtil.DBLibrary.SQuote(updateSupplier.Phone1) + ",");
                sCmdPilotCr.AppendLine("@Phone2 =" + DBUtil.DBLibrary.SQuote(updateSupplier.Phone2) + ",");
                sCmdPilotCr.AppendLine("@Mobile =" + DBUtil.DBLibrary.SQuote(updateSupplier.Mobile) + ",");
                sCmdPilotCr.AppendLine("@Fax =" + DBUtil.DBLibrary.SQuote(updateSupplier.Fax) + ",");
                sCmdPilotCr.AppendLine("@EmailAddress =" + DBUtil.DBLibrary.SQuote(updateSupplier.EmailAddress) + ",");
                sCmdPilotCr.AppendLine("@PCP =" + DBUtil.DBLibrary.SQuote(updateSupplier.PCP) + ",");
                sCmdPilotCr.AppendLine("@PCPTitle =" + DBUtil.DBLibrary.SQuote(updateSupplier.PCPTitle) + ",");
                sCmdPilotCr.AppendLine("@PCPContactNumber =" + DBUtil.DBLibrary.SQuote(updateSupplier.PCPContactNumber) + ",");
                sCmdPilotCr.AppendLine("@PCPEmail =" + DBUtil.DBLibrary.SQuote(updateSupplier.PCPEmail) + ",");
                sCmdPilotCr.AppendLine("@BankName =" + DBUtil.DBLibrary.SQuote(updateSupplier.BankName) + ",");
                sCmdPilotCr.AppendLine("@BankAddress =" + DBUtil.DBLibrary.SQuote(updateSupplier.BankAddress) + ",");
                sCmdPilotCr.AppendLine("@BankCountryID  = " + updateSupplier.BankCountryID + ",");
                sCmdPilotCr.AppendLine("@AccountNo =" + DBUtil.DBLibrary.SQuote(updateSupplier.AccountNo) + ",");
                sCmdPilotCr.AppendLine("@SortCodeORBLZCode = " + 0 + ",");
                sCmdPilotCr.AppendLine("@IBANNo =" + 0 + ",");
                sCmdPilotCr.AppendLine("@SwiftCode =" + 0 + ",");
                sCmdPilotCr.AppendLine("@CurrencyID  = " + updateSupplier.CurrencyID + ",");
                sCmdPilotCr.AppendLine("@SupplierCode = " + DBUtil.DBLibrary.SQuote(updateSupplier.SupplierCode) + ",");
                sCmdPilotCr.AppendLine("@CreatedBy  = " + updateSupplier.CreatedBy + ",");
                sCmdPilotCr.AppendLine("@TenantID  = " + updateSupplier.TenantID + ",");
                sCmdPilotCr.AppendLine("@CountryMasterID  = " + updateSupplier.CountryMasterID + ",");
                sCmdPilotCr.AppendLine("@AccountId  = " + updateSupplier.AccountId + ",");
                sCmdPilotCr.AppendLine("@SupplierID  = " + updateSupplier.SupplierID + ",");
                sCmdPilotCr.AppendLine("@RequestedBy  = " + updateSupplier.RequestedBy + ",");
                sCmdPilotCr.AppendLine("@SearchTerm = " + DBUtil.DBLibrary.SQuote(updateSupplier.SearchTerm) + ",");
                sCmdPilotCr.AppendLine("@IsApproved  = " + updateSupplier.IsApproved + ",");
                sCmdPilotCr.AppendLine("@IsActive  = " + updateSupplier.IsActive + ",");
                sCmdPilotCr.AppendLine("@IsFirstEdit  = " + updateSupplier.IsFirstEdit + ",");
                sCmdPilotCr.AppendLine("@LastEditedByID  = " + updateSupplier.LastEditedByID + ",");
                sCmdPilotCr.AppendLine("@SupplierCodeAprEditCount  = " + updateSupplier.SupplierCodeAprEditCount + ",");
                sCmdPilotCr.AppendLine("@NewSupplierID =@NewUpdateSupplierID OUTPUT;");
                sCmdPilotCr.AppendLine("Select @NewUpdateSupplierID as N;");
                string UpdateSupplier = sCmdPilotCr.ToString();
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                int NewUpdateSupplierID = DbUtility.GetSqlN(UpdateSupplier, ConnectionString);
                if (NewUpdateSupplierID == -1)  //to Check Supplier Name duplication while in Edit Mode OR New Record
                {
                    response.Result = ("2");    //Supplier Name is already existing.
                }
                else if (NewUpdateSupplierID > 0) //If insertion or updation is successful,
                {
                    response.Result = ("1");     //Successfully Saved
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
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                response.Result = ("Error while submitting the data");
            }
            return response;
        }





        public async Task<Payload<string>> GetCustomerInfo(GetCustomerInfoModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                    { "NofRecordsPerPage" , items.NofRecordsPerPage },
                    { "PageIndex" , items.PageIndex },
                    { "AccountID_New" , items.AccountID_New },
                    { "UserTypeID_New" , items.UserTypeID_New },
                    { "TenantID_New" , items.TenantID_New },
                    { "UserID_New" , items.UserID_New },
                    { "SearchText" , items.SearchText },
                    { "CustomerID" , items.CustomerID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "USP_GEN_GetCustomerInfo", sqlParams).ConfigureAwait(false);
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



        public async Task<Payload<string>> ImportCustomerData(SaveCustomerDetailsInputModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                DataTable dt = JsonConvert.DeserializeObject<DataTable>(objE.Decryptword(obj.DataJson));
                //    var sqlParams = new Dictionary<string, object>
                //{
                //    {"@SupplierExcelType", objE.Decryptword(obj.DataJson) },
                //    {"@UserID", obj.UpdatedBy},
                //    {"@LoginAccountId", obj.LoginAccountId},

                //};
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                string SP = "EXEC [dbo].[Usp_Excel_BulkInsert_CustomerImport] @CustomerExcelType=" + "'" + objE.Decryptword(obj.DataJson) + "'" + ",@LoginAccountId=" + obj.LoginAccountId + ",@UserID=" + obj.UpdatedBy + "";
                var result = DbUtility.GetSqlN(SP, ConnectionString);
                response.Result = result.ToString();

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


        public async Task<Payload<string>> UpsertCustomer([FromBody] UpsertCustomerModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                string json = JsonConvert.SerializeObject(items.Inxml);
                XmlDocument xmlData = JsonConvert.DeserializeXmlNode("{\"data\":" + json + "}", "root");
                StringBuilder sb = new StringBuilder();
                //sb.Append(" Exec [dbo].[USP_GEN_UpsertCustomer] ");
                //sb.Append(" @inputDataXml='" + (xmlData.InnerXml) + "'");
                //sb.Append(" ,@CustomerID=" + items.CusId);
                //sb.Append(" ,@CreatedBy=" + items.UserID);
                //string XML = sb.ToString();
                //int Data = DbUtility.GetSqlN(XML, ConnectionString);

                string Query = "EXEC [dbo].[USP_GEN_UpsertCustomer] @inputDataXml='" + (xmlData.InnerXml) + "',@CustomerID=" + items.CusId + ", @CreatedBy = " + items.UserID + "";
                var DS = DbUtility.GetDS(Query, this.ConnectionString);
                response.Result = JsonConvert.SerializeObject(DS);


                //if (Data == 1)
                //{
                //    response.Result = "1";//Success
                //}
                //if (Data == -111)
                //{
                //    response.Result = "-1"; //CUSTOMER NAME ALREADY EXISTS 
                //}
                //if (Data == -222)
                //{
                //    response.Result = "-2"; //CUSTOMER CODE ALREADY EXISTS  
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
        public async Task<Payload<string>> GetEmployeeList(GetEmployeeListmodel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                    { "IsExcel", items.IsExcel },
                    { "EmpID", items.EmpID },
                    { "Rownumber", items.Rownumber },
                    { "NofRecordsPerPage", items.NofRecordsPerPage },

                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "SP_Get_EmployeeDetails", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> UpsertEmployee(UpsertEmployeeModel updateEmpInput)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                    {"@SupplierName",updateEmpInput.SupplierName },
                    {"@SupplierCode",updateEmpInput.SupplierCode },
                    {"@SupplierID",updateEmpInput.SupplierID },
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "SP_Upsert_Employeedetails", sqlParams).ConfigureAwait(false);
                if (response.Result == "[{\"N\":1}]")
                {
                    response.Result = "1";   //Success
                }
                else if (response.Result == "[{\"N\":0}]")
                {
                    response.Result = "2";   //Alredy exist 
                }
            }
            catch (SqlException Sqlex)
            {
                if (Sqlex.Message.IndexOf("Violation of UNIQUE KEY constraint 'IX_MMT_Supplier'") > -1)
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
                response.addError(ex.Message);
            }
            return response;
        }

        //16-03-23 Location Manager
#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<Payload<string>> GetLocationManager(GetLocationManagerModel items)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            Payload<string> response = new Payload<string>();
            List<Rack> rackList = new List<Rack>();
            try
            {
                string sp = "Exec sp_TPL_GetTenantLocDataForZone @ZoneID = " + items.ZoneID + " ,@TenantID=" + items.TenantID;
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                DataSet DS = new DataSet();
                DataSet dsLocations = DbUtility.GetDS(sp, this.ConnectionString);
                foreach (DataRow dr in dsLocations.Tables[0].Rows)
                {
                    // Find rack from rack list
                    Rack rack = rackList.FirstOrDefault(r => r.RackName == dr["RACK"].ToString());
                    if (rack == null)
                    {
                        // Find rack from dataset
                        rack = new Rack();
                        rack.RackName = dr["RACK"].ToString();
                        rackList.Add(rack);
                    }

                    // Find column list from Rack
                    List<Column> columnList = rack.ColumnList;
                    if (columnList == null)
                    {
                        // create new column list and add to rack
                        columnList = new List<Column>();
                        columnList.Add(new Column() { ColumnName = dr["RackColumn"].ToString() });
                        rack.ColumnList = columnList;
                    }


                    // Find column from column list
                    Column column = columnList.FirstOrDefault(a => a.ColumnName == dr["RackColumn"].ToString());
                    if (column == null)
                    {

                        // Create new column from data set
                        column = new Column();
                        column.ColumnName = dr["RackColumn"].ToString();

                        // Add column to column list
                        columnList.Add(column);
                    }


                    // Find Level List from Column
                    List<Level> levelList = column.LevelList;

                    if (levelList == null)
                    {
                        // Create new  Level List from dataset
                        levelList = new List<Level>();
                        levelList.Add(new Level() { LevelName = dr["RackLevel"].ToString() });

                        // add new level list to column
                        column.LevelList = levelList;
                    }


                    // Find level from level list
                    Level level = levelList.FirstOrDefault(a => a.LevelName == dr["RackLevel"].ToString());
                    if (level == null)
                    {
                        // Create new level from Data set
                        level = new Level();
                        level.LevelName = dr["RackLevel"].ToString();

                        //add level to level list
                        levelList.Add(level);
                    }


                    // Find bin list from level
                    List<Bin> binList = level.binList;

                    if (binList == null)
                    {
                        // Create new bin list from dataset
                        binList = new List<Bin>();
                        binList.Add(new Bin() { BinName = dr["RackLocation"].ToString(), FullLocation = dr["Location"].ToString(), bindata = dr["LocCont"].ToString(), binRepdata = dr["BinRepl"].ToString(), Tenant = dr["Tenant"].ToString(), Account = dr["Account"].ToString(), TenantID = dr["TenantID"].ToString(), LocationID = dr["LocationID"].ToString(), MCode = dr["MCode"].ToString(), IsActive = Convert.ToInt16(dr["IsActive"].ToString()), IsQuarantine = Convert.ToInt16(dr["IsQuarantine"].ToString()), IsMixedMaterialOK = Convert.ToInt16(dr["IsMixedMaterialOK"].ToString()), IsFastMoving = Convert.ToBoolean(dr["IsFastMoving"].ToString()), Suppliers = dr["Suppliers"].ToString() });

                        // add bin list to level
                        level.binList = binList;

                    }
                    else
                    {
                        // add bin to binlist
                        binList.Add(new Bin() { BinName = dr["RackLocation"].ToString(), FullLocation = dr["Location"].ToString(), bindata = dr["LocCont"].ToString(), binRepdata = dr["BinRepl"].ToString(), Tenant = dr["Tenant"].ToString(), Account = dr["Account"].ToString(), TenantID = dr["TenantID"].ToString(), LocationID = dr["LocationID"].ToString(), MCode = dr["MCode"].ToString(), IsActive = Convert.ToInt16(dr["IsActive"].ToString()), IsQuarantine = Convert.ToInt16(dr["IsQuarantine"].ToString()), IsMixedMaterialOK = Convert.ToInt16(dr["IsMixedMaterialOK"].ToString()), IsFastMoving = Convert.ToBoolean(dr["IsFastMoving"].ToString()), Suppliers = dr["Suppliers"].ToString() });
                    }


                }
                response.Result = JsonConvert.SerializeObject(rackList);
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

        public async Task<Payload<string>> UpsertLocation([FromBody] UpsertLocationModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                string locationPattern = "WH-ZONE-RACK-BIN-LEVEL-COL";
                int rackFrom = Convert.ToInt32(items.RackFrom);
                int rackTo = Convert.ToInt32(items.RackTo);
                int columnFrom = Convert.ToInt32(items.ColumnFrom);
                int columnTo = Convert.ToInt32(items.ColumnTo);
                int levelFrom = Convert.ToInt32(items.LevelFrom);
                int levelTo = Convert.ToInt32(items.LevelTo);
                int binFrom = Convert.ToInt32(items.BinFrom);
                int binTo = Convert.ToInt32(items.BinTo);
                int fastMoving = Convert.ToInt32(items.IsFastMoving);
                int tenantID = Convert.ToInt32(items.TenantID);
                string supList = items.SupList.ToString();

                string result = "";
                string rackLable = "";
                string columnLable = "";
                string levelLable = "";
                string binLable = "";
                string locationLable = "";
                string LableDisplayCode = "";

                string RackQuery = "EXEC [dbo].[USP_MST_LM_IL_GetRacList] @LocationZoneID =" + items.ZoneID;
                DataSet Rack = DbUtility.GetDS(RackQuery, this.ConnectionString);
                List<DropDownData> list1 = new List<DropDownData>();
                list1 = Rack.Tables[0].AsEnumerable().Select(s => new DropDownData() { ID = s.Field<int>("ID"), Value = s.Field<string>("Value") }).ToList();
                items.RackList = list1;

                string ColumnQuery = "EXEC [dbo].[USP_MST_LM_IL_GetColumnList] @LocationZoneID =" + items.ZoneID;
                DataSet Column = DbUtility.GetDS(ColumnQuery, this.ConnectionString);
                List<DropDownData> list2 = new List<DropDownData>();
                list2 = Column.Tables[0].AsEnumerable().Select(s => new DropDownData() { ID = s.Field<int>("ID"), Value = s.Field<string>("Value") }).ToList();
                items.ColumnList = list2;

                string LevelQuery = "EXEC [dbo].[USP_MST_LM_IL_GetLevelList]";
                DataSet Level = DbUtility.GetDS(LevelQuery, this.ConnectionString);
                List<DropDownData> list3 = new List<DropDownData>();
                list3 = Level.Tables[0].AsEnumerable().Select(s => new DropDownData() { ID = s.Field<int>("ID"), Value = s.Field<string>("Value") }).ToList();
                items.LevelList = list3;

                string BinQuery = "EXEC [dbo].[USP_MST_LM_IL_GetBinList]";
                DataSet Bin = DbUtility.GetDS(BinQuery, this.ConnectionString);
                List<DropDownData> list4 = new List<DropDownData>();
                list4 = Bin.Tables[0].AsEnumerable().Select(s => new DropDownData() { ID = s.Field<int>("ID"), Value = s.Field<string>("Value") }).ToList();
                items.BinList = list4;

                List<string> lstLocation = new List<string>();
                StringBuilder sbXML = new StringBuilder();
                sbXML.Append("<LocationHeader>");
                for (int rackindex = rackFrom; rackindex <= rackTo; rackindex++)
                {
                    for (int colIndex = columnFrom; colIndex <= columnTo; colIndex++)
                    {
                        for (int levelIndex = levelFrom; levelIndex <= levelTo; levelIndex++)
                        {
                            for (int binIndex = binFrom; binIndex <= binTo; binIndex++)
                            {
                                rackLable = items.RackList.Find(a => a.ID == rackindex).Value;
                                columnLable = items.ColumnList.Find(a => a.ID == colIndex).Value;
                                levelLable = items.LevelList.Find(a => a.ID == levelIndex).Value;
                                binLable = items.BinList.Find(a => a.ID == binIndex).Value;

                                if (locationPattern == "WH-ZONE-RACK-BIN-LEVEL-COL")
                                {
                                    locationLable = items.WhCode + "" + items.PhaseName + "" + rackLable + "" + binLable + "" + levelLable + "" + columnLable;
                                    LableDisplayCode = items.WhCode + "-" + items.PhaseName + "-" + rackLable + "-" + binLable + "-" + levelLable + "-" + columnLable;
                                }
                                else
                                {
                                    result = "-1";//"Bin Configuration not available";
                                    return response;
                                }
                                sbXML.Append("<LocationInfo>");
                                sbXML.Append("<Aisle>0</Aisle>");
                                sbXML.Append("<Rack>" + rackLable + "</Rack>");
                                sbXML.Append("<Level>" + levelLable + "</Level>");
                                sbXML.Append("<Column>" + columnLable + "</Column>");
                                sbXML.Append("<Bin>" + binLable + "</Bin>");
                                sbXML.Append("<Location>" + locationLable + "</Location>");
                                sbXML.Append("<LableDisplayCode>" + LableDisplayCode + "</LableDisplayCode>");
                                sbXML.Append("</LocationInfo>");
                            }
                        }
                    }
                }
                sbXML.Append("</LocationHeader>");

                string locationData = sbXML.ToString();
                var sqlParams = new Dictionary<string, object> {
                  {"@Locations", "'"+locationData+"'" },
                  {"@Tenant", "'"+items.TenantName+"'" },
                  {"@CreatedBy", items.UserID },
                  {"@Supplier", "'"+items.SupList+"'" },
                  {"@Zone", items.PhaseName },
                  {"@IsFastMoving", items.IsFastMoving },
                  {"@Length", items.Length },
                  {"@Width", items.Width },
                  {"@Height", items.Height },
                  {"@MaxWeight", items.Weight },
                  {"@ZoneID", items.ZoneID },
                  {"@LocationTypeID", items.LocationType },
                  {"@AccountID", items.AccountID }
                };
                result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "sp_INV_InsertLocation", sqlParams).ConfigureAwait(false);
                response.Result = "1";//Locations Created
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
        public async Task<Payload<string>> DeleteLocation(DeleteLocationModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                    {"Locations", items.Locations },
                    {"UpdatedBy", items.UpdatedBy },

                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "sp_INV_DeleteLocation", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetLoadBinDetails(GetLoadBinDetailsModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                    {"Location", items.Location },
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "sp_Inv_GetLocationDetailByLoc", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> UpDateBulkModify(UpDateBulkModifyModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {


                var sqlParams = new Dictionary<string, object> {
                    { "Location" , items.Location },
                    { "Length" , items.Length },
                    { "Width" , items.Width },
                    { "Height" , items.Height },
                    { "MaxWeight" , items.MaxWeight },
                    { "IsMixedMaterialOK" , items.IsMixedMaterialOK },
                    { "IsFastMoving" , items.IsFastMoving },
                    { "MCode" ,  (!string.IsNullOrEmpty(items.MCode) ? "'" + items.MCode + "'" : "NULL") },
                    { "IsActive" , items.IsActive },
                    { "IsQuarantine" , items.IsQuarantine },
                    { "Tenant" , items.Tenant },
                    { "Supplier" , items.Supplier },
                    { "CreatedBy" , items.CreatedBy },
                    { "ZoneID" , items.ZoneID },
                    { "LocationTypeID" , items.LocationTypeID },
                    { "AccountID" , items.AccountID }
                    };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                var Output = await DbUtility.GetjsonData(this.ConnectionString, "sp_INV_UpdateLocation", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> Modify_Locations(Modify_LocationsModel items)
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            Payload<string> response = new Payload<string>();
            try
            {
                string Data;
                if (items.LocationID == "0")
                {
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        connection.Open();

                        string query = "DECLARE @Rack NVARCHAR(2)=@RackCode, @Col NVARCHAR(2)=@Column, @Lev NVARCHAR(2)=@Level SELECT LocationID FROM INV_Location WHERE ZoneId = @ZoneID AND (@Col='0' OR Bay = @Col) AND (@Lev='0' OR Level = @Lev) AND (@Rack='0' OR Rack = @Rack) AND IsActive = 1 AND IsDeleted = 0";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@RackCode", items.RackCode);
                            command.Parameters.AddWithValue("@Column", items.Column);
                            command.Parameters.AddWithValue("@Level", items.Level);
                            command.Parameters.AddWithValue("@ZoneID", items.ZoneID);
                            Data = await DbUtility.GetJsonDataFromCommand(this.ConnectionString, command);

                        }
                    }
                    JObject data = JObject.Parse(Data);
                    JArray table = (JArray)data["Table"];
                    string LocationID = (string)table[0]["LocationID"];
                }
                string SP = "EXEC [dbo].[sp_INV_UpdateLocation] @Location=" + "'" + (items.LocationID) + "'" + ",@Length=" + items.Length + ",@Width=" + items.Width + ",@Height=" + items.Height + ",@MaxWeight=" + items.MaxWeight + ",@IsMixedMaterialOK=" + items.IsMixedMaterialOK + ",@IsFastMoving=" + items.IsFastMoving + ",@MCode=" + DBLibrary.SQuote(items.MCode) + ",@IsActive=" + items.IsActive + ",@IsQuarantine=" + items.IsQuarantine + ",@Tenant=" + DBLibrary.SQuote(items.Tenant) + ",@Supplier=" + DBLibrary.SQuote(items.Supplier) + ",@CreatedBy=" + items.UserID + ",@ZoneID=" + items.ZoneID + ",@LocationTypeID=" + items.LocationTypeID + ",@AccountID=" + items.AccountID + "";
                var result = DbUtility.GetSqlN(SP, ConnectionString);
                if (result == 0)
                {
                    response.Result = ("1"); //Successfully Updated
                }
                else
                {
                    response.Result = ("-1"); //error occured
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


        public async Task<Payload<string>> Modify_LocationPopup(Modify_LocationPopupModel items)
        {

            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                    {"@Location", items.LocationID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "sp_Inv_GetLocationDetailByLoc", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> ItemMaster_Print(ItemMaster_PrintModel obj)
        {
            Payload<string> response = new Payload<string>();
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                await Task.Run(async () =>
                {
                    string ZPL = "";
                    string Serial_Nos = "exec SP_Item_print @mcode='" + obj.MCode + "',@count=" + obj.PrintQty + "";
                    var ds = DbUtility.GetDS(Serial_Nos, this.ConnectionString);

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        int Port = 0;
                        PrintBarcode_MLabelModel Mlabel = new PrintBarcode_MLabelModel();
                        Mlabel.MCode = obj.MCode;
                        Mlabel.Description = obj.MDesc;
                        Mlabel.SerialNo = ds.Tables[0].Rows[i]["BoxSerialNo"].ToString();
                        Mlabel.BatchNo = obj.BatchNo;
                        Mlabel.MfgDate = obj.MfgDate;
                        Mlabel.ExpDate = obj.ExpDate;
                        Mlabel.ProjectNo = obj.ProjectRefNo;
                        Mlabel.SupplierLot = obj.SupplierLot;
                        Mlabel.Mrp = obj.MRP;
                        Mlabel.Grade = obj.Grade;
                        //Mlabel.HUSize = obj.HUSize;
                        //Mlabel.HUNo = obj.HUNo;
                        Mlabel.KitCode = "";
                        Mlabel.KitChildrenCount = 0;
                        Mlabel.ParentMCode = "0";
                        Mlabel.PrinterIP = "0";
                        Mlabel.IsBoxLabelReq = false;
                        Mlabel.Dpi = 203; //dpi;
                        Mlabel.PrintQty = "1";
                        Mlabel.Zone = obj.Zone;

                        string Query = "EXEC USP_MST_IM_IL_GetPrint @TenantBarcodeTypeID = " + obj.LabelID + "";
                        var DS = DbUtility.GetDS(Query, this.ConnectionString);
                        int length = Convert.ToInt32(DS.Tables[0].Rows[0]["Length"]);
                        int width = Convert.ToInt32(DS.Tables[0].Rows[0]["Width"]);
                        string LabelType = Convert.ToString(DS.Tables[0].Rows[0]["LabelType"]);

                        Mlabel.Length = length;
                        Mlabel.Width = width;
                        Mlabel.LabelType = LabelType;
                        Mlabel.WarehouseID = obj.WarehouseID;


                        ZPL += PrintBarcodeLabel(Mlabel).Result.Result;

                        if (obj.PrinterType == 1 || obj.PrinterType == 0)
                        {
                            Helper.PrintHelper.PrintUsingIP(this.ipaddress, this.port, ZPL);
                        }
                        else if (obj.PrinterType == 2)
                        {
                            string sp = "SELECT Port FROM GEN_ClientResource WHERE IsActive=1 and IsDeleted=0 and DeviceIP=@DeviceIP";
                            using (SqlConnection connection = new SqlConnection(ConnectionString))
                            {
                                connection.Open();
                                using (SqlCommand command = new SqlCommand(sp, connection))
                                {
                                    command.Parameters.AddWithValue("@DeviceIP", obj.ipaddress);
                                    Port = (int)command.ExecuteScalar();
                                }
                            }
                            Helper.PrintHelper.PrintUsingIP(obj.ipaddress, Port, ZPL);
                        }
                        else if (obj.PrinterType == 3)
                        {
                            Helper.PrintHelper.PrintUsingIP(obj.ipaddress, obj.port, ZPL);
                        }
                    }
                    response.Result = ZPL;
                    return response;
                });
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


        public static int GETDPI(string Printer, string connectionString)
        {
            int DPI = 0;
            try
            {
                if (Printer != null)
                {
                    string query = "select [Dpi] as N from GEN_ClientResource where DeviceIP='" + Printer + "'";

                    // Create an instance of SqlServerUtility
                    var sqlServerUtility = new SqlServerUtility();

                    // Call the non-static method using the instance
                    DPI = sqlServerUtility.GetSqlN(query, connectionString);
                }
            }
            catch (Exception ex)
            {
                // Handle the exception
            }
            return DPI;
        }


        public async Task<Payload<string>> SaveCustomerAddressInfo(SaveCustomerAddressInputModel obj)
        {

            Payload<string> response = new Payload<string>();
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();

                XmlSerializer serializer = new XmlSerializer(typeof(List<addressinfo>), new XmlRootAttribute("root"));
                string OutputXML = "";
                using (var stream = new StringWriter())
                {
                    serializer.Serialize(stream, obj.items);
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
                  {"@LanguageType","'"+obj.LanguageType+"'" },
                  {"@GEN_MST_Address_ID",obj.AddressID },
                  {"@Latitude", obj.Latitude},
                  {"@Longitude",obj.Longitude },
                  {"@DeliveryPoint",obj.DeliveryPoint },
                  {"@UpdatedBy",obj.CreatedBy }
                };
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "GEN_MST_SET_Addresses", sqlParams).ConfigureAwait(false);
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


            //Payload<string> response = new Payload<string>();
            //try
            //{

            //    var sqlParams = new Dictionary<string, object>{
            //    {"AddressID",obj.AddressID},
            //    {"AddressTypeID",obj.AddressTypeID},
            //    {"AddressLine1",obj.AddressLine1},
            //    {"AddressLine2",obj.AddressLine2},
            //    {"CityID",obj.CityID},
            //    {"StateID",obj.StateID},
            //    {"CountryID",obj.CountryID},
            //    {"ZipCodeID",obj.ZipCodeID},
            //    {"DeliveryPoint",obj.DeliveryPoint},
            //    {"IsActive",obj.IsActive},
            //    {"IsDeleted",obj.IsDeleted},
            //    {"CreatedBy",obj.CreatedBy},
            //    {"LanguageType",obj.LanguageType},
            //    {"Latitude",obj.Latitude},
            //    {"Longitude",obj.Longitude},
            //    {"EntityID",obj.EntityID},
            //    {"LoginAccountId",obj.LoginAccountId},
            //    {"LoginUserId",obj.LoginUserId},
            //    {"LoginTanentId",obj.LoginTanentId},
            //};

            //    DBFactory factory = new DBFactory();
            //    IDBUtility DbUtility = factory.getDBUtility();
            //    string result = await DbUtility.GetjsonData(this.ConnectionString, "GEN_MST_SET_Addresses", sqlParams).ConfigureAwait(false);
            //    Payload<string> resultPayload = new Payload<string> { Result = result };
            //    return resultPayload;
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

        }


        public async Task<Payload<string>> PrintBarcodeLabel(PrintBarcode_MLabelModel Mlabel)
        {
            Payload<string> response = new Payload<string>();
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                ZPL zplString = new ZPL();
                string barcodestring = null;

                //Mlabel.HUSize = Mlabel.HUSize == "" || Mlabel.HUSize == "0" ? "1" : Mlabel.HUSize;
                //Mlabel.HUNo = Mlabel.HUNo == "" || Mlabel.HUNo == "0" ? "1" : Mlabel.HUNo;

                Mlabel.HUSize = Mlabel.HUSize == "" || Mlabel.HUSize == null || Mlabel.HUSize == "0" ? "1" : Mlabel.HUSize;
                Mlabel.HUNo = Mlabel.HUNo == "" || Mlabel.HUNo == null || Mlabel.HUNo == "0" ? "1" : Mlabel.HUNo;

                barcodestring = Mlabel.MCode + "|" + Mlabel.BatchNo + "|" + Mlabel.SerialNo + "|" + String.Format("{0:dd-MM-yy}", Mlabel.MfgDate) + "|" + String.Format("{0:dd-MM-yy}", Mlabel.ExpDate) + "|" + Mlabel.ProjectNo + "|" + Mlabel.KitCode.ToString() + "|" + Mlabel.Mrp + "|" + Mlabel.Lineno + "|" + Mlabel.HUNo + "|" + Mlabel.HUSize;

                //DateTime currentDate = DateTime.Now;
                //string formattedDate = currentDate.ToString("dd-MM-yyyy");

                //Mlabel.MfgDate = formattedDate;


                string sp = "Exec Get_Zonebymcode_print @Mcode = '" + Mlabel.MCode + "', @WarehouseID ="+Mlabel.WarehouseID+"";
                var DS = DbUtility.GetDS(sp, ConnectionString);

                if (DS.Tables[0].Rows.Count > 0)
                {
                    Mlabel.Zone = DS.Tables[0].Rows[0]["Zone"].ToString();
                }



                var query = "EXEC [dbo].[sp_GetZPLString] @Dpi=" + Mlabel.Dpi + " , @Length=" + Mlabel.Length + ", @Width = " + Mlabel.Width + ", @LabelType = " + "'" + Mlabel.LabelType + "'";
                string result = DbUtility.GetSqlS(query, ConnectionString);
                if (result != "" && result != null)
                {
                    result = result.Replace("barcodegeneratorcodewithmfgandexp", barcodestring);
                    result = result.Replace("@SKU", "" + " " + Mlabel.MCode);
                    result = result.Replace("@Desc.", "" + " " + Mlabel.Description);

                    if (Mlabel.BatchNo != "")
                    {
                        result = result.Replace("@BatchNo", "" + " " + (Mlabel.BatchNo?.ToString() ?? "@BatchNo"));
                    }
                    else
                    {
                        result = result.Replace("@BatchNo", " @BatchNo" + "" + "");
                    }

                    if (Mlabel.MfgDate.ToString() != "")
                    {

                        result = result.Replace("@Mfg.Date", "" + " " + String.Format("{0:dd-MM-yy}", Mlabel.MfgDate));
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
                        result = result.Replace("@Project Ref No.", "Project Ref No.:" + "" + Mlabel.ProjectNo);
                    }
                    else
                    {
                        result = result.Replace("@Project Ref No.", "Project Ref No.:" + "" + "");
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
                        result = result.Replace("@MRP", "MRP : " + "" + Mlabel.Mrp + " /-");
                    }
                    else
                    {
                        result = result.Replace("@MRP", "MRP :" + "" + "");
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





        //public async Task<string> GetItemMasterPrint(PrintBarcode_MLabelModel obj)
        //{

        //    string ZPL = "";
        //    await Task.Run(() =>
        //    {
        //        //TracklineMLabel Mlabel = new TracklineMLabel();
        //        Mlabel.MCode = obj.MCode;
        //        Mlabel.Description = obj.MDesc;
        //        Mlabel.SerialNo = obj.SerialNo;
        //        Mlabel.BatchNo = obj.BatchNo;
        //        Mlabel.MfgDate = obj.MfgDate;
        //        Mlabel.ExpDate = obj.ExpDate;
        //        Mlabel.ProjectNo = obj.ProjectRefNo;
        //        Mlabel.Mrp = obj.MRP;
        //        Mlabel.HUSize = obj.HUSize;
        //        Mlabel.HUNo = obj.HUNo;
        //        string length = "";
        //        string width = "";
        //        string LabelType = "";
        //        string query = "select * from TPL_Tenant_BarcodeType where IsActive=1 and IsDeleted=0 and TenantBarcodeTypeID=" + obj.LabelID;
        //        DataSet DS = SqlServerUtility.GetDS(query, this.ConnectionString);
        //        foreach (DataRow row in DS.Tables[0].Rows)
        //        {
        //            length = row["Length"].ToString();
        //            width = row["Width"].ToString();
        //            LabelType = row["LabelType"].ToString();
        //        }
        //        Mlabel.KitCode = "";
        //        Mlabel.KitChildrenCount = DBLibrary.PrintCommon.CommonPrint.GetChildrenCount(Mlabel.KitPlannerID.ToString(), this.ConnectionString);
        //        Mlabel.ParentMCode = DBLibrary.PrintCommon.CommonPrint.GetKitParentCode(Mlabel.KitPlannerID.ToString(), this.ConnectionString);
        //        Mlabel.PrinterIP = "0";
        //        Mlabel.IsBoxLabelReq = false;
        //        Mlabel.Length = length;
        //        Mlabel.Width = width;
        //        int dpi = 0;
        //        dpi = DBLibrary.PrintCommon.CommonPrint.GETDPI(Mlabel.PrinterIP, this.ConnectionString);
        //        Mlabel.Dpi = 203; //dpi;
        //        Mlabel.PrintQty = obj.PrintQty;
        //        Mlabel.LabelType = LabelType;
        //        DBLibrary.PrintCommon.CommonPrint print = new DBLibrary.PrintCommon.CommonPrint();
        //        ZPL = print.PrintBarcodeLabel(Mlabel, this.ConnectionString, Convert.ToInt32(obj.LabelID));
        //    });
        //    return ZPL;
        //}


        //        public async Task<Payload<string>> LocationManager_LabelPrint(LocationManager_LabelPrintModel obj)
        //#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        //        {
        //            Payload<string> response = new Payload<string>();
        //            DBFactory factory = new DBFactory();
        //            IDBUtility DbUtility = factory.getDBUtility();
        //            try
        //            {
        //                int Port = 0;
        //                string ZPL = "";
        //                string[] locationsplit = obj.Location.Split(',');
        //                string zplData = DbUtility.GetSqlS("SELECT ZPLScript AS S FROM TPL_Tenant_BarcodeType WHERE Isactive=1 AND IsDeleted=0 AND LabelType='Location'", ConnectionString);

        //                for (int i = 0; i < locationsplit.Length; i++)
        //                {
        //                    if (locationsplit[i] != "")
        //                    {
        //                        ZPL += zplData.Replace("LocationCode", locationsplit[i]);
        //                    }
        //                }

        //                string sp = "SELECT Port FROM GEN_ClientResource WHERE IsActive=1 and IsDeleted=0 and DeviceIP=@DeviceIP";
        //                using (SqlConnection connection = new SqlConnection(ConnectionString))
        //                {
        //                    connection.Open();
        //                    using (SqlCommand command = new SqlCommand(sp, connection))
        //                    {
        //                        command.Parameters.AddWithValue("@DeviceIP", obj.ipaddress);
        //                        Port = (int)command.ExecuteScalar();
        //                    }
        //                }

        //                Helper.PrintHelper.PrintUsingIP(obj.ipaddress, Port, ZPL);
        //                response.Result = ZPL;
        //            }
        //            catch (SqlException Sqlex)
        //            {
        //                response.addError(Sqlex.Message);
        //            }
        //            catch (Exception ex)
        //            {
        //                response.addError(ex.Message);
        //            }
        //            return response;
        //        }




#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        //public async Task<Payload<string>> LocationManager_LabelPrint(LocationManager_LabelPrintModel obj)
        //{
        //    Payload<string> response = new Payload<string>();
        //    DBFactory factory = new DBFactory();
        //    IDBUtility DbUtility = factory.getDBUtility();
        //    try
        //    {
        //        int Port = 0;
        //        string ZPL = "";
        //        string[] locationsplit = obj.Location.Split(',');
        //        string zplData = DbUtility.GetSqlS("SELECT ZPLScript AS S FROM TPL_Tenant_BarcodeType WHERE Isactive=1 AND IsDeleted=0 AND TenantBarcodeTypeID='" + obj.LabelTypeID+"'", ConnectionString);

        //        for (int i = 0; i < locationsplit.Length; i++)
        //        {
        //            if (locationsplit[i] != "")
        //            {
        //                ZPL += zplData.Replace("LocationCode", locationsplit[i]);
        //            }
        //        }

        //        if (obj.PrinterType == 1 || obj.PrinterType == 0)
        //        {
        //            Helper.PrintHelper.PrintUsingIP(this.ipaddress, this.port, ZPL);
        //        }
        //        else if (obj.PrinterType == 2)
        //        {

        //            string sp = "SELECT Port FROM GEN_ClientResource WHERE IsActive=1 and IsDeleted=0 and DeviceIP=@DeviceIP";
        //            using (SqlConnection connection = new SqlConnection(ConnectionString))
        //            {
        //                connection.Open();
        //                using (SqlCommand command = new SqlCommand(sp, connection))
        //                {
        //                    command.Parameters.AddWithValue("@DeviceIP", obj.ipaddress);
        //                    Port = (int)command.ExecuteScalar();
        //                }
        //                Helper.PrintHelper.PrintUsingIP(obj.ipaddress, Port, ZPL);
        //            }
        //        }

        //        else if (obj.PrinterType == 3)
        //        {
        //            Helper.PrintHelper.PrintUsingIP(obj.ipaddress, obj.port, ZPL);
        //        }

        //        response.Result = ZPL;
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

        public async Task<Payload<string>> LocationManager_LabelPrint(LocationManager_LabelPrintModel obj)
        {
            Payload<string> response = new Payload<string>();
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();

            try
            {
                int Port = 0;
                StringBuilder finalZPL = new StringBuilder();

                string[] locations = obj.Location.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                if (locations.Length == 0)
                {
                    response.addError("No locations provided.");
                    return response;
                }

                string zplTemplate = DbUtility.GetSqlS("SELECT ZPLScript AS S FROM TPL_Tenant_BarcodeType WHERE Isactive=1 AND IsDeleted=0 AND TenantBarcodeTypeID='" + obj.LabelTypeID + "'", ConnectionString);

                if (string.IsNullOrWhiteSpace(zplTemplate))
                {
                    response.addError("ZPL Template not found.");
                    return response;
                }

                for (int i = 0; i < locations.Length; i += 2)
                {
                    string leftLoc = locations[i];
                    string rightLoc = (i + 1 < locations.Length) ? locations[i + 1] : "";

                    string labelZPL = zplTemplate
                        .Replace("LeftLocation", leftLoc)
                        .Replace("RightLocation", rightLoc);

                    finalZPL.Append(labelZPL);
                }

                string ZPL = finalZPL.ToString();

                if (obj.PrinterType == 1 || obj.PrinterType == 0)
                {
                    Helper.PrintHelper.PrintUsingIP(this.ipaddress, this.port, ZPL);
                }
                else if (obj.PrinterType == 2)
                {
                    string sp = "SELECT Port FROM GEN_ClientResource WHERE IsActive=1 and IsDeleted=0 and DeviceIP=@DeviceIP";
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand(sp, connection))
                        {
                            command.Parameters.AddWithValue("@DeviceIP", obj.ipaddress);
                            Port = (int)command.ExecuteScalar();
                        }
                        Helper.PrintHelper.PrintUsingIP(obj.ipaddress, Port, ZPL);
                    }
                }
                else if (obj.PrinterType == 3)
                {
                    Helper.PrintHelper.PrintUsingIP(obj.ipaddress, obj.port, ZPL);
                }

                response.Result = ZPL;
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
        //        public async Task<Payload<string>> LocationManager_Bulk_LabelPrint(LocationManager_Bulk_LabelPrintModel obj)
        //#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        //        {
        //            Payload<string> response = new Payload<string>();
        //            DBFactory factory = new DBFactory();
        //            IDBUtility DbUtility = factory.getDBUtility();
        //            int Port = 0;
        //            try
        //            {
        //                int ZoneID = obj.ZoneID;      
        //                string Zone = obj.Zone;                        
        //                string Column = obj.Column;
        //                string Bin = obj.Bin;

        //                var RackCode = obj.Rack;
        //                var ColCode = Convert.ToInt32(Column) <= 9 ? "0" + Column.ToString() : Column.ToString();
        //                var levCode = obj.Level;
        //                var BinCode = Convert.ToInt32(Bin) <= 9 ? "0" + Bin.ToString() : Bin.ToString();

        //                StringBuilder sb = new StringBuilder();
        //                sb.Append(" DECLARE @Rack NVARCHAR(2) = '" + RackCode + "', @Col NVARCHAR(10)= '" + ColCode + "', @Lev NVARCHAR(1)= '" + levCode + "', @Bin NVARCHAR(2)= '" + BinCode + "' ");
        //                sb.Append(" SELECT Location FROM INV_Location WHERE ZoneID = '" + ZoneID + "' AND Rack = @Rack AND (@Col = '00' OR Bay = CONVERT(INT,@Col)) AND (@Lev = '0' OR[Level] = CONVERT(INT,@Lev)) AND (@Bin = '00' OR RackLocation = CONVERT(INT,@Bin)) ");
        //                sb.Append("  AND IsActive = 1 AND IsDeleted = 0 ");
        //                string LocQuery = sb.ToString();
        //                var DS = DbUtility.GetDS(LocQuery , this.ConnectionString);

        //                StringBuilder locationdata = new StringBuilder();
        //                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
        //                {
        //                    locationdata.Append(DS.Tables[0].Rows[i][0].ToString() + ",");
        //                }
        //                string sLocationstring = locationdata.ToString();
        //                sLocationstring = sLocationstring.Remove(sLocationstring.Length - 1);
        //                string[] locationsplit = sLocationstring.Split(',');

        //                string ZPL = "";
        //                string zplData = DbUtility.GetSqlS("SELECT ZPLScript AS S FROM TPL_Tenant_BarcodeType WHERE Isactive=1 AND IsDeleted=0 AND LabelType='Location'", ConnectionString);

        //                for (int i = 0; i < locationsplit.Length; i++)
        //                {
        //                    if (locationsplit[i] != "")
        //                    {
        //                        ZPL += zplData.Replace("LocationCode", locationsplit[i]);
        //                    }
        //                }

        //                string sp = "SELECT Port FROM GEN_ClientResource WHERE IsActive=1 and IsDeleted=0 and DeviceIP=@DeviceIP";
        //                using (SqlConnection connection = new SqlConnection(ConnectionString))
        //                {
        //                    connection.Open();
        //                    using (SqlCommand command = new SqlCommand(sp, connection))
        //                    {
        //                        command.Parameters.AddWithValue("@DeviceIP", obj.ipaddress);
        //                        Port = (int)command.ExecuteScalar();
        //                    }
        //                }

        //                Helper.PrintHelper.PrintUsingIP(obj.ipaddress, Port , ZPL);
        //                response.Result = ZPL;
        //            }
        //            catch (SqlException Sqlex)
        //            {
        //                response.addError(Sqlex.Message);
        //            }
        //            catch (Exception ex)
        //            {
        //                response.addError(ex.Message);
        //            }
        //            return response;
        //        }


        //        public async Task<Payload<string>> LocationManager_Bulk_LabelPrint(LocationManager_Bulk_LabelPrintModel obj)
        //#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        //        {
        //            Payload<string> response = new Payload<string>();
        //            DBFactory factory = new DBFactory();
        //            IDBUtility DbUtility = factory.getDBUtility();
        //            int Port = 0;
        //            try
        //            {
        //                int ZoneID = obj.ZoneID;
        //                string Zone = obj.Zone;
        //                string Column = obj.Column;
        //                string Bin = obj.Bin;

        //                var RackCode = obj.Rack;
        //                var ColCode = obj.Column;
        //                var levCode = obj.Level;
        //                var BinCode = obj.Bin;
        //                //var ColCode = Convert.ToInt32(Column) <= 9 ? "0" + Column.ToString() : Column.ToString();

        //                //var BinCode = Convert.ToInt32(Bin) <= 9 ? "0" + Bin.ToString() : Bin.ToString();

        //                StringBuilder sb = new StringBuilder();
        //                //sb.Append(" DECLARE @Rack NVARCHAR(2) = '" + RackCode + "', @Col NVARCHAR(10)= '" + ColCode + "', @Lev NVARCHAR(1)= '" + levCode + "', @Bin NVARCHAR(2)= '" + BinCode + "' ");
        //                //sb.Append(" SELECT Location FROM INV_Location WHERE ZoneID = '" + ZoneID + "' AND Rack = @Rack AND (@Col = '00' OR Bay = CONVERT(INT,@Col)) AND (@Lev = '0' OR[Level] = CONVERT(INT,@Lev)) AND (@Bin = '00' OR RackLocation = CONVERT(INT,@Bin)) ");
        //                //sb.Append("  AND IsActive = 1 AND IsDeleted = 0 ");
        //                sb.Append(" DECLARE @Rack NVARCHAR(2) = '" + RackCode + "', @Col NVARCHAR(10)= '" + ColCode + "', @Lev NVARCHAR(1)= '" + levCode + "', @Bin NVARCHAR(2)= '" + BinCode + "' ");
        //                sb.Append(" SELECT Location FROM INV_Location WHERE ZoneID = '" + ZoneID + "' AND Rack = @Rack AND ((Bay = @Col or Bay = '' OR @Col = '') AND(Level = '' or Level = @Lev OR @Lev = '') AND(RackLocation = '' or RackLocation = @bin OR @bin = ''))");
        //                sb.Append("  AND IsActive = 1 AND IsDeleted = 0 ");
        //                string LocQuery = sb.ToString();
        //                var DS = DbUtility.GetDS(LocQuery, this.ConnectionString);

        //                StringBuilder locationdata = new StringBuilder();
        //                for (int i = 0; i < DS.Tables[0].Rows.Count; i++)
        //                {
        //                    locationdata.Append(DS.Tables[0].Rows[i][0].ToString() + ",");
        //                }
        //                string sLocationstring = locationdata.ToString();
        //                sLocationstring = sLocationstring.Remove(sLocationstring.Length - 1);
        //                string[] locationsplit = sLocationstring.Split(',');

        //                string ZPL = "";
        //                string zplData = DbUtility.GetSqlS("SELECT ZPLScript AS S FROM TPL_Tenant_BarcodeType WHERE Isactive=1 AND IsDeleted=0 AND TenantBarcodeTypeID='" + obj.LabelTypeID + "'", ConnectionString);

        //                for (int i = 0; i < locationsplit.Length; i++)
        //                {
        //                    if (locationsplit[i] != "")
        //                    {
        //                        ZPL += zplData.Replace("LocationCode", locationsplit[i]);
        //                    }
        //                }

        //                if (obj.PrinterType == 1 || obj.PrinterType == 0)
        //                {
        //                    Helper.PrintHelper.PrintUsingIP(this.ipaddress, this.port, ZPL);
        //                }
        //                else if (obj.PrinterType == 2)
        //                {

        //                    string sp = "SELECT Port FROM GEN_ClientResource WHERE IsActive=1 and IsDeleted=0 and DeviceIP=@DeviceIP";
        //                    using (SqlConnection connection = new SqlConnection(ConnectionString))
        //                    {
        //                        connection.Open();
        //                        using (SqlCommand command = new SqlCommand(sp, connection))
        //                        {
        //                            command.Parameters.AddWithValue("@DeviceIP", obj.ipaddress);
        //                            Port = (int)command.ExecuteScalar();
        //                        }
        //                        Helper.PrintHelper.PrintUsingIP(obj.ipaddress, Port, ZPL);
        //                    }
        //                }

        //                else if (obj.PrinterType == 3)
        //                {
        //                    Helper.PrintHelper.PrintUsingIP(obj.ipaddress, obj.port, ZPL);
        //                }

        //                response.Result = ZPL;
        //            }
        //            catch (SqlException Sqlex)
        //            {
        //                response.addError(Sqlex.Message);
        //            }
        //            catch (Exception ex)
        //            {
        //                response.addError(ex.Message);
        //            }
        //            return response;
        //        }

        public async Task<Payload<string>> LocationManager_Bulk_LabelPrint(LocationManager_Bulk_LabelPrintModel obj)
        {
            Payload<string> response = new Payload<string>();
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            int Port = 0;

            try
            {
                int ZoneID = obj.ZoneID;
                string RackCode = obj.Rack;
                string ColCode = obj.Column;
                string levCode = obj.Level;
                string BinCode = obj.Bin;

                StringBuilder sb = new StringBuilder();
                sb.Append("DECLARE @Rack NVARCHAR(2) = '" + RackCode + "', @Col NVARCHAR(10)= '" + ColCode + "', @Lev NVARCHAR(1)= '" + levCode + "', @Bin NVARCHAR(2)= '" + BinCode + "' ");
                sb.Append("SELECT Location FROM INV_Location WHERE ZoneID = '" + ZoneID + "' AND Rack = @Rack AND ((Bay = @Col or Bay = '' OR @Col = '') AND (Level = '' or Level = @Lev OR @Lev = '') AND (RackLocation = '' or RackLocation = @Bin OR @Bin = '')) ");
                sb.Append("AND IsActive = 1 AND IsDeleted = 0 ");

                string LocQuery = sb.ToString();
                var DS = DbUtility.GetDS(LocQuery, this.ConnectionString);

                if (DS.Tables.Count == 0 || DS.Tables[0].Rows.Count == 0)
                {
                    response.addError("No locations found.");
                    return response;
                }

                List<string> locations = new List<string>();
                foreach (DataRow row in DS.Tables[0].Rows)
                {
                    locations.Add(row["Location"].ToString());
                }

                string zplTemplate = DbUtility.GetSqlS("SELECT ZPLScript AS S FROM TPL_Tenant_BarcodeType WHERE Isactive=1 AND IsDeleted=0 AND TenantBarcodeTypeID='" + obj.LabelTypeID + "'", ConnectionString);
                if (string.IsNullOrWhiteSpace(zplTemplate))
                {
                    response.addError("ZPL Template not found for the selected label type.");
                    return response;
                }

                StringBuilder finalZPL = new StringBuilder();

                for (int i = 0; i < locations.Count; i += 2)
                {
                    string leftLoc = locations[i];
                    string rightLoc = (i + 1 < locations.Count) ? locations[i + 1] : "";

                    string currentZPL = zplTemplate
                        .Replace("LeftLocation", leftLoc)
                        .Replace("RightLocation", rightLoc);

                    finalZPL.Append(currentZPL);
                }

                string ZPL = finalZPL.ToString();

                if (obj.PrinterType == 1 || obj.PrinterType == 0)
                {
                    Helper.PrintHelper.PrintUsingIP(this.ipaddress, this.port, ZPL);
                }
                else if (obj.PrinterType == 2)
                {
                    string sp = "SELECT Port FROM GEN_ClientResource WHERE IsActive=1 and IsDeleted=0 and DeviceIP=@DeviceIP";
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand(sp, connection))
                        {
                            command.Parameters.AddWithValue("@DeviceIP", obj.ipaddress);
                            Port = (int)command.ExecuteScalar();
                        }
                        Helper.PrintHelper.PrintUsingIP(obj.ipaddress, Port, ZPL);
                    }
                }
                else if (obj.PrinterType == 3)
                {
                    Helper.PrintHelper.PrintUsingIP(obj.ipaddress, obj.port, ZPL);
                }

                response.Result = ZPL;
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




        public async Task<Payload<string>> ImportSupplierData(SaveSupplierDetailsInputModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                DataTable dt = JsonConvert.DeserializeObject<DataTable>(objE.Decryptword(obj.DataJson));
                //var sqlParams = new Dictionary<string, object>
                // {
                //     {"@SupplierExcelType", "'"+objE.Decryptword(obj.DataJson)+"'" },
                //     {"@UserID", obj.UpdatedBy},
                //     {"@LoginAccountId", obj.LoginAccountId}

                // };

                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                string SP = "EXEC [dbo].[sp_MMT_importSupplier] @SupplierExcelType=" + "'" + objE.Decryptword(obj.DataJson) + "'" + ",@LoginAccountId=" + obj.LoginAccountId + ",@UserID=" + obj.UpdatedBy + "";
                var result = DbUtility.GetSqlN(SP, ConnectionString);
                response.Result = result.ToString();

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

        public async Task<Payload<string>> GetTenantList(TenantListInputModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
               {"@orderby",obj.orderby},
                {"@orderbyText",obj.orderbyText},
                {"@searchData",obj.searchData},
                {"@TenantID",obj.TenantID},
                {"@AccountID_New",obj.AccountID_New},
                {"@UserTypeID_New",obj.UserTypeID_New},
                {"@TenantID_New",obj.TenantID_New},
                {"@UserID_New",obj.UserID_New}
            };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "sp_TPL_GetTenantRegistrationDetails", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetBarcodeLabelData(BarcodeInputModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>{
                {"@TenantID",obj.TenantID},
                {"@BarcodeTypeID",obj.BarcodeTypeID},
                {"@Length",obj.Length},
                {"@Width",obj.Width},
                {"@LableType",obj.LableType},
                {"@DPI",obj.DPI},
                {"@ZPLScript",obj.ZPLScript},
                {"@CreatedBy",obj.CreatedBy},
                {"@TenantBarcodeTypeID",obj.TenantBarcodeTypeID },
                {"@ActionType",obj.ActionType},
                {"@PrintDesc",obj.PrintDesc }
            };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_GetBarcodeLabelData", sqlParams, true).ConfigureAwait(false);
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


        public async Task<Payload<string>> GetTenantContractData(TenantListInputModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>{

                {"@TenantID",obj.TenantID},
                //{"LoginAccountId",obj.LoginAccountId},
                //{"LoginUserId",obj.LoginUserId},
                //{"LoginTanentId",obj.LoginTanentId}
            };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "sp_TPL_GetTenantContract", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> SaveUpdateTenantData(SaveTenantInputModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>{
                {"@AccountID",obj.AccountID},
                {"@TenantID",obj.TenantID},
                {"@TenantName",obj.TenantName},
                {"@CompanyDBA",obj.TenantCode},
                {"@BusinessTypeID",obj.BusinessTypeID},
                {"@TenantRegistrationNo",obj.TenantRegistrationNo},
                {"@PCPFirstName",obj.PCPFirstName},
                {"@PCPLastName",obj.PCPLastName},
                {"@PCPContactNo",obj.PCPContactNo},
                {"@PCPEmail",obj.PCPEmail},
                {"@Website",obj.Website},
                {"@IsInsurance",obj.IsInsurance},
                {"@CreatedBy",obj.CreatedBy},
                // {"AddressBookTypeID_CI",obj.AddressBookTypeID_CI},
                {"@Address1_CI",obj.Address1_CI},
                {"@Address2_CI",obj.Address2_CI},
                {"@City_CI",obj.City_CI},
                {"@State_CI",obj.State_CI},
                {"@ZIP_CI",obj.ZIP_CI},
                {"@CountryMasterID_CI",obj.CountryMasterID_CI},
                {"@CurrencyID_CI",obj.CurrencyID_CI},
                {"@Phone1_CI",obj.Phone1_CI},
                {"@Phone2_CI",obj.Phone2_CI},
                {"@Mobile_CI",obj.Mobile_CI},
                {"@Fax_CI",obj.Fax_CI},
                {"@EMail_CI",obj.EMail_CI},
                {"@IsTaxApplicable",obj.IsTaxApplicable},
                {"@GSTNumber",obj.GSTNumber},
                {"@Latitude_CI",obj.Latitude_CI},
                {"@Longitude_CI",obj.Longitude_CI},
                {"@ZipCodeId_CI",obj.ZipCodeId_CI},
                {"@IsSameAddress_CI",obj.IsSameAddress_CI},
                // {"AddressBookTypeID_BI",obj.AddressBookTypeID_BI},
                {"@Address1_BI",obj.Address1_BI},
                {"@Address2_BI",obj.Address2_BI},
                {"@City_BI",obj.City_BI},
                {"@State_BI",obj.State_BI},
                {"@ZIP_BI",obj.ZIP_BI},
                {"@CountryMasterID_BI",obj.CountryMasterID_BI},
                {"@CurrencyID_BI",obj.CurrencyID_BI},
                {"@Phone1_BI",obj.Phone1_BI},
                {"@Phone2_BI",obj.Phone2_BI},
                {"@Mobile_BI",obj.Mobile_BI},
                {"@Fax_BI",obj.Fax_BI},
                {"@EMail_BI",obj.EMail_BI},
                {"@Latitude_BI",obj.Latitude_BI},
                {"@Longitude_BI",obj.Longitude_BI},
                {"@ZipCodeId_BI",obj.ZipCodeId_BI},
                {"@IsSameAddress_BI",obj.IsSameAddress_BI},
                {"@BillTypeID",obj.BillTypeID},
                {"@Invoice",obj.Invoice},
                {"@TenantActivityRateID",obj.TenantActivityRateID},
                //{"@InvoiceFrom",obj.InvoiceFrom},
                //{"@InvoiceTo",obj.InvoiceTo},
                {"@Active",obj.Active},
                //{"@LoginAccountId",obj.LoginAccountId},
                //{"@LoginUserId",obj.LoginUserId},
                //{"@LoginTanentId",obj.LoginTanentId},
             };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "sp_TPL_UpsertTenantRegistration", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> SaveTenantContractData(TenantContractInputModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                //obj.Type = "0";
                //string res = string.Empty;
                //string ResourcePath = string.Empty;
                //if (obj.bitecode != "")
                //{
                //    if (Convert.ToString(obj.fileName) != "")
                //    {
                //        string ImgName = obj.fileName;
                //        var name = ImgName.Split('.');

                //        String filename = name[0];
                //        String fileext = name[1];
                //        if (fileext == "jpg" || fileext == "jpeg" || fileext == "png")
                //        {
                //            obj.Type = "1";

                //            byte[] b = Convert.FromBase64String(obj.bitecode);
                //            using (MemoryStream ms = new MemoryStream(b))
                //            {
                //                using (Image pic = Image.Load(ms))
                //                {
                //                    pic.Save(AppSettings.DocumentsPath + obj.fileName);
                //                }
                //            }
                //        }
                //        else
                //        {
                //            if (fileext == "pdf")
                //            {
                //                obj.Type = "2";
                //            }
                //            else if (fileext == "xlxs" || fileext == "xls")
                //            {
                //                obj.Type = "3";
                //            }
                //            else if (fileext == "doc" || fileext == "docx")
                //            {
                //                obj.Type = "4";
                //            }
                //            else if (fileext == "txt")
                //            {
                //                obj.Type = "5";
                //            }
                //            byte[] rebin = Convert.FromBase64String(obj.bitecode);
                //            using (FileStream fs2 = new FileStream(AppSettings.DocumentsPath + obj.fileName + obj.Extention, FileMode.Create, FileAccess.ReadWrite))
                //            using (BinaryWriter bw = new BinaryWriter(fs2))
                //                bw.Write(rebin);
                //        }
                //        //res = await objAWS.UploadFilesToAWS(AppSettings.DocumentsPath, obj.fileName, obj.AppSSOAccountID + "/MasterData/Tenant/" + obj.fileName, AppSettings.aws_access_key_id, AppSettings.aws_secret_access_key);
                //    }
                //}
                //if (obj.fileName != "")
                //{
                //    ResourcePath = obj.AppSSOAccountID + "/MasterData/Tenant/" + obj.fileName;
                //}
                //else
                //{
                //    ResourcePath = obj.fileName;
                //}
                var sqlParams1 = new Dictionary<string, object>{
                      {"@TenantContractID",obj.TenantContractID},
                      {"@TenantContract",obj.TenantContract},
                      {"@EffectiveFrom", obj.EffectiveFrom.ToString("yyyy/MM/dd")},
                      {"@EffectiveTo",obj.EffectiveTo.ToString("yyyy/MM/dd")},
                      {"@TenantID",obj.TenantID},
                      {"@Remarks",obj.Remarks},
                      {"@WarehouseID",obj.WarehouseID},
                      {"@SpaceRental",obj.SpaceRental},
                      {"@IsActive",obj.IsActive},
                      {"@SquareUnits",obj.SquareUnits},
                      {"@CreatedBy",obj.CreatedBy}
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "sp_TPL_UpsertTenantContract", sqlParams1).ConfigureAwait(false);
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

        public async Task<Payload<string>> DeleteTenantContractData(TenantContractInputModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>{
                {"@TenantContractIDs",obj.TenantContractIDs},
                 {"@LoginAccountId",obj.LoginAccountId},
                {"@LoginTanentId",obj.LoginTanentId},
                {"@LoginUserId",obj.LoginUserId},
            };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "sp_TPL_DeleteTenantContract", sqlParams).ConfigureAwait(false);
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
