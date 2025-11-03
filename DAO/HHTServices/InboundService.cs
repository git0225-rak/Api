using FWMSC21Core.Entities;
using Simpolo_Endpoint.DAO.HHTInterface;
using Simpolo_Endpoint.DAO.Services;
using Simpolo_Endpoint.DBUtil;
using Simpolo_Endpoint.DTO;
using Simpolo_Endpoint.Models;
using Simpolo_Endpoint.ModelsLibrary;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Simpolo_Endpoint.DAO.HHTServices
{
    public class InboundService : AppDBService, IInbound
    {
        private string _ClassCode = string.Empty;
        public InboundService(IOptions<AppSettings> appSettings) : base(appSettings)
        {

        }
#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<List<Inbound>> GetStoreRefNos(Inbound items)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            List<Inbound> lInbound = new List<Inbound>();

            try
            {
               
                string StoreRefNosQuery = "EXEC [dbo].[Get_StoreRefnoList_HHT] @UserID = " + items.CreatedByUserID + ",@AccountId = " + items.AccountID + ",@warehouseID = " + items.WarehouseID + ",@TenantID = " + items.TenantID + ",@IsStockAdust="+items.IsStockAdjust+"";
                var DS = DbUtility.GetDS(StoreRefNosQuery, this.ConnectionString);

                if (DS != null)
                {
                    if (DS.Tables.Count > 0)
                    {
                        foreach (DataRow _drPickList in DS.Tables[0].Rows)
                        {
                            Inbound _oInbound = new Inbound()
                            {
                                InboundID = ConversionUtility.ConvertToInt(_drPickList["InboundID"].ToString()),
                                StoreRefNo = _drPickList["Storerefno"].ToString(),
                                InvoiceQty = ConversionUtility.ConvertToDecimal(_drPickList["InvoiceQuantity"].ToString()),
                                ReceivedQty = ConversionUtility.ConvertToDecimal(_drPickList["ReceivedQty"].ToString()),
                                VehicleNumber = _drPickList["VehicleRegNo"].ToString(),
                                DockNumber = _drPickList["Dock"].ToString(),
                                DockID = ConversionUtility.ConvertToInt(_drPickList["DockID"].ToString()),
                                POTypeID = Convert.ToInt32(_drPickList["POTypeID"]),
                                IsProductionOrder=Convert.ToInt32(_drPickList["IsProductionOrder"]),
                            };

                            lInbound.Add(_oInbound);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lInbound;
        }



        public async Task<Payload<string>> GET_INB_GRNList(InboundTrackingModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>{
                {"StoreRefNo",obj.StoreRefNo},
                {"WarehouseIDs" ,obj.WarehouseIDs},
                {"TenantID",obj.TenantID},
                {"Tenant",obj.Tenant},
                {"AccountID",obj.AccountID},
                {"TenantID_New",obj.TenantID},
                {"LoginAccountId",obj.LoginAccountId},
                {"LoginUserId",obj.LoginUserId},
                {"LoginTanentId",obj.LoginTanentId}
            };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "sp_INB_GetGRNList", sqlParams).ConfigureAwait(false);
            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }
        public async Task<BO.Inbound> UpdateReceiveItem(BO.Inbound inbound)
        {
            GoodsInDTO goodsInDTO = new GoodsInDTO();
            goodsInDTO.Location = inbound.Dock;
            goodsInDTO.InboundID = inbound.InboundId;
            goodsInDTO.LineNumber = inbound.Lineno;
            goodsInDTO.MCode = inbound.MCode;
            goodsInDTO.LoggedinUserID = Convert.ToInt32(inbound.CreatedBy);
            goodsInDTO.CartonCode = inbound.CartonNo;
            goodsInDTO.StorageLocation = inbound.SLoc;
            goodsInDTO.MfgDate = inbound.MfgDate;
            goodsInDTO.ExpDate = inbound.ExpDate;
            goodsInDTO.MRP = inbound.MRP;
            goodsInDTO.Grade = inbound.Grade;
            goodsInDTO.SerialNumber = inbound.SerialNo;
            goodsInDTO.BatchNumber = inbound.BatchNo;
            goodsInDTO.SupplierInvoiceId = inbound.SupplierInvoiceID;
            goodsInDTO.PoHeaderID = inbound.POSOHeaderId;
            goodsInDTO.IsRequestFromPC = 0;
            goodsInDTO.HUNo = inbound.HUNo;
            goodsInDTO.HUsize = inbound.HUSize;
            goodsInDTO.Storerefno = inbound.Storerefno;
            goodsInDTO.IsStockAdjust = inbound.IsStockAdjust;
            goodsInDTO.IsStockAdd = inbound.IsStockAdd;
            goodsInDTO.ActualQty = inbound.ActualQty;
            goodsInDTO.AdjustQty = inbound.AdjustQty;
            goodsInDTO.IsPhysicalEmpty = inbound.IsPhysicalEmpty;


            if (inbound.ProjectNo != "")
            {

                inbound =  CheckProjectQty(inbound);

                if (inbound.ProjectStock > 0)
                {

                    inbound.Qty = inbound.NormalStock.ToString();


                    goodsInDTO.ProjectRefNo = inbound.ProjectNo;
                    goodsInDTO.DocumentQuantity = inbound.ProjectStock.ToString();
                    goodsInDTO.ConvertedQuantity = inbound.ProjectStock.ToString();

                    

                     string Result = await ReceiveItem(goodsInDTO);

                    if (Result == "1")
                    {
                        inbound.Result = "Success";
                        await CheckItems(inbound);
                        await GetReceivedQty(inbound);
                       // return inbound;
                    }
                    else
                    {
                        inbound.Result = Result;
                       // return inbound;
                    }

                }


            }    




           // inbound.Result = "0";  //Commented by Abhishek.G on 23-01-2024 for Projec
            inbound.ProjectNo = "";


          //  if (inbound.Result == "0" && inbound.NormalStock > 0)

                if (Convert.ToDecimal(inbound.Qty) > 0)
                {
              
                goodsInDTO.DocumentQuantity = inbound.Qty;
                goodsInDTO.ConvertedQuantity = inbound.Qty.ToString();
                goodsInDTO.ProjectRefNo = inbound.ProjectNo;


                string Result = await ReceiveItem(goodsInDTO);

                if (Result == "1")
                {
                    inbound.Result = "Success";
                    await CheckItems(inbound);
                    await GetReceivedQty(inbound);
                    return inbound;
                }
                else
                {
                    inbound.Result = Result;
                    return inbound;
                }
            }
            else if (inbound.Result == "1")
            {
                inbound.Result = "Quantity exceed";
            }
            return inbound;
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<string> ReceiveItem(GoodsInDTO objGoodsin)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            StringBuilder sCmdUpsertPOQuantity = new StringBuilder();
            StringBuilder AutomaticGRN = new StringBuilder();

            if (objGoodsin.IsStockAdjust==1)
            {
                sCmdUpsertPOQuantity.AppendLine("EXEC [dbo].[sp_INV_AdjustItem] ");
                sCmdUpsertPOQuantity.AppendLine("@TransactionID = " + objGoodsin.InboundID + ",");
                sCmdUpsertPOQuantity.AppendLine("@MCode = " + DBLibrary.SQuote(objGoodsin.MCode) + ",");
                sCmdUpsertPOQuantity.AppendLine("@LineNumber = " + DBLibrary.SQuote(objGoodsin.LineNumber) + ",");
                sCmdUpsertPOQuantity.AppendLine("@POHeaderID = " + DBLibrary.SQuote(objGoodsin.PoHeaderID) + ",");
                sCmdUpsertPOQuantity.AppendLine("@DocQty = " + DBLibrary.SQuote(objGoodsin.DocumentQuantity) + ",");
                sCmdUpsertPOQuantity.AppendLine("@Quantity = " + DBLibrary.SQuote(objGoodsin.ConvertedQuantity) + ",");
                sCmdUpsertPOQuantity.AppendLine("@BatchNo = " + DBLibrary.SQuote(objGoodsin.BatchNumber) + ",");
                sCmdUpsertPOQuantity.AppendLine("@SerialNo = " + DBLibrary.SQuote(objGoodsin.SerialNumber) + ", ");
                sCmdUpsertPOQuantity.AppendLine("@MfgDate = " + DBLibrary.SQuote(objGoodsin.MfgDate) + ",");
                sCmdUpsertPOQuantity.AppendLine("@ExpDate = " + DBLibrary.SQuote(objGoodsin.ExpDate) + ",");
                sCmdUpsertPOQuantity.AppendLine("@ProjectRefNo = " + DBLibrary.SQuote(objGoodsin.ProjectRefNo) + ",");
                sCmdUpsertPOQuantity.AppendLine("@MRP = " + DBLibrary.SQuote(objGoodsin.MRP) + ",");
                sCmdUpsertPOQuantity.AppendLine("@StorageLocation = " + DBLibrary.SQuote(objGoodsin.StorageLocation) + ",");
                sCmdUpsertPOQuantity.AppendLine("@Loction = " + DBLibrary.SQuote(objGoodsin.Location) + ",");
                sCmdUpsertPOQuantity.AppendLine("@CartonCode = " + DBLibrary.SQuote(objGoodsin.CartonCode) + ",");
                sCmdUpsertPOQuantity.AppendLine("@GoodsMovementTypeID = " + 1 + ",");
                sCmdUpsertPOQuantity.AppendLine("@Createdby = " + objGoodsin.LoggedinUserID + ",");
                sCmdUpsertPOQuantity.AppendLine("@SupplierInvoiceID = " + DBLibrary.SQuote(objGoodsin.SupplierInvoiceId) + ",");
                sCmdUpsertPOQuantity.AppendLine("@HUNo = 1,");
                sCmdUpsertPOQuantity.AppendLine("@IsRequestFromPC = " + objGoodsin.IsRequestFromPC + ",");
                sCmdUpsertPOQuantity.AppendLine("@Grade = " + objGoodsin.Grade + ", ");
                sCmdUpsertPOQuantity.AppendLine("@IsStockAdd = " + objGoodsin.IsStockAdd + ", ");
                sCmdUpsertPOQuantity.AppendLine("@ActualQty = " + objGoodsin.ActualQty + ", ");
                sCmdUpsertPOQuantity.AppendLine("@AdjustQty = " + objGoodsin.AdjustQty + " ");

            }
            else
            {
                sCmdUpsertPOQuantity.AppendLine("EXEC [dbo].[sp_INV_ReceiveItem] ");
                sCmdUpsertPOQuantity.AppendLine("@TransactionID = " + objGoodsin.InboundID + ",");
                sCmdUpsertPOQuantity.AppendLine("@MCode = " + DBLibrary.SQuote(objGoodsin.MCode) + ",");
                sCmdUpsertPOQuantity.AppendLine("@LineNumber = " + DBLibrary.SQuote(objGoodsin.LineNumber) + ",");
                sCmdUpsertPOQuantity.AppendLine("@POHeaderID = " + DBLibrary.SQuote(objGoodsin.PoHeaderID) + ",");
                sCmdUpsertPOQuantity.AppendLine("@DocQty = " + DBLibrary.SQuote(objGoodsin.DocumentQuantity) + ",");
                sCmdUpsertPOQuantity.AppendLine("@Quantity = " + DBLibrary.SQuote(objGoodsin.ConvertedQuantity) + ",");
                sCmdUpsertPOQuantity.AppendLine("@BatchNo = " + DBLibrary.SQuote(objGoodsin.BatchNumber) + ",");
                sCmdUpsertPOQuantity.AppendLine("@SerialNo = " + DBLibrary.SQuote(objGoodsin.SerialNumber) + ", ");
                sCmdUpsertPOQuantity.AppendLine("@MfgDate = " + DBLibrary.SQuote(objGoodsin.MfgDate) + ",");
                sCmdUpsertPOQuantity.AppendLine("@ExpDate = " + DBLibrary.SQuote(objGoodsin.ExpDate) + ",");
                sCmdUpsertPOQuantity.AppendLine("@ProjectRefNo = " + DBLibrary.SQuote(objGoodsin.ProjectRefNo) + ",");
                sCmdUpsertPOQuantity.AppendLine("@MRP = " + DBLibrary.SQuote(objGoodsin.MRP) + ",");
                sCmdUpsertPOQuantity.AppendLine("@StorageLocation = " + DBLibrary.SQuote(objGoodsin.StorageLocation) + ",");
                sCmdUpsertPOQuantity.AppendLine("@Loction = " + DBLibrary.SQuote(objGoodsin.Location) + ",");
                sCmdUpsertPOQuantity.AppendLine("@CartonCode = " + DBLibrary.SQuote(objGoodsin.CartonCode) + ",");
                sCmdUpsertPOQuantity.AppendLine("@GoodsMovementTypeID = " + 1 + ",");
                sCmdUpsertPOQuantity.AppendLine("@Createdby = " + objGoodsin.LoggedinUserID + ",");
                sCmdUpsertPOQuantity.AppendLine("@SupplierInvoiceID = " + DBLibrary.SQuote(objGoodsin.SupplierInvoiceId) + ",");
                sCmdUpsertPOQuantity.AppendLine("@HUNo = 1,");
                sCmdUpsertPOQuantity.AppendLine("@IsRequestFromPC = " + objGoodsin.IsRequestFromPC + ",");
                sCmdUpsertPOQuantity.AppendLine("@Grade = " + objGoodsin.Grade + ",");
                sCmdUpsertPOQuantity.AppendLine("@IsPhysicalEmpty = " + objGoodsin.IsPhysicalEmpty + "");
            }

       

            string Query = sCmdUpsertPOQuantity.ToString();
            DataSet dsResult = DbUtility.GetDS(Query, this.ConnectionString);

            if (Convert.ToInt32(objGoodsin.HUNo) > 1)
            {
                if (Convert.ToInt32(dsResult.Tables[0].Rows[0][0]) == -20)
                {
                    return "Invalid carton scanned";
                }
                else if (Convert.ToInt32(dsResult.Tables[0].Rows[0][0]) == 0)
                {
                    return "Their is no pending SKU";
                }
                else if (Convert.ToInt32(dsResult.Tables[0].Rows[0][0]) == 1)
                {

                    string SPType = "Exec SP_GetInwardType @InboundID  = " + objGoodsin.InboundID;
                    int valueType = DbUtility.GetSqlN(SPType, ConnectionString);
                    if (valueType == 2)
                    {
                        await Task.Delay(TimeSpan.FromSeconds(3));
                        AutomaticGRN.AppendLine("Exec SP_Upsert_AutoGRNReceiving @TransactionID = " + objGoodsin.InboundID + ",");
                        AutomaticGRN.AppendLine("@MCode = " + DBLibrary.SQuote(objGoodsin.MCode) + ",");
                        AutomaticGRN.AppendLine("@LineNumber = " + DBLibrary.SQuote(objGoodsin.LineNumber) + ",");
                        AutomaticGRN.AppendLine("@POHeaderID = " + DBLibrary.SQuote(objGoodsin.PoHeaderID) + ",");
                        AutomaticGRN.AppendLine("@BatchNo = " + DBLibrary.SQuote(objGoodsin.BatchNumber) + ",");
                        AutomaticGRN.AppendLine("@StorageLocation = " + DBLibrary.SQuote(objGoodsin.StorageLocation) + ",");
                        AutomaticGRN.AppendLine("@Loction = " + DBLibrary.SQuote(objGoodsin.Location) + ",");
                        AutomaticGRN.AppendLine("@CartonCode = " + DBLibrary.SQuote(objGoodsin.CartonCode) + ",");
                        AutomaticGRN.AppendLine("@Createdby = " + objGoodsin.LoggedinUserID + ",");
                        AutomaticGRN.AppendLine("@SupplierInvoiceID = " + DBLibrary.SQuote(objGoodsin.SupplierInvoiceId) + ",");
                        AutomaticGRN.AppendLine("@Grade = " + objGoodsin.Grade + "");
                        string QueryGRN = AutomaticGRN.ToString();
                        DataSet _dsResultsGRN = DbUtility.GetDS(QueryGRN, this.ConnectionString);

                        string ReceivedQtyCheck = "Exec SP_GetCrossDockingReceiving @TransactionID=" + objGoodsin.InboundID;
                        int checkrecived= DbUtility.GetSqlN(ReceivedQtyCheck,this.ConnectionString);  
                        
                        if(checkrecived==1)
                        {
                            await Task.Delay(TimeSpan.FromSeconds(2));
                            string CrossDockInw = "Exec SP_AutoRelease_Cross_Inward @InboundID=" + objGoodsin.InboundID + ",@UserID=" +objGoodsin.LoggedinUserID;
                            string checkrecivedCross = DbUtility.GetSqlS(CrossDockInw, this.ConnectionString);
                        }
                    }

                    return "1";
                }
                else
                {
                    throw new Exception("Unexpected error, please contact support");
                }
            }
            else
            {
                if (dsResult != null && dsResult.Tables[0] != null && dsResult.Tables[0].Rows.Count == 1 && dsResult.Tables[0].Columns.Count == 2)
                {
                    return dsResult.Tables[0].Rows[0][1].ToString();
                }
                else if (dsResult != null && dsResult.Tables[0] != null && dsResult.Tables[0].Rows.Count == 1 && dsResult.Tables[0].Rows[0][0].ToString() == "1")
                {

                    string SPType = "Exec SP_GetInwardType @InboundID  = " + objGoodsin.InboundID;
                    int valueType = DbUtility.GetSqlN(SPType, ConnectionString);
                    if (valueType == 2)
                    {
                        await Task.Delay(TimeSpan.FromSeconds(3));
                        AutomaticGRN.AppendLine("Exec SP_Upsert_AutoGRNReceiving @TransactionID = " + objGoodsin.InboundID + ",");
                        AutomaticGRN.AppendLine("@MCode = " + DBLibrary.SQuote(objGoodsin.MCode) + ",");
                        AutomaticGRN.AppendLine("@LineNumber = " + DBLibrary.SQuote(objGoodsin.LineNumber) + ",");
                        AutomaticGRN.AppendLine("@POHeaderID = " + DBLibrary.SQuote(objGoodsin.PoHeaderID) + ",");
                        AutomaticGRN.AppendLine("@BatchNo = " + DBLibrary.SQuote(objGoodsin.BatchNumber) + ",");
                        AutomaticGRN.AppendLine("@StorageLocation = " + DBLibrary.SQuote(objGoodsin.StorageLocation) + ",");
                        AutomaticGRN.AppendLine("@Loction = " + DBLibrary.SQuote(objGoodsin.Location) + ",");
                        AutomaticGRN.AppendLine("@CartonCode = " + DBLibrary.SQuote(objGoodsin.CartonCode) + ",");
                        AutomaticGRN.AppendLine("@Createdby = " + objGoodsin.LoggedinUserID + ",");
                        AutomaticGRN.AppendLine("@SupplierInvoiceID = " + DBLibrary.SQuote(objGoodsin.SupplierInvoiceId) + ",");
                        AutomaticGRN.AppendLine("@Grade = " + objGoodsin.Grade + "");
                        string QueryGRN = AutomaticGRN.ToString();
                        DataSet _dsResultsGRN = DbUtility.GetDS(QueryGRN, this.ConnectionString);

                        string ReceivedQtyCheck = "Exec SP_GetCrossDockingReceiving @TransactionID=" + objGoodsin.InboundID;
                        int checkrecived = DbUtility.GetSqlN(ReceivedQtyCheck, this.ConnectionString);

                        if (checkrecived == 1)
                        {
                            await Task.Delay(TimeSpan.FromSeconds(2));
                            string CrossDockInw = "Exec SP_AutoRelease_Cross_Inward @InboundID=" + objGoodsin.InboundID + ",@UserID=" + objGoodsin.LoggedinUserID;
                            string checkrecivedCross = DbUtility.GetSqlS(CrossDockInw, this.ConnectionString);
                        }
                    }



                    return "1";
                }
                else if (dsResult != null && dsResult.Tables[0] != null && dsResult.Tables[0].Rows.Count == 1 && dsResult.Tables[0].Rows[0][0].ToString() != "1")
                {
                    return dsResult.Tables[0].Rows[0][0].ToString();
                }
                else
                {
                    throw new Exception("Unexpected error, please contact support");
                }
            }
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task CheckItems(BO.Inbound inbound)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();

            string SP = "Exec SP_ChangeGateEntryStatus @ShipmentId = " + inbound.InboundId + " , @IsForInbound = 1";
            int value = DbUtility.GetSqlN(SP, ConnectionString);
        }

        public async Task<BO.Inbound> GetReceivedQty(BO.Inbound inbound)
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            if (CheckValidItem(inbound))
            {
                StringBuilder sbSqlString = new StringBuilder();
                sbSqlString.AppendLine("EXEC [dbo].[Get_ReceivedQty_HHT] ");
                sbSqlString.AppendLine("@INBOUNDID = " + inbound.InboundId + ",");
                sbSqlString.AppendLine("@Mcode = " + DBLibrary.SQuote(inbound.MCode) + ",");
                sbSqlString.AppendLine("@SupplierInvoiceDetailsId = " + DBLibrary.SQuote(inbound.SupplierInvoiceDetailsId) + ",");
                sbSqlString.AppendLine("@HUNO = " + DBLibrary.SQuote(inbound.HUNo) + ",");
                sbSqlString.AppendLine("@LineNo = " + DBLibrary.SQuote(inbound.Lineno) + ", ");
                sbSqlString.AppendLine("@Cartoncode = " + "'"+inbound.CartonNo+"'" + ", ");
                sbSqlString.AppendLine("@Grade = " + "'" + inbound.Grade + "'" + "");

                DataSet ds = DbUtility.GetDS(sbSqlString.ToString(), this.ConnectionString);
                if (ds.Tables[0].Rows.Count != 0)
                {
                    inbound.ReceivedQty = Convert.ToDecimal(ds.Tables[0].Rows[0]["ReceivedQty"]);
                    inbound.ActualQty = Convert.ToDecimal(ds.Tables[0].Rows[0]["ActualQty"]);
                    inbound.SLoc = ds.Tables[0].Rows[0]["SLoc"].ToString();
                    inbound.SLocId = ds.Tables[0].Rows[0]["SLocId"].ToString();
                    inbound.AdjustQty= Convert.ToDecimal(ds.Tables[0].Rows[0]["AdjustQty"].ToString());
                    inbound.IsStockAdd= ds.Tables[0].Rows[0]["IsStockAdd"].ToString();
                }
                inbound = await GetPendingLineItemQty(inbound);
                return inbound;
            }
            else
            {
                inbound.Result = "invalid";
                return inbound;
            }
        }
        public bool CheckValidItem(BO.Inbound inbound)
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();

            Func<string, string> formatDate = (inputDate) =>
            {
                if (string.IsNullOrWhiteSpace(inputDate))
                {
                    return ""; 
                }

                DateTime date;
                if (DateTime.TryParseExact(inputDate, "dd-MMM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                {
                    return "'" + date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) + "'";
                }
                else if (DateTime.TryParseExact(inputDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                {
                    return "'" + date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) + "'";
                }
                throw new ArgumentException("Invalid date format: " + inputDate);
            };
          
                //string ExpDate = formatDate(inbound.ExpDate);
                string ExpDate = "";
                string MfgDate = formatDate(inbound.MfgDate);

                string SP = "Exec INB_ValidSKU_GoodsIn @INBOUNDID = " + inbound.InboundId + " ,@Mcode = " + DBLibrary.SQuote(inbound.MCode) + " ,@LineNo = " + DBLibrary.SQuote(inbound.Lineno) + " ,@ExpDate = " + DBLibrary.SQuote(ExpDate) + " ,@SerialNo = " + DBLibrary.SQuote(inbound.SerialNo) + " ,@BatchNo = " + DBLibrary.SQuote(inbound.BatchNo) + " ,@MfgDate = " + DBLibrary.SQuote(MfgDate) + " ,@ProjectRefNo = " + DBLibrary.SQuote(inbound.ProjectNo) + " ,@MRP = " + DBLibrary.SQuote(inbound.MRP) + ",@Grade = " + DBLibrary.SQuote(inbound.Grade) + "";
                int count = DbUtility.GetSqlN(SP, ConnectionString);

                return (count != 0) ? true : false;
            

        }
#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<BO.Inbound> GetPendingLineItemQty(BO.Inbound inbound)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();

            StringBuilder sbSqlString = new StringBuilder();
            sbSqlString.AppendLine("EXEC [dbo].[Get_InboundMaterialPendingQty_HHT] ");
            sbSqlString.AppendLine("@Storerefno = " + inbound.InboundId + ",");
            sbSqlString.AppendLine("@Mcode = " + DBLibrary.SQuote(inbound.MCode) + ",");
            sbSqlString.AppendLine("@LineNo = " + DBLibrary.SQuote(inbound.Lineno) + "");

            DataSet ds = DbUtility.GetDS(sbSqlString.ToString(), this.ConnectionString);

            if (ds.Tables[0].Rows.Count != 0)
            {
                inbound.ItemPendingQty = Convert.ToDecimal(ds.Tables[0].Rows[0]["LineItemInvoiceQty"]);
            }
            return inbound;
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<DataSet> GetSkipReasonList(string type)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();

            StringBuilder sbSqlString = new StringBuilder();
            sbSqlString.AppendLine("EXEC [dbo].[Get_SkipReasonList_HHT] ");
            sbSqlString.AppendLine("@TypeId = " + Convert.ToInt32(type) + "");

            DataSet ds = DbUtility.GetDS(sbSqlString.ToString(), this.ConnectionString);

            return ds;
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<string> CheckContainer(string CartonNo, string InboundID)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            string resultvalue;
            string sql = "select CartonID AS N from INV_Carton where CartonCode=" + DBLibrary.SQuote(CartonNo) + " and IsActive=1 and WareHouseID=(select WarehouseID from OBD_RefWarehouse_Details where InBoundID=" + InboundID + "";
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
        public async Task<BO.Inbound> CheckDock(BO.Inbound inbound)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();

            string sql = "EXEC [dbo].INB_CheckDock_HHT @Location =" + DBLibrary.SQuote(inbound.Dock) + ",@InboundID=" + inbound.InboundId + "";
            inbound.Result = DbUtility.GetSqlS(sql, ConnectionString);
            return inbound;
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<DataSet> GetStorageLocations()
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();

            string Query = "EXEC [dbo].[SP_INV_GET_STORAGELOCATION] ";
            DataSet DS = DbUtility.GetDS(Query, this.ConnectionString);

            return DS;
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<BO.PutAway> GetItemTOPutAway(BO.PutAway PutAway)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            if (PutAway.TransferRequestId != 0)
            {
                PutAway.InboundId = "0";

                string query = " EXEC [dbo].[GEN_PUTAWAY_ITEMS_ALLOCATED_LIST_INBWISE_HHT] @INBOUNDID=" + Convert.ToInt32(PutAway.InboundId) + ",@TransferRequestID=" + PutAway.TransferRequestId;
                try
                {
                    DataSet ds = DbUtility.GetDS(query, this.ConnectionString);

                    if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count != 0)
                    {
                        PutAway.SuggestedPutawayID = Convert.ToInt32(ds.Tables[0].Rows[0]["SuggestedPutawayID"].ToString());
                        PutAway.MaterialMasterId = Convert.ToInt32(ds.Tables[0].Rows[0]["MaterialMasterID"].ToString());
                        PutAway.MCode = ds.Tables[0].Rows[0]["MCode"].ToString();
                        PutAway.MDescription = ds.Tables[0].Rows[0]["MDescription"].ToString();
                        PutAway.CartonCode = ds.Tables[0].Rows[0]["CartonCode"].ToString();
                        PutAway.CartonID = Convert.ToInt32(ds.Tables[0].Rows[0]["CartonID"].ToString());
                        PutAway.Location = ds.Tables[0].Rows[0]["Location"].ToString();
                        PutAway.LocationID = Convert.ToInt32(ds.Tables[0].Rows[0]["LocationID"].ToString());
                        PutAway.MfgDate = ds.Tables[0].Rows[0]["MfgDate"].ToString();
                        PutAway.ExpDate = ds.Tables[0].Rows[0]["ExpDate"].ToString();
                        PutAway.SerialNo = ds.Tables[0].Rows[0]["SerialNo"].ToString();
                        PutAway.BatchNo = ds.Tables[0].Rows[0]["BatchNo"].ToString();
                        PutAway.ProjectRefNo = ds.Tables[0].Rows[0]["ProjectRefNo"].ToString();
                        PutAway.AssignedQuantity = ds.Tables[0].Rows[0]["InvoiceQty"].ToString();
                        PutAway.SuggestedQty = Convert.ToDecimal(ds.Tables[0].Rows[0]["SuggestedQty"].ToString());
                        PutAway.SuggestedReceivedQty = Convert.ToDecimal(ds.Tables[0].Rows[0]["SuggestedReceivedQty"].ToString());
                        PutAway.SuggestedRemainingQty = Convert.ToDecimal(ds.Tables[0].Rows[0]["SuggestedRemainingQty"].ToString());
                        PutAway.TransferRequestDetailsId = Convert.ToInt32(ds.Tables[0].Rows[0]["TransferRequestDetailsID"].ToString());
                        PutAway.PickedLocationID = Convert.ToInt32(ds.Tables[0].Rows[0]["PickedLocationID"].ToString());
                        PutAway.GMDRemainingQty = Convert.ToDecimal(ds.Tables[0].Rows[0]["PickedQty"].ToString());
                        PutAway.Dock = ds.Tables[0].Rows[0]["DOCK"].ToString();
                        PutAway.StorageLocation = ds.Tables[0].Rows[0]["StorageCode"].ToString();
                        PutAway.PutAwayQty = "1";
                    }
                    else
                    {
                        PutAway.PutAwayQty = "0";
                    }
                    return PutAway;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                string query = " EXEC [dbo].[GEN_PUTAWAY_ITEMS_ALLOCATED_LIST_INBWISE_HHT_NEW] @INBOUNDID=" + Convert.ToInt32(PutAway.InboundId);
                try
                {
                    DataSet ds = DbUtility.GetDS(query, this.ConnectionString);

                    if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count != 0)
                    {
                        PutAway.SuggestedPutawayID = Convert.ToInt32(ds.Tables[0].Rows[0]["SuggestedPutawayID"].ToString());
                        PutAway.MaterialMasterId = Convert.ToInt32(ds.Tables[0].Rows[0]["MaterialMasterID"].ToString());
                        PutAway.MCode = ds.Tables[0].Rows[0]["MCode"].ToString();
                        PutAway.CartonCode = "0";
                        PutAway.Location = ds.Tables[0].Rows[0]["Location"].ToString();
                        PutAway.LocationID = Convert.ToInt32(ds.Tables[0].Rows[0]["LocationID"].ToString());
                        PutAway.MfgDate = ds.Tables[0].Rows[0]["MfgDate"].ToString();
                        PutAway.ExpDate = ds.Tables[0].Rows[0]["ExpDate"].ToString();
                        PutAway.SerialNo = ds.Tables[0].Rows[0]["SerialNo"].ToString();
                        PutAway.BatchNo = ds.Tables[0].Rows[0]["BatchNo"].ToString();
                        PutAway.ProjectRefNo = ds.Tables[0].Rows[0]["ProjectRefNo"].ToString();
                        PutAway.MRP = ds.Tables[0].Rows[0]["MRP"].ToString();
                        PutAway.StorageLocation = ds.Tables[0].Rows[0]["StorageCode"].ToString();
                        PutAway.SuggestedQty = Convert.ToDecimal(ds.Tables[0].Rows[0]["SuggestedQty"].ToString());
                        PutAway.SuggestedRemainingQty = Convert.ToDecimal(ds.Tables[0].Rows[0]["SuggestedRemainingQty"].ToString());
                        PutAway.SuggestedReceivedQty = Convert.ToDecimal(ds.Tables[0].Rows[0]["SuggPutQTY"].ToString());
                        PutAway.ReceivedQuantity = Convert.ToDecimal(ds.Tables[0].Rows[0]["RemainingQTY"].ToString());
                        PutAway.GMDRemainingQty = Convert.ToDecimal(ds.Tables[0].Rows[0]["GMDQTY"].ToString());
                        PutAway.Dock = ds.Tables[0].Rows[0]["Dock"].ToString();
                        PutAway.PutAwayQty = "1";
                        PutAway.Result = "3";
                    }
                    else
                    {
                        // PutAway.PutAwayQty = "0";
                        string sql = "EXEC INB_Verify_Putaway @InboundID=" + PutAway.InboundId;
                        PutAway.Result = DbUtility.GetSqlS(sql, ConnectionString);
                    }
                    return PutAway;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<BO.PutAway> SkipItem(BO.PutAway putAway)
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();

            string drlStatement = "EXEC [dbo].[UpsertSkipItem_HHT]  @SuggestedId=" + Convert.ToInt32(putAway.SuggestedPutawayID) + ",@MCode=" + DBLibrary.SQuote(putAway.MCode) + ",@MfgDate=" + (putAway.MfgDate != "" ? DBLibrary.SQuote(putAway.MfgDate) : "''") + ",@ExpDate=" + (putAway.ExpDate != "" ? DBLibrary.SQuote(putAway.ExpDate) : "''") + ",@SerialNo=" + (putAway.SerialNo != "" ? DBLibrary.SQuote(putAway.SerialNo) : "''") + ",@BatchNo = " + (putAway.BatchNo != "" ? DBLibrary.SQuote(putAway.BatchNo) : "''") + ",@ProjectRefNo = " + (putAway.ProjectRefNo != "" ? DBLibrary.SQuote(putAway.ProjectRefNo) : "''") + ",@Qty = " + (putAway.SkipQty) + ", @CreatedBy=" + Convert.ToInt32(putAway.UserId) + ",@StorageLocation=" + DBLibrary.SQuote(putAway.StorageLocation) + ",@CartonCode=" + DBLibrary.SQuote(putAway.CartonCode) + ",@Location=" + DBLibrary.SQuote(putAway.Location) + ",@Reason=" + DBLibrary.SQuote(putAway.Skipreason) + ",@InboundId=" + putAway.InboundId + ",@TypeId=" + 1;
            int result = DbUtility.GetSqlN(drlStatement, ConnectionString);

            putAway.Result = result.ToString();

            if (putAway.Flag == 1)
            {
                await RegenerateSuggestedItem(putAway);
            }
            return putAway;
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<BO.PutAway> RegenerateSuggestedItem(BO.PutAway PutAway)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();

            string drlStatement = "EXEC [dbo].[USP_TRN_GeneratePutawaySuggestions] @InboundID=" + Convert.ToInt32(PutAway.InboundId) + ",@TransferRequestID=" + PutAway.TransferRequestId + ",@SuggestionID=" + PutAway.SuggestedPutawayID + ",@SuggestionQtyFulfilled=" + Convert.ToDecimal(PutAway.SuggestedReceivedQty) + ",@ReasonID=" + 1;
            DbUtility.GetSqlN(drlStatement, ConnectionString);

            return PutAway;
        }

        public async Task<BO.PutAway> UpsertPutAwayItem(BO.PutAway PutAway)
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                decimal qty = Convert.ToDecimal(PutAway.PutAwayQty);
                if (qty > PutAway.TotalQuantity)
                {
                    PutAway.Result = "Qty. should be less than putaway qty.";
                    return PutAway;
                }

                string valid = await CheckValidPutAwayItem(PutAway);
                if (valid == "1")
                {
                    int remainingqty = await CheckPutAwayItemRemainQty(PutAway);
                    if (remainingqty > 0)
                    {
                        PutAway.Result = " putaway could not perform, there is " + remainingqty + " Qty. has been received";
                        return PutAway;
                    }
                    else if (remainingqty == -999)
                    {
                        PutAway.Result = " Putaway could not perform";
                        return PutAway;
                    }
                    if (PutAway.TransferRequestId != 0)
                    {
                        await UpdateInternalTransferReceiveItem(PutAway);
                        PutAway.Result = "";
                        await GetItemTOPutAway(PutAway);
                        return PutAway;
                    }
                    else
                    {
                        string drlStatement = "EXEC [dbo].[UpsertPutAway_HHT]  @INBOUNDID=" + Convert.ToInt32(PutAway.InboundId) + ",@SuggestedPutAwayID=" + PutAway.SuggestedPutawayID + ",@MCode=" + DBLibrary.SQuote(PutAway.MCode) + ",@Quantity=" + Convert.ToDecimal(PutAway.PutAwayQty) + ",@SuggesstedLocation=" + DBLibrary.SQuote(PutAway.Location) + ",@CartonCode=" + DBLibrary.SQuote(PutAway.CartonCode) + ",@CreatedBy=" + Convert.ToInt32(PutAway.UserId) + ",@StorageLocation=" + DBLibrary.SQuote(PutAway.StorageLocation);
                        drlStatement += ",@ScannedLocation=" + DBLibrary.SQuote(PutAway.ScnnedLocation);
                        int result = DbUtility.GetSqlN(drlStatement, ConnectionString);
                        switch (result)
                        {
                            case -9999:
                                PutAway.Result = "Invalid carton scanned";

                                return PutAway;
                            case -9998:
                                PutAway.Result = "This carton available in other location";

                                return PutAway;
                            case -9997:
                                PutAway.Result = "Putaway quantity exceeded";
                                return PutAway;
                            case 1:
                                PutAway.Result = "";
                                await GetItemTOPutAway(PutAway);
                                return PutAway;
                        }
                    }
                }
                else
                {
                    PutAway.Result = "Scan valid SKU";
                }
                return PutAway;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<string> CheckValidPutAwayItem(BO.PutAway PutAway)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();

            decimal RecieveQty = 0;
            string res, sql;
            sql = "EXEC [dbo].[Get_ReceivedQty_HHT] @INBOUNDID=" + PutAway.InboundId + ",@Mcode=" + DBLibrary.SQuote(PutAway.MCode) + ", @LineNo = " + DBLibrary.SQuote(PutAway.Lineno) + ", @MfgDate = " + (PutAway.MfgDate != "" ? DBLibrary.SQuote(PutAway.MfgDate) : "''") + ", @ExpDate = " + (PutAway.ExpDate != "" ? DBLibrary.SQuote(PutAway.ExpDate) : "''") + ", @SerialNo = " + (PutAway.SerialNo != "" ? DBLibrary.SQuote(PutAway.SerialNo) : "''") + ", @BatchNo = " + (PutAway.BatchNo != "" ? DBLibrary.SQuote(PutAway.BatchNo) : "''") + ", @ProjectRefNo = " + (PutAway.ProjectRefNo != "" ? DBLibrary.SQuote(PutAway.ProjectRefNo) : "''") + ", @MRP = " + (PutAway.MRP != "" ? DBLibrary.SQuote(PutAway.MRP) : "''");
            DataSet ds = DbUtility.GetDS(sql, this.ConnectionString);

            if (ds.Tables[0].Rows.Count != 0)
            {
                RecieveQty = Convert.ToDecimal(ds.Tables[0].Rows[0]["ReceivedQty"]);
            }
            res = (RecieveQty > 0) ? "1" : "0";

            return res;
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<int> CheckPutAwayItemRemainQty(BO.PutAway PutAway)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();

            int RemainingQty = 0;
            string sql = "EXEC [dbo].[INB_CheckPutawayQty] @INBOUNDID=" + PutAway.InboundId + ",@Mcode=" + DBLibrary.SQuote(PutAway.MCode) + ", @LineNo = " + DBLibrary.SQuote(PutAway.Lineno) + ", @MfgDate = " + (PutAway.MfgDate != "" ? DBLibrary.SQuote(PutAway.MfgDate) : "''") + ", @ExpDate = " + (PutAway.ExpDate != "" ? DBLibrary.SQuote(PutAway.ExpDate) : "''") + ", @SerialNo = " + (PutAway.SerialNo != "" ? DBLibrary.SQuote(PutAway.SerialNo) : "''") + ", @BatchNo = " + (PutAway.BatchNo != "" ? DBLibrary.SQuote(PutAway.BatchNo) : "''") + ", @ProjectRefNo = " + (PutAway.ProjectRefNo != "" ? DBLibrary.SQuote(PutAway.ProjectRefNo) : "''") + ", @MRP = " + (PutAway.MRP != "" ? DBLibrary.SQuote(PutAway.MRP) : "''");
            RemainingQty = DbUtility.GetSqlN(sql, ConnectionString);

            if (RemainingQty != 0)
            {
                return Convert.ToInt32(PutAway.PutAwayQty) - RemainingQty;
            }
            else
            {
                return -999;
            }
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<BO.PutAway> UpdateInternalTransferReceiveItem(BO.PutAway PutAway)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();

            string drlStatement = "EXEC [dbo].[INTERNALSTOCK_TRANSFER_ITEM]   @TransferRequestID=" + Convert.ToInt32(PutAway.TransferRequestId) + ",@TransferRequestDetailsID=" + PutAway.TransferRequestDetailsId + ",@FromLocationID=" + PutAway.PickedLocationID + ",@BatchNo=" + DBLibrary.SQuote(PutAway.BatchNo) + ",@CreatedBy=" + PutAway.UserId + ",@Carton=" + DBLibrary.SQuote(PutAway.CartonCode) + ",@MCODE=" + DBLibrary.SQuote(PutAway.MCode) + ",@TLoc=" + DBLibrary.SQuote(PutAway.Location) + ",@Quantity=" + Convert.ToDecimal(PutAway.PutAwayQty) + ",@FullFilledQty=" + PutAway.TotalQuantity;

            int result = DbUtility.GetSqlN(drlStatement, ConnectionString);
            return PutAway;
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<BO.PutAway> CheckPutAwayItemQty(BO.PutAway PutAway)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();

            string drlStatement = "EXEC [dbo].[Check_PutAwayItem] @INBOUNDID=" + Convert.ToInt32(PutAway.InboundId) + ",@MaterialMasterId=" + PutAway.MaterialMasterId + ",@PartialPutAwayQty=" + Convert.ToDecimal(PutAway.PutAwayQty);
            int result = DbUtility.GetSqlN(drlStatement, ConnectionString);

            switch (result)
            {
                case -1:
                    PutAway.Result = "PutAway Qty is greater than the Received Qty.";
                    return PutAway;
                case -2:
                    PutAway.Result = "This Item not yet received";
                    return PutAway;
                case 1:
                    PutAway.Result = "";
                    return PutAway;
            }

            return PutAway;
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<List<Suggestion>> GeneratePutawaySuggestion(SearchCriteria oCriteria)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                List<Suggestion> lSuggestions = new List<Suggestion>();
                StringBuilder sCmdUpsertPOQuantity = new StringBuilder();

                sCmdUpsertPOQuantity.AppendLine("EXEC [dbo].[USP_TRN_GeneratePutawaySuggestions_Pallet] ");
                sCmdUpsertPOQuantity.AppendLine("@InboundID = " + oCriteria.InboundID + ",");
                sCmdUpsertPOQuantity.AppendLine("@StoreRefNo = " + DBLibrary.SQuote(oCriteria.StoreRefNo) + ",");
                sCmdUpsertPOQuantity.AppendLine("@TransferRequestID = " + oCriteria.TransferRequestID + ",");
                sCmdUpsertPOQuantity.AppendLine("@UpdatedBy = " + oCriteria.UserID + ",");
                sCmdUpsertPOQuantity.AppendLine("@SuggestionID = " + oCriteria.SuggestionID + ",");
                sCmdUpsertPOQuantity.AppendLine("@SuggestionQtyFulfilled = " + oCriteria.SuggestionFullfilledQuantity + ",");
                sCmdUpsertPOQuantity.AppendLine("@ReasonID = " + oCriteria.ReasonID + ",");
                sCmdUpsertPOQuantity.AppendLine("@PalletID = " + oCriteria.ContainerID + ",");
                sCmdUpsertPOQuantity.AppendLine("@PalletCode = " + DBLibrary.SQuote(oCriteria.ContainerCode) + "");

                string Query = sCmdUpsertPOQuantity.ToString();
                DataSet _dsSuggestions = DbUtility.GetDS(Query, this.ConnectionString);

                if (_dsSuggestions != null)
                {
                    if (_dsSuggestions.Tables.Count > 0)
                    {
                        foreach (DataRow _drSuggestion in _dsSuggestions.Tables[0].Rows)
                        {
                            Suggestion _oSuggestion = new Suggestion()
                            {
                                LocationCode = _drSuggestion["DisplayLocationCode"].ToString(),
                                LocationID = ConversionUtility.ConvertToInt(_drSuggestion["SuggestedLocationID"].ToString()),
                                MaterialCode = _drSuggestion["MCode"].ToString(),
                                MaterialMasterID = ConversionUtility.ConvertToInt(_drSuggestion["MaterialMasterID"].ToString()),
                                Quantity = ConversionUtility.ConvertToDecimal(_drSuggestion["SuggestedQty"].ToString()),
                                SuggestedPutawayID = ConversionUtility.ConvertToInt(_drSuggestion["SuggestedPutawayID"].ToString()),
                                WareHouseID = _drSuggestion["WarehouseID"].ToString(),
                                TenantID = ConversionUtility.ConvertToInt(_drSuggestion["TenantID"].ToString())
                            };

                            lSuggestions.Add(_oSuggestion);
                        }
                    }
                }

                return lSuggestions;
            }
            catch (WMSExceptionMessage excp)
            {
                throw excp;
            }
            catch (Exception excp)
            {
                ExceptionData oExcpData = new ExceptionData();
                oExcpData.AddInputs("oCriteria", oCriteria);

                ExceptionHandling.LogException(excp, _ClassCode + "008", oExcpData);
                throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = ErrorMessages.WMSExceptionMessage, ShowAsCriticalError = true };
            }
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<string> ChekContainerLocation(string cartoncode, string WarehouseID)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();

            if (WarehouseID == "")
            {
                WarehouseID = "0";
            }

            string Query = "EXEC [dbo].[SP_CHECK_CONTAINER_LOCATION_MAPPING]  @CONTAINER = " + DBLibrary.SQuote(cartoncode) + ",@WarehouseID = " + WarehouseID + "";
            string Location = DbUtility.GetSqlS(Query, ConnectionString).ToString();

            return Location;
        }

        public async Task<GRNDetails> GetMiscXMLData(string batchNo, string projectRefNo, string Qty, string remks, string part, string um, string conv, string qadAccount, string QADLocation, string uniqueID = "")
        {
            try
            {
                string xmlResult = GenerateMiscRcptXML(batchNo, projectRefNo, Qty, remks, part, um, conv, qadAccount, QADLocation, uniqueID).Result.Result;
                string soapString = xmlResult.ToString();
                //string uri = "http://UK1SAWN02272:8080/qxi/services/QdocWebService";
                string uri = this.QADSOAP;
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("SOAPAction", "urn:schemas-qad-com:xml-services:common");
                    //Request.Content.Headers.ContentType = new MediaTypeHeaderValue("Text/xml");
                    // client.DefaultRequestHeaders.Add("Content-Type", "Text/xml");
                    var content = new StringContent(soapString, Encoding.UTF8, "text/xml");
                    using (var response = await client.PostAsync(uri, content))
                    {
                        GRNDetails gRNDetails = new GRNDetails();
                        var soapResponse = await response.Content.ReadAsStringAsync();
                        return this.ParseSoapResponse(soapResponse, "receiveInventoryResponse");
                        //return gRNDetails;
                    }
                }
            }
            catch (Exception ex)
            {
                GRNDetails gRNDetails = new GRNDetails();
                gRNDetails.result = "Error";
                gRNDetails.errorcode = "Unexpected error from QAD..!" + ex.Message;
                return gRNDetails;
            }
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<Payload<string>> GenerateMiscRcptXML(string batchNo, string projectRefNo, string Qty, string remks, string part, string um, string conv, string qadAccount, string QADLocation, string uniqueID)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var env = new MiscReceiptQADXML.Envelope
                {
                    Header = new MiscReceiptQADXML.Header()
                    {
                        Action = "",
                        To = this.wsaTo,
                        MessageID = this.wsaMessageId,
                        ReferenceParameters = new MiscReceiptQADXML.ReferenceParameters() { suppressResponseDetail = false },
                        ReplyTo = new MiscReceiptQADXML.ReplyTo() { Address = "urn:services-qad-com:" }

                    },
                    Body = new MiscReceiptQADXML.Body()
                    {
                        ReceiveInventory = new MiscReceiptQADXML.receiveInventory()
                        {
                            DsSessionContext = new MiscReceiptQADXML.DsSessionContext()
                            {
                                lstTTContext = new List<MiscReceiptQADXML.TTContext>
                                {
                                    new MiscReceiptQADXML.TTContext()
                                    {
                                        PropertyQualifier= "QAD",
                                             PropertyName = "domain",
                                             PropertyValue="kenya"
                                    },
                                    new MiscReceiptQADXML.TTContext()
                                    {
                                        PropertyQualifier= "QAD",
                                             PropertyName = "version",
                                             PropertyValue="eB_2"
                                    },
                                    new MiscReceiptQADXML.TTContext()
                                    {
                                           PropertyQualifier= "QAD",
                                             PropertyName = "action",
                                             PropertyValue=""
                                    }
                                }
                            },
                            DsInventoryReceipt = new MiscReceiptQADXML.DsInventoryReceipt()
                            {
                                InventoryReceipt = new MiscReceiptQADXML.InventoryReceipt()
                                {
                                    Operation = "A",
                                    PtPart = part,
                                    LotserialQty = Qty,
                                    Um = um,
                                    Conv = conv,
                                    Site = "10",
                                    Location = QADLocation,
                                    Lotserial = batchNo,
                                    Lotref = projectRefNo,
                                    MultiEntry = "",
                                    Ordernbr = "",
                                    Orderline = "",
                                    SoJob = "",
                                    Addr = "",
                                    Rmks = uniqueID,
                                    EffDate = DateTime.Now.ToString("yyyy-MM-dd"),
                                    CrAcct = qadAccount,
                                    CrSub = "",
                                    CrCc = "020",
                                    CrProj = "",
                                    Absid = "",
                                    Shipdate = "",
                                    InvMov = "",
                                    Yn = "true",
                                    Yn1 = "true",
                                    ReceiptDetail = new MiscReceiptQADXML.ReceiptDetail()
                                    {
                                        operation = "A",
                                        Site = "10",
                                        Location = QADLocation,
                                        Lotserial = batchNo,
                                        Lotref = projectRefNo,
                                        LotserialQty = Qty,
                                        SerialsYn = "false"
                                    }
                                }
                            }
                        }
                    }
                };
                var serializer = new XmlSerializer(typeof(MiscReceiptQADXML.Envelope));
                var settings = new XmlWriterSettings
                {
                    Encoding = Encoding.UTF8,
                    Indent = true,
                    OmitXmlDeclaration = true,
                };
                var builder = new StringBuilder();
                using (var writer = XmlWriter.Create(builder, settings))
                {
                    serializer.Serialize(writer, env, env.xmlns);
                }

                response.Result = builder.ToString();
            }
            catch (SqlException Sqlex)
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
                        //gRNDetails.errorcode = xn["ns3:tt_msg_data"] == null ? "" : xn["ns3:tt_msg_data"].InnerText + " " + xn["ns3:tt_msg_desc"] == null ? "" : xn["ns3:tt_msg_desc"].InnerText + " " + xn["ns3:tt_msg_context"] == null ? "" : xn["ns3:tt_msg_context"].InnerText;
                        //gRNDetails.errorcode = xn.InnerText;
                        gRNDetails.errorcode = "QAD Error" + xn["ns3:dsExceptions"]?["ns3:temp_err_msg"]?["ns3:tt_msg_desc"] == null ? xn.InnerText : xn["ns3:dsExceptions"]?["ns3:temp_err_msg"]?["ns3:tt_msg_desc"]?.InnerText;
                        gRNDetails.result = gRNDetails.errorcode?.Length > 0 && gRNDetails.errorcode.Contains("WARNING: Shipper not printed.") ? "success" : gRNDetails.result;
                    }
                }
            }
            return gRNDetails;
        }

        public async Task<GRNDetails> GetGRNXMLData(string InboundId, string InvoiceNumber, string PONumber, int InboundType, string Remarks)
        {
#pragma warning disable CS0219 // The variable 'UserID' is assigned but its value is never used
            int UserID = 0;
#pragma warning restore CS0219 // The variable 'UserID' is assigned but its value is never used
            string gRNNumber = "";
            int gRNHeaderID = 0;
            string soapString = "";
#pragma warning disable CS0219 // The variable 'OPerationTypeID' is assigned but its value is never used
            int OPerationTypeID = 0;
#pragma warning restore CS0219 // The variable 'OPerationTypeID' is assigned but its value is never used
            GRNDetails details = new GRNDetails();

            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                string xmlResult = "";
                string responseType = "";
                DataSet ds = new DataSet();

                string GRNQuery = "EXEC [dbo].[GetGRNXMLDATA] @InboundID = " + InboundId + ",@InvoiceNumber = " + DBLibrary.SQuote(InvoiceNumber) + ",@PONumber = " + DBLibrary.SQuote(PONumber) + ",@Remarks = " + DBLibrary.SQuote(Remarks) + "";
                ds = DbUtility.GetDS(GRNQuery, this.ConnectionString);

                if (ds.Tables[0].Rows.Count == 0)
                {
                    await DeleteGRNHeader(gRNHeaderID);
                    return new GRNDetails() { errorcode = "No data found", result = "error" };
                }

                gRNNumber = Convert.ToString(ds.Tables[0].Rows[0]["GRNNumber"]);
                gRNHeaderID = Convert.ToInt32(ds.Tables[0].Rows[0]["GRNUpdateID"]);
                //UserID = Convert.ToInt32(ds.Tables[0].Rows[0]["CreatedBy"]);

                if (Convert.ToInt32(ds.Tables[0].Rows[0]["POTypeID"]) == 6)
                {
                    xmlResult = XMLforQADSOShipment(ds).Result.Result;
                    details = await UpdateShipmentforCustomerReturns(xmlResult);
                    if (details.result == "Error")
                    {
                        await DeleteGRNHeader(gRNHeaderID);
                        return details;
                    }
                    else
                    {
                        details = await GetSOPGIXMLDATA(0, gRNHeaderID, details.result);

                        details.result = details.errorcode?.Length > 0 && details.errorcode.Contains("WARNING: Shipper not printed.") ? "success" : details.result;
                        details.GRNHeaderID = gRNHeaderID;
                        if (details.result != "success")
                        {
                            await DeleteGRNHeader(gRNHeaderID);
                        }
                        return details;
                    }
                }
                else
                {
                    if (InboundType == 14)
                    {
                        responseType = "receiveWorkOrderResponse";
                        xmlResult = GenerateQADWORKORDERGRNXML(ds).Result.Result;
                    }
                    else
                    {
                        responseType = "receivePurchaseOrderResponse";
                        //xmlResult = GenerateQADWORKORDERGRNXML(ds);
                        xmlResult = GenerateQADGRNXML(ds).Result.Result;
                    }

                    soapString = xmlResult.ToString();

                    //_oInboundDAL.GetInsertQADResponseLog(gRNHeaderID, soapString, "GRN", 0, null, 1, UserID);


                    //string uri = "http://UK1SAWN02272:8080/qxi/services/QdocWebService";
                    string uri = this.QADSOAP;
                    using (var client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Add("SOAPAction", "urn:schemas-qad-com:xml-services:common");
                        //Request.Content.Headers.ContentType = new MediaTypeHeaderValue("Text/xml");
                        // client.DefaultRequestHeaders.Add("Content-Type", "Text/xml");
                        var content = new StringContent(soapString, Encoding.UTF8, "text/xml");
                        using (var response = await client.PostAsync(uri, content))
                        {
                            GRNDetails gRNDetails = new GRNDetails();
                            var soapResponse = await response.Content.ReadAsStringAsync();
                            gRNDetails = this.ParseGRNSoapResponse(soapResponse, gRNHeaderID, responseType);
                            var IsSuccess = gRNDetails.errorcode == null || gRNDetails.errorcode == "" ? 1 : 0;
                            //var IsSuccess = gRNDetails.Result == "success" ? 1 : 0;
                            var error = gRNDetails.errorcode;
                            //string.IsNullOrEmpty(gRNDetails.ErrorCode) ? 1 : 0;
                            //_oInboundDAL.GetInsertQADResponseLog(gRNHeaderID, soapString, "GRN", IsSuccess, error, 1, UserID);
                            return gRNDetails;
                        }
                    }
                }
            }
#pragma warning disable CS0168 // The variable 'tc' is declared but never used
            catch (TaskCanceledException tc)
#pragma warning restore CS0168 // The variable 'tc' is declared but never used
            {
                GRNDetails gRNDetails = new GRNDetails();
                gRNDetails.result = "Error";
                gRNDetails.GRNHeaderID = 0;
                gRNDetails.errorcode = "Unexpected error from QAD..Task!";
                await DeleteGRNHeader(gRNHeaderID);
                return gRNDetails;
            }
#pragma warning disable CS0168 // The variable 'he' is declared but never used
            catch (HttpRequestException he)
#pragma warning restore CS0168 // The variable 'he' is declared but never used
            {
                GRNDetails gRNDetails = new GRNDetails();

                gRNDetails.result = "Error";
                gRNDetails.GRNHeaderID = 0;
                gRNDetails.errorcode = "Unexpected error from QAD..Task!";
                await DeleteGRNHeader(gRNHeaderID);                ///gRNDetails.Result = "success";
                //gRNDetails.GRNHeaderID = gRNHeaderID;
                // gRNDetails.ErrorCode = "Unexpected error from QAD..Task!" + he.Message;
                //DeleteGRNHeader(gRNHeaderID);//no data from qad so..delete the generated gRNHeaderID

                //_oInboundDAL.GetInsertQADResponseLog(gRNHeaderID, soapString, "GRN", 0, "Unexpected error from QAD..Task!", 1, UserID);

                return gRNDetails;
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                GRNDetails gRNDetails = new GRNDetails();
                gRNDetails.result = "Error";
                gRNDetails.GRNHeaderID = 0;
                gRNDetails.errorcode = "Unexpected error from QAD..!";
                await DeleteGRNHeader(gRNHeaderID);
                return gRNDetails;
            }
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<Payload<string>> DeleteGRNHeader(int gRNHeaderID)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            Payload<string> response = new Payload<string>();
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                string Query = "Exec SP_INB_DeleteGRNHeader @GRNUpdateID = " + gRNHeaderID + "";
                response.Result = DbUtility.GetSqlS(Query, ConnectionString);
            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }

        public GRNDetails ParseGRNSoapResponse(string response, int gRNHeaderID, string responseType)
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
                    gRNDetails.GRNHeaderID = gRNHeaderID;
                    gRNDetails.result = Status;
                    if (Status != "success")
                    {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed. Consider applying the 'await' operator to the result of the call.
                        DeleteGRNHeader(gRNHeaderID);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed. Consider applying the 'await' operator to the result of the call.
                        //gRNDetails.errorcode = xn["ns3:tt_msg_data"] == null ? "" : xn["ns3:tt_msg_data"].InnerText + " " + xn["ns3:tt_msg_desc"] == null ? "" : xn["ns3:tt_msg_desc"].InnerText + " " + xn["ns3:tt_msg_context"] == null ? "" : xn["ns3:tt_msg_context"].InnerText;
                        //gRNDetails.errorcode = xn.InnerText;
                        gRNDetails.errorcode = "QAD Error:" + xn["ns3:dsExceptions"]["ns3:temp_err_msg"]["ns3:tt_msg_desc"] == null ? xn.InnerText : xn["ns3:dsExceptions"]["ns3:temp_err_msg"]["ns3:tt_msg_desc"].InnerText;
                    }
                }
            }
            return gRNDetails;
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<Payload<string>> XMLforQADSOShipment(DataSet ds)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            Payload<string> response = new Payload<string>();
            try
            {
                List<QADShipmentGenerate.ItemDetail> lstItem = new List<QADShipmentGenerate.ItemDetail>();
                QADShipmentGenerate.ItemDetail ItemDetailObj = new QADShipmentGenerate.ItemDetail();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    ItemDetailObj = new QADShipmentGenerate.ItemDetail()
                    {
                        operation = "A",
                        scxPart = Convert.ToString(row["scxPart"]),
                        scxPo = "",
                        scxCustref = "",
                        scxModelyr = "",
                        scxOrder = Convert.ToString(row["scxOrder"]),
                        scxLine = Convert.ToString(row["scxLine"]),
                        srQty = Convert.ToString(row["srQty"]),
                        transUm = Convert.ToString(row["transUm"]),
                        transConv = Convert.ToString(row["transConv"]),
                        srSite = Convert.ToString(row["absShipfrom"]),
                        srLoc = Convert.ToString(row["srLoc"]),
                        srLotser = Convert.ToString(row["srLotser"]),
                        srRef = Convert.ToString(row["srRef"]),
                        multiEntry = "false",
                        vCmmts = "false",
                        ioprmt = "true",
                        IssueDetail = new QADShipmentGenerate.IssueDetail()
                        {
                            operation = "A",
                            site = Convert.ToString(row["absShipfrom"]),
                            location = Convert.ToString(row["srLoc"]),
                            lotserial = Convert.ToString(row["srLotser"]),
                            lotref = Convert.ToString(row["srRef"]),
                            lotserialQty = Convert.ToString(row["srQty"])
                        },
                        AdditionalLineChargeDetail = new QADShipmentGenerate.AdditionalLineChargeDetail()
                        {
                            operation = "",
                            abslLcLine = "",
                            abslTrlCode = "",
                            abslLcAmt = "",
                            abslChargeType = "",
                            abslRef = ""
                        },
                        ItemDetailTransComment = new QADShipmentGenerate.ItemDetailTransComment()
                        {
                            operation = "",
                            cmtSeq = "",
                            cdRef = "",
                            cdType = "",
                            cdLang = "",
                            cmtCmmt = "",
                            prtOnQuote = "true",
                            prtOnSo = "true",
                            prtOnInvoice = "true",
                            prtOnPacklist = "true",
                            prtOnPo = "true",
                            prtOnRct = "true",
                            prtOnRtv = "true",
                            prtOnShpr = "true",
                            prtOnBol = "true",
                            prtOnCus = "true",
                            prtOnProb = "true",
                            prtOnSchedule = "true",
                            prtOnIsrqst = "true",
                            prtOnDo = "true",
                            prtOnIntern = "true"
                        }
                    };
                    lstItem.Add(ItemDetailObj);
                }
                var env = new QADShipmentGenerate.Envelope
                {
                    Header = new QADShipmentGenerate.Header()
                    {
                        Action = "",
                        To = this.wsaTo,
                        MessageID = this.wsaMessageId,
                        ReferenceParameters = new QADShipmentGenerate.ReferenceParameters() { suppressResponseDetail = false },
                        ReplyTo = new QADShipmentGenerate.ReplyTo() { Address = "urn:services-qad-com:" }

                    },
                    Body = new QADShipmentGenerate.Body()
                    {
                        MaintainSalesOrderShipper = new QADShipmentGenerate.MaintainSalesOrderShipper()
                        {
                            DsSessionContext = new QADShipmentGenerate.DsSessionContext()
                            {
                                lstTTContext = new List<QADShipmentGenerate.TTContext>
                                {
                                    new QADShipmentGenerate.TTContext()
                                    {
                                        PropertyQualifier= "QAD",
                                             PropertyName = "domain",
                                             PropertyValue="kenya"
                                    },
                                    new QADShipmentGenerate.TTContext()
                                    {
                                        PropertyQualifier= "QAD",
                                             PropertyName = "version",
                                             PropertyValue="eB21_2"
                                    },
                                    new QADShipmentGenerate.TTContext()
                                    {
                                           PropertyQualifier= "QAD",
                                             PropertyName = "action",
                                             PropertyValue=""
                                    }
                                }
                            },
                            DsSalesOrderShipper = new QADShipmentGenerate.DsSalesOrderShipper()
                            {
                                SalesOrderShipper = new QADShipmentGenerate.SalesOrderShipper()
                                {
                                    operation = "A",
                                    absShipfrom = Convert.ToString(ds.Tables[0].Rows[0]["absShipfrom"]),
                                    absId = "",
                                    absShipto = Convert.ToString(ds.Tables[0].Rows[0]["absShipto"]),
                                    vInvmov = "",
                                    vCont = "true",
                                    vCont1 = "false",
                                    vCarrier = "",
                                    vMulti = "false",
                                    absShipvia = "",
                                    vFob = "",
                                    vTransMode = "",
                                    vCarrRef = "",
                                    vVehRef = "",
                                    vFormat = "",
                                    vConsShip = "",
                                    absLang = "US",
                                    vCmmts = "false",
                                    vStatus = "",
                                    vCmmts1 = "false",
                                    vShipCmmts = "false",
                                    vPackCmmts = "false",
                                    vFeatures = "false",
                                    vPrintSodet = "true",
                                    lSoUm = "true",
                                    compAddr = "",
                                    lPrintLotserials = "true",
                                    dev = "",
                                    vOk = "true",
                                    SequenceDetail = new QADShipmentGenerate.SequenceDetail()
                                    {
                                        operation = "",
                                        absdFldSeq = "",
                                        absdFldName = "",
                                        absdFldValue = ""
                                    },
                                    CarrierDetail = new QADShipmentGenerate.CarrierDetail()
                                    {
                                        operation = "",
                                        seq = "",
                                        abscCarrier = ""
                                    },
                                    SalesOrderShipperTransComment = new QADShipmentGenerate.SalesOrderShipperTransComment()
                                    {
                                        operation = "",
                                        cmtSeq = "",
                                        cdRef = "text",
                                        cdType = "text",
                                        cdLang = "text",
                                        cdSeq = "999",
                                        cmtCmmt = "text",
                                        prtOnQuote = "true",
                                        prtOnSo = "true",
                                        prtOnInvoice = "true",
                                        prtOnPacklist = "true",
                                        prtOnPo = "true",
                                        prtOnRct = "true",
                                        prtOnRtv = "true",
                                        prtOnShpr = "true",
                                        prtOnBol = "true",
                                        prtOnCus = "true",
                                        prtOnProb = "true",
                                        prtOnSchedule = "true",
                                        prtOnIsrqst = "true",
                                        prtOnDo = "true",
                                        prtOnIntern = "true"
                                    },
                                    ItemDetail = lstItem
                                }
                            }
                        }
                    }
                };
                var serializer = new XmlSerializer(typeof(QADShipmentGenerate.Envelope));
                var settings = new XmlWriterSettings
                {
                    Encoding = Encoding.UTF8,
                    Indent = true,
                    OmitXmlDeclaration = true,
                };
                var builder = new StringBuilder();
                using (var writer = XmlWriter.Create(builder, settings))
                {
                    serializer.Serialize(writer, env, env.xmlns);
                }
                response.Result = builder.ToString();
            }
            catch (SqlException Sqlex)
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
        public async Task<Payload<string>> GenerateQADGRNXML(DataSet DS)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            Payload<string> response = new Payload<string>();
            try
            {
                List<GRNQADXML.LineDetail> listLineDetail = new List<GRNQADXML.LineDetail>();
                GRNQADXML.LineDetail lineDetail = new GRNQADXML.LineDetail();
                foreach (DataRow row in DS.Tables[0].Rows)
                {
                    lineDetail = new GRNQADXML.LineDetail()
                    {
                        operation = "A",
                        line = row["line"].ToString(),
                        lotserialQty = row["Qty"].ToString(),
                        packingQty = row["Qty"].ToString(),
                        cancelBo = "false",
                        receiptUm = row["receiptUm"].ToString(),
                        wolot = "",
                        woop = "0",
                        site = "10",
                        location = "Holding",
                        lotserial = row["lotserial"].ToString(),
                        lotref = row["lotref"].ToString(),
                        podQad04 = "",
                        multiEntry = "false",
                        chgAttr = "true",
                        cmmtYn = "true",
                        chgAssay = "999.99",
                        assayActv = "false",
                        chgGrade = "",
                        gradeActv = "false",
                        chgExpire = row["chgExpire"].ToString(),
                        expireActv = "true",
                        chgStatus = "",
                        statusActv = "false",
                        resetattr = "false",
                        updtBlnkt = "true",

                        receiptDetail = new GRNQADXML.ReceiptDetail()
                        {
                            operation = "A",
                            location = "Holding",
                            lotserial = row["lotserial"].ToString(),
                            lotref = row["lotref"].ToString(),
                            vendlot = row["vendlot"].ToString(),
                            lotserialQty = row["Qty"].ToString(),
                            serialsYn = "true"
                        },
                        lineDetailTransComment = new GRNQADXML.LineDetailTransComment()
                        {
                            operation = "A",
                            cmtSeq = "1",
                            cdRef = "RCPT",
                            cdType = "BK",
                            //serialsYn = "US",
                            cdLang = "US",
                            cdSeq = "1",
                            cmtCmmt = "text01102021",
                            prtOnQuote = "true",
                            prtOnSo = "true",
                            prtOnInvoice = "true",
                            prtOnPacklist = "true",
                            prtOnPo = "true",
                            prtOnRct = "true",
                            prtOnRtv = "true",
                            prtOnShpr = "true",
                            prtOnBol = "true",
                            prtOnCus = "true",
                            prtOnProb = "true",
                            prtOnSchedule = "true",
                            prtOnIsrqst = "true",
                            prtOnDo = "true",
                            prtOnIntern = "true"
                        }


                    };
                    listLineDetail.Add(lineDetail);

                }
                var env = new GRNQADXML.Envelope
                {
                    Header = new GRNQADXML.Header()
                    {
                        Action = "",
                        To = this.wsaTo,
                        MessageID = this.wsaMessageId,
                        ReferenceParameters = new GRNQADXML.ReferenceParameters() { suppressResponseDetail = false },
                        ReplyTo = new GRNQADXML.ReplyTo() { Address = "urn:services-qad-com:" }

                    },
                    Body = new GRNQADXML.Body()
                    {
                        receivePurchaseOrder = new GRNQADXML.ReceivePurchaseOrder()
                        {
                            dsSessionContext = new GRNQADXML.DsSessionContext()
                            {
                                lstTTContext = new List<GRNQADXML.TTContext>
                                {
                                    new GRNQADXML.TTContext()
                                    {
                                        PropertyQualifier= "QAD",
                                             PropertyName = "domain",
                                             PropertyValue="kenya"
                                    },
                                    new GRNQADXML.TTContext()
                                    {
                                        PropertyQualifier= "QAD",
                                             PropertyName = "version",
                                             PropertyValue="eB_2"
                                    },
                                    new GRNQADXML.TTContext()
                                    {
                                           PropertyQualifier= "QAD",
                                             PropertyName = "action",
                                             PropertyValue=""
                                    }
                                }
                            },
                            dsPurchaseOrderReceive = new GRNQADXML.DsPurchaseOrderReceive()
                            {
                                purchaseOrderReceive = new GRNQADXML.PurchaseOrderReceive()
                                {
                                    operation = "A",
                                    ordernum = Convert.ToString(DS.Tables[0].Rows[0]["ordernum"]),
                                    psNbr = Convert.ToString(DS.Tables[0].Rows[0]["psNbr"]),
                                    receivernbr = "",
                                    effDate = Convert.ToString(DS.Tables[0].Rows[0]["effDate"]),
                                    move = "false",
                                    fillAll = "false",
                                    cmmtYn = "true",
                                    shipDate = Convert.ToString(DS.Tables[0].Rows[0]["shipDate"]),
                                    absid = "",
                                    invMov = "",
                                    vRate = "1",
                                    vRate2 = Convert.ToDecimal(DS.Tables[0].Rows[0]["ExchangeRate"]),
                                    receiptDate = Convert.ToString(DS.Tables[0].Rows[0]["shipDate"]),
                                    yn = "false",
                                    yn1 = "true",
                                    taxEdited = "true",
                                    lFlag = "true",
                                    recalc = "true",
                                    purchaseOrderReceiveTransComment = new GRNQADXML.PurchaseOrderReceiveTransComment()
                                    {
                                        operation = "A",
                                        cmtSeq = "1",
                                        cdRef = "00018130",
                                        cdType = "BK",
                                        cdLang = "",
                                        cdSeq = "",
                                        cmtCmmt = "text",
                                        prtOnQuote = "true",
                                        prtOnSo = "true",
                                        prtOnInvoice = "true",
                                        prtOnPacklist = "true",
                                        prtOnPo = "true",
                                        prtOnRct = "true",
                                        prtOnRtv = "true",
                                        prtOnShpr = "true",
                                        prtOnBol = "true",
                                        prtOnCus = "true",
                                        prtOnProb = "true",
                                        prtOnSchedule = "true",
                                        prtOnIsrqst = "true",
                                        prtOnDo = "true",
                                        prtOnIntern = "true"
                                    },


                                    lineDetail = listLineDetail,


                                    taxDetailRecord = new GRNQADXML.TaxDetailRecord()
                                    {
                                        operation = "",
                                        taxLine = "",
                                        tx2dTotamt = "",
                                        tx2dTottax = "",
                                        tx2dCurTaxAmt = "",
                                        tx2dCurRecovAmt = "",
                                        taxTrl = ""
                                    }

                                }
                            }
                        }
                    }
                };
                var serializer = new XmlSerializer(typeof(GRNQADXML.Envelope));
                var settings = new XmlWriterSettings
                {
                    Encoding = Encoding.UTF8,
                    Indent = true,
                    OmitXmlDeclaration = true,
                };
                var builder = new StringBuilder();
                using (var writer = XmlWriter.Create(builder, settings))
                {
                    serializer.Serialize(writer, env, env.xmlns);
                }
                response.Result = builder.ToString();
            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }

        public async Task<GRNDetails> GetSOPGIXMLDATA(int OutboundID = 0, int GRNHeaderID = 0, string ShipNo = "")
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                //DataSet ds = new DataSet();            
                string SOPGIQuery = "EXEC [dbo].[USP_SOPGIXMLDATA] @OutboundId = " + OutboundID + ",@GRNHeaderID = " + GRNHeaderID + ",@ShipNo = " + ShipNo + "";
                var ds = DbUtility.GetDS(SOPGIQuery, this.ConnectionString);

                if (ds.Tables[0].Rows.Count == 0)
                {
                    return new GRNDetails() { errorcode = "No data found", result = "error" };

                }
                string xmlResult = GenerateQADSOPGIXML(ds).Result.Result;
                string soapString = xmlResult.ToString();

                //string uri = "http://UK1SAWN02272:8080/qxi/services/QdocWebService";
                string uri = this.QADSOAP;
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("SOAPAction", "urn:schemas-qad-com:xml-services:common");
                    //Request.Content.Headers.ContentType = new MediaTypeHeaderValue("Text/xml");
                    // client.DefaultRequestHeaders.Add("Content-Type", "Text/xml");
                    var content = new StringContent(soapString, Encoding.UTF8, "text/xml");
                    using (var response = await client.PostAsync(uri, content))
                    {
                        GRNDetails gRNDetails = new GRNDetails();
                        var soapResponse = await response.Content.ReadAsStringAsync();
                        return this.ParseSoapResponse(soapResponse, "confirmShipperResponse");
                        //return gRNDetails;
                    }
                }
            }
            catch (Exception ex)
            {
                GRNDetails gRNDetails = new GRNDetails();
                gRNDetails.result = "Error";
                gRNDetails.errorcode = "Unexpected error from QAD..!" + ex.Message;
                return gRNDetails;
            }
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<Payload<string>> GenerateQADSOPGIXML(DataSet ds)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var env = new SalesOrderPGI.Envelope
                {
                    Header = new SalesOrderPGI.Header()
                    {
                        Action = "",
                        To = this.wsaTo,
                        MessageID = this.wsaMessageId,
                        ReferenceParameters = new SalesOrderPGI.ReferenceParameters() { suppressResponseDetail = false },
                        ReplyTo = new SalesOrderPGI.ReplyTo() { Address = "urn:services-qad-com:" }
                    },
                    Body = new SalesOrderPGI.Body()
                    {
                        ConfirmShipper = new SalesOrderPGI.ConfirmShipper()
                        {
                            DsSessionContext = new SalesOrderPGI.DsSessionContext()
                            {
                                lstTTContext = new List<SalesOrderPGI.TTContext>
                                {
                                    new SalesOrderPGI.TTContext()
                                    {
                                        PropertyQualifier = "QAD",
                                        PropertyName = "domain",
                                        PropertyValue = "kenya"
                                    },
                                    new SalesOrderPGI.TTContext()
                                    {
                                        PropertyQualifier = "QAD",
                                        PropertyName = "version",
                                        PropertyValue = "eB2_2"
                                    },
                                    new SalesOrderPGI.TTContext()
                                    {
                                        PropertyQualifier = "QAD",
                                        PropertyName = "action",
                                        PropertyValue = ""
                                    }
                                }
                            },
                            DsShipperConfirm = new SalesOrderPGI.DsShipperConfirm()
                            {
                                ShipperConfirm = new SalesOrderPGI.ShipperConfirm()
                                {
                                    operation = Convert.ToString(ds.Tables[0].Rows[0]["operation"]),
                                    absShipfrom = Convert.ToString(ds.Tables[0].Rows[0]["absShipfrom"]),
                                    confType = Convert.ToString(ds.Tables[0].Rows[0]["confType"]),
                                    absId = Convert.ToString(ds.Tables[0].Rows[0]["absId"]),
                                    shipDt = Convert.ToString(ds.Tables[0].Rows[0]["shipDt"]),
                                    effDate = Convert.ToString(ds.Tables[0].Rows[0]["effDate"]),
                                    absVehRef = Convert.ToString(ds.Tables[0].Rows[0]["absVehRef"]),
                                    shpTime = Convert.ToString(ds.Tables[0].Rows[0]["shpTime"]),
                                    arrDate = Convert.ToString(ds.Tables[0].Rows[0]["arrDate"]),
                                    arrTime = Convert.ToString(ds.Tables[0].Rows[0]["arrTime"]),
                                    autoInv = Convert.ToString(ds.Tables[0].Rows[0]["autoInv"]),
                                    autoPost = Convert.ToString(ds.Tables[0].Rows[0]["autoPost"]),
                                    useShipper = Convert.ToString(ds.Tables[0].Rows[0]["useShipper"]),
                                    consolidate = Convert.ToString(ds.Tables[0].Rows[0]["consolidate"]),
                                    lCalcFreight = Convert.ToString(ds.Tables[0].Rows[0]["lCalcFreight"]),
                                    sEffError = Convert.ToString(ds.Tables[0].Rows[0]["sEffError"]),
                                    ivdate = Convert.ToString(ds.Tables[0].Rows[0]["ivdate"]),
                                    yn = Convert.ToString(ds.Tables[0].Rows[0]["yn"]),
                                    invOnly = Convert.ToString(ds.Tables[0].Rows[0]["invOnly"]),
                                    printLotserials = Convert.ToString(ds.Tables[0].Rows[0]["printLotserials"]),
                                    printOptions = Convert.ToString(ds.Tables[0].Rows[0]["printOptions"]),
                                    compAddr = Convert.ToString(ds.Tables[0].Rows[0]["compAddr"]),
                                    formCode = Convert.ToString(ds.Tables[0].Rows[0]["formCode"]),
                                    discDet = Convert.ToString(ds.Tables[0].Rows[0]["discDet"]),
                                    discSum = Convert.ToString(ds.Tables[0].Rows[0]["discSum"]),
                                    msg = Convert.ToString(ds.Tables[0].Rows[0]["msg"]),
                                    dev = Convert.ToString(ds.Tables[0].Rows[0]["dev"]),
                                    yn1 = Convert.ToString(ds.Tables[0].Rows[0]["yn1"])
                                }
                            }
                        }
                    }
                };
                var serializer = new XmlSerializer(typeof(SalesOrderPGI.Envelope));
                var settings = new XmlWriterSettings
                {
                    Encoding = Encoding.UTF8,
                    Indent = true,
                    OmitXmlDeclaration = true,
                };
                var builder = new StringBuilder();
                using (var writer = XmlWriter.Create(builder, settings))
                {
                    serializer.Serialize(writer, env, env.xmlns);
                }

                response.Result = builder.ToString();
            }
            catch (SqlException Sqlex)
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
        public async Task<Payload<string>> GenerateQADWORKORDERGRNXML(DataSet ds)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var env = new WorkOrderGRNXML.Envelope
                {
                    Header = new WorkOrderGRNXML.Header()
                    {
                        Action = "",
                        To = this.wsaTo,
                        MessageID = this.wsaMessageId,
                        ReferenceParameters = new WorkOrderGRNXML.ReferenceParameters() { suppressResponseDetail = false },
                        ReplyTo = new WorkOrderGRNXML.ReplyTo() { Address = "urn:services-qad-com:" }

                    },
                    Body = new WorkOrderGRNXML.Body()
                    {
                        receiveWorkOrder = new WorkOrderGRNXML.ReceiveworkOrder()
                        {
                            dsSessionContext = new WorkOrderGRNXML.DsSessionContext()
                            {
                                lstTTContext = new List<WorkOrderGRNXML.TTContext>
                                {
                                    new WorkOrderGRNXML.TTContext()
                                    {
                                        PropertyQualifier= "QAD",
                                             PropertyName = "domain",
                                             PropertyValue="kenya"
                                    },
                                    new WorkOrderGRNXML.TTContext()
                                    {
                                        PropertyQualifier= "QAD",
                                             PropertyName = "version",
                                             PropertyValue="eB_2"
                                    },
                                    new WorkOrderGRNXML.TTContext()
                                    {
                                           PropertyQualifier= "QAD",
                                             PropertyName = "action",
                                             PropertyValue=""
                                    }
                                }
                            },
                            dsWorkOrderReceipt = new WorkOrderGRNXML.DsWorkOrderReceipt()
                            {
                                workOrderReceipt = new WorkOrderGRNXML.WorkOrderReceipt()
                                {
                                    operation = "A",
                                    woNbr = Convert.ToString(ds.Tables[0].Rows[0]["ordernum"]),
                                    woLot = Convert.ToString(ds.Tables[0].Rows[0]["woLot"]),
                                    currWkctr = "2201",
                                    currMch = "",
                                    yn = "true",
                                    lotserialQty = Convert.ToString(ds.Tables[0].Rows[0]["Qty"]),
                                    um = Convert.ToString(ds.Tables[0].Rows[0]["receiptUm"]),
                                    conv = "1",
                                    rejectQty = "0",
                                    rejectUm = Convert.ToString(ds.Tables[0].Rows[0]["receiptUm"]),
                                    rejectConv = "1",
                                    site = "10",
                                    location = "Holding",
                                    lotserial = Convert.ToString(ds.Tables[0].Rows[0]["lotserial"]),
                                    lotref = Convert.ToString(ds.Tables[0].Rows[0]["lotref"]),
                                    multiEntry = "false",
                                    chgAttr = "false",
                                    chgAssay = "0",
                                    assayActv = "false",
                                    chgGrade = "",
                                    gradeActv = "false",
                                    //chgExpire = Convert.ToString(ds.Tables[0].Rows[0]["chgExpire"]),
                                    chgExpire = "",
                                    expireActv = "true",
                                    chgStatus = "text",
                                    statusActv = "true",
                                    resetattr = "true",
                                    rmks = Convert.ToString(ds.Tables[0].Rows[0]["GRNUpdateID"]),
                                    effDate = Convert.ToString(ds.Tables[0].Rows[0]["effDate"]),
                                    closeWo = "false",
                                    yn1 = "true",
                                    yn2 = "true",
                                    yn3 = "true",
                                    lotSerialDetail = new WorkOrderGRNXML.LotSerialDetail()
                                    {
                                        operation = "A",
                                        bowlLotser = Convert.ToString(ds.Tables[0].Rows[0]["lotserial"]),
                                        bowlRef = Convert.ToString(ds.Tables[0].Rows[0]["lotref"]),
                                        bowlQty = Convert.ToString(ds.Tables[0].Rows[0]["Qty"]),
                                    },
                                    receiptDetail = new WorkOrderGRNXML.ReceiptDetail()
                                    {
                                        operation = "A",
                                        site = "10",
                                        location = "Holding",
                                        lotserial = Convert.ToString(ds.Tables[0].Rows[0]["lotserial"]),
                                        lotref = Convert.ToString(ds.Tables[0].Rows[0]["lotref"]),
                                        lotserialQty = Convert.ToString(ds.Tables[0].Rows[0]["Qty"])
                                    }

                                }
                            }
                        }
                    }
                };
                var serializer = new XmlSerializer(typeof(WorkOrderGRNXML.Envelope));
                var settings = new XmlWriterSettings
                {
                    Encoding = Encoding.UTF8,
                    Indent = true,
                    OmitXmlDeclaration = true,
                };
                var builder = new StringBuilder();
                using (var writer = XmlWriter.Create(builder, settings))
                {
                    serializer.Serialize(writer, env, env.xmlns);
                }
                response.Result = builder.ToString();
            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }

        public async Task<GRNDetails> UpdateShipmentforCustomerReturns(string xml)
        {
            try
            {
                string soapString = xml.ToString();
                //string uri = "http://UK1SAWN02272:8080/qxi/services/QdocWebService";
                string uri = this.QADSOAP;
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("SOAPAction", "urn:schemas-qad-com:xml-services:common");
                    //Request.Content.Headers.ContentType = new MediaTypeHeaderValue("Text/xml");
                    // client.DefaultRequestHeaders.Add("Content-Type", "Text/xml");
                    var content = new StringContent(soapString, Encoding.UTF8, "text/xml");
                    using (var response = await client.PostAsync(uri, content))
                    {
                        GRNDetails gRNDetails = new GRNDetails();
                        var soapResponse = await response.Content.ReadAsStringAsync();
                        return this.ParseQADNumberSoapResponse(soapResponse, "maintainSalesOrderShipperResponse ");
                        //return gRNDetails;
                    }
                }
            }
            catch (Exception ex)
            {
                GRNDetails gRNDetails = new GRNDetails();
                gRNDetails.result = "Error";
                gRNDetails.errorcode = "Unexpected error from QAD..!" + ex.Message;
                return gRNDetails;
            }
        }

        public GRNDetails ParseQADNumberSoapResponse(string response, string responseType)
        {
            XmlDocument document = new XmlDocument();
            document.LoadXml(response);  //loading soap message as string
            XmlNamespaceManager manager = new XmlNamespaceManager(document.NameTable);
            manager.AddNamespace("d", "http://schemas.xmlsoap.org/soap/envelope/");
            manager.AddNamespace("bhr", "urn:schemas-qad-com:xml-services");
            GRNDetails gRNDetails = new GRNDetails();
            XmlNodeList xnList = document.SelectNodes("//bhr:" + responseType, manager);
            int nodes = xnList.Count;
            if (nodes == 0)
            {
                gRNDetails.result = "Error";
                gRNDetails.errorcode = "No Response from QAD";
            }
            else
            {
                foreach (XmlNode xn in xnList)
                {
                    gRNDetails.result = xn["ns1:dsSalesOrderShipperResponse"]["ns1:salesOrderShipper"]["ns1:absId"] == null ? "Error" : xn["ns1:dsSalesOrderShipperResponse"]["ns1:salesOrderShipper"]["ns1:absId"].InnerText;
                    gRNDetails.errorcode = xn["ns3:dsExceptions"]?["ns3:temp_err_msg"]?["ns3:tt_msg_context"] == null ? xn["ns3:dsExceptions"]?["ns3:temp_err_msg"]?["ns3:tt_msg_desc"]?.InnerText : xn["ns3:dsExceptions"]?["ns3:temp_err_msg"]?["ns3:tt_msg_context"]?.InnerText;
                    //gRNDetails.Result = xn["ns1:dsSalesOrderShipperResponse"]["ns1:salesOrderShipper"]["ns1:absId"] == null ? "Error" : xn["ns1:dsSalesOrderShipperResponse"]["ns1:salesOrderShipper"]["ns1:absId"].InnerText;
                    //gRNDetails.ErrorCode = xn["ns3:dsExceptions"]["ns3:temp_err_msg"]["ns3:tt_msg_context"] == null ? xn["ns3:dsExceptions"]["ns3:temp_err_msg"]["ns3:tt_msg_desc"]?.InnerText : xn["ns3:dsExceptions"]["ns3:temp_err_msg"]["ns3:tt_msg_context"].InnerText;
                    gRNDetails.errorcode = string.IsNullOrEmpty(gRNDetails.errorcode) ? "" : gRNDetails.errorcode;
                }
            }

            return gRNDetails;
        }

        public async Task<GRNDetails> GetGRNRevertXMLData(int grnHeaderID, int isSupplierRtn)
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                DataSet ds = new DataSet();

                string QADGRNRevertQuery = "EXEC [dbo].[SP_GetGRNRevertXMLData] @GRNHeaderID = " + grnHeaderID + ",@IsSupplierRtn = " + isSupplierRtn + "";
                ds = DbUtility.GetDS(QADGRNRevertQuery, this.ConnectionString);

                if (ds.Tables[0].Rows.Count == 0)
                {
                    return new GRNDetails() { errorcode = "No data found", result = "error" };

                }
                else if (Convert.ToString(ds.Tables[0].Rows[0]["GRNCHECK"]) == "2")
                {
                    return new GRNDetails() { errorcode = "Once Goods-Out process is started, cannot revert the shipment details", result = "error" };
                }

                string xmlResult = GenerateQADGRNREVERTXML(ds).Result.Result;
                string soapString = xmlResult.ToString();
                //string uri = "http://UK1SAWN02272:8080/qxi/services/QdocWebService";
                string uri = this.QADSOAP;
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("SOAPAction", "urn:schemas-qad-com:xml-services:common");
                    //Request.Content.Headers.ContentType = new MediaTypeHeaderValue("Text/xml");
                    // client.DefaultRequestHeaders.Add("Content-Type", "Text/xml");
                    var content = new StringContent(soapString, Encoding.UTF8, "text/xml");
                    using (var response = await client.PostAsync(uri, content))
                    {
                        GRNDetails gRNDetails = new GRNDetails();
                        var soapResponse = await response.Content.ReadAsStringAsync();
                        return this.ParseSoapResponse(soapResponse, "returnPurchaseOrderResponse");
                        //return gRNDetails;
                    }
                }
            }
            catch (Exception ex)
            {
                GRNDetails gRNDetails = new GRNDetails();
                gRNDetails.result = "Error";
                gRNDetails.errorcode = "Unexpected error from QAD..!" + ex.Message;
                return gRNDetails;
            }
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<Payload<string>> GenerateQADGRNREVERTXML(DataSet DS)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            Payload<string> response = new Payload<string>();
            try
            {
                List<GRNRevertQAD.LineDetail> listLineDetail = new List<GRNRevertQAD.LineDetail>();
                GRNRevertQAD.LineDetail lineDetail = new GRNRevertQAD.LineDetail();
                foreach (DataRow row in DS.Tables[0].Rows)
                {
                    lineDetail = new GRNRevertQAD.LineDetail()
                    {
                        operation = "M",
                        line = row["line"].ToString(),
                        yn = "false",
                        lotserialQty = row["Qty"].ToString(),
                        packingQty = row["Qty"].ToString(),
                        returnUm = row["returnUm"].ToString(),
                        wolot = "",
                        woop = "",
                        site = "10",
                        location = row["QADLocation"].ToString(),
                        lotserial = row["lotserial"].ToString(),
                        lotref = row["lotref"].ToString(),
                        multiEntry = "false",
                        podReason = "",
                        cmmtYn = "false",
                        yn1 = "true",

                        issueDetail = new GRNRevertQAD.IssueDetaill()
                        {
                            operation = "A",
                            location = "Holding",
                            lotserial = row["lotserial"].ToString(),
                            lotref = row["lotref"].ToString(),
                            vendlot = row["vendlot"].ToString(),
                            lotserialQty = row["Qty"].ToString(),
                            serialsYn = "false"
                        },
                        lineDetailTransComment = new GRNRevertQAD.LineDetailTransComment()
                        {
                            operation = "A",
                            cmtSeq = "1",
                            cdRef = "Ref1",
                            cdType = "BK",
                            cdLang = "US",
                            cdSeq = "1",
                            cmtCmmt = "",
                            prtOnQuote = "true",
                            prtOnSo = "true",
                            prtOnInvoice = "true",
                            prtOnPacklist = "true",
                            prtOnPo = "true",
                            prtOnRct = "true",
                            prtOnRtv = "true",
                            prtOnShpr = "true",
                            prtOnBol = "true",
                            prtOnCus = "true",
                            prtOnProb = "true",
                            prtOnSchedule = "true",
                            prtOnIsrqst = "true",
                            prtOnDo = "true",
                            prtOnIntern = "true"
                        }
                    };
                    listLineDetail.Add(lineDetail);
                }
                var env = new GRNRevertQAD.Envelope
                {
                    Header = new GRNRevertQAD.Header()
                    {
                        Action = "",
                        To = this.wsaTo,
                        MessageID = this.wsaMessageId,
                        ReferenceParameters = new GRNRevertQAD.ReferenceParameters() { suppressResponseDetail = false },
                        ReplyTo = new GRNRevertQAD.ReplyTo() { Address = "urn:services-qad-com:" }
                    },
                    Body = new GRNRevertQAD.Body()
                    {
                        returnPurchaseOrder = new GRNRevertQAD.ReturnPurchaseOrder()
                        {
                            dsSessionContext = new GRNRevertQAD.DsSessionContext()
                            {
                                lstTTContext = new List<GRNRevertQAD.TTContext>
                                {
                                    new GRNRevertQAD.TTContext()
                                    {
                                        PropertyQualifier= "QAD",
                                             PropertyName = "domain",
                                             PropertyValue="kenya"
                                    },
                                    new GRNRevertQAD.TTContext()
                                    {
                                        PropertyQualifier= "QAD",
                                             PropertyName = "version",
                                             PropertyValue="eB_2"
                                    },
                                    //new GRNRevertQAD.TTContext()
                                    //{
                                    //       PropertyQualifier= "QAD",
                                    //         PropertyName = "action",
                                    //         PropertyValue=""
                                    //}
                                }
                            },
                            dsPurchaseOrderReturn = new GRNRevertQAD.DsPurchaseOrderReturn()
                            {
                                purchaseOrderReturn = new GRNRevertQAD.PurchaseOrderReturn()
                                {
                                    operation = "M",
                                    poNbr = Convert.ToString(DS.Tables[0].Rows[0]["ordernum"]),
                                    yn = "true",
                                    receivernbr = "",
                                    vShipfrom = "10",
                                    vShipto = "00018130",
                                    effDate = Convert.ToString(DS.Tables[0].Rows[0]["effDate"]),
                                    fillAll = "false",
                                    replace = "true",
                                    cmmtYn = "true",
                                    move = "true",
                                    vRate = "1",
                                    vRate2 = "1",
                                    yn1 = "true",
                                    yn2 = "true",

                                    purchaseOrderReturnTransComment = new GRNRevertQAD.PurchaseOrderReturnTransComment()
                                    {
                                        operation = "A",
                                        cmtSeq = "2",
                                        cdRef = "TestR1",
                                        cdType = "BK",
                                        cdLang = "US",
                                        cdSeq = "1",
                                        cmtCmmt = "TEST",
                                        prtOnQuote = "true",
                                        prtOnSo = "true",
                                        prtOnInvoice = "true",
                                        prtOnPacklist = "true",
                                        prtOnPo = "false",
                                        prtOnRct = "false",
                                        prtOnRtv = "true",
                                        prtOnShpr = "true",
                                        prtOnBol = "true",
                                        prtOnCus = "true",
                                        prtOnProb = "true",
                                        prtOnSchedule = "true",
                                        prtOnIsrqst = "true",
                                        prtOnDo = "true",
                                        prtOnIntern = "true"
                                    },
                                    lineDetail = listLineDetail
                                }
                            }
                        }
                    }
                };
                var serializer = new XmlSerializer(typeof(GRNRevertQAD.Envelope));
                var settings = new XmlWriterSettings
                {
                    Encoding = Encoding.UTF8,
                    Indent = true,
                    OmitXmlDeclaration = true,
                };
                var builder = new StringBuilder();
                using (var writer = XmlWriter.Create(builder, settings))
                {
                    serializer.Serialize(writer, env,
                        env.xmlns);
                }
                response.Result = builder.ToString();
            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }

        public async Task<GRNDetails> GetCycleCountXMLData(InitiateStockModel stockObj)
        {
            try
            {
                //DataSet ds = new DataSet();
                //ds = _oInboundDAL.QADCYCLECOUNTXMLDATA(CCID);
                //if (ds.Tables[0].Rows.Count == 0)
                //{
                //    return new GRNDetails() { ErrorCode = "No data found", Result = "error" };

                //}
                List<InitiateStockModel> lstStockData = new List<InitiateStockModel>();
                lstStockData.Add(stockObj);
                string xmlResult = GenereateCycleCountXML(lstStockData).Result.Result;
                string soapString = xmlResult.ToString();
                //string uri = "http://UK1SAWN02272:8080/qxi/services/QdocWebService";
                string uri = this.QADSOAP;
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("SOAPAction", "urn:schemas-qad-com:xml-services:common");
                    //Request.Content.Headers.ContentType = new MediaTypeHeaderValue("Text/xml");
                    // client.DefaultRequestHeaders.Add("Content-Type", "Text/xml");
                    var content = new StringContent(soapString, Encoding.UTF8, "text/xml");
                    using (var response = await client.PostAsync(uri, content))
                    {
                        GRNDetails gRNDetails = new GRNDetails();
                        var soapResponse = await response.Content.ReadAsStringAsync();
                        return this.ParseSoapResponse(soapResponse, "enterCycleCountResultsResponse");
                        //return gRNDetails;
                    }
                }
            }
            catch (Exception ex)
            {
                GRNDetails gRNDetails = new GRNDetails();
                gRNDetails.result = "Error";
                gRNDetails.errorcode = "Unexpected error from QAD..!" + ex.Message;
                return gRNDetails;
            }
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<Payload<string>> GenereateCycleCountXML(List<InitiateStockModel> lstData)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            Payload<string> response = new Payload<string>();
            try
            {
                List<CycleCountQADXML.CycleCount> listCycleCount = new List<CycleCountQADXML.CycleCount>();
                CycleCountQADXML.CycleCount cycleCount = new CycleCountQADXML.CycleCount();
                foreach (var item in lstData)
                {
                    cycleCount = new CycleCountQADXML.CycleCount()
                    {
                        Operation = "A",
                        Part = item.MCode,
                        Site = "10",
                        Location = item.QADLocation,
                        Lotserial = item.BatchNo,
                        Lotref = item.ProjectRefNo,
                        QtyCnt = Convert.ToString(item.PhysicalQuantity),
                        Um = Convert.ToString(item.UoM),
                        Conv = Convert.ToString(item.UoMQty),
                        Rmks = item.Remarks,
                        EffDate = item.effDate,
                        CrAcct = item.crAcct,
                        CrSub = "",
                        CrCc = item.crCc,
                        Yn = item.yn
                    };
                    listCycleCount.Add(cycleCount);
                }
                var env = new CycleCountQADXML.Envelope
                {
                    Header = new CycleCountQADXML.Header()
                    {
                        Action = "",
                        To = this.wsaTo,
                        MessageID = this.wsaMessageId,
                        ReferenceParameters = new CycleCountQADXML.ReferenceParameters() { suppressResponseDetail = false },
                        ReplyTo = new CycleCountQADXML.ReplyTo() { Address = "urn:services-qad-com:" }

                    },
                    Body = new CycleCountQADXML.Body()
                    {
                        EnterCycleCountResults = new CycleCountQADXML.EnterCycleCountResults()
                        {
                            DsSessionContext = new CycleCountQADXML.DsSessionContext()
                            {
                                lstTTContext = new List<CycleCountQADXML.TTContext>
                                {
                                    new CycleCountQADXML.TTContext()
                                    {
                                        PropertyQualifier= "QAD",
                                             PropertyName = "domain",
                                             PropertyValue="kenya"
                                    },
                                    new CycleCountQADXML.TTContext()
                                    {
                                        PropertyQualifier= "QAD",
                                             PropertyName = "version",
                                             PropertyValue="eB_2"
                                    },
                                    new CycleCountQADXML.TTContext()
                                    {
                                           PropertyQualifier= "QAD",
                                             PropertyName = "action",
                                             PropertyValue=""
                                    }
                                }
                            },
                            DsCycleCountResults = new CycleCountQADXML.DsCycleCountResults()
                            {
                                CycleCountResults = new CycleCountQADXML.CycleCountResults()
                                {
                                    Operation = "A",
                                    CcInitial = "false",
                                    LstCycleCount = listCycleCount
                                }
                            }
                        }
                    }
                };
                var serializer = new XmlSerializer(typeof(CycleCountQADXML.Envelope));
                var settings = new XmlWriterSettings
                {
                    Encoding = Encoding.UTF8,
                    Indent = true,
                    OmitXmlDeclaration = true,
                };
                var builder = new StringBuilder();
                using (var writer = XmlWriter.Create(builder, settings))
                {
                    serializer.Serialize(writer, env,
                        env.xmlns);
                }

                response.Result = builder.ToString();
            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }

        public async Task<GRNDetails> QADUpdateInventorystatus(int TransferRequestID, int TransferTypeID, string uniqueID)
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                DataSet ds = new DataSet();
          
                string QADUpdateInventorystatusQuery = "EXEC [dbo].[sp_Transfer_SAPPosingData] @TransferRequestID = " + TransferRequestID + "";
                ds = DbUtility.GetDS(QADUpdateInventorystatusQuery, this.ConnectionString);


                if (ds.Tables[0].Rows.Count == 0)
                {
                    return new GRNDetails() { errorcode = "No data found", result = "error" };

                }
                string xmlResult = "";
                string ResponseType = "";
                if (TransferTypeID == 5)//SL to SL
                {
                    xmlResult = GenereateXMLforUpdateInventoryStatus(ds, uniqueID).Result.Result;
                    ResponseType = "transferInventoryResponse";
                }
                else //Expiry
                {
                    xmlResult = GenerateXMLforUpdateExpiryDate(ds).Result.Result;
                    ResponseType = "maintainInventoryDetailResponse";
                }
                string soapString = xmlResult.ToString();
                //string uri = "http://UK1SAWN02272:8080/qxi/services/QdocWebService";
                string uri = this.QADSOAP;
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("SOAPAction", "urn:schemas-qad-com:xml-services:common");
                    //Request.Content.Headers.ContentType = new MediaTypeHeaderValue("Text/xml");
                    // client.DefaultRequestHeaders.Add("Content-Type", "Text/xml");
                    var content = new StringContent(soapString, Encoding.UTF8, "text/xml");
                    using (var response = await client.PostAsync(uri, content))
                    {
                        GRNDetails gRNDetails = new GRNDetails();
                        var soapResponse = await response.Content.ReadAsStringAsync();
                        return this.ParseSoapResponse(soapResponse, ResponseType);
                        //return gRNDetails;
                    }
                }
            }
            catch (Exception ex)
            {
                GRNDetails gRNDetails = new GRNDetails();
                gRNDetails.result = "Error";
                gRNDetails.errorcode = "Unexpected error from QAD..!" + ex.Message;
                return gRNDetails;
            }
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<Payload<string>> GenereateXMLforUpdateInventoryStatus(DataSet DS, string uniqueID)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            Payload<string> response = new Payload<string>();
            try
            {
                List<QADRequestObj> lstqADRequestObjs = new List<QADRequestObj>();
                lstqADRequestObjs = (from DataRow dr in DS.Tables[0].Rows
                                     select new QADRequestObj()
                                     {
                                         MCode = Convert.ToString(dr["SourceSKU"]),
                                         BatchNo = Convert.ToString(dr["SourceBatch"]),
                                         CurrentDate = Convert.ToString(dr["effDate"]),
                                         FromLoc = Convert.ToString(dr["FromQADLoc"]),
                                         ProjectRefNo = Convert.ToString(dr["ProjectRefNo"]),
                                         Qty = Convert.ToDecimal(dr["Quantity"]),
                                         ToLoc = Convert.ToString(dr["ToQADLoc"]),
                                         TotalQty = Convert.ToDecimal(dr["TotalQty"]),
                                         RID = Convert.ToInt32(dr["RID"]),
                                         FromQADStatus = Convert.ToString(dr["FROMQADStatus"]),
                                         TOQADStatus = Convert.ToString(dr["TOQADStatus"]),
                                     }).ToList();
                int MaxCount = lstqADRequestObjs.Max(x => x.RID);
                List<InventoryStatusUpdateXML.InventoryTransfer> inventoryTransfers = new List<InventoryStatusUpdateXML.InventoryTransfer>();
                for (int i = 1; i <= MaxCount; i++)
                {
                    List<QADRequestObj> qADRequestObjs = lstqADRequestObjs.Where(x => x.RID == i).ToList();
                    InventoryStatusUpdateXML.InventoryTransfer inventoryTransfer = new InventoryStatusUpdateXML.InventoryTransfer();
                    inventoryTransfer.operation = "A";
                    inventoryTransfer.part = qADRequestObjs[0].MCode;
                    inventoryTransfer.lotserialQty = Convert.ToString(qADRequestObjs[0].TotalQty);
                    inventoryTransfer.effDate = qADRequestObjs[0].CurrentDate;
                    List<InventoryStatusUpdateXML.InventoryDetail> inventoryDetails = new List<InventoryStatusUpdateXML.InventoryDetail>();
                    foreach (QADRequestObj obj in qADRequestObjs)
                    {
                        InventoryStatusUpdateXML.InventoryDetail inventoryDetail = new InventoryStatusUpdateXML.InventoryDetail();
                        inventoryDetail.operation = "A";
                        inventoryDetail.lotserialQty = obj.Qty;
                        inventoryDetail.effDate = obj.CurrentDate;
                        inventoryDetail.nbr = "";
                        inventoryDetail.soJob = "";
                        inventoryDetail.rmks = uniqueID;
                        inventoryDetail.siteFrom = "10";
                        inventoryDetail.locFrom = obj.FromLoc;
                        inventoryDetail.lotserFrom = obj.BatchNo;
                        inventoryDetail.lotrefFrom = obj.ProjectRefNo;
                        inventoryDetail.siteTo = "10";
                        inventoryDetail.locTo = obj.ToLoc;
                        inventoryDetail.lotserTo = obj.BatchNo;
                        inventoryDetail.lotrefTo = obj.ProjectRefNo;
                        inventoryDetail.yn = "true";
                        inventoryDetails.Add(inventoryDetail);
                    }
                    inventoryTransfer.inventoryDetail = inventoryDetails;
                    inventoryTransfers.Add(inventoryTransfer);
                }


                //foreach (DataRow row in ds.Tables[0].Rows)
                //{
                //    ObjInventoryDetail = new InventoryStatusUpdateXML.InventoryDetail()
                //    {
                //        operation = "A",
                //        lotserialQty = row["Quantity"].ToString(),
                //        effDate = row["effDate"].ToString(),
                //        nbr = "",
                //        soJob = "",
                //        rmks = "",
                //        siteFrom = "10",
                //        locFrom = row["FromQADLoc"].ToString(),
                //        lotserFrom = row["SourceBatch"].ToString(),
                //        lotrefFrom = row["ProjectRefNo"].ToString(),
                //        siteTo = "10",
                //        locTo = row["ToQADLoc"].ToString(),
                //        lotserTo = row["SourceBatch"].ToString(),
                //        lotrefTo = row["ProjectRefNo"].ToString(),
                //        yn = "true"


                //    };
                //    lstInventoryDetails.Add(ObjInventoryDetail);
                //}

                var env = new InventoryStatusUpdateXML.Envelope
                {
                    Header = new InventoryStatusUpdateXML.Header()
                    {
                        Action = "",
                        To = this.wsaTo,
                        MessageID = this.wsaMessageId,
                        ReferenceParameters = new InventoryStatusUpdateXML.ReferenceParameters() { suppressResponseDetail = false },
                        ReplyTo = new InventoryStatusUpdateXML.ReplyTo() { Address = "urn:services-qad-com:" }

                    },
                    Body = new InventoryStatusUpdateXML.Body()
                    {
                        transferInventory = new InventoryStatusUpdateXML.TransferInventory()
                        {
                            dsSessionContext = new InventoryStatusUpdateXML.DsSessionContext()
                            {
                                lstTTContext = new List<InventoryStatusUpdateXML.TTContext>
                                {
                                    new InventoryStatusUpdateXML.TTContext()
                                    {
                                        PropertyQualifier= "QAD",
                                             PropertyName = "domain",
                                             PropertyValue="kenya"
                                    },
                                    new InventoryStatusUpdateXML.TTContext()
                                    {
                                        PropertyQualifier= "QAD",
                                             PropertyName = "version",
                                             PropertyValue="eB_2"
                                    },
                                    new InventoryStatusUpdateXML.TTContext()
                                    {
                                           PropertyQualifier= "QAD",
                                             PropertyName = "action",
                                             PropertyValue=""
                                    }
                                }
                            },
                            dsInventoryTransfer = new InventoryStatusUpdateXML.DsInventoryTransfer()
                            {
                                inventoryTransfer = inventoryTransfers
                            }
                        }
                    }
                };
                var serializer = new XmlSerializer(typeof(InventoryStatusUpdateXML.Envelope));
                var settings = new XmlWriterSettings
                {
                    Encoding = Encoding.UTF8,
                    Indent = true,
                    OmitXmlDeclaration = true,
                };
                var builder = new StringBuilder();
                using (var writer = XmlWriter.Create(builder, settings))
                {
                    serializer.Serialize(writer, env, env.xmlns);
                }

                response.Result = builder.ToString();
            }
            catch (SqlException Sqlex)
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
        public async Task<Payload<string>> GenerateXMLforUpdateExpiryDate(DataSet DS)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            Payload<string> response = new Payload<string>();
            try
            {
                List<QADRequestObj> lstqADRequestObjs = new List<QADRequestObj>();
                lstqADRequestObjs = (from DataRow dr in DS.Tables[0].Rows
                                     select new QADRequestObj()
                                     {
                                         MCode = Convert.ToString(dr["SourceSKU"]),
                                         BatchNo = Convert.ToString(dr["SourceBatch"]),
                                         CurrentDate = Convert.ToString(dr["effDate"]),
                                         FromLoc = Convert.ToString(dr["FromQADLoc"]),
                                         ProjectRefNo = Convert.ToString(dr["ProjectRefNo"]),
                                         Qty = Convert.ToDecimal(dr["Quantity"]),
                                         ToLoc = Convert.ToString(dr["ToQADLoc"]),
                                         TotalQty = Convert.ToDecimal(dr["TotalQty"]),
                                         RID = Convert.ToInt32(dr["RID"]),
                                         ExpDate = Convert.ToString(dr["ToExpDate"]),
                                         FromQADStatus = Convert.ToString(dr["FROMQADStatus"]),
                                         TOQADStatus = Convert.ToString(dr["TOQADStatus"]),
                                     }).ToList();

                List<ExpiryDateXML.InventoryDetail> lstinventoryDetails = new List<ExpiryDateXML.InventoryDetail>();
                foreach (QADRequestObj obj in lstqADRequestObjs)
                {
                    ExpiryDateXML.InventoryDetail inventoryDetail = new ExpiryDateXML.InventoryDetail
                    {
                        operation = "A",
                        ldSite = "10",
                        ldLoc = obj.FromLoc,
                        ldPart = obj.MCode,
                        ldLot = obj.BatchNo,
                        ldRef = obj.ProjectRefNo,
                        ldExpire = obj.ExpDate,
                        ldGrade = "",
                        ldAssay = "",
                        ldStatus = obj.FromQADStatus
                    };
                    lstinventoryDetails.Add(inventoryDetail);
                }

                var env = new ExpiryDateXML.Envelope
                {
                    Header = new ExpiryDateXML.Header()
                    {
                        Action = "",
                        To = this.wsaTo,
                        MessageID = this.wsaMessageId,
                        ReferenceParameters = new ExpiryDateXML.ReferenceParameters() { suppressResponseDetail = false },
                        ReplyTo = new ExpiryDateXML.ReplyTo() { Address = "urn:services-qad-com:" }

                    },
                    Body = new ExpiryDateXML.Body()
                    {
                        maintainInventoryDetail = new ExpiryDateXML.MaintainInventoryDetail()
                        {
                            dsSessionContext = new ExpiryDateXML.DsSessionContext()
                            {
                                lstTTContext = new List<ExpiryDateXML.TTContext>
                                {
                                    new ExpiryDateXML.TTContext()
                                    {
                                        PropertyQualifier= "QAD",
                                             PropertyName = "domain",
                                             PropertyValue="kenya"
                                    },
                                    new ExpiryDateXML.TTContext()
                                    {
                                        PropertyQualifier= "QAD",
                                             PropertyName = "version",
                                             PropertyValue="eB_2"
                                    },
                                    new ExpiryDateXML.TTContext()
                                    {
                                           PropertyQualifier= "QAD",
                                             PropertyName = "action",
                                             PropertyValue=""
                                    }
                                }
                            },
                            dsInventoryDetail = new ExpiryDateXML.DsInventoryDetail()
                            {
                                inventoryDetail = lstinventoryDetails
                            }
                        }
                    }
                };
                var serializer = new XmlSerializer(typeof(ExpiryDateXML.Envelope));
                var settings = new XmlWriterSettings
                {
                    Encoding = Encoding.UTF8,
                    Indent = true,
                    OmitXmlDeclaration = true,
                };
                var builder = new StringBuilder();
                using (var writer = XmlWriter.Create(builder, settings))
                {
                    serializer.Serialize(writer, env, env.xmlns);
                }

                response.Result = builder.ToString();
            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }

        public async Task<GRNDetails> GetSOPGIXMLDATA(int OutboundID)
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                DataSet ds = new DataSet();

                string SOPGIXMLQuery = "EXEC [dbo].[USP_SOPGIXMLDATA] @OutboundId = " + OutboundID + ",@GRNHeaderID = 0 , @ShipNo = ''";
                ds = DbUtility.GetDS(SOPGIXMLQuery, this.ConnectionString);


                if (ds.Tables[0].Rows.Count == 0)
                {
                    return new GRNDetails() { errorcode = "No data found", result = "error" };
                }
                string xmlResult = GenerateQADSOPGIXML(ds).Result.Result;
                string soapString = xmlResult.ToString();
                
                //string pattern = @"<(\w+)[^>]*>\s*<\/\1>|<(\w+)[^>]*/\s*>";
                //string soapString = Regex.Replace(StringXML, pattern, "<$1$2/>");

                //string uri = "http://UK1SAWN02272:8080/qxi/services/QdocWebService";
                string uri = this.QADSOAP;
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("SOAPAction", "urn:schemas-qad-com:xml-services:common");
                    //Request.Content.Headers.ContentType = new MediaTypeHeaderValue("Text/xml");
                    // client.DefaultRequestHeaders.Add("Content-Type", "Text/xml");
                    var content = new StringContent(soapString, Encoding.UTF8, "text/xml");
                    using (var response = await client.PostAsync(uri, content))
                    {
                        GRNDetails gRNDetails = new GRNDetails();
                        var soapResponse = await response.Content.ReadAsStringAsync();
                        return this.ParseSoapResponse(soapResponse, "confirmShipperResponse");
                        //return gRNDetails;
                    }
                }
            }
            catch (Exception ex)
            {
                GRNDetails gRNDetails = new GRNDetails();
                gRNDetails.result = "Error";
                gRNDetails.errorcode = "Unexpected error from QAD..!" + ex.Message;
                return gRNDetails;
            }
        }

        public async Task<GRNDetails> QADShipmentRequest(int OutboundID)
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                DataSet ds = new DataSet();
                string QADShipmentRequestQuery = "EXEC [dbo].[USP_GET_QADSOShipmentData] @OutboundId = " + OutboundID + "";
                ds = DbUtility.GetDS(QADShipmentRequestQuery, this.ConnectionString);

                if (ds.Tables[0].Rows.Count == 0)
                {
                    return new GRNDetails() { errorcode = "No data found", result = "Error" };
                }
                string xmlResult = XMLforQADSOShipment(ds).Result.Result;
                string soapString = xmlResult.ToString();
                //string uri = "http://UK1SAWN02272:8080/qxi/services/QdocWebService";
                string uri = this.QADSOAP;
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("SOAPAction", "urn:schemas-qad-com:xml-services:common");
                    //Request.Content.Headers.ContentType = new MediaTypeHeaderValue("Text/xml");
                    // client.DefaultRequestHeaders.Add("Content-Type", "Text/xml");
                    var content = new StringContent(soapString, Encoding.UTF8, "text/xml");
                    using (var response = await client.PostAsync(uri, content))
                    {
                        GRNDetails gRNDetails = new GRNDetails();
                        var soapResponse = await response.Content.ReadAsStringAsync();
                        return this.ParseQADNumberSoapResponse(soapResponse, "maintainSalesOrderShipperResponse ");
                        //return gRNDetails;
                    }
                }
            }
            catch (Exception ex)
            {
                GRNDetails gRNDetails = new GRNDetails();
                gRNDetails.result = "Error";
                gRNDetails.errorcode = "Unexpected error from QAD..!" + ex.Message;
                return gRNDetails;
            }
        }

        public async Task<WorkOrderResponse> GetItemLevelWOPGIXMLDATA(QADRequestObj qadWOData)
        {
#pragma warning disable CS0219 // The variable 'UserID' is assigned but its value is never used
            int UserID = 0;
#pragma warning restore CS0219 // The variable 'UserID' is assigned but its value is never used
            string soapString = "";
            int OutboundID = 0;
            var QADTransaction = "";
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                WorkOrderResponse woRespondeObject = new WorkOrderResponse();

                string sp = "select TOP 1 ISNULL(NULLIF(QADTransaction,''),'0') AS S from OBD_QADPGIPICKINFO where VLPDPickID=@VLPDPickID ORDER BY PGIPICKINFOID DESC";
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sp, connection))
                    {
                        command.Parameters.AddWithValue("@VLPDPickID", qadWOData.PickID);
                        QADTransaction = (string)command.ExecuteScalar();
                    }
                }

                if (string.IsNullOrEmpty(QADTransaction) || QADTransaction.Trim() == "0")
                {
                    List<QADRequestObj> innerList = new List<QADRequestObj>();
                    innerList.Add(qadWOData);
                    GRNDetails objGRNDetail = new GRNDetails();
                    string xmlResult = GenerateWOPGIXML(innerList).Result.Result;
                    soapString = xmlResult.ToString();
                    //string uri = "http://UK1SAWN02272:8080/qxi/services/QdocWebService";
                    string uri = this.QADSOAP;
                    using (var client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Add("SOAPAction", "urn:schemas-qad-com:xml-services:common");
                        var content = new StringContent(soapString, Encoding.UTF8, "text/xml");
                        using (var response = await client.PostAsync(uri, content))
                        {
                            var soapResponse = await response.Content.ReadAsStringAsync();
                            objGRNDetail = this.ParseSoapResponse(soapResponse, "issueWorkOrderComponentResponse");

                            //gRNDetails = this.ParseQADNumberSoapResponse(soapResponse, "maintainSalesOrderShipperResponse ");

                            var IsSuccess = objGRNDetail.result == "success" ? 1 : 0;
                            var error = objGRNDetail.errorcode;

                            //_oInboundDAL.GetInsertQADResponseLog(gRNHeaderID, soapString, "WOComponent", IsSuccess, error, 3, UserID);

                            woRespondeObject.result = objGRNDetail.result;
                            woRespondeObject.error = objGRNDetail.errorcode;
                        }
                    }
                    if (objGRNDetail.result == "success")
                    {
                        DataSet dresult = await UpdatePGIPICKInfo(innerList, 0);
                        if (dresult.Tables[0].Rows.Count > 0)
                        {
                            if (Convert.ToInt32(dresult.Tables[0].Rows[0][0]) == 3)
                            {
                                woRespondeObject.result = "PGI updated successfully..!";
                                woRespondeObject.error = "";
                            }
                            else if (Convert.ToInt32(dresult.Tables[0].Rows[0][0]) == -1)
                            {
                                woRespondeObject.result = "error";
                                woRespondeObject.error = "Unexpected error..!";
                            }
                            else if (Convert.ToInt32(dresult.Tables[0].Rows[0][0]) == -5)
                            {
                                woRespondeObject.result = "error";
                                woRespondeObject.error = "No stock available";
                            }
                        }
                        else
                        {
                            woRespondeObject.result = "WMS Error";
                            woRespondeObject.error = "No response from WMS";
                        }
                    }
                    else
                    {
                        DataSet dresult = await UpdatePGIPICKInfo(innerList, 1, woRespondeObject.error);
                    }
                    return woRespondeObject;
                }
                else
                {
                    woRespondeObject.result = "PGI already completed for this material";
                    woRespondeObject.error = "";
                    return woRespondeObject;
                }
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                GRNDetails gRNDetails = new GRNDetails();
                gRNDetails.result = "Error";
                //gRNDetails.Result = "Success";
                gRNDetails.errorcode = "Unexpected error from QAD..!";

                //string result = ex.Message;
                OutboundID = qadWOData.OutboundID;
                int MaterialMasterID = qadWOData.MMID;
                string Remarks = gRNDetails.errorcode;
                decimal Quantity = qadWOData.Qty;
                int SOHeaderID = qadWOData.SOHeaderID;
                int SODetailsID = qadWOData.SODetailsID;
                string BatchNo = qadWOData.BatchNo;
                string ProjectRefNo = qadWOData.ProjectRefNo;
                int CreatedBy = qadWOData.UserID;
                string ISError = qadWOData.ISError;
                QADTransaction = qadWOData.QADTransaction;
                int VLPDPickID = qadWOData.PickID;

                //string strQuery = "EXEC [dbo].[SP_Insert_QADResult] @MaterialMasterID=" + MaterialMasterID + ",@OutboundID = " + OutboundID + ",@Remarks=" + gRNDetails.ErrorCode + "";
                //DataSet dataSet = DB.GetDS(strQuery, false);

                await QADItemLevelWOPGI_InsertRemarks(OutboundID, SOHeaderID, SODetailsID, MaterialMasterID, BatchNo, ProjectRefNo, Quantity, Remarks, CreatedBy, ISError, QADTransaction, VLPDPickID);

                //_oInboundDAL.GetInsertQADResponseLog(gRNHeaderID, soapString, "WOComponent", 0, "Unexpected error from QAD..Task!", 3 , UserID);

                return new WorkOrderResponse() { error = "Unexpected error from QAD..!", result = "Error" };
            }
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<Payload<string>> GenerateWOPGIXML(List<QADRequestObj> reqQADList)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            Payload<string> response = new Payload<string>();
            try
            {
                List<WorkOrderPGI.ItemDetail> lstItemDetail = new List<WorkOrderPGI.ItemDetail>();
                WorkOrderPGI.ItemDetail workOrderobj = new WorkOrderPGI.ItemDetail();
                foreach (var item in reqQADList)
                {
                    workOrderobj = new WorkOrderPGI.ItemDetail()
                    {

                        Operation = "A",
                        Part = item.MCode,
                        Op = "",
                        LotserialQty = item.Qty,
                        SubComp = "false",
                        CancelBo = "false",
                        Site = "10",
                        Location = "RLSD", //item.FromLoc,
                        Lotserial = item.BatchNo,
                        Lotref = item.ProjectRefNo,
                        MultiEntry = "false",
                        IssueDetail = new WorkOrderPGI.IssueDetail()
                        {
                            Operation = "A",
                            Site = "10",
                            Location = "RLSD", //item.FromLoc,
                            Lotserial = item.BatchNo,
                            Lotref = item.ProjectRefNo,
                            LotserialQty = item.Qty,
                        }
                    };
                    lstItemDetail.Add(workOrderobj);
                }

                var env = new WorkOrderPGI.Envelope
                {
                    Header = new WorkOrderPGI.Header()
                    {
                        Action = "",
                        To = this.wsaTo,
                        MessageID = this.wsaMessageId,
                        ReferenceParameters = new WorkOrderPGI.ReferenceParameters() { suppressResponseDetail = false },
                        ReplyTo = new WorkOrderPGI.ReplyTo() { Address = "urn:services-qad-com:" }
                    },
                    Body = new WorkOrderPGI.Body()
                    {
                        IssueWorkOrderComponent = new WorkOrderPGI.IssueWorkOrderComponent()
                        {
                            DsSessionContext = new WorkOrderPGI.DsSessionContext()
                            {
                                lstTTContext = new List<WorkOrderPGI.TTContext>
                                {
                                    new WorkOrderPGI.TTContext()
                                    {
                                        PropertyQualifier= "QAD",
                                             PropertyName = "domain",
                                             PropertyValue="kenya"
                                    },
                                    new WorkOrderPGI.TTContext()
                                    {
                                        PropertyQualifier= "QAD",
                                             PropertyName = "version",
                                             PropertyValue="eB_2"
                                    },
                                    new WorkOrderPGI.TTContext()
                                    {
                                           PropertyQualifier= "QAD",
                                             PropertyName = "action",
                                             PropertyValue=""
                                    }
                                }
                            },
                            DsWorkOrderComponent = new WorkOrderPGI.DsWorkOrderComponent()
                            {
                                WorkOrderComponent = new WorkOrderPGI.WorkOrderComponent()
                                {

                                    Operation = "A",
                                    WoNbr = reqQADList[0].SONumber,
                                    WoLot = reqQADList[0].WoLot,
                                    WoOp = "",
                                    EffDate = reqQADList[0].CurrentDate,
                                    FillAll = "false",
                                    FillPick = "false",
                                    Yn = "true",
                                    Yn1 = "true",
                                    CurrWkctr = "",
                                    CurrMch = "",
                                    Wiplot = "",
                                    Yn2 = "true",
                                    Yn3 = "true",
                                    ItemDetail = lstItemDetail,
                                    LotSerialDetail = new WorkOrderPGI.LotSerialDetail()
                                    {
                                        Operation = "",
                                        BiwlLotser = "",
                                        BiwlRef = "",
                                        BiwlQty = ""
                                    }
                                }
                            }
                        }
                    }
                };
                var serializer = new XmlSerializer(typeof(WorkOrderPGI.Envelope));
                var settings = new XmlWriterSettings
                {
                    Encoding = Encoding.UTF8,
                    Indent = true,
                    OmitXmlDeclaration = true,
                };
                var builder = new StringBuilder();
                using (var writer = XmlWriter.Create(builder, settings))
                {
                    serializer.Serialize(writer, env, env.xmlns);
                }

                response.Result = builder.ToString();
            }
            catch (SqlException Sqlex)
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
        public async Task<DataSet> UpdatePGIPICKInfo(List<QADRequestObj> items, int isQADError, string error = "")
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DataSet response = new DataSet();
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                var emptyNamepsaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });
                emptyNamepsaces.Add("", "");
                XmlSerializer serializer = new XmlSerializer(typeof(List<QADRequestObj>));
                string finalxml = "";
                using (var stream = new StringWriter())
                {
                    serializer.Serialize(stream, items, emptyNamepsaces);
                    string xml = stream.ToString();
                    finalxml = xml.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");
                }

                string Query = "EXEC [dbo].[USP_UPDATEQADPGIPICKINFO] @XML = " + "'" + finalxml + "'" + ",@QADError = " + 0 + ",@Error = " + "'" + error + "'" + "";
                var DS = DbUtility.GetDS(Query, this.ConnectionString);

            }
            catch (SqlException sqlEx)
            {
                DataSet errorDataSet = new DataSet("ErrorDataSet");
                DataTable errorTable = new DataTable("ErrorTable");
              
                errorTable.Columns.Add("ErrorMessage", typeof(string));
                DataRow errorRow = errorTable.NewRow();
                errorRow["ErrorMessage"] = sqlEx.Message;
                errorTable.Rows.Add(errorRow);
                errorDataSet.Tables.Add(errorTable);

                return errorDataSet;
            }
            catch (Exception ex)
            {
                DataSet errorDataSet = new DataSet("ErrorDataSet");
                DataTable errorTable = new DataTable("ErrorTable");
                errorTable.Columns.Add("ErrorMessage", typeof(string));

                DataRow errorRow = errorTable.NewRow();
                errorRow["ErrorMessage"] = ex.Message;
                errorTable.Rows.Add(errorRow);
                errorDataSet.Tables.Add(errorTable);

                return errorDataSet;
            }
            return response;
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<Payload<string>> QADItemLevelWOPGI_InsertRemarks(int OutboundID, int SOHeaderID, int SODetailsID, int MaterialMasterID, string BatchNo, string ProjectRefNo, decimal Quantity, string Remarks, int UserID, string ISError, string QADTransaction, int VLPDPickID)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            Payload<string> response = new Payload<string>();
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                string strQuery = "EXEC [dbo].[SP_Insert_QADResult] @MaterialMasterID=" + MaterialMasterID + ",@OutboundID = " + OutboundID + ",@Remarks=" + "'" + Remarks + "'" + ",@Quantity=" + Quantity + ",@SOHeaderID=" + SOHeaderID + ",@SODetailsID=" + SODetailsID + ",@BatchNo=" + "'" + BatchNo + "'" + ",@ProjectRefNo=" + "'" + ProjectRefNo + "'" + ",@CreatedBy=" + UserID + ",@ISError=" + "'" + ISError + "'" + ",@QADTransaction=" + "'" + QADTransaction + "'" + ",@VLPDPickID=" + VLPDPickID + "";
                DbUtility.GetSqlN(strQuery, ConnectionString);
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


        public BO.Inbound CheckProjectQty(BO.Inbound inbound)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            StringBuilder sCheckProjectQty = new StringBuilder();

            sCheckProjectQty.AppendLine("EXEC [dbo].[usp_CheckProjectQty] ");
            sCheckProjectQty.AppendLine("@SupplierInvoiceDetailsId = " + inbound.SupplierInvoiceDetailsId + ",");
            sCheckProjectQty.AppendLine("@Lineno = " + inbound.Lineno + ",");
            sCheckProjectQty.AppendLine("@ReceivingQty = " + inbound.Qty + ",");
            sCheckProjectQty.AppendLine("@InboundId = " + inbound.InboundId + ",");
            sCheckProjectQty.AppendLine("@MCode = '" + inbound.MCode + "',");
            sCheckProjectQty.AppendLine("@ProjectRefNo = '" + inbound.ProjectNo + "'");


            string Query = sCheckProjectQty.ToString();
            DataSet ds = DbUtility.GetDS(Query, this.ConnectionString);



            if (ds.Tables[0].Rows.Count != 0)
            {
                inbound.ReceivedQty = Convert.ToDecimal(ds.Tables[0].Rows[0]["ReceivingQty"].ToString());
                inbound.NormalStock = Convert.ToDecimal(ds.Tables[0].Rows[0]["NormalStock"].ToString());
                inbound.ProjectStock = Convert.ToDecimal(ds.Tables[0].Rows[0]["ProjectStock"].ToString());

            }

          
            
            
                else
                {
                    throw new Exception("Unexpected error, please contact support");
                }
            return inbound;
        }
    }
}

