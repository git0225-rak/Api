using FWMSC21Core.Entities;
using FWMSC21Core_BusinessEntities.Entities;
using Simpolo_Endpoint.Controllers;
using Simpolo_Endpoint.DAO.HHTInterface;
using Simpolo_Endpoint.DAO.Services;
using Simpolo_Endpoint.DBUtil;
using Simpolo_Endpoint.DTO;
using Simpolo_Endpoint.Entities;
using Simpolo_Endpoint.ModelsLibrary;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Simpolo_Endpoint.DAO.HHTServices
{
    public class OutboundService : AppDBService, IOutbound
    {
        public OutboundService(IOptions<AppSettings> appSettings) : base(appSettings)
        {

        }
        private string _ClassCode = string.Empty;
        //BaseController baseController = new BaseController();
#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<List<Outbound>> GetOBDRefNos(Outbound outbound)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                List<Outbound> _lstOutbounds = new List<Outbound>();
                string GetOBDRefNosQuery = "EXEC [dbo].[Get_OBDNoList] @UserID = " + outbound.UserId + ",@AccountId = " + outbound.AccountID + ",@IsSamplePick = " + outbound.IsSample + ",@IsWorkOrder = " + DBLibrary.SQuote(outbound.IsWorkOrder) + ",@WareHouseID = " + outbound.WareHouseID + ",@TenantId = " + outbound.TenantId + ",@IsLoading=" + outbound.IsLoading + ",@ActionType="+DBLibrary.SQuote(outbound.ActionType);
                var _dsPickLists = DbUtility.GetDS(GetOBDRefNosQuery, this.ConnectionString);

                if (_dsPickLists != null && _dsPickLists.Tables.Count != 0 && _dsPickLists.Tables[0].Rows.Count != 0)
                {
                    foreach (DataRow _dtPack in _dsPickLists.Tables[0].Rows)
                    {
                        Outbound _oOutbound = new Outbound()
                        {
                            OBDNumber = _dtPack["OBDNumber"].ToString(),
                            OutboundID = ConversionUtility.ConvertToInt(_dtPack["OutboundID"].ToString()),
                            PickedQty = Convert.ToDecimal(_dtPack["PickedQty"].ToString()),
                            LoadQty = Convert.ToDecimal(_dtPack["LoadQty"].ToString()),
                            UnLoadQty = Convert.ToDecimal(_dtPack["UnLoadQty"].ToString())
                        };
                        _lstOutbounds.Add(_oOutbound);
                    }
                }
                else
                {
                    return _lstOutbounds;
                    //throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = "No Data Found For Given Search Criteria.", ShowAsWarning = true };
                }
                return _lstOutbounds;
            }
            catch (WMSExceptionMessage excp)
            {
                throw excp;
            }
            catch (Exception excp)
            {
                ExceptionData oExcpData = new ExceptionData();
                oExcpData.AddInputs("oCriteria", outbound);

                ExceptionHandling.LogException(excp, _ClassCode + "001", oExcpData);
                throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = ErrorMessages.WMSExceptionMessage, ShowAsCriticalError = true };
            }
        }

        public async Task<List<Outbound>> GetVehicleNumbers_Loading(Outbound outbound)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                List<Outbound> _lstOutbounds = new List<Outbound>();
                string GetOBDRefNosQuery = "EXEC [dbo].[Get_VehicleNumbers] @UserID = " + outbound.UserId + ",@AccountId = " + outbound.AccountID + ",@IsSamplePick = " + outbound.IsSample + ",@IsWorkOrder = " + DBLibrary.SQuote(outbound.IsWorkOrder) + ",@WareHouseID = " + outbound.WareHouseID + ",@TenantId = " + outbound.TenantId + ",@IsLoading=" + outbound.IsLoading + ",@ActionType=" + DBLibrary.SQuote(outbound.ActionType);
                var _dsPickLists = DbUtility.GetDS(GetOBDRefNosQuery, this.ConnectionString);

                if (_dsPickLists != null && _dsPickLists.Tables.Count != 0 && _dsPickLists.Tables[0].Rows.Count != 0)
                {
                    foreach (DataRow _dtPack in _dsPickLists.Tables[0].Rows)
                    {
                        Outbound _oOutbound = new Outbound()
                        {
                            Vehicle = _dtPack["VehicleNo"].ToString(),
                            OutboundID = ConversionUtility.ConvertToInt(_dtPack["OutboundID"].ToString()),
                            PickedQty = Convert.ToDecimal(_dtPack["PickedQty"].ToString()),
                            LoadQty = Convert.ToDecimal(_dtPack["LoadQty"].ToString()),
                            UnLoadQty = Convert.ToDecimal(_dtPack["UnLoadQty"].ToString())
                        };
                        _lstOutbounds.Add(_oOutbound);
                    }
                }
                else
                {
                    return _lstOutbounds;
                    //throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = "No Data Found For Given Search Criteria.", ShowAsWarning = true };
                }
                return _lstOutbounds;
            }
            catch (WMSExceptionMessage excp)
            {
                throw excp;
            }
            catch (Exception excp)
            {
                ExceptionData oExcpData = new ExceptionData();
                oExcpData.AddInputs("oCriteria", outbound);

                ExceptionHandling.LogException(excp, _ClassCode + "001", oExcpData);
                throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = ErrorMessages.WMSExceptionMessage, ShowAsCriticalError = true };
            }
        }


        public async Task<OutboundDTO> GetOBDItemToPick(OutboundDTO outbound)
        {
            string query = " EXEC [dbo].[GEN_OBDWISEDEATILS_HHT] @OBDID=" + outbound.OutboundID + ",@FetchNextItem=" + outbound.FetchNextItem + ",@RID=" + outbound.RID + ",@UserID=" + outbound.UserId;
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                DataSet ds = DbUtility.GetDS(query, this.ConnectionString);

                if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count != 0)
                {
                    outbound.AssignedID = ds.Tables[0].Rows[0]["AssignedID"].ToString();
                    outbound.MaterialMasterId = ds.Tables[0].Rows[0]["MaterialMasterID"].ToString();
                    outbound.MCode = ds.Tables[0].Rows[0]["MCode"].ToString();
                    outbound.MaterialDescription = ds.Tables[0].Rows[0]["MDescription"].ToString();
                    outbound.CartonNo = ds.Tables[0].Rows[0]["CartonCode"].ToString();
                    outbound.CartonID = ds.Tables[0].Rows[0]["CartonID"].ToString();
                    outbound.Location = ds.Tables[0].Rows[0]["Location"].ToString();
                    outbound.LocationId = ds.Tables[0].Rows[0]["LocationID"].ToString();
                    outbound.MfgDate = ds.Tables[0].Rows[0]["MfgDate"].ToString();
                    outbound.ExpDate = ds.Tables[0].Rows[0]["ExpDate"].ToString();
                    outbound.SerialNo = ds.Tables[0].Rows[0]["SerialNo"].ToString();
                    outbound.BatchNo = ds.Tables[0].Rows[0]["BatchNo"].ToString();
                    outbound.ProjectNo = ds.Tables[0].Rows[0]["ProjectRefNo"].ToString();
                    outbound.AssignedQuantity = ds.Tables[0].Rows[0]["AssignedQuantity"].ToString();
                    outbound.PickedQty = ds.Tables[0].Rows[0]["PickedQty"].ToString();
                    outbound.OutboundID = ds.Tables[0].Rows[0]["OutboundID"].ToString();
                    outbound.SODetailsID = ds.Tables[0].Rows[0]["SODetailsID"].ToString();
                    outbound.SLocId = ds.Tables[0].Rows[0]["StorageLocationID"].ToString();
                    outbound.SLoc = ds.Tables[0].Rows[0]["SLOC"].ToString();
                    outbound.GoodsmomentDeatilsId = ds.Tables[0].Rows[0]["GoodsMovementDetailsID"].ToString();
                    outbound.Lineno = ds.Tables[0].Rows[0]["LineNumber"].ToString();
                    outbound.MaterialMaster_IUoMID = ds.Tables[0].Rows[0]["MaterialMaster_SUoMID"].ToString();
                    outbound.CF = ds.Tables[0].Rows[0]["CF"].ToString();
                    outbound.POSOHeaderId = ds.Tables[0].Rows[0]["SOHeaderID"].ToString();
                    outbound.PendingQty = ds.Tables[0].Rows[0]["PendingQty"].ToString();
                    outbound.MRP = ds.Tables[0].Rows[0]["MRP"].ToString();
                    outbound.HUNo = ds.Tables[0].Rows[0]["HUNo"].ToString();
                    outbound.HUSize = ds.Tables[0].Rows[0]["HUSize"].ToString();
                    outbound.IsPSN = ds.Tables[0].Rows[0]["IsPSN"].ToString();
                    outbound.CustomerName = ds.Tables[0].Rows[0]["CustomerName"].ToString();
                    outbound.DockLocation = ds.Tables[0].Rows[0]["DockLocation"].ToString();
                    outbound.RID = Convert.ToInt32(ds.Tables[0].Rows[0]["RID"].ToString());
                    outbound.IsVstore = (ds.Tables[0].Rows[0]["IsVstore"]) == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["IsVstore"]);
                    outbound.TrayNo = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["TrayNo"].ToString()) ? "" : ds.Tables[0].Rows[0]["TrayNo"].ToString();
                    outbound.Machineno = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["MachineNo"].ToString()) ? "" : ds.Tables[0].Rows[0]["MachineNo"].ToString();
                    outbound.VStoreType = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["VStoreType"].ToString()) ? "" : ds.Tables[0].Rows[0]["VStoreType"].ToString();
                    outbound.Accesspoint = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Accesspoint"].ToString()) ? "" : ds.Tables[0].Rows[0]["Accesspoint"].ToString();
                    outbound.ItemCount = ds.Tables[0].Rows[0]["ItemCount"].ToString();
                }
                else
                {
                    outbound.PendingQty = "0";
                }
                return outbound;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }



        public async Task<OutboundDTO> GetOBDItemToPick_BatchGrade(OutboundDTO outbound)
        {
            string query = " EXEC [dbo].[GEN_OBDWISEDEATILS_HHT_BatchGrade] @TransferrefID=" + outbound.TransferRefId + ",@FetchNextItem=" + outbound.FetchNextItem + ",@RID=" + outbound.RID + ",@UserID=" + outbound.UserId;
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                DataSet ds = DbUtility.GetDS(query, this.ConnectionString);

                if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count != 0)
                {
                    outbound.AssignedID = ds.Tables[0].Rows[0]["AssignedID"].ToString();
                    outbound.MaterialMasterId = ds.Tables[0].Rows[0]["MaterialMasterID"].ToString();
                    outbound.MCode = ds.Tables[0].Rows[0]["MCode"].ToString();
                    outbound.MaterialDescription = ds.Tables[0].Rows[0]["MDescription"].ToString();
                    outbound.CartonNo = ds.Tables[0].Rows[0]["CartonCode"].ToString();
                    outbound.CartonID = ds.Tables[0].Rows[0]["CartonID"].ToString();
                    outbound.Location = ds.Tables[0].Rows[0]["Location"].ToString();
                    outbound.LocationId = ds.Tables[0].Rows[0]["LocationID"].ToString();
                    outbound.MfgDate = ds.Tables[0].Rows[0]["MfgDate"].ToString();
                    outbound.ExpDate = ds.Tables[0].Rows[0]["ExpDate"].ToString();
                    outbound.SerialNo = ds.Tables[0].Rows[0]["SerialNo"].ToString();
                    outbound.BatchNo = ds.Tables[0].Rows[0]["BatchNo"].ToString();
                    outbound.ProjectNo = ds.Tables[0].Rows[0]["ProjectRefNo"].ToString();
                    outbound.AssignedQuantity = ds.Tables[0].Rows[0]["AssignedQuantity"].ToString();
                    outbound.PickedQty = ds.Tables[0].Rows[0]["PickedQty"].ToString();
                    outbound.OutboundID = ds.Tables[0].Rows[0]["OutboundID"].ToString();
                    outbound.SODetailsID = ds.Tables[0].Rows[0]["SODetailsID"].ToString();
                    outbound.SLocId = ds.Tables[0].Rows[0]["StorageLocationID"].ToString();
                    outbound.SLoc = ds.Tables[0].Rows[0]["SLOC"].ToString();
                    outbound.GoodsmomentDeatilsId = ds.Tables[0].Rows[0]["GoodsMovementDetailsID"].ToString();
                    outbound.PendingQty = ds.Tables[0].Rows[0]["PendingQty"].ToString();
                    outbound.MRP = ds.Tables[0].Rows[0]["MRP"].ToString();
                    outbound.HUNo = ds.Tables[0].Rows[0]["HUNo"].ToString();
                    outbound.HUSize = ds.Tables[0].Rows[0]["HUSize"].ToString();
                    outbound.IsPSN = ds.Tables[0].Rows[0]["IsPSN"].ToString();
                    outbound.RID = Convert.ToInt32(ds.Tables[0].Rows[0]["RID"].ToString());
                    outbound.IsVstore = (ds.Tables[0].Rows[0]["IsVstore"]) == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["IsVstore"]);
                    outbound.TrayNo = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["TrayNo"].ToString()) ? "" : ds.Tables[0].Rows[0]["TrayNo"].ToString();
                    outbound.Machineno = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["MachineNo"].ToString()) ? "" : ds.Tables[0].Rows[0]["MachineNo"].ToString();
                    outbound.VStoreType = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["VStoreType"].ToString()) ? "" : ds.Tables[0].Rows[0]["VStoreType"].ToString();
                    outbound.Accesspoint = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Accesspoint"].ToString()) ? "" : ds.Tables[0].Rows[0]["Accesspoint"].ToString();
                    outbound.ItemCount = ds.Tables[0].Rows[0]["ItemCount"].ToString();
                }
                else
                {
                    outbound.PendingQty = "0";
                }
                return outbound;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }





        public async Task<OutboundDTO> GetOBDItemToPick_PalletConsolidate(OutboundDTO outbound)
        {
            string query = " EXEC [dbo].[GEN_OBDWISEDEATILS_HHT_Palletconsolidate] @TransferrefID=" + outbound.TransferRefId + ",@FetchNextItem=" + outbound.FetchNextItem + ",@RID=" + outbound.RID + ",@UserID=" + outbound.UserId;
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                DataSet ds = DbUtility.GetDS(query, this.ConnectionString);

                if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count != 0)
                {
                    outbound.AssignedID = ds.Tables[0].Rows[0]["AssignedID"].ToString();
                    outbound.MaterialMasterId = ds.Tables[0].Rows[0]["MaterialMasterID"].ToString();
                    outbound.MCode = ds.Tables[0].Rows[0]["MCode"].ToString();
                    outbound.MaterialDescription = ds.Tables[0].Rows[0]["MDescription"].ToString();
                    outbound.CartonNo = ds.Tables[0].Rows[0]["CartonCode"].ToString();
                    outbound.CartonID = ds.Tables[0].Rows[0]["CartonID"].ToString();
                    outbound.ToCartonNo = ds.Tables[0].Rows[0]["ToCartonCode"].ToString();
                    outbound.ToCartonID = ds.Tables[0].Rows[0]["ToCartonID"].ToString();
                    outbound.Location = ds.Tables[0].Rows[0]["Location"].ToString();
                    outbound.LocationId = ds.Tables[0].Rows[0]["LocationID"].ToString();
                    outbound.ToLocation = ds.Tables[0].Rows[0]["ToLocation"].ToString();
                    outbound.ToLocationId = ds.Tables[0].Rows[0]["ToLocationID"].ToString();
                    outbound.MfgDate = ds.Tables[0].Rows[0]["MfgDate"].ToString();
                    outbound.ExpDate = ds.Tables[0].Rows[0]["ExpDate"].ToString();
                    outbound.SerialNo = ds.Tables[0].Rows[0]["SerialNo"].ToString();
                    outbound.BatchNo = ds.Tables[0].Rows[0]["BatchNo"].ToString();
                    outbound.ProjectNo = ds.Tables[0].Rows[0]["ProjectRefNo"].ToString();
                    outbound.AssignedQuantity = ds.Tables[0].Rows[0]["AssignedQuantity"].ToString();
                    outbound.PickedQty = ds.Tables[0].Rows[0]["PickedQty"].ToString();
                    outbound.OutboundID = ds.Tables[0].Rows[0]["OutboundID"].ToString();
                    outbound.SODetailsID = ds.Tables[0].Rows[0]["SODetailsID"].ToString();
                    outbound.SLocId = ds.Tables[0].Rows[0]["StorageLocationID"].ToString();
                    outbound.SLoc = ds.Tables[0].Rows[0]["SLOC"].ToString();
                    outbound.GoodsmomentDeatilsId = ds.Tables[0].Rows[0]["GoodsMovementDetailsID"].ToString();
                    outbound.PendingQty = ds.Tables[0].Rows[0]["PendingQty"].ToString();
                    outbound.MRP = ds.Tables[0].Rows[0]["MRP"].ToString();
                    outbound.HUNo = ds.Tables[0].Rows[0]["HUNo"].ToString();
                    outbound.HUSize = ds.Tables[0].Rows[0]["HUSize"].ToString();
                    outbound.IsPSN = ds.Tables[0].Rows[0]["IsPSN"].ToString();
                    outbound.RID = Convert.ToInt32(ds.Tables[0].Rows[0]["RID"].ToString());
                    outbound.IsVstore = (ds.Tables[0].Rows[0]["IsVstore"]) == DBNull.Value ? false : Convert.ToBoolean(ds.Tables[0].Rows[0]["IsVstore"]);
                    outbound.TrayNo = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["TrayNo"].ToString()) ? "" : ds.Tables[0].Rows[0]["TrayNo"].ToString();
                    outbound.Machineno = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["MachineNo"].ToString()) ? "" : ds.Tables[0].Rows[0]["MachineNo"].ToString();
                    outbound.VStoreType = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["VStoreType"].ToString()) ? "" : ds.Tables[0].Rows[0]["VStoreType"].ToString();
                    outbound.Accesspoint = string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Accesspoint"].ToString()) ? "" : ds.Tables[0].Rows[0]["Accesspoint"].ToString();
                    outbound.ItemCount = ds.Tables[0].Rows[0]["ItemCount"].ToString();
                }
                else
                {
                    outbound.PendingQty = "0";
                }
                return outbound;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }





        public async Task<OutboundDTO> OBDSkipItem(OutboundDTO outbound)
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            StringBuilder sbSqlStrings = new StringBuilder();

            sbSqlStrings.AppendLine("EXEC [dbo].[USP_SET_OBD_SKIP] ");
            sbSqlStrings.AppendLine("@VLPDAssignID = " + DBLibrary.SQuote(outbound.AssignedID) + ",");
            sbSqlStrings.AppendLine("@Reason = " + DBLibrary.SQuote(outbound.SkipReason) + ",");
            sbSqlStrings.AppendLine("@CreatedBy = " + DBLibrary.SQuote(outbound.UserId) + "");

            string Query = sbSqlStrings.ToString();

            string result = DbUtility.GetSqlS(Query, ConnectionString).ToString();
            if (result == "1")
            {
                outbound.Result = "Success";
                await RegeneratePickItemsuggestionForOBD(outbound);
            }
            else
            {
                outbound.Result = result;
            }
            if (outbound.Flag == "1")
            {
                //await RegeneratePickItemsuggestionForOBD(outbound);
                return outbound;
            }
            return outbound;
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<OutboundDTO> RegeneratePickItemsuggestionForOBD(OutboundDTO outbound)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            StringBuilder sbSqlStrings = new StringBuilder();

            sbSqlStrings.AppendLine("EXEC [dbo].[Sp_OBD_UpsertPickingSuggestions] ");
            sbSqlStrings.AppendLine("@OutboundID = " + outbound.OutboundID + ",");
            sbSqlStrings.AppendLine("@UserID = " + outbound.UserId + "");

            string Query = sbSqlStrings.ToString();
            DataSet dsResult = DbUtility.GetDS(Query, this.ConnectionString);

            return outbound;
        }

        public async Task<BO.Outbound> UpdatePickItem(BO.Outbound outbound)
        {
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                StringBuilder sCmdUpsertPOQuantity = new StringBuilder();

                if (outbound.HUNo > 1)
                {
                    outbound.GoodsMovementDetailsID = "0";
                    outbound.GoodsMovementTypeID = "2";
                    outbound.IsPostiveRecall = "0";
                    outbound.IsDam = "0";
                    outbound.HasDisc = "0";

                    outbound.QtyinBUoM = Convert.ToDecimal(outbound.Qty * outbound.CF);

                    sCmdUpsertPOQuantity.Append("Exec [dbo].[SP_Upsert_HU_OBD_Items_Picking] ");
                    sCmdUpsertPOQuantity.Append("@OBDNumber=" + DBLibrary.SQuote(outbound.Obdno));
                    sCmdUpsertPOQuantity.Append(",@LineNumber=" + DBLibrary.SQuote(outbound.Lineno));
                    sCmdUpsertPOQuantity.Append(",@SOHeaderID=" + DBLibrary.SQuote(outbound.POSOHeaderId));
                    sCmdUpsertPOQuantity.Append(",@MCode=" + DBLibrary.SQuote(outbound.MCode));
                    sCmdUpsertPOQuantity.Append(",@Location=" + DBLibrary.SQuote(outbound.Location));
                    sCmdUpsertPOQuantity.Append(",@Quantity=" + outbound.Qty);
                    sCmdUpsertPOQuantity.Append(",@IsDamaged=" + DBLibrary.SQuote(outbound.IsDam));
                    sCmdUpsertPOQuantity.Append(",@HasDiscrepancy=" + DBLibrary.SQuote(outbound.HasDisc));
                    sCmdUpsertPOQuantity.Append(",@CreatedBy=" + outbound.CreatedBy);
                    sCmdUpsertPOQuantity.Append(",@MfgDate=" + DBLibrary.SQuote(outbound.MfgDate));
                    sCmdUpsertPOQuantity.Append(",@ExpDate=" + DBLibrary.SQuote(outbound.ExpDate));
                    sCmdUpsertPOQuantity.Append(",@SerialNo=" + DBLibrary.SQuote(outbound.SerialNo));
                    sCmdUpsertPOQuantity.Append(",@BatchNo=" + DBLibrary.SQuote(outbound.BatchNo));
                    sCmdUpsertPOQuantity.Append(",@Projrefno=" + DBLibrary.SQuote(outbound.ProjectNo));
                    sCmdUpsertPOQuantity.Append(",@CartonCode =" + DBLibrary.SQuote(outbound.CartonNo));
                    sCmdUpsertPOQuantity.Append(",@ToCartonCode =" + DBLibrary.SQuote(outbound.ToCartonNo));
                    sCmdUpsertPOQuantity.Append(",@AssignedId =" + outbound.Assignedid);
                    sCmdUpsertPOQuantity.Append(",@SoDetailsIdnew=" + outbound.SODetailsID);
                    sCmdUpsertPOQuantity.Append(",@MRP=" + DBLibrary.SQuote(outbound.MRP));
                    sCmdUpsertPOQuantity.Append(",@AccountID=" + outbound.AccountId);
                    sCmdUpsertPOQuantity.Append(",@HUSize=" + outbound.HUSize);
                    sCmdUpsertPOQuantity.Append(",@HUNo=" + outbound.HUNo);
                }
                else
                {
                    outbound.GoodsMovementDetailsID = "0";
                    outbound.GoodsMovementTypeID = "2";
                    outbound.IsPostiveRecall = "0";
                    outbound.IsDam = "0";
                    outbound.HasDisc = "0";

                    outbound.QtyinBUoM = Convert.ToDecimal(outbound.Qty * outbound.CF);

                    sCmdUpsertPOQuantity.Append("Exec [dbo].[sp_INV_PickItemFromBin] ");
                    sCmdUpsertPOQuantity.Append("@OBDNumber=" + DBLibrary.SQuote(outbound.Obdno));
                    sCmdUpsertPOQuantity.Append(",@LineNumber=" + DBLibrary.SQuote(outbound.Lineno));
                    sCmdUpsertPOQuantity.Append(",@SOHeaderID=" + outbound.POSOHeaderId);
                    sCmdUpsertPOQuantity.Append(",@MCode=" + DBLibrary.SQuote(outbound.MCode));
                    sCmdUpsertPOQuantity.Append(",@Location=" + DBLibrary.SQuote(outbound.Location));
                    sCmdUpsertPOQuantity.Append(",@Quantity=" + outbound.Qty);
                    sCmdUpsertPOQuantity.Append(",@IsDamaged=" + DBLibrary.SQuote(outbound.IsDam));
                    sCmdUpsertPOQuantity.Append(",@HasDiscrepancy=" + DBLibrary.SQuote(outbound.HasDisc));
                    sCmdUpsertPOQuantity.Append(",@CreatedBy=" + outbound.CreatedBy);
                    sCmdUpsertPOQuantity.Append(",@MfgDate=" + DBLibrary.SQuote(outbound.MfgDate));
                    sCmdUpsertPOQuantity.Append(",@ExpDate=" + DBLibrary.SQuote(outbound.ExpDate));
                    sCmdUpsertPOQuantity.Append(",@SerialNo=" + DBLibrary.SQuote(outbound.SerialNo));
                    sCmdUpsertPOQuantity.Append(",@BatchNo=" + DBLibrary.SQuote(outbound.BatchNo));
                    sCmdUpsertPOQuantity.Append(",@Projrefno=" + DBLibrary.SQuote(outbound.ProjectNo));
                    sCmdUpsertPOQuantity.Append(",@CartonCode =" + DBLibrary.SQuote(outbound.CartonNo));
                    sCmdUpsertPOQuantity.Append(",@ToCartonCode =" + DBLibrary.SQuote(outbound.ToCartonNo));
                    sCmdUpsertPOQuantity.Append(",@AssignedId =" + outbound.Assignedid);
                    sCmdUpsertPOQuantity.Append(",@SoDetailsIdnew=" + outbound.SODetailsID);
                    sCmdUpsertPOQuantity.Append(",@MRP=" + DBLibrary.SQuote(outbound.MRP));
                    sCmdUpsertPOQuantity.Append(",@AccountID=" + outbound.AccountId);
                    sCmdUpsertPOQuantity.Append(",@HUSize=" + outbound.HUSize);
                    sCmdUpsertPOQuantity.Append(",@HUNo=" + outbound.HUNo);
                    sCmdUpsertPOQuantity.Append(",@IsPSN=" + outbound.IsPSN);
                    sCmdUpsertPOQuantity.Append(",@PSN=" + DBLibrary.SQuote(outbound.PSN));
                    //sCmdUpsertPOQuantity.Append(",@IsExcess=" + outbound.RID);
                }

                int Result = DbUtility.GetSqlN(sCmdUpsertPOQuantity.ToString(), ConnectionString);

                if (Result == -999)
                {
                    outbound.Result = "Qty Exceeded";
                }
                else if (Result == 1)
                {
                    outbound.Result = "Success";
                    await CheckItem(outbound);
                    await GetOBDPickedInfo(outbound);
                }
                else if (Result == -444)
                {
                    outbound.Result = Result.ToString();
                }
                else if (Result == -333)
                {
                    outbound.Result = Result.ToString();
                }
                else if (Result == 2)
                {
                    outbound.Result = "Stock not Available";
                }
                else if (Result == -111)
                {
                    outbound.Result = "The PSN you are trying to scan is already picked";
                }
                else
                {
                    outbound.Result = "Process failed,Please Contact Support team";
                }
                return outbound;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task CheckItem(BO.Outbound outbound)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            StringBuilder sbSqlStrings = new StringBuilder();

            sbSqlStrings.AppendLine("EXEC [dbo].[SP_ChangeGateEntryStatus] ");
            sbSqlStrings.AppendLine("@ShipmentId = " + outbound.OutboundId + ",");
            sbSqlStrings.AppendLine("@IsForInbound = 0");

            string Query = sbSqlStrings.ToString();
            DbUtility.GetSqlN(sbSqlStrings.ToString(), ConnectionString);
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<BO.Outbound> GetOBDPickedInfo(BO.Outbound outbound)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            string query = " EXEC [dbo].[USP_OBD_Get_AssginedDetailsForOutbound] @OBDID=" + outbound.OutboundId + ",@AssignedId=" + outbound.Assignedid;
            try
            {
                DataSet ds = DbUtility.GetDS(query, this.ConnectionString);

                if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count != 0)
                {
                    outbound.Assignedid = Convert.ToInt32(ds.Tables[0].Rows[0]["Assignedid"].ToString());
                    outbound.MaterialMasterId = ds.Tables[0].Rows[0]["MaterialMasterID"].ToString();
                    outbound.MCode = ds.Tables[0].Rows[0]["MCode"].ToString();
                    outbound.MDescription = ds.Tables[0].Rows[0]["MDescription"].ToString();
                    outbound.CartonNo = ds.Tables[0].Rows[0]["CartonCode"].ToString();
                    outbound.CartonId = ds.Tables[0].Rows[0]["CartonID"].ToString();
                    outbound.Location = ds.Tables[0].Rows[0]["Location"].ToString();
                    outbound.LocationId = ds.Tables[0].Rows[0]["LocationID"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[0]["LocationID"]) : 0;

                    outbound.AssignedQuantity = ds.Tables[0].Rows[0]["AssignedQuantity"].ToString();
                    outbound.PickedQty = ds.Tables[0].Rows[0]["PickedQty"].ToString();
                    outbound.OutboundId = ds.Tables[0].Rows[0]["OutboundID"].ToString();
                    outbound.SODetailsID = ds.Tables[0].Rows[0]["SODetailsID"].ToString();
                    outbound.SLocId = ds.Tables[0].Rows[0]["StorageLocationID"].ToString();
                    outbound.SLoc = ds.Tables[0].Rows[0]["Code"].ToString();

                    outbound.Lineno = ds.Tables[0].Rows[0]["LineNumber"].ToString();
                    outbound.MaterialMaster_IUoMID = ds.Tables[0].Rows[0]["MaterialMaster_UoMID"].ToString();

                    outbound.POSOHeaderId = ds.Tables[0].Rows[0]["SOHeaderID"].ToString();
                    outbound.PendingQty = ds.Tables[0].Rows[0]["PendingQty"].ToString();
                    outbound.MRP = ds.Tables[0].Rows[0]["MRP"].ToString();
                    outbound.HUNo = Convert.ToInt32(ds.Tables[0].Rows[0]["HUNo"].ToString());
                    outbound.HUSize = Convert.ToInt32(ds.Tables[0].Rows[0]["HUSize"].ToString());
                    outbound.IsPSN = Convert.ToInt32(ds.Tables[0].Rows[0]["IsPSN"].ToString());
                    outbound.CustomerName = ds.Tables[0].Rows[0]["Customer"].ToString();
                    outbound.DockLocation = ds.Tables[0].Rows[0]["DockName"].ToString();
                }
                else
                {
                    outbound.PendingQty = "0";
                }
                return outbound;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<bool> CheckOBDSO(Outbound outbound)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            try
            {
                bool result = true;
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                string query = " EXEC [dbo].[USP_GET_OBD_SODetails_Picking] @SONO=" + DBLibrary.SQuote(outbound.SONumber) + ",@AccountID=" + outbound.AccountID + ",@UserID=" + outbound.UserId + "";

                DataSet _dsResults = DbUtility.GetDS(query, this.ConnectionString);

                if (_dsResults != null && _dsResults.Tables.Count != 0 && _dsResults.Tables[0].Rows.Count != 0)
                {
                    result = true;
                }
                else
                {
                    throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = "Invalid SO Number", ShowAsWarning = true };
                }

                return result;
            }
            catch (WMSExceptionMessage excp)
            {
                throw excp;
            }
            catch (Exception excp)
            {
                ExceptionData oExcpData = new ExceptionData();
                oExcpData.AddInputs("oCriteria", outbound);

                ExceptionHandling.LogException(excp, _ClassCode + "001", oExcpData);
                throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = ErrorMessages.WMSExceptionMessage, ShowAsCriticalError = true };
            }
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<List<Outbound>> GetOBDNosUnderSO(Outbound outbound)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            try
            {
                List<Outbound> _lstOutbounds = new List<Outbound>();
#pragma warning disable CS0219 // The variable 'result' is assigned but its value is never used
                bool result = true;
#pragma warning restore CS0219 // The variable 'result' is assigned but its value is never used
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                string query = "EXEC [dbo].[USP_Get_OBD_OutboundDetailsUnderSO] @SONO=" + DBLibrary.SQuote(outbound.SONumber) + ",@AccountID=" + outbound.AccountID + ",@UserID=" + outbound.UserId + "";

                DataSet _dsPickLists = DbUtility.GetDS(query, this.ConnectionString);

                foreach (DataRow _dtPack in _dsPickLists.Tables[0].Rows)
                {
                    Outbound _oOutbound = new Outbound()
                    {
                        OBDNumber = _dtPack["OBDNumber"].ToString(),
                        OutboundID = ConversionUtility.ConvertToInt(_dtPack["OutboundID"].ToString())
                    };
                    _lstOutbounds.Add(_oOutbound);
                }

                return _lstOutbounds;
            }
            catch (WMSExceptionMessage excp)
            {
                throw excp;
            }
            catch (Exception excp)
            {
                ExceptionData oExcpData = new ExceptionData();
                oExcpData.AddInputs("oCriteria", outbound);

                ExceptionHandling.LogException(excp, _ClassCode + "001", oExcpData);
                throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = ErrorMessages.WMSExceptionMessage, ShowAsCriticalError = true };
            }
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<string> CheckContainerOBD(string CartonNo, string OutboundID)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            string resultvalue;
            string sql = "select CartonID AS N from INV_Carton where CartonCode=" + DBLibrary.SQuote(CartonNo) + " and IsActive=1 and WareHouseID=(select WarehouseID from OBD_RefWarehouse_Details where OutboundID=" + OutboundID + ")";
            int result = DbUtility.GetSqlN(sql, ConnectionString);
            if (result != 0)
            {
                resultvalue = "";
            }
            else
            {
                resultvalue = "Not a valid Container.";
            }
            return resultvalue;
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<List<Outbound>> GetOBDItemsForPicking(Outbound outbound)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            try
            {
                List<Outbound> _lstOutbounds = new List<Outbound>();
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();

                string query = "EXEC [dbo].[USP_OBD_Get_AssginedDetailsForOutbound] @OBDID=" + outbound.OutboundID + "";
                DataSet _dsPickLists = DbUtility.GetDS(query, this.ConnectionString);

                if (_dsPickLists != null && _dsPickLists.Tables.Count != 0 && _dsPickLists.Tables[0].Rows.Count != 0)
                {
                    foreach (DataRow _dtPack in _dsPickLists.Tables[0].Rows)
                    {
                        Outbound _oOutbound = new Outbound()
                        {
                            OutboundID = ConversionUtility.ConvertToInt(_dtPack["OutboundID"].ToString()),
                            OBDNumber = _dtPack["OBDNumber"].ToString(),
                            Assignedid = ConversionUtility.ConvertToInt(_dtPack["Assignedid"].ToString()),
                            MaterialMasterID = ConversionUtility.ConvertToInt(_dtPack["MaterialMasterID"].ToString()),
                            Mcode = _dtPack["MCode"].ToString(),
                            MDescription = _dtPack["MDescription"].ToString(),
                            CartonNo = _dtPack["CartonCode"].ToString(),
                            CartonID = ConversionUtility.ConvertToInt(_dtPack["CartonID"].ToString()),
                            Location = _dtPack["Location"].ToString(),
                            LocationId = ConversionUtility.ConvertToInt(_dtPack["LocationID"].ToString()),

                            AssignedQuantity = ConversionUtility.ConvertToDecimal(_dtPack["AssignedQuantity"].ToString()),
                            PickedQty = ConversionUtility.ConvertToDecimal(_dtPack["PickedQty"].ToString()),
                            SoDetailsID = ConversionUtility.ConvertToInt(_dtPack["SODetailsID"].ToString()),
                            SLocId = ConversionUtility.ConvertToInt(_dtPack["StorageLocationID"].ToString()),
                            SLoc = _dtPack["Code"].ToString(),
                            Lineno = ConversionUtility.ConvertToInt(_dtPack["LineNumber"].ToString()),
                            MaterialMaster_IUoMID = ConversionUtility.ConvertToInt(_dtPack["MaterialMaster_UoMID"].ToString()),

                            POSOHeaderId = ConversionUtility.ConvertToInt(_dtPack["SOHeaderID"].ToString()),
                            PendingQty = ConversionUtility.ConvertToDecimal(_dtPack["PendingQty"].ToString()),
                            MRP = _dtPack["MRP"].ToString(),
                            HUNo = _dtPack["HUNo"].ToString(),
                            HUSize = _dtPack["HUSize"].ToString(),
                            IsPSN = ConversionUtility.ConvertToInt(_dtPack["IsPSN"].ToString()),
                            CustomerName = _dtPack["Customer"].ToString(),
                            DockLocation = _dtPack["DockName"].ToString(),
                            BatchNo = _dtPack["BatchNo"].ToString(),
                        };
                        _lstOutbounds.Add(_oOutbound);
                    }
                }
                else
                {
                    throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = "No Data Found For Given Search Criteria.", ShowAsWarning = true };
                }
                return _lstOutbounds;
            }
            catch (WMSExceptionMessage excp)
            {
                throw excp;
            }
            catch (Exception excp)
            {
                ExceptionData oExcpData = new ExceptionData();
                oExcpData.AddInputs("oCriteria", outbound);

                ExceptionHandling.LogException(excp, _ClassCode + "001", oExcpData);
                throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = ErrorMessages.WMSExceptionMessage, ShowAsCriticalError = true };
            }
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<DataSet> GetOpenVLPDNos(BO.VLPD vLPD)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();

            string query = "EXEC [dbo].[Get_VLPDList_HHT] @AccountId=" + vLPD.AccountId + "";
            DataSet ds = DbUtility.GetDS(query, this.ConnectionString);

            return ds;
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<BO.VLPD> GetItemToPick(BO.VLPD VLPD)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();

            if (VLPD.TransferRequestId != 0)
            {
                string queryresult = "EXEC [dbo].[GEN_PICK_INTERNALTRANSFER_HHT] @TransferRequestID=" + VLPD.TransferRequestId;
                try
                {
                    DataSet ds = DbUtility.GetDS(queryresult, this.ConnectionString);

                    if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count != 0)
                    {
                        VLPD.TransferRequestId = Convert.ToInt32(ds.Tables[0].Rows[0]["TransferRequestID"].ToString());
                        VLPD.VLPDNo = ds.Tables[0].Rows[0]["VLPDNumber"].ToString();
                        VLPD.Assignedid = Convert.ToInt32(ds.Tables[0].Rows[0]["AssignedID"].ToString());
                        VLPD.MaterialMasterId = Convert.ToInt32(ds.Tables[0].Rows[0]["MaterialMasterID"].ToString());
                        VLPD.MCode = ds.Tables[0].Rows[0]["MCode"].ToString();
                        VLPD.MDescription = ds.Tables[0].Rows[0]["MDescription"].ToString();
                        VLPD.FromCartonCode = ds.Tables[0].Rows[0]["CartonCode"].ToString();
                        VLPD.FromCartonID = Convert.ToInt32(ds.Tables[0].Rows[0]["CartonID"].ToString());
                        VLPD.Location = ds.Tables[0].Rows[0]["Location"].ToString();
                        VLPD.LocationID = Convert.ToInt32(ds.Tables[0].Rows[0]["LocationID"].ToString());
                        VLPD.MfgDate = ds.Tables[0].Rows[0]["MfgDate"].ToString();
                        VLPD.ExpDate = ds.Tables[0].Rows[0]["ExpDate"].ToString();
                        VLPD.SerialNo = ds.Tables[0].Rows[0]["SerialNo"].ToString();
                        VLPD.BatchNo = ds.Tables[0].Rows[0]["BatchNo"].ToString();
                        VLPD.ProjectRefNo = ds.Tables[0].Rows[0]["ProjectRefNo"].ToString();
                        VLPD.AssignedQuantity = ds.Tables[0].Rows[0]["AssignedQuantity"].ToString();
                        VLPD.PickedQty = ds.Tables[0].Rows[0]["PickedQty"].ToString();
                        VLPD.PendingQty = ds.Tables[0].Rows[0]["PendingQty"].ToString();
                        VLPD.OutboundID = ds.Tables[0].Rows[0]["OutboundID"].ToString();
                        VLPD.SODetailsID = ds.Tables[0].Rows[0]["SODetailsID"].ToString();
                        VLPD.StorageLocationID = ds.Tables[0].Rows[0]["StorageLocationID"].ToString();
                        VLPD.StorageLocation = ds.Tables[0].Rows[0]["SLOC"].ToString();
                        VLPD.GoodsmomentDeatilsId = Convert.ToInt32(ds.Tables[0].Rows[0]["GoodsMovementDetailsID"].ToString());
                        VLPD.TransferRequestDetailsId = Convert.ToInt32(ds.Tables[0].Rows[0]["TransferRequestDetailsID"].ToString());
                        VLPD.MRP = ds.Tables[0].Rows[0]["MRP"].ToString();
                    }
                    else
                    {
                        VLPD.PendingQty = "";
                    }

                    return VLPD;
                }
                catch (Exception ex)
                {
                    throw ex;

                }
            }
            else
            {
                string query = " EXEC [dbo].[GEN_VLPD_ITEMS_ALLOCATED_LIST_OBDWISE_HHT] @VLPDID=" + VLPD.VLPDId + ",@TransferRequestID=" + VLPD.TransferRequestId;
                try
                {
                    DataSet ds = DbUtility.GetDS(query, this.ConnectionString);

                    if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count != 0)
                    {
                        VLPD.TransferRequestId = Convert.ToInt32(ds.Tables[0].Rows[0]["TransferRequestID"].ToString());
                        VLPD.VLPDNo = ds.Tables[0].Rows[0]["VLPDNumber"].ToString();
                        VLPD.Assignedid = Convert.ToInt32(ds.Tables[0].Rows[0]["AssignedID"].ToString());
                        VLPD.MaterialMasterId = Convert.ToInt32(ds.Tables[0].Rows[0]["MaterialMasterID"].ToString());
                        VLPD.MCode = ds.Tables[0].Rows[0]["MCode"].ToString();
                        VLPD.MDescription = ds.Tables[0].Rows[0]["MDescription"].ToString();
                        VLPD.FromCartonCode = ds.Tables[0].Rows[0]["CartonCode"].ToString();
                        VLPD.FromCartonID = Convert.ToInt32(ds.Tables[0].Rows[0]["CartonID"].ToString());
                        VLPD.Location = ds.Tables[0].Rows[0]["Location"].ToString();
                        VLPD.LocationID = Convert.ToInt32(ds.Tables[0].Rows[0]["LocationID"].ToString());
                        VLPD.MfgDate = ds.Tables[0].Rows[0]["MfgDate"].ToString();
                        VLPD.ExpDate = ds.Tables[0].Rows[0]["ExpDate"].ToString();
                        VLPD.SerialNo = ds.Tables[0].Rows[0]["SerialNo"].ToString();
                        VLPD.BatchNo = ds.Tables[0].Rows[0]["BatchNo"].ToString();
                        VLPD.ProjectRefNo = ds.Tables[0].Rows[0]["ProjectRefNo"].ToString();
                        VLPD.AssignedQuantity = ds.Tables[0].Rows[0]["AssignedQuantity"].ToString();
                        VLPD.PickedQty = ds.Tables[0].Rows[0]["PickedQty"].ToString();
                        VLPD.PendingQty = ds.Tables[0].Rows[0]["PendingQty"].ToString();
                        VLPD.OutboundID = ds.Tables[0].Rows[0]["OutboundID"].ToString();
                        VLPD.SODetailsID = ds.Tables[0].Rows[0]["SODetailsID"].ToString();
                        VLPD.StorageLocationID = ds.Tables[0].Rows[0]["StorageLocationID"].ToString();
                        VLPD.StorageLocation = ds.Tables[0].Rows[0]["SLOC"].ToString();
                        VLPD.GoodsmomentDeatilsId = Convert.ToInt32(ds.Tables[0].Rows[0]["GoodsMovementDetailsID"].ToString());
                        VLPD.TransferRequestDetailsId = Convert.ToInt32(ds.Tables[0].Rows[0]["TransferRequestDetailsID"].ToString());
                        VLPD.MRP = ds.Tables[0].Rows[0]["MRP"].ToString();
                    }
                    else
                    {
                        VLPD.PendingQty = "0";
                    }

                    return VLPD;
                }
                catch (Exception ex)
                {
                    throw ex;

                }
            }
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<BO.VLPD> VLPDSkipItem(BO.VLPD VLPD)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();

            string drlStatement = "EXEC [dbo].[USP_SET_OBD_SKIP]  @VLPDAssignID=" + Convert.ToInt32(VLPD.Assignedid) + ",@MCode=" + DBLibrary.SQuote(VLPD.MCode) + ",@MfgDate=" + (VLPD.MfgDate != "" ? DBLibrary.SQuote(VLPD.MfgDate) : "''") + ",@ExpDate=" + (VLPD.ExpDate != "" ? DBLibrary.SQuote(VLPD.ExpDate) : "''") + ",@SerialNo=" + (VLPD.SerialNo != "" ? DBLibrary.SQuote(VLPD.SerialNo) : "''") + ",@BatchNo = " + (VLPD.BatchNo != "" ? DBLibrary.SQuote(VLPD.BatchNo) : "''") + ",@ProjectRefNo = " + (VLPD.ProjectRefNo != "" ? DBLibrary.SQuote(VLPD.ProjectRefNo) : "''") + ",@SkipQty = " + (VLPD.SkipQty) + ", @CreatedBy=" + Convert.ToInt32(VLPD.UserId) + ",@CartonCode=" + DBLibrary.SQuote(VLPD.FromCartonCode) + ",@Location=" + DBLibrary.SQuote(VLPD.Location) + ",@Reason=" + DBLibrary.SQuote(VLPD.SkipReason) + ",@VLPDID=" + VLPD.VLPDId;
            string result = DbUtility.GetSqlS(drlStatement, ConnectionString).ToString();

            VLPD.Result = result.ToString();

            if (VLPD.Flag == 1)
            {
                return VLPD;
            }

            return VLPD;
        }

        public async Task<BO.VLPD> UpsertPickItem(BO.VLPD Pickdata)
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();

            if (Pickdata.ToCartonCode != "")
            {
                string Query = "select count(*) as N from OBD_VLPD_PickedItems where  IsDeleted = 0  AND IsActive = 1 AND  VLPDID=" + Pickdata.VLPDId + "";
                int result = DbUtility.GetSqlN(Query.ToString(), ConnectionString);

                if (result > 0)
                {
                    string CartonPickingQuery = "select count(*) as N from OBD_VLPD_PickedItems WHERE IsDeleted = 0  AND IsActive = 1  and ToCartonID!=0 and ToCartonID is not null AND VLPDID = " + Pickdata.VLPDId + "";
                    int IsCartonPicking = DbUtility.GetSqlN(CartonPickingQuery.ToString(), ConnectionString);

                    if (IsCartonPicking == 0)
                    {
                        Pickdata.Result = "-444";
                        return Pickdata;
                    }
                }
            }
            try
            {
                StringBuilder dmlStatement = new StringBuilder();
                dmlStatement.Append("DECLARE @NewVLPDID_ID int");
                dmlStatement.Append(" EXEC  [dbo].[UPSERT_VLPD_PICKEDINFO_HHT] ");
                dmlStatement.Append("@MaterialMasterID =" + Pickdata.MaterialMasterId + ",");
                dmlStatement.Append("@AssignID=" + Pickdata.Assignedid + ",");
                dmlStatement.Append("@PickQty=" + Pickdata.PickedQty + ",");
                dmlStatement.Append("@LocationID=" + Pickdata.LocationID + ",");
                dmlStatement.Append("@CartonID=" + Pickdata.FromCartonID + ",");
                dmlStatement.Append("@MfgDate=" + (DBLibrary.SQuote(Pickdata.MfgDate)) + ",");
                dmlStatement.Append("@ExpDate=" + (DBLibrary.SQuote(Pickdata.ExpDate)) + ",");
                dmlStatement.Append("@SerialNo=" + (DBLibrary.SQuote(Pickdata.SerialNo)) + ",");
                dmlStatement.Append("@BatchNo=" + (DBLibrary.SQuote(Pickdata.BatchNo)) + ",");
                dmlStatement.Append("@VLPDID=" + Pickdata.VLPDId + ",");
                dmlStatement.Append("@ProjectRefNo=" + (DBLibrary.SQuote(Pickdata.ProjectRefNo)) + ",");
                dmlStatement.Append("@CreatedBy=" + Pickdata.UserId + ",");
                dmlStatement.Append("@OutboundID=" + (Pickdata.OutboundID != "" ? Pickdata.OutboundID : "NULL") + ",");
                dmlStatement.Append("@SODetailsID=" + (Pickdata.SODetailsID != "" ? Pickdata.SODetailsID : "NULL") + ",");
                dmlStatement.Append("@StorageLocationID=" + DBLibrary.SQuote(Pickdata.StorageLocationID) + ",");
                dmlStatement.Append("@TransferRequestId=" + Pickdata.TransferRequestId + ",");
                dmlStatement.Append("@TransferRequestDetailsId=" + Pickdata.TransferRequestDetailsId + ",");
                dmlStatement.Append("@ToCartonCode=" + DBLibrary.SQuote(Pickdata.ToCartonCode) + ",");
                dmlStatement.Append("@MRP=" + DBLibrary.SQuote(Pickdata.MRP) + ",");
                dmlStatement.Append("@NewVLPDID=@NewVLPDID_ID OUTPUT select @NewVLPDID_ID AS N;");

                int Result = DbUtility.GetSqlN(dmlStatement.ToString(), ConnectionString);

                Pickdata.Result = Result.ToString();

                if (Pickdata.Result != "0")
                {
                    //if(Pickdata.TransferRequestId!=0)
                    //{
                    //    UpdatePickItemForInternalTransfer(Pickdata);
                    //}
                    //else
                    //{
                    //    SetFastMovingLocation(Pickdata);
                    //}
                    Pickdata = await GetPickedQty(Pickdata);
                    await CheckItem(Pickdata);
                    await GetItemToPick(Pickdata);
                }
                //  DB.ExecuteSQL(dmlStatement.ToString());
                return Pickdata;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<BO.VLPD> GetPickedQty(BO.VLPD VLPd)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();

            string query = "EXEC [dbo].[Get_PickedQty_HHT] @VLPDID=" + VLPd.VLPDId + ",@Mcode=" + DBLibrary.SQuote(VLPd.MCode) + ",@TransferrequestID=" + VLPd.TransferRequestId + "";
            DataSet ds = DbUtility.GetDS(query, this.ConnectionString);

            if (ds.Tables[0].Rows.Count != 0)
            {
                VLPd.TotalPickedQty = Convert.ToDecimal(ds.Tables[0].Rows[0]["PickedQty"].ToString());
            }

            return VLPd;
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task CheckItem(BO.VLPD VLPd)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            StringBuilder sbSqlStrings = new StringBuilder();

            sbSqlStrings.AppendLine("EXEC [dbo].[SP_ChangeGateEntryStatus] ");
            sbSqlStrings.AppendLine("@ShipmentId = " + VLPd.VLPDId + ",");
            sbSqlStrings.AppendLine("@IsForInbound = 0");

            string Query = sbSqlStrings.ToString();
            DbUtility.GetSqlN(sbSqlStrings.ToString(), ConnectionString);
        }

        public DataSet GetVLPDPickedList(BO.VLPD vLPD, out string Result)
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();

            string query = "EXEC [dbo].[sp_GET_VLPD_PickedInfoList_HHT] @VLPDID=" + vLPD.VLPDId + ",@MCode=" + DBLibrary.SQuote(vLPD.MCode) + ",@MFGDate=" + DBLibrary.SQuote(vLPD.MfgDate) + ",@EXPDate=" + DBLibrary.SQuote(vLPD.ExpDate);
            query += ",@Batch=" + DBLibrary.SQuote(vLPD.BatchNo) + ",@SerialNo=" + DBLibrary.SQuote(vLPD.SerialNo) + ",@ProjectRef=" + DBLibrary.SQuote(vLPD.ProjectRefNo) + ",@MRP=" + DBLibrary.SQuote(vLPD.MRP) + "";

            DataSet ds = DbUtility.GetDS(query, this.ConnectionString);

            Result = ds.Tables[1].Rows[0][0].ToString();
            return ds;
        }

        public DataSet GetOBDPickedList(BO.Outbound outbound, out string Result)
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();

            string query = "[dbo].[sp_GET_VLPD_PickedInfoList_HHT] @OutboundID=" + outbound.OutboundId + ",@MCode=" + DBLibrary.SQuote(outbound.MCode) + ",@MFGDate=" + DBLibrary.SQuote(outbound.MfgDate) + ",@EXPDate=" + DBLibrary.SQuote(outbound.ExpDate);
            query += ",@Batch=" + DBLibrary.SQuote(outbound.BatchNo) + ",@SerialNo=" + DBLibrary.SQuote(outbound.SerialNo) + ",@ProjectRef=" + DBLibrary.SQuote(outbound.ProjectNo) + ",@MRP=" + DBLibrary.SQuote(outbound.MRP) + "";

            DataSet ds = DbUtility.GetDS(query, this.ConnectionString);

            Result = ds.Tables[1].Rows[0][0].ToString();
            return ds;
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<BO.VLPD> DeleteVLPDPickedItems(BO.VLPD vLPD)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();

            var Query = "[dbo].[DELETE_PICKEDITEMS_FOR_VLPD_OUTBOUND] @VLPDPickedID=" + vLPD.PickedId + ",@CreatedBy=" + vLPD.UserId + "";
            int value = DbUtility.GetSqlN(Query, ConnectionString);

            if (value == 1)
            {
                vLPD.Result = "Deleted successfully";
            }
            else
            {
                vLPD.Result = "Error while deleting.";
            }
            return vLPD;
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<DataSet> GetOpenVLPDNosForSorting(BO.VLPD vLPD)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();

            string query = "EXEC [dbo].[SP_GET_VLPD_SortageVLPDList] ";
            DataSet ds = DbUtility.GetDS(query, this.ConnectionString);

            return ds;
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<DataSet> GetOpenLoadsheetList(string Tenantid, string AccountId)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();

            string query = "EXEC SP_OpenLoadList @TenantID = " + Tenantid + ",@AccountID=" + AccountId + "";
            DataSet ds = DbUtility.GetDS(query, this.ConnectionString);

            return ds;
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<DataSet> GetPendingOBDListForLoading(string Tenantid, string AccountId)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();

            string query = "EXEC SP_Pending_Load_OBD @AcountId = " + AccountId + ", @TenantID = " + Tenantid + "";
            DataSet ds = DbUtility.GetDS(query, this.ConnectionString);

            return ds;
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<string> UpsertLoadCreated(BO.VLPD VLPD)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                string Query = "EXEC SP_GET_Upsert_LoadSheet @TenantId = " + VLPD.TenantId + ", @AccountID = " + VLPD.AccountId + ", @VEHICLENO = " + DBLibrary.SQuote(VLPD.Vehicle) + ", @OBDSNUMBER = " + DBLibrary.SQuote(VLPD.OBDNumber) + ", @DRIVERNO = " + DBLibrary.SQuote(VLPD.DriverNo) + ", @DRIVERNAME = " + DBLibrary.SQuote(VLPD.DriverName) + ", @LRNumber = " + DBLibrary.SQuote(VLPD.LRnumber) + ", @USERID = " + VLPD.UserId + "";
                return DbUtility.GetSqlS(Query, ConnectionString).ToString();
            }
            catch (Exception Ex)
            {
                return Ex.Message;
            }
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<string> UpsertLoad(BO.VLPD VLPD)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                string Query = "EXEC SP_OBD_Upsert_Loading @MCODE = " + DBLibrary.SQuote(VLPD.MCode) + ", @LOADNUMBER = " + DBLibrary.SQuote(VLPD.VLPDNo) + ", @MfgDate = " + DBLibrary.SQuote(VLPD.MfgDate) + ", @ExpDate = " + DBLibrary.SQuote(VLPD.ExpDate) + ", @BatchNo = " + DBLibrary.SQuote(VLPD.BatchNo) + ", @SerialNo = " + DBLibrary.SQuote(VLPD.SerialNo) + ", @ProjectRefNo = " + DBLibrary.SQuote(VLPD.ProjectRefNo) + ", @MRP = " + DBLibrary.SQuote(VLPD.MRP) + ", @LOADEDQTY = " + DBLibrary.SQuote(VLPD.PickedQty) + ",@UserID = " + VLPD.UserId + "";
                return DbUtility.GetSqlS(Query, ConnectionString).ToString();
            }
            catch (Exception Ex)
            {
                return Ex.Message;
            }
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<string> LoadVerification(string LoadNumber, string UserId)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                string Query = "EXEC [dbo].[SP_Load_CheckLoadingStatus] @LoadSheetNumber = " + DBLibrary.SQuote(LoadNumber) + ", @UserId = " + UserId + "";
                string result = DbUtility.GetSqlS(Query, ConnectionString).ToString();

                if (result == "1")
                {
                    string strOBDList = "EXEC [dbo].[SP_Load_GetOBDNumbersByLoadSheet] @LoadSheetRef=" + DBLibrary.SQuote(LoadNumber);

                    DataSet dsOBDList = DbUtility.GetDS(strOBDList, this.ConnectionString);

                    if (dsOBDList != null && dsOBDList.Tables[0].Rows.Count != 0)
                    {
                        int totalOBDCOunt = dsOBDList.Tables[0].Rows.Count;
                        int successOBD = 0;
                        foreach (DataRow dataROw in dsOBDList.Tables[0].Rows)
                        {
                            string outboundID = dataROw["OutboundID"].ToString();

                            string strQueryForStockOut = "EXEC [dbo].[sp_OBD_MoveStockOutByPickingData] @OutboundID=" + outboundID + ",@CreatedBy=" + UserId;
                            DataSet dsPickingResult = DbUtility.GetDS(strQueryForStockOut, this.ConnectionString);

                            if (dsPickingResult != null && dsPickingResult.Tables.Count != 0)
                            {
                                if (dsPickingResult.Tables.Count == 2)
                                {
                                    if (dsPickingResult.Tables[0].Rows[0][0].ToString() == "-1")
                                    {
                                        result = "Picking is not completed";
                                    }
                                    else if (dsPickingResult.Tables[0].Rows[0][0].ToString() == "-2")
                                    {
                                        result = "Picked quantity not available at dock location";
                                    }
                                    else if (dsPickingResult.Tables[0].Rows[0][0].ToString() == "-3")
                                    {
                                        result = "There is an issue in while processing stock out";
                                    }
                                    else if (dsPickingResult.Tables[0].Rows[0][0].ToString() == "-4")
                                    {
                                        result = "Unexpected error  while processing stock out";
                                    }
                                    else if (dsPickingResult.Tables[0].Rows[0][0].ToString() == "-5")
                                    {
                                        result = "There is no pending quantity for stock out";
                                    }
                                }
                                else if (dsPickingResult.Tables.Count == 1)
                                {
                                    StringBuilder sbPGIQuery = new StringBuilder();
                                    sbPGIQuery.Append("DECLARE @NewUpdateOutboundID int;  ");
                                    sbPGIQuery.Append("EXEC[dbo].[sp_OBD_UpsertPGI]");
                                    sbPGIQuery.Append("@OutboundID = " + outboundID);
                                    sbPGIQuery.Append(",@PGIDoneBy=" + UserId);
                                    sbPGIQuery.Append(",@DocumentTypeID=" + 1);
                                    sbPGIQuery.Append(",@UserID=" + UserId);
                                    sbPGIQuery.Append(",@OB_RefWarehouse_DetailsID=0");
                                    sbPGIQuery.Append(",@IsRequestFromPDT=1");
                                    sbPGIQuery.Append(",@UpdatedBy=" + UserId);
                                    sbPGIQuery.Append(",@NewOutboundID=@NewUpdateOutboundID OUTPUT ;");
                                    sbPGIQuery.Append(" SELECT @NewUpdateOutboundID AS N ;");

                                    DbUtility.GetSqlN(sbPGIQuery.ToString(), ConnectionString);

                                    string HUQuery = "EXEC [dbo].[SP_Upsert_HU_GoodsOut] @outboundID=" + outboundID + ",@UserID=" + UserId;
                                    DataSet dsHu = DbUtility.GetDS(HUQuery, this.ConnectionString);

                                    if (dsHu.Tables[0].Rows[0][0].ToString() == "Done")
                                    {
                                        successOBD = successOBD + 1;
                                    }
                                }
                            }
                            else
                            {
                                result = "Error while processing stock out transaction, please contact support ";
                            }
                        }
                        if (successOBD == totalOBDCOunt)
                        {
                            string SQLQuery = "UPDATE OBD_LoadSheet_Header SET StatusID = 3 WHERE LoadSheetNo = @LoadSheetNo";

                            using (SqlConnection connection = new SqlConnection(ConnectionString))
                            {
                                using (SqlCommand command = new SqlCommand(SQLQuery, connection))
                                {
                                    command.Parameters.AddWithValue("@LoadSheetNo", LoadNumber);
                                    command.ExecuteNonQuery();
                                }
                            }
                            return "PGI Updated";
                        }
                        else
                        {
                            return "Unable to cloase loadsheet as some deliveries are not processed";
                        }
                    }
                    else
                    {
                        return "Error while closing loadsheet";
                    }
                }
                else
                {
                    return result;
                }
            }
            catch (Exception Ex)
            {
                return Ex.Message;
            }
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<List<Outbound>> GetMaterialsUnderSOForPacking(string SONumber, int AccountID, int UserId)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            try
            {
                List<Outbound> lOutbound = new List<Outbound>();
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                StringBuilder sbSqlStrings = new StringBuilder();

                sbSqlStrings.AppendLine("EXEC [dbo].[USP_GetMCodesUnderSO] ");
                sbSqlStrings.AppendLine("@SONumber = " + DBLibrary.SQuote(SONumber) + ",");
                sbSqlStrings.AppendLine("@AccountID = " + AccountID + ",");
                sbSqlStrings.AppendLine("@UserId = " + UserId + "");

                string Query = sbSqlStrings.ToString();
                DataSet _dsPickLists = DbUtility.GetDS(Query, this.ConnectionString);

                if (_dsPickLists != null)
                {
                    if (_dsPickLists.Tables.Count > 0)
                    {
                        foreach (DataRow _drPickList in _dsPickLists.Tables[0].Rows)
                        {
                            Outbound _oOutbound = new Outbound()
                            {
                                //MaterialMasterID = ConversionUtility.ConvertToInt(_drPickList["MaterialMasterID"].ToString()),
                                Mcode = _drPickList["MCode"].ToString(),
                                PickedQty = ConversionUtility.ConvertToDecimal(_drPickList["PickedQty"].ToString()),
                                PackedQty = ConversionUtility.ConvertToDecimal(_drPickList["PackedQty"].ToString()),
                                // OutboundID = ConversionUtility.ConvertToInt(_drPickList["OutboundID"].ToString()),
                                CustomerName = _drPickList["CustomerName"].ToString(),
                                SOHeaderID = ConversionUtility.ConvertToInt(_drPickList["SOHeaderID"].ToString()),
                                BusinessType = _drPickList["BusinessType"].ToString(),
                                //OBDNumber = _drPickList["OBDNumber"].ToString(),
                                MFGDate = _drPickList["MfgDate"].ToString(),
                                EXPDate = _drPickList["ExpDate"].ToString(),
                                SerialNo = _drPickList["SerialNo"].ToString(),
                                ProjectRefNo = _drPickList["ProjectRefNo"].ToString(),
                                BatchNo = _drPickList["BatchNo"].ToString(),
                                MRP = _drPickList["MRP"].ToString(),
                                HUNo = _drPickList["HUNo"].ToString(),
                                HUSize = _drPickList["HUSize"].ToString()
                            };

                            lOutbound.Add(_oOutbound);
                        }
                    }
                }

                return lOutbound;
            }
            catch (WMSExceptionMessage excp)
            {
                throw excp;
            }
            catch (Exception excp)
            {
                ExceptionData oExcpData = new ExceptionData();
                oExcpData.AddInputs("oCriteria", SONumber);

                ExceptionHandling.LogException(excp, _ClassCode + "001", oExcpData);
                throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = ErrorMessages.WMSExceptionMessage, ShowAsCriticalError = true };
            }
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<List<Outbound>> GetMSPsForPacking(string SodetailsID)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            try
            {
                List<Outbound> lOutbound = new List<Outbound>();
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                StringBuilder sbSqlStrings = new StringBuilder();

                sbSqlStrings.AppendLine("EXEC [dbo].[USP_API_GetMSPsForPacking] ");
                sbSqlStrings.AppendLine("@SoDetailsID = " + DBLibrary.SQuote(SodetailsID) + "");

                string Query = sbSqlStrings.ToString();
                DataSet _dsPickLists = DbUtility.GetDS(Query, this.ConnectionString);

                if (_dsPickLists != null)
                {
                    if (_dsPickLists.Tables.Count > 0)
                    {
                        foreach (DataRow _drPickList in _dsPickLists.Tables[0].Rows)
                        {
                            Outbound _oOutbound = new Outbound()
                            {
                                MFGDate = _drPickList["MfgDate"].ToString(),
                                EXPDate = _drPickList["ExpDate"].ToString(),
                                SerialNo = _drPickList["SerialNo"].ToString(),
                                ProjectRefNo = _drPickList["ProjectRefNo"].ToString(),
                                BatchNo = _drPickList["BatchNo"].ToString(),
                                MRP = _drPickList["MRP"].ToString()
                            };

                            lOutbound.Add(_oOutbound);
                        }
                    }
                }

                return lOutbound;
            }
            catch (WMSExceptionMessage excp)
            {
                throw excp;
            }
            catch (Exception excp)
            {
                ExceptionData oExcpData = new ExceptionData();
                oExcpData.AddInputs("oCriteria", SodetailsID);

                ExceptionHandling.LogException(excp, _ClassCode + "001", oExcpData);
                throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = ErrorMessages.WMSExceptionMessage, ShowAsCriticalError = true };
            }
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<Outbound> UpsertPackItem(Outbound outbound)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                StringBuilder sbSqlStrings = new StringBuilder();

                sbSqlStrings.AppendLine("EXEC [dbo].[USP_API_UpsertPackItem] ");
                sbSqlStrings.AppendLine("@CreatedBy = " + outbound.UserId + ",");
                sbSqlStrings.AppendLine("@OutBoundId = " + outbound.OutboundID + ",");
                sbSqlStrings.AppendLine("@PSNHeaderID = " + outbound.PSNID + ",");
                sbSqlStrings.AppendLine("@PSNDetailsID = " + outbound.PSNDetailsID + ",");
                sbSqlStrings.AppendLine("@Material = " + DBLibrary.SQuote(outbound.Mcode) + ",");
                sbSqlStrings.AppendLine("@PackedQty = " + outbound.PackedQty + ",");
                sbSqlStrings.AppendLine("@PickedQty = " + outbound.PickedQty + ",");
                sbSqlStrings.AppendLine("@CartonNumber = " + DBLibrary.SQuote(outbound.CartonNo) + ",");
                sbSqlStrings.AppendLine("@MfgDate = " + DBLibrary.SQuote(outbound.MFGDate) + ",");
                sbSqlStrings.AppendLine("@ExpDate = " + DBLibrary.SQuote(outbound.EXPDate) + ",");
                sbSqlStrings.AppendLine("@BatchNo = " + DBLibrary.SQuote(outbound.BatchNo) + ",");
                sbSqlStrings.AppendLine("@SerialNo = " + DBLibrary.SQuote(outbound.SerialNo) + ",");
                sbSqlStrings.AppendLine("@ProjectRefNo = " + DBLibrary.SQuote(outbound.ProjectRefNo) + ",");
                sbSqlStrings.AppendLine("@MRP = " + DBLibrary.SQuote(outbound.MRP) + ",");
                sbSqlStrings.AppendLine("@PackType = " + DBLibrary.SQuote(outbound.PackType) + ",");
                sbSqlStrings.AppendLine("@SONumber = " + DBLibrary.SQuote(outbound.SONumber) + ",");
                sbSqlStrings.AppendLine("@SODetailsID = " + outbound.SoDetailsID + ",");
                sbSqlStrings.AppendLine("@SOHeaderID = " + outbound.SOHeaderID + ",");
                sbSqlStrings.AppendLine("@AccountID = " + outbound.AccountID + ",");
                sbSqlStrings.AppendLine("@HUSize = " + DBLibrary.SQuote(outbound.HUSize) + ",");
                sbSqlStrings.AppendLine("@HUNo = " + DBLibrary.SQuote(outbound.HUNo) + "");

                string Query = sbSqlStrings.ToString();
                DataSet _dsLoadInventory = DbUtility.GetDS(Query, this.ConnectionString);

                if (_dsLoadInventory == null)
                {
                    throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = ErrorMessages.WMSExceptionMessage, ShowAsCriticalError = true };
                }
                else
                {
                    int Columnscount = _dsLoadInventory.Tables[0].Columns.Count;
                    //if (Columnscount == 1)
                    //{

                    //    foreach (DataRow _dtPack in _dsLoadInventory.Tables[0].Rows)

                    //    {
                    //        outbound.PSNID = ConversionUtility.ConvertToInt(_dtPack["PSNID"].ToString());
                    //        outbound.PSNDetailsID = ConversionUtility.ConvertToInt(_dtPack["PSNDetailsID"].ToString());
                    //    }
                    //}
                    //else
                    //{
                    //    foreach (DataRow _dtPack in _dsLoadInventory.Tables[0].Rows)
                    //    {
                    //        string errormessage = "";
                    //        errormessage = _dtPack["ErrorMessage"].ToString();
                    //        throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = errormessage, ShowAsWarning = true };
                    //    }
                    //}
                    if (Columnscount == 1)
                    {
                        foreach (DataRow _dtPack in _dsLoadInventory.Tables[0].Rows)
                        {
                            string errormessage = "";
                            errormessage = _dtPack["ErrorMessage"].ToString();
                            throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = errormessage, ShowAsWarning = true };
                        }
                    }
                    else
                    {
                        foreach (DataRow _dtPack in _dsLoadInventory.Tables[0].Rows)
                        {
                            outbound.PSNID = ConversionUtility.ConvertToInt(_dtPack["PSNID"].ToString());
                            outbound.PSNDetailsID = ConversionUtility.ConvertToInt(_dtPack["PSNDetailsID"].ToString());
                        }
                    }
                }

                return outbound;
            }
            catch (WMSExceptionMessage exp)
            {
                throw exp;
            }
            catch (Exception excp)
            {
                ExceptionData oExcpData = new ExceptionData();
                oExcpData.AddInputs("outbound", outbound);

                ExceptionHandling.LogException(excp, _ClassCode + "010", oExcpData);
                throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = ErrorMessages.WMSExceptionMessage, ShowAsCriticalError = true };
            }
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<Outbound> PackComplete(Outbound outbound)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                StringBuilder sbSqlStrings = new StringBuilder();

                sbSqlStrings.AppendLine("EXEC [dbo].[USP_API_GetPackCompleteData] ");
                sbSqlStrings.AppendLine("@SONO = " + DBLibrary.SQuote(outbound.SONumber) + ",");
                sbSqlStrings.AppendLine("@AccountID = " + outbound.AccountID + "");

                string Query = sbSqlStrings.ToString();
                DataSet _dsPickLists = DbUtility.GetDS(Query, this.ConnectionString);

                if (_dsPickLists == null)
                {
                    throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = ErrorMessages.WMSExceptionMessage, ShowAsCriticalError = true };
                }
                else
                {
                    string packcmp = "";
                    foreach (DataRow _dtPack in _dsPickLists.Tables[0].Rows)

                    {
                        packcmp = _dtPack["PCKComplete"].ToString();
                    }

                    if (packcmp == "0.00")
                    {
                        outbound.PackComplete = "true";
                    }
                    else
                    {
                        outbound.PackComplete = "false";
                    }
                }

                return outbound;
            }
            catch (WMSExceptionMessage excp)
            {
                throw excp;
            }
            catch (Exception excp)
            {
                ExceptionData oExcpData = new ExceptionData();
                oExcpData.AddInputs("oCriteria", outbound);

                ExceptionHandling.LogException(excp, _ClassCode + "001", oExcpData);
                throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = ErrorMessages.WMSExceptionMessage, ShowAsCriticalError = true };
            }
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<Outbound> LoadSheetGeneration(Outbound outbound)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                StringBuilder sbSqlStrings = new StringBuilder();

                sbSqlStrings.AppendLine("EXEC [dbo].[USP_API_GenerateLoadSheet] ");
                sbSqlStrings.AppendLine("@TenantId = " + outbound.TenantId + ",");
                sbSqlStrings.AppendLine("@VEHICLENO = " + DBLibrary.SQuote(outbound.Vehicle) + ",");
                sbSqlStrings.AppendLine("@SOSNUMBER = " + DBLibrary.SQuote(outbound.SONumber) + ",");
                sbSqlStrings.AppendLine("@DRIVERNO = " + DBLibrary.SQuote(outbound.DriverNo) + ",");
                sbSqlStrings.AppendLine("@DRIVERNAME = " + DBLibrary.SQuote(outbound.DriverName) + ",");
                sbSqlStrings.AppendLine("@LRNumber = " + DBLibrary.SQuote(outbound.LRnumber) + ",");
                sbSqlStrings.AppendLine("@USERID = " + outbound.UserId + ",");
                sbSqlStrings.AppendLine("@AccountID = " + outbound.AccountID + "");

                string Query = sbSqlStrings.ToString();
                DataSet _dsPickLists = DbUtility.GetDS(Query, this.ConnectionString);

                if (_dsPickLists == null)
                {
                    throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = ErrorMessages.WMSExceptionMessage, ShowAsCriticalError = true };
                }
                else
                {
                    //string packcmp = "";
                    //foreach (DataRow _dtPack in _dsPickLists.Tables[0].Rows)

                    //{
                    //    outbound.LoadRefNo = _dtPack["S"].ToString();

                    //}

                    int Columnscount = _dsPickLists.Tables[0].Columns.Count;

                    if (Columnscount == 1)
                    {
                        foreach (DataRow _dtPack in _dsPickLists.Tables[0].Rows)
                        {
                            outbound.LoadRefNo = _dtPack["S"].ToString();
                        }
                    }
                    else
                    {
                        foreach (DataRow _dtPack in _dsPickLists.Tables[0].Rows)
                        {
                            string errormessage = "";
                            errormessage = _dtPack["ErrorMessage"].ToString();
                            throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = errormessage, ShowAsWarning = true };
                        }
                    }
                }

                return outbound;
            }
            catch (WMSExceptionMessage excp)
            {
                throw excp;
            }
            catch (Exception excp)
            {
                ExceptionData oExcpData = new ExceptionData();
                oExcpData.AddInputs("oCriteria", outbound);

                ExceptionHandling.LogException(excp, _ClassCode + "001", oExcpData);
                throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = ErrorMessages.WMSExceptionMessage, ShowAsCriticalError = true };
            }
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<Outbound> GetSOCountUnderLoadSheet(Outbound outbound)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                StringBuilder sbSqlStrings = new StringBuilder();

                sbSqlStrings.AppendLine("EXEC [dbo].[USP_API_GET_LoadSheetCount] ");
                sbSqlStrings.AppendLine("@LoadRefNO = " + DBLibrary.SQuote(outbound.LoadRefNo) + "");

                string Query = sbSqlStrings.ToString();
                DataSet _dsPickLists = DbUtility.GetDS(Query, this.ConnectionString);

                if (_dsPickLists == null)
                {
                    throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = ErrorMessages.WMSExceptionMessage, ShowAsCriticalError = true };
                }
                else
                {
                    foreach (DataRow _dtPack in _dsPickLists.Tables[0].Rows)
                    {
                        outbound.BusinessType = _dtPack["BussinessType"].ToString();
                        outbound.TotalSOCount = ConversionUtility.ConvertToInt(_dtPack["TotalSOs"].ToString());
                        outbound.ScannedSOCount = ConversionUtility.ConvertToInt(_dtPack["ScannedSOs"].ToString());
                    }
                }

                return outbound;
            }
            catch (WMSExceptionMessage excp)
            {
                throw excp;
            }
            catch (Exception excp)
            {
                ExceptionData oExcpData = new ExceptionData();
                oExcpData.AddInputs("oCriteria", outbound);

                ExceptionHandling.LogException(excp, _ClassCode + "001", oExcpData);
                throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = ErrorMessages.WMSExceptionMessage, ShowAsCriticalError = true };
            }
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<Outbound> UpsertLoadDetails(Outbound outbound)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                StringBuilder sbSqlStrings = new StringBuilder();

                sbSqlStrings.AppendLine("EXEC [dbo].[USP_API_UpsertSOUnderLoadSheet] ");
                sbSqlStrings.AppendLine("@LoadRefNo = " + DBLibrary.SQuote(outbound.LoadRefNo) + ",");
                sbSqlStrings.AppendLine("@SONumber = " + DBLibrary.SQuote(outbound.SONumber) + ",");
                sbSqlStrings.AppendLine("@CartonRefNO = " + outbound.CartonNo + ",");
                sbSqlStrings.AppendLine("@AccountID = " + outbound.AccountID + "");

                string Query = sbSqlStrings.ToString();
                DataSet _dsPickLists = DbUtility.GetDS(Query, this.ConnectionString);

                if (_dsPickLists == null)
                {
                    throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = ErrorMessages.WMSExceptionMessage, ShowAsCriticalError = true };
                }
                else if (_dsPickLists.Tables.Count == 2)
                {
                    foreach (DataRow _dtPack in _dsPickLists.Tables[1].Rows)
                    {
                        string errormessage = "";
                        errormessage = _dtPack["ErrorMessage"].ToString();
                        throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = errormessage, ShowAsWarning = true };
                    }
                }
                else
                {
                    foreach (DataRow _dtPack in _dsPickLists.Tables[0].Rows)
                    {
                        outbound.CustomerCode = _dtPack["CustomerCode"].ToString();
                        outbound.CustomerName = _dtPack["CustomerName"].ToString();
                        outbound.CustomerAddress = _dtPack["Address1"].ToString();
                    }
                }

                return outbound;
            }
            catch (WMSExceptionMessage excp)
            {
                throw excp;
            }
            catch (Exception excp)
            {
                ExceptionData oExcpData = new ExceptionData();
                oExcpData.AddInputs("oCriteria", outbound);

                ExceptionHandling.LogException(excp, _ClassCode + "001", oExcpData);
                throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = ErrorMessages.WMSExceptionMessage, ShowAsCriticalError = true };
            }
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<bool> CheckSO(Outbound outbound)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                bool result = true;
                StringBuilder sbSqlStrings = new StringBuilder();

                sbSqlStrings.AppendLine("EXEC [dbo].[USP_Get_OBD_SODetails] ");
                sbSqlStrings.AppendLine("@SONO = " + DBLibrary.SQuote(outbound.SONumber) + ",");
                sbSqlStrings.AppendLine("@AccountID = " + outbound.AccountID + ",");
                sbSqlStrings.AppendLine("@UserID = " + outbound.UserId + "");

                string Query = sbSqlStrings.ToString();
                DataSet _dsResults = DbUtility.GetDS(Query, this.ConnectionString);

                if (_dsResults != null && _dsResults.Tables.Count != 0 && _dsResults.Tables[0].Rows.Count != 0)
                {
                    result = true;
                }
                else
                {
                    throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = "Invalid SO Number", ShowAsWarning = true };
                }

                return result;
            }
            catch (WMSExceptionMessage excp)
            {
                throw excp;
            }
            catch (Exception excp)
            {
                ExceptionData oExcpData = new ExceptionData();
                oExcpData.AddInputs("oCriteria", outbound);

                ExceptionHandling.LogException(excp, _ClassCode + "001", oExcpData);
                throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = ErrorMessages.WMSExceptionMessage, ShowAsCriticalError = true };
            }
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<bool> CheckCarton(Outbound outbound)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                bool result = true;
                StringBuilder sbSqlStrings = new StringBuilder();

                sbSqlStrings.AppendLine("EXEC [dbo].[USP_Get_OBD_CartonDetails] ");
                sbSqlStrings.AppendLine("@CartonNumber = " + DBLibrary.SQuote(outbound.CartonSerialNo) + ",");
                sbSqlStrings.AppendLine("@WHID = " + outbound.WareHouseID + "");

                string Query = sbSqlStrings.ToString();
                DataSet _dsResults = DbUtility.GetDS(Query, this.ConnectionString);

                if (_dsResults != null && _dsResults.Tables.Count != 0 && _dsResults.Tables[0].Rows.Count != 0)
                {

                    result = true;
                }
                else
                {
                    throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = "Invalid Carton Serial Number", ShowAsWarning = true };

                }

                return result;
            }
            catch (WMSExceptionMessage excp)
            {
                throw excp;
            }
            catch (Exception excp)
            {
                ExceptionData oExcpData = new ExceptionData();
                oExcpData.AddInputs("oCriteria", outbound);

                ExceptionHandling.LogException(excp, _ClassCode + "001", oExcpData);
                throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = ErrorMessages.WMSExceptionMessage, ShowAsCriticalError = true };
            }
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<List<Outbound>> GetPackingCartonInfo(Outbound outbound)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                List<Outbound> _lstOutbounds = new List<Outbound>();
                StringBuilder sbSqlStrings = new StringBuilder();

                sbSqlStrings.AppendLine("EXEC [dbo].[OBD_SOWISECARTONDETAIL] ");
                sbSqlStrings.AppendLine("@SONUMBER = " + DBLibrary.SQuote(outbound.SONumber) + ",");
                sbSqlStrings.AppendLine("@CARTONCODE = " + DBLibrary.SQuote(outbound.CartonSerialNo) + ",");
                sbSqlStrings.AppendLine("@TenantID = " + outbound.TenantId + ",");
                sbSqlStrings.AppendLine("@AccountID = " + outbound.AccountID + ",");
                sbSqlStrings.AppendLine("@WHID = " + outbound.WareHouseID + "");

                string Query = sbSqlStrings.ToString();
                DataSet _dsPickLists = DbUtility.GetDS(Query, this.ConnectionString);

                if (_dsPickLists != null && _dsPickLists.Tables.Count != 0 && _dsPickLists.Tables[0].Rows.Count != 0)
                {
                    foreach (DataRow _dtPack in _dsPickLists.Tables[0].Rows)
                    {
                        Outbound _oOutbound = new Outbound()
                        {
                            CartonSerialNo = _dtPack["CartonSerialNO"].ToString(),
                            Mcode = _dtPack["MCode"].ToString(),
                            PickedQty = ConversionUtility.ConvertToDecimal(_dtPack["PickedQuantity"].ToString()),
                            SONumber = _dtPack["SONumber"].ToString()
                        };
                        _lstOutbounds.Add(_oOutbound);
                    }
                }
                else
                {
                    throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = "No Data Found For Given Search Criteria.", ShowAsWarning = true };
                }
                return _lstOutbounds;
            }
            catch (WMSExceptionMessage excp)
            {
                throw excp;
            }
            catch (Exception excp)
            {
                ExceptionData oExcpData = new ExceptionData();
                oExcpData.AddInputs("oCriteria", outbound);

                ExceptionHandling.LogException(excp, _ClassCode + "001", oExcpData);
                throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = ErrorMessages.WMSExceptionMessage, ShowAsCriticalError = true };
            }
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<List<Outbound>> GetRevertOBDList(Outbound outbound)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                List<Outbound> _lstOutbounds = new List<Outbound>();
                StringBuilder sbSqlStrings = new StringBuilder();

                sbSqlStrings.AppendLine("EXEC [dbo].[SP_GET_OBD_RevertOBDList] ");
                sbSqlStrings.AppendLine("@UserID = " + outbound.UserId + "");

                string Query = sbSqlStrings.ToString();
                DataSet _dsPickLists = DbUtility.GetDS(Query, this.ConnectionString);

                if (_dsPickLists != null && _dsPickLists.Tables.Count != 0 && _dsPickLists.Tables[0].Rows.Count != 0)
                {
                    foreach (DataRow _dtPack in _dsPickLists.Tables[0].Rows)
                    {
                        Outbound _oOutbound = new Outbound()
                        {
                            OutboundID = ConversionUtility.ConvertToInt(_dtPack["OutboundId"].ToString()),
                            OBDNumber = _dtPack["OBDNumber"].ToString()
                        };
                        _lstOutbounds.Add(_oOutbound);
                    }
                }
                else
                {
                    throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = "No Data Found For Given Search Criteria.", ShowAsWarning = true };
                }
                return _lstOutbounds;
            }
            catch (WMSExceptionMessage excp)
            {
                throw excp;
            }
            catch (Exception excp)
            {
                ExceptionData oExcpData = new ExceptionData();
                oExcpData.AddInputs("oCriteria", outbound);

                ExceptionHandling.LogException(excp, _ClassCode + "001", oExcpData);
                throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = ErrorMessages.WMSExceptionMessage, ShowAsCriticalError = true };
            }
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<List<Outbound>> GetRevertSOList(Outbound outbound)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                List<Outbound> _lstOutbounds = new List<Outbound>();
                StringBuilder sbSqlStrings = new StringBuilder();

                sbSqlStrings.AppendLine("EXEC [dbo].[SP_GET_OBD_Revert_SOList] ");
                sbSqlStrings.AppendLine("@UserID = " + outbound.UserId + ",");
                sbSqlStrings.AppendLine("@OutboundID = " + outbound.OutboundID + "");

                string Query = sbSqlStrings.ToString();
                DataSet _dsPickLists = DbUtility.GetDS(Query, this.ConnectionString);

                if (_dsPickLists != null && _dsPickLists.Tables.Count != 0 && _dsPickLists.Tables[0].Rows.Count != 0)
                {
                    foreach (DataRow _dtPack in _dsPickLists.Tables[0].Rows)
                    {
                        Outbound _oOutbound = new Outbound()
                        {
                            SOHeaderID = ConversionUtility.ConvertToInt(_dtPack["SOHeaderID"].ToString()),
                            SONumber = _dtPack["SONumber"].ToString()
                        };
                        _lstOutbounds.Add(_oOutbound);
                    }
                }
                else
                {
                    throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = "No Data Found For Given Search Criteria.", ShowAsWarning = true };
                }
                return _lstOutbounds;
            }
            catch (WMSExceptionMessage excp)
            {
                throw excp;
            }
            catch (Exception excp)
            {
                ExceptionData oExcpData = new ExceptionData();
                oExcpData.AddInputs("oCriteria", outbound);

                ExceptionHandling.LogException(excp, _ClassCode + "001", oExcpData);
                throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = ErrorMessages.WMSExceptionMessage, ShowAsCriticalError = true };
            }
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<List<Outbound>> GetRevertSOOBDInfo(Outbound outbound)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                List<Outbound> _lstOutbounds = new List<Outbound>();
                StringBuilder sbSqlStrings = new StringBuilder();

                sbSqlStrings.AppendLine("EXEC [dbo].[SP_GET_OBD_SO_Info] ");
                sbSqlStrings.AppendLine("@OutboundID = " + outbound.OutboundID + ",");
                sbSqlStrings.AppendLine("@SoNumber = " + DBLibrary.SQuote(outbound.SONumber) + "");
                sbSqlStrings.AppendLine("@AcccountID = " + outbound.AccountID + "");

                string Query = sbSqlStrings.ToString();
                DataSet _dsPickLists = DbUtility.GetDS(Query, this.ConnectionString);

                if (_dsPickLists != null && _dsPickLists.Tables.Count != 0 && _dsPickLists.Tables[0].Rows.Count != 0)
                {
                    foreach (DataRow _dtPack in _dsPickLists.Tables[0].Rows)
                    {
                        Outbound _oOutbound = new Outbound()
                        {
                            SOHeaderID = ConversionUtility.ConvertToInt(_dtPack["SOHeaderID"].ToString()),
                            SONumber = _dtPack["SONumber"].ToString(),
                            OBDNumber = _dtPack["OBDNumber"].ToString(),
                            Status = _dtPack["OutboundStatus"].ToString(),
                            BusinessType = _dtPack["BusinessType"].ToString(),
                            OutboundID = ConversionUtility.ConvertToInt(_dtPack["OutboundID"].ToString())
                        };
                        _lstOutbounds.Add(_oOutbound);
                    }
                }
                else
                {
                    throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = "No Data Found For Given Search Criteria.", ShowAsWarning = true };
                }
                return _lstOutbounds;
            }
            catch (WMSExceptionMessage excp)
            {
                throw excp;
            }
            catch (Exception excp)
            {
                ExceptionData oExcpData = new ExceptionData();
                oExcpData.AddInputs("oCriteria", outbound);

                ExceptionHandling.LogException(excp, _ClassCode + "001", oExcpData);
                throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = ErrorMessages.WMSExceptionMessage, ShowAsCriticalError = true };
            }
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<List<Outbound>> GetRevertCartonCheck(Outbound outbound)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                List<Outbound> _lstOutbounds = new List<Outbound>();
                StringBuilder sbSqlStrings = new StringBuilder();

                sbSqlStrings.AppendLine("EXEC [dbo].[SP_GET_OBD_CartonCheck] ");
                sbSqlStrings.AppendLine("@OutboundID = " + outbound.OutboundID + ",");
                sbSqlStrings.AppendLine("@SOHeaderID = " + outbound.SOHeaderID + "");
                sbSqlStrings.AppendLine("@CartonSerailNo = " + DBLibrary.SQuote(outbound.CartonSerialNo) + "");

                string Query = sbSqlStrings.ToString();
                DataSet _dsPickLists = DbUtility.GetDS(Query, this.ConnectionString);

                if (_dsPickLists != null && _dsPickLists.Tables.Count != 0 && _dsPickLists.Tables[0].Rows.Count != 0)
                {
                    foreach (DataRow _dtPack in _dsPickLists.Tables[0].Rows)
                    {
                        Outbound _oOutbound = new Outbound()
                        {
                            Status = _dtPack["Result"].ToString()
                        };
                        _lstOutbounds.Add(_oOutbound);
                    }
                }
                else
                {
                    throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = "No Data Found For Given Search Criteria.", ShowAsWarning = true };
                }
                return _lstOutbounds;
            }
            catch (WMSExceptionMessage excp)
            {
                throw excp;
            }
            catch (Exception excp)
            {
                ExceptionData oExcpData = new ExceptionData();
                oExcpData.AddInputs("oCriteria", outbound);

                ExceptionHandling.LogException(excp, _ClassCode + "001", oExcpData);
                throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = ErrorMessages.WMSExceptionMessage, ShowAsCriticalError = true };
            }
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<List<Outbound>> GetScanqtyvalidation(Outbound outbound)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                List<Outbound> _lstOutbounds = new List<Outbound>();
                StringBuilder sbSqlStrings = new StringBuilder();

                sbSqlStrings.AppendLine("EXEC [dbo].[SP_GET_OBD_MMT_Revet_ScanValidation] ");
                sbSqlStrings.AppendLine("@OutboundID = " + outbound.OutboundID + ",");
                sbSqlStrings.AppendLine("@SOHeaderID = " + outbound.SOHeaderID + ",");
                sbSqlStrings.AppendLine("@CartonSerailNo = " + DBLibrary.SQuote(outbound.CartonSerialNo) + ",");
                sbSqlStrings.AppendLine("@SKU = " + DBLibrary.SQuote(outbound.Mcode) + ",");
                sbSqlStrings.AppendLine("@MfgDate = " + DBLibrary.SQuote(outbound.MFGDate) + ",");
                sbSqlStrings.AppendLine("@BatchNo = " + DBLibrary.SQuote(outbound.BatchNo) + ",");
                sbSqlStrings.AppendLine("@ExpDate = " + DBLibrary.SQuote(outbound.EXPDate) + ",");
                sbSqlStrings.AppendLine("@SerialNo = " + DBLibrary.SQuote(outbound.SerialNo) + ",");
                sbSqlStrings.AppendLine("@ProjectRefNo = " + DBLibrary.SQuote(outbound.ProjectRefNo) + ",");
                sbSqlStrings.AppendLine("@MRP = " + DBLibrary.SQuote(outbound.MRP) + "");

                string Query = sbSqlStrings.ToString();
                DataSet _dsPickLists = DbUtility.GetDS(Query, this.ConnectionString);

                if (_dsPickLists != null && _dsPickLists.Tables.Count != 0 && _dsPickLists.Tables[0].Rows.Count != 0)
                {
                    foreach (DataRow _dtPack in _dsPickLists.Tables[0].Rows)
                    {
                        Outbound _oOutbound = new Outbound()
                        {
                            Status = _dtPack["Result"].ToString(),
                            SOQty = ConversionUtility.ConvertToDecimal(_dtPack["Qty"].ToString())
                        };
                        _lstOutbounds.Add(_oOutbound);
                    }
                }
                else
                {
                    throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = "No Data Found For Given Search Criteria.", ShowAsWarning = true };
                }
                return _lstOutbounds;
            }
            catch (WMSExceptionMessage excp)
            {
                throw excp;
            }
            catch (Exception excp)
            {
                ExceptionData oExcpData = new ExceptionData();
                oExcpData.AddInputs("oCriteria", outbound);

                ExceptionHandling.LogException(excp, _ClassCode + "001", oExcpData);
                throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = ErrorMessages.WMSExceptionMessage, ShowAsCriticalError = true };
            }
        }

        public async Task<WorkOrderOutbound> WorkOrderPicking(WorkOrderOutbound outbound)
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                StringBuilder sCmdUpsertPOQuantity = new StringBuilder();
                if (outbound.HUNo > 1)
                {
                    outbound.GoodsMovementDetailsID = "0";
                    outbound.GoodsMovementTypeID = "2";
                    outbound.IsPostiveRecall = "0";
                    outbound.IsDam = "0";
                    outbound.HasDisc = "0";
                    outbound.QtyinBUoM = Convert.ToDecimal(outbound.Qty * outbound.CF);

                    sCmdUpsertPOQuantity.Append("Exec [dbo].[SP_Upsert_HU_OBD_Items_Picking] ");
                    sCmdUpsertPOQuantity.Append("@OBDNumber=" + DBLibrary.SQuote(outbound.Obdno));
                    sCmdUpsertPOQuantity.Append(",@LineNumber=" + DBLibrary.SQuote(outbound.Lineno));
                    sCmdUpsertPOQuantity.Append(",@SOHeaderID=" + outbound.POSOHeaderId);
                    sCmdUpsertPOQuantity.Append(",@MCode=" + DBLibrary.SQuote(outbound.MCode));
                    sCmdUpsertPOQuantity.Append(",@Location=" + DBLibrary.SQuote(outbound.Location));
                    sCmdUpsertPOQuantity.Append(",@Quantity=" + outbound.Qty);
                    sCmdUpsertPOQuantity.Append(",@IsDamaged=" + DBLibrary.SQuote(outbound.IsDam));
                    sCmdUpsertPOQuantity.Append(",@HasDiscrepancy=" + DBLibrary.SQuote(outbound.HasDisc));
                    sCmdUpsertPOQuantity.Append(",@CreatedBy=" + outbound.CreatedBy);
                    sCmdUpsertPOQuantity.Append(",@MfgDate=" + DBLibrary.SQuote(outbound.MfgDate));
                    sCmdUpsertPOQuantity.Append(",@ExpDate=" + DBLibrary.SQuote(outbound.ExpDate));
                    sCmdUpsertPOQuantity.Append(",@SerialNo=" + DBLibrary.SQuote(outbound.SerialNo));
                    sCmdUpsertPOQuantity.Append(",@BatchNo=" + DBLibrary.SQuote(outbound.BatchNo));
                    sCmdUpsertPOQuantity.Append(",@Projrefno=" + DBLibrary.SQuote(outbound.ProjectNo));
                    sCmdUpsertPOQuantity.Append(",@CartonCode =" + DBLibrary.SQuote(outbound.CartonNo));
                    sCmdUpsertPOQuantity.Append(",@ToCartonCode =" + DBLibrary.SQuote(outbound.ToCartonNo));
                    sCmdUpsertPOQuantity.Append(",@AssignedId =" + outbound.Assignedid);
                    sCmdUpsertPOQuantity.Append(",@SoDetailsIdnew=" + outbound.SODetailsID);
                    sCmdUpsertPOQuantity.Append(",@MRP=" + DBLibrary.SQuote(outbound.MRP));
                    sCmdUpsertPOQuantity.Append(",@AccountID=" + outbound.AccountId);
                    sCmdUpsertPOQuantity.Append(",@HUSize=" + outbound.HUSize);
                    sCmdUpsertPOQuantity.Append(",@HUNo=" + outbound.HUNo);
                }
                else
                {
                    outbound.GoodsMovementDetailsID = "0";
                    outbound.GoodsMovementTypeID = "2";
                    outbound.IsPostiveRecall = "0";
                    outbound.IsDam = "0";
                    outbound.HasDisc = "0";
                    outbound.QtyinBUoM = Convert.ToDecimal(outbound.Qty * outbound.CF);

                    sCmdUpsertPOQuantity.Append("Exec [dbo].[sp_INV_SET_WorkOrderPickItemFromBin] ");
                    sCmdUpsertPOQuantity.Append("@OBDNumber=" + DBLibrary.SQuote(outbound.Obdno));
                    sCmdUpsertPOQuantity.Append(",@LineNumber=" + DBLibrary.SQuote(outbound.Lineno));
                    sCmdUpsertPOQuantity.Append(",@SOHeaderID=" + outbound.POSOHeaderId);
                    sCmdUpsertPOQuantity.Append(",@MCode=" + DBLibrary.SQuote(outbound.MCode));
                    sCmdUpsertPOQuantity.Append(",@Location=" + DBLibrary.SQuote(outbound.Location));
                    sCmdUpsertPOQuantity.Append(",@Quantity=" + outbound.Qty);
                    sCmdUpsertPOQuantity.Append(",@IsDamaged=" + DBLibrary.SQuote(outbound.IsDam));
                    sCmdUpsertPOQuantity.Append(",@HasDiscrepancy=" + DBLibrary.SQuote(outbound.HasDisc));
                    sCmdUpsertPOQuantity.Append(",@CreatedBy=" + outbound.CreatedBy);
                    sCmdUpsertPOQuantity.Append(",@MfgDate=" + DBLibrary.SQuote(outbound.MfgDate));
                    sCmdUpsertPOQuantity.Append(",@ExpDate=" + DBLibrary.SQuote(outbound.ExpDate));
                    sCmdUpsertPOQuantity.Append(",@SerialNo=" + DBLibrary.SQuote(outbound.SerialNo));
                    sCmdUpsertPOQuantity.Append(",@BatchNo=" + DBLibrary.SQuote(outbound.BatchNo));
                    sCmdUpsertPOQuantity.Append(",@Projrefno=" + DBLibrary.SQuote(outbound.ProjectNo));
                    sCmdUpsertPOQuantity.Append(",@CartonCode =" + DBLibrary.SQuote(outbound.CartonNo));
                    sCmdUpsertPOQuantity.Append(",@ToCartonCode =" + DBLibrary.SQuote(outbound.ToCartonNo));
                    sCmdUpsertPOQuantity.Append(",@AssignedId =" + outbound.Assignedid);
                    sCmdUpsertPOQuantity.Append(",@SoDetailsIdnew=" + DBLibrary.SQuote(outbound.SODetailsID));
                    sCmdUpsertPOQuantity.Append(",@MRP=" + DBLibrary.SQuote(outbound.MRP));
                    sCmdUpsertPOQuantity.Append(",@AccountID=" + outbound.AccountId);
                    sCmdUpsertPOQuantity.Append(",@HUSize=" + outbound.HUSize);
                    sCmdUpsertPOQuantity.Append(",@HUNo=" + outbound.HUNo);
                    sCmdUpsertPOQuantity.Append(",@IsPSN=" + outbound.IsPSN);
                    //sCmdUpsertPOQuantity.Append(",@PSN=" + base.SQuote(outbound.PSN));
                    //sCmdUpsertPOQuantity.Append(",@IsExcess=" + outbound.RID);
                }

                DataSet ds = DbUtility.GetDS(sCmdUpsertPOQuantity.ToString(), this.ConnectionString);

                if (Convert.ToInt32(ds.Tables[0].Rows[0][0]) == -999)
                {
                    outbound.Result = "Qty Exceeded";
                }
                else if (Convert.ToInt32(ds.Tables[0].Rows[0]["N"]) == 1)
                {
                    outbound.Result = "Success";
                    outbound.AssignedQuantity = Convert.ToString(ds.Tables[0].Rows[0]["AssignedQuantity"]);
                    outbound.PickedQty = Convert.ToString(ds.Tables[0].Rows[0]["PickedQty"]);
                    await CheckItem(outbound);
                    await GetOBDPickedInfo(outbound);
                }
                //else if (Result == -444)
                //{
                //    outbound.Result = Result.ToString();
                //}
                //else if (Result == -333)
                //{
                //    outbound.Result = Result.ToString();
                //}
                else if (Convert.ToInt32(ds.Tables[0].Rows[0][0]) == 2)
                {
                    outbound.Result = "Stock not Availble";
                }
                //else if (Result == -111)
                //{
                //    outbound.Result = "The PSN you are trying to scan is already picked";
                //}
                else
                {
                    outbound.Result = "Process failed,Please Contact Support team";
                }
                return outbound;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task CheckItem(WorkOrderOutbound outbound)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            StringBuilder sbSqlStrings = new StringBuilder();

            sbSqlStrings.AppendLine("EXEC [dbo].[SP_ChangeGateEntryStatus] ");
            sbSqlStrings.AppendLine("@ShipmentId = " + outbound.OutboundId + ",");
            sbSqlStrings.AppendLine("@IsForInbound = 0");

            string Query = sbSqlStrings.ToString();
            DbUtility.GetSqlN(sbSqlStrings.ToString(), ConnectionString);
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<WorkOrderOutbound> GetOBDPickedInfo(WorkOrderOutbound outbound)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            string query = " EXEC [dbo].[USP_OBD_Get_AssginedDetailsForOutbound] @OBDID=" + outbound.OutboundId + ",@AssignedId=" + outbound.Assignedid;
            try
            {
                DataSet ds = DbUtility.GetDS(query, this.ConnectionString);

                if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count != 0)
                {
                    outbound.Assignedid = Convert.ToInt32(ds.Tables[0].Rows[0]["Assignedid"].ToString());
                    outbound.MaterialMasterId = ds.Tables[0].Rows[0]["MaterialMasterID"].ToString();
                    outbound.MCode = ds.Tables[0].Rows[0]["MCode"].ToString();
                    outbound.MDescription = ds.Tables[0].Rows[0]["MDescription"].ToString();
                    outbound.CartonNo = ds.Tables[0].Rows[0]["CartonCode"].ToString();
                    outbound.CartonId = ds.Tables[0].Rows[0]["CartonID"].ToString();
                    outbound.Location = ds.Tables[0].Rows[0]["Location"].ToString();
                    outbound.LocationId = ds.Tables[0].Rows[0]["LocationID"] != DBNull.Value ? Convert.ToInt32(ds.Tables[0].Rows[0]["LocationID"]) : 0;

                    outbound.AssignedQuantity = ds.Tables[0].Rows[0]["AssignedQuantity"].ToString();
                    outbound.PickedQty = ds.Tables[0].Rows[0]["PickedQty"].ToString();
                    outbound.OutboundId = ds.Tables[0].Rows[0]["OutboundID"].ToString();
                    outbound.SODetailsID = ds.Tables[0].Rows[0]["SODetailsID"].ToString();
                    outbound.SLocId = ds.Tables[0].Rows[0]["StorageLocationID"].ToString();
                    outbound.SLoc = ds.Tables[0].Rows[0]["Code"].ToString();

                    outbound.Lineno = ds.Tables[0].Rows[0]["LineNumber"].ToString();
                    outbound.MaterialMaster_IUoMID = ds.Tables[0].Rows[0]["MaterialMaster_UoMID"].ToString();

                    outbound.POSOHeaderId = ds.Tables[0].Rows[0]["SOHeaderID"].ToString();
                    outbound.PendingQty = ds.Tables[0].Rows[0]["PendingQty"].ToString();
                    outbound.MRP = ds.Tables[0].Rows[0]["MRP"].ToString();
                    outbound.HUNo = Convert.ToInt32(ds.Tables[0].Rows[0]["HUNo"].ToString());
                    outbound.HUSize = Convert.ToInt32(ds.Tables[0].Rows[0]["HUSize"].ToString());
                    outbound.IsPSN = Convert.ToInt32(ds.Tables[0].Rows[0]["IsPSN"].ToString());
                    outbound.CustomerName = ds.Tables[0].Rows[0]["Customer"].ToString();
                    outbound.DockLocation = ds.Tables[0].Rows[0]["DockName"].ToString();
                }
                else
                {
                    outbound.PendingQty = "0";
                }
                return outbound;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<List<Outbound>> UpsertHHTOBDRevert(Outbound outbound)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                List<Outbound> _lstOutbounds = new List<Outbound>();
                StringBuilder sbSqlStrings = new StringBuilder();

                sbSqlStrings.AppendLine("EXEC [dbo].[SP_SET_HHT_Load_Pack_Revert] ");
                sbSqlStrings.AppendLine("@OutboundID = " + outbound.OutboundID + ",");
                sbSqlStrings.AppendLine("@SOHeaderID = " + outbound.SOHeaderID + ",");
                sbSqlStrings.AppendLine("@CartonSerailNo = " + DBLibrary.SQuote(outbound.CartonSerialNo) + ",");
                sbSqlStrings.AppendLine("@SKU = " + DBLibrary.SQuote(outbound.Mcode) + ",");
                sbSqlStrings.AppendLine("@BatchNo = " + DBLibrary.SQuote(outbound.BatchNo) + ",");
                sbSqlStrings.AppendLine("@MfgDate = " + DBLibrary.SQuote(outbound.MFGDate) + ",");
                sbSqlStrings.AppendLine("@ExpDate = " + DBLibrary.SQuote(outbound.EXPDate) + ",");
                sbSqlStrings.AppendLine("@SerialNo = " + DBLibrary.SQuote(outbound.SerialNo) + ",");
                sbSqlStrings.AppendLine("@ProjectRefNo = " + DBLibrary.SQuote(outbound.ProjectRefNo) + ",");
                sbSqlStrings.AppendLine("@MRP = " + DBLibrary.SQuote(outbound.MRP) + ",");
                sbSqlStrings.AppendLine("@RevertQty = " + outbound.PackedQty + ",");
                sbSqlStrings.AppendLine("@CreatedBy = " + outbound.UserId + "");

                string Query = sbSqlStrings.ToString();
                DataSet _dsPickLists = DbUtility.GetDS(Query, this.ConnectionString);

                if (_dsPickLists != null && _dsPickLists.Tables.Count != 0 && _dsPickLists.Tables[0].Rows.Count != 0)
                {
                    foreach (DataRow _dtPack in _dsPickLists.Tables[0].Rows)
                    {
                        Outbound _oOutbound = new Outbound()
                        {
                            Status = _dtPack["S"].ToString()
                        };
                        _lstOutbounds.Add(_oOutbound);
                    }
                }
                else
                {
                    throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = "No Data Found For Given Search Criteria.", ShowAsWarning = true };
                }
                return _lstOutbounds;
            }
            catch (WMSExceptionMessage excp)
            {
                throw excp;
            }
            catch (Exception excp)
            {
                ExceptionData oExcpData = new ExceptionData();
                oExcpData.AddInputs("oCriteria", outbound);

                ExceptionHandling.LogException(excp, _ClassCode + "001", oExcpData);
                throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = ErrorMessages.WMSExceptionMessage, ShowAsCriticalError = true };
            }
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<List<Outbound>> Get_WOListToRevert(Outbound outbound)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                List<Outbound> _lstOutbounds = new List<Outbound>();
                StringBuilder sbSqlStrings = new StringBuilder();

                sbSqlStrings.AppendLine("EXEC [dbo].[Get_WOListToRevert] ");
                sbSqlStrings.AppendLine("@AccountId = " + outbound.AccountID + ",");
                sbSqlStrings.AppendLine("@UserID = " + outbound.UserId + "");

                string Query = sbSqlStrings.ToString();
                DataSet _dsPickLists = DbUtility.GetDS(Query, this.ConnectionString);

                if (_dsPickLists != null && _dsPickLists.Tables.Count != 0 && _dsPickLists.Tables[0].Rows.Count != 0)
                {
                    foreach (DataRow _dtPack in _dsPickLists.Tables[0].Rows)
                    {
                        Outbound _oOutbound = new Outbound()
                        {
                            OutboundID = ConversionUtility.ConvertToInt(_dtPack["OutboundID"].ToString()),
                            OBDNumber = _dtPack["OBDNumber"].ToString()
                        };
                        _lstOutbounds.Add(_oOutbound);
                    }
                }
                else
                {
                    throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = "No Data Found For Given Search Criteria.", ShowAsWarning = true };
                }
                return _lstOutbounds;
            }
            catch (WMSExceptionMessage excp)
            {
                throw excp;
            }
            catch (Exception excp)
            {
                ExceptionData oExcpData = new ExceptionData();
                oExcpData.AddInputs("oCriteria", outbound);

                ExceptionHandling.LogException(excp, _ClassCode + "001", oExcpData);
                throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = ErrorMessages.WMSExceptionMessage, ShowAsCriticalError = true };
            }
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<List<Outbound>> GetWOItemsForPicking(Outbound outbound)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                List<Outbound> _lstOutbounds = new List<Outbound>();
                StringBuilder sbSqlStrings = new StringBuilder();

                sbSqlStrings.AppendLine("EXEC [dbo].[Get_WOSKUListtToRevert] ");
                sbSqlStrings.AppendLine("@OutboundID = " + outbound.OutboundID + "");

                string Query = sbSqlStrings.ToString();
                DataSet _dsPickLists = DbUtility.GetDS(Query, this.ConnectionString);

                if (_dsPickLists != null && _dsPickLists.Tables.Count != 0 && _dsPickLists.Tables[0].Rows.Count != 0)
                {
                    foreach (DataRow _dtPack in _dsPickLists.Tables[0].Rows)
                    {
                        Outbound _oOutbound = new Outbound()
                        {
                            MaterialMasterID = ConversionUtility.ConvertToInt(_dtPack["MaterialMasterID"].ToString()),
                            Mcode = _dtPack["MCode"].ToString(),
                            MDescription = _dtPack["MDescription"].ToString(),
                            Quantity = ConversionUtility.ConvertToDecimal(_dtPack["Quantity"].ToString()),
                            BatchNo = _dtPack["BatchNo"].ToString(),
                            RevertQty = ConversionUtility.ConvertToDecimal(_dtPack["RevertQty"].ToString()),
                            VLPDPickID = ConversionUtility.ConvertToInt(_dtPack["VLPDPickID"].ToString()),
                            ProjectRefNo = _dtPack["ProjectRefNo"].ToString(),
                            EXPDate = _dtPack["ExpDate"].ToString(),
                            MFGDate = _dtPack["MfgDate"].ToString()
                        };
                        _lstOutbounds.Add(_oOutbound);
                    }
                }
                else
                {
                    throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = "No Data Found For Given Search Criteria.", ShowAsWarning = true };
                }
                return _lstOutbounds;
            }
            catch (WMSExceptionMessage excp)
            {
                throw excp;
            }
            catch (Exception excp)
            {
                ExceptionData oExcpData = new ExceptionData();
                oExcpData.AddInputs("oCriteria", outbound);

                ExceptionHandling.LogException(excp, _ClassCode + "001", oExcpData);
                throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = ErrorMessages.WMSExceptionMessage, ShowAsCriticalError = true };
            }
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<List<OutboundDetails>> UpsertHHTWORevert(string xmlstring)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                List<OutboundDetails> _lstOutbounds = new List<OutboundDetails>();
                string WORevertQuery = "EXEC [dbo].[SP_UpsertHHTWORevert] @xml = " + "'" + xmlstring + "'" + "";
                var _dsPickLists = DbUtility.GetDS(WORevertQuery, this.ConnectionString);

                if (_dsPickLists != null && _dsPickLists.Tables.Count != 0 && _dsPickLists.Tables[0].Rows.Count != 0)
                {
                    foreach (DataRow _dtPack in _dsPickLists.Tables[0].Rows)
                    {
                        OutboundDetails _oOutbound = new OutboundDetails()
                        {
                            Result = _dtPack["N"].ToString(),
                            PendingQty = ConversionUtility.ConvertToDecimal(_dtPack["PendingQty"].ToString()),
                            RevertQty = ConversionUtility.ConvertToDecimal(_dtPack["RevertQty"].ToString()),
                            Qty = ConversionUtility.ConvertToDecimal(_dtPack["Quantity"].ToString())
                        };
                        _lstOutbounds.Add(_oOutbound);
                    }
                }
                else
                {
                    throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = "No Data Found For Given Search Criteria.", ShowAsWarning = true };
                }
                return _lstOutbounds;
            }
            catch (WMSExceptionMessage excp)
            {
                throw excp;
            }
            catch (Exception excp)
            {
                ExceptionData oExcpData = new ExceptionData();
                oExcpData.AddInputs("oCriteria", xmlstring);

                ExceptionHandling.LogException(excp, _ClassCode + "001", oExcpData);
                throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = ErrorMessages.WMSExceptionMessage, ShowAsCriticalError = true };
            }
        }
        public async Task<Outbound> LoadComplete(Outbound outbound)
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                string WORevertQuery = "EXEC [dbo].[USP_UPDATE_OBDLoadcomplete] @OutboundId = " + "" + outbound.OutboundID + ",@AccountId=" + outbound.AccountID + ",@UserId=" + outbound.UserId + "";
                var _dsPickLists = DbUtility.GetDS(WORevertQuery, this.ConnectionString);

                if (_dsPickLists != null && _dsPickLists.Tables.Count != 0 && _dsPickLists.Tables[0].Rows.Count != 0)
                {
                    string Result = "";
                    foreach (DataRow _dtPack in _dsPickLists.Tables[0].Rows)
                    {
                        Result = _dtPack["Result"].ToString();
                    }
                    outbound.LoadComplete = Result;
                }
                else
                {
                    throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = "No Data Found For Given Search Criteria.", ShowAsWarning = true };
                }
                return outbound;
            }
            catch (WMSExceptionMessage excp)
            {
                throw excp;
            }
            catch (Exception excp)
            {
                ExceptionData oExcpData = new ExceptionData();
                oExcpData.AddInputs("oCriteria", outbound);

                ExceptionHandling.LogException(excp, _ClassCode + "001", oExcpData);
                throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = ErrorMessages.WMSExceptionMessage, ShowAsCriticalError = true };
            }
        }
        public async Task<List<Outbound>> UpsertLoadItem(Outbound obj)
        {
            try
            {
                if (obj.ScanInput.Split('|').Length == 5)
                {
                    obj.Mcode = obj.ScanInput.Split('|')[0];
                    obj.BatchNo = obj.ScanInput.Split('|')[1];
                    obj.MFGDate = obj.ScanInput.Split('|')[2];
                    obj.EXPDate = obj.ScanInput.Split('|')[3];
                    obj.LabelSerialNo = (obj.ScanInput.Split('|')[4]);
                    obj.AccountID = obj.AccountID;
                    obj.UserId = obj.UserId;
                    obj.OutboundID = obj.OutboundID;
                }
               else if (obj.ScanInput.Split('|').Length == 11)
                {

              

                    obj.Mcode = obj.ScanInput.Split('|')[0];
                    obj.BatchNo = obj.ScanInput.Split('|')[1];
                    obj.LabelSerialNo = (obj.ScanInput.Split('|')[2]);
                    obj.MFGDate = obj.ScanInput.Split('|')[3];
                    obj.EXPDate = obj.ScanInput.Split('|')[4];
                    obj.ProjectRefNo = obj.ScanInput?.Split('|')[5];
                    obj.KitID = obj.ScanInput?.Split('|')[6];
                    obj.Grade = obj.ScanInput?.Split('|')[7];
                    obj.LineNumber = obj.ScanInput?.Split('|')[8];
                    obj.HUNo = obj.ScanInput?.Split('|')[9] == "" ? "1" : obj.ScanInput?.Split('|')[9];
                    obj.HUSize = obj.ScanInput?.Split('|')[10] == "" ? "1" : obj.ScanInput?.Split('|')[10];
                    

                    


                }

                else if (obj.ScanInput.Split('|').Length == 7)
                {



                    obj.Mcode = obj.ScanInput?.Split('|')[0];
                    var batchgrade = obj.ScanInput?.Split('|')[3];
                    obj.BatchNo = batchgrade.Split('-')[0];
                    obj.LabelSerialNo = "";
                   
                    string RCVDate = obj.ScanInput?.Split('|')[2];
                    obj.MFGDate = RCVDate;
                    //string EXDate = obj.ScanInput?.Split('|')[4];
                    //obj.ExpDate = EXDate; 
                    obj.EXPDate = "";
                    obj.ProjectRefNo = "";
                    obj.KitID = "";
                    obj.Grade = batchgrade.Split('-')[1];
                    obj.LineNumber = "";
                    obj.HUNo = "1";
                    obj.HUSize = "1";





                }



                else
                {
                    obj.Mcode = obj.ScanInput.Split('|')[0];
                    obj.BatchNo = obj.ScanInput.Split('|')[1];
                    obj.MFGDate = obj.ScanInput.Split('|')[2];
                    obj.EXPDate = obj.ScanInput.Split('|')[3];
                    obj.AccountID = obj.AccountID;
                    obj.UserId = obj.UserId;
                    obj.OutboundID = obj.OutboundID;
                }
                return await UpsertLoadedItem(obj);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<List<Outbound>> UpsertLoadedItem(Outbound obj)
        {
            try
            {
                List<Outbound> _oOutbound1 = new List<Outbound>();
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                string _sSQL = "";

                if (obj.ActionType=="Loading")
                {
                     _sSQL = "EXEC [dbo].[USP_UPSERT_OBDLoading] @SkuCode='" + obj.Mcode + "',@ProjectRefNo='" + obj.ProjectRefNo + "',@Batch='" + obj.BatchNo + "',@KitID='" + obj.KitID + "',@Grade='" + obj.Grade + "',@MfgDate='" + obj.MFGDate + "',@ExpDate='" + obj.EXPDate + "',@LabelSerialNo=" + (obj.LabelSerialNo == null ? "null" : DBLibrary.SQuote(obj.LabelSerialNo)) + ",@AccountID=" + obj.AccountID + ",@LineNumber=" + 0 + ",@HUNo=" + obj.HUNo + ",@HUSize=" + obj.HUSize + ",@UserId=" + obj.UserId + ",@OutboundID=" + obj.OutboundID+",@VehicleNumber="+DBLibrary.SQuote(obj.Vehicle)+",@TenantID="+obj.TenantId;
                }
                else
                {
                    _sSQL = "EXEC [dbo].[SP_OBD_Upsert_UNLoading] @SkuCode='" + obj.Mcode + "',@ProjectRefNo='" + obj.ProjectRefNo + "',@Batch='" + obj.BatchNo + "',@KitID='" + obj.KitID + "',@Grade='" + obj.Grade + "',@MfgDate='" + obj.MFGDate + "',@ExpDate='" + obj.EXPDate + "',@LabelSerialNo=" + (obj.LabelSerialNo == null ? "null" : DBLibrary.SQuote(obj.LabelSerialNo)) + ",@AccountID=" + obj.AccountID + ",@LineNumber=" + 0 + ",@HUNo=" + obj.HUNo + ",@HUSize=" + obj.HUSize + ",@UserId=" + obj.UserId + ",@OutboundID=" + obj.OutboundID+",@PalletNumber="+DBLibrary.SQuote(obj.CartonNo)+",@TenantID="+obj.TenantId;
                }




                DataSet _dsLoadItem = DbUtility.GetDS(_sSQL, this.ConnectionString);

                if (_dsLoadItem != null)
                {
                    if (_dsLoadItem.Tables.Count > 0)
                    {
                        foreach (DataRow _drPickList in _dsLoadItem.Tables[0].Rows)
                        {
                            Outbound _oOutbound = new Outbound()
                            {
                                Result = _drPickList["Result"].ToString(),
                                PickedQty = ConversionUtility.ConvertToDecimal(_drPickList["PickedQty"].ToString()),
                                LoadQty = ConversionUtility.ConvertToDecimal(_drPickList["Loadqty"].ToString()),
                                IsLoadComplete = ConversionUtility.ConvertToInt(_drPickList["LoadComplete"].ToString()),
                                Mcode = obj.Mcode,
                                BatchNo = obj.BatchNo,
                                Grade = obj.Grade,
                                UnLoadQty = obj.ActionType == "Loading" ? 0 : ConversionUtility.ConvertToInt(_drPickList["UnLoadQty"].ToString()),
                                LabelSerialNo = obj.LabelSerialNo,
                                MDescription = DbUtility.GetSqlS("Exec SP_Get_MDescription @Mcode=" + DBLibrary.SQuote(obj.Mcode), this.ConnectionString)

                            };
                            _oOutbound1.Add(_oOutbound);
                        }
                    }
                }
                return _oOutbound1;
            }
            catch (WMSExceptionMessage excp)
            {
                throw excp;
            }
            catch (Exception excp)
            {
                ExceptionData oExcpData = new ExceptionData();
                oExcpData.AddInputs("oCriteria", obj);

                ExceptionHandling.LogException(excp, _ClassCode + "001", oExcpData);
                throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = ErrorMessages.WMSExceptionMessage, ShowAsCriticalError = true };
            }
        }
        public async Task<List<Outbound>> GetSkuListForLoading(string OutboundID, int AccountID, int UserId)
        {
            try
            {
                List<Outbound> lOutbound = new List<Outbound>();
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();

                string _sSQL = "EXEC [dbo].[USP_GetMCodesUnderLoadSheet]  @OutboundID=" + OutboundID + ",@AccountID=" + AccountID + ",@UserId=" + UserId + "";

                DataSet _dsPickLists = DbUtility.GetDS(_sSQL, this.ConnectionString);

                if (_dsPickLists != null)
                {
                    if (_dsPickLists.Tables.Count > 0)
                    {
                        foreach (DataRow _drPickList in _dsPickLists.Tables[0].Rows)
                        {
                            Outbound _oOutbound = new Outbound()
                            {
                                Mcode = _drPickList["MCode"].ToString(),
                                PickedQty = ConversionUtility.ConvertToDecimal(_drPickList["PickedQty"].ToString()),
                                LoadQty = ConversionUtility.ConvertToDecimal(_drPickList["Loadqty"].ToString()),
                                SOHeaderID = ConversionUtility.ConvertToInt(_drPickList["SOHeaderID"].ToString()),
                                MFGDate = _drPickList["MfgDate"].ToString(),
                                EXPDate = _drPickList["ExpDate"].ToString(),
                                SerialNo = _drPickList["SerialNo"].ToString(),
                                ProjectRefNo = _drPickList["ProjectRefNo"].ToString(),
                                BatchNo = _drPickList["BatchNo"].ToString(),
                                MRP = _drPickList["MRP"].ToString(),
                                HUNo = _drPickList["HUNo"].ToString(),
                                HUSize = _drPickList["HUSize"].ToString(),
                                TotalQty = _drPickList["TotalQty"].ToString()
                            };

                            lOutbound.Add(_oOutbound);
                        }
                    }
                }

                return lOutbound;
            }
            catch (WMSExceptionMessage excp)
            {
                throw excp;
            }
            catch (Exception excp)
            {
                ExceptionData oExcpData = new ExceptionData();
                oExcpData.AddInputs("oCriteria", OutboundID);

                ExceptionHandling.LogException(excp, _ClassCode + "001", oExcpData);
                throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = ErrorMessages.WMSExceptionMessage, ShowAsCriticalError = true };
            }

        }
    }
}
