using Simpolo_Endpoint.BO;
using Simpolo_Endpoint.DAO.HHTInterface;
using Simpolo_Endpoint.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Simpolo_Endpoint.Models;

namespace Simpolo_Endpoint.Controllers.HHTControllers
{
    [Route("[controller]")]
    //[Authorize]
    [ApiController]
    public class HouseKeepingController : ControllerBase
    {
        private readonly IHouseKeeping _HouseKeeping;
        //BaseController baseController = new BaseController();
        JsonSettings jsonSettings = new JsonSettings();
        public HouseKeepingController(IHouseKeeping housekeeping)
        {
            _HouseKeeping = housekeeping;
            _ClassCode = ExceptionHandling.GetClassExceptionCode(ExceptionHandling.ExcpConstants_API_Enpoint.HouseKeepingController);
        }

        private string _ClassCode = string.Empty;

        [HttpPost]
        [Route("GetActivestock")]
        public async Task<string> GetActivestock(WMSCoreMessage oRequest)
        {
            try
            {
                DataSet userDataTable = null;

                List<InventoryDTO> _lstinventory = new List<InventoryDTO>();
                if (oRequest != null)
                {
                    List<InventoryDTO> lInventory = new List<InventoryDTO>();
                    dynamic obj = oRequest.EntityObject;
                    //InventoryDTO _oInventory = JsonConvert.DeserializeObject<InventoryDTO>(obj.ToString());

                    InventoryDTO _oInventory = (InventoryDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);

                    if (_oInventory != null)
                    {
                        InventoryDTO oiLiveStock = new InventoryDTO();
                        oiLiveStock.MaterialCode = _oInventory.MaterialCode;
                        oiLiveStock.ContainerCode = _oInventory.ContainerCode;
                        oiLiveStock.LocationCode = _oInventory.LocationCode;
                        oiLiveStock.TenantCode = _oInventory.TenantCode;
                        oiLiveStock.BatchNo = _oInventory.BatchNo;
                        oiLiveStock.SerialNo = _oInventory.SerialNo;
                        oiLiveStock.ProjectNo = _oInventory.ProjectNo;
                        oiLiveStock.MfgDate = _oInventory.MfgDate;
                        oiLiveStock.ExpDate = _oInventory.ExpDate;
                        oiLiveStock.MRP = _oInventory.MRP;
                        oiLiveStock.AccountID = _oInventory.AccountID;
                        oiLiveStock.TenantID = _oInventory.TenantID;
                        oiLiveStock.WarehouseId = _oInventory.WarehouseId;
                        //oiLiveStock.Result = _oInventory.Result;
                        userDataTable = await _HouseKeeping.GetLiveStockData(_oInventory);
                    }
                    if (userDataTable != null)
                    {
                        foreach (DataRow row in userDataTable.Tables[0].Rows)
                        {
                            InventoryDTO responseDTO = new InventoryDTO();
                            responseDTO.MaterialCode = row["Part Number"].ToString();
                            responseDTO.LocationCode = row["Location"].ToString();
                            responseDTO.Quantity = row["AvailableQty"].ToString();
                            responseDTO.SLOC = row["SLOC"].ToString();
                            responseDTO.MfgDate = row["MfgDate"].ToString();
                            responseDTO.ExpDate = row["ExpDate"].ToString();
                            responseDTO.SerialNo = row["SerialNo"].ToString();
                            responseDTO.BatchNo = row["BatchNo"].ToString();
                            responseDTO.ProjectNo = row["ProjectRefNo"].ToString();
                            responseDTO.MRP = row["MRP"].ToString();
                            responseDTO.SugLocation = row["SugLocation"].ToString();
                            // responseDTO.HasDisp = row["HasDiscrepancy"].ToString();
                            _lstinventory.Add(responseDTO);
                        }
                    }

                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Inventory, _lstinventory), jsonSettings.JsonSerializerSettings));
                    return json;
                }

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
                ExceptionHandling.LogException(excp, _ClassCode + "004");
                return null;
            }
        }



        [HttpPost]
        [Route("GetItemPutawaySuggestion")]
        public async Task<string> GetItemPutawaySuggestion(WMSCoreMessage oRequest)
        {
            try
            {
                DataSet userDataTable = null;

                List<InventoryDTO> _lstinventory = new List<InventoryDTO>();
                if (oRequest != null)
                {
                    List<InventoryDTO> lInventory = new List<InventoryDTO>();
                    dynamic obj = oRequest.EntityObject;
                    //InventoryDTO _oInventory = JsonConvert.DeserializeObject<InventoryDTO>(obj.ToString());

                    InventoryDTO _oInventory = (InventoryDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);

                    if (_oInventory != null)
                    {
                        InventoryDTO oiLiveStock = new InventoryDTO();
                        oiLiveStock.MaterialCode = _oInventory.MaterialCode;
                        oiLiveStock.ContainerCode = _oInventory.ContainerCode;
                        oiLiveStock.LocationCode = _oInventory.LocationCode;
                        oiLiveStock.TenantCode = _oInventory.TenantCode;
                        oiLiveStock.BatchNo = _oInventory.BatchNo;
                        oiLiveStock.SerialNo = _oInventory.SerialNo;
                        oiLiveStock.ProjectNo = _oInventory.ProjectNo;
                        oiLiveStock.MfgDate = _oInventory.MfgDate;
                        oiLiveStock.ExpDate = _oInventory.ExpDate;
                        oiLiveStock.MRP = _oInventory.MRP;
                        oiLiveStock.AccountID = _oInventory.AccountID;
                        oiLiveStock.TenantID = _oInventory.TenantID;
                        oiLiveStock.WarehouseId = _oInventory.WarehouseId;
                        //oiLiveStock.Result = _oInventory.Result;
                        userDataTable = await _HouseKeeping.GetItemPutawaySuggestion(_oInventory);
                    }
                    if (userDataTable != null)
                    {
                        foreach (DataRow row in userDataTable.Tables[0].Rows)
                        {
                            InventoryDTO responseDTO = new InventoryDTO();
                            responseDTO.MaterialCode = row["Part Number"].ToString();
                            responseDTO.MDescription = row["MDescription"].ToString();
                            responseDTO.LocationCode = row["Location"].ToString();
                            responseDTO.Quantity = row["AvailableQty"].ToString();
                            responseDTO.SLOC = row["SLOC"].ToString();
                            responseDTO.MfgDate = row["MfgDate"].ToString();
                            responseDTO.ExpDate = row["ExpDate"].ToString();
                            responseDTO.SerialNo = row["SerialNo"].ToString();
                            responseDTO.BatchNo = row["BatchNo"].ToString();
                            responseDTO.ProjectNo = row["ProjectRefNo"].ToString();
                            responseDTO.MRP = row["MRP"].ToString();
                            responseDTO.SugLocation = row["SugLocation"].ToString();
                            responseDTO.SugQty = row["SugQty"].ToString();
                            responseDTO.IsVstore = row["IsVstore"] == DBNull.Value ? false : Convert.ToBoolean(row["IsVstore"]);
                            responseDTO.TrayNo = string.IsNullOrEmpty(row["TrayNo"].ToString()) ? "" : row["TrayNo"].ToString();
                            responseDTO.Machineno = string.IsNullOrEmpty(row["Machineno"].ToString()) ? "" : row["Machineno"].ToString();
                            responseDTO.VStoreType = string.IsNullOrEmpty(row["VStoreType"].ToString()) ? "" : row["VStoreType"].ToString();
                            responseDTO.Accesspoint = string.IsNullOrEmpty(row["Accesspoint"].ToString()) ? "" : row["Accesspoint"].ToString();
                            responseDTO.Grade = string.IsNullOrEmpty(row["Grade"].ToString()) ? "" : row["Grade"].ToString();
                            // responseDTO.HasDisp = row["HasDiscrepancy"].ToString();
                            _lstinventory.Add(responseDTO);
                        }
                    }

                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Inventory, _lstinventory), jsonSettings.JsonSerializerSettings));
                    return json;
                }

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
                ExceptionHandling.LogException(excp, _ClassCode + "004");
                return null;
            }
        }


        [HttpPost]
        [Route("GetWarehouse")]
        public async Task<string> GetWarehouse(WMSCoreMessage oRequest)
        {
            try
            {
                DataSet ds = null;
                List<HouseKeepingDTO> _lsthousekeeping = new List<HouseKeepingDTO>();

                if (oRequest != null)
                {
                    HouseKeepingDTO _oHousekeepingDTO = (HouseKeepingDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);

                    if (_oHousekeepingDTO != null)
                    {
                        LiveStock ooutbound = new LiveStock()
                        {
                            AccountId = ConversionUtility.ConvertToInt(_oHousekeepingDTO.AccountID),
                            TenantID = ConversionUtility.ConvertToInt(_oHousekeepingDTO.TenantID),
                            UserId = ConversionUtility.ConvertToInt(_oHousekeepingDTO.UserId)
                        };

                        ds = await _HouseKeeping.GetWarehouse(ooutbound);
                    }
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        HouseKeepingDTO responseDTO = new HouseKeepingDTO();
                        responseDTO.Warehouse = row["WHCode"].ToString();
                        responseDTO.WarehouseId = row["WarehouseID"].ToString();
                        _lsthousekeeping.Add(responseDTO);
                    }

                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.HouseKeepingDTO, _lsthousekeeping), jsonSettings.JsonSerializerSettings));
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
        [Route("GetTenants")]
        public async Task<string> GetTenants(WMSCoreMessage oRequest)
        {
            try
            {
                DataSet ds = null;
                List<HouseKeepingDTO> _lsthousekeeping = new List<HouseKeepingDTO>();

                if (oRequest != null)
                {
                    HouseKeepingDTO _oHousekeepingDTO = (HouseKeepingDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);

                    if (_oHousekeepingDTO != null)
                    {
                        LiveStock ooutbound = new LiveStock()
                        {
                            AccountId = ConversionUtility.ConvertToInt(_oHousekeepingDTO.AccountID),
                            WarehouseID = ConversionUtility.ConvertToInt(_oHousekeepingDTO.WarehouseId)
                        };

                        ds = await _HouseKeeping.GetTenants(ooutbound);
                    }
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        HouseKeepingDTO responseDTO = new HouseKeepingDTO();
                        responseDTO.TenantID = row["TenantID"].ToString();
                        responseDTO.TenantName = row["TenantName"].ToString();
                        _lsthousekeeping.Add(responseDTO);
                    }

                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.HouseKeepingDTO, _lsthousekeeping), jsonSettings.JsonSerializerSettings));
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
        [Route("CheckLocationForLiveStock")]
        public async Task<string> CheckLocationForLiveStock(WMSCoreMessage oRequest)
        {
            try
            {
                string result = null;
                InventoryDTO _oResponse = new InventoryDTO();
                if (oRequest != null)
                {
                    List<InventoryDTO> lInventoryDTO = new List<InventoryDTO>();
                    InventoryDTO _oInventory = (InventoryDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);

                    if (_oInventory != null)
                    {
                        BO.LiveStock liveStock = new BO.LiveStock()
                        {
                            AccountId = ConversionUtility.ConvertToInt(_oInventory.AccountID),
                            Location = _oInventory.LocationCode,
                            WarehouseID = ConversionUtility.ConvertToInt(_oInventory.WarehouseId)
                        };

                        result = await _HouseKeeping.CheckLoction(liveStock);

                        if (result != null && result != "")
                        {
                            throw new WMSExceptionMessage() { WMSExceptionCode = result, WMSMessage = result, ShowAsError = true };
                        }
                        else
                        {
                            _oResponse.Result = "1";
                            lInventoryDTO.Add(_oResponse);
                        }

                        string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Inventory, lInventoryDTO), jsonSettings.JsonSerializerSettings));
                        return json;
                    }
                }
                return null;
            }
            catch (WMSExceptionMessage ex)
            {
                List<WMSExceptionMessage> _lstwMSExceptionMessage = new List<WMSExceptionMessage>();
                _lstwMSExceptionMessage.Add(ex);
                string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Exception, _lstwMSExceptionMessage), jsonSettings.JsonSerializerSettings));
                return json;
                //  return null;
            }
            catch (Exception excp)
            {
                ExceptionHandling.LogException(excp, _ClassCode + "003");
                return null;
            }
        }

        [HttpPost]
        [Route("CheckTenatMaterial")]
        public async Task<string> CheckTenatMaterial(WMSCoreMessage oRequest)
        {
            try
            {
                string result = null;
                InventoryDTO _oResponse = new InventoryDTO();
                if (oRequest != null)
                {
                    List<InventoryDTO> lInventoryDTO = new List<InventoryDTO>();
                    InventoryDTO _oInventory = (InventoryDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);

                    if (_oInventory != null)
                    {
                        result = await _HouseKeeping.CheckTenatMaterial(_oInventory.MaterialCode, ConversionUtility.ConvertToInt(_oInventory.AccountID), _oInventory.TenantCode);

                        if (result != null && result != "")
                        {
                            throw new WMSExceptionMessage() { WMSExceptionCode = result, WMSMessage = result, ShowAsError = true };
                        }
                        else
                        {
                            _oResponse.Result = "1";
                            lInventoryDTO.Add(_oResponse);
                        }

                        string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Inventory, lInventoryDTO), jsonSettings.JsonSerializerSettings));
                        return json;
                    }
                }
                return null;
            }
            catch (WMSExceptionMessage ex)
            {
                List<WMSExceptionMessage> _lstwMSExceptionMessage = new List<WMSExceptionMessage>();
                _lstwMSExceptionMessage.Add(ex);
                string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Exception, _lstwMSExceptionMessage), jsonSettings.JsonSerializerSettings));
                return json;
                //  return null;
            }
            catch (Exception excp)
            {
                ExceptionHandling.LogException(excp, _ClassCode + "003");
                return null;
            }
        }

        [HttpPost]
        [Route("ValidateCartonForLiveStock")]
        public async Task<string> ValidateCartonForLiveStock(WMSCoreMessage oRequest)
        {
            try
            {
                string Result = "";

                List<HouseKeepingDTO> _lsthousekeeping = new List<HouseKeepingDTO>();

                if (oRequest != null)
                {
                    HouseKeepingDTO _oHousekeepingDTO = (HouseKeepingDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);

                    if (_oHousekeepingDTO != null)
                    {
                        BO.LiveStock liveStock = new BO.LiveStock()
                        {

                            WarehouseID = ConversionUtility.ConvertToInt(_oHousekeepingDTO.WarehouseId),
                            CartonNo = _oHousekeepingDTO.CartonNo
                        };

                        Result = await _HouseKeeping.ValidateCartonLiveStock(liveStock);
                    }

                    if (Result == "1")
                    {
                        _oHousekeepingDTO.Result = Result;
                        _lsthousekeeping.Add(_oHousekeepingDTO);
                    }
                    else
                    {
                        throw new WMSExceptionMessage() { WMSExceptionCode = "", WMSMessage = Result, ShowAsError = true };
                    }

                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.HouseKeepingDTO, _lsthousekeeping), jsonSettings.JsonSerializerSettings));
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
        [Route("UpsertStockTake")]
        public async Task<string> UpsertStockTake(WMSCoreMessage oRequest)
        {
            try
            {
                bool status = false;
                string xmlString = null;
                int UserID = 0;

                if (oRequest != null)
                {
                    //List<StockTakeDTO> stockTakeRequestDTO = (List<StockTakeDTO>)WMSCoreEndpointSecurity.ValidateRequest(oRequest);
                    StockTakeDTO stockTakeRequestDTO = (StockTakeDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);

                    List<StockTakeDetials> _lstAPISORequestDTO = stockTakeRequestDTO.StockTake;

                    string finalresult = "";
                    var emptyNamepsaces = new XmlSerializerNamespaces(new[] { XmlQualifiedName.Empty });

                    emptyNamepsaces.Add("", "");
                    var settings = new XmlWriterSettings();

                    using (StringWriter sw = new StringWriter())
                    {
                        XmlSerializer serialiser = new XmlSerializer(typeof(List<StockTakeDetials>));
                        serialiser.Serialize(sw, _lstAPISORequestDTO, emptyNamepsaces);

                        xmlString = sw.ToString().Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", " ");
                    }

                    finalresult = await _HouseKeeping.UpsertStock(xmlString, oRequest.AuthToken.UserID);

                    return finalresult;
                }

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
                ExceptionHandling.LogException(excp, _ClassCode + "006");
                return null;
            }
        }


        [HttpPost]
        [Route("GetMachineNos")]
        public async Task<string> GetMachineNos(WMSCoreMessage oRequest)
        {
            try
            {
                DataSet ds = null;
                List<InventoryDTO> _lsthousekeeping = new List<InventoryDTO>();

                if (oRequest != null)
                {
                    InventoryDTO _oHousekeepingDTO = (InventoryDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);

                    if (_oHousekeepingDTO != null)
                    {
                        InventoryDTO ooutbound = new InventoryDTO()
                        {
                            AccountID = _oHousekeepingDTO.AccountID,
                            WarehouseID = _oHousekeepingDTO.WarehouseId
                        };

                        ds = await _HouseKeeping.GetMachineNos(ooutbound);
                    }
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        InventoryDTO responseDTO = new InventoryDTO();
                        responseDTO.Machineno = row["MachineNo"].ToString();
                        responseDTO.MachineID = Convert.ToInt32(row["MachineID"]);
                        responseDTO.VStoreType = string.IsNullOrEmpty(row["VStoreType"].ToString()) ? "" : row["VStoreType"].ToString();
                        responseDTO.Accesspoint = string.IsNullOrEmpty(row["Accesspoint"].ToString()) ? "" : row["Accesspoint"].ToString();
                        _lsthousekeeping.Add(responseDTO);
                    }

                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Inventory, _lsthousekeeping), jsonSettings.JsonSerializerSettings));
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
        [Route("GetVehicleNos")]
        public async Task<string> GetVehicleNos(WMSCoreMessage oRequest)
        {
            try
            {
                DataSet ds = null;
                List<InboundDTO> _lstvehiclenos = new List<InboundDTO>();
                InboundDTO _vehicletypes = (InboundDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);
                if (oRequest != null)
                {
                    InboundDTO obj = new InboundDTO();
                    if (_vehicletypes != null)
                    {
                        obj.TransactionType = _vehicletypes.TransactionType;
                        obj.ActionType = _vehicletypes.ActionType;
                        ds = await _HouseKeeping.GetVehicleNos(obj);
                    }

                    foreach (DataRow row in ds.Tables[0].Rows)  
                    {
                        InboundDTO responseDTO = new InboundDTO();
                        responseDTO.TransactionId = Convert.ToInt32(row["TransactionId"]);
                        responseDTO.VehicleNumber = Convert.ToString(row["VehicleNo"]);
                        responseDTO.ReceivingStatus = Convert.ToInt32(row["RecievingStatus"]);
                        responseDTO.VehiclePreLoadWeight = row["VehiclePreLoadWeight"].ToString();
                        responseDTO.VehiclePostLoadWeight = row["VehiclePostLoadWeight"].ToString();
                        responseDTO.DriverNumber = row["DriverNumber"].ToString();
                        _lstvehiclenos.Add(responseDTO);
                    }

                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Inventory, _lstvehiclenos), jsonSettings.JsonSerializerSettings));
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
        [Route("GetVehicleNosDock")]
        public async Task<string> GetVehicleNosDock(WMSCoreMessage oRequest)
        {
            try
            {
                DataSet ds = null;
                List<InboundDTO> _lstvehiclenos = new List<InboundDTO>();
                InboundDTO _vehicletypes = (InboundDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);
                if (oRequest != null)
                {
                    InboundDTO obj = new InboundDTO();
                    if (_vehicletypes != null)
                    {
                        obj.TenantID = _vehicletypes.TenantID;
                        obj.DockId = _vehicletypes.DockId;
                        ds = await _HouseKeeping.GetVehicleNosDock(obj);
                    }

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        InboundDTO responseDTO = new InboundDTO();
                        responseDTO.VehicleNumber = Convert.ToString(row["VehicleNo"]);
                        _lstvehiclenos.Add(responseDTO);
                    }

                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Inventory, _lstvehiclenos), jsonSettings.JsonSerializerSettings));
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
        [Route("GetVehicleTypes")]
        public async Task<string> GetVehicleTypes(WMSCoreMessage oRequest)
        {
            try
            {
                DataSet ds = null;
                List<InboundDTO> _lstvehicletypes = new List<InboundDTO>();

                if (oRequest != null)
                {
                    
                    ds = await _HouseKeeping.GetVehicleTypes();
                    
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        InboundDTO responseDTO = new InboundDTO();
                        responseDTO.VehicleTypeId = Convert.ToInt32(row["VehicleTypeId"]);
                        responseDTO.VehicleType = Convert.ToString(row["VehicleType"]);
                        _lstvehicletypes.Add(responseDTO);
                    }

                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Inventory, _lstvehicletypes), jsonSettings.JsonSerializerSettings));
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
        [Route("GetDocksByTenant")]
        public async Task<string> GetDocksByTenant(WMSCoreMessage oRequest)
        {
            try
            {
                DataSet ds = null;
                List<OutboundDTO> _lstvehicletypes = new List<OutboundDTO>();
                OutboundDTO _DocksList = (OutboundDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);

                if (oRequest != null)
                {

                    ds = await _HouseKeeping.GetDocksByTenant(_DocksList.TenantID);

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        OutboundDTO responseDTO = new OutboundDTO();
                        responseDTO.LocationId = Convert.ToString(row["LocationID"]);
                        responseDTO.Location = Convert.ToString(row["Location"]);
                        _lstvehicletypes.Add(responseDTO);
                    }

                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Outbound, _lstvehicletypes), jsonSettings.JsonSerializerSettings));
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
        [Route("UpsertVehicleGateManagement_HHT")]
        public async Task<string> UpsertVehicleGateManagement_HHT(WMSCoreMessage oRequest)
        {
            try
            {
                DataSet ds = null;

                if (oRequest != null)
                {
                    InboundDTO _vehicletypes = (InboundDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);

                    if (_vehicletypes != null)
                    {
                        InboundDTO obj = new InboundDTO()
                        {
                            VehicleTypeId = _vehicletypes.VehicleTypeId,
                            VehicleNumber = _vehicletypes.VehicleNumber,
                            TransactionId = _vehicletypes.TransactionId,
                            VehiclePreLoadWeight = _vehicletypes.VehiclePreLoadWeight,
                            VehiclePostLoadWeight = _vehicletypes.VehiclePostLoadWeight,
                            DriverNumber = _vehicletypes.DriverNumber,
                            TransactionType = _vehicletypes.TransactionType,
                            ActionType = _vehicletypes.ActionType,
                            ReceivingStatus = _vehicletypes.ReceivingStatus,
                            UserId = _vehicletypes.UserId,
                            DockId = _vehicletypes.DockId,
                            LoadingPointID = _vehicletypes.LoadingPointID

                        };
                        ds = await _HouseKeeping.Upsert_VehicleGateManagement_HHT(obj);
                    }
                    List<InboundDTO> _lstdTO = new();
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        InboundDTO dTO = new();
                        dTO.Message = row["N"].ToString();
                        dTO.Result = row["S"].ToString();
                        _lstdTO.Add(dTO);
                    }

                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Inventory, _lstdTO), jsonSettings.JsonSerializerSettings));
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
