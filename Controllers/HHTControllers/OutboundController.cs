using FWMSC21Core.Entities;
using FWMSC21Core_BusinessEntities.Entities;
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
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Simpolo_Endpoint.Models;

namespace Simpolo_Endpoint.Controllers.HHTControllers
{
    [Route("[controller]")]
    //[Authorize]
    [ApiController]
    public class OutboundController : ControllerBase
    {
        private readonly IOutbound _Outbound;
        private string _ClassCode = string.Empty;
        JsonSettings jsonSettings = new JsonSettings();
        private readonly WhatsAppService _whatsappservice;
        public OutboundController(IOutbound outbound, WhatsAppService whatsappservice)
        {
            _Outbound = outbound;
            _ClassCode = ExceptionHandling.GetClassExceptionCode(ExceptionHandling.ExcpConstants_API_Enpoint.OutboundController);
            _whatsappservice = whatsappservice;
        }



        [HttpPost]
        [Route("GetobdRefNos")]
        public async Task<string> GetobdRefNos(WMSCoreMessage oRequest)
        {
            try
            {
                List<OutboundDTO> _lstoutboundDTO = new List<OutboundDTO>();

                if (oRequest != null)
                {
                    OutboundDTO _ooutboundDTO = (OutboundDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);

                    List<Outbound> _lstOutbound = new List<Outbound>();

                    if (_ooutboundDTO != null)
                    {
                        Outbound outbound = new Outbound()
                        {
                            AccountID = ConversionUtility.ConvertToInt(_ooutboundDTO.AccountID),
                            UserId = ConversionUtility.ConvertToInt(_ooutboundDTO.UserId),
                            WareHouseID = _ooutboundDTO.WareHouseID,
                            TenantId = _ooutboundDTO.TenantID,
                            IsSample = Convert.ToInt32(_ooutboundDTO.RID),
                            IsWorkOrder = string.IsNullOrEmpty(_ooutboundDTO.IsWorkOrder) ? "0" : Convert.ToString(_ooutboundDTO.IsWorkOrder),
                            IsLoading = _ooutboundDTO.IsLoading,
                            ActionType=_ooutboundDTO.ActionType
                        };

                        _lstOutbound = await _Outbound.GetOBDRefNos(outbound);

                        if (_lstOutbound.Count != 0)
                        {
                            foreach (Outbound outboundItem in _lstOutbound)
                            {
                                OutboundDTO outboundDTO = new OutboundDTO();
                                outboundDTO.OBDNo = outboundItem.OBDNumber.ToString();
                                outboundDTO.OutboundID = outboundItem.OutboundID.ToString();
                                outboundDTO.PickedQty = outboundItem.PickedQty.ToString();
                                outboundDTO.Loadqty = outboundItem.LoadQty.ToString();
                                outboundDTO.UnLoadQty = outboundItem.UnLoadQty.ToString();
                                _lstoutboundDTO.Add(outboundDTO);
                            }
                        }
                        else
                        {
                            throw new WMSExceptionMessage() { WMSExceptionCode = null, WMSMessage = "No OBD's for this Tenant", ShowAsError = true };
                        }
                    }
                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Outbound, _lstoutboundDTO), jsonSettings.JsonSerializerSettings));
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
        [Route("GetVehicleNumbers_Loading")]
        public async Task<string> GetVehicleNumbers_Loading(WMSCoreMessage oRequest)
        {
            try
            {
                List<OutboundDTO> _lstoutboundDTO = new List<OutboundDTO>();

                if (oRequest != null)
                {
                    OutboundDTO _ooutboundDTO = (OutboundDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);

                    List<Outbound> _lstOutbound = new List<Outbound>();

                    if (_ooutboundDTO != null)
                    {
                        Outbound outbound = new Outbound()
                        {
                            AccountID = ConversionUtility.ConvertToInt(_ooutboundDTO.AccountID),
                            UserId = ConversionUtility.ConvertToInt(_ooutboundDTO.UserId),
                            WareHouseID = _ooutboundDTO.WareHouseID,
                            TenantId = _ooutboundDTO.TenantID,
                            IsSample = Convert.ToInt32(_ooutboundDTO.RID),
                            IsWorkOrder = string.IsNullOrEmpty(_ooutboundDTO.IsWorkOrder) ? "0" : Convert.ToString(_ooutboundDTO.IsWorkOrder),
                            IsLoading = _ooutboundDTO.IsLoading,
                            ActionType = _ooutboundDTO.ActionType
                        };

                        _lstOutbound = await _Outbound.GetVehicleNumbers_Loading(outbound);

                        if (_lstOutbound.Count != 0)
                        {
                            foreach (Outbound outboundItem in _lstOutbound)
                            {
                                OutboundDTO outboundDTO = new OutboundDTO();
                                outboundDTO.Vehicle = outboundItem.Vehicle.ToString();
                                outboundDTO.OutboundID = outboundItem.OutboundID.ToString();
                                outboundDTO.PickedQty = outboundItem.PickedQty.ToString();
                                outboundDTO.Loadqty = outboundItem.LoadQty.ToString();
                                outboundDTO.UnLoadQty = outboundItem.UnLoadQty.ToString();
                                _lstoutboundDTO.Add(outboundDTO);
                            }
                        }
                        else
                        {
                            throw new WMSExceptionMessage() { WMSExceptionCode = null, WMSMessage = "No OBD's for this Tenant", ShowAsError = true };
                        }
                    }
                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Outbound, _lstoutboundDTO), jsonSettings.JsonSerializerSettings));
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
        [Route("GetOBDItemToPick_BatchGrade")]
        public async Task<string> GetOBDItemToPick_BatchGrade(WMSCoreMessage oRequest)
        {
            try
            {
                OutboundDTO responseDTO = new OutboundDTO();
                OutboundDTO oOutboundresponse = new OutboundDTO();
                List<OutboundDTO> _lstoutbound = new List<OutboundDTO>();

                if (oRequest != null)
                {
                    OutboundDTO _ooutboundDTO = (OutboundDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);

                    if (_ooutboundDTO != null)
                    {
                        Simpolo_Endpoint.BO.Outbound oOutbound = new Simpolo_Endpoint.BO.Outbound()
                        {
                            CreatedBy = _ooutboundDTO.UserId,
                            TrasferRefId = _ooutboundDTO.TransferRefId,
                            RID = _ooutboundDTO.RID,
                            FetchNextItem = _ooutboundDTO.FetchNextItem
                        };

                        oOutboundresponse = await _Outbound.GetOBDItemToPick_BatchGrade(_ooutboundDTO);

                        if (oOutboundresponse != null)
                        {
                            responseDTO.AssignedID = oOutboundresponse.AssignedID;
                            responseDTO.MaterialMasterId = oOutboundresponse.MaterialMasterId;
                            responseDTO.SKU = oOutboundresponse.MCode;
                            responseDTO.MaterialDescription = oOutboundresponse.MaterialDescription;
                            responseDTO.PalletNo = oOutboundresponse.CartonNo;
                            responseDTO.CartonID = oOutboundresponse.CartonID;
                            responseDTO.Location = oOutboundresponse.Location;
                            responseDTO.LocationId = oOutboundresponse.LocationId;
                            responseDTO.MfgDate = oOutboundresponse.MfgDate;
                            responseDTO.ExpDate = oOutboundresponse.ExpDate;
                            responseDTO.SerialNo = oOutboundresponse.SerialNo;
                            responseDTO.BatchNo = oOutboundresponse.BatchNo;
                            responseDTO.ProjectNo = oOutboundresponse.ProjectNo;
                            responseDTO.AssignedQuantity = oOutboundresponse.AssignedQuantity;
                            responseDTO.PickedQty = oOutboundresponse.PickedQty;
                            responseDTO.OutboundID = oOutboundresponse.OutboundID;
                            responseDTO.SODetailsID = oOutboundresponse.SODetailsID;
                            responseDTO.SLocId = oOutboundresponse.SLocId;
                            responseDTO.SLoc = oOutboundresponse.SLoc;
                            responseDTO.GoodsmomentDeatilsId = oOutboundresponse.GoodsmomentDeatilsId;
                            responseDTO.Lineno = oOutboundresponse.Lineno;
                            responseDTO.MaterialMaster_IUoMID = oOutboundresponse.MaterialMaster_IUoMID;
                            responseDTO.CF = oOutboundresponse.CF;
                            responseDTO.POSOHeaderId = oOutboundresponse.POSOHeaderId;
                            responseDTO.PendingQty = oOutboundresponse.PendingQty;
                            responseDTO.MRP = oOutboundresponse.MRP;
                            responseDTO.HUNo = oOutboundresponse.HUNo;
                            responseDTO.HUSize = oOutboundresponse.HUSize;
                            responseDTO.IsPSN = oOutboundresponse.IsPSN;
                            responseDTO.CustomerName = oOutboundresponse.CustomerName;
                            responseDTO.DockLocation = oOutboundresponse.DockLocation;
                            responseDTO.RID = oOutboundresponse.RID;
                            responseDTO.IsVstore = oOutboundresponse.IsVstore;
                            responseDTO.TrayNo = oOutboundresponse.TrayNo;
                            responseDTO.Machineno = oOutboundresponse.Machineno;
                            responseDTO.ItemCount = oOutboundresponse.ItemCount;
                            responseDTO.VStoreType = oOutboundresponse.VStoreType;
                            responseDTO.Accesspoint = oOutboundresponse.Accesspoint;
                        }
                        _lstoutbound.Add(responseDTO);
                    }

                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Outbound, _lstoutbound), jsonSettings.JsonSerializerSettings));
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
        [Route("GetOBDItemToPick_PalletConsolidate")]
        public async Task<string> GetOBDItemToPick_PalletConsolidate(WMSCoreMessage oRequest)
        {
            try
            {
                OutboundDTO responseDTO = new OutboundDTO();
                OutboundDTO oOutboundresponse = new OutboundDTO();
                List<OutboundDTO> _lstoutbound = new List<OutboundDTO>();

                if (oRequest != null)
                {
                    OutboundDTO _ooutboundDTO = (OutboundDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);

                    if (_ooutboundDTO != null)
                    {
                        Simpolo_Endpoint.BO.Outbound oOutbound = new Simpolo_Endpoint.BO.Outbound()
                        {
                            CreatedBy = _ooutboundDTO.UserId,
                            TrasferRefId = _ooutboundDTO.TransferRefId,
                            RID = _ooutboundDTO.RID,
                            FetchNextItem = _ooutboundDTO.FetchNextItem
                        };

                        oOutboundresponse = await _Outbound.GetOBDItemToPick_PalletConsolidate(_ooutboundDTO);

                        if (oOutboundresponse != null)
                        {
                            responseDTO.AssignedID = oOutboundresponse.AssignedID;
                            responseDTO.MaterialMasterId = oOutboundresponse.MaterialMasterId;
                            responseDTO.SKU = oOutboundresponse.MCode;
                            responseDTO.MaterialDescription = oOutboundresponse.MaterialDescription;
                            responseDTO.PalletNo = oOutboundresponse.CartonNo;
                            responseDTO.CartonID = oOutboundresponse.CartonID;
                            responseDTO.Location = oOutboundresponse.Location;
                            responseDTO.LocationId = oOutboundresponse.LocationId;
                            responseDTO.MfgDate = oOutboundresponse.MfgDate;
                            responseDTO.ExpDate = oOutboundresponse.ExpDate;
                            responseDTO.SerialNo = oOutboundresponse.SerialNo;
                            responseDTO.BatchNo = oOutboundresponse.BatchNo;
                            responseDTO.ProjectNo = oOutboundresponse.ProjectNo;
                            responseDTO.AssignedQuantity = oOutboundresponse.AssignedQuantity;
                            responseDTO.PickedQty = oOutboundresponse.PickedQty;
                            responseDTO.OutboundID = oOutboundresponse.OutboundID;
                            responseDTO.SODetailsID = oOutboundresponse.SODetailsID;
                            responseDTO.SLocId = oOutboundresponse.SLocId;
                            responseDTO.SLoc = oOutboundresponse.SLoc;
                            responseDTO.GoodsmomentDeatilsId = oOutboundresponse.GoodsmomentDeatilsId;
                            responseDTO.Lineno = oOutboundresponse.Lineno;
                            responseDTO.MaterialMaster_IUoMID = oOutboundresponse.MaterialMaster_IUoMID;
                            responseDTO.CF = oOutboundresponse.CF;
                            responseDTO.POSOHeaderId = oOutboundresponse.POSOHeaderId;
                            responseDTO.PendingQty = oOutboundresponse.PendingQty;
                            responseDTO.MRP = oOutboundresponse.MRP;
                            responseDTO.HUNo = oOutboundresponse.HUNo;
                            responseDTO.HUSize = oOutboundresponse.HUSize;
                            responseDTO.IsPSN = oOutboundresponse.IsPSN;
                            responseDTO.CustomerName = oOutboundresponse.CustomerName;
                            responseDTO.DockLocation = oOutboundresponse.DockLocation;
                            responseDTO.RID = oOutboundresponse.RID;
                            responseDTO.IsVstore = oOutboundresponse.IsVstore;
                            responseDTO.TrayNo = oOutboundresponse.TrayNo;
                            responseDTO.Machineno = oOutboundresponse.Machineno;
                            responseDTO.ItemCount = oOutboundresponse.ItemCount;
                            responseDTO.VStoreType = oOutboundresponse.VStoreType;
                            responseDTO.Accesspoint = oOutboundresponse.Accesspoint;
                            responseDTO.ToCartonID = oOutboundresponse.ToCartonID;
                            responseDTO.ToCartonNo = oOutboundresponse.ToCartonNo;
                            responseDTO.ToLocation = oOutboundresponse.ToLocation;
                            responseDTO.ToLocationId=oOutboundresponse.ToLocationId;
                        }
                        _lstoutbound.Add(responseDTO);
                    }

                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Outbound, _lstoutbound), jsonSettings.JsonSerializerSettings));
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
        [Route("GetOBDItemToPick")]
        public async Task<string> GetOBDItemToPick(WMSCoreMessage oRequest)
        {
            try
            {
                OutboundDTO responseDTO = new OutboundDTO();
                OutboundDTO oOutboundresponse = new OutboundDTO();
                List<OutboundDTO> _lstoutbound = new List<OutboundDTO>();

                if (oRequest != null)
                {
                    OutboundDTO _ooutboundDTO = (OutboundDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);

                    if (_ooutboundDTO != null)
                    {
                        Simpolo_Endpoint.BO.Outbound oOutbound = new Simpolo_Endpoint.BO.Outbound()
                        {
                            CreatedBy = _ooutboundDTO.UserId,
                            OutboundId = _ooutboundDTO.OutboundID,
                            RID = _ooutboundDTO.RID,
                            FetchNextItem = _ooutboundDTO.FetchNextItem
                        };

                        oOutboundresponse = await _Outbound.GetOBDItemToPick(_ooutboundDTO);

                        if (oOutboundresponse != null)
                        {
                            responseDTO.AssignedID = oOutboundresponse.AssignedID;
                            responseDTO.MaterialMasterId = oOutboundresponse.MaterialMasterId;
                            responseDTO.SKU = oOutboundresponse.MCode;
                            responseDTO.MaterialDescription = oOutboundresponse.MaterialDescription;
                            responseDTO.PalletNo = oOutboundresponse.CartonNo;
                            responseDTO.CartonID = oOutboundresponse.CartonID;
                            responseDTO.Location = oOutboundresponse.Location;
                            responseDTO.LocationId = oOutboundresponse.LocationId;
                            responseDTO.MfgDate = oOutboundresponse.MfgDate;
                            responseDTO.ExpDate = oOutboundresponse.ExpDate;
                            responseDTO.SerialNo = oOutboundresponse.SerialNo;
                            responseDTO.BatchNo = oOutboundresponse.BatchNo;
                            responseDTO.ProjectNo = oOutboundresponse.ProjectNo;
                            responseDTO.AssignedQuantity = oOutboundresponse.AssignedQuantity;
                            responseDTO.PickedQty = oOutboundresponse.PickedQty;
                            responseDTO.OutboundID = oOutboundresponse.OutboundID;
                            responseDTO.SODetailsID = oOutboundresponse.SODetailsID;
                            responseDTO.SLocId = oOutboundresponse.SLocId;
                            responseDTO.SLoc = oOutboundresponse.SLoc;
                            responseDTO.GoodsmomentDeatilsId = oOutboundresponse.GoodsmomentDeatilsId;
                            responseDTO.Lineno = oOutboundresponse.Lineno;
                            responseDTO.MaterialMaster_IUoMID = oOutboundresponse.MaterialMaster_IUoMID;
                            responseDTO.CF = oOutboundresponse.CF;
                            responseDTO.POSOHeaderId = oOutboundresponse.POSOHeaderId;
                            responseDTO.PendingQty = oOutboundresponse.PendingQty;
                            responseDTO.MRP = oOutboundresponse.MRP;
                            responseDTO.HUNo = oOutboundresponse.HUNo;
                            responseDTO.HUSize = oOutboundresponse.HUSize;
                            responseDTO.IsPSN = oOutboundresponse.IsPSN;
                            responseDTO.CustomerName = oOutboundresponse.CustomerName;
                            responseDTO.DockLocation = oOutboundresponse.DockLocation;
                            responseDTO.RID = oOutboundresponse.RID;
                            responseDTO.IsVstore = oOutboundresponse.IsVstore;
                            responseDTO.TrayNo = oOutboundresponse.TrayNo;
                            responseDTO.Machineno = oOutboundresponse.Machineno;
                            responseDTO.ItemCount = oOutboundresponse.ItemCount;
                            responseDTO.VStoreType = oOutboundresponse.VStoreType;
                            responseDTO.Accesspoint = oOutboundresponse.Accesspoint;
                        }
                        _lstoutbound.Add(responseDTO);
                    }

                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Outbound, _lstoutbound), jsonSettings.JsonSerializerSettings));
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
        [Route("OBDSkipItem")]
        public async Task<string> OBDSkipItem(WMSCoreMessage oRequest)
        {
            try
            {
                OutboundDTO responseDTO = new OutboundDTO();
                OutboundDTO ooutboundresponse = new OutboundDTO();
                List<OutboundDTO> _lstoutbound = new List<OutboundDTO>();

                if (oRequest != null)
                {
                    OutboundDTO _ooutboundDTO = (OutboundDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);

                    if (_ooutboundDTO != null)
                    {
                        BO.Outbound ooutbound = new BO.Outbound()
                        {
                            TotalPickedQty = ConversionUtility.ConvertToDecimal(_ooutboundDTO.PickedQty),
                            Flag = 1,
                            SkipReason = _ooutboundDTO.SkipReason,
                            OutboundId = _ooutboundDTO.OutboundID,
                            SkipQty = ConversionUtility.ConvertToDecimal(_ooutboundDTO.SkipQty),
                            Assignedid = Convert.ToInt32(_ooutboundDTO.AssignedID),
                            MCode = _ooutboundDTO.SKU,
                            MfgDate = _ooutboundDTO.MfgDate,
                            ExpDate = _ooutboundDTO.ExpDate,
                            SerialNo = _ooutboundDTO.SerialNo,
                            BatchNo = _ooutboundDTO.BatchNo,
                            ProjectNo = _ooutboundDTO.ProjectNo,
                            CreatedBy = _ooutboundDTO.UserId,
                            SLoc = _ooutboundDTO.SLoc,
                            CartonNo = _ooutboundDTO.PalletNo,
                            Location = _ooutboundDTO.Location,
                            MRP = _ooutboundDTO.MRP
                        };

                        ooutboundresponse = await _Outbound.OBDSkipItem(_ooutboundDTO);

                        if (ooutboundresponse.Result == "Success")
                        {
                            return await GetOBDItemToPick(oRequest);
                        }
                        else
                        {
                            throw new WMSExceptionMessage() { WMSExceptionCode = ooutboundresponse.Erocode, WMSMessage = ooutboundresponse.Result, ShowAsError = true };
                        }
                    }
                 
                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Outbound, _lstoutbound), jsonSettings.JsonSerializerSettings));
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
        [Route("UpdatePickItem")]
        public async Task<string> UpdatePickItem(WMSCoreMessage oRequest)
        {
            try
            {
                BO.Outbound oOutboundresponse = new BO.Outbound();
                OutboundDTO responseDTO = new OutboundDTO();
                List<OutboundDTO> _lstoutbound = new List<OutboundDTO>();

                if (oRequest != null)
                {
                    OutboundDTO _ooutboundDTO = (OutboundDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);

                    if (_ooutboundDTO != null)
                    {
                        BO.Outbound oOutbound = new BO.Outbound()
                        {
                            AccountId = Convert.ToInt32(_ooutboundDTO.AccountID),
                            CartonNo = _ooutboundDTO.CartonNo,
                            Qty = ConversionUtility.ConvertToDecimal(_ooutboundDTO.PickedQty),
                            MCode = _ooutboundDTO.MCode,
                            CreatedBy = _ooutboundDTO.UserId,
                            MaterialStorageParameterIDs = "",
                            MaterialStorageParameterValues = "",
                            KitNo = _ooutboundDTO.KitId,
                            ToCartonNo = _ooutboundDTO.ToCartonNO,
                            SerialNo = _ooutboundDTO.SerialNo,
                            MfgDate = _ooutboundDTO.MfgDate,
                            ExpDate = _ooutboundDTO.ExpDate,
                            BatchNo = _ooutboundDTO.BatchNo,
                            ProjectNo = _ooutboundDTO.ProjectNo,
                            Assignedid = Convert.ToInt32(_ooutboundDTO.AssignedID),
                            Obdno = _ooutboundDTO.OBDNo,
                            Lineno = _ooutboundDTO.Lineno,
                            POSOHeaderId = _ooutboundDTO.POSOHeaderId,
                            Location = _ooutboundDTO.Location,
                            IsDam = _ooutboundDTO.IsDam,
                            HasDisc = _ooutboundDTO.HasDis,
                            OutboundId = _ooutboundDTO.OutboundID,
                            SODetailsID = _ooutboundDTO.SODetailsID,
                            MRP = _ooutboundDTO.MRP,
                            HUNo = ConversionUtility.ConvertToInt(_ooutboundDTO.HUNo),
                            HUSize = ConversionUtility.ConvertToInt(_ooutboundDTO.HUSize),
                            IsPSN = ConversionUtility.ConvertToInt(_ooutboundDTO.IsPSN),
                            PSN = _ooutboundDTO.PSN,
                            RID = _ooutboundDTO.RID
                        };

                        oOutboundresponse = await _Outbound.UpdatePickItem(oOutbound);

                        if (oOutboundresponse != null)
                        {
                            if (oOutboundresponse.Result == "Success")
                            {
                                responseDTO.AssignedID = oOutboundresponse.Assignedid.ToString();
                                responseDTO.MaterialMasterId = oOutboundresponse.MaterialMasterId;
                                responseDTO.SKU = oOutboundresponse.MCode;
                                responseDTO.MaterialDescription = oOutboundresponse.MDescription;
                                responseDTO.PalletNo = oOutboundresponse.CartonNo;
                                responseDTO.CartonID = oOutboundresponse.CartonId;
                                responseDTO.Location = oOutboundresponse.Location;
                                responseDTO.LocationId = oOutboundresponse.LocationId.ToString();
                                responseDTO.MfgDate = oOutboundresponse.MfgDate;
                                responseDTO.ExpDate = oOutboundresponse.ExpDate;
                                responseDTO.SerialNo = oOutboundresponse.SerialNo;
                                responseDTO.BatchNo = oOutboundresponse.BatchNo;
                                responseDTO.ProjectNo = oOutboundresponse.ProjectNo;
                                responseDTO.AssignedQuantity = oOutboundresponse.AssignedQuantity;
                                responseDTO.PickedQty = oOutboundresponse.PickedQty;
                                responseDTO.OutboundID = oOutboundresponse.OutboundId;
                                responseDTO.SODetailsID = oOutboundresponse.SODetailsID;
                                responseDTO.SLocId = oOutboundresponse.SLocId;
                                responseDTO.SLoc = oOutboundresponse.SLoc;
                                responseDTO.GoodsmomentDeatilsId = oOutboundresponse.GoodsmomentDeatilsId.ToString();
                                responseDTO.Lineno = oOutboundresponse.Lineno;
                                responseDTO.MaterialMaster_IUoMID = oOutboundresponse.MaterialMaster_IUoMID;
                                responseDTO.CF = oOutboundresponse.CF.ToString();
                                responseDTO.POSOHeaderId = oOutboundresponse.POSOHeaderId;
                                responseDTO.PendingQty = oOutboundresponse.PendingQty;
                                responseDTO.Result = oOutboundresponse.Result;
                                responseDTO.MRP = oOutboundresponse.MRP;
                                responseDTO.HUNo = oOutboundresponse.HUNo.ToString();
                                responseDTO.HUSize = oOutboundresponse.HUSize.ToString();
                            }
                            else if (oOutboundresponse.Result == "-444")
                            {
                                throw new WMSExceptionMessage() { WMSExceptionCode = "EMC_OB_DAL_010", WMSMessage = ErrorMessages.EMC_OB_DAL_010, ShowAsError = true };
                            }
                            else if (oOutboundresponse.Result == "-333")
                            {
                                throw new WMSExceptionMessage() { WMSExceptionCode = "EMC_OB_DAL_011", WMSMessage = ErrorMessages.EMC_OB_DAL_011, ShowAsError = true };
                            }
                            else
                            {
                                throw new WMSExceptionMessage() { WMSExceptionCode = null, WMSMessage = oOutboundresponse.Result, ShowAsError = true };
                            }
                        }

                        _lstoutbound.Add(responseDTO);
                    }
               
                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Outbound, _lstoutbound), jsonSettings.JsonSerializerSettings));
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
        [Route("GetOBDNosUnderSO")]
        public async Task<string> GetOBDNosUnderSO(WMSCoreMessage oRequest)
        {
            try
            {
                OutboundDTO responseDTO = new OutboundDTO();
                List<OutboundDTO> _lstoutbound = new List<OutboundDTO>();
                Outbound _oOutbound = new Outbound();

                if (oRequest != null)
                {
                    List<Outbound> outbounds = new List<Outbound>();
                    OutboundDTO _ooutboundDTO = (OutboundDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);

                    if (_ooutboundDTO != null)
                    {
                        Outbound oOutbound = new Outbound()
                        {
                            SONumber = _ooutboundDTO.SONumber,
                            AccountID = Convert.ToInt32(_ooutboundDTO.AccountID),
                            UserId = Convert.ToInt32(_ooutboundDTO.TenatID)
                        };

                        bool result;
                        result = await _Outbound.CheckOBDSO(oOutbound);

                        outbounds = await _Outbound.GetOBDNosUnderSO(oOutbound);

                        foreach (Outbound outbound in outbounds)
                        {
                            OutboundDTO _outboundDTO = new OutboundDTO();
                            _outboundDTO.OutboundID = outbound.OutboundID.ToString();
                            _outboundDTO.OBDNo = outbound.OBDNumber.ToString();

                            _lstoutbound.Add(_outboundDTO);
                        }
                    }

                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Outbound, _lstoutbound), jsonSettings.JsonSerializerSettings));
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
        [Route("CheckContainerOBD")]
        public async Task<string> CheckContainerOBD(WMSCoreMessage oRequest)
        {
            try
            {
                OutboundDTO responseDTO = new OutboundDTO();
                List<OutboundDTO> _lstinbound = new List<OutboundDTO>();
                String result = null;
                if (oRequest != null)
                {

                    OutboundDTO _oInboundDTO = (OutboundDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);

                    if (_oInboundDTO != null)
                    {
                        result = await _Outbound.CheckContainerOBD(_oInboundDTO.PalletNo, _oInboundDTO.OutboundID);

                        if (result != "")
                        {
                            throw new WMSExceptionMessage() { WMSExceptionCode = result, WMSMessage = result, ShowAsError = true };
                        }
                        else
                        {
                            responseDTO.Result = "1";
                        }
                        _lstinbound.Add(responseDTO);
                    }

                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Inbound, _lstinbound), jsonSettings.JsonSerializerSettings));
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
        [Route("GetOBDItemsForPicking")]
        public async Task<string> GetOBDItemsForPicking(WMSCoreMessage oRequest)
        {
            try
            {
                BO.Outbound oOutboundresponse = new BO.Outbound();
                OutboundDTO responseDTO = new OutboundDTO();
                List<OutboundDTO> _lstoutbound = new List<OutboundDTO>();

                if (oRequest != null)
                {
                    List<Outbound> _lstOutbound = new List<Outbound>();

                    OutboundDTO _ooutboundDTO = (OutboundDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);

                    if (_ooutboundDTO != null)
                    {
                        Outbound outbound = new Outbound()
                        {
                            OutboundID = ConversionUtility.ConvertToInt(_ooutboundDTO.OutboundID)
                        };

                        _lstOutbound = await _Outbound.GetOBDItemsForPicking(outbound);

                        foreach (Outbound outboundItem in _lstOutbound)
                        {
                            OutboundDTO outboundDTO = new OutboundDTO();

                            outboundDTO.OBDNo = outboundItem.OBDNumber.ToString();
                            outboundDTO.OutboundID = outboundItem.OutboundID.ToString();
                            outboundDTO.AssignedID = outboundItem.Assignedid.ToString();
                            outboundDTO.MaterialMasterId = outboundItem.MaterialMasterID.ToString();
                            outboundDTO.SKU = outboundItem.Mcode.ToString();
                            outboundDTO.MaterialDescription = outboundItem.MDescription.ToString();
                            outboundDTO.PalletNo = outboundItem.CartonNo;
                            outboundDTO.CartonID = outboundItem.CartonID.ToString();
                            outboundDTO.Location = outboundItem.Location;
                            outboundDTO.LocationId = outboundItem.LocationId.ToString();

                            outboundDTO.AssignedQuantity = outboundItem.AssignedQuantity.ToString();
                            outboundDTO.PickedQty = outboundItem.PickedQty.ToString();
                            outboundDTO.SODetailsID = outboundItem.SoDetailsID.ToString();
                            outboundDTO.SLocId = outboundItem.SLocId.ToString();
                            outboundDTO.SLoc = outboundItem.SLoc;
                            outboundDTO.Lineno = outboundItem.Lineno.ToString();
                            outboundDTO.MaterialMaster_IUoMID = outboundItem.MaterialMaster_IUoMID.ToString();

                            outboundDTO.POSOHeaderId = outboundItem.POSOHeaderId.ToString();
                            outboundDTO.PendingQty = outboundItem.PendingQty.ToString();
                            outboundDTO.MRP = outboundItem.MRP.ToString();
                            outboundDTO.HUNo = outboundItem.HUNo.ToString();
                            outboundDTO.HUSize = outboundItem.HUSize.ToString();
                            outboundDTO.IsPSN = outboundItem.IsPSN.ToString();
                            outboundDTO.CustomerName = outboundItem.CustomerName.ToString();
                            outboundDTO.DockLocation = outboundItem.DockLocation.ToString();
                            outboundDTO.BatchNo = Convert.ToString(outboundItem.BatchNo);

                            _lstoutbound.Add(outboundDTO);
                        }
                    }

                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Outbound, _lstoutbound), jsonSettings.JsonSerializerSettings));
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
        [Route("GetOpenVLPDNos")]
        public async Task<string> GetOpenVLPDNos(WMSCoreMessage oRequest)
        {
            try
            {
                DataSet userDataTable = null;
                List<OutboundDTO> _lstoutbound = new List<OutboundDTO>();

                if (oRequest != null)
                {
                    OutboundDTO _ooutboundDTO = (OutboundDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);

                    if (_ooutboundDTO != null)
                    {
                        BO.VLPD oVLPD = new BO.VLPD()
                        {
                            AccountId = ConversionUtility.ConvertToInt(_ooutboundDTO.AccountID)
                        };

                        userDataTable = await _Outbound.GetOpenVLPDNos(oVLPD);
                    }
                    //foreach (DataRow row in userDataTable.Table.Rows)
                    //{
                    //    OutboundDTO responseDTO = new OutboundDTO();
                    //    _ooutboundDTO.VLPDNumber = row["VLPDNumber"].ToString();
                    //    _ooutboundDTO.VLPDId = row["Id"].ToString();
                    //    _lstoutbound.Add(_ooutboundDTO);
                    //}

                    foreach (DataRow row in userDataTable.Tables[0].Rows)
                    {
                        OutboundDTO vlpdresponseDTO = new OutboundDTO();
                        vlpdresponseDTO.VLPDNumber = row["VLPDNumber"].ToString();
                        vlpdresponseDTO.VLPDId = row["Id"].ToString();
                        _lstoutbound.Add(vlpdresponseDTO);
                    }

                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Outbound, _lstoutbound), jsonSettings.JsonSerializerSettings));
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
        [Route("GetItemToPick")]
        public async Task<string> GetItemToPick(WMSCoreMessage oRequest)
        {
            try
            {
                BO.VLPD oOutboundresponse = new BO.VLPD();
                OutboundDTO responseDTO = new OutboundDTO();
                List<OutboundDTO> _lstoutbound = new List<OutboundDTO>();

                if (oRequest != null)
                {
                    OutboundDTO _ooutboundDTO = (OutboundDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);

                    if (_ooutboundDTO != null)
                    {
                        BO.VLPD oVLPD = new BO.VLPD()
                        {
                            UserId = ConversionUtility.ConvertToInt(_ooutboundDTO.UserId),
                            VLPDId = ConversionUtility.ConvertToInt(_ooutboundDTO.VLPDId),
                            TransferRequestId = _ooutboundDTO.TransferRequestId
                        };

                        oOutboundresponse = await _Outbound.GetItemToPick(oVLPD);

                        if (oOutboundresponse != null)
                        {
                            responseDTO.VLPDNumber = oOutboundresponse.VLPDNo;
                            responseDTO.AssignedID = oOutboundresponse.Assignedid.ToString();
                            responseDTO.MaterialMasterId = oOutboundresponse.MaterialMasterId.ToString();
                            responseDTO.SKU = oOutboundresponse.MCode;
                            responseDTO.MaterialDescription = oOutboundresponse.MDescription;
                            responseDTO.PalletNo = oOutboundresponse.FromCartonCode;
                            responseDTO.CartonID = oOutboundresponse.FromCartonID.ToString();
                            responseDTO.Location = oOutboundresponse.Location;
                            responseDTO.LocationId = oOutboundresponse.LocationID.ToString();
                            responseDTO.MfgDate = oOutboundresponse.MfgDate;
                            responseDTO.ExpDate = oOutboundresponse.ExpDate;
                            responseDTO.SerialNo = oOutboundresponse.SerialNo;
                            responseDTO.BatchNo = oOutboundresponse.BatchNo;
                            responseDTO.ProjectNo = oOutboundresponse.ProjectRefNo;
                            responseDTO.AssignedQuantity = oOutboundresponse.AssignedQuantity;
                            responseDTO.PickedQty = oOutboundresponse.PickedQty;
                            responseDTO.OutboundID = oOutboundresponse.OutboundID;
                            responseDTO.SODetailsID = oOutboundresponse.SODetailsID;
                            responseDTO.SLocId = oOutboundresponse.StorageLocationID;
                            responseDTO.SLoc = oOutboundresponse.StorageLocation;
                            responseDTO.GoodsmomentDeatilsId = oOutboundresponse.GoodsmomentDeatilsId.ToString();
                            responseDTO.Lineno = oOutboundresponse.Lineno;
                            responseDTO.PendingQty = oOutboundresponse.PendingQty;
                            responseDTO.TransferRequestDetailsId = oOutboundresponse.TransferRequestDetailsId;
                            responseDTO.TransferRequestId = oOutboundresponse.TransferRequestId;
                            responseDTO.MRP = oOutboundresponse.MRP;
                        }
                        _lstoutbound.Add(responseDTO);
                    }

                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Outbound, _lstoutbound), jsonSettings.JsonSerializerSettings));
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
        [Route("VLPDSkipItem")]
        public async Task<string> VLPDSkipItem(WMSCoreMessage oRequest)
        {
            try
            {
                BO.VLPD ooutboundresponse = new BO.VLPD();
                OutboundDTO responseDTO = new OutboundDTO();
                List<OutboundDTO> _lstoutbound = new List<OutboundDTO>();

                if (oRequest != null)
                {
                    OutboundDTO _ooutboundDTO = (OutboundDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);

                    if (_ooutboundDTO != null)
                    {
                        BO.VLPD ooutbound = new BO.VLPD()
                        {
                            TotalPickedQty = ConversionUtility.ConvertToDecimal(_ooutboundDTO.PickedQty),
                            Flag = 1,
                            SkipReason = _ooutboundDTO.SkipReason,
                            VLPDId = ConversionUtility.ConvertToInt(_ooutboundDTO.VLPDId),
                            SkipQty = ConversionUtility.ConvertToDecimal(_ooutboundDTO.SkipQty),
                            Assignedid = Convert.ToInt32(_ooutboundDTO.AssignedID),
                            MCode = _ooutboundDTO.SKU,
                            MfgDate = _ooutboundDTO.MfgDate,
                            ExpDate = _ooutboundDTO.ExpDate,
                            BatchNo = _ooutboundDTO.BatchNo,
                            SerialNo = _ooutboundDTO.SerialNo,
                            ProjectRefNo = _ooutboundDTO.ProjectNo,
                            UserId = Convert.ToInt32(_ooutboundDTO.UserId),
                            StorageLocation = _ooutboundDTO.SLoc,
                            FromCartonCode = _ooutboundDTO.PalletNo,
                            Location = _ooutboundDTO.Location
                        };

                        ooutboundresponse = await _Outbound.VLPDSkipItem(ooutbound);

                        if (ooutboundresponse.Result == "Success")
                        {
                            return await GetItemToPick(oRequest);
                        }
                        else
                        {
                            throw new WMSExceptionMessage() { WMSExceptionCode = ooutboundresponse.Erocode, WMSMessage = ooutboundresponse.Result, ShowAsError = true };
                        }
                    }

                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Outbound, _lstoutbound), jsonSettings.JsonSerializerSettings));
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
        [Route("UpsertPickItem")]
        public async Task<string> UpsertPickItem(WMSCoreMessage oRequest)
        {
            try
            {
                BO.VLPD oOutboundresponse = new BO.VLPD();
                OutboundDTO responseDTO = new OutboundDTO();
                List<OutboundDTO> _lstoutbound = new List<OutboundDTO>();

                if (oRequest != null)
                {
                    OutboundDTO _ooutboundDTO = (OutboundDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);

                    if (_ooutboundDTO != null)
                    {
                        BO.VLPD oOutbound = new BO.VLPD()
                        {
                            Location = _ooutboundDTO.Location,
                            MfgDate = _ooutboundDTO.MfgDate,
                            ExpDate = _ooutboundDTO.ExpDate,
                            BatchNo = _ooutboundDTO.BatchNo,
                            ProjectRefNo = _ooutboundDTO.ProjectNo,
                            SerialNo = _ooutboundDTO.SerialNo,
                            PickedQty = _ooutboundDTO.PickedQty,
                            Assignedid = Convert.ToInt32(_ooutboundDTO.AssignedID),
                            MaterialMasterId = Convert.ToInt32(_ooutboundDTO.MaterialMasterId),
                            UserId = Convert.ToInt32(_ooutboundDTO.UserId),
                            LocationID = Convert.ToInt32(_ooutboundDTO.LocationId),
                            FromCartonID = Convert.ToInt32(_ooutboundDTO.CartonID),
                            VLPDId = Convert.ToInt32(_ooutboundDTO.VLPDId),
                            OutboundID = _ooutboundDTO.OutboundID,
                            SODetailsID = _ooutboundDTO.SODetailsID,
                            // StorageLocation=_ooutboundDTO.SLoc,
                            StorageLocationID = _ooutboundDTO.SLocId,
                            TransferRequestId = Convert.ToInt32(_ooutboundDTO.TransferRequestId),
                            TransferRequestDetailsId = Convert.ToInt32(_ooutboundDTO.TransferRequestDetailsId),
                            MCode = _ooutboundDTO.SKU,
                            ToCartonCode = _ooutboundDTO.ToCartonNO,
                            MRP = _ooutboundDTO.MRP
                        };

                        oOutboundresponse = await _Outbound.UpsertPickItem(oOutbound);

                        if (oOutboundresponse != null)
                        {
                            responseDTO.VLPDNumber = oOutboundresponse.VLPDNo;
                            responseDTO.AssignedID = oOutboundresponse.Assignedid.ToString();
                            responseDTO.MaterialMasterId = oOutboundresponse.MaterialMasterId.ToString();
                            responseDTO.SKU = oOutboundresponse.MCode;
                            responseDTO.MaterialDescription = oOutboundresponse.MDescription;
                            responseDTO.PalletNo = oOutboundresponse.FromCartonCode;
                            responseDTO.CartonID = oOutboundresponse.FromCartonID.ToString();
                            responseDTO.Location = oOutboundresponse.Location;
                            responseDTO.LocationId = oOutboundresponse.LocationID.ToString();
                            responseDTO.MfgDate = oOutboundresponse.MfgDate;
                            responseDTO.ExpDate = oOutboundresponse.ExpDate;
                            responseDTO.SerialNo = oOutboundresponse.SerialNo;
                            responseDTO.BatchNo = oOutboundresponse.BatchNo;
                            responseDTO.ProjectNo = oOutboundresponse.ProjectRefNo;
                            responseDTO.AssignedQuantity = oOutboundresponse.AssignedQuantity;
                            responseDTO.PickedQty = oOutboundresponse.PickedQty;
                            responseDTO.OutboundID = oOutboundresponse.OutboundID;
                            responseDTO.SODetailsID = oOutboundresponse.SODetailsID;
                            responseDTO.SLocId = oOutboundresponse.StorageLocationID;
                            responseDTO.SLoc = oOutboundresponse.StorageLocation;
                            responseDTO.GoodsmomentDeatilsId = oOutboundresponse.GoodsmomentDeatilsId.ToString();
                            responseDTO.Lineno = oOutboundresponse.Lineno;
                            responseDTO.PendingQty = oOutboundresponse.PendingQty;
                            responseDTO.MRP = oOutboundresponse.MRP;

                            if (oOutboundresponse.Result == "-444")
                            {
                                throw new WMSExceptionMessage() { WMSExceptionCode = "EMC_OB_DAL_010", WMSMessage = ErrorMessages.EMC_OB_DAL_010, ShowAsError = true };
                            }
                        }
                        _lstoutbound.Add(responseDTO);
                    }

                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Outbound, _lstoutbound), jsonSettings.JsonSerializerSettings));
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
        [Route("GetVLPDPickedList")]
        public string GetVLPDPickedList(WMSCoreMessage oRequest)
        {
            try
            {
                DataSet userDataTable = null;
                string Result = "0";
                List<OutboundDTO> _lstoutbound = new List<OutboundDTO>();

                if (oRequest != null)
                {
                    OutboundDTO _ooutboundDTO = (OutboundDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);

                    if (_ooutboundDTO != null)
                    {
                        BO.VLPD oVLPD = new BO.VLPD()
                        {

                            VLPDId = ConversionUtility.ConvertToInt(_ooutboundDTO.VLPDId),
                            MCode = _ooutboundDTO.SKU,
                            MfgDate = _ooutboundDTO.MfgDate,
                            ExpDate = _ooutboundDTO.ExpDate,
                            BatchNo = _ooutboundDTO.BatchNo,
                            SerialNo = _ooutboundDTO.SerialNo,
                            ProjectRefNo = _ooutboundDTO.ProjectNo,
                            MRP = _ooutboundDTO.MRP
                        };

                        userDataTable =  _Outbound.GetVLPDPickedList(oVLPD, out Result);
                    }

                    if (Result == "-1")
                    {
                        throw new WMSExceptionMessage() { WMSExceptionCode = null, WMSMessage = "Scanned SKU does not belong to the VLPD", ShowAsError = true };
                    }

                    foreach (DataRow row in userDataTable.Tables[0].Rows)
                    {
                        OutboundDTO responseDTO = new OutboundDTO();
                        responseDTO.PickedId = row["PickedID"].ToString();
                        responseDTO.MaterialMasterId = row["MaterialMasterID"].ToString();
                        responseDTO.SKU = row["MCode"].ToString();
                        responseDTO.LocationId = row["LocationID"].ToString();
                        responseDTO.Location = row["Location"].ToString();
                        responseDTO.CartonID = row["CartonID"].ToString();
                        responseDTO.PalletNo = row["CartonCode"].ToString();
                        responseDTO.PickedQty = row["PickedQty"].ToString();
                        _lstoutbound.Add(responseDTO);
                    }

                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Outbound, _lstoutbound), jsonSettings.JsonSerializerSettings));
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
        [Route("GetOBDPickedList")]
        public string GetOBDPickedList(WMSCoreMessage oRequest)
        {
            try
            {
                DataSet userDataTable = null;
                string Result = "0";
                List<OutboundDTO> _lstoutbound = new List<OutboundDTO>();

                if (oRequest != null)
                {
                    OutboundDTO _ooutboundDTO = (OutboundDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);

                    if (_ooutboundDTO != null)
                    {
                        BO.Outbound _oOutbound = new BO.Outbound()
                        {

                            OutboundId = _ooutboundDTO.OutboundID,
                            MCode = _ooutboundDTO.SKU,
                            MfgDate = _ooutboundDTO.MfgDate,
                            ExpDate = _ooutboundDTO.ExpDate,
                            BatchNo = _ooutboundDTO.BatchNo,
                            SerialNo = _ooutboundDTO.SerialNo,
                            ProjectNo = _ooutboundDTO.ProjectNo,
                            MRP = _ooutboundDTO.MRP
                        };

                        userDataTable =  _Outbound.GetOBDPickedList(_oOutbound, out Result);
                    }

                    if (Result == "-1")
                    {
                        throw new WMSExceptionMessage() { WMSExceptionCode = null, WMSMessage = "Scanned SKU does not belong to the Outbound", ShowAsError = true };
                    }

                    foreach (DataRow row in userDataTable.Tables[0].Rows)
                    {
                        OutboundDTO responseDTO = new OutboundDTO();
                        responseDTO.PickedId = row["PickedID"].ToString();
                        responseDTO.MaterialMasterId = row["MaterialMasterID"].ToString();
                        responseDTO.SKU = row["MCode"].ToString();
                        responseDTO.LocationId = row["LocationID"].ToString();
                        responseDTO.Location = row["Location"].ToString();
                        responseDTO.CartonID = row["CartonID"].ToString();
                        responseDTO.PalletNo = row["CartonCode"].ToString();
                        responseDTO.PickedQty = row["PickedQty"].ToString();
                        _lstoutbound.Add(responseDTO);
                    }

                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Outbound, _lstoutbound), jsonSettings.JsonSerializerSettings));
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
        [Route("DeleteVLPDPickedItems")]
        public async Task<string> DeleteVLPDPickedItems(WMSCoreMessage oRequest)
        {
            try
            {
                BO.VLPD oVLPd = new BO.VLPD();
                List<OutboundDTO> _lstoutbound = new List<OutboundDTO>();

                if (oRequest != null)
                {
                    OutboundDTO _ooutboundDTO = (OutboundDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);

                    if (_ooutboundDTO != null)
                    {
                        BO.VLPD oVLPD = new BO.VLPD()
                        {
                            VLPDId = ConversionUtility.ConvertToInt(_ooutboundDTO.VLPDId),
                            OutboundID = _ooutboundDTO.OutboundID,
                            PickedId = ConversionUtility.ConvertToInt(_ooutboundDTO.PickedId),
                            UserId = ConversionUtility.ConvertToInt(_ooutboundDTO.UserId),
                            MCode = _ooutboundDTO.SKU
                        };

                        oVLPd = await _Outbound.DeleteVLPDPickedItems(oVLPD);
                    }

                    if (oVLPd.Result == "Deleted successfully")
                    {
                        if (oVLPd.VLPDId != 0)
                        {
                            return GetVLPDPickedList(oRequest);
                        }
                        else
                        {
                            return GetOBDPickedList(oRequest);
                        }
                    }
                    else
                    {
                        throw new WMSExceptionMessage() { WMSExceptionCode = oVLPd.Result, WMSMessage = oVLPd.Result, ShowAsError = true };
                    }

                    //var jsonSerializerSettings = new JsonSerializerSettings
                    //{
                    //    Converters = { new StringEnumConverter() },
                    //    NullValueHandling = NullValueHandling.Include,
                    //};
                    //string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Outbound, _lstoutbound),jsonSerializerSettings));
                    //return json;
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
        [Route("GetOpenVLPDNosForSorting")]
        public async Task<string> GetOpenVLPDNosForSorting(WMSCoreMessage oRequest)
        {
            try
            {
                DataSet userDataTable = null;
                List<OutboundDTO> _lstoutbound = new List<OutboundDTO>();

                if (oRequest != null)
                {
                    OutboundDTO _ooutboundDTO = (OutboundDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);

                    if (_ooutboundDTO != null)
                    {
                        BO.VLPD oVLPD = new BO.VLPD()
                        {
                            AccountId = ConversionUtility.ConvertToInt(_ooutboundDTO.AccountID)
                        };

                        userDataTable = await _Outbound.GetOpenVLPDNosForSorting(oVLPD);
                    }

                    foreach (DataRow row in userDataTable.Tables[0].Rows)
                    {
                        OutboundDTO vlpdresponseDTO = new OutboundDTO();
                        vlpdresponseDTO.VLPDNumber = row["VLPDNumber"].ToString();
                        vlpdresponseDTO.VLPDId = row["Id"].ToString();
                        _lstoutbound.Add(vlpdresponseDTO);
                    }

                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Outbound, _lstoutbound), jsonSettings.JsonSerializerSettings));
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
        [Route("GetOpenLoadsheetList")]
        public async Task<string> GetOpenLoadsheetList(WMSCoreMessage oRequest)
        {
            try
            {
                DataSet userDataTable = null;
                List<OutboundDTO> _lstoutbound = new List<OutboundDTO>();

                if (oRequest != null)
                {
                    OutboundDTO _ooutboundDTO = (OutboundDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);

                    if (_ooutboundDTO != null)
                    {
                        string Tenantid = _ooutboundDTO.TenatID;
                        BO.VLPD oVLPD = new BO.VLPD()
                        {
                            AccountId = ConversionUtility.ConvertToInt(_ooutboundDTO.AccountID),
                            UserId = ConversionUtility.ConvertToInt(_ooutboundDTO.UserId)
                        };
                        //oFalcon.GetOpenLoadsheetList

                        userDataTable = await _Outbound.GetOpenLoadsheetList(Tenantid, oVLPD.AccountId.ToString());
                    }
                    //foreach (DataRow row in userDataTable.Table.Rows)
                    //{
                    //    OutboundDTO responseDTO = new OutboundDTO();
                    //    _ooutboundDTO.VLPDNumber = row["VLPDNumber"].ToString();
                    //    _ooutboundDTO.VLPDId = row["Id"].ToString();
                    //    _lstoutbound.Add(_ooutboundDTO);
                    //}

                    foreach (DataRow row in userDataTable.Tables[0].Rows)
                    {
                        OutboundDTO vlpdresponseDTO = new OutboundDTO();
                        vlpdresponseDTO.VLPDNumber = row["LoadSheetNo"].ToString();
                        vlpdresponseDTO.VLPDId = row["LoadSheetId"].ToString();
                        _lstoutbound.Add(vlpdresponseDTO);
                    }
               
                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Outbound, _lstoutbound), jsonSettings.JsonSerializerSettings));
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
        [Route("GetPendingOBDListForLoading")]
        public async Task<string> GetPendingOBDListForLoading(WMSCoreMessage oRequest)
        {
            try
            {
                DataSet userDataTable = null;
                List<OutboundDTO> _lstoutbound = new List<OutboundDTO>();

                if (oRequest != null)
                {
                    OutboundDTO _ooutboundDTO = (OutboundDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);

                    if (_ooutboundDTO != null)
                    {
                        string Tenantid = _ooutboundDTO.TenatID;
                        BO.VLPD oVLPD = new BO.VLPD()
                        {
                            AccountId = ConversionUtility.ConvertToInt(_ooutboundDTO.AccountID)
                        };
                        //oFalcon.GetOpenLoadsheetList

                        userDataTable = await _Outbound.GetPendingOBDListForLoading(Tenantid, oVLPD.AccountId.ToString());
                    }
                    //foreach (DataRow row in userDataTable.Table.Rows)
                    //{
                    //    OutboundDTO responseDTO = new OutboundDTO();
                    //    _ooutboundDTO.VLPDNumber = row["VLPDNumber"].ToString();
                    //    _ooutboundDTO.VLPDId = row["Id"].ToString();
                    //    _lstoutbound.Add(_ooutboundDTO);
                    //}

                    foreach (DataRow row in userDataTable.Tables[0].Rows)
                    {
                        OutboundDTO vlpdresponseDTO = new OutboundDTO();
                        vlpdresponseDTO.OBDNumber = row["OBDNumber"].ToString();
                        vlpdresponseDTO.OutboundID = row["OutboundID"].ToString();
                        _lstoutbound.Add(vlpdresponseDTO);
                    }

                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Outbound, _lstoutbound), jsonSettings.JsonSerializerSettings));
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
        [Route("UpsertLoadCreated")]
        public async Task<string> UpsertLoadCreated(WMSCoreMessage oRequest)
        {
            try
            {
                BO.VLPD oOutboundresponse = new BO.VLPD();
                OutboundDTO responseDTO = new OutboundDTO();
                List<OutboundDTO> _lstoutbound = new List<OutboundDTO>();

                if (oRequest != null)
                {
                    OutboundDTO _ooutboundDTO = (OutboundDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);

                    if (_ooutboundDTO != null)
                    {
                        BO.VLPD oVLPD = new BO.VLPD()
                        {
                            UserId = ConversionUtility.ConvertToInt(_ooutboundDTO.UserId),
                            TenantId = (_ooutboundDTO.TenatID),
                            Vehicle = _ooutboundDTO.Vehicle,
                            OBDNumber = _ooutboundDTO.OBDNumber,
                            DriverNo = _ooutboundDTO.DriverNo,
                            DriverName = _ooutboundDTO.DriverName,
                            LRnumber = _ooutboundDTO.LRnumber
                        };

                        string Result = "";
                        Result = await _Outbound.UpsertLoadCreated(oVLPD);

                        responseDTO.Result = Result;
                        _lstoutbound.Add(responseDTO);
                    }
       
                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Outbound, _lstoutbound), jsonSettings.JsonSerializerSettings));
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
        [Route("UpsertLoad")]
        public async Task<string> UpsertLoad(WMSCoreMessage oRequest)
        {
            try
            {
                BO.VLPD oOutboundresponse = new BO.VLPD();
                OutboundDTO responseDTO = new OutboundDTO();
                List<OutboundDTO> _lstoutbound = new List<OutboundDTO>();

                if (oRequest != null)
                {
                    OutboundDTO _ooutboundDTO = (OutboundDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);

                    if (_ooutboundDTO != null)
                    {
                        BO.VLPD oVLPD = new BO.VLPD()
                        {
                            UserId = ConversionUtility.ConvertToInt(_ooutboundDTO.UserId),
                            MCode = (_ooutboundDTO.MCode),
                            VLPDNo = _ooutboundDTO.VLPDNumber,
                            MfgDate = _ooutboundDTO.MfgDate,
                            ExpDate = _ooutboundDTO.ExpDate,
                            BatchNo = _ooutboundDTO.BatchNo,
                            SerialNo = _ooutboundDTO.SerialNo,
                            ProjectRefNo = _ooutboundDTO.ProjectNo,
                            MRP = _ooutboundDTO.MRP,
                            PickedQty = _ooutboundDTO.PickedQty
                        };

                        string Result = "";
                        Result = await _Outbound.UpsertLoad(oVLPD);

                        responseDTO.Result = Result;
                        _lstoutbound.Add(responseDTO);
                    }

                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Outbound, _lstoutbound), jsonSettings.JsonSerializerSettings));
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
        [Route("LoadVerification")]
        public async Task<string> LoadVerification(WMSCoreMessage oRequest)
        {
            try
            {
                BO.VLPD oOutboundresponse = new BO.VLPD();
                OutboundDTO responseDTO = new OutboundDTO();
                List<OutboundDTO> _lstoutbound = new List<OutboundDTO>();

                if (oRequest != null)
                {
                    OutboundDTO _ooutboundDTO = (OutboundDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);

                    if (_ooutboundDTO != null)
                    {
                        string Result = "";
                        Result = await _Outbound.LoadVerification(_ooutboundDTO.VLPDNumber, _ooutboundDTO.UserId);

                        responseDTO.Result = Result;
                        _lstoutbound.Add(responseDTO);
                    }

                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Outbound, _lstoutbound), jsonSettings.JsonSerializerSettings));
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
        [Route("ScanSONumberForPacking")]
        public async Task<string> ScanSONumberForPacking(WMSCoreMessage oRequest)
        {
            try
            {
                OutboundDTO _ooutboundDTO = (OutboundDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);
                string SONumber = _ooutboundDTO.SONumber;
                int AccountID = Convert.ToInt32(_ooutboundDTO.AccountID);
                int UserId = Convert.ToInt32(_ooutboundDTO.TenatID);

                Outbound oCustomer = new Outbound();

                List<Outbound> _lstcustomers = new List<Outbound>();
                List<OutboundDTO> _lstOutboundDTO = new List<OutboundDTO>();
                OutboundDTO _oOutboundResponse = new OutboundDTO();
                List<string> _PackingMcode = new List<string>();

                _lstcustomers = await _Outbound.GetMaterialsUnderSOForPacking(SONumber, AccountID, UserId);

                foreach (Outbound Loadsheetitem in _lstcustomers)
                {
                    OutboundDTO _outboundDTO = new OutboundDTO();
                    //_outboundDTO.MaterialMasterId = Loadsheetitem.MaterialMasterID.ToString();
                    _outboundDTO.MCode = Loadsheetitem.Mcode;
                    // _outboundDTO.SOQty = Loadsheetitem.SOQty.ToString();
                    _outboundDTO.PickedQty = Loadsheetitem.PickedQty.ToString();
                    _outboundDTO.PackedQty = Loadsheetitem.PackedQty.ToString();
                    //_outboundDTO.OutboundID = Loadsheetitem.OutboundID.ToString();
                    _outboundDTO.CustomerName = Loadsheetitem.CustomerName;
                    _outboundDTO.SOHeaderID = Loadsheetitem.SOHeaderID.ToString();
                    _outboundDTO.BusinessType = Loadsheetitem.BusinessType.ToString();
                    //_outboundDTO.OBDNumber = Loadsheetitem.OBDNumber.ToString();
                    //_outboundDTO.PSNID = Loadsheetitem.PSNID.ToString();
                    //_outboundDTO.PSNDetailsID = Loadsheetitem.PSNDetailsID.ToString();
                    _outboundDTO.MfgDate = Loadsheetitem.MFGDate;
                    _outboundDTO.ExpDate = Loadsheetitem.EXPDate;
                    _outboundDTO.SerialNo = Loadsheetitem.SerialNo;
                    _outboundDTO.ProjectNo = Loadsheetitem.ProjectRefNo;
                    _outboundDTO.BatchNo = Loadsheetitem.BatchNo;
                    _outboundDTO.MRP = Loadsheetitem.MRP;
                    _outboundDTO.HUNo = Loadsheetitem.HUNo;
                    _outboundDTO.HUSize = Loadsheetitem.HUSize;
                    //_outboundDTO.AccountID = Loadsheetitem.AccountID.ToString(); //============ Added By M.D.Prasad On 28-Aug-2020 for Account add1 ============//

                    _lstOutboundDTO.Add(_outboundDTO);
                }
           
                string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Inbound, _lstOutboundDTO), jsonSettings.JsonSerializerSettings));
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
        [Route("GETMSPsForPacking")]
        public async Task<string> GETMSPsForPacking(WMSCoreMessage oRequest)
        {
            try
            {
                OutboundDTO _ooutboundDTO = (OutboundDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);
                string sodetailsid = _ooutboundDTO.SODetailsID;

                Outbound oCustomer = new Outbound();

                List<Outbound> _lstcustomers = new List<Outbound>();
                List<OutboundDTO> _lstOutboundDTO = new List<OutboundDTO>();
                OutboundDTO _oOutboundResponse = new OutboundDTO();
                List<string> _PackingMcode = new List<string>();

                _lstcustomers = await _Outbound.GetMSPsForPacking(sodetailsid);

                foreach (Outbound Loadsheetitem in _lstcustomers)
                {
                    OutboundDTO _outboundDTO = new OutboundDTO();
                    _outboundDTO.MfgDate = Loadsheetitem.MFGDate;
                    _outboundDTO.ExpDate = Loadsheetitem.EXPDate;
                    _outboundDTO.SerialNo = Loadsheetitem.SerialNo;
                    _outboundDTO.ProjectNo = Loadsheetitem.ProjectRefNo;
                    _outboundDTO.BatchNo = Loadsheetitem.BatchNo;
                    _outboundDTO.MRP = Loadsheetitem.MRP;

                    _lstOutboundDTO.Add(_outboundDTO);
                }

                string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Inbound, _lstOutboundDTO), jsonSettings.JsonSerializerSettings));
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
        [Route("UpsertPackItem")]
        public async Task<string> UpsertPackItem(WMSCoreMessage oRequest)
        {
            try
            {
                OutboundDTO _ooutboundDTO = (OutboundDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);
                string sodetailsid = _ooutboundDTO.SODetailsID;
                Outbound oOutbound = new Outbound();

                List<Outbound> _lstcustomers = new List<Outbound>();
                List<OutboundDTO> _lstOutboundDTO = new List<OutboundDTO>();
                OutboundDTO _oOutboundResponse = new OutboundDTO();
                List<string> _PackingMcode = new List<string>();

                Outbound outboundItem = new Outbound()
                {
                    Mcode = _ooutboundDTO.MCode,
                    OutboundID = ConversionUtility.ConvertToInt(_ooutboundDTO.OutboundID),
                    PSNID = ConversionUtility.ConvertToInt(_ooutboundDTO.PSNID),
                    PickedQty = ConversionUtility.ConvertToDecimal(_ooutboundDTO.PickedQty),
                    PackedQty = ConversionUtility.ConvertToInt(_ooutboundDTO.PackedQty),
                    CartonSerialNo = _ooutboundDTO.CartonSerialNo,
                    MFGDate = _ooutboundDTO.MfgDate,
                    EXPDate = _ooutboundDTO.ExpDate,
                    SerialNo = _ooutboundDTO.SerialNo,
                    ProjectRefNo = _ooutboundDTO.ProjectNo,
                    BatchNo = _ooutboundDTO.BatchNo,
                    MRP = _ooutboundDTO.MRP,
                    PSNDetailsID = ConversionUtility.ConvertToInt(_ooutboundDTO.PSNDetailsID),
                    PackType = _ooutboundDTO.PackType,
                    SoDetailsID = ConversionUtility.ConvertToInt(_ooutboundDTO.SODetailsID),
                    SOHeaderID = ConversionUtility.ConvertToInt(_ooutboundDTO.SOHeaderID),
                    SONumber = _ooutboundDTO.SONumber,
                    AccountID = Convert.ToInt32(_ooutboundDTO.AccountID),
                    HUSize = _ooutboundDTO.HUSize,
                    HUNo = _ooutboundDTO.HUNo
                };

                oOutbound = await _Outbound.UpsertPackItem(outboundItem);

                OutboundDTO _outboundDTO = new OutboundDTO();

                _outboundDTO.PSNID = oOutbound.PSNID.ToString();
                _outboundDTO.PSNDetailsID = oOutbound.PSNDetailsID.ToString();
                _lstOutboundDTO.Add(_outboundDTO);
              
                string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Inbound, _lstOutboundDTO), jsonSettings.JsonSerializerSettings));
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
        [Route("UpdatePackComplete")]
        public async Task<string> UpdatePackComplete(WMSCoreMessage oRequest)
        {
            try
            {
                OutboundDTO _ooutboundDTO = (OutboundDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);
                string sodetailsid = _ooutboundDTO.SODetailsID;

                Outbound oOutbound = new Outbound();

                List<Outbound> _lstcustomers = new List<Outbound>();
                List<OutboundDTO> _lstOutboundDTO = new List<OutboundDTO>();
                OutboundDTO _oOutboundResponse = new OutboundDTO();
                List<string> _PackingMcode = new List<string>();

                Outbound outboundItem = new Outbound()
                {
                    SONumber = _ooutboundDTO.SONumber,
                    AccountID = Convert.ToInt32(_ooutboundDTO.AccountID)
                };

                oOutbound = await _Outbound.PackComplete(outboundItem);

                OutboundDTO _outboundDTO = new OutboundDTO();

                _outboundDTO.PackComplete = oOutbound.PackComplete.ToString();

                _lstOutboundDTO.Add(_outboundDTO);

                string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Inbound, _lstOutboundDTO), jsonSettings.JsonSerializerSettings));
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
        [Route("GenerateLoadSheet")]
        public async Task<string> GenerateLoadSheet(WMSCoreMessage oRequest)
        {
            try
            {
                BO.VLPD oOutboundresponse = new BO.VLPD();
                OutboundDTO responseDTO = new OutboundDTO();
                List<OutboundDTO> _lstoutbound = new List<OutboundDTO>();
                Outbound _oOutbound = new Outbound();

                List<SalesOrderDTO> _lstsalesorders = new List<SalesOrderDTO>();

                if (oRequest != null)
                {
                    OutboundDTO _ooutboundDTO = (OutboundDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);

                    if (_ooutboundDTO != null)
                    {
                        Outbound oOutbound = new Outbound()
                        {
                            UserId = ConversionUtility.ConvertToInt(_ooutboundDTO.UserId),
                            TenantId = _ooutboundDTO.TenatID,
                            Vehicle = _ooutboundDTO.Vehicle,
                            OBDNumber = _ooutboundDTO.OBDNumber,
                            DriverNo = _ooutboundDTO.DriverNo,
                            DriverName = _ooutboundDTO.DriverName,
                            LRnumber = _ooutboundDTO.LRnumber,
                            SONumber = _ooutboundDTO.SONumber,
                            AccountID = Convert.ToInt32(_ooutboundDTO.AccountID)
                        };

                        _oOutbound = await _Outbound.LoadSheetGeneration(oOutbound);

                        OutboundDTO _outboundDTO = new OutboundDTO();
                        _outboundDTO.LoadRefNo = _oOutbound.LoadRefNo;
                        _lstoutbound.Add(_outboundDTO);
                    }
               
                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Outbound, _lstoutbound), jsonSettings.JsonSerializerSettings));
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
        [Route("GetSOCountUnderLoadSheet")]
        public async Task<string> GetSOCountUnderLoadSheet(WMSCoreMessage oRequest)
        {
            try
            {
                BO.VLPD oOutboundresponse = new BO.VLPD();
                OutboundDTO responseDTO = new OutboundDTO();
                List<OutboundDTO> _lstoutbound = new List<OutboundDTO>();
                Outbound _oOutbound = new Outbound();

                List<SalesOrderDTO> _lstsalesorders = new List<SalesOrderDTO>();

                if (oRequest != null)
                {
                    OutboundDTO _ooutboundDTO = (OutboundDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);

                    if (_ooutboundDTO != null)
                    {
                        Outbound oOutbound = new Outbound()
                        {
                            LoadRefNo = _ooutboundDTO.LoadRefNo
                        };

                        _oOutbound = await _Outbound.GetSOCountUnderLoadSheet(oOutbound);

                        OutboundDTO _outboundDTO = new OutboundDTO();
                        _outboundDTO.BusinessType = _oOutbound.BusinessType;
                        _outboundDTO.TotalSOCount = _oOutbound.TotalSOCount.ToString();
                        _outboundDTO.ScannedSOCount = _oOutbound.ScannedSOCount.ToString();
                        _lstoutbound.Add(_outboundDTO);
                    }

                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Outbound, _lstoutbound), jsonSettings.JsonSerializerSettings));
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
        [Route("UpsertLoadDetails")]
        public async Task<string> UpsertLoadDetails(WMSCoreMessage oRequest)
        {
            try
            {
                BO.VLPD oOutboundresponse = new BO.VLPD();
                OutboundDTO responseDTO = new OutboundDTO();
                List<OutboundDTO> _lstoutbound = new List<OutboundDTO>();
                Outbound _oOutbound = new Outbound();

                List<SalesOrderDTO> _lstsalesorders = new List<SalesOrderDTO>();

                if (oRequest != null)
                {
                    OutboundDTO _ooutboundDTO = (OutboundDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);

                    if (_ooutboundDTO != null)
                    {
                        Outbound oOutbound = new Outbound()
                        {
                            LoadRefNo = _ooutboundDTO.LoadRefNo,
                            SONumber = _ooutboundDTO.SONumber,
                            CartonSerialNo = _ooutboundDTO.CartonSerialNo,
                            AccountID = Convert.ToInt32(_ooutboundDTO.AccountID)
                        };

                        _oOutbound = await _Outbound.UpsertLoadDetails(oOutbound);

                        OutboundDTO _outboundDTO = new OutboundDTO();
                        _outboundDTO.CustomerName = _oOutbound.CustomerName;
                        _outboundDTO.CustomerCode = _oOutbound.CustomerCode.ToString();
                        _outboundDTO.CustomerAddress = _oOutbound.CustomerAddress.ToString();
                        _lstoutbound.Add(_outboundDTO);
                    }

                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Outbound, _lstoutbound), jsonSettings.JsonSerializerSettings));
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
        [Route("GetPackingCartonInfo")]
        public async Task<string> GetPackingCartonInfo(WMSCoreMessage oRequest)
        {
            try
            {
                OutboundDTO responseDTO = new OutboundDTO();
                List<OutboundDTO> _lstoutboundDTO = new List<OutboundDTO>();
                Outbound _oOutbound = new Outbound();

                if (oRequest != null)
                {
                    List<Outbound> _lstOutbound = new List<Outbound>();
                    OutboundDTO _ooutboundDTO = (OutboundDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);

                    if (_ooutboundDTO != null)
                    {
                        Outbound outbound = new Outbound()
                        {
                            SONumber = _ooutboundDTO.SONumber,
                            CartonSerialNo = _ooutboundDTO.CartonSerialNo,
                            TenantId = _ooutboundDTO.TenatID,
                            WareHouseID = _ooutboundDTO.WareHouseID,
                            AccountID = Convert.ToInt32(_ooutboundDTO.AccountID),
                            UserId = Convert.ToInt32(_ooutboundDTO.UserId)
                        };
                        if (outbound.SONumber != "")
                        {
                            bool SOresult;
                            SOresult = await _Outbound.CheckSO(outbound);
                        }
                        if (outbound.CartonSerialNo != "")
                        {
                            bool CartonResult;
                            CartonResult = await _Outbound.CheckCarton(outbound);
                        }

                        _lstOutbound = await _Outbound.GetPackingCartonInfo(outbound);

                        foreach (Outbound outboundItem in _lstOutbound)
                        {
                            OutboundDTO outboundDTO = new OutboundDTO();
                            outboundDTO.CartonSerialNo = outboundItem.CartonSerialNo.ToString();
                            outboundDTO.MCode = outboundItem.Mcode.ToString();
                            outboundDTO.PickedQty = outboundItem.PickedQty.ToString();
                            outboundDTO.SONumber = outboundItem.SONumber.ToString();
                            _lstoutboundDTO.Add(outboundDTO);
                        }
                    }

                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Outbound, _lstoutboundDTO), jsonSettings.JsonSerializerSettings));
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
                ExceptionHandling.LogException(excp, _ClassCode + "001");
                return null;
            }
        }


        [HttpPost]
        [Route("GetRevertOBDList")]
        public async Task<string> GetRevertOBDList(WMSCoreMessage oRequest)
        {
            try
            {
                List<OutboundDTO> _lstoutboundDTO = new List<OutboundDTO>();

                if (oRequest != null)
                {
                    OutboundDTO _ooutboundDTO = (OutboundDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);
                    List<Outbound> _lstOutbound = new List<Outbound>();

                    if (_ooutboundDTO != null)
                    {
                        Outbound outbound = new Outbound()
                        {
                            AccountID = ConversionUtility.ConvertToInt(_ooutboundDTO.AccountID),
                            UserId = ConversionUtility.ConvertToInt(_ooutboundDTO.UserId)
                        };

                        _lstOutbound = await _Outbound.GetRevertOBDList(outbound);

                        foreach (Outbound outboundItem in _lstOutbound)
                        {
                            OutboundDTO outboundDTO = new OutboundDTO();
                            outboundDTO.OBDNumber = outboundItem.OBDNumber.ToString();
                            outboundDTO.OutboundID = outboundItem.OutboundID.ToString();

                            _lstoutboundDTO.Add(outboundDTO);
                        }
                    }

                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Outbound, _lstoutboundDTO), jsonSettings.JsonSerializerSettings));
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
                ExceptionHandling.LogException(excp, _ClassCode + "R001");
                return null;
            }
        }

        [HttpPost]
        [Route("GetRevertSOList")]
        public async Task<string> GetRevertSOList(WMSCoreMessage oRequest)
        {
            try
            {
                List<OutboundDTO> _lstoutboundDTO = new List<OutboundDTO>();

                if (oRequest != null)
                {
                    OutboundDTO _ooutboundDTO = (OutboundDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);

                    List<Outbound> _lstOutbound = new List<Outbound>();

                    if (_ooutboundDTO != null)
                    {
                        Outbound outbound = new Outbound()
                        {
                            AccountID = ConversionUtility.ConvertToInt(_ooutboundDTO.AccountID),
                            UserId = ConversionUtility.ConvertToInt(_ooutboundDTO.UserId),
                            OutboundID = ConversionUtility.ConvertToInt(_ooutboundDTO.OutboundID)
                        };

                        _lstOutbound = await _Outbound.GetRevertSOList(outbound);

                        foreach (Outbound outboundItem in _lstOutbound)
                        {
                            OutboundDTO outboundDTO = new OutboundDTO();
                            outboundDTO.SONumber = outboundItem.SONumber.ToString();
                            outboundDTO.SOHeaderID = outboundItem.SOHeaderID.ToString();

                            _lstoutboundDTO.Add(outboundDTO);
                        }
                    }

                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Outbound, _lstoutboundDTO), jsonSettings.JsonSerializerSettings));
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
                ExceptionHandling.LogException(excp, _ClassCode + "R002");
                return null;
            }
        }


        [HttpPost]
        [Route("GetRevertSOOBDInfo")]
        public async Task<string> GetRevertSOOBDInfo(WMSCoreMessage oRequest)
        {
            try
            {
                List<OutboundDTO> _lstoutboundDTO = new List<OutboundDTO>();

                if (oRequest != null)
                {
                    OutboundDTO _ooutboundDTO = (OutboundDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);
                    List<Outbound> _lstOutbound = new List<Outbound>();

                    if (_ooutboundDTO != null)
                    {
                        Outbound outbound = new Outbound()
                        {
                            AccountID = ConversionUtility.ConvertToInt(_ooutboundDTO.AccountID),
                            UserId = ConversionUtility.ConvertToInt(_ooutboundDTO.UserId),
                            OutboundID = ConversionUtility.ConvertToInt(_ooutboundDTO.OutboundID),
                            SONumber = _ooutboundDTO.SONumber
                        };

                        _lstOutbound = await _Outbound.GetRevertSOOBDInfo(outbound);

                        foreach (Outbound outboundItem in _lstOutbound)
                        {
                            OutboundDTO outboundDTO = new OutboundDTO();
                            outboundDTO.SONumber = outboundItem.SONumber.ToString();
                            outboundDTO.SOHeaderID = outboundItem.SOHeaderID.ToString();
                            outboundDTO.OutboundID = outboundItem.OutboundID.ToString();
                            outboundDTO.OBDNumber = outboundItem.OBDNumber.ToString();
                            outboundDTO.BusinessType = outboundItem.BusinessType.ToString();
                            outboundDTO.Status = outboundItem.Status.ToString();
                            _lstoutboundDTO.Add(outboundDTO);
                        }
                    }

                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Outbound, _lstoutboundDTO), jsonSettings.JsonSerializerSettings));
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
                ExceptionHandling.LogException(excp, _ClassCode + "R003");
                return null;
            }
        }


        [HttpPost]
        [Route("GetRevertCartonCheck")]
        public async Task<string> GetRevertCartonCheck(WMSCoreMessage oRequest)
        {
            try
            {
                List<OutboundDTO> _lstoutboundDTO = new List<OutboundDTO>();

                if (oRequest != null)
                {
                    OutboundDTO _ooutboundDTO = (OutboundDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);
                    List<Outbound> _lstOutbound = new List<Outbound>();
                    if (_ooutboundDTO != null)
                    {
                        Outbound outbound = new Outbound()
                        {
                            OutboundID = ConversionUtility.ConvertToInt(_ooutboundDTO.OutboundID),
                            SOHeaderID = ConversionUtility.ConvertToInt(_ooutboundDTO.SOHeaderID),
                            CartonSerialNo = _ooutboundDTO.CartonSerialNo
                        };
                        _lstOutbound = await _Outbound.GetRevertCartonCheck(outbound);

                        foreach (Outbound outboundItem in _lstOutbound)
                        {
                            OutboundDTO outboundDTO = new OutboundDTO();
                            outboundDTO.Status = outboundItem.Status.ToString();
                            _lstoutboundDTO.Add(outboundDTO);
                        }
                    }

                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Outbound, _lstoutboundDTO), jsonSettings.JsonSerializerSettings));
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
                ExceptionHandling.LogException(excp, _ClassCode + "R004");
                return null;
            }
        }


        [HttpPost]
        [Route("GetScanqtyvalidation")]
        public async Task<string> GetScanqtyvalidation(WMSCoreMessage oRequest)
        {
            try
            {
                List<OutboundDTO> _lstoutboundDTO = new List<OutboundDTO>();

                if (oRequest != null)
                {
                    OutboundDTO _ooutboundDTO = (OutboundDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);
                    List<Outbound> _lstOutbound = new List<Outbound>();
                    if (_ooutboundDTO != null)
                    {
                        Outbound outbound = new Outbound()
                        {
                            OutboundID = ConversionUtility.ConvertToInt(_ooutboundDTO.OutboundID),
                            SOHeaderID = ConversionUtility.ConvertToInt(_ooutboundDTO.SOHeaderID),
                            Mcode = _ooutboundDTO.MCode,
                            MFGDate = _ooutboundDTO.MfgDate,
                            EXPDate = _ooutboundDTO.ExpDate,
                            SerialNo = _ooutboundDTO.SerialNo,
                            ProjectRefNo = _ooutboundDTO.ProjectNo,
                            MRP = _ooutboundDTO.MRP,
                            BatchNo = _ooutboundDTO.BatchNo,
                            CartonSerialNo = _ooutboundDTO.CartonSerialNo
                        };
                        _lstOutbound = await _Outbound.GetScanqtyvalidation(outbound);

                        foreach (Outbound outboundItem in _lstOutbound)
                        {
                            OutboundDTO outboundDTO = new OutboundDTO();
                            outboundDTO.Status = outboundItem.Status.ToString();
                            outboundDTO.SOQty = outboundItem.SOQty.ToString();
                            _lstoutboundDTO.Add(outboundDTO);
                        }
                    }

                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Outbound, _lstoutboundDTO), jsonSettings.JsonSerializerSettings));
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
                ExceptionHandling.LogException(excp, _ClassCode + "R005");
                return null;
            }
        }


        [HttpPost]
        [Route("WorkOrderPicking")]
        public async Task<string> WorkOrderPicking(WMSCoreMessage oRequest)
        {
            try
            {
                WorkOrderOutbound oOutboundresponse = new WorkOrderOutbound();
                OutboundDTO responseDTO = new OutboundDTO();
                List<OutboundDTO> _lstoutbound = new List<OutboundDTO>();

                if (oRequest != null)
                {
                    OutboundDTO _ooutboundDTO = (OutboundDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);

                    if (_ooutboundDTO != null)
                    {
                        WorkOrderOutbound oOutbound = new WorkOrderOutbound()
                        {
                            AccountId = Convert.ToInt32(_ooutboundDTO.AccountID),
                            CartonNo = _ooutboundDTO.PalletNo,
                            Qty = ConversionUtility.ConvertToDecimal(_ooutboundDTO.PickedQty),
                            MCode = _ooutboundDTO.SKU,
                            CreatedBy = _ooutboundDTO.UserId,
                            MaterialStorageParameterIDs = "",
                            MaterialStorageParameterValues = "",
                            KitNo = _ooutboundDTO.KitId,
                            ToCartonNo = _ooutboundDTO.ToCartonNO,
                            SerialNo = _ooutboundDTO.SerialNo,
                            MfgDate = _ooutboundDTO.MfgDate,
                            ExpDate = _ooutboundDTO.ExpDate,
                            BatchNo = _ooutboundDTO.BatchNo,
                            ProjectNo = _ooutboundDTO.ProjectNo,
                            Assignedid = Convert.ToInt32(_ooutboundDTO.AssignedID),
                            Obdno = _ooutboundDTO.OBDNo,
                            Lineno = _ooutboundDTO.Lineno,
                            POSOHeaderId = _ooutboundDTO.POSOHeaderId,
                            Location = _ooutboundDTO.Location,
                            IsDam = _ooutboundDTO.IsDam,
                            HasDisc = _ooutboundDTO.HasDis,
                            OutboundId = _ooutboundDTO.OutboundID,
                            SODetailsID = _ooutboundDTO.SODetailsID,
                            MRP = _ooutboundDTO.MRP,
                            HUNo = ConversionUtility.ConvertToInt(_ooutboundDTO.HUNo),
                            HUSize = ConversionUtility.ConvertToInt(_ooutboundDTO.HUSize),
                            IsPSN = ConversionUtility.ConvertToInt(_ooutboundDTO.IsPSN),
                            PSN = _ooutboundDTO.PSN,
                            RID = _ooutboundDTO.RID
                        };
                        //FWMSC21Service.FWMSC21HHTWCFServiceClient oFalcon = new FWMSC21Service.FWMSC21HHTWCFServiceClient();
                        //oOutboundresponse = oFalcon.UpdatePickItem(oOutbound);
                        //OutboundBL outboundBL = new OutboundBL(LoggedInUserID, ConnectionString);

                        oOutboundresponse = await _Outbound.WorkOrderPicking(oOutbound);

                        if (oOutboundresponse != null)
                        {
                            if (oOutboundresponse.Result == "Success")
                            {
                                responseDTO.AssignedID = oOutboundresponse.Assignedid.ToString();
                                responseDTO.MaterialMasterId = oOutboundresponse.MaterialMasterId;
                                responseDTO.SKU = oOutboundresponse.MCode;
                                responseDTO.MaterialDescription = oOutboundresponse.MDescription;
                                responseDTO.PalletNo = oOutboundresponse.CartonNo;
                                responseDTO.CartonID = oOutboundresponse.CartonId;
                                responseDTO.Location = oOutboundresponse.Location;
                                responseDTO.LocationId = oOutboundresponse.LocationId.ToString();
                                responseDTO.MfgDate = oOutboundresponse.MfgDate;
                                responseDTO.ExpDate = oOutboundresponse.ExpDate;
                                responseDTO.SerialNo = oOutboundresponse.SerialNo;
                                responseDTO.BatchNo = oOutboundresponse.BatchNo;
                                responseDTO.ProjectNo = oOutboundresponse.ProjectNo;
                                responseDTO.AssignedQuantity = oOutboundresponse.AssignedQuantity;
                                responseDTO.PickedQty = oOutboundresponse.PickedQty;
                                responseDTO.OutboundID = oOutboundresponse.OutboundId;
                                responseDTO.SODetailsID = oOutboundresponse.SODetailsID;
                                responseDTO.SLocId = oOutboundresponse.SLocId;
                                responseDTO.SLoc = oOutboundresponse.SLoc;
                                responseDTO.GoodsmomentDeatilsId = oOutboundresponse.GoodsmomentDeatilsId.ToString();
                                responseDTO.Lineno = oOutboundresponse.Lineno;
                                responseDTO.MaterialMaster_IUoMID = oOutboundresponse.MaterialMaster_IUoMID;
                                responseDTO.CF = oOutboundresponse.CF.ToString();
                                responseDTO.POSOHeaderId = oOutboundresponse.POSOHeaderId;
                                responseDTO.PendingQty = oOutboundresponse.PendingQty;
                                responseDTO.Result = oOutboundresponse.Result;
                                responseDTO.MRP = oOutboundresponse.MRP;
                                responseDTO.HUNo = oOutboundresponse.HUNo.ToString();
                                responseDTO.HUSize = oOutboundresponse.HUSize.ToString();
                            }
                            else if (oOutboundresponse.Result == "-444")
                            {
                                throw new WMSExceptionMessage() { WMSExceptionCode = "EMC_OB_DAL_010", WMSMessage = ErrorMessages.EMC_OB_DAL_010, ShowAsError = true };
                            }
                            else if (oOutboundresponse.Result == "-333")
                            {
                                throw new WMSExceptionMessage() { WMSExceptionCode = "EMC_OB_DAL_011", WMSMessage = ErrorMessages.EMC_OB_DAL_011, ShowAsError = true };
                            }
                            else
                            {
                                throw new WMSExceptionMessage() { WMSExceptionCode = null, WMSMessage = oOutboundresponse.Result, ShowAsError = true };
                            }
                        }

                        _lstoutbound.Add(responseDTO);
                    }

                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Outbound, _lstoutbound), jsonSettings.JsonSerializerSettings));
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
        [Route("UpsertHHTOBDRevert")]
        public async Task<string> UpsertHHTOBDRevert(WMSCoreMessage oRequest)
        {
            try
            {
                List<OutboundDTO> _lstoutboundDTO = new List<OutboundDTO>();
                if (oRequest != null)
                {
                    OutboundDTO _ooutboundDTO = (OutboundDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);
                    List<Outbound> _lstOutbound = new List<Outbound>();
                    if (_ooutboundDTO != null)
                    {
                        Outbound outbound = new Outbound()
                        {
                            OutboundID = ConversionUtility.ConvertToInt(_ooutboundDTO.OutboundID),
                            SOHeaderID = ConversionUtility.ConvertToInt(_ooutboundDTO.SOHeaderID),
                            Mcode = _ooutboundDTO.MCode,
                            MFGDate = _ooutboundDTO.MfgDate,
                            EXPDate = _ooutboundDTO.ExpDate,
                            SerialNo = _ooutboundDTO.SerialNo,
                            ProjectRefNo = _ooutboundDTO.ProjectNo,
                            MRP = _ooutboundDTO.MRP,
                            BatchNo = _ooutboundDTO.BatchNo,
                            CartonSerialNo = _ooutboundDTO.CartonSerialNo,
                            PackedQty = Convert.ToDecimal(_ooutboundDTO.PackedQty),
                            UserId = Convert.ToInt32(_ooutboundDTO.UserId)
                        };
                        _lstOutbound = await _Outbound.UpsertHHTOBDRevert(outbound);

                        foreach (Outbound outboundItem in _lstOutbound)
                        {
                            OutboundDTO outboundDTO = new OutboundDTO();
                            outboundDTO.Status = outboundItem.Status.ToString();
                            _lstoutboundDTO.Add(outboundDTO);
                        }
                    }
         
                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Outbound, _lstoutboundDTO), jsonSettings.JsonSerializerSettings));
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
                ExceptionHandling.LogException(excp, _ClassCode + "R006");
                return null;
            }
        }


        [HttpPost]
        [Route("GetWORefNos")]
        public async Task<string> GetWORefNos(WMSCoreMessage oRequest)
        {
            try
            {
                List<OutboundDTO> _lstoutboundDTO = new List<OutboundDTO>();
                if (oRequest != null)
                {
                    OutboundDTO _ooutboundDTO = (OutboundDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);
                    List<Outbound> _lstOutbound = new List<Outbound>();
                    if (_ooutboundDTO != null)
                    {
                        Outbound outbound = new Outbound()
                        {
                            AccountID = ConversionUtility.ConvertToInt(_ooutboundDTO.AccountID),
                            UserId = ConversionUtility.ConvertToInt(_ooutboundDTO.UserId),
                            //IsSample = Convert.ToInt32(_ooutboundDTO.RID),
                            //IsWorkOrder = string.IsNullOrEmpty(_ooutboundDTO.IsWorkOrder) ? "0" : Convert.ToString(_ooutboundDTO.IsWorkOrder)
                        };

                        _lstOutbound = await _Outbound.Get_WOListToRevert(outbound);

                        foreach (Outbound outboundItem in _lstOutbound)
                        {
                            OutboundDTO outboundDTO = new OutboundDTO();
                            outboundDTO.OBDNo = outboundItem.OBDNumber.ToString();
                            outboundDTO.OutboundID = outboundItem.OutboundID.ToString();

                            _lstoutboundDTO.Add(outboundDTO);
                        }
                    }
                   
                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Outbound, _lstoutboundDTO), jsonSettings.JsonSerializerSettings));
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
        [Route("GetWORevertItemsForPicking")]
        public async Task<string> GetWORevertItemsForPicking(WMSCoreMessage oRequest)
        {
            try
            {
                OutboundDTO responseDTO = new OutboundDTO();
                List<OutboundDTO> _lstoutbound = new List<OutboundDTO>();

                if (oRequest != null)
                {
                    List<Outbound> _lstOutbound = new List<Outbound>();
                    OutboundDTO _ooutboundDTO = (OutboundDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);
                    if (_ooutboundDTO != null)
                    {
                        Outbound outbound = new Outbound()
                        {
                            OutboundID = ConversionUtility.ConvertToInt(_ooutboundDTO.OutboundID)
                        };

                        _lstOutbound = await _Outbound.GetWOItemsForPicking(outbound);

                        foreach (Outbound outboundItem in _lstOutbound)
                        {
                            OutboundDTO outboundDTO = new OutboundDTO();
                            outboundDTO.MaterialMasterId = outboundItem.MaterialMasterID.ToString();
                            outboundDTO.SKU = outboundItem.Mcode.ToString();
                            outboundDTO.MaterialDescription = outboundItem.MDescription.ToString();
                            outboundDTO.Qty = outboundItem.Quantity.ToString();
                            outboundDTO.BatchNo = Convert.ToString(outboundItem.BatchNo);
                            outboundDTO.ProjectNo = Convert.ToString(outboundItem.ProjectRefNo);
                            outboundDTO.ExpDate = Convert.ToString(outboundItem.ProjectRefNo);
                            outboundDTO.RevertQty = Convert.ToString(outboundItem.RevertQty);
                            outboundDTO.VLPDId = Convert.ToString(outboundItem.VLPDPickID);
                            outboundDTO.MfgDate = Convert.ToString(outboundItem.MFGDate.Split(' ')[0]);
                            outboundDTO.ExpDate = Convert.ToString(outboundItem.EXPDate.Split(' ')[0]);

                            _lstoutbound.Add(outboundDTO);
                        }
                    }

                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Outbound, _lstoutbound), jsonSettings.JsonSerializerSettings));
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
        [Route("UpsertHHTWORevert")]
        public async Task<string> UpsertHHTWORevert(WMSCoreMessage oRequest)
        {
            try
            {           
                List<OutboundDTO> _lstoutboundDTO = new List<OutboundDTO>();
                if (oRequest != null)
                {
                    OutboundDTO _ooutboundDTO = (OutboundDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);
                    List<OutboundDetails> _lstOutbound = new List<OutboundDetails>();
                    if (_ooutboundDTO != null)
                    {
                        OutboundDetails outbound = new OutboundDetails()
                        {
                            OutboundID = ConversionUtility.ConvertToInt(_ooutboundDTO.OutboundID),
                            //SOHeaderID = ConversionUtility.ConvertToInt(_ooutboundDTO.SOHeaderID),
                            Mcode = _ooutboundDTO.SKU,
                            MFGDate = _ooutboundDTO.MfgDate,
                            EXPDate = _ooutboundDTO.ExpDate,
                            SerialNo = _ooutboundDTO.SerialNo,
                            ProjectRefNo = _ooutboundDTO.ProjectNo,
                            Location = _ooutboundDTO.Location,
                            //MRP = _ooutboundDTO.MRP,
                            BatchNo = _ooutboundDTO.BatchNo,
                            CartonNo = _ooutboundDTO.CartonNo,
                            Qty = Convert.ToDecimal(_ooutboundDTO.Qty),
                            UserId = Convert.ToInt32(_ooutboundDTO.UserId),
                            VLPDPickID = Convert.ToInt32(_ooutboundDTO.VLPDId)
                        };

                        XmlSerializer serializer = new XmlSerializer(typeof(OutboundDetails));
                        string xmlString = null;
                        using (StringWriter writer = new StringWriter())
                        {
                            serializer.Serialize(writer, outbound);
                            string xml = writer.ToString();
                            xmlString = xml.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", " ");

                            _lstOutbound = await _Outbound.UpsertHHTWORevert(xmlString);
                        }

                        OutboundDTO outboundDTO = new OutboundDTO();

                        foreach (OutboundDetails outboundItem in _lstOutbound)
                        {
                            outboundDTO.Result = outboundItem.Result.ToString();
                            outboundDTO.PendingQty = outboundItem.PendingQty.ToString();
                            outboundDTO.RevertQty = outboundItem.RevertQty.ToString();
                            outboundDTO.Qty = outboundItem.Qty.ToString();
                            _lstoutboundDTO.Add(outboundDTO);
                        }
                        if (outboundDTO.Result == "-1")
                        {
                            throw new WMSExceptionMessage() { WMSExceptionCode = null, WMSMessage = "Error while Revert", ShowAsError = true };
                        }
                    }
             
                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Outbound, _lstoutboundDTO), jsonSettings.JsonSerializerSettings));
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
                ExceptionHandling.LogException(excp, _ClassCode + "R006");
                return null;
            }
        }
        [HttpPost]
        [Route("UpdateLoadComplete")]
        public async Task<string> UpdateLoadComplete(WMSCoreMessage oRequest)
        {
            try
            {
                OutboundDTO responseDTO = new OutboundDTO();
                List<OutboundDTO> _lstoutbound = new List<OutboundDTO>();
                OutboundDTO _ooutboundDTO = (OutboundDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);

                Outbound oOutbound = new Outbound();

                if (oRequest != null)
                {
                    Outbound outboundItem = new Outbound()
                    {
                        OutboundID = ConversionUtility.ConvertToInt(_ooutboundDTO.OutboundID),
                        AccountID = Convert.ToInt32(_ooutboundDTO.AccountID),
                        UserId = ConversionUtility.ConvertToInt(_ooutboundDTO.UserId)
                    };
                    oOutbound = await _Outbound.LoadComplete(outboundItem);

                    OutboundDTO _outboundDTO = new OutboundDTO();

                    _outboundDTO.LoadComplete = oOutbound.LoadComplete.ToString();

                    _lstoutbound.Add(_outboundDTO);

                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Outbound, _lstoutbound), jsonSettings.JsonSerializerSettings));
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
        [Route("UpsertLoading")]
        public async Task<string> UpsertLoading(WMSCoreMessage oRequest)
        {
            try
            {
                List<Outbound> oOutbound = new List<Outbound>();
                OutboundDTO responseDTO = new OutboundDTO();
                List<Outbound> _lstoutbound = new List<Outbound>();
                OutboundDTO _ooutboundDTO = (OutboundDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);

                if (oRequest != null)
                {
                    Outbound outboundItem = new Outbound()
                    {
                        AccountID = ConversionUtility.ConvertToInt(_ooutboundDTO.AccountID),
                        UserId = ConversionUtility.ConvertToInt(_ooutboundDTO.UserId),
                        OutboundID = ConversionUtility.ConvertToInt(_ooutboundDTO.OutboundID),
                        Vehicle = _ooutboundDTO.Vehicle,
                        ScanInput = _ooutboundDTO.ScanInput,
                        ActionType = _ooutboundDTO.ActionType,
                        CartonNo = _ooutboundDTO.CartonNo,
                        TenantId=_ooutboundDTO.TenantID
                    };
                    oOutbound = await _Outbound.UpsertLoadItem(outboundItem);

                    Outbound oobd = oOutbound[0];

                  /*  if (oobd.Result == "1")
                    {*/
                        Outbound outboundDTO = new Outbound();
                        outboundDTO.Result = oobd.Result.ToString();
                        outboundDTO.Loadqty = oobd.LoadQty.ToString();
                        outboundDTO.PickedQty = ConversionUtility.ConvertToDecimal(oobd.PickedQty.ToString());
                        outboundDTO.IsLoadComplete = ConversionUtility.ConvertToInt(oobd.IsLoadComplete.ToString());
                        outboundDTO.MCode = oobd.Mcode;
                        outboundDTO.BatchNo = oobd.BatchNo;
                        outboundDTO.Grade = oobd.Grade;
                        outboundDTO.UnLoadQty = oobd.UnLoadQty;
                        outboundDTO.SerialNo = oobd.LabelSerialNo;
                        outboundDTO.MDescription = oobd.MDescription;
                       _lstoutbound.Add(outboundDTO);

                    if (outboundDTO.IsLoadComplete==1)
                    {
                        try
                        {
                            WhatAppNotes notes = new();
                            notes.VechileNumber = _ooutboundDTO.Vehicle;
                            notes.ScenarioID = 3;
                            string response = await _whatsappservice.SendWAMBasedOnActivity(notes);
                        }
                        catch (Exception)
                        {

                            throw;
                        }

                    }

                    /*}
                    else
                    {

                        WMSExceptionMessage ex = new WMSExceptionMessage();
                        ex.WMSMessage = oobd.Result;


                        string json1 = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Exception, ex.WMSMessage), jsonSettings.JsonSerializerSettings));
                        return json1;


                    }*/
                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Outbound, _lstoutbound), jsonSettings.JsonSerializerSettings));
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
        [Route("GetSkuListForLoading")]
        public async Task<string> GetSkuListForLoading(WMSCoreMessage oRequest)
        {
            try
            {
                if (oRequest != null)
                {

                    OutboundDTO _ooutboundDTO = (OutboundDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);
                    string OutboundID = _ooutboundDTO.OutboundID;

                    int AccountID = Convert.ToInt32(_ooutboundDTO.AccountID);
                    int UserId = Convert.ToInt32(_ooutboundDTO.UserId);

                    Outbound oCustomer = new Outbound();

                    List<Outbound> _lstcustomers = new List<Outbound>();
                    List<OutboundDTO> _lstOutboundDTO = new List<OutboundDTO>();
                    OutboundDTO _oOutboundResponse = new OutboundDTO();
                    List<string> _PackingMcode = new List<string>();

                    _lstcustomers = await _Outbound.GetSkuListForLoading(OutboundID, AccountID, UserId);

                    foreach (Outbound Loadsheetitem in _lstcustomers)
                    {
                        OutboundDTO _outboundDTO = new OutboundDTO();

                        _outboundDTO.MCode = Loadsheetitem.Mcode;
                        _outboundDTO.PickedQty = Loadsheetitem.PickedQty.ToString();
                        _outboundDTO.Loadqty = Loadsheetitem.LoadQty.ToString();
                        _outboundDTO.SOHeaderID = Loadsheetitem.SOHeaderID.ToString();
                        _outboundDTO.MfgDate = Loadsheetitem.MFGDate;
                        _outboundDTO.ExpDate = Loadsheetitem.EXPDate;
                        _outboundDTO.SerialNo = Loadsheetitem.SerialNo;
                        _outboundDTO.ProjectNo = Loadsheetitem.ProjectRefNo;
                        _outboundDTO.BatchNo = Loadsheetitem.BatchNo;
                        _outboundDTO.MRP = Loadsheetitem.MRP;
                        _outboundDTO.HUNo = Loadsheetitem.HUNo;
                        _outboundDTO.HUSize = Loadsheetitem.HUSize;
                        _outboundDTO.TotalQty = Loadsheetitem.TotalQty;
                        _lstOutboundDTO.Add(_outboundDTO);
                    }
                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Outbound, _lstOutboundDTO), jsonSettings.JsonSerializerSettings));
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
