using Simpolo_Endpoint.DAO.HHTInterface;
using Simpolo_Endpoint.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using WMSCore_BusinessEntities.Entities;

namespace Simpolo_Endpoint.Controllers.HHTControllers
{
    [Route("[controller]")]
    //[Authorize]
    [ApiController]
    public class ScanController : ControllerBase
    {
        private readonly IScan _Scan;
        public ScanController(IScan scan)
        {
            _Scan = scan;
        }

        private string _ClassCode = string.Empty;
        JsonSettings jsonSettings = new JsonSettings();

        [Route("ValidateLocation")]
        [HttpPost]
        public async Task<string> ValidateLocation(WMSCoreMessage oRequest)
        {          
            try
            {
                DTO.ScanDTO _oScanDTO = (DTO.ScanDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);
                dynamic _obj = oRequest.EntityObject;
                ScanDTO _obj1 = JsonConvert.DeserializeObject<ScanDTO>(_obj.ToString());

                if (_oScanDTO != null)
                {
                    ScannedItem responseDTO = new ScannedItem();
                    {
                        responseDTO.ScanInput = _oScanDTO.ScanInput;
                        responseDTO.WarehouseID = _oScanDTO.WarehouseID;
                        responseDTO.InboundID = _oScanDTO.InboundID;
                        responseDTO.ObdNumber = _oScanDTO.ObdNumber;
                        responseDTO.VlpdNumber = _oScanDTO.VlpdNumber;
                        responseDTO.UserID = Convert.ToInt32(_oScanDTO.UserID);
                        responseDTO.IsCycleCount = _oScanDTO.IsCycleCount ? 1 : 0;
                    };

                    ScannedItem scanItems = await _Scan.ValidateLocation(responseDTO);

                    _obj1.ScanResult = scanItems.ScanResult;
                    _obj1.Message = scanItems.Message;
                 
                    return JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.ScanDTO, _obj1), jsonSettings.JsonSerializerSettings));
                }
                else
                {
                    List<WMSExceptionMessage> _lstwMSExceptionMessage = new List<WMSExceptionMessage>();
                    _lstwMSExceptionMessage.Add(new WMSExceptionMessage() { WMSMessage = "Invalid Request format" });
                    return JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Exception, _lstwMSExceptionMessage), jsonSettings.JsonSerializerSettings));
                }
            }
            catch (WMSExceptionMessage ex)
            {
                List<WMSExceptionMessage> _lstwMSExceptionMessage = new List<WMSExceptionMessage>();
                _lstwMSExceptionMessage.Add(ex);
                return JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Exception, _lstwMSExceptionMessage), jsonSettings.JsonSerializerSettings));
            }
            catch (Exception excp)
            {
                return excp.Message;
            }
        }


        [Route("ValidatePallet")]
        [HttpPost]
        public async Task<string> ValidatePallet(WMSCoreMessage oRequest)
        {
            try
            {
                DTO.ScanDTO _oScanDTO = (DTO.ScanDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);
                dynamic _obj = oRequest.EntityObject;
                ScanDTO _obj1 = JsonConvert.DeserializeObject<ScanDTO>(_obj.ToString());

                if (_oScanDTO != null)
                {
                    ScannedItem responseEntity = new ScannedItem()
                    {
                        ScanInput = _oScanDTO.ScanInput,
                        WarehouseID = _oScanDTO.WarehouseID,
                        InboundID = _oScanDTO.InboundID,
                        ObdNumber = _oScanDTO.ObdNumber,
                        VlpdNumber = _oScanDTO.VlpdNumber,
                        UserID = Convert.ToInt32(_oScanDTO.UserID)
                    };

                    ScannedItem scanItem = await _Scan.ValidatePallet(responseEntity);

                    _obj1.ScanResult = scanItem.ScanResult;
                    _obj1.Message = scanItem.Message;
                    _obj1.AvailableQty = scanItem.AvailableQty;

                    //_obj1.Length = scanItem.Length;
                    //_obj1.Width = scanItem.Width;
                    //_obj1.Height = scanItem.Height;
                    //_obj1.Weight = scanItem.Weight;
                    //_obj1.volume = scanItem.volume;
             
                    return JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.ScanDTO, _obj1), jsonSettings.JsonSerializerSettings));
                }
                else
                {
                    List<WMSExceptionMessage> _lstwMSExceptionMessage = new List<WMSExceptionMessage>();
                    _lstwMSExceptionMessage.Add(new WMSExceptionMessage() { WMSMessage = "Invalid Request format" });
                    return JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, _lstwMSExceptionMessage), jsonSettings.JsonSerializerSettings));
                }
            }
            catch (WMSExceptionMessage ex)
            {
                List<WMSExceptionMessage> _lstwMSExceptionMessage = new List<WMSExceptionMessage>();
                _lstwMSExceptionMessage.Add(ex);
                return JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Exception, _lstwMSExceptionMessage), jsonSettings.JsonSerializerSettings));
            }
            catch (Exception excp)
            {
                return excp.Message;
            }
        }


        [Route("ValiDateMaterial")]
        [HttpPost]
        public async Task<string> ValiDateMaterial(WMSCoreMessage oRequest)
        {
            try
            {
                DTO.ScanDTO _oScanDTO = (DTO.ScanDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);
                dynamic _obj = oRequest.EntityObject;
                ScanDTO _obj1 = JsonConvert.DeserializeObject<ScanDTO>(_obj.ToString());

                if (_oScanDTO != null)
                {
                    ScannedItem responseEntity = new ScannedItem()
                    {
                        ScanInput = _oScanDTO.ScanInput,
                        InboundID = _oScanDTO.InboundID,
                        TenantID = _oScanDTO.TenantID,
                        ObdNumber = _oScanDTO.ObdNumber,
                        VlpdNumber = _oScanDTO.VlpdNumber
                    };

                    ScannedItem scanItem = await _Scan.ValidateSKU(responseEntity);

                    //_obj1.ScanResult = scanItem.ScanResult;
                    //_obj1.SkuCode = scanItem.SkuCode;
                    //_obj1.Batch = scanItem.Batch;
                    //_obj1.SerialNumber = scanItem.SerialNumber;
                    //_obj1.Mrp = scanItem.Mrp;
                    //_obj1.LineNumber = scanItem.LineNumber;
                    //_obj1.KitID = scanItem.KitID;
                    //_obj1.PrjRef = scanItem.PrjRef;
                    //_obj1.SupplierInvoiceDetailsID = scanItem.SupplierInvoiceDetailsId;
                    //_obj1.HUNo = scanItem.HUNo.ToString();
                    //_obj1.HUSize = scanItem.HUSize.ToString();

                    //try
                    //{
                    //    //_obj1.MfgDate = (scanItem.MfgDate != "" ? DateTime.ParseExact(scanItem.MfgDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("dd-MMM-yyyy") : "");
                    //    _obj1.Message = scanItem.Message;
                    //    _obj1.MfgDate = scanItem.MfgDate;
                    //}
                    //catch (Exception ex)
                    //{
                    //    _obj1.MfgDate = string.Empty;
                    //    _obj1.Message = ex.Message;
                    //    _obj1.ScanResult = false;
                    //}
                    //try
                    //{
                    //    //_obj1.ExpDate = (scanItem.ExpDate != "" ? DateTime.ParseExact(scanItem.ExpDate, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None).ToString("dd-MMM-yyyy") : "");
                    //    _obj1.Message = scanItem.Message;
                    //    _obj1.ExpDate = scanItem.ExpDate;
                    //}
                    //catch (Exception ex)
                    //{
                    //    _obj1.ExpDate = string.Empty;
                    //    _obj1.Message = ex.Message;
                    //    _obj1.ScanResult = false;
                    //}

                    _oScanDTO.ScanResult = scanItem.ScanResult;
                    _oScanDTO.SkuCode = scanItem.SkuCode;
                    _oScanDTO.Batch = scanItem.Batch;
                    _oScanDTO.SerialNumber = scanItem.SerialNumber;
                    _oScanDTO.MfgDate = scanItem.MfgDate;
                    _oScanDTO.ExpDate = scanItem.ExpDate;
                    _oScanDTO.Mrp = scanItem.Mrp;
                    _oScanDTO.LineNumber = scanItem.LineNumber;
                    _oScanDTO.KitID = scanItem.KitID;
                    _oScanDTO.PrjRef = scanItem.PrjRef;
                    _oScanDTO.SupplierInvoiceDetailsID = scanItem.SupplierInvoiceDetailsId;
                    _oScanDTO.HUNo = scanItem.HUNo.ToString();
                    _oScanDTO.HUSize = scanItem.HUSize.ToString();
                    _oScanDTO.Message = scanItem.Message;
                    _oScanDTO.Grade = scanItem.Grade;
                    _oScanDTO.BoxSerialNo = scanItem.LabelSerialNumber;
                    _oScanDTO.MDescription = scanItem.MDescription;

                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.ScanDTO, _oScanDTO), jsonSettings.JsonSerializerSettings));

                    return json;
                }
                else
                {
                    List<WMSExceptionMessage> _lstwMSExceptionMessage = new List<WMSExceptionMessage>();
                    _lstwMSExceptionMessage.Add(new WMSExceptionMessage() { WMSMessage = "Invalid Request format" });
                    return JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, _obj.Constants.EndpointConstants.DTO.Exception, _lstwMSExceptionMessage), jsonSettings.JsonSerializerSettings));
                }
            }
            catch (WMSExceptionMessage ex)
            {
                List<WMSExceptionMessage> _lstwMSExceptionMessage = new List<WMSExceptionMessage>();
                _lstwMSExceptionMessage.Add(ex);
                return JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Exception, _lstwMSExceptionMessage), jsonSettings.JsonSerializerSettings));
            }
            catch (Exception excp)
            {
                ExceptionHandling.LogException(excp, _ClassCode + "003");
                return null;
            }
        }


        [Route("ValidateCarton")]
        [HttpPost]
        public async Task<string> ValidateCarton(WMSCoreMessage oRequest)
        {
            try
            {
                DTO.ScanDTO _oScanDTO = (DTO.ScanDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);

                if (_oScanDTO != null)
                {
                    WMSCore_BusinessEntities.Entities.ScannedItem obj = new WMSCore_BusinessEntities.Entities.ScannedItem()
                    {
                        ScanInput = _oScanDTO.ScanInput,
                        ObdNumber = _oScanDTO.ObdNumber,
                        AccountID = _oScanDTO.AccountID
                    };
                    WMSCore_BusinessEntities.Entities.ScannedItem scanItem = await _Scan.ValidateCarton(obj);
                    _oScanDTO.ScanResult = scanItem.ScanResult;
                    _oScanDTO.Message = scanItem.Message;

                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.ScanDTO, _oScanDTO), jsonSettings.JsonSerializerSettings));
                    return json;
                }
                else
                {
                    List<WMSExceptionMessage> _lstwMSExceptionMessage = new List<WMSExceptionMessage>();
                    _lstwMSExceptionMessage.Add(new WMSExceptionMessage() { WMSMessage = "Invalid Request format" });
                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Exception, _lstwMSExceptionMessage), jsonSettings.JsonSerializerSettings));
                    return json;
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
                ExceptionHandling.LogException(excp, _ClassCode + "004");
                return null;
            }
        }


        [Route("ValidateSO")]
        [HttpPost]
        public async Task<string> ValidateSO(WMSCoreMessage oRequest) 
        {
            try
            {
                DTO.ScanDTO _oScanDTO = (DTO.ScanDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);

                if (_oScanDTO != null)
                {
                    WMSCore_BusinessEntities.Entities.ScannedItem obj = new WMSCore_BusinessEntities.Entities.ScannedItem()
                    {
                        ScanInput = _oScanDTO.ScanInput,
                        AccountID = _oScanDTO.AccountID,
                        UserID = Convert.ToInt32(_oScanDTO.UserID)
                    };
                    WMSCore_BusinessEntities.Entities.ScannedItem scanItem = await _Scan.ValidateSO(obj);
                    _oScanDTO.ScanResult = scanItem.ScanResult;
                    _oScanDTO.Message = scanItem.Message;

                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.ScanDTO, _oScanDTO), jsonSettings.JsonSerializerSettings));
                    return json;
                }
                else
                {
                    List<WMSExceptionMessage> _lstwMSExceptionMessage = new List<WMSExceptionMessage>();
                    _lstwMSExceptionMessage.Add(new WMSExceptionMessage() { WMSMessage = "Invalid Request format" });
                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Exception, _lstwMSExceptionMessage), jsonSettings.JsonSerializerSettings));
                    return json;
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
                ExceptionHandling.LogException(excp, _ClassCode + "004");
                return null;
            }
        }

    }
}
