using Simpolo_Endpoint.BO;
using Simpolo_Endpoint.Controllers;
using Simpolo_Endpoint.DAO.HHTInterface;
using Simpolo_Endpoint.DAO.Services;
using Simpolo_Endpoint.DBUtil;
using Simpolo_Endpoint.DTO;
using Simpolo_Endpoint.Entities;
using Simpolo_Endpoint.Models;
using Simpolo_Endpoint.ModelsLibrary;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Simpolo_Endpoint.DAO.HHTServices
{
    public class InternalTransferService : AppDBService, IInternalTransfer
    {
        //BaseController baseController = new BaseController();
        public InternalTransferService(IOptions<AppSettings> appSettings) : base(appSettings)
        {

        }

        private string _ClassCode = string.Empty;

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<InventoryDTO> TransferPallettoLocation(InventoryDTO obj , int UserID)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
       {
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                StringBuilder sCmdTransferPallettoLocation = new StringBuilder();
                StringBuilder AutomaticGRN = new StringBuilder();


                sCmdTransferPallettoLocation.AppendLine("EXEC [dbo].[sp_INV_TransferPalletToBin] ");
                sCmdTransferPallettoLocation.AppendLine("@CartonCode = " + DBLibrary.SQuote(obj.ContainerCode) + ",");
                sCmdTransferPallettoLocation.AppendLine("@TenantID = " + obj.TenantID + ",");
                sCmdTransferPallettoLocation.AppendLine("@WarehouseID = " + obj.WarehouseID + ",");
                sCmdTransferPallettoLocation.AppendLine("@LocationCode = " + DBLibrary.SQuote(obj.LocationCode) + ",");
                sCmdTransferPallettoLocation.AppendLine("@CreatedBy = " + UserID + ",");
                sCmdTransferPallettoLocation.AppendLine("@MaterialCode = " + DBLibrary.SQuote(obj.MaterialCode) + ",");
                sCmdTransferPallettoLocation.AppendLine("@BatchNo = " + DBLibrary.SQuote(obj.BatchNo) + ",");
                sCmdTransferPallettoLocation.AppendLine("@SerialNo = " + DBLibrary.SQuote(obj.SerialNo) + ",");
                sCmdTransferPallettoLocation.AppendLine("@MfgDate = " + DBLibrary.SQuote(obj.MfgDate) + ",");
                sCmdTransferPallettoLocation.AppendLine("@ExpDate = " + DBLibrary.SQuote(obj.ExpDate) + ",");
                sCmdTransferPallettoLocation.AppendLine("@ProjectNo = " + DBLibrary.SQuote(obj.ProjectNo) + ",");
                sCmdTransferPallettoLocation.AppendLine("@Quantity = " + DBLibrary.SQuote(obj.Quantity) + ",");
                sCmdTransferPallettoLocation.AppendLine("@MRP = " + DBLibrary.SQuote(obj.MRP) + ",");
                sCmdTransferPallettoLocation.AppendLine("@IsSkipReasonId = " + obj.ReasonId + "");

                string Query = sCmdTransferPallettoLocation.ToString();
                DataSet _dsResults = DbUtility.GetDS(Query, this.ConnectionString);

                if (_dsResults != null && _dsResults.Tables.Count != 0 && _dsResults.Tables[0].Rows.Count != 0)
                {
                    if (_dsResults.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        await Task.Delay(TimeSpan.FromSeconds(3));
                        AutomaticGRN.AppendLine("Exec SP_Upsert_AutoGRN @CartonCode = " + DBLibrary.SQuote(obj.ContainerCode) + ",");
                        AutomaticGRN.AppendLine("@TenantID = " + obj.TenantID + ",");
                        AutomaticGRN.AppendLine("@WarehouseID = " + obj.WarehouseID + ",");
                        AutomaticGRN.AppendLine("@LocationCode = " + DBLibrary.SQuote(obj.LocationCode) + ",");
                        AutomaticGRN.AppendLine("@CreatedBy = " + UserID + "");
                        string QueryGRN = AutomaticGRN.ToString();
                        DataSet _dsResultsGRN = DbUtility.GetDS(QueryGRN, this.ConnectionString);
                        return obj;
                    }
                    else if (_dsResults.Tables[0].Rows[0][0].ToString() == "-111")
                    {
                        throw new WMSExceptionMessage() { WMSExceptionCode = "WMC_DAL_INV_0001", WMSMessage = ErrorMessages.WMC_DAL_INV_0001, ShowAsError = true };
                    }
                    else if (_dsResults.Tables[0].Rows[0][0].ToString() == "-222")
                    {
                        throw new WMSExceptionMessage() { WMSExceptionCode = "WMC_DAL_INV_0002", WMSMessage = ErrorMessages.WMC_DAL_INV_0002, ShowAsError = true };
                    }
                    else if (_dsResults.Tables[0].Rows[0][0].ToString() == "-333")
                    {
                        throw new WMSExceptionMessage() { WMSExceptionCode = "WMC_DAL_INV_0003", WMSMessage = ErrorMessages.WMC_DAL_INV_0003, ShowAsError = true };
                    }
                    else if (_dsResults.Tables[0].Rows[0][0].ToString() == "-444")
                    {
                        throw new WMSExceptionMessage() { WMSExceptionCode = "WMC_DAL_INV_0004", WMSMessage = ErrorMessages.WMC_DAL_INV_0004, ShowAsError = true };
                    }
                    else if (_dsResults.Tables[0].Rows[0][0].ToString() == "-1")
                    {
                        throw new WMSExceptionMessage() { WMSExceptionCode = "WMC_DAL_INV_0005", WMSMessage = ErrorMessages.WMC_DAL_INV_0005, ShowAsError = true };

                    }
                    else if (_dsResults.Tables[0].Rows[0][0].ToString() == "-555")
                    {
                        throw new WMSExceptionMessage() { WMSExceptionCode = "WMC_DAL_INV_0006", WMSMessage = ErrorMessages.WMC_DAL_INV_0006, ShowAsError = true };
                    }
                    else if (_dsResults.Tables[0].Rows[0][0].ToString() == "-2")
                    {
                        throw new WMSExceptionMessage() { WMSExceptionCode = "WMC_DAL_INV_0008", WMSMessage = ErrorMessages.WMC_DAL_INV_0008, ShowAsError = true };
                    }
                    else if (_dsResults.Tables[0].Rows[0][0].ToString() == "-777")
                    {
                        throw new WMSExceptionMessage() { WMSExceptionCode = "WMC_DAL_INV_0009", WMSMessage = ErrorMessages.WMC_DAL_INV_0009, ShowAsError = true };
                    }
                    else
                    {
                        throw new WMSExceptionMessage() { WMSExceptionCode = "WMC_DAL_INV_0007", WMSMessage = ErrorMessages.WMC_DAL_INV_0007, ShowAsError = true };
                    }
                }
                else
                {

                    throw new WMSExceptionMessage() { WMSExceptionCode = "WMC_DAL_INV_0005", WMSMessage = ErrorMessages.WMC_DAL_INV_0005, ShowAsError = true };
                }

#pragma warning disable CS0162 // Unreachable code detected
                return obj;
#pragma warning restore CS0162 // Unreachable code detected
            }
            catch (WMSExceptionMessage excp)
            {
                throw excp;
            }
            catch (Exception excp)
            {
                ExceptionData oExcpData = new ExceptionData();
                oExcpData.AddInputs("ScannedItem", obj);

                ExceptionHandling.LogException(excp, _ClassCode + "001", oExcpData);
                throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = ErrorMessages.WMSExceptionMessage, ShowAsCriticalError = true };
            }
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<string> GetConatinerLocationBin(string ContainerCode, string WarehouseId, string UserID, string LocationCheck)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();

            if (WarehouseId == "")
            {
                WarehouseId = "0";
            }
          
            string Query = "EXEC [dbo].[SP_CHECK_CONTAINER_LOCATION_MAPPING]  @CONTAINER=" + DBLibrary.SQuote(ContainerCode) + ",@WarehouseID=" + WarehouseId + ",@UserID=" + UserID + ",@Location=" + DBLibrary.SQuote(LocationCheck);
            string Location = DbUtility.GetSqlS(Query , ConnectionString).ToString();

            return Location;
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<TransferBO> GetAvailbleQtyList(TransferBO transferBO)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            DataSet dataset = null;

            if (transferBO.WarehouseID == "")
            {
                transferBO.WarehouseID = "0";
            }

            if (transferBO.TenantID == "")
            {
                transferBO.TenantID = "0";
            }

            if (transferBO.UserID == "")
            {
                transferBO.UserID = "0";
            }

            string query = "EXEC [dbo].[sp_INV_GetAvailableQty_HHT]  @Mcode=" + (transferBO.MCode != "" ? DBLibrary.SQuote(transferBO.MCode) : "''") + ", @SLOC=" + DBLibrary.SQuote(transferBO.FromSLoc) + ", " +
                "@Location=" + DBLibrary.SQuote(transferBO.FromLocation) + ",@CartonCode=" + DBLibrary.SQuote(transferBO.FromCartonNo) + ",@MfgDate=" + ((transferBO.MfgDate != "") ? DBLibrary.SQuote(transferBO.MfgDate) : "''") + ",@ExpDate=" + ((transferBO.ExpDate != "") ? DBLibrary.SQuote(transferBO.ExpDate) : "''") + ",@SerialNo=" + ((transferBO.SerialNo != "") ? DBLibrary.SQuote(transferBO.SerialNo) : "''") + ",@BatchNo = " + ((transferBO.BatchNo != "") ? DBLibrary.SQuote(transferBO.BatchNo) : "''") + ",@ProjectRefNo = " + (((transferBO.ProjectNo != "") ? DBLibrary.SQuote(transferBO.ProjectNo) : "''")) + " ";
            query += ",@TenantID=" + transferBO.TenantID + ",@WarehouseID=" + transferBO.WarehouseID + ",@MRP=" + ((transferBO.MRP != "") ? DBLibrary.SQuote(transferBO.MRP) : "''") + ",@UserID=" + transferBO.UserID + ",@IsWorkOrder = " + transferBO.TransferOrderId + "";
            
            dataset = DbUtility.GetDS(query, this.ConnectionString);

            if (dataset != null && dataset.Tables[0] != null && dataset.Tables[0].Rows.Count != 0)
            {
                transferBO.AvailQty = dataset.Tables[0].Rows[0]["AvailableQuantity"].ToString();
                //transferBO.FromSLoc = dataset.Tables[0].Rows[0]["SLOC"].ToString();
            }

            return transferBO;
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<List<Inventory>> GetActivestockStorageLocations(string MCode, int UserId)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            try
            {
                List<Inventory> inventories = new List<Inventory>();            

                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                StringBuilder sCmdActivestockStorageLocations = new StringBuilder();

                sCmdActivestockStorageLocations.AppendLine("EXEC [dbo].[USP_INV_GetActiveStockStorageLocations] ");
                sCmdActivestockStorageLocations.AppendLine("@Mcode = " + DBLibrary.SQuote(MCode) + ",");
                sCmdActivestockStorageLocations.AppendLine("@UserID = " + UserId + "");
               
                string Query = sCmdActivestockStorageLocations.ToString();
                DataSet _dsResults = DbUtility.GetDS(Query, this.ConnectionString);

                if (_dsResults != null)
                {
                    if (_dsResults.Tables.Count > 0)
                    {
                        foreach (DataRow _drPickList in _dsResults.Tables[0].Rows)
                        {
                            Inventory _oInventory = new Inventory()
                            {
                                StorageLocationID = ConversionUtility.ConvertToInt(_drPickList["ID"].ToString()),
                                StorageLocation = _drPickList["Code"].ToString()
                            };
                            inventories.Add(_oInventory);
                        }
                    }
                }

                return inventories;
            }
            catch (Exception)
            {
                throw;
            }
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<TransferBO> UpsertBinToBinTransferItem(TransferBO transferBO)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            string drlStatement = "EXEC [dbo].[Upsert_BinToBinTransfer_HHT] @FromLocation=" + DBLibrary.SQuote(transferBO.FromLocation) + ",@MfgDate=" + (transferBO.MfgDate != "" ? DBLibrary.SQuote(transferBO.MfgDate) : "''") + ",@ExpDate=" + (transferBO.ExpDate != "" ? DBLibrary.SQuote(transferBO.ExpDate) : "''") + ",@SerialNo=" + (transferBO.SerialNo != "" ? DBLibrary.SQuote(transferBO.SerialNo) : "''") + ",@BatchNo = " + (transferBO.BatchNo != "" ? DBLibrary.SQuote(transferBO.BatchNo) : "''") + ",@ProjectRefNo = " + (transferBO.ProjectNo != "" ? DBLibrary.SQuote(transferBO.ProjectNo) : "''") + ",@MRP = " + (transferBO.MRP != "" ? DBLibrary.SQuote(transferBO.MRP) : "''") + ",@CreatedBy=" + transferBO.UserId + ",@FromCarton=" + DBLibrary.SQuote(transferBO.FromCartonNo) + ",@MCODE=" + DBLibrary.SQuote(transferBO.MCode) + ",@TLoc=" + DBLibrary.SQuote(transferBO.ToLocation) + ",@Quantity=" + Convert.ToDecimal(transferBO.TransferQty) + ",@FromSLOC=" + DBLibrary.SQuote(transferBO.FromSLoc) + ",@ToSLOC=" + DBLibrary.SQuote(transferBO.ToSLoc) + ",@ToCarton=" + DBLibrary.SQuote(transferBO.ToCartonNo) + ",@TenantID=" + DBLibrary.SQuote(transferBO.TenantID) + ",@WarehouseID=" + DBLibrary.SQuote(transferBO.WarehouseID) + ",@IsWorkOrder=" + transferBO.TransferOrderId + ",@EmpreqNumber=" + DBLibrary.SQuote("") + "";
           

            string result = DbUtility.GetSqlS(drlStatement, ConnectionString);

            transferBO.Result = result;

            //if (result == -2)
            //{
            //    transferBO.Result = "Mis Matched container Configuration";
            //}
            //else if (result == -3)
            //{
            //    transferBO.Result = "Container is Configured to different Loc.";
            //}
            //else if (result == -4)
            //{
            //    transferBO.Result = "No stock available";
            //}
            //else if (result == -5)
            //{
            //    transferBO.Result = "Quantity exceeded";
            //}
            //else if (result == -6)
            //{
            //    transferBO.Result = "Another item already exist in the bin";
            //}
            //else
            //{
            //    transferBO.Result = "1";
            //}

            return transferBO;
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<List<Inventory>> GetSKUList(string outboundId)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                List<Inventory> inventories = new List<Inventory>();
                string _sbSQL = ("EXEC [dbo].[GetSkuList] @OutboundID = " + outboundId + "");
                DataSet _dsResults = DbUtility.GetDS(_sbSQL, this.ConnectionString);

                if (_dsResults != null)
                {
                    if (_dsResults.Tables.Count > 0)
                    {
                        foreach (DataRow dr in _dsResults.Tables[0].Rows)
                        {
                            Inventory inventory = new Inventory()
                            {
                                MaterialCode = dr["MCode"].ToString(),
                                Quantity = ConversionUtility.ConvertToDecimal(dr["Quantity"].ToString()),
                                BatchNumber = dr["BatchNo"].ToString(),
                                VLPDId = Convert.ToInt32(dr["VlpdID"])
                            };
                            inventories.Add(inventory);
                        }
                    }
                }
                return inventories;
            }
            catch (WMSExceptionMessage excp)
            {
                throw excp;
            }
            catch (Exception excp)
            {
                ExceptionData oExcpData = new ExceptionData();
                oExcpData.AddInputs("ScannedItem", outboundId);

                ExceptionHandling.LogException(excp, _ClassCode + "001", oExcpData);
                throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = ErrorMessages.WMSExceptionMessage, ShowAsCriticalError = true };
            }
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<List<Inventory>> GetLocationsBySKU(string Mcode, string BatchNo, string IsMoreOptions , string IsWorkOrder)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                List<Inventory> inventories = new List<Inventory>();

                string isMoreOptions = string.IsNullOrEmpty(IsMoreOptions) ? "0" : IsMoreOptions;
                string isWorkOrder = string.IsNullOrEmpty(IsWorkOrder) ? "0" : IsWorkOrder;

                string _sbSQL = "EXEC [dbo].[GetStockForSKU] @Mcode = " + DBLibrary.SQuote(Mcode) + ",@BatchNo = " + DBLibrary.SQuote(BatchNo) + ",@IsMoreOptions = " + DBLibrary.SQuote(isMoreOptions) + ",@IsWorkOrder = " + DBLibrary.SQuote(isWorkOrder) + "";
                DataSet _dsResults = DbUtility.GetDS(_sbSQL, this.ConnectionString);

                if (_dsResults != null)
                {
                    if (_dsResults.Tables.Count > 0)
                    {
                        foreach (DataRow dr in _dsResults.Tables[0].Rows)
                        {
                            Inventory inventory = new Inventory()
                            {
                                MaterialCode = dr["MCode"].ToString(),
                                Quantity = ConversionUtility.ConvertToDecimal(dr["Quantity"].ToString()),
                                LocationCode = dr["Location"].ToString(),
                                BatchNumber = dr["BatchNo"].ToString()
                            };
                            inventories.Add(inventory);
                        }
                    }
                }
                return inventories;
            }
            catch (WMSExceptionMessage excp)
            {
                throw excp;
            }
            catch (Exception excp)
            {
                ExceptionData oExcpData = new ExceptionData();
                oExcpData.AddInputs("ScannedItem", Mcode);

                ExceptionHandling.LogException(excp, _ClassCode + "001", oExcpData);
                throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = ErrorMessages.WMSExceptionMessage, ShowAsCriticalError = true };
            }
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<List<Inventory>> GetDockLocations()
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                List<Inventory> inventories = new List<Inventory>();
                string _sbSQL = ("exec [dbo].[GetDockLocations] ");
                DataSet _dsResults = DbUtility.GetDS(_sbSQL, this.ConnectionString);

                if (_dsResults != null)
                {
                    if (_dsResults.Tables.Count > 0)
                    {
                        foreach (DataRow dr in _dsResults.Tables[0].Rows)
                        {
                            Inventory inventory = new Inventory()
                            {
                                LocationCode = dr["DisplayLocationCode"].ToString(),
                                LocationID = Convert.ToInt32(dr["locationid"])
                            };
                            inventories.Add(inventory);
                        }
                    }
                }
                return inventories;
            }
            catch (WMSExceptionMessage excp)
            {
                throw excp;
            }
            catch (Exception excp)
            {
                ExceptionData oExcpData = new ExceptionData();
                oExcpData.AddInputs("ScannedItem", "");

                ExceptionHandling.LogException(excp, _ClassCode + "001", oExcpData);
                throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = ErrorMessages.WMSExceptionMessage, ShowAsCriticalError = true };
            }
        }

        public async Task<List<Inventory>> GetLoadingPoints(string TenantID, int UserID, string WarehouseId,string VehicleNumber)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                List<Inventory> inventories = new List<Inventory>();
                string _sbSQL = ("exec [dbo].[SP_GetLoadingPoints_HHT] @TenantID=" + TenantID+",@UserID="+UserID+",@WarehouseId="+(DBUtil.DBLibrary.SQuote(WarehouseId)) +",@VehicleNumber="+DBUtil.DBLibrary.SQuote(VehicleNumber)+"");
                DataSet _dsResults = DbUtility.GetDS(_sbSQL, this.ConnectionString);

                if (_dsResults != null)
                {
                    if (_dsResults.Tables.Count > 0)
                    {
                        foreach (DataRow dr in _dsResults.Tables[0].Rows)
                        {
                            Inventory inventory = new Inventory()
                            {
                                LoadingPoint = dr["LoadingPoint"].ToString(),
                                LoadingPointID = Convert.ToInt32(dr["LoadingPointID"])
                            };
                            inventories.Add(inventory);
                        }
                    }
                }
                return inventories;
            }
            catch (WMSExceptionMessage excp)
            {
                throw excp;
            }
            catch (Exception excp)
            {
                ExceptionData oExcpData = new ExceptionData();
                oExcpData.AddInputs("ScannedItem", "");

                ExceptionHandling.LogException(excp, _ClassCode + "001", oExcpData);
                throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = ErrorMessages.WMSExceptionMessage, ShowAsCriticalError = true };
            }
        }


        public async Task<List<Inventory>> GetLocations(string RefNumber, string WarehouseId, string TenantID, int UserID, int InboundID, string IsScerario)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                List<Inventory> inventories = new List<Inventory>();

                StringBuilder sCmdTransferPallettoLocation_Putaway = new StringBuilder();
                sCmdTransferPallettoLocation_Putaway.AppendLine("EXEC [dbo].[sp_GetLocations] ");
                sCmdTransferPallettoLocation_Putaway.AppendLine("@RefNumber = " + DBLibrary.SQuote(RefNumber) + ",");
                sCmdTransferPallettoLocation_Putaway.AppendLine("@TenantID = " + TenantID + ",");
                sCmdTransferPallettoLocation_Putaway.AppendLine("@WarehouseID = " + WarehouseId + ",");
                sCmdTransferPallettoLocation_Putaway.AppendLine("@InboundID = " + InboundID+ ",");
                sCmdTransferPallettoLocation_Putaway.AppendLine("@CreatedBy = " + UserID + ",");
                sCmdTransferPallettoLocation_Putaway.AppendLine("@IsScerario = " + IsScerario + "");


                string Query = sCmdTransferPallettoLocation_Putaway.ToString();
                DataSet _dsResults = DbUtility.GetDS(Query, this.ConnectionString);


                if (_dsResults != null)
                {
                    if (_dsResults.Tables.Count > 0)
                    {
                        foreach (DataRow dr in _dsResults.Tables[0].Rows)
                        {
                            Inventory inventory = new Inventory()
                            {
                                LocationCode = dr["LocationCode"].ToString(),
                                LocationID = Convert.ToInt32(dr["LocationId"])
                            };
                            inventories.Add(inventory);
                        }
                    }
                }
                return inventories;
            }
            catch (WMSExceptionMessage excp)
            {
                throw excp;
            }
            catch (Exception excp)
            {
                ExceptionData oExcpData = new ExceptionData();
                oExcpData.AddInputs("ScannedItem", "");

                ExceptionHandling.LogException(excp, _ClassCode + "001", oExcpData);
                throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = ErrorMessages.WMSExceptionMessage, ShowAsCriticalError = true };
            }
        }



        public async Task<List<Inventory>> GetLocationsBatchPicking(string RefNumber, string WarehouseId, string TenantID, int UserID, int InboundID, string IsScerario, string MaterialCode, string BatchNo, string Grade,string vlpdid)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                List<Inventory> inventories = new List<Inventory>();

                StringBuilder sCmdTransferPallettoLocation_Putaway = new StringBuilder();
                sCmdTransferPallettoLocation_Putaway.AppendLine("EXEC [dbo].[sp_GetLocations_BatchPicking] ");
                sCmdTransferPallettoLocation_Putaway.AppendLine("@RefNumber = " + DBLibrary.SQuote(RefNumber) + ",");
                sCmdTransferPallettoLocation_Putaway.AppendLine("@TenantID = " + TenantID + ",");
                sCmdTransferPallettoLocation_Putaway.AppendLine("@WarehouseID = " + WarehouseId + ",");
                sCmdTransferPallettoLocation_Putaway.AppendLine("@InboundID = " + InboundID + ",");
                sCmdTransferPallettoLocation_Putaway.AppendLine("@CreatedBy = " + UserID + ",");
                sCmdTransferPallettoLocation_Putaway.AppendLine("@IsScerario = " + IsScerario + ",");
                sCmdTransferPallettoLocation_Putaway.AppendLine("@MaterialCode = " + DBLibrary.SQuote(MaterialCode) + ",");
                sCmdTransferPallettoLocation_Putaway.AppendLine("@BatchNo = " + DBLibrary.SQuote(BatchNo) + ",");
                sCmdTransferPallettoLocation_Putaway.AppendLine("@Grade = " + DBLibrary.SQuote(Grade) + ",");
                sCmdTransferPallettoLocation_Putaway.AppendLine("@VLPDID = " + vlpdid + "") ;

                string Query = sCmdTransferPallettoLocation_Putaway.ToString();
                DataSet _dsResults = DbUtility.GetDS(Query, this.ConnectionString);


                if (_dsResults != null)
                {
                    if (_dsResults.Tables.Count > 0)
                    {
                        foreach (DataRow dr in _dsResults.Tables[0].Rows)
                        {
                            Inventory inventory = new Inventory()
                            {
                                LocationCode = dr["LocationCode"].ToString(),
                                LocationID = Convert.ToInt32(dr["LocationId"])
                            };
                            inventories.Add(inventory);
                        }
                    }
                }
                return inventories;
            }
            catch (WMSExceptionMessage excp)
            {
                throw excp;
            }
            catch (Exception excp)
            {
                ExceptionData oExcpData = new ExceptionData();
                oExcpData.AddInputs("ScannedItem", "");

                ExceptionHandling.LogException(excp, _ClassCode + "001", oExcpData);
                throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = ErrorMessages.WMSExceptionMessage, ShowAsCriticalError = true };
            }
        }







#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<int> WorkOrderLineItemComplete(string vLPDID)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                string _sbSQL = "exec [dbo].[WorkOrderLineItemComplete] @vLPDID = " + vLPDID + "";
                DataSet ds = DbUtility.GetDS(_sbSQL, this.ConnectionString);
                if (Convert.ToInt32(ds.Tables[0].Rows[0]["N"]) == 1)
                {
                    return 1;
                }
                else
                {
                    throw new WMSExceptionMessage() { WMSExceptionCode = "WMSExceptionMessage", WMSMessage = ErrorMessages.WMSExceptionMessage, ShowAsError = true };
                }
            }
            catch (WMSExceptionMessage excp)
            {
                throw excp;
            }
            catch (Exception excp)
            {
                ExceptionData oExcpData = new ExceptionData();
                oExcpData.AddInputs("ScannedItem", vLPDID);

                ExceptionHandling.LogException(excp, _ClassCode + "001", oExcpData);
                throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = ErrorMessages.WMSExceptionMessage, ShowAsCriticalError = true };
            }
        }



        public async Task<List<Inventory>> MaterialTransferBlockComplete(int TransferRequestID,int UserID,int BlockReasonID)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            Inventory inventory = new Inventory();
            List<Inventory> list = new List<Inventory>();
            try
            {
                string _sbSQL = "exec [dbo].[SP_Complete_MaterialTransferBlockItem] @TransferRequestID = " + TransferRequestID + ",@UserID="+UserID+ ",@BlockReasonID=" + BlockReasonID;
                string Result = DbUtility.GetSqlS(_sbSQL, this.ConnectionString);

                if(Result.Contains("Success"))
                {
                    inventory.Result = 1;
                    inventory.ResponseMessage = Result;
                }
                else
                {

                    inventory.Result = 1;
                    inventory.ResponseMessage = Result;
                }

                list.Add(inventory);
                return list;

                 
            }
            catch (WMSExceptionMessage excp)
            {
                throw excp;
            }
            catch (Exception excp)
            {
                ExceptionData oExcpData = new ExceptionData();
                oExcpData.AddInputs("ScannedItem", TransferRequestID);

                ExceptionHandling.LogException(excp, _ClassCode + "001", oExcpData);
                throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = ErrorMessages.WMSExceptionMessage, ShowAsCriticalError = true };
            }
        }


#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<Inventory> TransferPallettoLocation_Putaway(Inventory obj)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                StringBuilder sCmdTransferPallettoLocation_Putaway = new StringBuilder();

                sCmdTransferPallettoLocation_Putaway.AppendLine("EXEC [dbo].[USP_INV_Putawat_AtBin] ");
                sCmdTransferPallettoLocation_Putaway.AppendLine("@CartonCode = " + DBLibrary.SQuote(obj.ContainerCode) + ",");
                sCmdTransferPallettoLocation_Putaway.AppendLine("@TenantID = " + obj.TenantID + ",");
                sCmdTransferPallettoLocation_Putaway.AppendLine("@WarehouseID = " + obj.WarehouseID + ",");
                sCmdTransferPallettoLocation_Putaway.AppendLine("@LocationCode = " + DBLibrary.SQuote(obj.LocationCode) + ",");
                sCmdTransferPallettoLocation_Putaway.AppendLine("@CreatedBy = " + obj.UserId + ",");
                sCmdTransferPallettoLocation_Putaway.AppendLine("@Mcode = " + DBLibrary.SQuote(obj.MaterialCode) + ",");
                sCmdTransferPallettoLocation_Putaway.AppendLine("@TransferQty = " + obj.Quantity + "");

                string Query = sCmdTransferPallettoLocation_Putaway.ToString();
                DataSet _dsResults = DbUtility.GetDS(Query, this.ConnectionString);

                if (_dsResults != null && _dsResults.Tables.Count != 0 && _dsResults.Tables[0].Rows.Count != 0)
                {
                    if (_dsResults.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        return obj;
                    }
                    else if (_dsResults.Tables[0].Rows[0][0].ToString() == "-111")
                    {
                        throw new WMSExceptionMessage() { WMSExceptionCode = "WMC_DAL_INV_0001", WMSMessage = ErrorMessages.WMC_DAL_INV_0001, ShowAsError = true };
                    }
                    else if (_dsResults.Tables[0].Rows[0][0].ToString() == "-222")
                    {
                        throw new WMSExceptionMessage() { WMSExceptionCode = "WMC_DAL_INV_0002", WMSMessage = ErrorMessages.WMC_DAL_INV_0002, ShowAsError = true };
                    }
                    else if (_dsResults.Tables[0].Rows[0][0].ToString() == "-333")
                    {
                        throw new WMSExceptionMessage() { WMSExceptionCode = "WMC_DAL_INV_0003", WMSMessage = ErrorMessages.WMC_DAL_INV_0003, ShowAsError = true };
                    }
                    else if (_dsResults.Tables[0].Rows[0][0].ToString() == "-444")
                    {
                        throw new WMSExceptionMessage() { WMSExceptionCode = "WMC_DAL_INV_0004", WMSMessage = ErrorMessages.WMC_DAL_INV_0004, ShowAsError = true };
                    }
                    else if (_dsResults.Tables[0].Rows[0][0].ToString() == "-1")
                    {
                        throw new WMSExceptionMessage() { WMSExceptionCode = "WMC_DAL_INV_0005", WMSMessage = ErrorMessages.WMC_DAL_INV_0005, ShowAsError = true };
                    }
                    else if (_dsResults.Tables[0].Rows[0][0].ToString() == "-555")
                    {
                        throw new WMSExceptionMessage() { WMSExceptionCode = "WMC_DAL_INV_0006", WMSMessage = ErrorMessages.WMC_DAL_INV_0006, ShowAsError = true };
                    }
                    else
                    {
                        throw new WMSExceptionMessage() { WMSExceptionCode = "WMC_DAL_INV_0007", WMSMessage = ErrorMessages.WMC_DAL_INV_0007, ShowAsError = true };
                    }
                }
                else
                {
                    throw new WMSExceptionMessage() { WMSExceptionCode = "WMC_DAL_INV_0005", WMSMessage = ErrorMessages.WMC_DAL_INV_0005, ShowAsError = true };
                }

#pragma warning disable CS0162 // Unreachable code detected
                return obj;
#pragma warning restore CS0162 // Unreachable code detected
            }
            catch (WMSExceptionMessage excp)
            {
                throw excp;
            }
            catch (Exception excp)
            {
                ExceptionData oExcpData = new ExceptionData();
                oExcpData.AddInputs("ScannedItem", obj);

                ExceptionHandling.LogException(excp, _ClassCode + "001", oExcpData);
                throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = ErrorMessages.WMSExceptionMessage, ShowAsCriticalError = true };
            }
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<TransferBO> UpsertPalletBuilding(TransferBO transferBO)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                string drlStatement = "EXEC [dbo].[USP_INV_Upsert_PalletBuilding_HHT] @FromLocation=" + DBLibrary.SQuote(transferBO.FromLocation) + ",@CreatedBy=" + transferBO.UserId + ",@FromCarton=" + DBLibrary.SQuote(transferBO.FromCartonNo) + ",@MCODE=" + DBLibrary.SQuote(transferBO.MCode) + ",@Quantity=" + Convert.ToDecimal(transferBO.TransferQty) + ",@FromSLOC=" + DBLibrary.SQuote(transferBO.FromSLoc) + ",@ToCarton=" + DBLibrary.SQuote(transferBO.ToCartonNo) + ",@UserID=" + transferBO.UserId;
                int result = DbUtility.GetSqlN(drlStatement, ConnectionString);
                if (result == -2)
                {
                    transferBO.Result = "Mis Matched container Configuration";
                }
                else if (result == -3)
                {
                    transferBO.Result = "Container is Configured to different Loc.";
                }
                else if (result == -4)
                {
                    transferBO.Result = "No stock available";
                }
                else if (result == -5)
                {
                    transferBO.Result = "Quantity exceeded";
                }
                else
                {
                    transferBO.Result = "1";
                }
                return transferBO;
            }
            catch (Exception ex)
            {
                transferBO.Result = ex.Message;
                return transferBO;
            }
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<List<Inventory>> GetSlocWiseActiveStock(Inventory obj)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                List<Inventory> inventories = new List<Inventory>();
                StringBuilder sCmdTransferPallettoLocation = new StringBuilder();

                sCmdTransferPallettoLocation.AppendLine("EXEC [dbo].[USP_API_INV_Get_SLOCWiseActiveStockDetails_MaterialTransfer] ");
                sCmdTransferPallettoLocation.AppendLine("@CartonCode = " + DBLibrary.SQuote(obj.ContainerCode) + ",");
                sCmdTransferPallettoLocation.AppendLine("@TenantID = " + obj.TenantID + ",");
                sCmdTransferPallettoLocation.AppendLine("@ProjectRefNo = " + DBLibrary.SQuote(obj.ProjectRefNo) + ",");
                sCmdTransferPallettoLocation.AppendLine("@WarehouseID = " + obj.WarehouseID + ",");
                sCmdTransferPallettoLocation.AppendLine("@LocationCode = " + DBLibrary.SQuote(obj.LocationCode) + ",");
                sCmdTransferPallettoLocation.AppendLine("@MCode = " + DBLibrary.SQuote(obj.MaterialCode) + ",");
                sCmdTransferPallettoLocation.AppendLine("@BATCHNO = " + DBLibrary.SQuote(obj.BatchNumber) + ",");
                sCmdTransferPallettoLocation.AppendLine("@MFGDate = " + DBLibrary.SQuote(obj.Mfg_Date) + ",");
                sCmdTransferPallettoLocation.AppendLine("@EXPDate = " + DBLibrary.SQuote(obj.ExpDate) + ",");
                sCmdTransferPallettoLocation.AppendLine("@SerialNo = " + DBLibrary.SQuote(obj.SerialNo) + ",");
                sCmdTransferPallettoLocation.AppendLine("@MRP = " + "'"+obj.MRP+"'" + "");

                string Query = sCmdTransferPallettoLocation.ToString();
                DataSet _dsResults = DbUtility.GetDS(Query, this.ConnectionString);

                if (_dsResults != null)
                {
                    if (_dsResults.Tables.Count > 0)
                    {
                        foreach (DataRow dr in _dsResults.Tables[0].Rows)
                        {
                            Inventory inventory = new Inventory()
                            {
                                StorageLocation = dr["Code"].ToString(),
                                AvailableQuantity = ConversionUtility.ConvertToDecimal(dr["AvlQty"].ToString())
                            };
                            inventories.Add(inventory);
                        }
                    }
                }

                return inventories;
            }
            catch (WMSExceptionMessage excp)
            {
                throw excp;
            }
            catch (Exception excp)
            {
                ExceptionData oExcpData = new ExceptionData();
                oExcpData.AddInputs("ScannedItem", obj);

                ExceptionHandling.LogException(excp, _ClassCode + "001", oExcpData);
                throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = ErrorMessages.WMSExceptionMessage, ShowAsCriticalError = true };
            }
        }

        public async Task<List<Inventory>> UpdateMaterialTrasnfer(Inventory obj)
        {
            Payload<string> response = new Payload<string>();
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                List<Inventory> inventories = new List<Inventory>();
                StringBuilder sCmdTransferPallettoLocation = new StringBuilder();

                sCmdTransferPallettoLocation.AppendLine("EXEC [dbo].[USP_API_INV_MaterialTransfer] ");
                sCmdTransferPallettoLocation.AppendLine("@CartonCode = " + DBLibrary.SQuote(obj.ContainerCode) + ",");
                sCmdTransferPallettoLocation.AppendLine("@Tenat_ID = " + obj.TenantID + ",");
                sCmdTransferPallettoLocation.AppendLine("@WarehouseId = " + obj.WarehouseID + ",");
                sCmdTransferPallettoLocation.AppendLine("@LocationCode = " + DBLibrary.SQuote(obj.LocationCode) + ",");
                sCmdTransferPallettoLocation.AppendLine("@Mcode = " + DBLibrary.SQuote(obj.MaterialCode) + ",");
                sCmdTransferPallettoLocation.AppendLine("@BatchNo = " + DBLibrary.SQuote(obj.BatchNumber) + ",");
                sCmdTransferPallettoLocation.AppendLine("@UserID = " + obj.UserId + ",");
                sCmdTransferPallettoLocation.AppendLine("@FromSL = " + DBLibrary.SQuote(obj.StorageLocation) + ",");
                sCmdTransferPallettoLocation.AppendLine("@ToSL = " + DBLibrary.SQuote(obj.ToStorageLocation) + ",");
                sCmdTransferPallettoLocation.AppendLine("@Quantity = " + obj.Quantity + "");

                string Query = sCmdTransferPallettoLocation.ToString();
                DataSet ds = DbUtility.GetDS(Query, this.ConnectionString);

                int StatusID = 0;
                int TransferTypeID = 0;
                string uniqueID = "";
                if (ds.Tables.Count > 0)
                {

                    StatusID = Convert.ToInt32(ds.Tables[0].Rows[0]["StatusID"]);
                    TransferTypeID = Convert.ToInt32(ds.Tables[0].Rows[0]["TransferTypeID"]);
                    obj.TransferRequestedID = Convert.ToInt32(ds.Tables[0].Rows[0]["TransferRequestID"]);
                    uniqueID = DateTime.Now.ToString("ddhhmmss");

                }


                if (TransferTypeID == 5)
                {
                    if (ds.Tables.Count > 1)
                    {


                        int strTransferRequestID = 0;
                        strTransferRequestID += obj.TransferRequestedID;

                        string result1 = await PostDataToSAPMaterialTransfer(obj.TransferRequestedID, obj.UserId);
                        if (result1.Contains("SAP Error:"))
                        {
                            response.Result = "-1";
                            throw new WMSExceptionMessage() { WMSExceptionCode = null, WMSMessage = result1, ShowAsError = true };

                        }

                        else if (result1.Contains("Error:"))
                        {
                            response.Result = "-1";
                            throw new WMSExceptionMessage() { WMSExceptionCode = null, WMSMessage = result1, ShowAsError = true };
                        }

                        else if (result1.Contains("Internal Server Error"))
                        {
                            response.Result = "-1";
                            throw new WMSExceptionMessage() { WMSExceptionCode = null, WMSMessage = "Error while establishing the connection", ShowAsError = true };
                        }                       
                        else if (result1.Contains("Success:"))

                        {
                            string saprefno = "";
                            string[] Sapno = result1.Split(":");

                            saprefno = Sapno[1];

                            obj.SapMaterialRefno = saprefno;
                            string result = ReserveStock(obj);
                            Inventory inventory = new Inventory()
                            {
                                Result = Convert.ToInt32(result)

                            };
                            inventories.Add(inventory);

                        }
                        else
                        {
                            throw new WMSExceptionMessage() { WMSExceptionCode = null, WMSMessage = "Error while establishing the connection", ShowAsError = true };
                        }
                    }
                    else
                    {

                        string result = ReserveStock(obj);
                        Inventory inventory = new Inventory()
                        {
                            Result = Convert.ToInt32(result)

                        };
                        inventories.Add(inventory);
                    }

                    }
                if (StatusID == 4)
                {
                    throw new Exception();

                }
                return inventories;
            }
            catch (WMSExceptionMessage excp)
            {
                throw excp;
            }
            catch (Exception excp)
            {
                ExceptionData oExcpData = new ExceptionData();
                oExcpData.AddInputs("ScannedItem", obj);

                ExceptionHandling.LogException(excp, _ClassCode + "001", oExcpData);
                throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = ErrorMessages.WMSExceptionMessage, ShowAsCriticalError = true };
            }
        }


        public async Task<List<Inventory>> UpsertMaterialTransferItem_HHT(Inventory obj)
        {
            Payload<string> response = new Payload<string>();
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                List<Inventory> inventories = new List<Inventory>();
                StringBuilder sCmdTransferPallettoLocation = new StringBuilder();

                sCmdTransferPallettoLocation.AppendLine("EXEC [dbo].[USP_MaterialTransfer_Block_HHT] ");
                sCmdTransferPallettoLocation.AppendLine("@CartonCode = " + DBLibrary.SQuote(obj.ContainerCode) + ",");
                sCmdTransferPallettoLocation.AppendLine("@TenantID = " + obj.TenantID + ",");
                sCmdTransferPallettoLocation.AppendLine("@WarehouseId = " + obj.WarehouseID + ",");
                sCmdTransferPallettoLocation.AppendLine("@LocationCode = " + DBLibrary.SQuote(obj.LocationCode) + ",");
                sCmdTransferPallettoLocation.AppendLine("@Mcode = " + DBLibrary.SQuote(obj.MaterialCode) + ",");
                sCmdTransferPallettoLocation.AppendLine("@BatchNo = " + DBLibrary.SQuote(obj.BatchNumber) + ",");
                sCmdTransferPallettoLocation.AppendLine("@UserID = " + obj.UserId + ",");
                sCmdTransferPallettoLocation.AppendLine("@FromSL = " + DBLibrary.SQuote(obj.StorageLocation) + ",");
                sCmdTransferPallettoLocation.AppendLine("@ToSL = " + DBLibrary.SQuote(obj.ToStorageLocation) + ",");
                sCmdTransferPallettoLocation.AppendLine("@Quantity = " + obj.Quantity + ",");
                sCmdTransferPallettoLocation.AppendLine("@TransferRequestID = " + obj.TransferRequestedID + ",");
                sCmdTransferPallettoLocation.AppendLine("@FromGrade = " + DBLibrary.SQuote(obj.Grade) + "");

                string Query = sCmdTransferPallettoLocation.ToString();
                string Message = DbUtility.GetSqlS(Query, this.ConnectionString);

                int count = DbUtility.GetSqlN("Exec [dbo].[SP_Get_TransferItem_Count] @TransferRequestID=" + obj.TransferRequestedID, this.ConnectionString);

                if (Message.Contains("Error"))
                {
                    Inventory inv = new Inventory
                    {
                        Result = -1,
                        ResponseMessage = Message,
                        Count = count
                    };
                    inventories.Add(inv);
                }
                if (Message.Contains("Success"))
                {
                    Inventory inv = new Inventory
                    {
                        Result = 1,
                        ResponseMessage = Message,
                        Count=count
                    };
                    inventories.Add(inv);
                }

                return inventories;
            }
            catch (WMSExceptionMessage excp)
            {
                throw excp;
            }
            catch (Exception excp)
            {
                ExceptionData oExcpData = new ExceptionData();
                oExcpData.AddInputs("ScannedItem", obj);

                ExceptionHandling.LogException(excp, _ClassCode + "001", oExcpData);
                throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = ErrorMessages.WMSExceptionMessage, ShowAsCriticalError = true };
            }
        }



        public async Task<List<Inventory>> UpsertTranferDetails_BG(Inventory obj)
        {
            Payload<string> response = new Payload<string>();
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                List<Inventory> inventories = new List<Inventory>();
                StringBuilder sCmdTransferPallettoLocation = new StringBuilder();

                sCmdTransferPallettoLocation.AppendLine("EXEC [dbo].[USP_API_INV_MaterialTransfer_BatchandGrade] ");
                sCmdTransferPallettoLocation.AppendLine("@CartonCode = " + DBLibrary.SQuote(obj.ContainerCode) + ",");
                sCmdTransferPallettoLocation.AppendLine("@TenantID = " + obj.TenantID + ",");
                sCmdTransferPallettoLocation.AppendLine("@WarehouseId = " + obj.WarehouseID + ",");
                sCmdTransferPallettoLocation.AppendLine("@LocationCode = " + DBLibrary.SQuote(obj.LocationCode) + ",");
                sCmdTransferPallettoLocation.AppendLine("@Mcode = " + DBLibrary.SQuote(obj.MaterialCode) + ",");
                sCmdTransferPallettoLocation.AppendLine("@BatchNo = " + DBLibrary.SQuote(obj.BatchNumber) + ",");
                sCmdTransferPallettoLocation.AppendLine("@UserID = " + obj.UserId + ",");
                sCmdTransferPallettoLocation.AppendLine("@FromSL = " + DBLibrary.SQuote(obj.StorageLocation) + ",");
                sCmdTransferPallettoLocation.AppendLine("@ToSL = " + DBLibrary.SQuote(obj.ToStorageLocation) + ",");
                sCmdTransferPallettoLocation.AppendLine("@Quantity = " + obj.Quantity + ",");
                sCmdTransferPallettoLocation.AppendLine("@ToBatchNo = " + DBLibrary.SQuote(obj.ToBatchNumber) + ",");
                sCmdTransferPallettoLocation.AppendLine("@FromGrade = " + DBLibrary.SQuote(obj.Grade) + ",");
                sCmdTransferPallettoLocation.AppendLine("@ToGrade = " + DBLibrary.SQuote(obj.ToGrade) + ",");
                sCmdTransferPallettoLocation.AppendLine("@ToMcode = " + DBLibrary.SQuote(obj.ToMaterialCode) + ",");
                sCmdTransferPallettoLocation.AppendLine("@TransferRequestID = " + obj.TransferRequestedID + "");

                string Query = sCmdTransferPallettoLocation.ToString();
                string  Message = DbUtility.GetSqlS(Query, this.ConnectionString);
                if(Message.Contains("Error"))
                {
                    Inventory inv = new Inventory
                    {
                        Result = -1,
                        ResponseMessage = Message
                    };
                    inventories.Add(inv);
                }
                if (Message.Contains("Success"))
                {
                    Inventory inv = new Inventory
                    {
                        Result = 1,
                        ResponseMessage = Message
                    };
                    inventories.Add(inv);
                }

                return inventories;
            }
            catch (WMSExceptionMessage excp)
            {
                throw excp;
            }
            catch (Exception excp)
            {
                ExceptionData oExcpData = new ExceptionData();
                oExcpData.AddInputs("ScannedItem", obj);

                ExceptionHandling.LogException(excp, _ClassCode + "001", oExcpData);
                throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = ErrorMessages.WMSExceptionMessage, ShowAsCriticalError = true };
            }
        }


        public async Task<List<Inventory>> UpsertPalletConsolidationTransfer(Inventory obj)
        {
            Payload<string> response = new Payload<string>();
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                List<Inventory> inventories = new List<Inventory>();
                StringBuilder sCmdTransferPallettoLocation = new StringBuilder();

                sCmdTransferPallettoLocation.AppendLine("EXEC [dbo].[USP_API_INV_MaterialTransfer_PalletCosolidation] ");
                sCmdTransferPallettoLocation.AppendLine("@CartonCode = " + DBLibrary.SQuote(obj.ContainerCode) + ",");
                sCmdTransferPallettoLocation.AppendLine("@TenantID = " + obj.TenantID + ",");
                sCmdTransferPallettoLocation.AppendLine("@WarehouseId = " + obj.WarehouseID + ",");
                sCmdTransferPallettoLocation.AppendLine("@LocationCode = " + DBLibrary.SQuote(obj.LocationCode) + ",");
                sCmdTransferPallettoLocation.AppendLine("@Mcode = " + DBLibrary.SQuote(obj.MaterialCode) + ",");
                sCmdTransferPallettoLocation.AppendLine("@BatchNo = " + DBLibrary.SQuote(obj.BatchNumber) + ",");
                sCmdTransferPallettoLocation.AppendLine("@UserID = " + obj.UserId + ",");
                sCmdTransferPallettoLocation.AppendLine("@FromSL = " + DBLibrary.SQuote(obj.StorageLocation) + ",");
                sCmdTransferPallettoLocation.AppendLine("@ToSL = " + DBLibrary.SQuote(obj.ToStorageLocation) + ",");
                sCmdTransferPallettoLocation.AppendLine("@Quantity = " + obj.Quantity + ",");
                sCmdTransferPallettoLocation.AppendLine("@ToBatchNo = " + DBLibrary.SQuote(obj.ToBatchNumber) + ",");
                sCmdTransferPallettoLocation.AppendLine("@FromGrade = " + DBLibrary.SQuote(obj.Grade) + ",");
                sCmdTransferPallettoLocation.AppendLine("@ToGrade = " + DBLibrary.SQuote(obj.ToGrade) + ",");
                sCmdTransferPallettoLocation.AppendLine("@ToMcode = " + DBLibrary.SQuote(obj.ToMaterialCode) + ",");
                sCmdTransferPallettoLocation.AppendLine("@TransferRequestID = " + obj.TransferRequestedID + ",");
                sCmdTransferPallettoLocation.AppendLine("@ToCartonCode = " + DBLibrary.SQuote(obj.ToContainerCode) + ",");
                sCmdTransferPallettoLocation.AppendLine("@ToLocationCode= " + DBLibrary.SQuote(obj.ToLocationCode) + ",");
                sCmdTransferPallettoLocation.AppendLine("@VLPDAssignedID= " +obj.VLPDAssignedID + "");

                string Query = sCmdTransferPallettoLocation.ToString();
                string Message = DbUtility.GetSqlS(Query, this.ConnectionString);
                if (Message.Contains("Error"))
                {
                    Inventory inv = new Inventory
                    {
                        Result = -1,
                        ResponseMessage = Message
                    };
                    inventories.Add(inv);
                }
                if (Message.Contains("Success"))
                {
                    Inventory inv = new Inventory
                    {
                        Result = 1,
                        ResponseMessage = Message
                    };
                    inventories.Add(inv);
                }

                return inventories;
            }
            catch (WMSExceptionMessage excp)
            {
                throw excp;
            }
            catch (Exception excp)
            {
                ExceptionData oExcpData = new ExceptionData();
                oExcpData.AddInputs("ScannedItem", obj);

                ExceptionHandling.LogException(excp, _ClassCode + "001", oExcpData);
                throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = ErrorMessages.WMSExceptionMessage, ShowAsCriticalError = true };
            }
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


        public  string ReserveStock(Inventory obj)
        {
            string uniqueID = DateTime.Now.ToString("ddhhmmss");
            Payload<string> response = new Payload<string>();

            string Query = "EXEC [dbo].[INTERNALSTOCK_RESERVE] @TransferRequestedID = " + obj.TransferRequestedID + " , @CreatedBy = " + obj.UserId + " , @SAPRefno = " + (obj.SapMaterialRefno != null ? "'" + obj.SapMaterialRefno + "'" : "''") + "";
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            int Result = DbUtility.GetSqlN(Query, ConnectionString);
            string result = Result.ToString();
            return result;
        }


        //        if (_dsResults != null)
        //        {
        //            if (_dsResults.Tables.Count > 0)
        //            {
        //                foreach (DataRow dr in _dsResults.Tables[0].Rows)
        //                {
        //                    Inventory inventory = new Inventory()
        //                    {
        //                        Result = ConversionUtility.ConvertToInt(dr["N"].ToString())

        //                    };
        //                    inventories.Add(inventory);
        //                }
        //            }
        //        }

        //        return inventories;
        //    }
        //    catch (WMSExceptionMessage excp)
        //    {
        //        throw excp;
        //    }
        //    catch (Exception excp)
        //    {
        //        ExceptionData oExcpData = new ExceptionData();
        //        oExcpData.AddInputs("ScannedItem", obj);

        //        ExceptionHandling.LogException(excp, _ClassCode + "001", oExcpData);
        //        throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = ErrorMessages.WMSExceptionMessage, ShowAsCriticalError = true };
        //    }
        //}

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<DataSet> GetStorageLocations()
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();

            string Query = "EXEC [dbo].[SP_INV_GET_STORAGELOCATION] ";
            DataSet _dsResults = DbUtility.GetDS(Query, this.ConnectionString);

            //UserDataTable usertable = new UserDataTable();
            //DataSet ds = DB.GetDS("select Code from StorageLocation where ID  in (3,4,5)", false);
            //DataTable dt = ds.Tables[0];
            //usertable.Table = dt;
            return _dsResults;
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<DataSet> GetTransferOrderNos(TransferBO Transferinfo)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();

            string Query = "EXEC [dbo].[Get_TransfernoList_HHT] @AccountId=" + Transferinfo.AccountId + ",@TenantID="+Transferinfo.TenantID+ ",@IsBlockScreen="+Transferinfo.IsBlockScreen;
            DataSet _dsResults = DbUtility.GetDS(Query, this.ConnectionString);
            
            return _dsResults;
        }


        public async Task<DataSet> GetTransferBlockReasons()
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();

            string Query = "EXEC [dbo].[Get_TransferBlockReasons_HHT]";
            DataSet _dsResults = DbUtility.GetDS(Query, this.ConnectionString);

            return _dsResults;
        }


        public async Task<DataSet> BatchTransfertoPick(TransferBO Transferinfo)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();

            string Query = "EXEC [dbo].[Get_PendingBatchItemstoPick_HHT] @AccountId=" + Transferinfo.AccountId + ",@UserID="+Transferinfo.UserId+",@TransferID="+Transferinfo.TransferOrderId+",@TransferRefNumber="+DBLibrary.SQuote(Transferinfo.TransferOrderNo);
            DataSet _dsResults = DbUtility.GetDS(Query, this.ConnectionString);

            return _dsResults;
        }




#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<DataSet> ItemMasterPrint(string TenanatId, string Labletype)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();

            //string ZPLQuery = "EXEC [dbo].[Usp_GetPrintLabels_hht] @TenantID=" + TenanatId + ", @Labletype=" + DBLibrary.SQuote(Labletype) + "";
            string ZPLQuery = "EXEC [dbo].[Usp_GetPrintLabels_hht] @TenantID=" + TenanatId + "";
            DataSet dataset = DbUtility.GetDS(ZPLQuery, this.ConnectionString);

            return dataset;     
        }
    }
}





  