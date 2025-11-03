using FWMSC21Core.Entities;
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


namespace Simpolo_Endpoint.Controllers.HHTControllers
{
    [Route("[controller]")]
    //[Authorize]
    [ApiController]
    public class InboundController : ControllerBase
    {
        private readonly IInbound _Inbound;
        private string _ClassCode = string.Empty;
        //BaseController baseController = new BaseController();
        JsonSettings jsonSettings = new JsonSettings();
        public InboundController(IInbound inbound)
        {
            _Inbound = inbound;
            _ClassCode = ExceptionHandling.GetClassExceptionCode(ExceptionHandling.ExcpConstants_API_Enpoint.InboundController);
        }

        [Route("GetStoreRefNos")]
        [HttpPost]
        public async Task<string> GetStoreRefNos(WMSCoreMessage oRequest)
        {
            try
            {
                List<InboundDTO> _lstinbound = new List<InboundDTO>();
                List<Inbound> lInbound = new List<Inbound>();

                if (oRequest != null)
                {
                    InboundDTO _oInboundDTO = (InboundDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);

                    if (_oInboundDTO != null)
                    {
                        Inbound inbound = new Inbound()
                        {
                            AccountID = ConversionUtility.ConvertToInt(_oInboundDTO.AccountID),
                            CreatedByUserID = ConversionUtility.ConvertToInt(_oInboundDTO.UserId),
                            WarehouseID = ConversionUtility.ConvertToInt(_oInboundDTO.WarehouseID),
                            TenantID = ConversionUtility.ConvertToInt(_oInboundDTO.TenantID),
                            IsStockAdjust= _oInboundDTO.IsStockAdjust
                        };

                        lInbound = await _Inbound.GetStoreRefNos(inbound);

                        foreach (Inbound inboundItem in lInbound)
                        {
                            InboundDTO responseDTO = new InboundDTO();
                            responseDTO.StoreRefNo = inboundItem.StoreRefNo.ToString();
                            responseDTO.InboundID = inboundItem.InboundID.ToString();
                            responseDTO.InvoiceQty = inboundItem.InvoiceQty.ToString();
                            responseDTO.ReceivedQty = inboundItem.ReceivedQty.ToString();
                            responseDTO.POType = inboundItem.POTypeID;
                            responseDTO.IsProductionOrder = inboundItem.IsProductionOrder;

                            List<EntryDTO> entryList = new List<EntryDTO>();

                            EntryDTO entry = new EntryDTO()
                            {
                                VehicleNumber = inboundItem.VehicleNumber.ToString(),
                                DockNumber = inboundItem.DockNumber.ToString(),
                                DockID = inboundItem.DockID.ToString()
                            };
                            entryList.Add(entry);

                            responseDTO.Entry = entryList;
                            _lstinbound.Add(responseDTO);
                        }
                    }                 

                    string json1 = JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Inbound, _lstinbound), jsonSettings.JsonSerializerSettings);
                    string json = JsonConvert.SerializeObject(json1);
                    return json;
                }
                else
                {
                    return null;
                }
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

        [Route("GET_INB_GRNList"), HttpPost]
        public async Task<IActionResult> GET_INB_GRNList(InboundTrackingModel obj)
        {
            Payload<string> response = await _Inbound.GET_INB_GRNList(obj);
            if (response != null)
            {
                if (!response.HasErrors && !response.HasWarnings)
                {
                    return Ok(response);
                }
                else if (response.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response.GetErrors());
                }
                else
                {
                    return StatusCode(StatusCodes.Status202Accepted, response.GetWarnings());
                }
            }
            else
            {
                return BadRequest("Failed to retrieve data");
            }

        }


        [HttpPost]
        [Route("UpdateReceiveItemForHHT")]
        public async Task<string> UpdateReceiveItemForHHT(WMSCoreMessage oRequest)
        {
            try
            {
                BO.Inbound oresponseinbound = new BO.Inbound();
                InboundDTO responseDTO = new InboundDTO();
                List<InboundDTO> _lstinbound = new List<InboundDTO>();

                if (oRequest != null)
                {
                    InboundDTO _oInboundDTO = (InboundDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);

                    if (_oInboundDTO != null)
                    {
                        BO.Inbound oinbound = new BO.Inbound()
                        {
                            AccountId = ConversionUtility.ConvertToInt(_oInboundDTO.AccountID),
                            InboundId = _oInboundDTO.InboundID,
                            MCode = _oInboundDTO.Mcode,
                            Qty = _oInboundDTO.Qty,
                            IsDam = _oInboundDTO.IsDam,
                            HasDisc = _oInboundDTO.HasDisc,
                            Lineno = _oInboundDTO.LineNo,
                            CreatedBy = _oInboundDTO.UserId,
                            CartonNo = _oInboundDTO.CartonNo,
                            SLoc = _oInboundDTO.StorageLocation,
                            BatchNo = _oInboundDTO.BatchNo,
                            SerialNo = _oInboundDTO.SerialNo,
                            ExpDate = _oInboundDTO.ExpDate,
                            MfgDate = _oInboundDTO.MfgDate,
                            ProjectNo = _oInboundDTO.ProjectRefno,
                            Storerefno = _oInboundDTO.StoreRefNo,
                            MRP = _oInboundDTO.MRP,
                            Dock = _oInboundDTO.Dock,
                            SupplierInvoiceDetailsId = _oInboundDTO.SupplierInvoiceDetailsID,
                            HUNo = _oInboundDTO.HUNo.ToString(),
                            HUSize = _oInboundDTO.HUSize.ToString(),
                            VehicleNo = _oInboundDTO.VehicleNo,
                            BoxSerialNo = _oInboundDTO.BoxSerialNo,
                            Grade = _oInboundDTO.Grade,
                            IsStockAdjust = _oInboundDTO.IsStockAdjust,
                            AdjustQty = ConversionUtility.ConvertToDecimal(_oInboundDTO.AdjustQty),
                            ActualQty = ConversionUtility.ConvertToDecimal(_oInboundDTO.ActualQty),
                            IsStockAdd = _oInboundDTO.IsStockAdd,
                            IsPhysicalEmpty = _oInboundDTO.IsPhysicalEmpty

                        };

                        oresponseinbound = await _Inbound.UpdateReceiveItem(oinbound);

                        if (oresponseinbound.Result == "Please enter correct recieved Qty")
                        {
                            throw new WMSExceptionMessage()
                            {
                                WMSExceptionCode = oresponseinbound.Result,
                                WMSMessage = oresponseinbound.Result,
                                ShowAsError = true
                            };
                        }

                        if (oresponseinbound != null)
                        {
                            responseDTO.ItemPendingQty = oresponseinbound.ItemPendingQty.ToString();
                            responseDTO.ReceivedQty = oresponseinbound.ReceivedQty.ToString();
                            responseDTO.Result = oresponseinbound.Result;
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
        [Route("GetSkipReasonList")]
        public async Task<string> GetSkipReasonList(WMSCoreMessage oRequest)
        {
            try
            {
                DataSet userDataTable = null;
                InboundDTO responseDTO = new InboundDTO();
                List<InboundDTO> _lstinbound = new List<InboundDTO>();

                if (oRequest != null)
                {
                    InboundDTO _oInboundDTO = (InboundDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);

                    if (_oInboundDTO != null)
                    {
                        userDataTable = await _Inbound.GetSkipReasonList(_oInboundDTO.SkipType);
                    }
                    foreach (DataRow row in userDataTable.Tables[0].Rows)
                    {
                        string sss = row["Reason"].ToString();
                        InboundDTO responseDTO1 = new InboundDTO();
                        responseDTO1.SkipReason = row["Reason"].ToString();
                        responseDTO1.ReasonId = Convert.ToInt32(row["ReasonID"]);

                        _lstinbound.Add(responseDTO1);
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
        [Route("CheckContainer")]
        public async Task<string> CheckContainer(WMSCoreMessage oRequest)
        {
            try
            {
                InboundDTO responseDTO = new InboundDTO();
                List<InboundDTO> _lstinbound = new List<InboundDTO>();
                String result = null;
                if (oRequest != null)
                {
                    InboundDTO _oInboundDTO = (InboundDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);
                    if (_oInboundDTO != null)
                    {
                        result = await _Inbound.CheckContainer(_oInboundDTO.PalletNo, _oInboundDTO.InboundID);

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
        [Route("GetReceivedQty")]
        public async Task<string> GetReceivedQty(WMSCoreMessage oRequest)
        {
            try
            {
                BO.Inbound oresponseinbound = new BO.Inbound();
                InboundDTO responseDTO = new InboundDTO();
                List<InboundDTO> _lstinbound = new List<InboundDTO>();
#pragma warning disable CS0219 // The variable 'result' is assigned but its value is never used
                String result = null;
#pragma warning restore CS0219 // The variable 'result' is assigned but its value is never used
                if (oRequest != null)
                {
                    InboundDTO _oInboundDTO = (InboundDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);

                    if (_oInboundDTO != null)
                    {
                        BO.Inbound oinbound = new BO.Inbound()
                        {
                            AccountId = ConversionUtility.ConvertToInt(_oInboundDTO.AccountID),
                            InboundId = _oInboundDTO.InboundID,
                            MCode = _oInboundDTO.Mcode,
                            Storerefno = _oInboundDTO.StoreRefNo,
                            Lineno = _oInboundDTO.LineNo,
                            MfgDate = _oInboundDTO.MfgDate,
                            ExpDate = _oInboundDTO.ExpDate,
                            SerialNo = _oInboundDTO.SerialNo,
                            ProjectNo = _oInboundDTO.ProjectRefno,
                            MRP = _oInboundDTO.MRP,
                            SupplierInvoiceDetailsId = _oInboundDTO.SupplierInvoiceDetailsID,
                            HUNo = _oInboundDTO.HUNo,
                            HUSize = _oInboundDTO.HUSize,
                            BatchNo = _oInboundDTO.BatchNo,
                            Grade = _oInboundDTO.Grade,
                            CartonNo=_oInboundDTO.CartonNo
                        };

                        oresponseinbound = await _Inbound.GetReceivedQty(oinbound);

                        if (oresponseinbound != null)
                        {
                            if (oresponseinbound.Result == "invalid")
                            {
                                throw new WMSExceptionMessage() { WMSExceptionCode = null, WMSMessage = " Please scan valid SKU", ShowAsError = true };
                            }
                            responseDTO.ItemPendingQty = oresponseinbound.ItemPendingQty.ToString();
                            responseDTO.ReceivedQty = oresponseinbound.ReceivedQty.ToString();
                            responseDTO.SLoc = oresponseinbound.SLoc.ToString();
                            responseDTO.ActualQty = oresponseinbound.ActualQty.ToString();
                            responseDTO.AdjustQty = oresponseinbound.AdjustQty.ToString();
                            responseDTO.IsStockAdd = oresponseinbound.IsStockAdd.ToString();

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
        [Route("CheckDockGoodsIn")]
        public async Task<string> CheckDockGoodsIn(WMSCoreMessage oRequest)
        {
            try
            {
                InboundDTO responseDTO = new InboundDTO();
                List<InboundDTO> _lstinbound = new List<InboundDTO>();
                if (oRequest != null)
                {
                    InboundDTO _oInboundDTO = (InboundDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);

                    if (_oInboundDTO != null)
                    {
                        BO.Inbound oInbound = new BO.Inbound()
                        {
                            InboundId = _oInboundDTO.InboundID,
                            Dock = _oInboundDTO.Dock
                        };

                        BO.Inbound resInbound = new BO.Inbound();

                        resInbound = await _Inbound.CheckDock(oInbound);

                        if (resInbound.Result == "0")
                        {
                            responseDTO.Result = "Please scan valid Dock";
                            throw new WMSExceptionMessage() { WMSExceptionCode = responseDTO.Result, WMSMessage = responseDTO.Result, ShowAsError = true };
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
        [Route("GetStorageLocations")]
        public async Task<string> GetStorageLocations(WMSCoreMessage oRequest)
        {
            DataSet userDataTable = null;
            try
            {
                InboundDTO responseDTO = new InboundDTO();
                List<InboundDTO> _lstinbound = new List<InboundDTO>();
                if (oRequest != null)
                {
                    InboundDTO _oInboundDTO = (InboundDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);

                    if (_oInboundDTO != null)
                    {
                        userDataTable = await _Inbound.GetStorageLocations();
                    }
                    foreach (DataRow row in userDataTable.Tables[0].Rows)
                    {
                        InboundDTO _responseInboundDTO = new InboundDTO();
                        _responseInboundDTO.StorageLocation = row["LocationCode"].ToString();

                        _lstinbound.Add(_responseInboundDTO);
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
        [Route("GetItemTOPutAway")]
        public async Task<string> GetItemTOPutAway(WMSCoreMessage oRequest)
        {
            try
            {
                BO.PutAway oPutawayresponse = new BO.PutAway();
                PutAwayDTO responseDTO = new PutAwayDTO();
                List<PutAwayDTO> _lstinbound = new List<PutAwayDTO>();

                if (oRequest != null)
                {
                    PutAwayDTO _oPutawayDTO = (PutAwayDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);

                    if (_oPutawayDTO != null)
                    {
                        BO.PutAway oPutaway = new BO.PutAway()
                        {
                            InboundId = _oPutawayDTO.InboundId,
                            TransferRequestId = Convert.ToInt32(_oPutawayDTO.TransferRequestId)
                        };

                        oPutawayresponse = await _Inbound.GetItemTOPutAway(oPutaway);

                        if (oPutawayresponse != null && oPutawayresponse.Result == "3")
                        {
                            responseDTO.SuggestedPutawayID = oPutawayresponse.SuggestedPutawayID.ToString();
                            responseDTO.MaterialMasterId = oPutawayresponse.MaterialMasterId.ToString();
                            responseDTO.MCode = oPutawayresponse.MCode.ToString();

                            responseDTO.CartonCode = oPutawayresponse.CartonCode.ToString();
                            responseDTO.CartonID = oPutawayresponse.CartonID.ToString();
                            responseDTO.Location = oPutawayresponse.Location.ToString();
                            responseDTO.LocationID = oPutawayresponse.LocationID.ToString();
                            responseDTO.MfgDate = oPutawayresponse.MfgDate.ToString();
                            responseDTO.ExpDate = oPutawayresponse.ExpDate.ToString();
                            responseDTO.SerialNo = oPutawayresponse.SerialNo.ToString();
                            responseDTO.BatchNo = oPutawayresponse.BatchNo.ToString();
                            responseDTO.ProjectRefNo = oPutawayresponse.ProjectRefNo.ToString();
                            responseDTO.MRP = oPutawayresponse.MRP.ToString();
                            responseDTO.SuggestedQty = oPutawayresponse.SuggestedQty.ToString();
                            responseDTO.SuggestedReceivedQty = oPutawayresponse.SuggestedReceivedQty.ToString();
                            responseDTO.SuggestedRemainingQty = oPutawayresponse.SuggestedRemainingQty.ToString();
                            responseDTO.TransferRequestDetailsId = oPutawayresponse.TransferRequestDetailsId.ToString();
                            responseDTO.PickedLocationID = oPutawayresponse.PickedLocationID.ToString();
                            responseDTO.GMDRemainingQty = oPutawayresponse.GMDRemainingQty.ToString();
                            responseDTO.PutAwayQty = "1";
                            responseDTO.Result = oPutawayresponse.Result;
                            responseDTO.Dock = oPutawayresponse.Dock;
                            responseDTO.StorageCode = oPutawayresponse.StorageLocation;
                        }
                        else
                        {
                            responseDTO.Result = oPutawayresponse.Result;
                        }
                        _lstinbound.Add(responseDTO);
                    }

                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.PutAwayDTO, _lstinbound), jsonSettings.JsonSerializerSettings));
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
        [Route("SkipItem")]
        public async Task<string> SkipItem(WMSCoreMessage oRequest)
        {
            try
            {
                BO.PutAway oPutawayresponse = new BO.PutAway();
                PutAwayDTO responseDTO = new PutAwayDTO();
                List<PutAwayDTO> _lstinbound = new List<PutAwayDTO>();

                if (oRequest != null)
                {
                    PutAwayDTO _oputawayDTO = (PutAwayDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);

                    if (_oputawayDTO != null)
                    {
                        BO.PutAway oPutaway = new BO.PutAway()
                        {
                            TransferRequestId = Convert.ToInt32(_oputawayDTO.TransferRequestId),
                            InboundId = _oputawayDTO.InboundId,
                            SuggestedPutawayID = Convert.ToInt32(_oputawayDTO.SuggestedPutawayID),
                            MCode = _oputawayDTO.MCode,
                            MfgDate = _oputawayDTO.MfgDate,
                            ExpDate = _oputawayDTO.ExpDate,
                            BatchNo = _oputawayDTO.BatchNo,
                            SerialNo = _oputawayDTO.SerialNo,
                            ProjectRefNo = _oputawayDTO.ProjectRefNo,
                            MRP = _oputawayDTO.MRP,
                            SkipQty = Convert.ToDecimal(_oputawayDTO.SkipQty),
                            SuggestedReceivedQty = Convert.ToDecimal(_oputawayDTO.SuggestedReceivedQty),
                            UserId = Convert.ToInt32(_oputawayDTO.UserID),
                            StorageLocation = "Available",
                            CartonCode = _oputawayDTO.CartonCode,
                            Flag = 1,
                            Location = _oputawayDTO.Location,
                            Skipreason = _oputawayDTO.SkipReason
                        };

                        oPutawayresponse = await _Inbound.SkipItem(oPutaway);

                        if (oPutawayresponse != null)
                        {
                            responseDTO.Result = "1";

                            return await GetItemTOPutAway(oRequest);
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
        [Route("UpsertPutAwayItem")]
        public async Task<string> UpsertPutAwayItem(WMSCoreMessage oRequest)
        {
            try
            {
                PutAwayDTO responseDTO = new PutAwayDTO();
                List<PutAwayDTO> _lstPutaway = new List<PutAwayDTO>();
                BO.PutAway oPutawayresponse = new BO.PutAway();

                if (oRequest != null)
                {
                    PutAwayDTO _oPutawayDTO = (PutAwayDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);

                    if (_oPutawayDTO != null)
                    {
                        BO.PutAway oPutaway = new BO.PutAway()
                        {
                            TransferRequestId = Convert.ToInt32(_oPutawayDTO.TransferRequestId),
                            InboundId = _oPutawayDTO.InboundId,
                            SuggestedPutawayID = ConversionUtility.ConvertToInt(_oPutawayDTO.SuggestedPutawayID),
                            MCode = _oPutawayDTO.MCode,
                            MfgDate = _oPutawayDTO.MfgDate,
                            ExpDate = _oPutawayDTO.ExpDate,
                            BatchNo = _oPutawayDTO.BatchNo,
                            ProjectRefNo = _oPutawayDTO.ProjectRefNo,
                            SerialNo = _oPutawayDTO.SerialNo,
                            MRP = _oPutawayDTO.MRP,
                            PutAwayQty = _oPutawayDTO.PutAwayQty,
                            TotalQuantity = ConversionUtility.ConvertToDecimal(_oPutawayDTO.TotalQty),
                            UserId = Convert.ToInt32(_oPutawayDTO.UserID),
                            CartonCode = _oPutawayDTO.CartonCode,
                            Location = _oPutawayDTO.Location,
                            StorageLocation = _oPutawayDTO.StorageCode,
                            ScnnedLocation = _oPutawayDTO.ScannedLocation
                        };

                        oPutawayresponse = await _Inbound.UpsertPutAwayItem(oPutaway);

                        if (oPutawayresponse != null)
                        {
                            if (oPutawayresponse.Result == "3")
                            {
                                responseDTO.SuggestedPutawayID = oPutawayresponse.SuggestedPutawayID.ToString();
                                responseDTO.MaterialMasterId = oPutawayresponse.MaterialMasterId.ToString();
                                responseDTO.MCode = oPutawayresponse.MCode.ToString();
                                responseDTO.CartonCode = oPutawayresponse.CartonCode.ToString();
                                responseDTO.CartonID = oPutawayresponse.CartonID.ToString();
                                responseDTO.Location = oPutawayresponse.Location.ToString();
                                responseDTO.LocationID = oPutawayresponse.LocationID.ToString();
                                responseDTO.MfgDate = oPutawayresponse.MfgDate.ToString();
                                responseDTO.ExpDate = oPutawayresponse.ExpDate.ToString();
                                responseDTO.SerialNo = oPutawayresponse.SerialNo.ToString();
                                responseDTO.BatchNo = oPutawayresponse.BatchNo.ToString();
                                responseDTO.ProjectRefNo = oPutawayresponse.ProjectRefNo.ToString();
                                responseDTO.MRP = oPutawayresponse.MRP.ToString();
                                responseDTO.SuggestedQty = oPutawayresponse.SuggestedQty.ToString();
                                responseDTO.SuggestedReceivedQty = oPutawayresponse.SuggestedReceivedQty.ToString();
                                responseDTO.SuggestedRemainingQty = oPutawayresponse.SuggestedRemainingQty.ToString();
                                responseDTO.TransferRequestDetailsId = oPutawayresponse.TransferRequestDetailsId.ToString();
                                responseDTO.PickedLocationID = oPutawayresponse.PickedLocationID.ToString();
                                responseDTO.GMDRemainingQty = oPutawayresponse.GMDRemainingQty.ToString();
                                responseDTO.PutAwayQty = "1";
                                responseDTO.Result = "3";
                            }
                            else if (oPutawayresponse.Result == "1" || oPutawayresponse.Result == "2")
                            {
                                responseDTO.Result = oPutawayresponse.Result == "1" ? "1" : "2";
                                responseDTO.SuggestedReceivedQty = oPutawayresponse.SuggestedReceivedQty.ToString();
                                responseDTO.SuggestedQty = oPutawayresponse.SuggestedQty.ToString();
                                responseDTO.PutAwayQty = oPutawayresponse.PutAwayQty.ToString();
                                responseDTO.CartonCode = "";
                                responseDTO.MCode = "";
                                responseDTO.MfgDate = oPutawayresponse.MfgDate.ToString();
                                responseDTO.ExpDate = oPutawayresponse.ExpDate.ToString();
                                responseDTO.SerialNo = oPutawayresponse.SerialNo.ToString();
                                responseDTO.BatchNo = oPutawayresponse.BatchNo.ToString();
                                responseDTO.ProjectRefNo = oPutawayresponse.ProjectRefNo.ToString();
                                responseDTO.MRP = oPutawayresponse.MRP.ToString();
                                responseDTO.Location = oPutawayresponse.Location.ToString();
                            }
                            else
                            {
                                throw new WMSExceptionMessage() { WMSExceptionCode = oPutawayresponse.Result, WMSMessage = oPutawayresponse.Result, ShowAsError = true };
                            }
                        }
                        _lstPutaway.Add(responseDTO);
                    }

                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.PutAwayDTO, _lstPutaway), jsonSettings.JsonSerializerSettings));
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
        [Route("CheckPutAwayItemQty")]
        public async Task<string> CheckPutAwayItemQty(WMSCoreMessage oRequest)
        {
            try
            {
                BO.PutAway oresponseputaway = new BO.PutAway();
                PutAwayDTO responseDTO = new PutAwayDTO();
                List<PutAwayDTO> _lstputaway = new List<PutAwayDTO>();
                String result = null;
                if (oRequest != null)
                {
                    PutAwayDTO _oPutawayDTO = (PutAwayDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);

                    if (_oPutawayDTO != null)
                    {
                        BO.PutAway oinbound = new BO.PutAway()
                        {
                            TransferRequestId = Convert.ToInt32(_oPutawayDTO.TransferRequestId),
                            InboundId = _oPutawayDTO.InboundId,
                            MaterialMasterId = ConversionUtility.ConvertToInt(_oPutawayDTO.MaterialMasterId),
                            PutAwayQty = "0",
                            SuggestedPutawayID = ConversionUtility.ConvertToInt(_oPutawayDTO.SuggestedPutawayID)
                        };

                        oresponseputaway = await _Inbound.CheckPutAwayItemQty(oinbound);

                        if (oresponseputaway != null)
                        {
                            if (oresponseputaway.Result != "")
                            {
                                result = oresponseputaway.Result;
                                throw new WMSExceptionMessage() { WMSExceptionCode = result, WMSMessage = result, ShowAsError = true };
                            }
                            else
                            {
                                responseDTO.Result = "1";
                            }
                        }
                        _lstputaway.Add(responseDTO);
                    }

                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.PutAwayDTO, _lstputaway), jsonSettings.JsonSerializerSettings));
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
        [Route("GeneratePutawaySuggestions")]
        public async Task<string> GeneratePutawaySuggestions(WMSCoreMessage oRequest)
        {
            try
            {
                List<Suggestion> _lstinventory = new List<Suggestion>();
                List<InventoryDTO> _lInventoryDTO = new List<InventoryDTO>();
                if (oRequest != null)
                {
                    InventoryDTO _oInventoryDTO = (InventoryDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);
                    if (_oInventoryDTO != null)
                    {
                        Location _oLoc = new Location();
                        SearchCriteria oSearchInventory = new SearchCriteria()
                        {
                            ContainerCode = _oInventoryDTO.ContainerCode,
                            MaterialRSN = _oInventoryDTO.RSN,
                            InboundID = _oInventoryDTO.InboundID,
                            StoreRefNo = _oInventoryDTO.StoreRefNo,
                            UserID = _oInventoryDTO.UserID
                        };

                        _lstinventory = await _Inbound.GeneratePutawaySuggestion(oSearchInventory);

                        foreach (Suggestion osuggesteditem in _lstinventory)
                        {
                            InventoryDTO _oResponse = new InventoryDTO();

                            _oResponse.LocationCode = osuggesteditem.LocationCode;
                            _oResponse.MaterialCode = osuggesteditem.MaterialCode;
                            //_oResponse.RSN = osuggesteditem.SerialNo;
                            _oResponse.Quantity = osuggesteditem.Quantity.ToString();
                            _oResponse.SuggestionID = osuggesteditem.SuggestedPutawayID.ToString();
                            _oResponse.TenantID = osuggesteditem.TenantID.ToString();
                            _oResponse.WarehouseId = osuggesteditem.WareHouseID.ToString();

                            _lInventoryDTO.Add(_oResponse);
                        }
                     
                        string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Inbound, _lInventoryDTO), jsonSettings.JsonSerializerSettings));
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
            }
            catch (Exception excp)
            {
                ExceptionHandling.LogException(excp, _ClassCode + "006");
                return null;
            }
        }


        [HttpPost]
        [Route("GetConatinerLocation")]
        public async Task<string> GetConatinerLocation(WMSCoreMessage oRequest)
        {
            try
            {
                InboundDTO responseDTO = new InboundDTO();
                List<InboundDTO> _lstinbound = new List<InboundDTO>();
                String result = null;
                if (oRequest != null)
                {
                    InboundDTO _oInboundDTO = (InboundDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);

                    if (_oInboundDTO != null)
                    {
                        result = await _Inbound.ChekContainerLocation(_oInboundDTO.PalletNo, _oInboundDTO.WarehouseID);
                        responseDTO.Result = result;

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


        //Below QAD Calls

        [HttpGet]
        [Route("GetMiscRecptXMLData")]
        public async Task<GRNDetails> GenerateMiscReceiptData(string batchNo, string projectRefNo, string Qty, string remks, string part, string um, int val, string qadAccount, string QADLocation, string uniqueID = "")
        {
            try
            {
                var splitString = um?.Split('/');
                string um1 = splitString[0]?.Trim();
                string conv = splitString[1]?.Trim();
                decimal miscQty = Convert.ToDecimal(Qty) * val;
                GRNDetails obj = await _Inbound.GetMiscXMLData(batchNo, projectRefNo, miscQty.ToString(), remks, part, um1, conv, qadAccount, QADLocation, uniqueID);
                return obj;
            }
            catch (Exception ex)
            {
                GRNDetails gRNDetails = new GRNDetails();
                gRNDetails.errorcode = ex.Message;
                gRNDetails.result = "Error";
                return gRNDetails;
            }
        }


        [HttpGet]
        [Route("GetGRNXMLData")]
        public async Task<GRNDetails> GetGRNXMLData(string InboundId, string InvoiceNumber, string PONumber, int InboundType, string Remarks = "")
        {
            try
            {
                Task<GRNDetails> obj = _Inbound.GetGRNXMLData(InboundId, InvoiceNumber, PONumber, InboundType, Remarks);
                return await obj;
            }
            catch (Exception ex)
            {
                GRNDetails gRNDetails = new GRNDetails();
                gRNDetails.errorcode = ex.Message;
                gRNDetails.result = "Error";
                return gRNDetails;
            }
        }

        [HttpGet]
        [Route("QADGRNRevert")]
        public async Task<GRNDetails> QADGRNRevert(int grnHeaderID, int isSupplierRtn)
        {
            try
            {
                Task<GRNDetails> obj = _Inbound.GetGRNRevertXMLData(grnHeaderID, isSupplierRtn);
                return await obj;
            }
            catch (Exception ex)
            {
                GRNDetails gRNDetails = new GRNDetails();
                gRNDetails.errorcode = ex.Message;
                gRNDetails.result = "Error";
                return gRNDetails;
            }
        }


        [HttpPost]
        [Route("QADCycleCount")]
        public async Task<GRNDetails> QADCycleCount(InitiateStockModel stockObj)
        {
            try
            {
#pragma warning disable CS0219 // The variable 'CCID' is assigned but its value is never used
                int CCID = 0;
#pragma warning restore CS0219 // The variable 'CCID' is assigned but its value is never used
                Task<GRNDetails> obj = _Inbound.GetCycleCountXMLData(stockObj);
                return await obj;
            }
            catch (Exception ex)
            {

                GRNDetails gRNDetails = new GRNDetails();
                gRNDetails.errorcode = ex.Message;
                gRNDetails.result = "Error";
                return gRNDetails;
            }
        }

        [HttpGet]
        [Route("QADUpdateInventorystatus")]
        public async Task<GRNDetails> QADUpdateInventorystatus(int TransferRequestID, int TransferTypeID, string uniqueID)
        {
            try
            {

                Task<GRNDetails> obj = _Inbound.QADUpdateInventorystatus(TransferRequestID, TransferTypeID, uniqueID);
                return await obj;
            }
            catch (Exception ex)
            {
                GRNDetails gRNDetails = new GRNDetails();
                gRNDetails.errorcode = ex.Message;
                gRNDetails.result = "Error";
                return gRNDetails;
            }
        }


        [HttpGet]
        [Route("QADSOPGI")]
        public async Task<GRNDetails> QADSOPGI(int OutboundID)
        {
            try
            {
                Task<GRNDetails> obj = _Inbound.GetSOPGIXMLDATA(OutboundID);
                return await obj;
            }
            catch (Exception ex)
            {
                GRNDetails gRNDetails = new GRNDetails();
                gRNDetails.errorcode = ex.Message;
                gRNDetails.result = "Error";
                return gRNDetails;
            }
        }

        [HttpGet]
        [Route("QADShipmentRequest")]
        public async Task<GRNDetails> QADShipmentRequest(int OutboundID)
        {
            try
            {
                Task<GRNDetails> obj = _Inbound.QADShipmentRequest(OutboundID);
                return await obj;
            }
            catch (Exception ex)
            {
                GRNDetails gRNDetails = new GRNDetails();
                gRNDetails.errorcode = ex.Message;
                gRNDetails.result = "Error";
                return gRNDetails;
            }
        }

        [HttpPost]
        [Route("QADItemLevelWOPGI")]
        public async Task<WorkOrderResponse> QADItemLevelWOPGI(QADRequestObj qadWOData)
        {
            try
            {
                Task<WorkOrderResponse> obj = _Inbound.GetItemLevelWOPGIXMLDATA(qadWOData);
                return await obj;
            }
            catch (Exception ex)
            {
                //GRNDetails gRNDetails = new GRNDetails();
                //gRNDetails.errorcode = ex.Message;
                //gRNDetails.result = "Error";
                return new WorkOrderResponse() { error = "Error", result = ex.Message };
            }
        }
    }
}
