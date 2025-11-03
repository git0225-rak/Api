using Simpolo_Endpoint.DAO.interfaces;
using Simpolo_Endpoint.DBUtil;
using Simpolo_Endpoint.Models;
using Simpolo_Endpoint.ModelsLibrary;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Simpolo_Endpoint.Models.POSOModel;

namespace Simpolo_Endpoint.DAO.Services
{
    public class POSOService : AppDBService, IPOSO
    {
        public POSOService(IOptions<AppSettings> appSettings) : base(appSettings)
        {

        }
        public async Task<Payload<string>> GetSOList(SOListModel sOHeaderList)
        {
            Payload<string> response = new Payload<string>();
            try
            {

                var sqlParams = new Dictionary<string, object> {
                    {"@SONumber",sOHeaderList.SONumber },
                    {"@SOStatusID",sOHeaderList.SOStatusID },
                    {"@Tenant",sOHeaderList.Tenant },
                    {"@AccountID_New",sOHeaderList.AccountID_New },
                    {"@TenantID_New",sOHeaderList.TenantID_New },
                    {"@UserTypeID_New",sOHeaderList.UserTypeID_New },
                    {"@UserID_New",sOHeaderList.UserID_New },
                    {"@fromdate",sOHeaderList.fromdate },
                    {"@Todate",sOHeaderList.Todate },
                    {"@SOTypeID",sOHeaderList.SOTypeID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "sp_ORD_SOHeaderList", sqlParams).ConfigureAwait(false);
            }
            catch (SqlException Sqlex)
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
        public async Task<Payload<string>> UpsertSoHeaderDetails(UpdateSOModel updateSO)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            Payload<string> response = new Payload<string>();
            try
            {

                StringBuilder sCmdPilotCr = new StringBuilder();

                sCmdPilotCr.AppendLine("DECLARE @UpdateSOHeaderID int EXEC");
                sCmdPilotCr.AppendLine("[dbo].[sp_ORD_UpsertSOHeader]");

                sCmdPilotCr.AppendLine("@SOHeaderID =" + updateSO.SOHeaderID + ",");
                sCmdPilotCr.AppendLine("@SONumber =" + DBUtil.DBLibrary.SQuote(updateSO.SONumber) + ",");
                sCmdPilotCr.AppendLine("@AccountID =" + updateSO.AccountID + ",");
                sCmdPilotCr.AppendLine("@TenantID =" + updateSO.TenantID + ",");
                sCmdPilotCr.AppendLine("@CustomerID =" + updateSO.CustomerID + ",");
                //sCmdPilotCr.AppendLine("@ProjectCode =" + DBUtil.DBLibrary.SQuote(updateSO.ProjectCode) :("NULL") +",");
                //  sCmdPilotCr.AppendLine("@ShipmentAddress1 =" + DBUtil.DBLibrary.SQuote(updateSO.ShipmentAddress1) + ",");
                sCmdPilotCr.Append("@ProjectCode=" + (updateSO.ProjectCode != "" ? DBUtil.DBLibrary.SQuote(updateSO.ProjectCode) : "NULL"));
                sCmdPilotCr.Append(",@ShipmentAddress1=" + (updateSO.ShipmentAddress1 != "" ? DBUtil.DBLibrary.SQuote(updateSO.ShipmentAddress1) : "NULL"));
                sCmdPilotCr.Append(",@ShipmentAddress2=" + (updateSO.ShipmentAddress2 != "" ? DBUtil.DBLibrary.SQuote(updateSO.ShipmentAddress2) : "NULL"));
                sCmdPilotCr.Append(",@City=" + (updateSO.City != "" ? DBUtil.DBLibrary.SQuote(updateSO.City) : "NULL"));
                sCmdPilotCr.Append(",@Province=" + (updateSO.Province != "" ? DBUtil.DBLibrary.SQuote(updateSO.Province) : "NULL"));
                sCmdPilotCr.Append(",@CountryMasterID=" + (updateSO.CountryMasterID != 0 ? (updateSO.CountryMasterID) : "NULL"));
                sCmdPilotCr.Append(",@Zip=" + (updateSO.Zip != "" ? DBUtil.DBLibrary.SQuote(updateSO.Zip) : "NULL"));
                sCmdPilotCr.Append(",@Mobile=" + (updateSO.Mobile != "" ? DBUtil.DBLibrary.SQuote(updateSO.Mobile) : "NULL"));
                sCmdPilotCr.Append(",@CurrencyID=" + (updateSO.CurrencyID != 0 ? (updateSO.CurrencyID) : "NULL"));
                sCmdPilotCr.Append(",@DepartmentID=null");
                sCmdPilotCr.Append(",@DivisionID=null");
                sCmdPilotCr.Append(",@ShipmentCharges=" + (updateSO.ShipmentCharges != "" ? DBUtil.DBLibrary.SQuote(updateSO.ShipmentCharges) : "NULL"));
                sCmdPilotCr.Append(",@FreightCompanyID=" + (updateSO.FreightCompanyID != 0 ? (updateSO.FreightCompanyID) : "NULL" + ","));
                sCmdPilotCr.AppendLine("@SOTypeID =" + updateSO.SOTypeID + ",");
                sCmdPilotCr.AppendLine("@RequestedBy  = " + updateSO.RequestedBy + ",");
                sCmdPilotCr.AppendLine("@SODate = " + DBUtil.DBLibrary.SQuote(updateSO.SODate) + ",");
                sCmdPilotCr.AppendLine("@DeliveryDueDate  = " + DBUtil.DBLibrary.SQuote(updateSO.DeliveryDueDate) + ",");
                sCmdPilotCr.AppendLine("@RequirementNumber  = " + DBUtil.DBLibrary.SQuote(updateSO.RequirementNumber) + ",");
                sCmdPilotCr.AppendLine("@CreatedBy  = " + updateSO.CreatedBy + ",");
                sCmdPilotCr.AppendLine("@GEN_MST_Address_ID  =null");
                sCmdPilotCr.Append(",@Remarks=" + (updateSO.Remarks != "" ? DBUtil.DBLibrary.SQuote(updateSO.Remarks) : "NULL") + ",");
                sCmdPilotCr.AppendLine("@LastModifiedBy  = " + updateSO.LastModifiedBy + ",");
                sCmdPilotCr.Append("@SOTax=" + (updateSO.SOTax != "" ? DBUtil.DBLibrary.SQuote(updateSO.SOTax) : "NULL"));
                sCmdPilotCr.Append(",@NetValue=" + (updateSO.NetValue != "" ? DBUtil.DBLibrary.SQuote(updateSO.NetValue) : "NULL"));
                sCmdPilotCr.Append(",@GrossValue=" + (updateSO.GrossValue != "" ? DBUtil.DBLibrary.SQuote(updateSO.GrossValue) : "NULL" + ","));
                sCmdPilotCr.AppendLine("@IsActive  = " + updateSO.IsActive + ",");
                sCmdPilotCr.AppendLine("@IsDeleted  = " + updateSO.IsDeleted + ",");
                sCmdPilotCr.AppendLine("@SOStatusID  = " + updateSO.SOStatusID + ",");
                sCmdPilotCr.AppendLine("@NewSOHeaderID=@UpdateSOHeaderID OUTPUT;");
                sCmdPilotCr.AppendLine("select @UpdateSOHeaderID as N;");

                string UpdateSO = sCmdPilotCr.ToString();

                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                int UpdateSOHeaderID = DbUtility.GetSqlN(UpdateSO, ConnectionString);
                if (UpdateSOHeaderID > 0)
                {
                    response.Result = UpdateSOHeaderID.ToString();     //Successfully Saved
                    return response;
                }
                else
                {
                    response.Result = ("-1");    //alredy exist
                }

            }
            catch (SqlException ex)
            {
                if (ex.Message.StartsWith("Violation of UNIQUE KEY constraint 'UK_ORD_SONumber'"))
                {
                    //test case ID (TC_045)
                    //if exist same WO Number then

                    response.Result = ("Error duplicate SO Number generated, regenerate SO Number");
                    return response;
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

        public async Task<Payload<string>> GetCustomerPODetailsList(CustomerPOListModel customerPOList)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                    {"@SOHeaderID",customerPOList.SOHeaderID }

                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "sp_ORD_CustomerPOList", sqlParams).ConfigureAwait(false);
            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }

        public async Task<Payload<string>> UpsertCustomerPO(UpsertCustomerPOModel upsertCustomerPO)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                string sp = "exec[dbo].[sp_ORD_UpsertCustomerPO] @SOHeaderID = " + upsertCustomerPO.SOHeaderID + ",@CustomerPOID = " + (upsertCustomerPO.CustomerPOID) + ",@CustPONumber = " + (upsertCustomerPO.CustPONumber) + ",@CustPODate = " + DBUtil.DBLibrary.SQuote(Convert.ToString(upsertCustomerPO.CustPODate))
                + ",@ExchangeRate = " + upsertCustomerPO.ExchangeRate + ",@CustPOValue = " + (upsertCustomerPO.CustPOValue) + ",@CreatedBy = " + (upsertCustomerPO.CreatedBy) + ",@CurrencyID = " + (upsertCustomerPO.CurrencyID != 0 && upsertCustomerPO.CurrencyID != 0 ? upsertCustomerPO.CurrencyID : "NULL") + "";
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDatasetINQ(this.ConnectionString, sp).ConfigureAwait(false);
                if (response.Result == "{}" || response.Result == null)
                {
                    response.Result = ("1");     //Successfully Saved
                    return response;
                }
                else
                {
                    response.Result = ("2");    //alredy exist
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

        public async Task<Payload<string>> GetMaterialSODetailsList(MaterialSODetailsListModel materialSODetailsList)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                    {"@SOHeaderID",materialSODetailsList.SOHeaderID },
                    {"@MCode",materialSODetailsList.MCode }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "sp_ORD_SODetailsList", sqlParams).ConfigureAwait(false);
            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }
        //po
        public async Task<Payload<string>> GetPOHeaderList(POHeaderListInputModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    { "@PONumber", items.PONumber },
                    { "@POStatusID", items.POStatusID },
                    { "@TenantID", items.TenantID },
                    { "@Tenant", items.Tenant },
                    { "@AccountID_New", items.AccountID },
                    { "@UserTypeID_New", items.UserTypeID },
                    { "@TenantID_New", items.TenantID },
                    { "@UserID_New", items.UserID },
                    { "@fromdate", items.fromdate },
                    { "@Todate", items.Todate }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "sp_ORD_POHeaderList", sqlParams).ConfigureAwait(false);
            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }

        public async Task<Payload<string>> GetSoHeaderDetails(GetSoHeaderDetailsModel getSoHeaderDetails)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                    {"@SOHeaderID",getSoHeaderDetails.SOHeaderID },
                    {"@AccountID_New",getSoHeaderDetails.AccountID_New }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "sp_ORD_SOHeaderDetails", sqlParams).ConfigureAwait(false);
            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }

        public async Task<Payload<string>> DeleteCustomerPO(DeleteCustomerPOModel deleteCustomerPO)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                    {"@CustomerPOIDs",deleteCustomerPO.CustomerPOIDs },
                    {"@UpdatedBy",deleteCustomerPO.UpdatedBy }

                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "sp_ORD_DeleteCustomerPO", sqlParams).ConfigureAwait(false);
                if (response.Result == "[]" || response.Result == null)
                {
                    response.Result = ("1");     //Successfully deleted
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


        //Update Material Details

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<Payload<string>> UpsertMaterialSODetails(UpsertMaterialSODetailsModel upsertMaterialSODetails)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            Payload<string> response = new Payload<string>();
            try
            {
                StringBuilder sCmdPilotCr = new StringBuilder();

                sCmdPilotCr.AppendLine("EXEC dbo.sp_ORD_UpsertSODetails");
                sCmdPilotCr.AppendLine("@SOHeaderID =" + upsertMaterialSODetails.SOHeaderID + ",");
                sCmdPilotCr.AppendLine("@SODetailsID =" + upsertMaterialSODetails.SODetailsID + ",");
                sCmdPilotCr.AppendLine("@LineNumber=" + upsertMaterialSODetails.LineNumber + ",");
                sCmdPilotCr.AppendLine("@MaterialMasterID=" + upsertMaterialSODetails.MaterialMasterID + ",");
                sCmdPilotCr.AppendLine("@MaterialMaster_SUoMID =" + upsertMaterialSODetails.MaterialMaster_SUoMID + ",");
                sCmdPilotCr.Append("@KitPlannerID=" + (!string.IsNullOrEmpty(upsertMaterialSODetails.KitPlannerID) ? "'" + upsertMaterialSODetails.KitPlannerID + "'" : "NULL"));
                sCmdPilotCr.AppendLine(",@SOQuantity=" + upsertMaterialSODetails.SOQuantity + ",");
                sCmdPilotCr.Append("@UnitPrice = " + (!string.IsNullOrEmpty(upsertMaterialSODetails.UnitPrice) ? "'" + upsertMaterialSODetails.UnitPrice + "'" : "NULL"));
                sCmdPilotCr.AppendLine(",@CreatedBy = " + upsertMaterialSODetails.CreatedBy + ",");
                sCmdPilotCr.AppendLine("@MaterialMaster_CustPOUoMID = " + upsertMaterialSODetails.MaterialMaster_CustPOUoMID + ",");
                sCmdPilotCr.AppendLine("@CustomerPOID = " + upsertMaterialSODetails.CustomerPOID + ",");
                sCmdPilotCr.AppendLine("@CustPOQuantity = " + upsertMaterialSODetails.CustPOQuantity + ",");
                sCmdPilotCr.Append("@SODiscountInPercentage = " + (!string.IsNullOrEmpty(upsertMaterialSODetails.SODiscountInPercentage) ? "'" + upsertMaterialSODetails.SODiscountInPercentage + "'" : "NULL"));
                sCmdPilotCr.Append(",@VATCode = " + (!string.IsNullOrEmpty(upsertMaterialSODetails.VATCode) ? "'" + upsertMaterialSODetails.VATCode + "'" : "NULL"));
                sCmdPilotCr.Append(",@InvoiceNo = " + (!string.IsNullOrEmpty(upsertMaterialSODetails.InvoiceNo) ? "'" + upsertMaterialSODetails.InvoiceNo + "'" : "NULL"));
                sCmdPilotCr.AppendLine(",@StoreParameterIDS = " + DBUtil.DBLibrary.SQuote(upsertMaterialSODetails.StoreParameterIDS));
                sCmdPilotCr.Append(",@StoreParameterValues = " + DBUtil.DBLibrary.SQuote(upsertMaterialSODetails.StoreParameterValues));
                sCmdPilotCr.AppendLine(",@StorageLocationId = '" + upsertMaterialSODetails.StorageLocationId + "',");
                sCmdPilotCr.Append("@GEN_MST_Address_ID = " + (upsertMaterialSODetails.GEN_MST_Address_ID != "" ? (upsertMaterialSODetails.GEN_MST_Address_ID) : "NULL"));
                sCmdPilotCr.Append(",@MfgDate = " + (!string.IsNullOrEmpty(upsertMaterialSODetails.MfgDate) ? "'" + upsertMaterialSODetails.MfgDate + "'" : "NULL"));
                sCmdPilotCr.Append(",@InvoiceDate = " + DBUtil.DBLibrary.SQuote(Convert.ToString(upsertMaterialSODetails.InvoiceDate)));
                sCmdPilotCr.Append(",@ExpDate = " + (!string.IsNullOrEmpty(upsertMaterialSODetails.ExpDate) ? "'" + upsertMaterialSODetails.ExpDate + "'" : "NULL"));
                sCmdPilotCr.AppendLine(",@SerialNo = '" + upsertMaterialSODetails.SerialNo + "',");
                sCmdPilotCr.AppendLine("@BatchNo = '" + upsertMaterialSODetails.BatchNo + "',");
                sCmdPilotCr.AppendLine("@ProjectRefNo = '" + upsertMaterialSODetails.ProjectRefNo + "'");

                string SOHeaderID = sCmdPilotCr.ToString();

                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                int SODetailsID = DbUtility.GetSqlN(SOHeaderID, ConnectionString);
                if (response.Result == "[]" || response.Result == "{}")
                {
                    response.Result = ("1");     //Successfully Saved
                    return response;
                }
                else
                {
                    response.Result = ("2");    //alredy exist
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
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_GET_CCM_TRN_CycleCounts_Ascending", sqlParams).ConfigureAwait(false);
            }
            catch (SqlException Sqlex)
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
        public async Task<Payload<string>> SavePOMaterialDetailsData(POMaterialDetailsDataModel items)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            Payload<string> response = new Payload<string>();
            try
            {

                StringBuilder sCmdUpdateSupplierInvoiceMaterialList = new StringBuilder();
                sCmdUpdateSupplierInvoiceMaterialList.AppendLine("exec [dbo].[sp_ORD_UpsertPODetails]");
                sCmdUpdateSupplierInvoiceMaterialList.AppendLine("@POHeaderID = " + items.POHeaderID + ",");
                sCmdUpdateSupplierInvoiceMaterialList.AppendLine("@PODetailsID = " + items.PODetailsID + ",");
                sCmdUpdateSupplierInvoiceMaterialList.AppendLine("@LineNumber =" + items.LineNumber + ",");
                sCmdUpdateSupplierInvoiceMaterialList.AppendLine("@MaterialMasterID =" + items.MaterialMasterID + ",");
                sCmdUpdateSupplierInvoiceMaterialList.AppendLine("@POQuantity =" + items.POQuantity + ",");
                sCmdUpdateSupplierInvoiceMaterialList.AppendLine("@MaterialMaster_PUoMID = " + items.MaterialMaster_PUoMID + ",");
                sCmdUpdateSupplierInvoiceMaterialList.AppendLine("@KitPlannerID =" + (String.IsNullOrEmpty(items.KitPlannerID) ? "NULL" : items.KitPlannerID) + ",");
                sCmdUpdateSupplierInvoiceMaterialList.AppendLine("@MfgDate =" + DBLibrary.SQuote(items.MfgDate) + ",");
                sCmdUpdateSupplierInvoiceMaterialList.AppendLine("@ExpDate = " + DBLibrary.SQuote(items.ExpDate) + ",");
                sCmdUpdateSupplierInvoiceMaterialList.AppendLine("@BatchNo =" + DBLibrary.SQuote(items.BatchNo) + ",");
                sCmdUpdateSupplierInvoiceMaterialList.AppendLine("@ProjectRefNo =" + DBLibrary.SQuote(items.ProjectRefNo) + ",");
                sCmdUpdateSupplierInvoiceMaterialList.AppendLine("@SerialNo =" + DBLibrary.SQuote(items.SerialNo) + ",");
                sCmdUpdateSupplierInvoiceMaterialList.AppendLine("@CreatedBy =" + items.UserID + ",");
                sCmdUpdateSupplierInvoiceMaterialList.AppendLine("@RequirementNumber =" + DBLibrary.SQuote(items.RequirementNumber) + ",");
                sCmdUpdateSupplierInvoiceMaterialList.AppendLine("@UpdatedOn =" + DBLibrary.SQuote(DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss")) + ",");
                sCmdUpdateSupplierInvoiceMaterialList.AppendLine("@StoreParameterIDS =" + DBLibrary.SQuote(items.StoreParameterIDS) + ",");
                sCmdUpdateSupplierInvoiceMaterialList.AppendLine("@StoreParameterValues =" + DBLibrary.SQuote(items.StoreParameterValues) + "");

                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();

                int result = DbUtility.GetSqlN(sCmdUpdateSupplierInvoiceMaterialList.ToString(), ConnectionString);

                if (result == 0)
                {
                    response.Result = "1";//Successfully Updated
                    //await GetPODetailsList(items);
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

        public async Task<Payload<string>> GetPODetailsList(POMaterialDetailsDataModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    { "@MCode", items.MCode },
                    { "@POHeaderID", items.POHeaderID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "sp_ORD_PODetailsList", sqlParams).ConfigureAwait(false);//Successfully Updated
            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }

        //upsert invoice
#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<Payload<string>> UpsertSupplierInvoice(SupplierInvoiceInputModel items)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            Payload<string> response = new Payload<string>();
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                string InvoiceQuery = "Exec [dbo].[sp_ORD_UpsertSupplierInvoice] @SupplierInvoiceID=" + items.SupplierInvoiceID + ",@POHeaderID=" + items.POHeaderID + ",@InvoiceNumber='" + items.InvoiceNumber + "',@InvoiceDate='" + items.InvoiceDate + "',@CurrencyID=" + (items.CurrencyID == 0 ? "null" : items.CurrencyID) + ",@InvoiceValue='" + items.InvoiceValue + "',@NoofPackages='" + items.NoofPackages + "',@NetWeight='" + items.NetWeight + "',@GrossWeight='" + items.GrossWeight + "',@InvCountryofOriginID=" + (items.InvCountryofOriginID == 0 ? "null" : items.InvCountryofOriginID) + ",@CreatedBy='" + items.UserID + "'";
                DataSet dsinvoicedetails = DbUtility.GetDS(InvoiceQuery, ConnectionString);

                if (dsinvoicedetails.Tables[0].Rows[0]["N"].ToString() == "1")
                {
                    response.Result = "1";//Successfully Updated
                }
                else
                {
                    response.Result = "-1";//Invoice Number already exists
                }
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }


        //get Supplier Invoice List
        public async Task<Payload<string>> GetSupplierInvoice(SupplierInvoiceInputModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    { "@POHeaderID", items.POHeaderID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "sp_ORD_SupplierInvoiceList", sqlParams).ConfigureAwait(false);
            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }
        public async Task<Payload<string>> DeleteSupplierInvoice(DeleteSupplierInvoiceInputModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                   {
                       { "@SupplierInvoiceIDs", items.SupplierInvoiceID },
                       {"@UpdatedBy", items.UserID }
                   };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                var result = await DbUtility.GetjsonData(this.ConnectionString, "sp_ORD_DeleteSupplierInvoice", sqlParams).ConfigureAwait(false);
                if (result == "[]")
                {
                    response.Result = "1";//Successfully deleted the selected Supplier Invoice Details
                }
                else
                {
                    response.Result = "2";//Error while deleting
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


        //Mapping invoice to materials
        public async Task<Payload<string>> SupplerInvoiceDetailsRowUpdating(SupplerInvoiceMaterialListModel items)
        {
            var Parametername = "";
            var Isrequired = "";
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();

            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    { "@MaterialMasterID", items.MaterialMasterID }
                };

                DataTable dt = await DbUtility.GetDataTable(this.ConnectionString, "USP_MMT_MaterialMaster_MSPList", sqlParams);

                if (dt.Rows.Count != 0)
                {
                    foreach (DataRow Row1 in dt.Rows)
                    {
                        Isrequired = Row1["IsRequired"].ToString();
                        Parametername = Row1["ParameterName"].ToString();
                        if (dt != null)
                        {
                            if (Parametername == "MfgDate" && Isrequired == "True")
                            {
                                if (items.MfgDate == "")
                                {
                                    response.Result = "-1"; //Please Select Mandatory Mfg. Date
                                    return response;
                                }
                            }
                            else if (Parametername == "ExpDate" && Isrequired == "True")
                            {
                                if (items.ExpDate == "")
                                {
                                    response.Result = "-2"; //Please Enter Mandatory Exp. Date
                                    return response;
                                }
                            }
                            else if (Parametername == "BatchNo" && Isrequired == "True")
                            {
                                if (items.BatchNo == "")
                                {
                                    response.Result = "-3"; //Please Enter Mandatory Batch No
                                    return response;
                                }
                            }
                            else if (Parametername == "MRP" && Isrequired == "True")
                            {
                                if (items.MRP == 0)
                                {
                                    response.Result = "-4"; //Please Select Mandatory MRP
                                    return response;
                                }
                            }
                            else if (Parametername == "ProjectRefNo" && Isrequired == "True")
                            {
                                if (items.ProjectRefNo == "")
                                {
                                    response.Result = "-5"; //Please Select Mandatory Project RefNo
                                    return response;
                                }
                            }
                            else if (Parametername == "SerialNo" && Isrequired == "True")
                            {
                                if (items.SerialNo == "")
                                {
                                    response.Result = "-6"; //Please Select Mandatory SerialNo.
                                    return response;
                                }
                            }
                            else if (Parametername == "SupplierLot" && Isrequired == "True")
                            {
                                if (items.SupplierLot == "")
                                {
                                    response.Result = "-7"; //Please Select Mandatory SupplierLot
                                    return response;
                                }
                            }
                        }
                    }
                }

                string sp = "Exec USP_ORD_CheckInboundStatusWithPO @POHeaderID=" + items.POHeaderID + "";
                int inbStatusID = DbUtility.GetSqlN(sp, ConnectionString);

                string Query = "Exec sp_ORD_GetMaterialReceivedOrNot @SupplierInvoiceDetailsID=" + items.SupplierInvoiceDetailsID + "";
                IDataReader GetShipmentreceive = DbUtility.GetRS(Query, ConnectionString);

                if (inbStatusID > 2)
                {
                    if (GetShipmentreceive.Read())
                    {
                        response.Result = ("1");   //Invoice number is received, cannot edit
                        GetShipmentreceive.Close();
                        return response;
                    }
                }

                StringBuilder sCmdUpdateSupplierInvoiceMaterialList = new StringBuilder();
                sCmdUpdateSupplierInvoiceMaterialList.AppendLine("Declare @UPResult int exec [dbo].[sp_ORD_UpsertSupplierInvoiceDetails]");
                sCmdUpdateSupplierInvoiceMaterialList.AppendLine("EXEC[dbo].[sp_ORD_UpsertPOHeader]");
                sCmdUpdateSupplierInvoiceMaterialList.AppendLine("@SupplierInvoiceDetailsID = " + items.SupplierInvoiceDetailsID + ",");
                sCmdUpdateSupplierInvoiceMaterialList.AppendLine("@PODetailsID = " + items.PODetailsID + ",");
                sCmdUpdateSupplierInvoiceMaterialList.AppendLine("@SupplierInvoiceID =" + items.SupplierInvoiceID + ",");
                sCmdUpdateSupplierInvoiceMaterialList.AppendLine("@InvoiceQuantity =" + items.InvoiceQuantity + ",");
                sCmdUpdateSupplierInvoiceMaterialList.AppendLine("@InvDiscountInPercentage =" + items.InvDiscountInPercentage + ",");
                sCmdUpdateSupplierInvoiceMaterialList.AppendLine("@UnitPrice = " + items.UnitPrice + ",");
                sCmdUpdateSupplierInvoiceMaterialList.AppendLine("@Tax =" + items.Tax + ",");
                sCmdUpdateSupplierInvoiceMaterialList.AppendLine("@MaterialMaster_InvUoMID =" + items.MaterialMaster_InvUoMID + ",");
                sCmdUpdateSupplierInvoiceMaterialList.AppendLine("@POHeaderID = " + items.POHeaderID + ",");
                sCmdUpdateSupplierInvoiceMaterialList.AppendLine("@KitPlannerID =" + items.KitPlannerID + ",");
                sCmdUpdateSupplierInvoiceMaterialList.AppendLine("@MfgDate =" + items.MfgDate + ",");
                sCmdUpdateSupplierInvoiceMaterialList.AppendLine("@ExpDate =" + items.ExpDate + ",");
                sCmdUpdateSupplierInvoiceMaterialList.AppendLine("@BatchNo =" + items.BatchNo + ",");
                sCmdUpdateSupplierInvoiceMaterialList.AppendLine("@SerialNo =" + items.SerialNo + ",");
                sCmdUpdateSupplierInvoiceMaterialList.AppendLine("@SupplierLot =" + items.SupplierLot + ",");
                sCmdUpdateSupplierInvoiceMaterialList.AppendLine("@ProjectRefNo =" + items.ProjectRefNo + ",");
                sCmdUpdateSupplierInvoiceMaterialList.AppendLine("@UserID =" + items.UserID + ",");
                sCmdUpdateSupplierInvoiceMaterialList.AppendLine("@LineNumber =" + items.LineNumber + ",");
                sCmdUpdateSupplierInvoiceMaterialList.AppendLine("@MRP =" + items.MRP + ",");
                sCmdUpdateSupplierInvoiceMaterialList.AppendLine("@Result=@UPResult out select @UPResult as N");

                int result = DbUtility.GetSqlN(sCmdUpdateSupplierInvoiceMaterialList.ToString(), ConnectionString);
                if (result == 0)
                {
                    response.Result = "2";//Successfully Updated
                    await GetSupplerInvoiceMaterialDetails(items);
                }
                else
                {
                    response.Result = "3";//Successfully Updated, but total 'Invoice Quantity' is greater than 'PO Quantity
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

        //Mapping invoice to materials
#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<Payload<string>> UpsertSku_SupplerInvoiceDetails(UpsertSku_SupplerInvoiceDetailsModel items)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();

            Payload<string> response = new Payload<string>();
            try
            {
                string sp = "Exec USP_ORD_CheckInboundStatusWithPO @POHeaderID=" + items.POHeaderID + "";
                int inbStatusID = DbUtility.GetSqlN(sp, ConnectionString);

                string Query = "Exec sp_ORD_GetMaterialReceivedOrNot @SupplierInvoiceDetailsID=" + items.SupplierInvoiceDetailsID + "";
                IDataReader GetShipmentreceive = DbUtility.GetRS(Query, ConnectionString);

                if (inbStatusID > 2)
                {
                    if (GetShipmentreceive.Read())
                    {
                        response.Result = ("1");   //Invoice number is received, cannot edit
                        GetShipmentreceive.Close();
                        return response;
                    }
                }

                StringBuilder sCmdUpdateSupplierInvoiceMaterialList = new StringBuilder();
                sCmdUpdateSupplierInvoiceMaterialList.AppendLine("Declare @UPResult int exec [dbo].[sp_ORD_UpsertSupplierInvoiceDetails]");
                sCmdUpdateSupplierInvoiceMaterialList.AppendLine("@SupplierInvoiceDetailsID = " + items.SupplierInvoiceDetailsID + ",");
                sCmdUpdateSupplierInvoiceMaterialList.AppendLine("@PODetailsID = " + items.PODetailsID + ",");
                sCmdUpdateSupplierInvoiceMaterialList.AppendLine("@SupplierInvoiceID =" + items.SupplierInvoiceID + ",");
                sCmdUpdateSupplierInvoiceMaterialList.AppendLine("@InvoiceQuantity =" + items.InvoiceQuantity + ",");
                sCmdUpdateSupplierInvoiceMaterialList.AppendLine("@InvDiscountInPercentage =" + (String.IsNullOrEmpty(items.InvDiscountInPercentage) ? "NULL" : items.InvDiscountInPercentage) + ",");
                sCmdUpdateSupplierInvoiceMaterialList.AppendLine("@UnitPrice = " + (String.IsNullOrEmpty(items.UnitPrice) ? "NULL" : items.UnitPrice) + ",");
                sCmdUpdateSupplierInvoiceMaterialList.AppendLine("@Tax =" + (String.IsNullOrEmpty(items.Tax) ? "NULL" : items.Tax) + ",");
                sCmdUpdateSupplierInvoiceMaterialList.AppendLine("@MaterialMaster_InvUoMID =" + (items.MaterialMaster_InvUoMID == 0 ? (object)DBNull.Value : items.MaterialMaster_InvUoMID) + ",");
                sCmdUpdateSupplierInvoiceMaterialList.AppendLine("@POHeaderID = " + items.POHeaderID + ",");
                sCmdUpdateSupplierInvoiceMaterialList.AppendLine("@KitPlannerID =" + (String.IsNullOrEmpty(items.KitPlannerID) ? "NULL" : items.KitPlannerID) + ",");
                sCmdUpdateSupplierInvoiceMaterialList.AppendLine("@MfgDate =" + DBLibrary.SQuote(items.MfgDate) + ",");
                sCmdUpdateSupplierInvoiceMaterialList.AppendLine("@ExpDate =" + DBLibrary.SQuote(items.ExpDate) + ",");
                sCmdUpdateSupplierInvoiceMaterialList.AppendLine("@BatchNo =" + DBLibrary.SQuote(items.BatchNo) + ",");
                sCmdUpdateSupplierInvoiceMaterialList.AppendLine("@SerialNo =" + DBLibrary.SQuote(items.SerialNo) + ",");
                sCmdUpdateSupplierInvoiceMaterialList.AppendLine("@SupplierLot =" + DBLibrary.SQuote(items.SupplierLot) + ",");
                sCmdUpdateSupplierInvoiceMaterialList.AppendLine("@ProjectRefNo =" + DBLibrary.SQuote(items.ProjectRefNo) + ",");
                sCmdUpdateSupplierInvoiceMaterialList.AppendLine("@CreatedBy =" + items.UserID + ",");
                sCmdUpdateSupplierInvoiceMaterialList.AppendLine("@LineNumber =" + items.LineNumber + ",");
                sCmdUpdateSupplierInvoiceMaterialList.AppendLine("@MRP =" + "'" + items.MRP + "'" + ",");
                sCmdUpdateSupplierInvoiceMaterialList.AppendLine("@Result=@UPResult out select @UPResult as N");

                int result = DbUtility.GetSqlN(sCmdUpdateSupplierInvoiceMaterialList.ToString(), ConnectionString);
                if (result == 0)
                {
                    response.Result = "2";//Successfully Updated
                    //await GetSupplerInvoiceMaterialDetails(items);
                }
                else
                {
                    response.Result = "3";//Successfully Updated, but total 'Invoice Quantity' is greater than 'PO Quantity
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


        public async Task<Payload<string>> GetSku_SupplerInvoiceDetails(GetSku_SupplerInvoiceDetailsModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    { "@PODetailsID", items.PODetailsID },
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "sp_ORD_GetSupplierInvoiceDetails", sqlParams).ConfigureAwait(false);
            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }

        public async Task<Payload<string>> DeleteSku_SupplerInvoiceDetails(DeleteSku_SupplerInvoiceDetailsModel items)
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            Payload<string> response = new Payload<string>();
            try
            {
                string sp = "Exec USP_ORD_CheckInboundStatusWithPO @POHeaderID=" + items.POHeaderID + "";
                int inbStatusID = DbUtility.GetSqlN(sp, ConnectionString);
                if (inbStatusID > 2)
                {
                    response.Result = "1";//Invoice number is received, cannot delete
                }
                else
                {
                    if (items.SupplierInvoiceDetailsID != "")
                    {
                        var sqlParams = new Dictionary<string, object>
                       {
                           { "@SupplierInvoiceDetailsIDs", items.SupplierInvoiceDetailsID },
                           { "@SupplierInvoiceID", items.SupplierInvoiceID },
                           { "@LineNumber", items.LineNumber },
                           { "@UpdatedBy", items.UserID },
                           { "@POHeaderID", items.POHeaderID }
                       };

                        string result = await DbUtility.GetjsonData(this.ConnectionString, "sp_ORD_DeleteSupplierInvoiceDetails", sqlParams).ConfigureAwait(false);
                        JArray jArray = JArray.Parse(result);
                        int n = (int)jArray[0]["Column1"];
                        if (n == 0)
                        {
                            response.Result = "2";//Successfully deleted
                        }
                        else
                        {
                            response.Result = "3";//Error while deleting
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

        public async Task<Payload<string>> Getmspscheckboxs(GetmspscheckboxsModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    { "@MaterialMasterID", items.MaterialMasterID },
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "USP_MMT_MaterialMaster_MSPList", sqlParams).ConfigureAwait(false);
            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }
        public async Task<Payload<string>> GetSupplerInvoiceMaterialDetails(SupplerInvoiceMaterialListModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    { "@PODetailsID", items.PODetailsID },
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "sp_ORD_GetSupplierInvoiceDetails", sqlParams).ConfigureAwait(false);
            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }
        public async Task<Payload<string>> DeleteSupplerInvoiceDetails(SupplerInvoiceMaterialListModel items)
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            Payload<string> response = new Payload<string>();
            try
            {
                string sp = "Exec USP_ORD_CheckInboundStatusWithPO @POHeaderID=" + items.POHeaderID + "";
                int inbStatusID = DbUtility.GetSqlN(sp, ConnectionString);
                if (inbStatusID > 2)
                {
                    response.Result = "1";//Invoice number is received, cannot delete
                }
                else
                {
                    if (items.SupplierInvoiceDetailsID != 0)
                    {
                        string Query = "Exec sp_ORD_DeleteSupplierInvoiceDetails @SupplierInvoiceDetailsIDs=" + items.SupplierInvoiceDetailsID + ",@SupplierInvoiceID=" + items.SupplierInvoiceID + ",@POHeaderID=" + items.POHeaderID + ",@UpdatedBy=" + items.UserID + ",@LineNumber=" + items.LineNumber + "";
                        var result = DbUtility.GetSqlN(Query, ConnectionString);
                        if (result == 0)
                        {
                            response.Result = "2";//Successfully deleted
                            await GetSupplerInvoiceMaterialDetails(items);
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
        public async Task<Payload<string>> GetPOList(POHeaderListInputModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    { "@PONumber", items.PONumber },
                    { "@POStatusID", items.POStatusID },
                    { "@TenantID", items.TenantID },
                    { "@Tenant","'"+ items.Tenant +"'"},
                    { "@AccountID_New", items.AccountID },
                    { "@UserTypeID_New", items.UserTypeID },
                    { "@TenantID_New", items.TenantID },
                    { "@UserID_New", items.UserID },
                    { "@fromdate", items.fromdate },
                    { "@Todate", items.Todate },
                    { "@POTypeID", items.POTypeID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "sp_ORD_POHeaderList", sqlParams).ConfigureAwait(false);
            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }

        //save in podetailsinfo
#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<Payload<string>> UpsertPOHeaderData(POHeaderModel items)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            Payload<string> response = new Payload<string>();
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                string Query = "Exec sp_ORD_GetMaterialReceivedOrNot @POHeaderID=" + items.POHeaderID + "";
                var result = DbUtility.GetSqlN(Query, ConnectionString);
                if (result != 0)
                {
                    response.Result = "-3";     //PO is mapped to an inbound, Cannot update
                    return response;
                }
                else
                {
                    StringBuilder sCmdPoHeader = new StringBuilder();
                    sCmdPoHeader.AppendLine("DECLARE @UpdatePOHeaderID int");
                    sCmdPoHeader.AppendLine("EXEC[dbo].[sp_ORD_UpsertPOHeader]");
                    sCmdPoHeader.AppendLine("@POHeaderID = " + items.POHeaderID + ",");
                    sCmdPoHeader.AppendLine("@PONumber = " + DBLibrary.SQuote(items.PONumber) + ",");
                    sCmdPoHeader.AppendLine("@PODate =" + DBLibrary.SQuote(items.PODate) + ",");
                    sCmdPoHeader.AppendLine("@SupplierID =" + items.SupplierID + ",");
                    sCmdPoHeader.AppendLine("@DateRequested =" + (String.IsNullOrEmpty(items.DateRequested) ? "null" + "," : DBLibrary.SQuote(items.DateRequested) + ","));
                    sCmdPoHeader.AppendLine("@DateDue =" + (String.IsNullOrEmpty(items.DateDue) ? "null" + "," : DBLibrary.SQuote(items.DateDue) + ","));
                    sCmdPoHeader.AppendLine("@DepartmentID =" + "null" + ",");
                    sCmdPoHeader.AppendLine("@CreatedBy =" + items.UserID + ",");
                    sCmdPoHeader.AppendLine("@DivisionID = " + "null" + ",");
                    sCmdPoHeader.AppendLine("@RequestedBy =" + items.RequestedBy + ",");
                    sCmdPoHeader.AppendLine("@LastModifiedBy =" + items.LastModifiedBy + ",");
                    sCmdPoHeader.AppendLine("@TotalValue =" + items.TotalValue + ",");
                    sCmdPoHeader.AppendLine("@CurrencyID =" + (String.IsNullOrEmpty(items.CurrencyID) ? "null" + "," : DBLibrary.SQuote(items.CurrencyID) + ","));
                    sCmdPoHeader.AppendLine("@ExchangeRate =" + items.ExchangeRate + ",");
                    sCmdPoHeader.AppendLine("@Instructions =" + DBLibrary.SQuote(items.Instructions) + ",");
                    sCmdPoHeader.AppendLine("@POStatusID =" + items.POStatusID + ",");
                    sCmdPoHeader.AppendLine("@POTypeID =" + items.POTypeID + ",");
                    sCmdPoHeader.AppendLine("@IsActive =" + items.IsActive + ",");
                    sCmdPoHeader.AppendLine("@IsDeleted =" + items.IsDeleted + ",");
                    sCmdPoHeader.AppendLine("@UpdatedOn =" + DBLibrary.SQuote(items.UpdatedOn) + ",");
                    sCmdPoHeader.AppendLine("@Remarks =" + DBLibrary.SQuote(items.Remarks) + ",");
                    sCmdPoHeader.AppendLine("@POTax =" + items.POTax + ",");
                    sCmdPoHeader.AppendLine("@AccountID =" + items.AccountID + ",");
                    sCmdPoHeader.AppendLine("@TenantID =" + items.TenantID + ",");
                    sCmdPoHeader.AppendLine("@NewPOHeaderID = @UpdatePOHeaderID OUTPUT Select @UpdatePOHeaderID as N");

                    int POHeaderID = DbUtility.GetSqlN(sCmdPoHeader.ToString(), ConnectionString);
                    if (POHeaderID != 0)
                    {
                        response.Result = POHeaderID.ToString();//Successfully Saved
                    }
                    else
                    {
                        response.Result = "-2"; // error
                    }
                }
            }
            catch (SqlException Sqlex)
            {
                if (Sqlex.Message.StartsWith("Cannot insert duplicate key row in object 'dbo.ORD_POHeader'"))
                {
                    response.Result = ("-1");   //Error duplicate PONumber generated, regenerate PONumber
                }
                //else
                //{
                //    response.Result = ("-2"); //Error while updating
                //}
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }


        public async Task<Payload<string>> GetPOMaterialDetailsList(GetPOMaterialDetailsDataModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    { "@MCode", items.MCode },
                    { "@POHeaderID", items.POHeaderID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "sp_ORD_PODetailsList", sqlParams).ConfigureAwait(false);//Successfully Updated
            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }

        public async Task<Payload<string>> GetPOHeaderDetails(GetPOHeaderDetailsModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    { "@POHeaderID", items.POHeaderID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "sp_ORD_GetPOHeaderDetails ", sqlParams).ConfigureAwait(false);
            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }


        public async Task<Payload<string>> DownloadASNIO(DownloadASNIOModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                string Sp_Set = "[dbo].[USP_MST_POStatusDrop]";
                var sqlParams = new Dictionary<string, object>  {

                    { "@POHeaderID" , items.Poheaderid }


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
                    sb.Append(pair.Key + "=" + pair.Value);
                    count++;
                }
                string Append = "EXEC " + Sp_Set + " " + sb.ToString();
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                int StatusID = DbUtility.GetSqlN(Append, ConnectionString);

                if (StatusID == 1 || StatusID == 2)
                {
                    string SP = "Exec sp_POExcelExport @POHeaderID=" + items.Poheaderid;
                    var ds = await DbUtility.GetjsonDataFromDatasetINQ(this.ConnectionString, SP);
                    if (ds == null || ds == "0")
                    {
                        response.Result = "2";//No Items found to download excel

                    }
                    else
                    {
                        response.Result = ds;

                    }
                }
                else
                {
                    response.Result = "3"; //Unable to Export 

                }
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {

            }
            return response;
        }

        public async Task<Payload<string>> ImportASNCheckMandatory(ImportASNCheckMandatorModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                string Sp_Set = "[dbo].[USP_MMT_MaterialMaster_MSPListByMcode]";
                var sqlParams = new Dictionary<string, object>  {

                    { "@MCode" , "'"+ items.MaterialCode+"'" }


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
                    sb.Append(pair.Key + "=" + pair.Value);
                    count++;
                }
                string Append = "EXEC " + Sp_Set + " " + sb.ToString();
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                var data = await DbUtility.GetjsonDataFromDatasetINQ(this.ConnectionString, Append);

                response.Result = data;
            }
            catch (Exception ex)
            {
                response.Result = ex.Message;
            }
            return response;
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<Payload<string>> ImportASNIO(ImportASNIOModel items)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            Payload<string> response = new Payload<string>();
            try
            {
                DataTable Excelresults = JsonConvert.DeserializeObject<DataTable>(items.DataJson.ToString());
                string Query = "EXEC USP_GETBatchNumber @POHeaderID = '" + items.Poheaderid + "' ";

                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                string BatchNumber = DbUtility.GetSqlS(Query, ConnectionString);
                for (int i = 0; i < Excelresults.Rows.Count; i++)
                {
                    string invoicenumber = Excelresults.Rows[i]["InvoiceNo"].ToString();
                    string LineNumber = Excelresults.Rows[i]["LineNumber"].ToString();
                    string suppliercode = items.suppliercode;

                    string country = Excelresults.Rows[i]["Country"].ToString().Trim() != "" ? Excelresults.Rows[i]["Country"].ToString() : "Null";
                    string Currency = Excelresults.Rows[i]["Currency"].ToString().Trim() != "" ? Excelresults.Rows[i]["Currency"].ToString() : "Null";
                    Decimal invoicevalue = Excelresults.Rows[i]["InvoiceValue"].ToString().Trim() == "" ? 0 : Convert.ToDecimal(Excelresults.Rows[i]["InvoiceValue"].ToString());
                    int noofpackages = Excelresults.Rows[i]["NoOfPackages"].ToString().Trim() == "" ? 0 : Convert.ToInt32(Excelresults.Rows[i]["NoOfPackages"].ToString());
                    Decimal netweight = Excelresults.Rows[i]["NetWeight"].ToString().Trim() == "" ? 0 : Convert.ToDecimal(Excelresults.Rows[i]["NetWeight"].ToString());
                    Decimal grossweight = Excelresults.Rows[i]["NetWeight"].ToString().Trim() == "" ? 0 : Convert.ToDecimal(Excelresults.Rows[i]["GrossWeight"].ToString());
                    string materialcode = Excelresults.Rows[i]["PartNo"].ToString();
                    Decimal invoiceqty = Excelresults.Rows[i]["InvoiceQuantity"].ToString().Trim() == "" ? 0 : Convert.ToDecimal(Excelresults.Rows[i]["InvoiceQuantity"].ToString());
                    Decimal poqty = Excelresults.Rows[i]["POQuantity"].ToString().Trim() == "" ? 0 : Convert.ToDecimal(Excelresults.Rows[i]["POQuantity"].ToString());
                    int unitprice = Excelresults.Rows[i]["UnitPrice"].ToString().Trim() == "" ? 0 : Convert.ToInt32(Excelresults.Rows[i]["UnitPrice"].ToString());
                    string invoiceUoM = Excelresults.Rows[i]["InvoiceUoM"].ToString().Trim() != "" ? Excelresults.Rows[i]["InvoiceUoM"].ToString() : "0";
                    Decimal invoiceUoMqty = Excelresults.Rows[i]["InvoiceUoMQty"].ToString().Trim() == "" ? 0 : Convert.ToDecimal(Excelresults.Rows[i]["InvoiceUoMQty"].ToString().Trim());
                    int invoicedertailid = 0;

                    string BatchNo = BatchNumber != "" ? BatchNumber : Excelresults.Rows[i]["BatchNo"].ToString().Trim() != "" ? Excelresults.Rows[i]["BatchNo"].ToString() : null;
                    string SerialNo = Excelresults.Rows[i]["SerialNo"].ToString().Trim() != "" ? Excelresults.Rows[i]["SerialNo"].ToString() : null;
                    string ProjectRefNo = Excelresults.Rows[i]["ProjectRefNo"].ToString().Trim() != "" ? Excelresults.Rows[i]["ProjectRefNo"].ToString() : null;

                    string date = DateTime.Now.ToString("yyyy-MM-dd h:mm tt");
                    string CreatedOn = date.Split(' ')[0];
                    String mfgdate = Excelresults.Rows[i]["MfgDate"].ToString() == "" || Excelresults.Rows[i]["MfgDate"].ToString() == null ? "" : DBLibrary.SQuote((Excelresults.Rows[i]["MfgDate"]).ToString());
                    string ExpDate = Excelresults.Rows[i]["ExpDate"].ToString() == "" || Excelresults.Rows[i]["ExpDate"].ToString() == null ? "" : DBLibrary.SQuote((Excelresults.Rows[i]["ExpDate"]).ToString());
                    String InvoiceDate = DBLibrary.Formateddate((Excelresults.Rows[i]["InvoiceDate"]).ToString());
                    string MRP = Excelresults.Rows[i]["MRP"].ToString().Trim() != "" ? Excelresults.Rows[i]["MRP"].ToString() : null;
                    string SupplierLot = Excelresults.Rows[i]["SupplierLot"].ToString().Trim() != "" ? Excelresults.Rows[i]["SupplierLot"].ToString() : null;
                    // string strinvoice = "select * from ORD_PODetails where PODetailsID IN (SELECT POD.PODetailsID  FROM ORD_PODetails POD  JOIN ORD_POHeader POH ON POH.POHeaderID=POD.POHeaderID where POD.POHeaderID=" + ViewState["HeaderID"] + "  and POD.IsDeleted=0) and IsDeleted=0 and MaterialMasterID in (select MaterialMasterID from MMT_MaterialMaster where MCode='" + materialcode + "' and isdeleted=0)";

                    string strinvoice = "Exec [dbo].[USP_CHECKMCodeInPO] @POHeaderID=" + items.Poheaderid + ",@MCode='" + materialcode + "'";
                    DataSet dsinvoicedetails = DbUtility.GetDS(strinvoice, ConnectionString);
                    if (dsinvoicedetails.Tables[0].Rows.Count == 0)
                    {
                        response.Result = "Invalid MCode in excel";
                        return response;
                    }
                    string CheckMandatory = "";

                    if (SerialNo != null)
                    {
                        //<!-------------------Procedure Conversion--------------->
                        // int podetailid = DB.GetSqlN("SELECT DISTINCT POD.PODetailsID as N from MMT_MaterialMaster MM JOIN ORD_PODetails POD ON POD.MaterialMasterID = MM.MaterialMasterID JOIN ORD_POHeader oh ON oh.POHeaderID = pod.POHeaderID WHERE mm.MCode = '"+ materialcode + "' AND oh.PONumber = '"+ PONumber + "' and pod.LineNumber = "+LineNumber+" and POD.IsDeleted = 0 and POD.IsActive = 1");
                        int podetailid = DbUtility.GetSqlN("Exec [dbo].[USP_GETPODetailID] @MCode= '" + materialcode + "',@PONumber='" + items.PONumber + "',@LineNumber=" + LineNumber + "", ConnectionString);
                        if (invoicedertailid != 0)
                        {
                            //<!-------------Procedure Conversion---->
                            //int serialnocount = DB.GetSqlN("select COUNT(SerialNo) as N from ORD_SupplierInvoiceDetails where PODetailsID=" + podetailid + " and isactive=1 and isdeleted=0 and SerialNo='" + SerialNo + "' and SupplierInvoiceDetailsID =" + invoicedertailid + "");
                            int serialnocount = DbUtility.GetSqlN("Exec [dbo].[USP_SerialNOCountWithInvoiceDetail]  @PODetailsID=" + podetailid + ",@SupplierInvoiceDetailsID=" + invoicedertailid + ", @SerialNo='" + SerialNo + "'", ConnectionString);
                            if (serialnocount > 0)
                            {
                                //resetSupplierDetailsError("Serial No. already Exists in  row " + (i + 1), true);
                                //return;
                                CheckMandatory = "1";
                                return response;
                            }
                        }
                        else if (invoicedertailid == 0)
                        {
                            //<!---------------------Procedure Conversion------->
                            /// int serialnocount = DB.GetSqlN("select COUNT(SerialNo) as N from ORD_SupplierInvoiceDetails where PODetailsID=" + podetailid + " and isactive=1 and isdeleted=0 and SerialNo='" + SerialNo + "' ");
                            int serialnocount = DbUtility.GetSqlN("Exec [dbo].[USP_SerialNOCountWithInvoiceDetail]  @PODetailsID=" + podetailid + ",@SerialNo='" + SerialNo + "'", ConnectionString);
                            if (serialnocount > 0)
                            {
                                response.Result = "Serial No. already Exists";//Serial No. already Exists
                                CheckMandatory = "1";
                                return response;
                            }
                        }
                        int count = DbUtility.GetSqlN("Exec sp_GET_SERIAL_COUNT @SerialNo='" + SerialNo + "',@MCode='" + materialcode + "'", ConnectionString);
                        if (count > 0)
                        {
                            response.Result = "Serial No. already Exists"; //Serial No. already Exists
                            CheckMandatory = "1";
                            return response;
                        }

                        invoiceqty = 1;

                    }
                    DataSet ds1 = DbUtility.GetDS("Exec [dbo].[sp_GEN_CheckPOLineFulFil]  @PONumber='" + items.PONumber + "',@LineNumber='" + LineNumber + "'", ConnectionString);
                    if (ds1.Tables[0].Rows.Count != 0)
                    {

                        int fulfilpoqty = Convert.ToInt32(ds1.Tables[0].Rows[0]["POQuantity"]);
                        int actualinvQty = Convert.ToInt32(ds1.Tables[0].Rows[0]["InvQty"]);
                        int fulfilinvqty = Convert.ToInt32(ds1.Tables[0].Rows[0]["InvQty"]) + Convert.ToInt32(invoiceqty);
                        if (fulfilpoqty == actualinvQty)
                        {
                            response.Result = "LineItem already fulfilled ";
                            CheckMandatory = "1";
                            return response;
                        }
                        if (fulfilinvqty > fulfilpoqty)
                        {
                            response.Result = "Invoice Quantity exceeds POQuantity";
                            CheckMandatory = "1";
                            return response;
                        }
                    }
                    if (invoicedertailid == 0 && CheckMandatory == "")
                    {
                        upsertInvoiceDetails(invoicenumber, LineNumber, items.PONumber, InvoiceDate, suppliercode, country, Currency, invoicevalue, noofpackages, netweight, grossweight, materialcode, invoiceqty, unitprice, invoiceUoM, invoiceUoMqty, CreatedOn, items.LoginUserId, items.AccountID, 1, 1, 0, mfgdate, ExpDate, BatchNo, SerialNo, ProjectRefNo, MRP, SupplierLot);
                    }

                }

                string strinvoice1 = "Exec [dbo].[USP_GETSupplierInvoiceDetail] @POHeaderID=" + items.Poheaderid + "";

                DataSet dsinvoicedetails1 = DbUtility.GetDS(strinvoice1, ConnectionString);
                if (dsinvoicedetails1.Tables[0].Rows.Count != 0)
                {
                    response.Result = "Success";
                }
                else
                {
                    response.Result = "Unable to import excel file in supplier invoice";
                }
            }
            catch (Exception ex)
            {
                response.Result = ex.Message;
            }
            return response;
        }

        public void upsertInvoiceDetails(string InvoiceNumber, string LineNumber, string PONumber, string InvoiceDate, string SupplierCode, string CountryName, string Currency, decimal InvoiceValue, int NoofPackages, decimal NetWeight, decimal GrossWeight, string MaterialCode, decimal InvoiceQty, int UnitPrice, string InvoiceUOM, Decimal InvoiceUOMQty, string CreatedOn, int LoginUserId, int AccountID, int CreatedBy, int IsActive, int IsDeleted, string MfgDate, string ExpDate, string BatchNo, string SerialNo, string ProjectRefNo, string MRP, string SupplierLot)
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            StringBuilder sCmdupdatelineitem = new StringBuilder();
            sCmdupdatelineitem.Append(" EXEC dbo.Sp_ORD_AddInvoice ");
            sCmdupdatelineitem.Append("@InvoiceNumber=" + DBLibrary.SQuote(InvoiceNumber) + "");
            sCmdupdatelineitem.Append(",@PONumber=" + DBLibrary.SQuote(PONumber) + "");
            sCmdupdatelineitem.Append(",@LineNumber=" + Convert.ToInt32(LineNumber));
            sCmdupdatelineitem.Append(", @invoiceDate=" + DBLibrary.SQuote(InvoiceDate) + "");
            sCmdupdatelineitem.Append(",@SupplierCode=" + DBLibrary.SQuote(SupplierCode) + "");
            sCmdupdatelineitem.Append(", @CountryName=" + DBLibrary.SQuote(CountryName) + "");
            sCmdupdatelineitem.Append(",@Currency=" + DBLibrary.SQuote(Currency) + "");
            sCmdupdatelineitem.Append(", @InvoiceValue=" + InvoiceValue);
            sCmdupdatelineitem.Append(",@NoofPackages=" + NoofPackages);
            sCmdupdatelineitem.Append(",@NetWeight=" + NetWeight);
            sCmdupdatelineitem.Append(",@GrossWeight=" + GrossWeight);
            sCmdupdatelineitem.Append(",@MaterialCode=" + DBLibrary.SQuote(MaterialCode) + "");
            sCmdupdatelineitem.Append(",@InvoiceQty=" + InvoiceQty);
            sCmdupdatelineitem.Append(",@UnitPrice=" + UnitPrice);
            sCmdupdatelineitem.Append(",@InvoiceUOM=" + DBLibrary.SQuote(InvoiceUOM) + "");
            sCmdupdatelineitem.Append(",@InvoiceUOMQty=" + InvoiceUOMQty);
            sCmdupdatelineitem.Append(",@CreatedOn=" + DBLibrary.SQuote(CreatedOn) + "");
            sCmdupdatelineitem.Append(",@CreatedBy=" + LoginUserId);
            sCmdupdatelineitem.Append(",@AccountID=" + AccountID);
            sCmdupdatelineitem.Append(",@IsActive=" + IsActive);
            sCmdupdatelineitem.Append(",@IsDeleted=" + IsDeleted);
            sCmdupdatelineitem.Append(",@MfgDate=" + MfgDate + "");
            sCmdupdatelineitem.Append(",@ExpDate=" + ExpDate + "");
            sCmdupdatelineitem.Append(",@BatchNo=" + DBLibrary.SQuote(BatchNo) + "");
            sCmdupdatelineitem.Append(",@SerialNo=" + DBLibrary.SQuote(SerialNo) + "");
            sCmdupdatelineitem.Append(",@ProjectRefNo=" + DBLibrary.SQuote(ProjectRefNo) + "");
            sCmdupdatelineitem.Append(",@MRP=" + DBLibrary.SQuote(MRP) + "");
            sCmdupdatelineitem.Append(",@SupplierLot=" + DBLibrary.SQuote(SupplierLot) + "");

            DbUtility.GetDS(sCmdupdatelineitem.ToString(), ConnectionString);
        }
        public async Task<Payload<string>> SODetails_UpdateStorageLoaction(SODetails_UpdateStorageLoactionModel items)
        {
            Payload<string> response = new Payload<string>();
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                string Query = "Exec [dbo].[SP_INV_UpdateStorageLocation] @SODetailsID=" + items.SODetailsID + ",@MaterialMasterID=" + items.MaterialMasterID + ",@Id=" + items.SLOCId + "";
                DataSet dataSet = DbUtility.GetDS(Query, ConnectionString);

                if (dataSet != null || (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0))
                {
                    response.Result = "1";//Successfully Saved
                }
                else
                {
                    response.Result = "-1";// error
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

        public async Task<Payload<string>> GetKitStoreRefNo(KitModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                    {"@prefix",items.Prefix},
                    {"@TenantID",items.TenantID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "SP_GetKitRefNo", sqlParams).ConfigureAwait(false);
            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }

        public async Task<Payload<string>> GetKittingList(KitModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                    {"@KitRefNo",items.KitRefNo },
                    {"@TenantID",items.TenantID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "SP_GetKittingList", sqlParams).ConfigureAwait(false);
            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }

        public async Task<Payload<string>> GetChildItemsForKitting(KitModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>
                {
                    {"@KitHeaderID",items.KitHeaderID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonData(this.ConnectionString, "SP_GetChildItemsforKIT", sqlParams).ConfigureAwait(false);
            }
            catch (SqlException Sqlex)
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
