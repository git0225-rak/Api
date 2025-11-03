using Simpolo_Endpoint.BO;
using Simpolo_Endpoint.DAO.HHTInterface;
using Simpolo_Endpoint.DAO.Services;
using Simpolo_Endpoint.DBUtil;
using Simpolo_Endpoint.DTO;
using Simpolo_Endpoint.ModelsLibrary;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Simpolo_Endpoint.Models;
using System.Data.SqlClient;

namespace Simpolo_Endpoint.DAO.HHTServices
{
    public class HouseKeepingService : AppDBService, IHouseKeeping
    {
        private readonly WhatsAppService _whatsappservice;
        public HouseKeepingService(IOptions<AppSettings> appSettings, WhatsAppService whatsappservice) : base(appSettings)
        {
            _whatsappservice = whatsappservice;
        }

        private string _ClassCode = string.Empty;

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<DataSet> GetLiveStockData(InventoryDTO liveStock)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();

            string LiveStockData = "dbo.sp_INV_GetLiveStock_HHT @MaterialMaster=" + (liveStock.MaterialCode != "0" ? DBLibrary.SQuote(liveStock.MaterialCode) : "null") + ",@Location =" + (liveStock.LocationCode != "0" ? DBLibrary.SQuote(liveStock.LocationCode) : "null") + ",@BatchNo=" + (liveStock.BatchNo != "0" ? DBLibrary.SQuote(liveStock.BatchNo) : "null") + ",@CartonCode=" + (liveStock.ContainerCode != "" ? DBLibrary.SQuote(liveStock.ContainerCode) : "null") + ",@TenantID=" + liveStock.TenantID + ",@AccountID=" + liveStock.AccountID + ",@WarehouseID=" + liveStock.WarehouseId;
            LiveStockData += "  ,@ProjectRef = " + (liveStock.ProjectNo != "" ? DBLibrary.SQuote(liveStock.ProjectNo) : "''") + " ,@Serial = " + (liveStock.SerialNo != "" ? DBLibrary.SQuote(liveStock.SerialNo) : "''") + " ,@MFGDate = " + (liveStock.MfgDate != "" ? DBLibrary.SQuote(liveStock.MfgDate) : "''") + " ,@EXPDate = " + (liveStock.ExpDate != "" ? DBLibrary.SQuote(liveStock.ExpDate) : "''") + " ,@MRP = " + (liveStock.MRP != "" ? DBLibrary.SQuote(liveStock.MRP) : "''");

            return DbUtility.GetDS(LiveStockData, this.ConnectionString);
        }

        public async Task<DataSet> GetItemPutawaySuggestion(InventoryDTO liveStock)
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                
                string LiveStockData = "Exec dbo.USP_ITEMPUTAWAYSUGGESTION_HHT @MaterialMaster=" + (liveStock.MaterialCode != "0" ? DBLibrary.SQuote(liveStock.MaterialCode) : "null") + ",@Location =" + (liveStock.LocationCode != "0" ? DBLibrary.SQuote(liveStock.LocationCode) : "null") + ",@BatchNo=" + (liveStock.BatchNo != "0" ? DBLibrary.SQuote(liveStock.BatchNo) : "null") + ",@CartonCode=" + (liveStock.ContainerCode != "" ? DBLibrary.SQuote(liveStock.ContainerCode) : "null") + ",@TenantID=" + liveStock.TenantID + ",@AccountID=" + liveStock.AccountID + ",@WarehouseID=" + liveStock.WarehouseId;
                LiveStockData += "  ,@ProjectRef = " + (liveStock.ProjectNo != "" ? DBLibrary.SQuote(liveStock.ProjectNo) : "''") + " ,@Serial = " + (liveStock.SerialNo != "" ? DBLibrary.SQuote(liveStock.SerialNo) : "''") + " ,@MFGDate = " + (liveStock.MfgDate != "" ? DBLibrary.SQuote(liveStock.MfgDate) : "''") + " ,@EXPDate = " + (liveStock.ExpDate != "" ? DBLibrary.SQuote(liveStock.ExpDate) : "''") + " ,@MRP = " + (liveStock.MRP != "" ? DBLibrary.SQuote(liveStock.MRP) : "''");

                DataSet DS = DbUtility.GetDS(LiveStockData, this.ConnectionString);

                if (DS.Tables[0].Columns.Contains("S") && DS.Tables[0].Rows.Count > 0)
                {
                    throw new WMSExceptionMessage()
                    {
                        WMSExceptionCode = ErrorMessages.WMSExceptionCode,
                        WMSMessage = DS.Tables[0].Rows[0]["S"].ToString(),
                        ShowAsWarning = true
                    };
                }
                else
                {
                    return DS;
                }
            }
            catch (WMSExceptionMessage excp)
            {
                throw excp;
            }
            catch (Exception excp)
            {
                ExceptionData oExcpData = new ExceptionData();
                oExcpData.AddInputs("oCriteria", liveStock);

                ExceptionHandling.LogException(excp, _ClassCode + "001", oExcpData);
                throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = ErrorMessages.WMSExceptionMessage, ShowAsCriticalError = true };
            }

        }

        public async Task<DataSet> GetWarehouse(LiveStock liveStock)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            StringBuilder sbSqlString = new StringBuilder();

            sbSqlString.AppendLine("EXEC [dbo].[USP_MST_DropWH] ");
            sbSqlString.AppendLine("@AccountID = " + liveStock.AccountId + ",");
            sbSqlString.AppendLine("@TenantID = " + liveStock.TenantID + ",");
            sbSqlString.AppendLine("@UserID = " + liveStock.UserId + ",");
            sbSqlString.AppendLine("@Flag = 2");
       
            DataSet ds = DbUtility.GetDS(sbSqlString.ToString(), this.ConnectionString);

            return ds;
        }
#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<DataSet> GetTenants(LiveStock liveStock)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            StringBuilder sbSqlString = new StringBuilder();

            sbSqlString.AppendLine("EXEC [dbo].[Get_Tenants_HHT] ");
            sbSqlString.AppendLine("@AccountID = " + liveStock.AccountId + ",");
            sbSqlString.AppendLine("@WarehouseID = " + liveStock.WarehouseID + "");       

            DataSet ds = DbUtility.GetDS(sbSqlString.ToString(), this.ConnectionString);

            return ds;
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<string> CheckLoction(BO.LiveStock stock)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            string resultvalue;

            string sql = "EXEC [dbo].[CheckLocation_HHT]   @Location=" + DBLibrary.SQuote(stock.Location) + ",@AccountId=" + stock.AccountId + ",@WarehouseID=" + stock.WarehouseID + "";
            int result = DbUtility.GetSqlN(sql, ConnectionString);

            if (result == 0)
            {
                resultvalue = "Location doesn't belong to this Warehouse ";
            }
            else
            {
                resultvalue = "";
            }

            return resultvalue;
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<string> CheckTenatMaterial(string Mcode, int AccountID, string TenantName)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();

            string resultvalue;
            // int result = DB.GetSqlN("select MaterialMasterID AS N from MMT_MaterialMaster where TenantID="+0+"and Mcode="+DB.SQuote(Mcode)+" and IsActive=1 and IsDeleted=0");

            string sql = "EXEC [dbo].[CheckTenantMaterial_HHT]   @MCODE=" + DBLibrary.SQuote(Mcode) + ", @ACCOUNTID=" + AccountID + ", @TENANTNAME=" + DBLibrary.SQuote(TenantName) + "";

            int result = DbUtility.GetSqlN(sql, ConnectionString);
            if (result != 0)
            {
                resultvalue = "";
            }
            else
            {
                resultvalue = "Material doesn't belong to this Tenant";
            }
            return resultvalue;
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<string> ValidateCartonLiveStock(BO.LiveStock liveStock)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();

            string resultvalue;
            string sql = "Exec ValidateCarton_HHT @WarehouseId =" + liveStock.WarehouseID + ",@CartonCode=" + DBLibrary.SQuote(liveStock.CartonNo);
            int result = DbUtility.GetSqlN(sql, ConnectionString);
            if (result != 0)
            {
                resultvalue = "1";
            }
            else
            {
                resultvalue = "Not a valid Container.";
            }
            return resultvalue;
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<string> UpsertStock(string XML, string UserID)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                int Result;
                string _sSQL = "EXEC [dbo].[SP_Upsert_StockTake] @UserID=" + UserID + " ,@XML_StockTake ='" + XML + "'";
                DataSet _dsResults = DbUtility.GetDS(_sSQL.ToString(), this.ConnectionString);

                if (Convert.ToInt32(_dsResults.Tables[0].Rows[0][0]) > 0)
                {
                    Result = ConversionUtility.ConvertToInt(_dsResults.Tables[0].Rows[0]["N"].ToString());
                    return JsonConvert.SerializeObject(Result);
                }
                else
                {
                    return "";
                }
            }
            catch (WMSExceptionMessage excp)
            {
                throw excp;
            }
            catch (Exception ex)
            {
                ExceptionData oExcpData = new ExceptionData();
                oExcpData.AddInputs("XML", XML);

                ExceptionHandling.LogException(ex, _ClassCode + "003", oExcpData);

                throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = ErrorMessages.WMSExceptionMessage, ShowAsCriticalError = true };
            }
        }
        public async Task<DataSet> GetMachineNos(InventoryDTO items)
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            StringBuilder sbSqlString = new StringBuilder();

            sbSqlString.AppendLine("EXEC [dbo].[SP_GetMachineNos] ");
            sbSqlString.AppendLine("@AccountId = " + items.AccountID + ",");
            sbSqlString.AppendLine("@UserID = " + items.UserId + "");

            DataSet ds = DbUtility.GetDS(sbSqlString.ToString(), this.ConnectionString);

            return ds;
        }
        public async Task<DataSet> GetVehicleNos(InboundDTO obj)
        {
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                StringBuilder sbSqlString = new StringBuilder();

                sbSqlString.AppendLine("EXEC [dbo].[SP_GetVehicleNos] ");
                sbSqlString.AppendLine("@TransactionType = " + obj.TransactionType + ",");
                sbSqlString.AppendLine("@ActionType = " + obj.ActionType + "");
                DataSet ds = DbUtility.GetDS(sbSqlString.ToString(), this.ConnectionString);

                return ds;
            }
            catch(WMSExceptionMessage excp)
            {
                throw excp;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<DataSet> GetVehicleNosDock(InboundDTO obj)
        {
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                StringBuilder sbSqlString = new StringBuilder();

                sbSqlString.AppendLine("EXEC [dbo].[SP_GetVehicleNos_Docks] ");
                sbSqlString.AppendLine("@TenantID = " + obj.TenantID + ",");
                sbSqlString.AppendLine("@DockId = " + obj.DockId + "");
                DataSet ds = DbUtility.GetDS(sbSqlString.ToString(), this.ConnectionString);

                return ds;
            }
            catch (WMSExceptionMessage excp)
            {
                throw excp;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public async Task<DataSet> GetDocksByTenant(string TenantID)
        {
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                StringBuilder sbSqlString = new StringBuilder();

                sbSqlString.AppendLine("EXEC [dbo].[SP_GetDockNumberBasedonTenantID] ");
                sbSqlString.AppendLine("@TenantID = " + TenantID + "");
                DataSet ds = DbUtility.GetDS(sbSqlString.ToString(), this.ConnectionString);

                return ds;
            }
            catch (WMSExceptionMessage excp)
            {
                throw excp;
            }
            catch (Exception)
            {

                throw;
            }
        }





        public async Task<DataSet> GetVehicleTypes()
        {
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                StringBuilder sbSqlString = new StringBuilder();
                sbSqlString.AppendLine("EXEC [dbo].[Get_VehicleTypes]");
                DataSet ds = DbUtility.GetDS(sbSqlString.ToString(), this.ConnectionString);
                return ds;
            }
            catch (WMSExceptionMessage excp)
            {
                throw excp;
            }
            catch (Exception)
            {

                throw;
            }

        }


        public async Task<DataSet> GetOBDLoadingPoints()
        {
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                StringBuilder sbSqlString = new StringBuilder();
                sbSqlString.AppendLine("EXEC [dbo].[Get_VehicleTypes]");
                DataSet ds = DbUtility.GetDS(sbSqlString.ToString(), this.ConnectionString);
                return ds;
            }
            catch (WMSExceptionMessage excp)
            {
                throw excp;
            }
            catch (Exception)
            {

                throw;
            }

        }




        public async Task<DataSet> Upsert_VehicleGateManagement_HHT(InboundDTO items)
        {
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                StringBuilder sbSqlString = new StringBuilder();
                sbSqlString.AppendLine("EXEC [dbo].[USP_UpsertGateInData_HHT]"); 
                sbSqlString.AppendLine("@VehicleNo = '" + items.VehicleNumber + "',");
                sbSqlString.AppendLine("@TransactionId = " + items.TransactionId + ",");
                sbSqlString.AppendLine("@VehicleTypeId = " + items.VehicleTypeId + ",");
                sbSqlString.AppendLine("@TransactionTypeId = " + items.TransactionType + ",");
                sbSqlString.AppendLine("@ActionType = '" + items.ActionType + "',");
                sbSqlString.AppendLine("@ReceivingStatusId = " + items.ReceivingStatus + ",");
                sbSqlString.AppendLine("@PreLoadWeight = '" + items.VehiclePreLoadWeight + "',");
                sbSqlString.AppendLine("@PostLoadWeight = '" + items.VehiclePostLoadWeight + "',");
                sbSqlString.AppendLine("@DriverNumber = " + (string.IsNullOrEmpty(items.DriverNumber) ? 0 : items.DriverNumber) + ",");
                sbSqlString.AppendLine("@UserId = " + (string.IsNullOrEmpty(items.UserId) ? 0 : int.Parse(items.UserId)) + ",");
                sbSqlString.AppendLine("@LoadingPointID = " + items.LoadingPointID + ",");
                sbSqlString.AppendLine("@DockId = '" + items.DockId +"'");


                DataSet ds = DbUtility.GetDS(sbSqlString.ToString(), this.ConnectionString);

                if (ds.Tables[0].Rows[0]["S"].ToString() == "Success")
                {
                    if (items.TransactionType == 2 && items.ActionType == "DockIn")
                    {
                        try
                        {
                            WhatAppNotes notes = new();
                            notes.OutboundID = items.TransactionId;
                            notes.ScenarioID = 2;
                            notes.VechileNumber = items.VehicleNumber;
                            string response = await _whatsappservice.SendWAMBasedOnActivity(notes);
                        }
                        catch (Exception)
                        {

                            throw;
                        }

                    }
                    if (items.TransactionType == 2 && items.ActionType == "DockOut")
                    {
                        try
                        {
                            WhatAppNotes notes = new();
                            notes.OutboundID = items.TransactionId;
                            notes.ScenarioID = 4;
                            notes.VechileNumber = items.VehicleNumber;
                            string response = await _whatsappservice.SendWAMBasedOnActivity(notes);
                        }
                        catch (Exception)
                        {

                            throw;
                        }

                    }
                }

             
                return ds;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
