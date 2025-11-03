using Simpolo_Endpoint.DAO.HHTInterface;
using Simpolo_Endpoint.DAO.Services;
using Simpolo_Endpoint.DBUtil;
using Simpolo_Endpoint.DTO;
using Simpolo_Endpoint.ModelsLibrary;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMSCore_BusinessEntities.Entities;

namespace Simpolo_Endpoint.DAO.HHTServices
{
    public class ScanService : AppDBService, IScan
    {
        public ScanService(IOptions<AppSettings> appSettings) : base(appSettings)
        {

        }
        private string _ClassCode = string.Empty;
#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<ScannedItem> ValidateLocation(ScannedItem obj)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();

                string WarehouseID = string.IsNullOrEmpty(obj.WarehouseID) ? "" : obj.WarehouseID;

                string ValidateLocQuery = "EXEC [dbo].[sp_Validate_Location] @LocationCode = " + DBLibrary.SQuote(obj.ScanInput) + ",@WarehouseID = " + DBLibrary.SQuote(WarehouseID) + ",@InboundID = " + DBLibrary.SQuote(obj.InboundID) + ",@OBDNumber = " + DBLibrary.SQuote(obj.ObdNumber) + ",@VLPDNumber = " + DBLibrary.SQuote(obj.VlpdNumber) + ",@IsCycleCount = " + obj.IsCycleCount + ",@UserID = " + obj.UserID + "";
                var _dsResults = DbUtility.GetDS(ValidateLocQuery, this.ConnectionString);

                if (_dsResults != null && _dsResults.Tables.Count != 0 && _dsResults.Tables[0].Rows.Count != 0)
                {
                    if (Convert.ToString(_dsResults.Tables[0].Rows[0][0]) == "-2")
                    {
                        obj.ScanResult = false;
                        obj.Message = "Please Scan Empty Location";
                        throw new WMSExceptionMessage() { WMSExceptionCode = "WMC_DAL_SCAN_0005", WMSMessage = obj.Message, ShowAsError = true };
                    }
                    else if (Convert.ToString(_dsResults.Tables[0].Rows[0][0]) == "-1")
                    {
                        obj.ScanResult = false;
                        obj.Message = "Scanned Location is Blocked For Cycle Count";
                        throw new WMSExceptionMessage() { WMSExceptionCode = "WMC_DAL_SCAN_0004", WMSMessage = ErrorMessages.WMC_DAL_SCAN_0004, ShowAsError = true };             
                    }
                    else
                    {
                        obj.ScanResult = true;
                    }
                }
                else
                {
                    obj.ScanResult = false;
                    obj.Message = "Invalid location scanned";
                    throw new WMSExceptionMessage() { WMSExceptionCode = "WMC_DAL_SCAN_0002", WMSMessage = ErrorMessages.WMC_DAL_SCAN_0002, ShowAsError = true };
                }
                return obj;
            }
            catch (WMSExceptionMessage excp)
            {
                throw excp;
            }
            catch (Exception excp)
            {
                ExceptionData oExcpData = new ExceptionData();
                oExcpData.AddInputs("ScannedItem", obj);

                ExceptionHandling.LogException(excp, _ClassCode + "002", oExcpData);
                throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = ErrorMessages.WMSExceptionMessage, ShowAsCriticalError = true };
            }
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<ScannedItem> ValidatePallet(ScannedItem obj)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();

                string WarehouseID = string.IsNullOrEmpty(obj.WarehouseID) ? "" : obj.WarehouseID;

                string ValidateLocQuery = "EXEC [dbo].[sp_Validate_Container] @CartonCode = " + DBLibrary.SQuote(obj.ScanInput) + ",@WarehouseID = " + DBLibrary.SQuote(WarehouseID) + ",@InboundID = " + DBLibrary.SQuote(obj.InboundID) + ",@OBDNumber = " + DBLibrary.SQuote(obj.ObdNumber) + ",@VLPDNumber = " + DBLibrary.SQuote(obj.VlpdNumber) + ",@UserID = " + obj.UserID + "";
                var _dsResults = DbUtility.GetDS(ValidateLocQuery, this.ConnectionString);


                if (_dsResults != null && _dsResults.Tables.Count != 0 && _dsResults.Tables[0].Rows.Count != 0)
                {
                    if (Convert.ToString(_dsResults.Tables[0].Rows[0][0]) == "-2")
                    {
                        obj.ScanResult = false;
                        obj.Message = "Please Scan Empty Location";
                        throw new WMSExceptionMessage() { WMSExceptionCode = "WMC_DAL_SCAN_0005", WMSMessage = obj.Message, ShowAsError = true };
                    }
                    else if (Convert.ToString(_dsResults.Tables[0].Rows[0][0]) == "-3")
                    {
                        obj.ScanResult = false;
                        obj.Message = "Pallet was already used";
                        throw new WMSExceptionMessage() { WMSExceptionCode = "WMC_DAL_SCAN_0005", WMSMessage = obj.Message, ShowAsError = true };
                    }
                    else if (Convert.ToString(_dsResults.Tables[0].Rows[0][0]) == "-1")
                    {
                        obj.ScanResult = false;
                        obj.Message = "Scanned Location is Blocked For Cycle Count";
                        throw new WMSExceptionMessage() { WMSExceptionCode = "WMC_DAL_SCAN_0004", WMSMessage = obj.Message, ShowAsError = true };
                    }
                    else
                    {
                        obj.Message = _dsResults.Tables[0].Rows[0][4].ToString();
                        obj.AvailableQty = Convert.ToInt64(_dsResults.Tables[0].Rows[0][3]);
                        obj.ScanResult = true;
                    }
                }
                else
                {
                    obj.ScanResult = false;
                    obj.Message = "Invalid Pallet scanned";
                    throw new WMSExceptionMessage() { WMSExceptionCode = "WMC_DAL_SCAN_0001", WMSMessage = ErrorMessages.WMC_DAL_SCAN_0001, ShowAsError = true };
                }
                return obj;
            }
            catch (WMSExceptionMessage excp)
            {
                throw excp;
            }
            catch (Exception excp)
            {
                ExceptionData oExcpData = new ExceptionData();
                oExcpData.AddInputs("ScannedItem", obj);

                ExceptionHandling.LogException(excp, _ClassCode + "003", oExcpData);
                throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = ErrorMessages.WMSExceptionMessage, ShowAsCriticalError = true };
            }
        }

        public async Task<ScannedItem> ValidateSKU(ScannedItem obj)
        {
            try
            {
                if (obj.ScanInput?.Split('|').Length == 9)
                {
                    obj.SkuCode = obj.ScanInput?.Split('|')[0];
                    obj.Batch = obj.ScanInput?.Split('|')[1];
                    obj.SerialNumber = "";
                    obj.LabelSerialNumber = "";
                    //obj.MfgDate = obj.ScanInput?.Split('|')[2];
                    string RCVDate = obj.ScanInput?.Split('|')[2];
                    obj.MfgDate = getFormattedDate(RCVDate);
                    // obj.MfgDate = DateTime.TryParseExact(RCVDate, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out _) ? RCVDate : (DateTime.TryParse(RCVDate, out DateTime tempDate) ? tempDate.ToString("yyyy-MM-dd") : "");
                    //obj.ExpDate = obj.ScanInput?.Split('|')[3];
                    string EXDate = obj.ScanInput?.Split('|')[3];
                    //obj.ExpDate = DateTime.TryParseExact(EXDate, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out _) ? EXDate : (DateTime.TryParse(EXDate, out DateTime tempDate1) ? tempDate1.ToString("yyyy-MM-dd") : "");
                    obj.ExpDate = getFormattedDate(EXDate);
                    obj.PrjRef = obj.ScanInput?.Split('|')[5];
                    obj.KitID = obj.ScanInput?.Split('|')[4];
                    obj.Grade = obj.ScanInput?.Split('|')[6];
                    obj.LineNumber = obj.ScanInput?.Split('|')[7];
                    obj.HUSize = 0;
                    obj.HUNo = 0;
         
                 
                }

                else if (obj.ScanInput?.Split('|').Length == 7)
                {
                    obj.SkuCode = obj.ScanInput?.Split('|')[0];
                    var batchgrade = obj.ScanInput?.Split('|')[3];
                    obj.Batch = batchgrade.Split('-')[0];
                    obj.LabelSerialNumber = "";
                    obj.SerialNumber = "";
                    string RCVDate = obj.ScanInput?.Split('|')[2];
                    obj.MfgDate = RCVDate;
                    //string EXDate = obj.ScanInput?.Split('|')[4];
                    //obj.ExpDate = EXDate; 
                    obj.ExpDate = "";
                    obj.PrjRef = "";
                    obj.KitID = "";
                    obj.Grade = batchgrade.Split('-')[1];
                    obj.LineNumber = "";
                    obj.HUNo = 1;
                    obj.HUSize = 1;

                }

                else if (obj.ScanInput?.Split('|').Length == 6)
                {
                    obj.SkuCode = obj.ScanInput?.Split('|')[0];
                    var batchgrade = obj.ScanInput?.Split('|')[3];
                    obj.Batch = batchgrade.Split('-')[0];
                    obj.LabelSerialNumber = "";
                    obj.SerialNumber = "";
                    string RCVDate = obj.ScanInput?.Split('|')[2];
                    obj.MfgDate = RCVDate;
                    //string EXDate = obj.ScanInput?.Split('|')[4];
                    //obj.ExpDate = EXDate; 
                    obj.ExpDate = "";
                    obj.PrjRef = "";
                    obj.KitID = "";
                    obj.Grade = batchgrade.Split('-')[1];
                    obj.LineNumber = "";
                    obj.HUNo = 1;
                    obj.HUSize = 1;

                }



                else if (obj.ScanInput?.Split('|').Length == 11)
                {
                    obj.SkuCode = obj.ScanInput?.Split('|')[0];
                    obj.Batch = obj.ScanInput?.Split('|')[1];
                    obj.LabelSerialNumber = obj.ScanInput?.Split('|')[2];
                    obj.SerialNumber = "";
                    //obj.MfgDate = obj.ScanInput?.Split('|')[3];

                    //string RCVDate = obj.ScanInput?.Split('|')[3];
                    //string[] expectedFormats = { "yyyy-MM-dd", "dd-MM-yyyy", "MM-dd-yyyy", "yyyyMMdd", "ddMMyyyy" };

                    //DateTime tempDate;
                    //bool isValidDate = DateTime.TryParseExact(RCVDate, expectedFormats, null, System.Globalization.DateTimeStyles.None, out tempDate);
                    //obj.MfgDate = isValidDate ? tempDate.ToString("yyyy-MM-dd") : "";

                    string RCVDate = obj.ScanInput?.Split('|')[3];
                    // obj.MfgDate = getFormattedDate(RCVDate);   // Commented for label format issue by abhishek.g

                    obj.MfgDate = RCVDate;

                    //obj.MfgDate = DateTime.TryParseExact(RCVDate, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out _) ? RCVDate : (DateTime.TryParse(RCVDate, out DateTime tempDate) ? tempDate.ToString("yyyy-MM-dd") :"");
                    //obj.ExpDate = obj.ScanInput?.Split('|')[4];
                    string EXDate = obj.ScanInput?.Split('|')[4];
                    //obj.ExpDate = getFormattedDate(EXDate);
                    obj.ExpDate = EXDate; // Commented for label format issue by abhishek.g


                    //obj.ExpDate = DateTime.TryParseExact(EXDate, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out _) ? EXDate : (DateTime.TryParse(EXDate, out DateTime tempDate1) ? tempDate1.ToString("yyyy-MM-dd") : "");
                    obj.PrjRef = obj.ScanInput?.Split('|')[5];
                    obj.KitID = obj.ScanInput?.Split('|')[6];
                    obj.Grade = obj.ScanInput?.Split('|')[7];
                    obj.LineNumber = obj.ScanInput?.Split('|')[8];
                    obj.HUNo = Convert.ToInt32(obj.ScanInput?.Split('|')[9] == "" ? "1" : obj.ScanInput?.Split('|')[9]);
                    obj.HUSize = Convert.ToInt32(obj.ScanInput?.Split('|')[10] == "" ? "1" : obj.ScanInput?.Split('|')[10]);
                   
                }
                else if (obj.ScanInput?.Split('|').Length == 5)
                {
                    obj.SkuCode = obj.ScanInput?.Split('|')[0];
                    obj.Batch = obj.ScanInput?.Split('|')[1];
                    obj.SerialNumber = "";
                    obj.LabelSerialNumber = obj.ScanInput?.Split('|')[2];
                    obj.KitID = obj.ScanInput?.Split('|')[3];
                    obj.LineNumber = obj.ScanInput?.Split('|')[4];
                    obj.MfgDate = string.Empty;
                    obj.ExpDate = string.Empty;
                    obj.PrjRef = string.Empty;
                    obj.Grade = string.Empty;
                    obj.HUSize = 0;
                    obj.HUNo = 0;
                }
                else if (obj.ScanInput?.Split('|').Length == 1)
                {
                    obj.SkuCode = obj.ScanInput?.Split('|')[0];
                    obj.Batch = string.Empty;
                    obj.LabelSerialNumber = string.Empty;
                    obj.SerialNumber = string.Empty;
                    obj.MfgDate = string.Empty;
                    obj.ExpDate = string.Empty;
                    obj.PrjRef = string.Empty;
                    obj.KitID = string.Empty;
                    obj.Grade = string.Empty;
                    obj.LineNumber = string.Empty;
                    obj.HUSize = 0;
                    obj.HUNo = 0;
                }
                else if (obj.InboundID != "0" && obj.InboundID != "")
                {
                    if (obj.ScanInput?.Split('|').Length == 2)
                    {
                        obj.SkuCode = obj.ScanInput?.Split('|')[0];
                        obj.SupplierInvoiceDetailsId = obj.ScanInput?.Split('|')[1];
                        obj.Batch = string.Empty;
                        obj.SerialNumber = string.Empty;
                        obj.LabelSerialNumber = string.Empty;
                        obj.MfgDate = string.Empty;
                        obj.ExpDate = string.Empty;
                        obj.PrjRef = string.Empty;
                        obj.KitID = string.Empty;
                        obj.Grade = string.Empty;
                        obj.LineNumber = string.Empty;
                        obj.HUSize = 0;
                        obj.HUNo = 0;
                    }
                    if (obj.ScanInput?.Split('|').Length == 10)
                    {
                        obj.SkuCode = obj.ScanInput?.Split('|')[0];
                        obj.Batch = obj.ScanInput?.Split('|')[1];
                        obj.SerialNumber = string.Empty;
                        obj.LabelSerialNumber = string.Empty;
                        //obj.MfgDate = obj.ScanInput?.Split('|')[2];
                        // obj.ExpDate = obj.ScanInput?.Split('|')[3];                   
                        string RCVDate = obj.ScanInput?.Split('|')[2];
                        obj.MfgDate = getFormattedDate(RCVDate);
                            //DateTime.TryParseExact(RCVDate, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out _) ? RCVDate : (DateTime.TryParse(RCVDate, out DateTime tempDate) ? tempDate.ToString("yyyy-MM-dd") : "");
                        string EXDate = obj.ScanInput?.Split('|')[3];
                        obj.ExpDate = getFormattedDate(EXDate);
                        //DateTime.TryParseExact(EXDate, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out _) ? EXDate : (DateTime.TryParse(EXDate, out DateTime tempDate1) ? tempDate1.ToString("yyyy-MM-dd") : "");
                        obj.PrjRef = obj.ScanInput?.Split('|')[4];
                        obj.KitID = obj.ScanInput?.Split('|')[5];
                        obj.Grade = obj.ScanInput?.Split('|')[6];
                        obj.LineNumber = obj.ScanInput?.Split('|')[7];
                        obj.HUNo = Convert.ToInt32(obj.ScanInput?.Split('|')[8] == "" ? "1" : obj.ScanInput?.Split('|')[9]);
                        obj.HUSize = Convert.ToInt32(obj.ScanInput?.Split('|')[9] == "" ? "1" : obj.ScanInput?.Split('|')[9]);
                    }
                   
                    else
                    {
                        throw new WMSExceptionMessage() { WMSExceptionCode = "WMC_DAL_SCAN_0003", WMSMessage = ErrorMessages.WMC_DAL_SCAN_0003, ShowAsError = true };
                    }
                }
                else
                {
                    throw new WMSExceptionMessage() { WMSExceptionCode = "WMC_DAL_SCAN_0003", WMSMessage = ErrorMessages.WMC_DAL_SCAN_0003, ShowAsError = true };
                }

                return await ValidateItem(obj); ;
            }
            catch (WMSExceptionMessage excp)
            {
                throw excp;
            }
            catch (Exception excp)
            {
                ExceptionData oExcpData = new ExceptionData();
                oExcpData.AddInputs("obj", obj);

                ExceptionHandling.LogException(excp, _ClassCode + "003", oExcpData);
                throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = ErrorMessages.WMSExceptionMessage, ShowAsCriticalError = true };
            }
        }
        private string getFormattedDate(string datefromLable)
        {
            string[] dateFormats = { "yyyy-MM-dd", "MM/dd/yyyy", "dd-MM-yyyy", "yyyyMMdd", "dd-MMM-yyyy" };
            bool parsed = false;
            DateTime parsedDate = DateTime.MinValue;

            foreach (string format in dateFormats)
            {
                if (DateTime.TryParseExact(datefromLable, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
                {
                    parsed = true;
                    break;
                }
            }
            return parsed ? parsedDate.ToString("yyyy-MM-dd") : "";
        }


#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<ScannedItem> ValidateItem(ScannedItem obj)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();

                string TenantID = string.IsNullOrEmpty(obj.TenantID) ? "''" : obj.TenantID;

                string ValidateKUQuery = "EXEC [dbo].[sp_Validate_Material] @Mcode = " + DBLibrary.SQuote(obj.SkuCode) + ",@TenantID = " + TenantID + ",@InboundID = " + DBLibrary.SQuote(obj.InboundID) + ",@OBDNumber = " + DBLibrary.SQuote(obj.ObdNumber) + ",@VLPDNumber = " + DBLibrary.SQuote(obj.VlpdNumber) + "";
                var _dsResults = DbUtility.GetDS(ValidateKUQuery, this.ConnectionString);
             
                if (_dsResults != null && _dsResults.Tables.Count != 0 && _dsResults.Tables[0].Rows.Count != 0)
                {
                    obj.ScanResult = true;
                    obj.MDescription = _dsResults.Tables[0].Rows[0]["MDescription"].ToString();
                }
                else
                {
                    obj.ScanResult = false;
                    obj.Message = "Invalid material scanned";
                    throw new WMSExceptionMessage() { WMSExceptionCode = "WMC_DAL_SCAN_0003", WMSMessage = ErrorMessages.WMC_DAL_SCAN_0003, ShowAsError = true };
                }
                return obj;
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
        public async Task<ScannedItem> ValidateCarton(ScannedItem obj)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();

                StringBuilder sbSqlStrings = new StringBuilder();
                sbSqlStrings.AppendLine("EXEC [dbo].[USP_API_ValidateCarton] ");
                sbSqlStrings.AppendLine("@CartonNumber = " + DBLibrary.SQuote(obj.ScanInput) + ",");
                sbSqlStrings.AppendLine("@SONumber = " + DBLibrary.SQuote(obj.ObdNumber) + ",");
                sbSqlStrings.AppendLine("@AccountID = " + obj.AccountID + "");

                string Query = sbSqlStrings.ToString();
                DataSet _dsResults = DbUtility.GetDS(Query, this.ConnectionString);

                if (_dsResults != null && _dsResults.Tables.Count != 0 && _dsResults.Tables[0].Rows.Count != 0)
                {
                    obj.ScanResult = true;
                }
                else
                {
                    obj.ScanResult = false;
                    obj.Message = "Invalid carton scanned";
                    throw new WMSExceptionMessage() { WMSExceptionCode = "WMC_DAL_SCAN_0001", WMSMessage = ErrorMessages.WMC_DAL_SCAN_0001, ShowAsError = true };
                }
                return obj;
            }
            catch (WMSExceptionMessage excp)
            {
                throw excp;
            }
            catch (Exception excp)
            {
                ExceptionData oExcpData = new ExceptionData();
                oExcpData.AddInputs("ScannedItem", obj);

                ExceptionHandling.LogException(excp, _ClassCode + "004", oExcpData);
                throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = ErrorMessages.WMSExceptionMessage, ShowAsCriticalError = true };
            }
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<ScannedItem> ValidateSO(ScannedItem obj)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();

                StringBuilder sbSqlStrings = new StringBuilder();
                sbSqlStrings.AppendLine("EXEC [dbo].[USP_Validate_SalesOrder] ");
                sbSqlStrings.AppendLine("@SOOrderNo = " + DBLibrary.SQuote(obj.ScanInput) + ",");
                sbSqlStrings.AppendLine("@AccountID = " + obj.AccountID + ",");
                sbSqlStrings.AppendLine("@UserId = " + obj.UserID + "");

                string Query = sbSqlStrings.ToString();
                DataSet _dsResults = DbUtility.GetDS(Query, this.ConnectionString);   

                if (_dsResults != null && _dsResults.Tables.Count != 0 && _dsResults.Tables[0].Rows.Count != 0)
                {
                    obj.ScanResult = true;
                }
                else
                {
                    obj.ScanResult = false;
                    obj.Message = "Packing Not Yet Started for SO Number.";
                    throw new WMSExceptionMessage() { WMSExceptionCode = "WMC_DAL_SCAN_0001", WMSMessage = obj.Message, ShowAsError = true };
                }

                return obj;
            }
            catch (WMSExceptionMessage excp)
            {
                throw excp;
            }
            catch (Exception excp)
            {
                ExceptionData oExcpData = new ExceptionData();
                oExcpData.AddInputs("ScannedItem", obj);

                ExceptionHandling.LogException(excp, _ClassCode + "004", oExcpData);
                throw new WMSExceptionMessage() { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = ErrorMessages.WMSExceptionMessage, ShowAsCriticalError = true };
            }
        }
    }
}
