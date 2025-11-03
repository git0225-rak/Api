using Simpolo_Endpoint.BO;
using Simpolo_Endpoint.DAO.HHTInterface;
using Simpolo_Endpoint.DTO;
using Simpolo_Endpoint.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Simpolo_Endpoint.Entities;
using System.Globalization;
using FWMSC21Core.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Drawing;
using SixLabors.ImageSharp;
using System.Security.Cryptography;
using Nancy.Diagnostics;

namespace Simpolo_Endpoint.Controllers.HHTControllers
{
    [Route("[controller]")]
    //[Authorize]
    [ApiController]
    public class GroupOBDController : ControllerBase
    {

        private readonly IGroupOBD _GroupOBD;
        JsonSettings jsonSettings = new JsonSettings();
        public GroupOBDController(IGroupOBD groupOBD)
        {
            _GroupOBD = groupOBD;
        }
        private string _ClassCode = string.Empty;



        [HttpPost]
        [Route("GetItemsAgainstOBD")]
        public async Task<string> GetItemsAgainstOBD(WMSCoreMessage oRequest)
        {
            try
            {
                if (oRequest != null)
                {
                    dynamic obj = oRequest.EntityObject;
                    OutboundModel _obj = JsonConvert.DeserializeObject<OutboundModel>(obj.ToString());
                    List<OutboundModel> list = new List<OutboundModel>();

                    list = await _GroupOBD.GetItemsAgainstOBD(_obj);

                    return JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Inbound, list));
                }
                return null;
            }
            catch (WMSExceptionMessage ex)
            {
                List<WMSExceptionMessage> _lstwMSExceptionMessage = new List<WMSExceptionMessage>();
                _lstwMSExceptionMessage.Add(ex);
                return JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Exception, _lstwMSExceptionMessage), jsonSettings.JsonSerializerSettings));

            }
            catch (Exception excp)
            {
                ExceptionHandling.LogException(excp, _ClassCode + "001");
                return null;
            }
        }
        [HttpPost]
        [Route("GetVlpdNoByDock")]
        public async Task<string> GetVLPDNoByDock(WMSCoreMessage oRequest)
        {
            try
            {
                List<GroupOBDModel> _lstoutboundDTO = new List<GroupOBDModel>();

                if (oRequest != null)
                {
                    dynamic obj = oRequest.EntityObject;
                    GroupOBDModel _obj = JsonConvert.DeserializeObject<GroupOBDModel>(obj.ToString());

                    List<GroupOutbound> _lstOutbound = new List<GroupOutbound>();

                    if (_obj != null)
                    {
                        GroupOutbound outbound = new GroupOutbound()
                        {
                            AccountID = Convert.ToInt32(_obj.AccountID),
                            UserId = Convert.ToInt32(_obj.UserId),
                            Warehouseid = string.IsNullOrEmpty(Convert.ToString(_obj.WareHouseID)) ? "" : Convert.ToString(_obj.WareHouseID),
                            dock = _obj.DockNumber
                        };

                        _lstOutbound = await _GroupOBD.GetVLPDNosByDock(outbound);

                        foreach (GroupOutbound outboundItem in _lstOutbound)
                        {
                            GroupOBDModel outboundDTO = new GroupOBDModel();
                            outboundDTO.VLPDNumber = outboundItem.Vlpdnumber.ToString();
                            outboundDTO.VLPDId = Convert.ToString(outboundItem.Vlpdid);
                            outboundDTO.IsCustomLabel = Convert.ToString(outboundItem.IsCustomLabel);


                            _lstoutboundDTO.Add(outboundDTO);
                        }
                    }
                    return JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Outbound, _lstoutboundDTO));
                }
                else return null;
            }
            catch (WMSExceptionMessage ex)
            {
                List<WMSExceptionMessage> _lstwMSExceptionMessage = new List<WMSExceptionMessage>();
                _lstwMSExceptionMessage.Add(ex);
                return JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Exception, _lstwMSExceptionMessage), jsonSettings.JsonSerializerSettings));

            }
            catch (Exception excp)
            {
                ExceptionHandling.LogException(excp, _ClassCode + "001");
                return null;
            }
        }

        [HttpPost]
        [Route("GetZPLScriptsforOBDsorting")]
        public async Task<string> GetZPLScriptsforOBDsorting(WMSCoreMessage oRequest)
        {
            try
            {
                List<GroupOBDModel> _lstoutboundDTO = new List<GroupOBDModel>();

                if (oRequest != null)
                {
                    dynamic obj = oRequest.EntityObject;
                    GroupOBDModel _obj = JsonConvert.DeserializeObject<GroupOBDModel>(obj.ToString());

                    List<GroupOutbound> _lstOutbound = new List<GroupOutbound>();

                    if (_obj != null)
                    {
                        GroupOutbound outbound = new GroupOutbound()
                        {
                            AccountID = Convert.ToInt32(_obj.AccountID),
                            Vlpdnumber = _obj.VLPDNumber
                        };

                        _lstOutbound = await _GroupOBD.GetZPL_ScriptsforOBDsorting(outbound);

                        foreach (GroupOutbound outboundItem in _lstOutbound)
                        {
                            GroupOBDModel outboundDTO = new GroupOBDModel();
                            outboundDTO.ZplScript = outboundItem.ZplScript.ToString();
                            outboundDTO.OBDNumber = Convert.ToString(outboundItem.OBDNumber);
                            outboundDTO.SONumber = Convert.ToString(outboundItem.SONumber);
                            outboundDTO.CustomerName = Convert.ToString(outboundItem.CustomerName);

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
                return JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Exception, _lstwMSExceptionMessage), jsonSettings.JsonSerializerSettings));

            }
            catch (Exception excp)
            {
                ExceptionHandling.LogException(excp, _ClassCode + "001");
                return null;
            }
        }



        [HttpPost]
        [Route("GetVLPDNos")]
        public async Task<string> GetVLPDNos(WMSCoreMessage oRequest)
        {
            try
            {
                List<GroupOBDModel> _lstoutboundDTO = new List<GroupOBDModel>();

                if (oRequest != null)
                {
                    dynamic obj = oRequest.EntityObject;
                    GroupOBDModel _obj = JsonConvert.DeserializeObject<GroupOBDModel>(obj.ToString());

                    List<GroupOutbound> _lstOutbound = new List<GroupOutbound>();



                    if (_obj != null)
                    {
                        GroupOutbound outbound = new GroupOutbound()
                        {
                            AccountID = Convert.ToInt32(_obj.AccountID),
                            UserId = Convert.ToInt32(_obj.UserId),
                            WareHouseID = string.IsNullOrEmpty(Convert.ToString(_obj.WareHouseID)) ? "" : Convert.ToString(_obj.WareHouseID),
                            IsVLPD = _obj.IsVLPD,
                            TenantId = _obj.TenantID,
                            IsSorting = _obj.IsSorting

                        };

                        _lstOutbound = await _GroupOBD.GetVLPDNos(outbound);

                        foreach (GroupOutbound outboundItem in _lstOutbound)
                        {
                            GroupOBDModel outboundDTO = new GroupOBDModel();
                            outboundDTO.VLPDNumber = outboundItem.Vlpdnumber.ToString();
                            outboundDTO.VLPDId = Convert.ToString(outboundItem.Vlpdid);
                            outboundDTO.PickedQty = outboundItem.PickedQty.ToString();
                            outboundDTO.LoadQty = outboundItem.LoadQty;


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
                return JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Exception, _lstwMSExceptionMessage), jsonSettings.JsonSerializerSettings));

            }
            catch (Exception excp)
            {
                ExceptionHandling.LogException(excp, _ClassCode + "001");
                return null;
            }

        }





        [HttpPost]
        [Route("GetVLPDNosForSorting")]
        public async Task<string> GetVLPDNosForSorting(WMSCoreMessage oRequest)
        {
            try
            {
                List<GroupOBDModel> _lstoutboundDTO = new List<GroupOBDModel>();

                if (oRequest != null)
                {
                    dynamic obj = oRequest.EntityObject;
                    GroupOBDModel _obj = JsonConvert.DeserializeObject<GroupOBDModel>(obj.ToString());

                    List<GroupOutbound> _lstOutbound = new List<GroupOutbound>();



                    if (_obj != null)
                    {
                        GroupOutbound outbound = new GroupOutbound()
                        {
                            AccountID = Convert.ToInt32(_obj.AccountID),
                            UserId = Convert.ToInt32(_obj.UserId),
                            WareHouseID = string.IsNullOrEmpty(Convert.ToString(_obj.WareHouseID)) ? "" : Convert.ToString(_obj.WareHouseID),
                            TenantId = _obj.TenantID,


                        };

                        _lstOutbound = await _GroupOBD.GetVLPDNosForSorting(outbound);

                        foreach (GroupOutbound outboundItem in _lstOutbound)
                        {
                            GroupOBDModel outboundDTO = new GroupOBDModel();
                            outboundDTO.VLPDNumber = outboundItem.Vlpdnumber.ToString();
                            outboundDTO.VLPDId = Convert.ToString(outboundItem.Vlpdid);



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
                return JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Exception, _lstwMSExceptionMessage), jsonSettings.JsonSerializerSettings));

            }
            catch (Exception excp)
            {
                ExceptionHandling.LogException(excp, _ClassCode + "001");
                return null;
            }

        }



        [HttpPost]
        [Route("VLPDOBDItemToPick")]
        public async Task<string> VLPDOBDItemToPick(WMSCoreMessage oRequest)
        {
            try
            {
                //GroupOBDModel oOutboundresponse = new GroupOBDModel();
               // GroupOBDModel responseDTO = new GroupOBDModel();
                List<GroupOBDModel> _lstoutbound = new List<GroupOBDModel>();

                if (oRequest != null)
                {
                    dynamic obj = oRequest.EntityObject;
                    GroupOBDModel _ooutboundDTO = JsonConvert.DeserializeObject<GroupOBDModel>(obj.ToString());

                    if (_ooutboundDTO != null)
                    {
                        List<GroupOBDModel> oOutboundresponses = await _GroupOBD.VLPDOBDItemToPick(_ooutboundDTO);

                        if (oOutboundresponses != null && oOutboundresponses.Count > 0)
                        {
                            foreach (var oOutboundresponse in oOutboundresponses)
                            {
                                GroupOBDModel responseDTO = new GroupOBDModel(); 

                                responseDTO.AssignedID = oOutboundresponse.AssignedID;
                                responseDTO.MaterialMasterId = oOutboundresponse.MaterialMasterId;
                                responseDTO.SKU = oOutboundresponse.MCode;
                                responseDTO.MaterialDescription = oOutboundresponse.MaterialDescription;
                                responseDTO.PalletNo = oOutboundresponse.CartonNo;
                                responseDTO.CartonID = oOutboundresponse.CartonID;
                                responseDTO.Location = oOutboundresponse.Location;
                                responseDTO.LocationId = oOutboundresponse.LocationId;
                                responseDTO.MfgDate = !string.IsNullOrEmpty(oOutboundresponse.MfgDate)
                                    ? Convert.ToDateTime(oOutboundresponse.MfgDate).ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture)
                                    : "";
                                responseDTO.ExpDate = !string.IsNullOrEmpty(oOutboundresponse.ExpDate)
                                    ? Convert.ToDateTime(oOutboundresponse.ExpDate).ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture)
                                    : "";
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
                                responseDTO.UOM = oOutboundresponse.UOM;
                                responseDTO.GradeID = oOutboundresponse.GradeID;
                                responseDTO.Grade = oOutboundresponse.Grade;
                                responseDTO.Result = oOutboundresponse.Result;
                                responseDTO.IsPickingCompleted = oOutboundresponse.IsPickingCompleted;
                                responseDTO.OBDNumber = oOutboundresponse.OBDNumber;

                                _lstoutbound.Add(responseDTO);
                            }
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
                return JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Exception, _lstwMSExceptionMessage), jsonSettings.JsonSerializerSettings));

            }
            catch (Exception excp)
            {
                ExceptionHandling.LogException(excp, _ClassCode + "001");
                return null;
            }

        }



        [HttpPost]
        [Route("VLPDPickedItems")]

        public string VLPDPickedItems(WMSCoreMessage oRequest)
        {
            try
            {
                GroupOBDModel oOutboundresponse = new GroupOBDModel();
                GroupOBDModel responseDTO = new GroupOBDModel();
                List<GroupOBDModel> _lstoutbound = new List<GroupOBDModel>();

                if (oRequest != null)
                {
                    dynamic obj = oRequest.EntityObject;
                    GroupOBDModel _obj = JsonConvert.DeserializeObject<GroupOBDModel>(obj.ToString());





                    if (_obj != null)
                    {


                        GroupOBDModel outbound = new GroupOBDModel()
                        {


                            AccountID = _obj.AccountID,
                            UserId = _obj.UserId,
                            WareHouseID = string.IsNullOrEmpty(Convert.ToString(_obj.WareHouseID)) ? "" : Convert.ToString(_obj.WareHouseID),
                            VLPDId = _obj.VLPDId,
                            AssignedID = _obj.AssignedID,
                            VLPDNumber = _obj.VLPDNumber,
                            Lineno = _obj.Lineno,
                            HUNo = _obj.HUNo,
                            HUSize = _obj.HUSize,
                            MRP = _obj.MRP,
                            MfgDate = _obj.MfgDate,
                            MCode = _obj.SKU,
                            ToCartonNo = _obj.ToCartonNo,
                            BatchNo = _obj.BatchNo,
                            CartonNo = _obj.CartonNo,
                            ExpDate = _obj.ExpDate,
                            Location = _obj.Location,
                            PickedQty = _obj.PickedQty,
                            ProjectNo = _obj.ProjectNo,
                            SerialNo = _obj.SerialNo,
                            GradeID = _obj.GradeID,
                            SkipReason=_obj.SkipReason,
                            IsSkip=_obj.IsSkip,
                            SkipReasonID=_obj.SkipReasonID,
                            SkipLocation=_obj.SkipLocation,
                            OutboundID=_obj.OutboundID,
                            SLoc=_obj.SLoc


                        };

                        oOutboundresponse = _GroupOBD.VLPDPickedItems(outbound);
                        if (oOutboundresponse != null)
                        {
                            responseDTO.AssignedQuantity = oOutboundresponse.AssignedQuantity;
                            responseDTO.PickedQty = oOutboundresponse.PickedQty;
                            responseDTO.PendingQty = oOutboundresponse.PendingQty;
                            responseDTO.Message = oOutboundresponse.Message;
                            responseDTO.SortingLocaton = oOutboundresponse.SortingLocaton;
                            if (oOutboundresponse.Result.Contains("Error"))
                            {
                                responseDTO.Result = oOutboundresponse.Result;
                            }
                            else
                            {
                                responseDTO.Result = "";
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
                return JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Exception, _lstwMSExceptionMessage), jsonSettings.JsonSerializerSettings));

            }
            catch (Exception excp)
            {
                ExceptionHandling.LogException(excp, _ClassCode + "001");
                return null;
            }

        }



        [HttpPost]
        [Route("GetOBDSuggestionForSorting")]
        public async Task<string> GetOBDSuggestionForSorting(WMSCoreMessage oRequest)
        {
            try
            {
                List<OutboundModel> _lstoutboundDTO = new List<OutboundModel>();

                if (oRequest != null)
                {
                    dynamic obj = oRequest.EntityObject;
                    OutboundModel _obj = JsonConvert.DeserializeObject<OutboundModel>(obj.ToString());


                    if (_obj != null)
                    {
                        OutboundModel vlpdobj = new OutboundModel()
                        {
                            AccountID = Convert.ToString(_obj.AccountID),
                            UserId = Convert.ToString(_obj.UserId),
                            WareHouseID = string.IsNullOrEmpty(Convert.ToString(_obj.WareHouseID)) ? "" : Convert.ToString(_obj.WareHouseID),
                            IsPicking = _obj.IsPicking,
                            VLPDId = _obj.VLPDId,
                            MCode = _obj.MCode,
                            HUSize = _obj.HUSize,
                            HUNo = _obj.HUNo,
                            BatchNo = _obj.BatchNo,
                            SerialNo = _obj.SerialNo,
                            MfgDate = _obj.MfgDate,
                            ExpDate = _obj.ExpDate,
                            ProjectNo = _obj.ProjectNo,
                            MRP = _obj.MRP,
                            Grade = _obj.Grade
                        };

                        _lstoutboundDTO = await _GroupOBD.GetOBDSuggestionForSorting(vlpdobj);

                        if (_lstoutboundDTO[0].Result == "Sorting is completed for this Batch")
                        {
                            throw new WMSExceptionMessage() { WMSExceptionCode = "EMC_OB_DAL_011", WMSMessage = ErrorMessages.WMC_DAL_INV_0009, ShowAsSuccess = true };
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
                return JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Exception, _lstwMSExceptionMessage), jsonSettings.JsonSerializerSettings));

            }
            catch (Exception excp)
            {
                ExceptionHandling.LogException(excp, _ClassCode + "001");
                return null;
            }

        }




        [HttpPost]
        [Route("UpsertOBDSorting")]
        public async Task<string> UpsertOBDSorting(WMSCoreMessage oRequest)
        {
            try
            {
                List<OutboundModel> _lstoutboundDTO = new List<OutboundModel>();
                OutboundModel _vlpdobj = new OutboundModel();
                if (oRequest != null)
                {
                    dynamic obj = oRequest.EntityObject;
                    OutboundModel _obj = JsonConvert.DeserializeObject<OutboundModel>(obj.ToString());

                    //List<OutboundModel> _lstvlpdobj = new List<OutboundModel>();

                    if (_obj != null)
                    {
                        OutboundModel vlpdobj = new OutboundModel()
                        {
                            AccountID = Convert.ToString(_obj.AccountID),
                            UserId = Convert.ToString(_obj.UserId),
                            WareHouseID = string.IsNullOrEmpty(Convert.ToString(_obj.WareHouseID)) ? "" : Convert.ToString(_obj.WareHouseID),
                            IsPicking = _obj.IsPicking,
                            VLPDId = _obj.VLPDId,
                            OBDNumber = _obj.OBDNumber,
                            MCode = _obj.MCode,
                            SortingQty = _obj.SortingQty,
                            TenantID = _obj.TenantID,
                            HUSize = _obj.HUSize,
                            HUNo = _obj.HUNo,
                            BatchNo = _obj.BatchNo,
                            MfgDate = _obj.MfgDate,
                            SerialNo = _obj.SerialNo,
                            ProjectNo = _obj.ProjectNo,
                            ExpDate = _obj.ExpDate,
                            Location = _obj.Location,
                            Grade = _obj.Grade,
                            PickSerialNumber = _obj.PickSerialNumber

                        };

                        _vlpdobj = await _GroupOBD.UpsertOBDSorting(vlpdobj);
                        _lstoutboundDTO.Add(_vlpdobj);
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
                return JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Exception, _lstwMSExceptionMessage), jsonSettings.JsonSerializerSettings));

            }
            catch (Exception excp)
            {
                ExceptionHandling.LogException(excp, _ClassCode + "001");
                return null;
            }

        }






        [HttpPost]
        [Route("GetLocationSuggestionForSorting")]
        public async Task<string> GetLocationSuggestionForSorting(WMSCoreMessage oRequest)
        {
            try
            {
                List<OutboundModel> _lstoutboundDTO = new List<OutboundModel>();

                if (oRequest != null)
                {
                    dynamic obj = oRequest.EntityObject;
                    OutboundModel _obj = JsonConvert.DeserializeObject<OutboundModel>(obj.ToString());


                    if (_obj != null)
                    {
                        OutboundModel vlpdobj = new OutboundModel()
                        {
                            AccountID = Convert.ToString(_obj.AccountID),
                            UserId = Convert.ToString(_obj.UserId),
                            WareHouseID = string.IsNullOrEmpty(Convert.ToString(_obj.WareHouseID)) ? "" : Convert.ToString(_obj.WareHouseID),
                            VLPDId = _obj.VLPDId,
                            MCode = _obj.MCode,
                            OBDNumber = _obj.OBDNumber,
                            HUSize = _obj.HUSize,
                            HUNo = _obj.HUNo,
                            BatchNo = _obj.BatchNo,
                            SerialNo = _obj.SerialNo,
                            MfgDate = _obj.MfgDate,
                            ExpDate = _obj.ExpDate,
                            ProjectNo = _obj.ProjectNo,
                            MRP = _obj.MRP
                        };

                        _lstoutboundDTO = await _GroupOBD.GetLocationSuggestionForSorting(vlpdobj);

                        if (_lstoutboundDTO[0].Result == "Sorting is completed for this Batch")
                        {
                            throw new WMSExceptionMessage() { WMSExceptionCode = "EMC_OB_DAL_011", WMSMessage = ErrorMessages.WMC_DAL_INV_0009, ShowAsSuccess = true };
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
                return JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Exception, _lstwMSExceptionMessage));
            }
            catch (Exception excp)
            {
                ExceptionHandling.LogException(excp, _ClassCode + "001");
                return null;
            }

        }



    }
}
