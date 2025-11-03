using FWMSC21Core.Entities;
using Simpolo_Endpoint.BO;
using Simpolo_Endpoint.Controllers;
using Simpolo_Endpoint.DAO.HHTInterface;
using Simpolo_Endpoint.DAO.Services;
using Simpolo_Endpoint.DBUtil;
using Simpolo_Endpoint.Entities;
using Simpolo_Endpoint.ModelsLibrary;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simpolo_Endpoint.DAO.HHTServices
{
    public class CycleCountService : AppDBService, ICycleCount
    {
        public CycleCountService(IOptions<AppSettings> appSettings) : base(appSettings)
        {

        }

        private string _ClassCode = string.Empty;

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<DataSet> GetCCNames(BO.CycleCount cycleCount)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();

            string Query = "EXEC [dbo].[Get_CCName_HHT] @AcoountId = " + cycleCount.AccountId + ",@UserID = " + cycleCount.UserId + "";
            var DS = DbUtility.GetDS(Query, this.ConnectionString);
       
            return DS;
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<DataSet> GetCCBlockedLocations(BO.CycleCount cycleCount)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();

            string Query = "EXEC [dbo].[SP_GET_CycleCount_BlockedLocations] @CCM_CNF_AccountCycleCount_ID = 0 , @LoggedInUserID = " + cycleCount.UserId + " , @AM_MST_Account_ID = " + cycleCount.AccountId + " , @CCM_TRN_CycleCount_ID = 0 , @Prefix = '' , @CycleCountCode=" + DBLibrary.SQuote(cycleCount.CycleCountCode) + "";
            var DS = DbUtility.GetDS(Query, this.ConnectionString);

            return DS;
        }

        public async Task<BO.CycleCount> IsBlockedLocation(BO.CycleCount cycleCount)
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            cycleCount = await CheckLocationForCycleCount(cycleCount);

            if (cycleCount.Result == "")
            {
                string sql = "EXEC IsBlockedLocationForCC @CCName=" + DBLibrary.SQuote(cycleCount.CCName) + " ,@WarehouseID=" + cycleCount.WarehouseID + " ,@Location=" + DBLibrary.SQuote(cycleCount.Location) + ",@UserID=" + cycleCount.UserId;
                int result = DbUtility.GetSqlN(sql, ConnectionString);
                if (result == 1)
                {
                    cycleCount = await GetBinCount(cycleCount);
                    cycleCount.Result = "-1";
                }
                else
                {
                    cycleCount.Result = "-4";
                }
                //{
                //    cycleCount.Result = "0";
                //}
            }
            return cycleCount;
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<BO.CycleCount> CheckLocationForCycleCount(BO.CycleCount cycleCount)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            string sql = "EXEC [dbo].[CheckLocationForCycleCount_HHT]  @Location=" + DBLibrary.SQuote(cycleCount.Location) + ",@UserId =" + cycleCount.UserId + ",@CCNAME =" + DBLibrary.SQuote(cycleCount.CCName) + ",@WarehouseID=" + cycleCount.WarehouseID + "";
            int result = DbUtility.GetSqlN(sql, ConnectionString);

            if (result == 0)
            {
                cycleCount.Result = "This Location is not configured to the user";
            }
            else
            {
                cycleCount.Result = "";
            }
            return cycleCount;
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<BO.CycleCount> GetBinCount(BO.CycleCount cycleCount)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            string sql = "EXEC [dbo].[GetBincount_HHT]  @Location=" + DBLibrary.SQuote(cycleCount.Location) + ",@CycleCountName =" + DBLibrary.SQuote(cycleCount.CCName) + ",@WarehouseID =" + cycleCount.WarehouseID + "";
            int result = DbUtility.GetSqlN(sql, ConnectionString);

            if (result != 0)
            {
                cycleCount.Result = "";
                cycleCount.Count = result.ToString();
            }
            else
            {
                cycleCount.Count = "0";
            }

            return cycleCount;
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<BO.CycleCount> ReleaseCycleCountLocation(BO.CycleCount cycleCount)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            string sql = "EXEC [dbo].[CloseBinForCylceCount_HHT]  @CCName=" + DBLibrary.SQuote(cycleCount.CCName) + ",@Location =" + DBLibrary.SQuote(cycleCount.Location) + ",@WarehouseID =" + cycleCount.WarehouseID + "";
            int result = DbUtility.GetSqlN(sql, ConnectionString);

            if (result == 0)
            {
                cycleCount.Result = "Scanned Location already closed";
            }
            else
            {
                cycleCount.Result = "Closed successfully";
            }
            return cycleCount;
        }

        public async Task<BO.CycleCount> BlockLocationForCycleCount(BO.CycleCount cycleCount)
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            string sql = "EXEC [dbo].[BlockLocationForCycelCount_HHT]  @Location = " + DBLibrary.SQuote(cycleCount.Location) + "";
            int result = DbUtility.GetSqlN(sql, ConnectionString);

            if (result == 0)
            {
                cycleCount.Result = "This Location already in Transaction, Scan another Location";
                return cycleCount;
            }
            else
            {

                cycleCount = await GetBinCount(cycleCount);
                cycleCount.Result = "";
            }
            return cycleCount;
        }

        public async Task<BO.CycleCount> CheckMaterialAvailablilty(BO.CycleCount cycleCount)
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();

            string Query = "exec [dbo].[CheckMaterialAvailablity_HHT]  @MCode=" + DBLibrary.SQuote(cycleCount.SKU) + ",@TenantID=" + cycleCount.TenantID + ",@UserID=" + cycleCount.UserId + "";
            var ds = DbUtility.GetDS(Query, this.ConnectionString);

            if (ds != null)
            {
                if (ds.Tables[0].Rows[0]["MATERIALMASTERID"].ToString() == "0")
                {
                    cycleCount.Result = "Material is not belongs to this user";
                }
                else if (ds.Tables[1].Rows[0]["UOMID"].ToString() == "0")
                {
                    cycleCount.Result = "UoM is not Configured to this Material";
                }
                else
                {
                    cycleCount = await IsMaterialMappedToCycleCount(cycleCount);
                }
            }
            return cycleCount;
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<BO.CycleCount> IsMaterialMappedToCycleCount(BO.CycleCount cycleCount)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            string sql = "EXEC [dbo].[IsMaterialMappedToCycleCount_HHT] @MCode=" + DBLibrary.SQuote(cycleCount.SKU) + ",@AccountCycleCountName =" + DBLibrary.SQuote(cycleCount.CCName) + ",@UserID=" + cycleCount.UserId + ",@AccountID=" + cycleCount.AccountId + "";
            int result = DbUtility.GetSqlN(sql, ConnectionString);

            if (result == 0)
            {
                cycleCount.Result = "This Material is Not Configured in this CC";
            }
            else
            {
                cycleCount.Result = "";
            }
            return cycleCount;         
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<List<BO.CycleCount>> GetCycleCountInformation(BO.CycleCount cycleCount)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            List<BO.CycleCount> lstCCStock = new List<BO.CycleCount>();
            try
            {
                string Query = "exec [dbo].[GetCycleCountConsolidatedData_HHT]   @Location=" + DBLibrary.SQuote(cycleCount.Location) + ",@CCNAME =" + DBLibrary.SQuote(cycleCount.CCName) + ",@WarehouseID=" + cycleCount.WarehouseID + "";
                var ds = DbUtility.GetDS(Query, this.ConnectionString);

                if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        BO.CycleCount ccinformation = new BO.CycleCount();
                        ccinformation.CCName = dr["CCHeader"].ToString();
                        ccinformation.Location = dr["Location"].ToString();
                        ccinformation.SKU = dr["MCode"].ToString();
                        ccinformation.MfgDate = dr["Mfgdate"].ToString();
                        ccinformation.ExpDate = dr["expdate"].ToString();
                        ccinformation.SerialNo = dr["serialno"].ToString();
                        ccinformation.BatchNo = dr["Batchno"].ToString();
                        ccinformation.ProjectRefNo = dr["projectrefno"].ToString();
                        ccinformation.CCQty = Convert.ToDecimal(dr["Qty"].ToString());
                        lstCCStock.Add(ccinformation);
                    }
                }

                return lstCCStock;
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                return lstCCStock;
            }
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<bool> ClearLocationBlock(Location oLocation, Entities.CycleCount oCycleCount, Entities.Inventory oInventory , bool isConsolidationRequired)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();

                if (oLocation == null)
                    throw new WMSExceptionMessage() { };

                string Query = "SELECT LocationID FROM Inv_Location WHERE Location =" + DBLibrary.SQuote(oLocation.LocationCode) + "";
                var DS = DbUtility.GetDS(Query, this.ConnectionString);

                var result = JsonConvert.SerializeObject(DS);

                JObject data = JObject.Parse(result);
                JArray table = (JArray)data["Table"];
                int LocationID = (int)table[0]["LocationID"];

                //string _sSQL = "UPDATE LM SET LM.IsBlockedForCycleCount = 0 FROM INV_Location AS LM WHERE LM.LocationID = " + oLocation.LocationID.ToString() + " OR LM.DisplayLocationCode LIKE '%" + (oLocation.SystemLocationCode == null ? oLocation.LocationCode : oLocation.SystemLocationCode) + "%' OR LM.Location LIKE '%" + (oLocation.SystemLocationCode == null ? oLocation.LocationCode : oLocation.SystemLocationCode) + "%'";
                //string _sSQL = "UPDATE LM SET LM.IsBlockedForCycleCount = 0 FROM INV_Location AS LM WHERE LM.LocationID = " + oLocation.LocationID.ToString() + " OR LM.DisplayLocationCode = '" + (oLocation.SystemLocationCode == null ? oLocation.LocationCode : oLocation.SystemLocationCode) + "'";

                //string ClearLocationBlockQuery = "Exec SP_CYC_ClearLocationBlock @LocationID = " + oLocation.LocationID.ToString() + ",@DisplayLocationCode = " + (oLocation.SystemLocationCode == null ? DBLibrary.SQuote(oLocation.LocationCode) : DBLibrary.SQuote(oLocation.SystemLocationCode)) + "";

                string ClearLocationBlockQuery = "Exec SP_CYC_ClearLocationBlock @LocationID = " + LocationID + ",@DisplayLocationCode = " + (oLocation.SystemLocationCode == null ? DBLibrary.SQuote(oLocation.LocationCode) : DBLibrary.SQuote(oLocation.SystemLocationCode)) + "";
                int _Result = DbUtility.GetSqlN(ClearLocationBlockQuery , ConnectionString);

                //string _sQuery = "UPDATE CCM_TRN_CycleCountInventoryPlan SET IsDeleted=1,IsScanned=1 WHERE CCM_CNF_AccountCycleCount_ID=(SELECT CCM_CNF_AccountCycleCount_ID FROM CCM_CNF_AccountCycleCounts WHERE IsActive=1 AND IsDeleted=0 AND AM_MST_Account_ID=" + oCycleCount.AccountID + " AND ISNULL(dbo.UDF_ParseAndReturnLocaleString(AccountCycleCountName, 'en'),'')='" + oCycleCount.AccountCycleCountName + "') AND LocationID = (SELECT LocationID FROM INV_Location WHERE IsActive=1 AND IsDeleted=0 AND DisplayLocationCode='" + oLocation.LocationCode + "')";
                string UpdateClearLocationBlock = "Exec SP_CYC_UpdateClearLocationBlock @AM_MST_Account_ID=" + oCycleCount.AccountID + ",@AccountCycleCountName=" + DBLibrary.SQuote(oCycleCount.AccountCycleCountName) + ",@DisplayLocationCode = " + DBLibrary.SQuote(oLocation.LocationCode) + "";
                int _ResultPaln = DbUtility.GetSqlN(UpdateClearLocationBlock , ConnectionString);

                if (isConsolidationRequired)
                {
                    ConsloidateCycleCountInventory(oCycleCount, oInventory);
                }

                //else
                //{
                //    StringBuilder _sSqlBuilder = new StringBuilder();
                //    _sSqlBuilder.Append("EXEC [dbo].[USP_API_CCM_UpdateBinCompleteStatus]");
                //    _sSqlBuilder.Append("@CycleCountCode=" + base.SQuote(oCycleCount.CycleCountCode));
                //    _sSqlBuilder.Append(",@LocationCode=" + base.SQuote(oLocation.LocationCode));
                //    _sSqlBuilder.Append(",@ActivityByUser=" + base.LoggedInUserID);
                //    ExecuteNonQuery(_sSqlBuilder.ToString());
                //    oInventory.LocationCode = oLocation.LocationCode;
                //}

                if (_Result > 0)
                {
                    return true;
                }
                else
                {
                    throw new Exception("Failed to Clear cycle count block on location");
                }
            }
            catch (WMSExceptionMessage excp)
            {
                throw excp;
            }
            catch (Exception excp)
            {
                ExceptionData oExcpData = new ExceptionData();
                oExcpData.AddInputs("oLocation", oLocation);

                ExceptionHandling.LogException(excp, _ClassCode + "005", oExcpData);

                throw new WMSExceptionMessage()
                {
                    WMSExceptionCode = ErrorMessages.WMSExceptionCode,
                    WMSMessage = ErrorMessages.WMSExceptionMessage,
                    ShowAsCriticalError = true
                };
            }
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async void ConsloidateCycleCountInventory(Entities.CycleCount oCycleCount, Inventory oInventory)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            try
            {
                if (oCycleCount == null || oInventory == null)
                    throw new WMSExceptionMessage() { };

                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                StringBuilder sCmdUpsertPOQuantity = new StringBuilder();

                sCmdUpsertPOQuantity.AppendLine("EXEC [dbo].[USP_API_CCM_ConsolidateInventoryAfterCycleCount] ");
                sCmdUpsertPOQuantity.AppendLine("@CycleCountCode = " + DBLibrary.SQuote(oCycleCount.CycleCountCode) + ",");
                sCmdUpsertPOQuantity.AppendLine("@LocationCode = " + DBLibrary.SQuote(oInventory.LocationCode) + ",");
                sCmdUpsertPOQuantity.AppendLine("@AccountID = " + oCycleCount.AccountID + ",");
                sCmdUpsertPOQuantity.AppendLine("@ActivityByUser = " + oInventory.CreatedBy + "");

                string Query = sCmdUpsertPOQuantity.ToString();
                DataSet dsResult = DbUtility.GetDS(Query, this.ConnectionString);
            }
            catch (WMSExceptionMessage excp)
            {
                throw excp;
            }
            catch (Exception excp)
            {
                ExceptionData oExcpData = new ExceptionData();
                oExcpData.AddInputs("oCycleCount", oCycleCount);
                oExcpData.AddInputs("oInventory", oInventory);

                ExceptionHandling.LogException(excp, _ClassCode + "010", oExcpData);

                throw new WMSExceptionMessage()
                {
                    WMSExceptionCode = ErrorMessages.WMSExceptionCode,
                    WMSMessage = ErrorMessages.WMSExceptionMessage,
                    ShowAsCriticalError = true
                };
            }
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<string> GetConatinerLocationBin(string cartoncode, string WarehouseId, string UserID, string LocationCheck)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();

            if (WarehouseId == "")
            {
                WarehouseId = "0";
            }
           
            string Query = "EXEC [dbo].[SP_CHECK_CONTAINER_LOCATION_MAPPING]  @CONTAINER=" + DBLibrary.SQuote(cartoncode) + ",@WarehouseID=" + WarehouseId + ",@UserID=" + UserID + ",@Location=" + DBLibrary.SQuote(LocationCheck) + "";
            var Location = DbUtility.GetSqlS(Query, ConnectionString).ToString();
                
            return Location;
        }

        public async Task<BO.CycleCount> UpsertCycleCount(BO.CycleCount cyclecountinfo)
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();

            string Query = "exec [dbo].[Get_CCDetails_HHT] @CCName=" + DBLibrary.SQuote(cyclecountinfo.CCName) + "";
            DataSet ds = DbUtility.GetDS(Query, this.ConnectionString);

            if (ds != null && ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    cyclecountinfo.CycleCountID = Convert.ToInt32(ds.Tables[0].Rows[0]["CCM_TRN_CycleCount_ID"].ToString());
                    cyclecountinfo.AccountCycleCountID = Convert.ToInt32(ds.Tables[0].Rows[0]["CCM_CNF_AccountCycleCount_ID"].ToString());
                    cyclecountinfo.MSTCycleCountID = Convert.ToInt32(ds.Tables[0].Rows[0]["CCM_MST_CycleCount_ID"].ToString());
                    cyclecountinfo.CycleCountEntityID = Convert.ToInt32(ds.Tables[0].Rows[0]["CCM_MST_CycleCountEntity_ID"].ToString());
                    cyclecountinfo.EntityID = Convert.ToInt32(ds.Tables[0].Rows[0]["Entity_ID"].ToString());
                }
            }
            else
            {
                cyclecountinfo.Result = "Error while inserting";
                return cyclecountinfo;
            }

            string strQuery = "EXEC [dbo].[UpsertCycleCountDetails_HHT] @CCM_TRN_CycleCount_ID=" + cyclecountinfo.CycleCountID;
            strQuery += ",@CCM_CNF_AccountCycleCount_ID=" + cyclecountinfo.AccountCycleCountID;
            strQuery += ",@CCM_MST_CycleCount_ID=" + cyclecountinfo.MSTCycleCountID;
            strQuery += ",@CCM_MST_CycleCountEntity_ID=" + cyclecountinfo.CycleCountEntityID;
            strQuery += ",@Entity_ID=" + cyclecountinfo.EntityID;
            strQuery += ",@Location=" + DBLibrary.SQuote(cyclecountinfo.Location);
            strQuery += ",@Container=" + DBLibrary.SQuote(cyclecountinfo.Container);
            strQuery += ",@Mcode=" + DBLibrary.SQuote(cyclecountinfo.SKU);
            strQuery += ",@Qty=" + cyclecountinfo.CCQty;
            strQuery += ",@UserId =" + cyclecountinfo.UserId;
            strQuery += ",@TenantID =" + cyclecountinfo.TenantID;
            strQuery += ",@Mfgdate =" + DBLibrary.SQuote(cyclecountinfo.MfgDate != null ? cyclecountinfo.MfgDate : "NULL");
            strQuery += ",@Expdate =" + DBLibrary.SQuote(cyclecountinfo.ExpDate != null ? cyclecountinfo.ExpDate : "NULL");
            strQuery += ",@StorageLocation =" + DBLibrary.SQuote(cyclecountinfo.StorageLocation != null ? cyclecountinfo.StorageLocation : "NULL");
            if (cyclecountinfo.SerialNo != "")
            {
                strQuery += ",@SerialNo =" + DBLibrary.SQuote(cyclecountinfo.SerialNo != null ? cyclecountinfo.SerialNo : "NULL");
            }
            if (cyclecountinfo.BatchNo != "")
            {
                strQuery += ",@BatchNo =" + DBLibrary.SQuote(cyclecountinfo.BatchNo != null ? cyclecountinfo.BatchNo : "NULL");
            }
            if (cyclecountinfo.ProjectRefNo != "")
            {
                strQuery += ",@ProjectRefNo =" + DBLibrary.SQuote(cyclecountinfo.ProjectRefNo != null ? cyclecountinfo.ProjectRefNo : "NULL");
            }
            if (cyclecountinfo.ProjectRefNo != "")
            {
                strQuery += ",@MRP =" + DBLibrary.SQuote(cyclecountinfo.MRP != null ? cyclecountinfo.MRP : "NULL");
            }
            strQuery += " ,@WarehouseID=" + cyclecountinfo.WarehouseID;
            strQuery += ",@Grade =" + DBLibrary.SQuote(cyclecountinfo.Grade != null ? cyclecountinfo.Grade : "NULL");
            int result = DbUtility.GetSqlN(strQuery, ConnectionString);
            if (result == 0)
            {
                cyclecountinfo.Result = "Error while inserting";
            }
            else if (result == -1)
            {
                cyclecountinfo.Result = "Invalid Location";
            }
            else
            {
                cyclecountinfo = await GetBinCount(cyclecountinfo);
                cyclecountinfo.Result = "Confirmed successfully";
            }

            return cyclecountinfo;
        }
    }
}
