using Simpolo_Endpoint.DAO.interfaces;
using Simpolo_Endpoint.DBUtil;
using Simpolo_Endpoint.Models;
using Simpolo_Endpoint.ModelsLibrary;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using static Simpolo_Endpoint.DBUtil.DBLibrary;

namespace Simpolo_Endpoint.DAO.Services
{
    public class CommonService : AppDBService, ICommon
    {
        public CommonService(IOptions<AppSettings> appSettings) : base(appSettings)
        {

        }
        public async Task<Payload<string>> GetUserDropdown(GetUserDropdownModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "AccountID" , obj.AccountID },
                    { "UserIDNew" , obj.UserIDNew },
                    { "TenantId" , obj.TenantId },
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
        public async Task<Payload<string>> GetUserTypes()
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> { };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_MST_GetUserRoleType", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetTenantDropdown_Account_Tenant(GetTenantDropdown_Account_TenantModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "prefix" , obj.prefix },
                    { "AccountID" , obj.AccountID },
                    { "TenantID" , obj.TenantID },
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_MST_DropTenantAccountWise", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetTenantDropdown_Account(GetTenantDropdown_AccountModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "@Flag" , obj.Flag },
                    { "@AccountID" , obj.AccountID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_MST_DropTenantAccountWise", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetAddressTypes(SaveCustomerDetailsInputModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>{
                {"@CustomerID",obj.CustomerID },
            };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_MST_GetAddressTypeDrop", sqlParams, true).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetUserRoles(GetUserRolesModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "UserTypeID" , obj.UserTypeID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_MST_GetUserRoleByType", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetWHDropdown_Account(GetWHDropdown_AccountModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "AccountID" , obj.AccountID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_ACC_IL_GetWareHouseData", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetWHDropdown_User(GetWHDropdown_UserModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "USERID" , obj.USERID },
                    { "PRIFIX" , obj.PRIFIX }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "SP_RPT_WHDROPDOWN", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetLocDropdown(GetLocDropdown_Model obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "USERID" , obj.USERID },
                    { "PREFIX" , obj.PREFIX },
                    { "AccountID" , obj.AccountID}
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "Get_LocationForReplenishment", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetTenantDropdown_Account_User_Tenant(GetTenantDropdown_Account_User_TenantModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {

                //var sqlParams = new Dictionary<string, object>  {
                //    { "@prefix" , "'" + obj.prefix + "'" },
                //    { "@AccountID" , obj.AccountID },
                //    { "@USERID" , obj.USERID },
                //    { "@TenantID" , obj.TenantID }
                //};
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                string Query = "EXEC [dbo].[USP_LoadTenantDataByUserWH] @Prefix=" + "'" + obj.prefix + "'" + " , @TenantID=" + obj.TenantID + ", @AccountID=" + obj.AccountID + ", @USERID="+ obj.USERID + "";
                var DS = DbUtility.GetDS(Query, this.ConnectionString);
                response.Result = JsonConvert.SerializeObject(DS.Tables[0]);
              
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
        


        public async Task<Payload<string>> GetSeriesType(GetSeriesTypeModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "prefix" , obj.prefix }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_Service_INL_GetSeriesType", sqlParams).ConfigureAwait(false);
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



        public async Task<Payload<string>> GetContainerType(GetContainerTypeModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "prefix" , obj.prefix }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_Service_INL_GetContainerType", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetDockTypes()
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> { };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_LoadDockType", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetCountries()
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> { };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_LoadCountryDropDown", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetCurrency(GetCurrencyModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "CountryID" , obj.CountryID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_MST_GetCurrencyByCountry", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetStates(GetStatesModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "Flag" , obj.Flag },
                    { "CountryID" , obj.CountryID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_MST_GetStateByCountry", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetCities(GetCitiesModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "Flag" , obj.Flag },
                    { "StateID" , obj.StateID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_MST_GetCityDrop", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetZipCodes(GetZipCodesModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "CityID" , obj.CityID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_MST_GetZipCodes", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetTimePreferences()
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> { };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_MST_GetPreferenceOptionDrop", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetDockList(GetDockListModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "AccountID" , obj.AccountID },
                    { "WarehouseID" , obj.WarehouseID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_GetDocListByWH", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetZoneList(GetZoneListModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "AccountID" , obj.AccountID },
                    { "WarehouseID" , obj.WarehouseID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_ACC__IL_GetZoneList", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetRackTypes()
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> { };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_MST_GetRackDrop", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetWarehouseTypes(GetWarehouseTypesModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "Flag" , obj.Flag }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "sp_Android_GetWarehouses", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetSupplierDropdown_Tenant(GetSupplierDropdown_TenantModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "@prefix" , obj.prefix },
                    { "@AccountID" , obj.AccountID },
                    { "@TenantID" , obj.TenantID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_MST_DropSupplierTenantWise", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetMCodeDropdown(GetMCodeDropdownModel obj)
        {
            Payload<string> response = new Payload<string>();

            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "prefix" , obj.prefix },
                    { "AccountID" , obj.AccountID },
                    { "SupplierID" , obj.SupplierID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_INL_LoadMCodeDataFor3PLSupplier", sqlParams).ConfigureAwait(false);

                //string Query = "EXEC [dbo].[USP_INL_LoadMCodeDataFor3PLSupplier] @prefix=" + "'" + obj.prefix + "'" + " , @AccountID=" + obj.AccountID + ", @SupplierID=" + obj.SupplierID + "";
                //var DS = DbUtility.GetDS(Query, this.ConnectionString);
                //response.Result = JsonConvert.SerializeObject(DS);
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

        public async Task<Payload<string>> GetMTypeData(GetMTypeDataModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "prefix" , obj.prefix },
                    { "TenantID" , obj.TenantID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_Service_INL_LoadMTypeDataItem", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetMGroupData(GetMGroupDataModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "prefix" , obj.prefix },
                    { "TenantID" , obj.TenantID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_Service_INL_LoadMaterialGroupDataItem", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> LoadEmployeeList(LoadEmployeeListModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "prefix" , obj.prefix }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_INL_LoadEmployeeListData", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetRackList(GetlistModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                    { "@LocationZoneID", obj.ZoneID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_MST_LM_IL_GetRacList", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetBayList(GetlistModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> { { "@LocationZoneID", obj.ZoneID } };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_MST_LM_IL_GetBayList", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetColumnList(GetlistModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> { { "@LocationZoneID", obj.ZoneID } };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_MST_LM_IL_GetColumnList", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetBinList()
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> { };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_MST_LM_IL_GetBinList", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetWarehouse(GetWarehouseDataModel obj)
        {
            Payload<string> response = new Payload<string>();
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                StringBuilder sp = new StringBuilder();
                sp.AppendLine("EXEC [sp_GetWarehouseData] @WarehouseIDs ='" + obj.WarehouseIDs + "',@InOutID=" + obj.InOutID + ",@AccountID_New=" + obj.AccountID_New + ",@UserID_New=" + obj.UserID_New + ",@TenantID_New=" + obj.TenantID_New + ",@UserTypeID_New=" + obj.UserTypeID_New + ";"
                + "EXEC [USP_INL_GetLocationZonesByWHID] @AccountID =" + obj.AccountID_New + ",@WarehouseID='" + obj.WarehouseIDs + "',@ZoneID=" + obj.ZoneID + "");
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
        public async Task<Payload<string>> GetLocationZonesByWHID(GetLocationZonesByWHIDModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {

                 { "@WarehouseID" , obj.WarehouseID },
                 { "@AccountID" , obj.AccountID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_INL_GetLocationZonesByWHID", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> LoadLocationTypes()
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> { };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_MST_LM_IL_LoadLocationTypes", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetInwardStatus()
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> { };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_ORD_INL_GetPoStatus", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> LoadPOTypes(LoadPOTypesModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "prefix" , obj.prefix }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_Service_INL_LoadPOTypes", sqlParams).ConfigureAwait(false);
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


        public async Task<Payload<string>> LoadINBTypes(LoadINBTypesModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "prefix" , obj.prefix }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_Service_INL_LoadINBTypes", sqlParams).ConfigureAwait(false);
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

        
        public async Task<Payload<string>> GetSuppliers_POType(GetSuppliers_POTypeModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "TenantID" , obj.@TenantID },
                    { "AccountID" , obj.@AccountID },
                    { "prefix" , obj.prefix },
                    { "LogTenantID" , obj.LogTenantID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_LoadSupplierDataPOtype", sqlParams).ConfigureAwait(false);
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


        public async Task<Payload<string>> Get_inv_cs_Locations_tolocation(HouseKeepingInputModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>{
                {"@WarehouseID",obj.WarehouseID},
                {"@AccountID",obj.AccountID },
                {"@MaterialmasterID", obj.MaterialMasterId },
                {"@prefix",obj.prefix}
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "Usp_inv_cs_Locations_tolocation", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetPONumbers(GetPONumbersModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                StringBuilder SB = new StringBuilder();
                SB.AppendLine("SELECT TOP 10 PONumber, POHeaderID FROM ORD_POHeader ORD");
                SB.AppendLine("INNER JOIN TPL_Tenant TNT ON ORD.TenantID = TNT.TenantID");
                SB.AppendLine("AND TNT.IsActive = 1 AND TNT.IsDeleted = 0");
                SB.AppendLine("WHERE ORD.IsActive = 1 AND ORD.IsDeleted = 0 AND POTypeID NOT IN(31)");
                SB.AppendLine("AND (0 = @TenantID OR TNT.TenantID = @TenantID)");
                SB.AppendLine("AND PONumber LIKE @Prefix + '%'");
                SB.AppendLine("AND TNT.TenantID = CASE WHEN 0 = @LogTenantID THEN TNT.TenantID ELSE @LogTenantID END");
                SB.AppendLine("AND TNT.AccountID = @AccountID");
                SB.AppendLine("ORDER BY PONumber");
                string query = SB.ToString();
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                SqlCommand command = new SqlCommand(query);
                command.Parameters.AddWithValue("@TenantID", obj.TenantID);
                command.Parameters.AddWithValue("@Prefix", obj.prefix);
                command.Parameters.AddWithValue("@LogTenantID", obj.LogTenantID);
                command.Parameters.AddWithValue("@AccountID", obj.AccountID);
                response.Result = await DbUtility.GetJsonDataFromCommand(this.ConnectionString, command);
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
        public async Task<Payload<string>> SearchPartNumber(SearchPartNumberModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "prefix" , obj.prefix },
                    { "POHeaderID" , obj.POHeaderID },
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_PODetails_MCodeDropData", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetSKUPO(GetSKUPOModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "prefix" , obj.prefix },
                    { "TenantID" , obj.TenantID },
                    { "SupplierID" , obj.SupplierID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_MST_MCodeDropData", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetUoMQty(UoMQtyPOModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "MaterialID" , obj.MaterialID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_LoadUoMQtyPO", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> InvNumbersSI(InvNumbersSIModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "SupplierInvoiceID" , obj.SupplierInvoiceID },
                    { "POHeaderID" , obj.POHeaderID },
                    { "PODetailsID" , obj.PODetailsID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_LoadInvNumbersSI", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetSOStatus()
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> { };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_GetSOStatus", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetSONumbersList(GetSONumbersListModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "LogTenantID" , obj.LogTenantID },
                    { "LogAccountID" , obj.LogAccountID },
                    { "prefix" , obj.prefix },
                    { "IsToolItem" , obj.IsToolItem }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_GetSONumbersList", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetSoType(GetSoTypeModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "prefix" , obj.prefix }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_MST_DropSoType", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetCustomersTenant(GetCustomersTenantModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "@prefix" , obj.prefix },
                    { "@TenantID" , obj.TenantID },
                    { "@LoggedTenantID" , obj.LogTenantID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_LoadCustomers", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetMCodeForSaleOrder(GetMCodeForSaleOrderModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "prefix" , obj.prefix },
                    { "TenantID" , obj.TenantID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                string jsonString = await DbUtility.GetjsonData(this.ConnectionString, "USP_Service_INL_LoadTenantMCodeForSaleOrder", sqlParams).ConfigureAwait(false);
                List<Dictionary<string, object>> table = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonString);
                foreach (Dictionary<string, object> row in table)
                {

                    string mcode = row["MCode"].ToString();
                    string[] mcodeParts = mcode.Split('`');
                    string newMCode = mcodeParts[0].Replace("'", "");
                    row["MCode"] = newMCode;
                }


                string Mcode = JsonConvert.SerializeObject(table);
                response.Result = Mcode;



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
        public async Task<Payload<string>> GetCustomerPOdata(GetCustomerPOdataModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "SOHeaderID" , obj.SOHeaderID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_GetCustomerPOdata", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetStorageLocations()
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> { };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "SP_INV_GET_STORAGELOCATION", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetMCodeForSOWithOEM(GetMCodeForSOWithOEMModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "prefix" , obj.prefix },
                    { "SOHeaderID" , obj.SOHeaderID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_LoadMCodeForSOWithOEM", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetOrderforIssue(GetOrderforIssueModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "prefix" , obj.prefix }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_INL_GetOrderforissue", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetQONumber(GetQONumberModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "prefix" , obj.prefix }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_INL_GetQONumber", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetStatusForEmp(GetStatusForEmpModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "prefix" , obj.prefix }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_ORD_GetStatusForEmp", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetTenantbyUser_Emp(GetTenantbyUser_EmpModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "TenantID" , obj.TenantID },
                    { "prefix" , obj.prefix }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_INL_GetTenantbyUser", sqlParams).ConfigureAwait(false);
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


        public async Task<Payload<string>> GetTopallets(ItemsModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>{
                {"Prefix",obj.prefix},
                {"MaterialMasterID",obj.MaterialMasterID},
                {"WarehouseID",obj.Warehouseid},
                {"LocationID",obj.LocationID},
                {"TenantID",obj.TenantId}
            };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "sp_GET_Container_INTER_Topallet", sqlParams, true).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetLabSampleLocation(GetLabSampleLocationModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "prefix" , obj.prefix }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_ORD_INL_GETLocationData", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetEmployeeRequestDetails(GetEmployeeRequestDetailsModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "EmployeeID" , obj.EmployeeID },
                    { "RequestHeaderID" , obj.RequestHeaderID },
                    { "RequestTypeID" , obj.RequestTypeID },
                    { "SAP_MDNumber" , obj.SAP_MDNumber },
                    { "EmpRequestStatusID" , obj.EmpRequestStatusID },
                    { "TenantId" , obj.TenantId },
                    { "Rownumber" , obj.Rownumber },
                    { "NofRecordsPerPage" , obj.NofRecordsPerPage }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "sp_ORD_GetEmployeeRequestDetails", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetPONumbersLabSampleRequest(GetPONumbersLabSampleRequestModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                String TenantID = obj.TenantID;
                string prefix = obj.prefix;
                StringBuilder SB = new StringBuilder();
                SB.AppendLine("select distinct TOP 10 PONumber,POH.POHeaderID from ORD_POHeader POH JOIN INB_GRNUpdate GRU ON GRU.POHeaderID = POH.POHeaderID JOIN INB_Inbound INB ON INB.InboundID = GRU.InboundID WHERE POH.ISACTIVE = 1 AND POH.ISDELETED = 0 AND POH.TENANTID = @TenantID AND PONumber like @prefix order by PONumber");
                string Data = SB.ToString();
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                SqlCommand command = new SqlCommand(Data);
                command.Parameters.AddWithValue("@TenantID", TenantID);
                command.Parameters.AddWithValue("@prefix", prefix + "%");
                response.Result = await DbUtility.GetJsonDataFromCommand(this.ConnectionString, command);

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
        public async Task<Payload<string>> GetMCodesBasedOnPO(GetMCodesBasedOnPOModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "POHeaderID" , obj.POHeaderID },
                    { "prefix" , obj.prefix }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_GetMCodesBasedOnPO", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetStorageLocationForStockPosting(GetStorageLocationForStockPostingModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "prefix" , obj.prefix }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_GetGetStorageLocationForStockPosting", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetDeliveryPointData(GetDeliveryPointDataModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "EntityID" , obj.EntityID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "USP_GetDeliveryPointData", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetFastMoveData()
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> { };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "USP_ORD_INL_FastMoveData", sqlParams).ConfigureAwait(false);
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



        public async Task<Payload<string>> GetIndustry(MaterialDataInputModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                {"Prefix",obj.prefix},
                {"LoginAccountId",obj.LoginAccountId},
                {"LoginUserId",obj.LoginUserId},
                {"LoginTanentId",obj.LoginTanentId},
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_Im_GetIndustry", sqlParams).ConfigureAwait(false);
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


        public async Task<Payload<string>> GetIndustryMaterialAttributes(IndustryMaterialAttributesInputModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>{
               {"MaterialMasterID",obj.MaterialMasterID},
               {"LanguageType",obj.LanguageType},
               {"GEN_IndustryID",obj.GEN_IndustryID},
               {"AccountID",obj.AccountID},
               {"TenantId",obj.TenantId},
               {"LoginAccountId",obj.LoginAccountId},
                {"LoginUserId",obj.LoginUserId},
                {"LoginTanentId",obj.LoginTanentId},
            };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "Usp_GetIndustryMaterialAttribute", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetLoadSAPReference(GetLoadSAPReferenceModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "@prefix" , obj.prefix }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_Service_INL_LoadSAPReference", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetFastMovingWH(GetFastMovingWHModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "prefix" , obj.prefix },
                    { "AccountID" , obj.AccountID },
                    { "UserID" , obj.UserID },
                    { "Flag" , 2 }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_MST_DropWH", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetTenantsDropdown_Warehouse(GetTenantsDropdown_WarehouseModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "prefix" , obj.prefix },
                    { "whid" , obj.whid }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "SP_RPT_TENANTDROPDOWN", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetPOHeaderListTenant(GetPOHeaderListTenantModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "prefix" , obj.prefix },
                    { "TenantID" , obj.TenantID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "SP_USP_FalconServices_GetPOHeaderListTenant", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetInvoiceListForPONumber(GetInvoiceListForPONumberModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "POHeaderID" , obj.POHeaderID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "SP_USP_FalconServices_GetInvoiceListForPONumber", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetDocks(GetDocksModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "IsOutbound" , obj.IsOutbound },  
                    { "Warehouseid" , obj.Warehouseid },
                    { "Inboundid" , obj.InboundID },
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "SP_GetDocks", sqlParams).ConfigureAwait(false);
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



        public async Task<Payload<string>> GetLoadingPoints(GetLoadingPoints obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "TenantID" , obj.TenantID },
                    { "Warehouseid" , obj.Warehouseid }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "SP_GetLoadingPoints", sqlParams).ConfigureAwait(false);
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


        public async Task<Payload<string>> GetTokenNumberOBD(GetLoadingPoints obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "TenantID" , obj.TenantID },
                    { "Warehouseid" , obj.Warehouseid }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "SP_GetTokenNumbersOBD", sqlParams).ConfigureAwait(false);
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


        public async Task<Payload<string>> GetVehicleTypes()
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "Get_VehicleTypes_Web", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetVehicleTypesWeb(GetVehicleTypes vehicle)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    {"Prefix" , vehicle.prefix }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "Get_VehicleTypes_Web", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetEmployeeReturnList(GetEmployeeReturnListModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "EmployeeID" , obj.EmployeeID },
                    { "RequestHeaderID" , obj.RequestHeaderID },
                    { "RequestTypeID" , obj.RequestTypeID },
                    { "SAP_MDNumber" , obj.SAP_MDNumber },
                    { "EmpRequestStatusID" , obj.EmpRequestStatusID },
                    { "TenantId" , obj.TenantId },
                    { "Rownumber" , obj.Rownumber },
                    { "NofRecordsPerPage" , obj.NofRecordsPerPage }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "sp_ORD_GetEmployeeRequestDetails", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetDeleteItemsById(GetDeleteItemsByIdModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "ID" , obj.ID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_ORD_INL_ORD_EmployeeRequestHeader", sqlParams).ConfigureAwait(false);
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

        //material transfer
        public async Task<Payload<string>> GetMaterial_Internal(GetMaterial_InternalModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                    { "@prefix" , items.prefix },
                    { "@TenantID" , items.TenantID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                string jsonString = await DbUtility.GetjsonData(this.ConnectionString, "SP_INL_GetSitecode", sqlParams).ConfigureAwait(false);
                List<Dictionary<string, object>> table = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(jsonString);
                foreach (Dictionary<string, object> row in table)
                {

                    string mcode = row["MCode"].ToString();
                    string[] mcodeParts = mcode.Split('`');
                    string newMCode = mcodeParts[0].Replace("'", "");
                    row["MCode"] = newMCode;
                }


                string Mcode = JsonConvert.SerializeObject(table);
                response.Result = Mcode;



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


        public async Task<Payload<string>> ToLocationsDropDown(ToLocationsDropDownModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>{
                    {"WarehouseID",obj.WarehouseID},
                    {"AccountID",obj.AccountID },
                    {"prefix",obj.prefix},
                    { "refcur","Ref_Cursor"},
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "Usp_inv_cs_Locations_tolocation", sqlParams, true).ConfigureAwait(false);
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


        public async Task<Payload<string>> GetLocationsDropDown(GetLocationsDropDownModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                    { "@AccountID" , items.AccountID },
                    { "@TenantID" , items.TenantID },
                    { "@WarehouseID" , items.WarehouseID },
                    { "@MaterialMasterID" , items.MaterialMasterID },
                    { "@prefix" , items.prefix },
                    { "@IsProdOrder",items.IsProdOrder}
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_Service_INL_GetActiveStockLocations", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetBatchListDropDown(GetBatchListDropDownModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "@MATERIALMASTERID" , items.MaterialMasterID },
                    { "@LOCATIONID" , items.LocationID },
                    { "@CARTONID" , items.CartonID },
                    { "@prefix" , items.prefix },
                    { "@TENANTID" , items.TenantID },
                    { "@IsProdOrder", items.IsProdOrder }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "SP_TRN_ABL_BATCHLIST", sqlParams).ConfigureAwait(false);
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


        public async Task<Payload<string>> GetBatchGradeList(GetBatchListDropDownModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "@MATERIALMASTERID" , items.MaterialMasterID },
                    { "@LOCATIONID" , items.LocationID },
                    { "@CARTONID" , items.CartonID },
                    { "@prefix" , items.prefix },
                    { "@TENANTID" , items.TenantID },
                    { "@IsProdOrder",items.IsProdOrder}
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "SP_TRN_ABL_BATCHGRADELIST", sqlParams).ConfigureAwait(false);
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


        public async Task<Payload<string>> GetGradeListByBatch(GetGrdaeListDropDown items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "@MATERIALMASTERID" , items.MaterialMasterID },
                    { "@LOCATIONID" , items.LocationID },
                    { "@CARTONID" , items.CartonID },
                    { "@prefix" , items.prefix },
                    { "@TENANTID" , items.TenantID },
                    { "@IsProdOrder",items.IsProdOrder},
                    { "@BatchNo",items.BatchNo}
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "SP_TRN_ABL_GRADELISTByBatch", sqlParams).ConfigureAwait(false);
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





        public async Task<Payload<string>> GetSLOCDropDownBatchGrade(GetSLOCDropDownModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                   { "@MATERIALMASTERID" , items.MaterialMasterID },
                    { "@LOCATIONID" , items.LocationID },
                    { "@CARTONID" , items.CartonID },
                    { "@prefix" , items.prefix },
                    { "@TENANTID" , items.TenantID },
                    { "@BATCH" , items.BatchNo },
                    { "@IsProdOrder",items.IsProdOrder}

                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_Service_INL_GetToStorageLocation_BatchGrade", sqlParams).ConfigureAwait(false);
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


        public async Task<Payload<string>> GetSLOCDropDown(GetSLOCDropDownModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "@prefix" , items.prefix }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_Service_INL_GetToStorageLocation", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetPalletsDropDown(GetPalletsDropDownModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "@prefix" , items.prefix },
                    { "@WarehouseID" , items.WarehouseID },
                    { "@LocationID" , items.LocationID },
                    { "@MaterialMasterID" , items.MaterialMasterID },
                    { "@IsProdOrder",items.IsProdOrder}
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "sp_GET_Container_INTER", sqlParams).ConfigureAwait(false);
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


        public async Task<Payload<string>> GetProjectBasedSKUDropdown(GetProjectBasedSKUDropdownModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {          
                    { "@TENANTID" , items.TenantID },
                    { "@Projectrefno" , items.Projectrefno},
                    { "@prefix" , items.prefix }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "SP_Project_Based_SKU", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetProjectRefNoDropDown(GetProjectRefNoDropDownModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "@MATERIALMASTERID" , items.MaterialMasterID },
                    { "@LOCATIONID" , items.LocationID },
                    { "@CARTONID" , items.CartonID },
                    { "@TENANTID" , items.TenantID },
                    { "@prefix" , items.prefix },
                    { "@BatchNo" , items.BatchNo }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "SP_TRN_ABL_ProjectRefNoList", sqlParams).ConfigureAwait(false);
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


        //cyclecounts
        public async Task<Payload<string>> GetZonesDropDown(GetZonesDropDownModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "@prefix" , items.prefix },
                    { "@WarehouseID" , items.WarehouseID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "SP_USP_FalconService_GetLocationZoneDetails", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetCCListData(GetCCListDataModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                    { "@CCM_CNF_AccountCycleCount_ID" , items.CycleCount_ID }
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



        public async Task<Payload<string>> GetCC_UserDropDown(GetCC_UserDropDownModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                    { "@prefix" , items.prefix },
                    { "@WarehouseID" , items.WarehouseID },
                    { "@AccountID" , items.AccountID },
                    { "@TenantID" , items.TenantID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "SP_USP_FalconService_GetWarehouseUserDetails", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetCC_Materials(GetCC_MaterialsModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                    { "@prefix" , items.prefix },
                    { "@TenantID" , items.TenantID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "SP_USP_RPT_GetSKUData", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetCC_RackDetails(GetCC_RackDetailsModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                    { "@UserId" , items.UserID },
                    { "@AM_MST_Account_ID" , items.AccountID },
                    { "@CCM_CNF_AccountCycleCount_ID" , items.CycleCount_ID }
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
        //inbound
        public async Task<Payload<string>> GetTenantDropdown_Warehouse(GetTenantDropdown_WarehouseModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                    { "prefix" , items.prefix },
                    { "WHID" , items.WHID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "USP_INL_GetWarehouseTenant", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetShipmentTypes(GetShipmentTypesModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                    { "prefix" , items.prefix },
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "USP_Service_INL_LoadShipmentTypeDataForInbound", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetSupplierForInbound(GetSupplierForInboundModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                    { "perfix" , items.prefix },
                    { "TenantID" , items.TenantID },
                    { "ShipmentTypeID" , items.ShipmentTypeID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "USP_GetSuppliersForInbound", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetPONumbersForInbound(GetPONumbersForInboundModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                    { "prefix" , items.prefix },
                    { "TenantID" , items.TenantID },
                    { "SupplierID" , items.SupplierID },
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "USP_Service_INL_LoadPONumbers", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetInvoiceNoForInbound(GetInvoiceNoForInboundModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                    { "SupplierID" , items.SupplierID },
                    { "POHeaderID" , items.POHeaderID },
                    { "prefix" , items.prefix },
                    { "SupplierInvoiceID" , items.SupplierInvoiceID },
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "USP_InboundLoadInvoiceNo", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetPONumberForGRN(GetPONumberForGRNModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                   { "prefix" , items.prefix },
                   { "InboundID" , items.InboundID },
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "USP_Service_INL_LoadPoNumberForGRN", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetInvoiceNumberForGRN(GetInvoiceNumberForGRNModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                   { "prefix" , items.prefix },
                   { "InboundID" , items.InboundID },
                   { "POHeaderID" , items.POHeaderID },
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "USP_Service_INL_LoadGRNPOInvoiceNumbers", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetInvoiceNumberForDisc(GetInvoiceNumberForDiscModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                   { "prefix" , items.prefix },
                   { "InboundID" , items.InboundID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "USP_Service_LoadDisInvoiceNumbers", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetPONumberForDisc(GetPONumberForDiscModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                   { "prefix" , items.prefix },
                   { "InboundID" , items.InboundID },
                   { "TenantID" , items.TenantID },
                   { "SupplierInvoiceID" , items.SupplierInvoiceID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "USP_Service_INL_LoadConfiguredIBPONumbers", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetMCodesForDisc(GetMCodesForDiscModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                   { "prefix" , items.prefix },
                   { "InboundID" , items.InboundID },
                   { "SupplierInvoiceID" , items.SupplierInvoiceID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "USP_Service_INL_LoadPOMCodes", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetPOLineNumbersForDisc(GetPOLineNumbersForDiscModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                   { "POHeaderID" , items.POHeaderID },
                   { "InboundID" , items.InboundID },
                   { "SupplierInvoiceID" , items.SupplierInvoiceID },
                   { "MaterialMasterID" , items.MaterialMasterID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "USP_Service_INL_LoadPOLineNumbers", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetUsersDataForInbound(GetUsersDataForInboundModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                   { "prefix" , items.prefix },
                   { "TenantID" , items.TenantID },
                   { "LogAccountID" , items.LogAccountID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "USP_LoadUserDropDown", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetRTRMCodes(GetRTRMCodesModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                   { "InboundID" , items.InboundID },
                   { "MCode" , items.MCode }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
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
        public async Task<Payload<string>> GetLoactionsForGoodsIn(GetLoactionsForGoodsInModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                   { "prefix" , items.prefix },
                   { "InboundID" , items.InboundID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "USP_Service_INL_Loadloaction_GoodsIn", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetPalletsForGoodsIn(GetPalletsForGoodsInModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                   { "prefix" , items.prefix },
                   { "InboundID" , items.InboundID },
                   { "Location" , items.Location }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "sp_GET_Loction_CartonList", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetStorageLoactionsForGoodsIn()
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> { };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_Service_INL_LoadStorageLocation_GoodsIn", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetVehicleListForGoodsIn(GetVehicleListForGoodsInModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                   { "InboundID" , items.InboundID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "USP_Service_INL_LoadVehicleList_GoodsIn", sqlParams).ConfigureAwait(false);
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


        public async Task<Payload<string>> GetVehicleListForGroupOBD(GetVehicleListForGoodsInModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                   { "InboundID" , items.InboundID },
                   { "TenantID", items.TenantID},
                   { "WarehouseID", items.WarehouseID},
                   { "CustomerID", items.CustomerID}

                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "USP_Service_INL_LoadVehicleList_GroupOBD", sqlParams).ConfigureAwait(false);
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


        public async Task<Payload<string>> GetSITStoreRefNumbers(GetStoreRefNumbersModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                   { "StoreRefNo" , items.StoreRefNo },
                   { "WarehouseIDs" , items.WarehouseIDs },
                   { "AccountID_New" , items.AccountID_New },
                   { "TenantID" , items.TenantID }
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
        public async Task<Payload<string>> GetSIEStoreRefNumbers(GetStoreRefNumbersModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                   { "@StoreRefNo" , items.StoreRefNo },
                   { "@WarehouseIDs" , items.WarehouseIDs },
                   { "@AccountID_New" , items.AccountID_New },
                   { "@TenantID" , items.TenantID },
                   { "@InbPanelId", items.InbPanelId }
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
        public async Task<Payload<string>> GetSIPStoreRefNumbers(GetStoreRefNumbersModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                   { "StoreRefNo" , items.StoreRefNo },
                   { "WarehouseIDs" , items.WarehouseIDs },
                   { "AccountID_New" , items.AccountID_New },
                   { "TenantID" , items.TenantID }
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
        public async Task<Payload<string>> GetInboundStatus(GetInboundStatusModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                StringBuilder SB = new StringBuilder();
                SB.AppendLine("select top 10 InboundStatusID,InboundStatus from INB_InboundStatus where IsActive = 1 and IsDeleted = 0 and InboundStatus like @prefix order by 1 desc");
                string Account = SB.ToString();
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                SqlCommand command = new SqlCommand(Account);
                command.Parameters.AddWithValue("@prefix", obj.prefix + "%");
                response.Result = await DbUtility.GetJsonDataFromCommand(this.ConnectionString, command);
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
        public async Task<Payload<string>> GetShipmentTypes_InboundSearch(GetShipmentTypes_InboundSearchModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                StringBuilder SB = new StringBuilder();
                SB.AppendLine("SELECT TOP 10 ShipmentTypeID, ShipmentType FROM GEN_ShipmentType WHERE IsActive = 1 AND IsDeleted = 0 AND ShipmentType LIKE @prefix");
                string Account = SB.ToString();
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                SqlCommand command = new SqlCommand(Account);
                command.Parameters.AddWithValue("@prefix", obj.prefix + "%");
                response.Result = await DbUtility.GetJsonDataFromCommand(this.ConnectionString, command);

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
        public async Task<Payload<string>> GetRevertStoreRefNo(GetRevertStoreRefNoModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                   { "@StoreRefNo" , items.StoreRefNo },
                   { "@UserID_New" , items.UserID_New },
                   { "@AccountID_New" , items.AccountID_New },
                   { "@TenantID" , items.TenantID },
                   { "@TenantID_New" , items.TenantID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "sp_Get_Store_Refno", sqlParams).ConfigureAwait(false);
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
        //release outbound //OBD NUMBERS
        public async Task<Payload<string>> GetLoadOBDNumbers(GetLoadOBDNumbersModel obj)
       {
            Payload<string> response = new Payload<string>();
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
             

                string Query = "EXEC [dbo].[SP_GetReleaseOBDNos] @prefix=" + "'" + obj.prefix + "'" + " , @AccountID=" + obj.AccountID + ", @Isreport=" + obj.Isreport + ", @TenantID=" + obj.TenantID + "";
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
        //OUTBOUND TRACKING//Pending OBD's For VLPD Creation//SERACH.DELV.DOC#
        public async Task<Payload<string>> GetLoadVLPDDelvDocNo(GetLoadVLPDDelvDocNoModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                   { "Accountid" , items.Accountid },
                   { "Tenantid" , items.Tenantid },
                   { "OBDNumber" , items.prefix }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "sp_OBD_GetVLPDPendingList", sqlParams).ConfigureAwait(false);
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
        //Pick N Check Pending   //SERACH.DELV.DOC#
        public async Task<Payload<string>> GetLoadPNCPendingDelvDocNo_TC(GetLoadPNCPendingDelvDocNo_TCModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                    { "OBDNumber" , items.OBDNumber },
                   { "WarehouseIDs" , items.WarehouseIDs },
                   { "TenantID" , items.TenantID },
                   { "AccountID_New" , items.AccountID_New },
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
        //outboundtracking//PGI Pending  //SERACH.DELV.DOC#

        public async Task<Payload<string>> GetLoadPGIPendingDelvDocNo(GetLoadPGIPendingDelvDocNoModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                   { "OBDNumber" , items.OBDNumber },
                   { "WarehouseIDs" , items.WarehouseIDs },
                   { "TenantID" , items.TenantID },
                   { "AccountID_New" , items.AccountID_New },
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
        // outboundtracking//POD Pending //SERACH.DELV.DOC#
        public async Task<Payload<string>> GetLoadPODDelvDocNo(GetLoadPODDelvDocNoModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                   { "OBDNumber" , items.prefix },
                   { "WarehouseIDs" , items.WarehouseIDs },
                   { "TenantID" , items.TenantID },
                   { "AccountID_New" , items.AccountID_New }
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
        // outboundtracking//POD Pending //SERACH.DELV.DOC#
        public async Task<Payload<string>> GetShipmentVerificationRef(GetShipmentVerificationRefModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                   { "InboundID" , items.InboundID },
                   { "IB_RefWarehouse_DetailsID" , items.IB_RefWarehouse_DetailsID },
                   };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "USP_INB_INL_ShipmentVerificationRef", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetSkipReason(GetSkipReasonModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                StringBuilder SB = new StringBuilder();
                SB.AppendLine("SELECT Reason, ReasonID FROM GEN_SkipReason WHERE ReasonID <> @reasonId AND IsActive = 1 AND IsDeleted = 0");
                string Account = SB.ToString();
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                SqlCommand command = new SqlCommand(Account);
                command.Parameters.AddWithValue("@reasonId", 2);
                response.Result = await DbUtility.GetJsonDataFromCommand(this.ConnectionString, command);
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
        public async Task<Payload<string>> GetDeliveryDetails(GetDeliveryDetailsModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                   { "WarehouseID" , items.WarehouseID },
                   { "OutboundID" , items.OutboundID },
                   };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "USP_GetREF_WHID", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> Getlabel(GetLabelModel obj)
        {
            Payload<string> response = new Payload<string>();
            string? stringValue = obj.ActionType.ToString();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                    { "@ActionType", !string.IsNullOrEmpty(stringValue) ? (object)stringValue : DBNull.Value },
                   };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "SP_USP_RPT_GetLabel", sqlParams).ConfigureAwait(false);
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
        //inventory //SKU
        public async Task<Payload<string>> GetLoadMaterialsForCurrentStock(GetLoadMaterialsForCurrentStockModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "TenantID" , obj.TenantID },
                    { "Prefix" , obj.Prefix }
                    };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "SP_USP_RPT_GetSKUData", sqlParams).ConfigureAwait(false);
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
        //inventory//currentstock //materialtype
        public async Task<Payload<string>> GetLoadMaterialTypesForCurrentStock(GetLoadMaterialTypesForCurrentStockModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "TenantID" , obj.TenantID },
                    { "Prefix" , obj.Prefix }
                    };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_Service_INL_LoadMaterialTypesForCurrentStock", sqlParams).ConfigureAwait(false);
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
        //inventory//currentstock //materialdraw type
        public async Task<Payload<string>> GetLoadMaterialDrawTypesForCurrentStock(GetLoadMaterialDrawTypesForCurrentStockModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "Prefix" , obj.Prefix }
                    };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_Service_INL_LoadMaterialDrawTypesForCurrentStock", sqlParams).ConfigureAwait(false);
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
        //inventory//currentstock //storage location
        public async Task<Payload<string>> GetSlocforCurrentstock(GetSlocforCurrentstockModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "Prefix" , obj.Prefix }
                    };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_INL_getSlocforCurrentstock", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetDocumentType()
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> { };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "sp_Android_GetDocumentType", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetLoadLocationsForCurrentStock(GetLoadLocationsForCurrentStockModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "Prefix" , obj.Prefix },
                    { "AccountID" , obj.AccountID },
                    { "WarehouseID" , obj.WarehouseID }
                    };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_Service_INL_LoadLocationsForCurrentStock", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetKitPlannerId(GetKitPlannerIdModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "Prefix" , obj.Prefix },
                    { "TenantID" , obj.TenantID },
                    };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_Service_INL_GetKitPlannerId", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetLoadIndustries_Auto(GetLoadIndustries_AutoModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "Prefix" , obj.Prefix },
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_INL_LoadIndustries_Auto", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetContainersForCurrentStock(GetContainersForCurrentStockModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "Prefix" , obj.Prefix },
                     { "WarehouseID" , obj.WarehouseID },
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_INL_getContainersForCurrentStock", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetLoadSOCustomerNames(GetLoadSOCustomerNamesModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "Prefix" , obj.Prefix },
                    { "TenantID" , obj.TenantID }
                    };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_GetCustomersForOBD", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetLoadUsersData(GetLoadUsersDataModel items)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            Payload<string> response = new Payload<string>();
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                string Query = "EXEC [dbo].[USP_LoadUserDropDown1] @Prefix=" + "'" + items.Prefix + "'" + " , @TenantID=" + items.TenantID + ", @LogAccountID=" + items.AccountID + ", @flag=" + 0 + ", @WarehouseID=" + items.WarehouseID + "";
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

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<Payload<string>> GetPalletCode(GetPalletCodeModel obj)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            Payload<string> response = new Payload<string>();
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                string Query = "EXEC [dbo].[sp_GET_Loction_CartonList_Tenant] @Prefix=" + "'" + obj.Prefix + "'" + " , @Location=" + obj.Location + ", @WarehouseId=" + obj.WarehouseId + ", @TenantID=" + obj.TenantID + ", @UserId=" + obj.UserId + "";
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
        public async Task<Payload<string>> GetAllLocationsUnderWarehouse(GetAllLocationsUnderWarehouseModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "Prefix" , obj.Prefix },
                    { "MaterialMasterID" , obj.MaterialMasterID },
                    { "WarehouseId" , obj.WarehouseId },
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "sp_GET_Loction_INTER", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetAuditLogReferenceNo_RPRT(GetAuditLogReferenceNo_RPRTModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                var SQLQuery = "";
                if (obj.CategoryID == 1)
                {
                    SQLQuery = "Exec SP_USP_FalconServices_GetInboundID @Prefix=" + DBLibrary.SQuote(obj.Prefix);
                }
                else if (obj.CategoryID == 2)
                {
                    SQLQuery = "Exec SP_USP_FalconServices_GetOutboundID @Prefix=" + DBLibrary.SQuote(obj.Prefix);
                }
                else if (obj.CategoryID == 3)
                {
                    SQLQuery = "Exec SP_USP_FalconServices_GetTransferRequestIDDeatils @Prefix= " + DBLibrary.SQuote(obj.Prefix);
                }
                else if (obj.CategoryID == 4)
                {
                    SQLQuery = "Exec SP_USP_FalconServices_GetCCM_CNF_AccountCycleCount_ID @Prefix=" + DBLibrary.SQuote(obj.Prefix);
                }
                response.Result = await DbUtility.GetjsonDataFromDatasetINQ(this.ConnectionString, SQLQuery);

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

        public async Task<Payload<string>> GetMaterialForMiscReceipt(GetMaterialForMiscReceiptModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    { "Prefix" , obj.Prefix },
                    { "TenantID" , obj.TenantID },
                    { "AccountID" , obj.AccountID },
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "SP_USP_FalconServices_GetMaterialForMiscReceipt", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetMaterialForMiscIssue(GetMaterialForMiscIssueModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "Prefix" , obj.Prefix },
                    { "TenantID" , obj.TenantID },
                    { "AccountID" , obj.AccountID },
                    };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "SP_USP_FalconServices_GetRawMaterial", sqlParams).ConfigureAwait(false);
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
        //15-03-2023 
        public async Task<Payload<string>> GetStoreRefNumbers_RPRT(GetStoreRefNumbers_RPRT_Model obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    {"@WarehouseID" , obj.WarehouseID },
                    {"@TenantID" , obj.TenantID },
                    {"@AccountID" , obj.AccountID },
                    };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "SP_USP_RPT_LoadStoreRefNumbersData", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetOutboundNumbers_RPRT(GetOutboundNumbers_RPRT_Model obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {

                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                
                string Query = "EXEC [dbo].[SP_GET_OBDLIST] @Prefix=" + "'" + obj.Prefix + "'" + " , @TenantID=" + obj.TenantID + ", @CustomerId=" + obj.CustomerId + ", @WareHouseId=" + obj.WareHouseId + "";
                var DS = DbUtility.GetDS(Query, this.ConnectionString);
                response.Result = JsonConvert.SerializeObject(DS.Tables[0]);

                //var sqlParams = new Dictionary<string, object>  {
                //    {"@CustomerId" , obj.CustomerId },
                //    {"@TenantID" , 8 /*obj.TenantID*/ },
                //    {"@WareHouseId" , obj.WareHouseId },
                //    {"@Prefix" , "'"+obj.Prefix+"'" }

                ////};
                //response.Result = await DbUtility.GetDS(this.ConnectionString, "SP_GET_OBDLIST", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetCustomerData_RPRT(GetCustomerData_RPRT_Model obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    {"TenantId" , obj.TenantId },
                    {"Prefix" , obj.Prefix }

                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "SP_USP_RPT_GetCustmerData ", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetSONumbers_RPRT(GetSONumbers_RPRT_Model obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    {"OutboundID" , obj.OutboundID },
                    {"prefix" , obj.prefix }

                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_INL_GetSOList", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetSONumbersForSO_RPRT(GetSONumbersForSO_RPRT_Model obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    {"@AccountID" , obj.LoginAccountId },
                    {"@TenantID" , obj.LoginTanentId },
                    {"@UserID" , obj.LoginUserId },
                    {"@prefix" , obj.prefix }

                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "SP_USP_FalconServices_LoadSONumbersForSalesOrderReport ", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetReplenishedMaterialCode_RPRT(GetReplenishedMaterialCode_RPRTModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    {"AccountID" , obj.AccountID },
                    {"TenantID" , obj.TenantID },
                    {"prefix" , obj.prefix }

                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "SP_USP_FalconServices_GetReplenishedMaterialCode", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetMaterialsForMaterialTracking_RPRT(GetMaterialsForMaterialTracking_RPRTModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    {"TenantID" , obj.TenantID },
                    {"prefix" , obj.prefix }

                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_Service_INL_LoadMaterialsData", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetMaterialsForBinReplenishment_RPRT(GetMaterialsForBinReplenishment_RPRTModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    {"TenantID" , obj.TenantID },
                    {"AccountID" , obj.AccountID },
                    {"prefix" , obj.prefix }

                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "SP_USP_FalconServices_LoadItemNumbersForBinReplenishmentReportNew ", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetPOInvoiceNumbers_RPRT(GetPOInvoiceNumbers_RPRTModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {

                    {"POHeaderID" , obj.POHeaderID },
                    {"prefix" , obj.prefix }

                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_Service_INL_LoadPOInvoiceNumbers", sqlParams).ConfigureAwait(false);
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
        // 16-03-23

        public async Task<Payload<string>> GetMCodeforGRN_RPRT(GetMCodeforGRN_RPRTModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {

                    {"TenantID" , obj.TenantID },
                    {"prefix" , obj.prefix }

                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_Service_INL_LoadMcodeforGRN", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetUsers_RPRT(GetUsers_RPRTModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {

                    {"TenantID" , obj.TenantID },
                    {"WarehouseID" , obj.WarehouseID },
                    {"LogAccountID" , obj.LogAccountID },
                    {"flag" , obj.flag },
                    {"Prefix" , obj.Prefix },


                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_LoadUserDropDown1", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetMaterialsForExpiryDate_RPRT(GetMaterialsForExpiryDate_RPRTModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {

                    {"TenantID" , obj.TenantID },
                    {"Prefix" , obj.Prefix }


                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_GetMaterialsForExpiryDateReport", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetOperatorSummaryUsers_RPRT(GetOperatorSummaryUsers_RPRTModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    {"Prefix" , obj.Prefix }

                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_INL_txtOperatorNamesDetails", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetOperatorSummaryOBDNumbers_RPRT(GetOperatorSummaryOBDNumbers_RPRTModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    {"Prefix" , obj.Prefix },
                    {"UserID" , obj.UserID }

                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "SP_GET_OBDNUMBERS", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetOperatorSummarySONumbers_RPRT(GetOperatorSummarySONumbers_RPRTModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    {"prefix" , obj.prefix },
                    {"OBDID" , obj.OBDID }

                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_INL_GetSOListUnderOBD ", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetMCodeforExpiredMaterial_RPRT(GetMCodeforExpiredMaterial_RPRTModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    {"prefix" , obj.prefix },
                    {"TenantID" , obj.TenantID }

                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_INL_getPartNOForJoblist  ", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetCycleCountNames_RPRT(GetCycleCountNames_RPRTModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    {"AccountID" , obj.AccountID },
                    {"WarehouseID" , obj.WarehouseID },
                    {"CCM_MST_CycleCount_ID" , obj.CCM_MST_CycleCount_ID },
                    {"prefix" , obj.prefix }

                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "SP_USP_FalconService_GetCCNames", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetCycleCountCodes_RPRT(GetCycleCountCodes_RPRTModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {

                    {"CCM_CNF_AccountCycleCount_ID" , obj.CCM_CNF_AccountCycleCount_ID },
                    {"prefix" , obj.prefix }

                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "SP_USP_FalconService_GetCycleCountCodes", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetMaterialsForCycleCount_RPRT(GetMaterialsForCycleCount_RPRTModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {

                    {"UserID" , obj.UserID },
                    {"WarehouseId" , obj.WarehouseId },
                    {"prefix" , obj.prefix }

                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_Service_INL_LoadMaterialsForCycleCountReport", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetLocationManager_Supplier(GetLocationManager_Supplier obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    {"@TenantID" , obj.TenantID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_MST_LM_IL_GetSuppliersList", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetLocationManager_TenantList()
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {

                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_MST_LM_IL_GetTenantList", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetRacks_BulkModify(GetRacks_BulkModifyModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    {"@ZoneID" , items.ZoneID}
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_MST_LM_IL_GetRacks", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetColumns_Levels_BulkModify(GetColumns_Levels_BulkModifyModel items)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            Payload<string> response = new Payload<string>();
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();

                string Query = "EXEC [dbo].[USP_MST_LM_IL_GetColumnAndLevel] @rackCode=" + "'" + items.RackCode + "'" + " , @ZoneID=" + items.ZoneID + " ";
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

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<Payload<string>> GetBins_BulkModify(GetBins_BulkModifyModel items)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            Payload<string> response = new Payload<string>();
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();

                string Query = "EXEC [dbo].[USP_MST_LM_IL_GetBins] @rackCode=" + "'" + items.RackCode + "'" + " ,@ColCode=" + "'" + items.ColumnCode + "'" + " ,@LevCode=" + "'" + items.LevelCode + "'" + " ,@ZoneID=" + items.ZoneID + "";
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

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<Payload<string>> GetAvlBatchQty(GetAvlBatchQtyModel items)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            Payload<string> response = new Payload<string>();
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();

                string Query = "EXEC [dbo].[USP_GetAvlBatchQty] @prefix = '" + items.prefix + "',@whid = " + items.whid + ",@pendingQty = " + items.pendingQty + ",@SODetailsID=" + items.SODetailsID + "";
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
        public async Task<Payload<string>> GetMCodesList(GetMCodesListModel MCodesListModel)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    {"@AccountID" , MCodesListModel.AccountID},
                    { "@prefix" , MCodesListModel.prefix },
                    { "@TenantID" , MCodesListModel.TenantId },
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "SP_Get_MCodesUnderAccount", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetLabels(GetLabelModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {

                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "USP_FC_COM_IL_LoadLabelSizes", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> OBDWareHouse(OBDWareHouseModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    {"@OutboundID" , items.OutboundID}
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_OBDWareHouseDropDown", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetLoadHHTypes(GetLoadHHTypesModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {

                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_OBDHandlingTypeDropDown", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetMCodes_DeliveryPicNote(GetMCodes_DeliveryPicNoteModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    { "@prefix", items.prefix },
                    { "@OutboundID", items.OutboundID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "SP_USP_FalconServices_LoadDPNoteMCodeOEMData", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetSupplierList(GetSupplierListModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    {"MaterialMasterID" , items.MaterialMasterID}
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_MST_IM_IL_rsGetSupplierList", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetPO_OrderType(GetPO_OrderTypeModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    { "prefix" , items.prefix }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_Service_INL_LoadPOTypes", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetSO_OrderType(GetSO_OrderTypeModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    { "prefix" , items.prefix }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_MST_DropSoType", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetSKUList(GetSKUListModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                    { "@prefix" , items.prefix },
                    { "@TenantID" , items.TenantID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "SP_USP_RPT_GetSKUData", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetCC_ShopFloor_Locations()
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> { };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "GetLocations_UnderShopFloor", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetCC_Capture_Containers(GetCC_Capture_ContainersModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    { "@prefix" , items.prefix }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "GetCC_Capture_Containers", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetCC_Capture_Materials(GetCC_Capture_MaterialsModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    { "@prefix" , items.prefix }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "GetCC_Capture_Materials", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> Get_PrinterIPList()
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {

                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_FC_COM_IL_DDLLoadPrinters", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> LoadSONumbers(LoadSONumbersModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    {"@CustomerID" , items.CustomerId},
                    { "@Prefix" , items.prefix },
                    { "@TenantID" , items.TenantID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_GetSONumbersForOBD", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> LoadCustomerPONumbers(LoadCustomerPONumbersModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    { "@SOHeaderID" , items.SOHeaderID},
                    { "@prefix" , items.prefix },
                    { "@Type" , 1 },
                    { "@InvoiceNo" , DBLibrary.SQuote(items.InvoiceNo) }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "sp_OBD_GetDataForCustomerPo", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetCusPOInvoiceNoList(GetCusPOInvoiceNoListModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    { "@CustomerPOID" , items.CustomerPOID},
                    { "@InvoiceNo" , items.InvoiceNo },
                    { "@Type" , 2 },
                    { "@ActualCustomerPOID" , items.CustomerPOID },
                    { "@prefix" , items.prefix }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "sp_OBD_GetDataForCustomerPo", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> Delete_DeliveryDoc_LineItems(Delete_DeliveryDoc_LineItemsModel items)
        {
            Payload<string> response = new Payload<string>();
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                      {"@SOHeaderID",items.SOHeaderID}
                };
                var result = await DbUtility.GetjsonData(this.ConnectionString, "SP_USP_obd_updateSoheaderID", sqlParams).ConfigureAwait(false);
                JArray jArray = JArray.Parse(result);
                int n = (int)jArray[0]["N"];

                if (n == 1)
                {
                    response.Result = "1";// Successfully deleted the selected line items
                }
                else
                {
                    response.Result = "-1"; // Error while deleting selected line items
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

        public async Task<Payload<string>> GetBusinessTypes(GetSoTypeModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    {"@Prefix",obj.prefix},
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_MST_GetBusinessType", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetBillTypes()
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_LoadBillTypeDropDown", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetGrades(ItemsModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    { "@prefix" , obj.prefix },
                    { "@MaterialmasterId" ,obj.MaterialMasterID}
                
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_GetGrades", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetCustomers(Getcustomerobj obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>{
                 {"Prefix",obj.prefix==null?"":obj.prefix},
                  {"TenantID",obj.TenantID},
                  {"LoggedTenantID",obj.LoggedTenantID}
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_LoadCustomers", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetSecondLabelInputs()
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>{};
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "USP_GetSecondLabelInputs", sqlParams).ConfigureAwait(false);
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

        public async Task<Payload<string>> GetInOutVehicleNos(Getcustomerobj obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>{
                    { "@IsInbound",obj.IsInbound }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "SP_GET_InOutVehicleNo", sqlParams).ConfigureAwait(false);
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
        public async Task<Payload<string>> GetOBDStatusList()
        {
            
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>{
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "SP_GetOBDStatusList", sqlParams).ConfigureAwait(false);
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


















