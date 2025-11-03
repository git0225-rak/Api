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
using Simpolo_Endpoint.Entities;
using Microsoft.Identity.Client;

namespace Simpolo_Endpoint.DAO.HHTServices

{
    public class GroupOBDService : AppDBService, IGroupOBD
    {
        private string _ClassCode = string.Empty;
        public GroupOBDService(IOptions<AppSettings> appSettings) : base(appSettings)
        {

          

        }


        public async Task<List<GroupOutbound>> GetVLPDNosByDock(GroupOutbound outbound)
        {
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                List<GroupOutbound> lGroupOutbound = new List<GroupOutbound>();

                string StoreRefNosQuery = $"EXEC [dbo].[sp_get_vlpdnumberbydock] @warehouseid = {outbound.WareHouseID}, @dock = {outbound.dock}";

                
                var DS =  DbUtility.GetDS(StoreRefNosQuery, this.ConnectionString);

                if (DS?.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in DS.Tables[0].Rows)
                    {
                        lGroupOutbound.Add(new GroupOutbound
                        {
                            Vlpdid = ConversionUtility.ConvertToInt(row["vlpdid"].ToString()),
                            Vlpdnumber = row["Vlpdnumber"].ToString(),
                            IsCustomLabel = row["IsCustomLabel"].ToString()
                        });
                    }
                }
                else
                {
                    throw new WMSExceptionMessage
                    {
                        WMSExceptionCode = ErrorMessages.WMSExceptionCode,
                        WMSMessage = "No Data Found For Given Search Criteria.",
                        ShowAsWarning = true
                    };
                }

                return lGroupOutbound;
            }
            catch (Exception excp)
            {
                ExceptionData oExcpData = new ExceptionData();
                oExcpData.AddInputs("oCriteria", outbound);
                ExceptionHandling.LogException(excp, _ClassCode + "001", oExcpData);
                throw new WMSExceptionMessage
                {
                    WMSExceptionCode = ErrorMessages.WMSExceptionCode,
                    WMSMessage = "No Data Found For Given Search Criteria.",
                    ShowAsCriticalError = true
                };
            }
        }

        public async Task<List<GroupOutbound>> GetZPL_ScriptsforOBDsorting(GroupOutbound outbound)
        {
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                List<GroupOutbound> lGroupOutbound = new List<GroupOutbound>();

                string StoreRefNosQuery = $"EXEC [dbo].[sp_getsortingobds] @accountid = {outbound.AccountID}, @vlpdnumber = {outbound.Vlpdnumber}";


                var DS = DbUtility.GetDS(StoreRefNosQuery, this.ConnectionString);

                if (DS?.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in DS.Tables[0].Rows)
                    {
                        lGroupOutbound.Add(new GroupOutbound
                        {
                            ZplScript =(row["zplscript"].ToString()),
                            SONumber = row["sonumber"].ToString(),
                            OBDNumber = row["OBDNumber"].ToString(),
                            CustomerName = row["CustomerName"].ToString()
                        });
                    }
                }
                else
                {
                    throw new WMSExceptionMessage
                    {
                        WMSExceptionCode = ErrorMessages.WMSExceptionCode,
                        WMSMessage = "No Data Found For Given Search Criteria.",
                        ShowAsWarning = true
                    };
                }

                return lGroupOutbound;
            }
            catch (Exception excp)
            {
                ExceptionData oExcpData = new ExceptionData();
                oExcpData.AddInputs("oCriteria", outbound);
                ExceptionHandling.LogException(excp, _ClassCode + "001", oExcpData);
                throw new WMSExceptionMessage
                {
                    WMSExceptionCode = ErrorMessages.WMSExceptionCode,
                    WMSMessage = "No Data Found For Given Search Criteria.",
                    ShowAsCriticalError = true
                };
            }
        }


        public async Task<List<OutboundModel>> GetItemsAgainstOBD(OutboundModel outbound)
        {
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                List<OutboundModel> lobd = new List<OutboundModel>();

                string StoreRefNosQuery = $"EXEC [dbo].[usp_getitemsforsorting] @OBDNumber = {outbound.OBDNumber}";


                var DS = DbUtility.GetDS(StoreRefNosQuery, this.ConnectionString);

                if (DS?.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in DS.Tables[0].Rows)
                    {
                        lobd.Add(new OutboundModel
                        {
                            OBDNumber = (row["obdnumber"].ToString()),
                            SONumber = row["sonumber"].ToString(),
                            SOQty = (row["soqty"].ToString()),
                            MCode = row["mCode"].ToString()
                        });
                    }
                }
                else
                {
                    throw new WMSExceptionMessage
                    {
                        WMSExceptionCode = ErrorMessages.WMSExceptionCode,
                        WMSMessage = "No Data Found For Given Search Criteria.",
                        ShowAsWarning = true
                    };
                }

                return lobd;
            }
            catch (Exception excp)
            {
                ExceptionData oExcpData = new ExceptionData();
                oExcpData.AddInputs("oCriteria", outbound);
                ExceptionHandling.LogException(excp, _ClassCode + "001", oExcpData);
                throw new WMSExceptionMessage
                {
                    WMSExceptionCode = ErrorMessages.WMSExceptionCode,
                    WMSMessage = "No Data Found For Given Search Criteria.",
                    ShowAsCriticalError = true
                };
            }
        }


        public async Task<List<GroupOutbound>> GetVLPDNos(GroupOutbound outbound)
        {
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                List<GroupOutbound> lGroupOutbound = new List<GroupOutbound>();
              
                string StoreRefNosQuery = $"EXEC [dbo].[Get_OBDNoList] @warehouseid = {outbound.WareHouseID},@AccountID = {outbound.AccountID},@UserId = {outbound.UserId}, @IsVLPD = {outbound.IsVLPD},@TenantId = {outbound.TenantId}";


                var DS = DbUtility.GetDS(StoreRefNosQuery, this.ConnectionString);

                if (DS?.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in DS.Tables[0].Rows)
                    {
                        lGroupOutbound.Add(new GroupOutbound
                        {
                            Vlpdid = ConversionUtility.ConvertToInt(row["vlpdid"].ToString()),
                            Vlpdnumber = row["Vlpdnumber"].ToString(),
                            PickedQty= ConversionUtility.ConvertToInt(row["PickedQty"].ToString()),
                            LoadQty = ConversionUtility.ConvertToInt(row["LoadQty"].ToString()),
                        });
                    }
                }
                else
                {
                    throw new WMSExceptionMessage
                    {
                        WMSExceptionCode = ErrorMessages.WMSExceptionCode,
                        WMSMessage = "No Data Found For Given Search Criteria.",
                        ShowAsWarning = true
                    };


                }

                return lGroupOutbound;
            }
            catch (Exception excp)
            {
                ExceptionData oExcpData = new ExceptionData();
                oExcpData.AddInputs("oCriteria", outbound);
                ExceptionHandling.LogException(excp, _ClassCode + "001", oExcpData);
                throw new WMSExceptionMessage
                {
                    WMSExceptionCode = ErrorMessages.WMSExceptionCode,
                    WMSMessage = "No Data Found For Given Search Criteria.",
                    ShowAsCriticalError = true
                };
            }
        }

        public async Task<List<GroupOutbound>> GetVLPDNosForSorting(GroupOutbound outbound)
        {
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                List<GroupOutbound> lGroupOutbound = new List<GroupOutbound>();

                string StoreRefNosQuery = $"EXEC [dbo].[sp_get_vlpdnumberbydock] @warehouseid = {outbound.WareHouseID},@AccountID = {outbound.AccountID},@UserId = {outbound.UserId},@TenantId = {outbound.TenantId}";


                var DS = DbUtility.GetDS(StoreRefNosQuery, this.ConnectionString);

                if (DS?.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in DS.Tables[0].Rows)
                    {
                        lGroupOutbound.Add(new GroupOutbound
                        {
                            Vlpdid = ConversionUtility.ConvertToInt(row["vlpdid"].ToString()),
                            Vlpdnumber = row["Vlpdnumber"].ToString(),
                           
                        });
                    }
                }
                else
                {
                    throw new WMSExceptionMessage
                    {
                        WMSExceptionCode = ErrorMessages.WMSExceptionCode,
                        WMSMessage = "No Data Found For Given Search Criteria.",
                        ShowAsWarning = true
                    };
                }

                return lGroupOutbound;
            }
            catch (Exception excp)
            {
                ExceptionData oExcpData = new ExceptionData();
                oExcpData.AddInputs("oCriteria", outbound);
                ExceptionHandling.LogException(excp, _ClassCode + "001", oExcpData);
                throw new WMSExceptionMessage
                {
                    WMSExceptionCode = ErrorMessages.WMSExceptionCode,
                    WMSMessage = "No Data Found For Given Search Criteria.",
                    ShowAsCriticalError = true
                };
            }
        }


        public async Task<List<GroupOBDModel>> VLPDOBDItemToPick(GroupOBDModel outbound)
        {
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                List<GroupOBDModel> outboundlist = new List<GroupOBDModel>();

                string StoreRefNosQuery = $"EXEC [dbo].[GEN_GROUPOBDWISEDEATILS_HHT] @vlpdid = {outbound.VLPDId}, @userid = {outbound.UserId}";

                var ds = DbUtility.GetDS(StoreRefNosQuery, this.ConnectionString);

                if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count != 0)
                {
                    if (ds.Tables[0].Rows[0]["S"].ToString().Contains("Error"))
                    {
                        outbound.Result = ds.Tables[0].Rows[0]["S"].ToString();
                        outbound.IsPickingCompleted = DbUtility.GetSqlS("Exec SP_Get_PendingQty_VLPD @VLPDID=" + outbound.VLPDId, this.ConnectionString);
                    }
                    else
                    {

                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            GroupOBDModel newOutbound = new GroupOBDModel
                            {
                                AssignedID = Convert.ToString(row["AssignedID"]),
                                MaterialMasterId = Convert.ToString(row["MaterialMasterID"]),
                                MCode = Convert.ToString(row["MCode"]),
                                MaterialDescription = Convert.ToString(row["MDescription"]),
                                CartonNo = Convert.ToString(row["CartonCode"]),
                                CartonID = Convert.ToString(row["CartonID"]),
                                Location = Convert.ToString(row["Location"]),
                                LocationId = Convert.ToString(row["LocationID"]),
                                MfgDate = Convert.ToString(row["MfgDate"]),
                                ExpDate = Convert.ToString(row["ExpDate"]),
                                SerialNo = Convert.ToString(row["SerialNo"]),
                                BatchNo = Convert.ToString(row["BatchNo"]),
                                ProjectNo = Convert.ToString(row["ProjectRefNo"]),
                                AssignedQuantity = Convert.ToString(row["AssignedQuantity"]),
                                PickedQty = Convert.ToString(row["PickedQty"]),
                                OutboundID = Convert.ToString(row["OutboundID"]),
                                SODetailsID = Convert.ToString(row["SODetailsID"]),
                                SLocId = Convert.ToString(row["StorageLocationID"]),
                                SLoc = Convert.ToString(row["SLOC"]),
                                GoodsmomentDeatilsId = Convert.ToString(row["GoodsMovementDetailsID"]),
                                Lineno = Convert.ToString(row["LineNumber"]),
                                MaterialMaster_IUoMID = Convert.ToString(row["MaterialMaster_SUoMID"]),
                                CF = Convert.ToString(row["CF"]),
                                POSOHeaderId = Convert.ToString(row["SOHeaderID"]),
                                PendingQty = Convert.ToString(row["PendingQty"]),
                                MRP = Convert.ToString(row["MRP"]),
                                HUNo = Convert.ToString(row["HUNo"]),
                                HUSize = Convert.ToString(row["HUSize"]),
                                UOM = Convert.ToString(row["UoM"]),
                                GradeID = Convert.ToString(row["GradeID"]),
                                Grade = Convert.ToString(row["Grade"]),
                                Result = Convert.ToString(row["S"]),
                                OBDNumber = Convert.ToString(row["OBDNumber"])
                                
                            };

                            outboundlist.Add(newOutbound);
                        }
                    }
                }
                else
                {
                    outbound.Result = "Success";
                    outbound.PendingQty = "0";
                    outbound.IsPickingCompleted = DbUtility.GetSqlS("Exec SP_Get_PendingQty_VLPD @VLPDID=" + outbound.VLPDId, this.ConnectionString);

                    outboundlist.Add(outbound);
                }

                return outboundlist;
            }
            catch (Exception ex)
            {

                throw;
            }
        }




        public GroupOBDModel VLPDPickedItems(GroupOBDModel outbound)
        {

            try
            {

                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                List<GroupOutbound> lGroupOutbound = new List<GroupOutbound>();

                int IsRpckplanttoPlantOrder=DbUtility.GetSqlN("Exec GetVLPDGroupType @VLPDNumber='"+outbound.VLPDNumber+"'",this.ConnectionString);
                string StoreRefNosQuery = "";

                if (IsRpckplanttoPlantOrder==1)
                {
                     StoreRefNosQuery = " EXEC sp_INV_GROUPOBD_PickItemFromBin_RPCK_P2P @VLPDNumber  ='" + outbound.VLPDNumber + "',@LineNumber = '" + outbound.Lineno + "',@SOHeaderID = '" + outbound.SOHeaderID + "',@MCode= '" + outbound.MCode + "',@Location = '" + outbound.Location + "',@GoodsMovementTypeID = 0,@Quantity = " + outbound.PickedQty + ",@GradeID = " + outbound.GradeID + ",@IsDamaged = 0,@LastModifiedBy = " + outbound.UserId + ",@HasDiscrepancy= 0,@CreatedBy = " + outbound.UserId + ",@Mfgdate = '" + outbound.MfgDate + "',@ExpDate = '" + outbound.ExpDate + "',@BatchNo=  '" + outbound.BatchNo + "',@SerialNo= '" + outbound.SerialNo + "',@Projrefno = '" + outbound.ProjectNo + "',@CartonCode= '" + outbound.CartonNo + "',@ToCartonCode= '" + outbound.ToCartonNo + "',@AccountID= " + outbound.AccountID + ",@SoDetailsIdnew= '" + outbound.SODetailsID + "',@AssignedId= " + outbound.AssignedID + ",@MRP= '" + outbound.MRP + "',@HUSize= '" + outbound.HUSize + "',@HUNo= '" + outbound.HUNo + "',@SkipReason='" + outbound.SkipReason + "',@IsSkip='" + outbound.IsSkip + "',@SkipReasonID='" + outbound.SkipReasonID + "',@SkipLocation='" + outbound.SkipLocation + "',@OutboundID='"+outbound.OutboundID+"',@IsPSN = 0,@PSN  = '',@IsExcess = 0,@SLOC='"+outbound.SLoc+"'";

                }
                else
                {
                     StoreRefNosQuery = " EXEC sp_INV_GROUPOBD_PickItemFromBin @VLPDNumber  ='" + outbound.VLPDNumber + "',@LineNumber = '" + outbound.Lineno + "',@SOHeaderID = '" + outbound.SOHeaderID + "',@MCode= '" + outbound.MCode + "',@Location = '" + outbound.Location + "',@GoodsMovementTypeID = 0,@Quantity = " + outbound.PickedQty + ",@GradeID = " + outbound.GradeID + ",@IsDamaged = 0,@LastModifiedBy = " + outbound.UserId + ",@HasDiscrepancy= 0,@CreatedBy = " + outbound.UserId + ",@Mfgdate = '" + outbound.MfgDate + "',@ExpDate = '" + outbound.ExpDate + "',@BatchNo=  '" + outbound.BatchNo + "',@SerialNo= '" + outbound.SerialNo + "',@Projrefno = '" + outbound.ProjectNo + "',@CartonCode= '" + outbound.CartonNo + "',@ToCartonCode= '" + outbound.ToCartonNo + "',@AccountID= " + outbound.AccountID + ",@SoDetailsIdnew= '" + outbound.SODetailsID + "',@AssignedId= " + outbound.AssignedID + ",@MRP= '" + outbound.MRP + "',@HUSize= '" + outbound.HUSize + "',@HUNo= '" + outbound.HUNo + "',@SkipReason='" + outbound.SkipReason + "',@IsSkip='" + outbound.IsSkip + "',@SkipReasonID='" + outbound.SkipReasonID + "',@SkipLocation='" + outbound.SkipLocation + "',@OutboundID='"+outbound.OutboundID+ "',@IsPSN = 0,@PSN  = '',@IsExcess = 0,@SLOC='"+outbound.SLoc+"'";

                }

                var ds = DbUtility.GetDS(StoreRefNosQuery, this.ConnectionString);

                if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count != 0)
                {

                    int result = ConversionUtility.ConvertToInt(ds.Tables[0].Rows[0]["N"].ToString());

                    if (result == -999)
                    {

                    }
                    else if (result == 1)
                    {
                        outbound.Result = "";
                        outbound.Message = ds.Tables[0].Rows[0]["S"].ToString();
                        outbound.SortingLocaton= ds.Tables[0].Rows[0]["SortingLocation"].ToString();

                         VLPDOBDItemToPick(outbound);
                    }

                    else if (result == 2)
                    {
                        outbound.Result = "Error : Stock not Available";
                    }
                    else if (result == 5)
                    {
                        outbound.Result = "Error : From Pallet and To Pallet should not be same";
                    }
                    else if (result == -111)
                    {
                        outbound.Result = "Error : The PSN you are trying to scan is already picked";
                    }
                    else
                    {
                        outbound.Result = "Error : Process failed,Please Contact Support team";
                    }


                }
               

                return outbound;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<List<OutboundModel>> GetOBDSuggestionForSorting(OutboundModel outbound)
        {
            List<OutboundModel> _OutboundModel = new List<OutboundModel>();
            try
            {

                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                List<OutboundModel> lGroupOutbound = new List<OutboundModel>();

                string StoreRefNosQuery = " EXEC USP_GET_SUGGESTIONSLISTFORSORTING  @vlpdid  =" + outbound.VLPDId + ",@userid = " + outbound.UserId + ",@mcode = '" + outbound.MCode + "',@Grade = '" + outbound.Grade + "',@Mfgdate = '" + outbound.MfgDate + "',@ExpDate = '" + outbound.ExpDate + "',@BatchNo=  '" + outbound.BatchNo + "',@SerialNo= '" + outbound.SerialNo + "',@projectref = '" + outbound.ProjectNo + "',@MRP= '" + outbound.MRP + "',@husize= " + outbound.HUSize + ",@huno=" + outbound.HUNo;


                var ds = DbUtility.GetDS(StoreRefNosQuery, this.ConnectionString);

                if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count != 0)
                {


                    if (ds.Tables[0].Rows[0][0].ToString() == "-1")
                    {
                        OutboundModel _oOutbound = new OutboundModel()
                        {
                            VLPDId = outbound.VLPDId,
                            Result = "Invalid SKU scaned in sorting zone"
                        };
                        _OutboundModel.Add(_oOutbound);
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "-2")
                    {
                        OutboundModel _oOutbound = new OutboundModel()
                        {
                            VLPDId = outbound.VLPDId,
                            Result = "Stock not available in sorting location"
                        };
                        _OutboundModel.Add(_oOutbound);

                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "-3")
                    {
                        OutboundModel _oOutbound = new OutboundModel()
                        {
                            VLPDId = outbound.VLPDId,
                            Result = "No pending qty for sorting"
                        };
                        _OutboundModel.Add(_oOutbound);
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "-4")
                    {
                        OutboundModel _oOutbound = new OutboundModel()
                        {
                            VLPDId = outbound.VLPDId,
                            Result = "Invalid SKU scaned in sorting zone"
                        };
                        _OutboundModel.Add(_oOutbound);
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "-5")
                    {
                        OutboundModel _oOutbound = new OutboundModel()
                        {
                            VLPDId = outbound.VLPDId,
                            Result = "No pending qty for sorting"
                        };
                        _OutboundModel.Add(_oOutbound);
                    }

                    else if (ds.Tables[0].Rows[0][0].ToString() == "-6")
                    {
                        OutboundModel _oOutbound = new OutboundModel()
                        {
                            VLPDId = outbound.VLPDId,
                            Result = "Sorting is completed for this Batch"
                        };
                        _OutboundModel.Add(_oOutbound);
                    }
                    else
                    {
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            OutboundModel _oOutbound = new OutboundModel()
                            {
                                Result = "Done",
                                OBDNumber = Convert.ToString(row["obdnumber"]),
                                MCode = Convert.ToString(row["mcode"]),
                                // Lineno = Convert.ToString(row["lineumber"]),
                                Location = Convert.ToString(row["location"]),
                                SortingQty = Convert.ToString(row["pendingsortqty"]),
                                VehicleNo = row["VehicleNo"].ToString()
                               
                            };
                            _OutboundModel.Add(_oOutbound);
                        }
                    }


                }
                else
                {


                     OutboundModel _oOutbound = new OutboundModel()
                        {
                            VLPDId = outbound.VLPDId,
                            Result = "No suggestions found"
                     };
                        _OutboundModel.Add(_oOutbound);


                }


                return _OutboundModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }




        public async Task<List<OutboundModel>> GetLocationSuggestionForSorting(OutboundModel outbound)
        {
            List<OutboundModel> _OutboundModel = new List<OutboundModel>();
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                List<OutboundModel> lGroupOutbound = new List<OutboundModel>();

                string StoreRefNosQuery = " EXEC USP_GET_LocationSUGGESTIONSLISTFORSORTING  @vlpdid  =" + outbound.VLPDId + ",@userid = " + outbound.UserId + ",@mcode = '" + outbound.MCode + "',@OBDNumber = '" + outbound.OBDNumber + "',@Mfgdate = '" + outbound.MfgDate + "',@ExpDate = '" + outbound.ExpDate + "',@BatchNo=  '" + outbound.BatchNo + "',@SerialNo= '" + outbound.SerialNo + "',@projectref = '" + outbound.ProjectNo + "',@MRP= '" + outbound.MRP + "',@husize= " + outbound.HUSize + ",@huno=" + outbound.HUNo;


                var ds = DbUtility.GetDS(StoreRefNosQuery, this.ConnectionString);

                if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count != 0)
                {



                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        OutboundModel _oOutbound = new OutboundModel()
                        {

                            CustomerName = Convert.ToString(row["CustomerName"]),
                            Location = Convert.ToString(row["location"]),
                            Message = Convert.ToString(row["Message"])

                        };
                        _OutboundModel.Add(_oOutbound);
                    }


                }
                else
                {

                    OutboundModel _oOutbound = new OutboundModel()
                    {

                          Result = "No suggestions found"

                    };
                    _OutboundModel.Add(_oOutbound);
                }
                return _OutboundModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public async Task<OutboundModel> UpsertOBDSorting(OutboundModel outbound)
        {
            OutboundModel _vlpdmodel = new OutboundModel();
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                string WORevertQuery = " EXEC usp_obd_sortingitems  @vlpdid  =" + outbound.VLPDId + ",@outboundno = '" + outbound.OBDNumber + "',@mcode = '" + outbound.MCode + "',@Grade = '" + outbound.Grade + "',@sortingqty = " + outbound.SortingQty + ",@tenantid=" + outbound.TenantID + ",@accountid=" + outbound.AccountID + ",@husize= " + outbound.HUSize + ",@huno=" + outbound.HUNo + ",@Mfgdate = '" + outbound.MfgDate + "',@ExpDate = '" + outbound.ExpDate + "',@BatchNo=  '" + outbound.BatchNo + "',@SerialNo= '" + outbound.SerialNo + "',@projectrefno = '" + outbound.ProjectNo + "',@location='" + outbound.Location + "',@pickserialnumber='" + outbound.PickSerialNumber + "',@warehouseid=" + outbound.WareHouseID + ",@userid=" + outbound.UserId;

                var _dsPickLists = DbUtility.GetDS(WORevertQuery, this.ConnectionString);

                if (_dsPickLists != null && _dsPickLists.Tables.Count != 0 && _dsPickLists.Tables[0].Rows.Count != 0)
                {
                    string Result = "";
                    foreach (DataRow _dtPack in _dsPickLists.Tables[0].Rows)
                    {
                        Result = _dtPack["S"].ToString();
                    }
                    outbound.Result = Result;
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




    }
}
