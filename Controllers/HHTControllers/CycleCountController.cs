using FWMSC21Core.Entities;
using Simpolo_Endpoint.DAO.HHTInterface;
using Simpolo_Endpoint.DTO;
using Simpolo_Endpoint.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Simpolo_Endpoint.Controllers.HHTControllers
{
    [Route("[controller]")]
    //[Authorize]
    [ApiController]
    public class CycleCountController : ControllerBase
    {
        private readonly ICycleCount _CycleCount;
        //BaseController baseController = new BaseController();
        JsonSettings jsonSettings = new JsonSettings();
        public CycleCountController(ICycleCount cycleCount)
        {
            _CycleCount = cycleCount;
            _ClassCode = ExceptionHandling.GetClassExceptionCode(ExceptionHandling.ExcpConstants_API_Enpoint.CycleCountController);
        }

        private string _ClassCode = string.Empty;

        [HttpPost]
        [Route("GetCCNames")]
        public async Task<string> GetCCNames(WMSCoreMessage oRequest)
        {
            try
            {
                DataSet userDataTable = null;

                List<CycleCountDTO> _lstcyclecount = new List<CycleCountDTO>();

                if (oRequest != null)
                {

                    CycleCountDTO _ocyclecountDTO = (CycleCountDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);

                    if (_ocyclecountDTO != null)
                    {

                        BO.CycleCount ocycleCount = new BO.CycleCount()
                        {
                            AccountId = _ocyclecountDTO.AccountID,
                            UserId = Convert.ToInt32(_ocyclecountDTO.UserId)
                        };

                        userDataTable = await _CycleCount.GetCCNames(ocycleCount);
                    }
                    foreach (System.Data.DataRow row in userDataTable.Tables[0].Rows)
                    {
                        CycleCountDTO responseDTO = new CycleCountDTO();
                        responseDTO.CCName = row["CCHeader"].ToString();
                        responseDTO.WarehouseID = row["WarehouseID"].ToString();
                        responseDTO.TenantId = row["TenantID"].ToString();
                        responseDTO.Rack = row["Rack"].ToString();
                        responseDTO.Column = row["Column"].ToString();
                        responseDTO.Level = row["Level"].ToString();
                        responseDTO.CycleCountSeqCode = row["CycleCountCode"].ToString();
                        _lstcyclecount.Add(responseDTO);
                    }
                   
                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.CycleCount, _lstcyclecount), jsonSettings.JsonSerializerSettings));
                    return json;
                }
                else return null;
            }
            catch (WMSExceptionMessage ex)
            {
                List<WMSExceptionMessage> _lstwMSExceptionMessage = new List<WMSExceptionMessage>();
                _lstwMSExceptionMessage.Add(ex);
                string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Exception, _lstwMSExceptionMessage), jsonSettings.JsonSerializerSettings));
                return json;
            }
            catch (Exception excp)
            {
                ExceptionHandling.LogException(excp, _ClassCode + "001");
                return null;
            }
        }


        [HttpPost]
        [Route("GetCCBlockedLocations")]
        public async Task<string> GetCCBlockedLocations(WMSCoreMessage oRequest)
        {
            try
            {
                DataSet userDataTable = null;
      
                List<CycleCountDTO> _lstcyclecount = new List<CycleCountDTO>();

                if (oRequest != null)
                {
                    CycleCountDTO _ocyclecountDTO = (CycleCountDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);

                    if (_ocyclecountDTO != null)
                    {
                        BO.CycleCount ocycleCount = new BO.CycleCount()
                        {
                            AccountId = _ocyclecountDTO.AccountID,
                            UserId = Convert.ToInt32(_ocyclecountDTO.UserId),
                            CycleCountCode = _ocyclecountDTO.CycleCountSeqCode,
                        };

                        userDataTable = await _CycleCount.GetCCBlockedLocations(ocycleCount);
                    }

                    foreach (System.Data.DataRow row in userDataTable.Tables[0].Rows)
                    {
                        CycleCountDTO responseDTO = new CycleCountDTO();
                        responseDTO.LocationID = Convert.ToInt32(row["LocationID"]);
                        responseDTO.Location = row["Location"].ToString();
                        responseDTO.IsBlockedForCycleCount = Convert.ToBoolean(row["IsBlockedForCycleCount"]);
                        responseDTO.IsScanned = row["IsScanned"].ToString();
                        responseDTO.IsVstore = row["IsVstore"] == DBNull.Value ? false : Convert.ToBoolean(row["IsVstore"]);
                        responseDTO.TrayNo = string.IsNullOrEmpty(row["TrayNo"].ToString()) ? "" : row["TrayNo"].ToString();
                        responseDTO.Machineno = string.IsNullOrEmpty(row["Machineno"].ToString()) ? "" : row["Machineno"].ToString();
                        responseDTO.VStoreType = string.IsNullOrEmpty(row["VStoreType"].ToString()) ? "" : row["VStoreType"].ToString();
                        responseDTO.Accesspoint = string.IsNullOrEmpty(row["Accesspoint"].ToString()) ? "" : row["Accesspoint"].ToString();
                        _lstcyclecount.Add(responseDTO);
                    }

                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.CycleCount, _lstcyclecount), jsonSettings.JsonSerializerSettings));
                    return json;
                }
                else return null;
            }
            catch (WMSExceptionMessage ex)
            {
                List<WMSExceptionMessage> _lstwMSExceptionMessage = new List<WMSExceptionMessage>();
                _lstwMSExceptionMessage.Add(ex);
                string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Exception, _lstwMSExceptionMessage)));
                return json;
            }
            catch (Exception excp)
            {
                ExceptionHandling.LogException(excp, _ClassCode + "001");
                return null;
            }
        }


        [HttpPost]
        [Route("IsBlockedLocation")]
        public async Task<string> IsBlockedLocation(WMSCoreMessage oRequest)
        {
            try
            {
                BO.CycleCount oCyclecount = new BO.CycleCount();
                List<CycleCountDTO> _lstcyclecount = new List<CycleCountDTO>();

                if (oRequest != null)
                {

                    CycleCountDTO _ocyclecountDTO = (CycleCountDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);

                    if (_ocyclecountDTO != null)
                    {

                        BO.CycleCount ocycleCount = new BO.CycleCount()
                        {
                            AccountId = _ocyclecountDTO.AccountID,
                            Location = _ocyclecountDTO.Location,
                            CCName = _ocyclecountDTO.CCName,
                            UserId = Convert.ToInt32(_ocyclecountDTO.UserId),
                            WarehouseID = Convert.ToInt32(_ocyclecountDTO.WarehouseID)
                        };

                        oCyclecount = await _CycleCount.IsBlockedLocation(ocycleCount);
                    }

                    CycleCountDTO responseDTO = new CycleCountDTO();
                    if (oCyclecount.Result == "-4")
                    {
                        throw new WMSExceptionMessage() { WMSExceptionCode = null, WMSMessage = "Not Blocked for CycleCount", ShowAsError = true };
                    }
                    responseDTO.Result = oCyclecount.Result;

                    _lstcyclecount.Add(responseDTO);
                   
                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.CycleCount, _lstcyclecount), jsonSettings.JsonSerializerSettings));
                    return json;
                }
                else return null;
            }
            catch (WMSExceptionMessage ex)
            {
                List<WMSExceptionMessage> _lstwMSExceptionMessage = new List<WMSExceptionMessage>();
                _lstwMSExceptionMessage.Add(ex);
                string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Exception, _lstwMSExceptionMessage), jsonSettings.JsonSerializerSettings));
                return json;
            }
            catch (Exception excp)
            {
                ExceptionHandling.LogException(excp, _ClassCode + "001");
                return null;
            }
        }

        [HttpPost]
        [Route("ReleaseCycleCountLocationOLD")]
        public async Task<string> ReleaseCycleCountLocation(WMSCoreMessage oRequest)
        {
            try
            {
                BO.CycleCount oCycleCount = new BO.CycleCount();
                List<CycleCountDTO> _lstcyclecount = new List<CycleCountDTO>();

                if (oRequest != null)
                {
                    CycleCountDTO _ocyclecountDTO = (CycleCountDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);

                    if (_ocyclecountDTO != null)
                    {
                        BO.CycleCount ocycleCount = new BO.CycleCount()
                        {
                            AccountId = _ocyclecountDTO.AccountID,
                            Location = _ocyclecountDTO.Location,
                            CCName = _ocyclecountDTO.CCName,
                            WarehouseID = Convert.ToInt32(_ocyclecountDTO.WarehouseID)
                        };

                        oCycleCount = await _CycleCount.ReleaseCycleCountLocation(ocycleCount);

                        CycleCountDTO orespnonseDTO = new CycleCountDTO();
                        orespnonseDTO.Result = oCycleCount.Result;

                        _lstcyclecount.Add(orespnonseDTO);
                    }
                }

                var jsonSerializerSettings = new JsonSerializerSettings
                {
                    Converters = { new StringEnumConverter() },
                    NullValueHandling = NullValueHandling.Include,
                };

                string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.CycleCount, _lstcyclecount), jsonSettings.JsonSerializerSettings));
                return json;
            }
            catch (WMSExceptionMessage ex)
            {
                List<WMSExceptionMessage> _lstwMSExceptionMessage = new List<WMSExceptionMessage>();
                _lstwMSExceptionMessage.Add(ex);
                string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Exception, _lstwMSExceptionMessage), jsonSettings.JsonSerializerSettings));
                return json;
            }
            catch (Exception excp)
            {
                ExceptionHandling.LogException(excp, _ClassCode + "001");
                return null;
            }
        }

        [HttpPost]
        [Route("BlockLocationForCycleCount")]
        public async Task<string> BlockLocationForCycleCount(WMSCoreMessage oRequest)
        {
            try
            {

                BO.CycleCount oCycleCount = new BO.CycleCount();

                List<CycleCountDTO> _lstcyclecount = new List<CycleCountDTO>();

                if (oRequest != null)
                {
                    CycleCountDTO _ocyclecountDTO = (CycleCountDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);

                    if (_ocyclecountDTO != null)
                    {
                        BO.CycleCount ocycleCount = new BO.CycleCount()
                        {
                            AccountId = _ocyclecountDTO.AccountID,
                            Location = _ocyclecountDTO.Location,
                            CCName = _ocyclecountDTO.CCName
                        };

                        oCycleCount = await _CycleCount.BlockLocationForCycleCount(ocycleCount);
                    }

                    CycleCountDTO responseDTO = new CycleCountDTO();
                    responseDTO.Result = oCycleCount.Result;
                    if (responseDTO.Result == "")
                    {
                        responseDTO.Count = oCycleCount.Count;
                    }
                    else
                    {
                        throw new WMSExceptionMessage() { WMSExceptionCode = oCycleCount.Result, WMSMessage = oCycleCount.Result, ShowAsError = true };
                    }
                    _lstcyclecount.Add(responseDTO);

                    var jsonSerializerSettings = new JsonSerializerSettings
                    {
                        Converters = { new StringEnumConverter() },
                        NullValueHandling = NullValueHandling.Include,
                    };

                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.CycleCount, _lstcyclecount), jsonSettings.JsonSerializerSettings));
                    return json;
                }
                else return null;
            }
            catch (WMSExceptionMessage ex)
            {
                List<WMSExceptionMessage> _lstwMSExceptionMessage = new List<WMSExceptionMessage>();
                _lstwMSExceptionMessage.Add(ex);
                string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Exception, _lstwMSExceptionMessage), jsonSettings.JsonSerializerSettings));
                return json;
            }
            catch (Exception excp)
            {
                ExceptionHandling.LogException(excp, _ClassCode + "001");
                return null;
            }
        }

        [HttpPost]
        [Route("CheckMaterialAvailablilty")]
        public async Task<string> CheckMaterialAvailablilty(WMSCoreMessage oRequest)
        {
            try
            {         
                BO.CycleCount oCyclecount = new BO.CycleCount();

                List<CycleCountDTO> _lstcyclecount = new List<CycleCountDTO>();

                if (oRequest != null)
                {
                    CycleCountDTO _ocyclecountDTO = (CycleCountDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);

                    if (_ocyclecountDTO != null)
                    {
                        BO.CycleCount ocycleCount = new BO.CycleCount()
                        {
                            AccountId = _ocyclecountDTO.AccountID,
                            Location = _ocyclecountDTO.Location,
                            SKU = _ocyclecountDTO.MaterialCode,
                            CCName = _ocyclecountDTO.CCName,
                            TenantID = Convert.ToInt32(_ocyclecountDTO.TenantId),
                            UserId = Convert.ToInt32(_ocyclecountDTO.UserId)
                        };

                        oCyclecount = await _CycleCount.CheckMaterialAvailablilty(ocycleCount);
                    }

                    CycleCountDTO responseDTO = new CycleCountDTO();
                    responseDTO.Result = oCyclecount.Result;
                    if (responseDTO.Result == "")
                    {
                        responseDTO.Result = "1";
                    }
                    else
                    {
                        throw new WMSExceptionMessage() { WMSExceptionCode = oCyclecount.Result, WMSMessage = oCyclecount.Result, ShowAsError = true };
                    }
                    _lstcyclecount.Add(responseDTO);

                    var jsonSerializerSettings = new JsonSerializerSettings
                    {
                        Converters = { new StringEnumConverter() },
                        NullValueHandling = NullValueHandling.Include,
                    };

                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.CycleCount, _lstcyclecount), jsonSettings.JsonSerializerSettings));
                    return json;
                }
                else return null;
            }
            catch (WMSExceptionMessage ex)
            {
                List<WMSExceptionMessage> _lstwMSExceptionMessage = new List<WMSExceptionMessage>();
                _lstwMSExceptionMessage.Add(ex);
                string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Exception, _lstwMSExceptionMessage), jsonSettings.JsonSerializerSettings));
                return json;
            }
            catch (Exception excp)
            {
                ExceptionHandling.LogException(excp, _ClassCode + "001");
                return null;
            }
        }

        [HttpPost]
        [Route("GetCycleCountInformation")]
        public async Task<string> GetCycleCountInformation(WMSCoreMessage oRequest)
        {
            try
            {
                List<CycleCountDTO> _lstcyclecount = new List<CycleCountDTO>();

                if (oRequest != null)
                {
                    CycleCountDTO _ocyclecountDTO = (CycleCountDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);

                    if (_ocyclecountDTO != null)
                    {
                        BO.CycleCount ocycleCountrequest = new BO.CycleCount()
                        {
                            AccountId = _ocyclecountDTO.AccountID,
                            Location = _ocyclecountDTO.Location,
                            CCName = _ocyclecountDTO.CCName
                        };

                        List<BO.CycleCount> _lstCC = await _CycleCount.GetCycleCountInformation(ocycleCountrequest);

                        foreach (BO.CycleCount oCycleCount in _lstCC)
                        {
                            _lstcyclecount.Add(new CycleCountDTO()
                            {

                                CCName = oCycleCount.CCName,
                                Location = oCycleCount.Location,
                                MaterialCode = oCycleCount.SKU,
                                MfgDate = oCycleCount.MfgDate,
                                ExpDate = oCycleCount.ExpDate,
                                SerialNo = oCycleCount.SerialNo,
                                BatchNo = oCycleCount.BatchNo,
                                ProjectRefNo = oCycleCount.ProjectRefNo,
                                CCQty = oCycleCount.CCQty.ToString(),
                            });
                        }
                    }
                  
                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.CycleCount, _lstcyclecount), jsonSettings.JsonSerializerSettings));
                    return json;
                }
                else return null;
            }
            catch (WMSExceptionMessage ex)
            {
                List<WMSExceptionMessage> _lstwMSExceptionMessage = new List<WMSExceptionMessage>();
                _lstwMSExceptionMessage.Add(ex);
                string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Exception, _lstwMSExceptionMessage), jsonSettings.JsonSerializerSettings));
                return json;
            }
            catch (Exception excp)
            {
                ExceptionHandling.LogException(excp, _ClassCode + "001");
                return null;
            }
        }


        [HttpPost]
        [Route("ReleaseCycleCountLocation")]
        public async Task<string> MarkBinComplete(WMSCoreMessage oRequest)
        {
            try
            {
                CycleCountDTO cycleCountDTO = new CycleCountDTO();
                List<CycleCountDTO> _lCycleCountDTO = new List<CycleCountDTO>();

                bool status = false;
                if (oRequest != null)
                {
                    CycleCountDTO _oCycleCountDTO = (CycleCountDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);

                    if (_oCycleCountDTO != null)
                    {
                        Location loc = new Location()
                        {
                            LocationCode = _oCycleCountDTO.Location
                        };
                        CycleCount oCycleCount = new CycleCount()
                        {
                            CycleCountCode = _oCycleCountDTO.CycleCountSeqCode,
                            AccountCycleCountName = _oCycleCountDTO.CCName,
                            AccountID = _oCycleCountDTO.AccountID,
                        };
                        Inventory oInventory = new Inventory()
                        {
                            LocationCode = _oCycleCountDTO.Location,
                            CreatedBy = ConversionUtility.ConvertToInt(_oCycleCountDTO.UserId)
                        };

                        status = await _CycleCount.ClearLocationBlock(loc, oCycleCount, oInventory , true);

                        if (status)
                        {
                            cycleCountDTO.Result = "Closed successfully";
                        }
                        else
                        {
                            cycleCountDTO = null;
                        }
                        _lCycleCountDTO.Add(cycleCountDTO);

                    }
                    //else
                    //    cycleCountDTO = null;

                }
          
                string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.CycleCount, _lCycleCountDTO), jsonSettings.JsonSerializerSettings));
                return json;
            }
            catch (WMSExceptionMessage ex)
            {
                List<WMSExceptionMessage> _lstwMSExceptionMessage = new List<WMSExceptionMessage>();
                _lstwMSExceptionMessage.Add(ex);
                string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Exception, _lstwMSExceptionMessage), jsonSettings.JsonSerializerSettings));
                return json;

            }
            catch (Exception excp)
            {
                ExceptionHandling.LogException(excp, _ClassCode + "006");
                return null;
            }
        }

        [HttpPost]
        [Route("ChekPalletLocation")]
        public async Task<string> ChekPalletLocation(WMSCoreMessage oRequest)
        {
            try
            {
                string LocationCode = "";
                string LocationCheck = "";
#pragma warning disable CS0219 // The variable 'UserID' is assigned but its value is never used
                int UserID = 0;
#pragma warning restore CS0219 // The variable 'UserID' is assigned but its value is never used
                BO.CycleCount oresponse = new BO.CycleCount();

                List<CycleCountDTO> _lstcyclecount = new List<CycleCountDTO>();
                if (oRequest != null)
                {
                    CycleCountDTO _ocyclecountDTO = (CycleCountDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);
                    if (_ocyclecountDTO != null)
                    {
                        LocationCheck = _ocyclecountDTO.Location;
                        LocationCode = await _CycleCount.GetConatinerLocationBin(_ocyclecountDTO.PalletNo, _ocyclecountDTO.WarehouseID, oRequest.AuthToken.UserID, LocationCheck);
                    }
                    if (LocationCode != null || LocationCode != string.Empty)
                    {
                        if (LocationCheck == LocationCode)
                        {
                            _ocyclecountDTO.Result = "1";
                        }
                        else
                        {
                            if (LocationCode == "-1")
                            {
                                throw new WMSExceptionMessage() { WMSExceptionCode = null, WMSMessage = "Container does not exist in the Warehouse", ShowAsError = true };
                            }
                            else
                            {
                                throw new WMSExceptionMessage() { WMSExceptionCode = null, WMSMessage = "Container does not exist in the Location", ShowAsError = true };
                            }
                        }
                    }
                    _lstcyclecount.Add(_ocyclecountDTO);

                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Inventory, _lstcyclecount), jsonSettings.JsonSerializerSettings));
                    return json;
                }
                else
                    return null;
            }
            catch (WMSExceptionMessage ex)
            {
                List<WMSExceptionMessage> _lstwMSExceptionMessage = new List<WMSExceptionMessage>();
                _lstwMSExceptionMessage.Add(ex);
                string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Exception, _lstwMSExceptionMessage), jsonSettings.JsonSerializerSettings));
                return json;
            }
            catch (Exception excp)
            {
                ExceptionHandling.LogException(excp, _ClassCode + "005");
                return null;
            }
        }

        [HttpPost]
        [Route("UpsertCycleCount")]
        public async Task<string> UpsertCycleCount(WMSCoreMessage oRequest)
        {
            try
            {
                BO.CycleCount oresponse = new BO.CycleCount();

                List<CycleCountDTO> _lstcyclecount = new List<CycleCountDTO>();

                if (oRequest != null)
                {
                    CycleCountDTO _ocyclecountDTO = (CycleCountDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);

                    if (_ocyclecountDTO != null)
                    {
                        BO.CycleCount ocycleCount = new BO.CycleCount()
                        {
                            Location = _ocyclecountDTO.Location,
                            Container = _ocyclecountDTO.PalletNo,
                            SKU = _ocyclecountDTO.MaterialCode,
                            UserId = ConversionUtility.ConvertToInt(_ocyclecountDTO.UserId),
                            CCName = _ocyclecountDTO.CCName,
                            CCQty = Convert.ToDecimal(_ocyclecountDTO.CCQty),
                            BatchNo = _ocyclecountDTO.BatchNo,
                            SerialNo = _ocyclecountDTO.SerialNo,
                            ProjectRefNo = _ocyclecountDTO.ProjectRefNo,
                            MfgDate = _ocyclecountDTO.MfgDate,
                            ExpDate = _ocyclecountDTO.ExpDate,
                            MRP = _ocyclecountDTO.MRP,
                            WarehouseID = Convert.ToInt32(_ocyclecountDTO.WarehouseID),
                            TenantID = Convert.ToInt32(_ocyclecountDTO.TenantId),
                            StorageLocation = _ocyclecountDTO.StorageLocation,
                            Grade=_ocyclecountDTO.Grade
                        };

                        oresponse = await _CycleCount.UpsertCycleCount(ocycleCount);
                    }

                    CycleCountDTO responseDTO = new CycleCountDTO();
                    responseDTO.Result = oresponse.Result;

                    _lstcyclecount.Add(responseDTO);

                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.CycleCount, _lstcyclecount), jsonSettings.JsonSerializerSettings));
                    return json;
                }
                else return null;
            }
            catch (WMSExceptionMessage ex)
            {
                List<WMSExceptionMessage> _lstwMSExceptionMessage = new List<WMSExceptionMessage>();
                _lstwMSExceptionMessage.Add(ex);
                string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Exception, _lstwMSExceptionMessage), jsonSettings.JsonSerializerSettings));
                return json;
            }
            catch (Exception excp)
            {
                ExceptionHandling.LogException(excp, _ClassCode + "001");
                return null;
            }
        }

    }
}
