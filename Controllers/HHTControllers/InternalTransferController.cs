using FWMSC21Core.Entities;
using Simpolo_Endpoint.BO;
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
using Simpolo_Endpoint.Models;
using Simpolo_Endpoint.DAO.interfaces;

namespace Simpolo_Endpoint.Controllers.HHTControllers
{
    [Route("Transfers")]
    //[Authorize]
    [ApiController]
    public class InternalTransferController : ControllerBase
    {
        private readonly IInternalTransfer _InternalTransfer;
        //BaseController baseController = new BaseController();
        JsonSettings jsonSettings = new JsonSettings();
        public InternalTransferController(IInternalTransfer internalTransfer)
        {
            _InternalTransfer = internalTransfer;
            _ClassCode = ExceptionHandling.GetClassExceptionCode(ExceptionHandling.ExcpConstants_API_Enpoint.InternalTransferController);
        }

        private string _ClassCode = string.Empty;

        [HttpPost]
        [Route("TransferPalletToLocation")]
        public async Task<string> TransferPalletToLocation(WMSCoreMessage oRequest)
        {
            try
            {
                InventoryDTO _oinventoryDTO1 = new InventoryDTO();
                if (oRequest != null)
                {
                    InventoryDTO _oinventoryDTO = (InventoryDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);

                    int UserID = Convert.ToInt32(oRequest.AuthToken.UserID);

                    InventoryDTO _oInventory = new InventoryDTO();
                    if (_oinventoryDTO != null)
                    {
                        _oInventory.ContainerCode = _oinventoryDTO.ContainerCode;
                        _oInventory.LocationCode = _oinventoryDTO.LocationCode;
                        _oInventory.WarehouseID = _oinventoryDTO.WarehouseId;
                        _oInventory.TenantID = _oinventoryDTO.TenantID;
                        _oInventory.MaterialCode = _oinventoryDTO.MaterialCode;
                        _oInventory.BatchNo = _oinventoryDTO.BatchNo;
                        _oInventory.SerialNo = _oinventoryDTO.SerialNo;
                        _oInventory.MfgDate = _oinventoryDTO.MfgDate;
                        _oInventory.ExpDate = _oinventoryDTO.ExpDate;
                        _oInventory.ProjectNo = _oinventoryDTO.ProjectNo;
                        _oInventory.MRP = _oinventoryDTO.MRP;
                        _oInventory.Quantity = _oinventoryDTO.Quantity;
                        _oInventory.ReasonId = _oinventoryDTO.ReasonId;
                        _oinventoryDTO1 = await _InternalTransfer.TransferPallettoLocation(_oInventory, UserID);
                    }

                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Inventory, _oInventory), jsonSettings.JsonSerializerSettings));
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
                ExceptionHandling.LogException(excp, _ClassCode + "010");
                return null;
            }
        }


        [HttpPost]
        [Route("ChekContainerLocation")]
        public async Task<string> ChekContainerLocation(WMSCoreMessage oRequest)
        {
            try
            {
                string LocationCode = "";
                string LocationCheck = "";
#pragma warning disable CS0219 // The variable 'UserID' is assigned but its value is never used
                int UserID = 0;
#pragma warning restore CS0219 // The variable 'UserID' is assigned but its value is never used

                InventoryDTO oinventoryDTO = new InventoryDTO();
                TransferBO oresponseTransfer = new TransferBO();              
                List<InventoryDTO> _lstinventory = new List<InventoryDTO>();
                if (oRequest != null)
                {
                    InventoryDTO _oinventoryDTO = (InventoryDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);
               
                    if (_oinventoryDTO != null)
                    {
                        LocationCheck = _oinventoryDTO.LocationCode;
                        LocationCode = await _InternalTransfer.GetConatinerLocationBin(_oinventoryDTO.ContainerCode, _oinventoryDTO.WarehouseId, oRequest.AuthToken.UserID, LocationCheck);
                    }
                    if (LocationCode != null || LocationCode != string.Empty)
                    {
                        if (LocationCheck == LocationCode)
                        {
                            TransferBO oTransfer = new TransferBO()
                            {
                                AccountId = ConversionUtility.ConvertToInt(_oinventoryDTO.AccountID),
                                MCode = "",
                                FromSLoc = "",
                                FromLocation = _oinventoryDTO.LocationCode,
                                FromCartonNo = _oinventoryDTO.ContainerCode,
                                MfgDate = "",
                                ExpDate = "",
                                SerialNo = "",
                                BatchNo = "",
                                ProjectNo = "",
                                MRP = "",
                                TenantID = _oinventoryDTO.TenantID,
                                WarehouseID = _oinventoryDTO.WarehouseId,
                                UserID = Convert.ToString(_oinventoryDTO.UserID)
                            };

                            oresponseTransfer = await _InternalTransfer.GetAvailbleQtyList(oTransfer);

                            if (oresponseTransfer != null)
                                oinventoryDTO.Result = "1";
                            oinventoryDTO.Quantity = oresponseTransfer.AvailQty;
                            //oinventoryDTO.SLOC = oresponseTransfer.FromSLoc;
                        }
                        else
                        {
                            if (LocationCode == "-1")
                            {
                                throw new WMSExceptionMessage() { WMSExceptionCode = null, WMSMessage = "Container does not exist in the Warehouse", ShowAsError = true };
                            }
                            else if (LocationCode != "" && LocationCode != "1")
                            {
                                throw new WMSExceptionMessage() { WMSExceptionCode = null, WMSMessage = "Container does not exist in the Location", ShowAsError = true };
                            }
                            else
                            {
                                oinventoryDTO.Result = "1";
                                oinventoryDTO.Quantity = oresponseTransfer.AvailQty;
                                oinventoryDTO.SLOC = oresponseTransfer.FromSLoc;
                            }
                        }
                    }
                    _lstinventory.Add(oinventoryDTO);
               
                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Inventory, _lstinventory), jsonSettings.JsonSerializerSettings));
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
        [Route("GetActivestockStorageLocations")]
        public async Task<string> GetActivestockStorageLocations(WMSCoreMessage oRequest)
        {
            try
            {
                TransferBO oresponseTransfer = new TransferBO();
                List<InventoryDTO> _lstinventory = new List<InventoryDTO>();
                if (oRequest != null)
                {
#pragma warning disable CS0219 // The variable 'result' is assigned but its value is never used
                    string result = "";
#pragma warning restore CS0219 // The variable 'result' is assigned but its value is never used
                    InventoryDTO _oinventoryDTO = (InventoryDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);
                    List<Inventory> inventories = new List<Inventory>();

                    List<Inventory> _lstInv = new List<Inventory>();

                    if (_oinventoryDTO != null)
                    {
                        inventories = await _InternalTransfer.GetActivestockStorageLocations(_oinventoryDTO.MaterialCode , _oinventoryDTO.UserId);

                        foreach (Inventory inventory in inventories)
                        {
                            Inventory _oInvDTO = new Inventory();

                            _oInvDTO.StorageLocationID = inventory.StorageLocationID;
                            _oInvDTO.StorageLocation = inventory.StorageLocation;

                            _lstInv.Add(_oInvDTO);
                        }
                    }

                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Inventory, _lstInv), jsonSettings.JsonSerializerSettings));
                    return json;
                }
                else
                    return null;
            }
            catch (Exception excp)
            {
                ExceptionHandling.LogException(excp, _ClassCode + "006");
                return null;
            }
        }


        [HttpPost]
        [Route("UpsertBinToBinTransfer")]
        public async Task<string> UpsertBinToBinTransfer(WMSCoreMessage oRequest)
        {
            try
            {
                TransferBO oresponseTransfer = new TransferBO();
                InventoryDTO oInventoryDTO = new InventoryDTO();
                List<InventoryDTO> _lstinventory = new List<InventoryDTO>();
                if (oRequest != null)
                {
                    InventoryDTO _oinventoryDTO = (InventoryDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);
                    if (_oinventoryDTO != null)
                    {
                        TransferBO oTransfer = new TransferBO()
                        {
                            FromLocation = _oinventoryDTO.LocationCode,
                            MfgDate = _oinventoryDTO.MfgDate,
                            ExpDate = _oinventoryDTO.ExpDate,
                            SerialNo = _oinventoryDTO.SerialNo,
                            BatchNo = _oinventoryDTO.BatchNo,
                            ProjectNo = _oinventoryDTO.ProjectNo,
                            UserId = _oinventoryDTO.UserId,
                            FromCartonNo = _oinventoryDTO.ContainerCode,
                            MCode = _oinventoryDTO.MaterialCode,
                            ToLocation = _oinventoryDTO.ToLocationCode,
                            TransferQty = _oinventoryDTO.Quantity,
                            FromSLoc = _oinventoryDTO.SLOC,
                            ToSLoc = _oinventoryDTO.SLOC,
                            ToCartonNo = _oinventoryDTO.ToContainerCode,
                            WarehouseID = _oinventoryDTO.WarehouseId,
                            TenantID = _oinventoryDTO.TenantID,
                            MRP = _oinventoryDTO.MRP
                        };

                        oresponseTransfer = await _InternalTransfer.UpsertBinToBinTransferItem(oTransfer);

                        if (oresponseTransfer != null)
                        {
                            oInventoryDTO.Result = oresponseTransfer.Result;
                        }

                        _lstinventory.Add(oInventoryDTO);
                    }            

                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Inventory, _lstinventory), jsonSettings.JsonSerializerSettings));
                    return json;
                }
                else
                    return null;
            }
            catch (Exception excp)
            {
                ExceptionHandling.LogException(excp, _ClassCode + "006");
                return null;
            }
        }


        [HttpPost]
        [Route("GetSKUList")]
        public async Task<string> GetSKUList(WMSCoreMessage oRequest)
        {
            try
            {
                if (oRequest != null)
                {
                    List<InventoryDTO> _lstInventoryDTO = new List<InventoryDTO>();
                    OutboundDTO _outboundDTO = (OutboundDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);

                    List<Inventory> _listInvetory = new List<Inventory>();

                    _listInvetory = await _InternalTransfer.GetSKUList(_outboundDTO.OutboundID);

                    foreach (Inventory inventory in _listInvetory)
                    {
                        InventoryDTO inventoryDTO = new InventoryDTO();

                        inventoryDTO.MaterialCode = inventory.MaterialCode;
                        inventoryDTO.Quantity = Convert.ToString(inventory.Quantity);
                        inventoryDTO.BatchNo = inventory.BatchNumber;
                        inventoryDTO.VLPDId = inventory.VLPDId.ToString();
                        _lstInventoryDTO.Add(inventoryDTO);
                    }

                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Inventory, _lstInventoryDTO), jsonSettings.JsonSerializerSettings));
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
                ExceptionHandling.LogException(excp, _ClassCode + "012");
                return null;
            }
        }


        [HttpPost]
        [Route("GetLocationsBySKU")]
        public async Task<string> GetLocationsBySKU(WMSCoreMessage oRequest)
        {
            try
            {
                if (oRequest != null)
                {
                    List<InventoryDTO> _lstInventoryDTO = new List<InventoryDTO>();
                    OutboundDTO _outboundDTO = (OutboundDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);

                    List<Inventory> _listInvetory = new List<Inventory>();

                    _listInvetory = await _InternalTransfer.GetLocationsBySKU(_outboundDTO.MCode, _outboundDTO.BatchNo, String.IsNullOrEmpty(_outboundDTO.IsMoreOptions) ? "0" : _outboundDTO.IsMoreOptions, String.IsNullOrEmpty(_outboundDTO.IsWorkOrder) ? "0" : _outboundDTO.IsWorkOrder);

                    foreach (Inventory inventory in _listInvetory)
                    {
                        InventoryDTO inventoryDTO = new InventoryDTO();

                        inventoryDTO.MaterialCode = inventory.MaterialCode;
                        inventoryDTO.Quantity = Convert.ToString(inventory.Quantity);
                        inventoryDTO.LocationCode = inventory.LocationCode.ToString();
                        inventoryDTO.BatchNo = inventory.BatchNumber.ToString();
                        _lstInventoryDTO.Add(inventoryDTO);
                    }
        
                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Inventory, _lstInventoryDTO), jsonSettings.JsonSerializerSettings));
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
                ExceptionHandling.LogException(excp, _ClassCode + "012");
                return null;
            }
        }


        [HttpPost]
        [Route("GetDockLocations")]
        public async Task<string> GetDockLocations(WMSCoreMessage oRequest)
        {
            try
            {
                if (oRequest != null)
                {

          
                    List<InventoryDTO> _lstInventoryDTO = new List<InventoryDTO>();

                    List<Inventory> _listInvetory = new List<Inventory>();

                    _listInvetory = await _InternalTransfer.GetDockLocations();

                    foreach (Inventory inventory in _listInvetory)
                    {
                        InventoryDTO inventoryDTO = new InventoryDTO();
                        inventoryDTO.LocationID = inventory.LocationID;
                        inventoryDTO.LocationCode = inventory.LocationCode;
                        _lstInventoryDTO.Add(inventoryDTO);
                    }

                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Inventory, _lstInventoryDTO), jsonSettings.JsonSerializerSettings));
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
                ExceptionHandling.LogException(excp, _ClassCode + "012");
                return null;
            }
        }


        [HttpPost]
        [Route("GetLoadingPoints")]
        public async Task<string> GetLoadingPoints(WMSCoreMessage oRequest)
        {
            try
            {
                if (oRequest != null)
                {


                    List<InventoryDTO> _lstInventoryDTO = new List<InventoryDTO>();
                    InventoryDTO _inventorydDTO = (InventoryDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);

                    List<Inventory> _listInvetory = new List<Inventory>();

                    _listInvetory = await _InternalTransfer.GetLoadingPoints(_inventorydDTO.TenantID,_inventorydDTO.UserId, _inventorydDTO.WarehouseID, _inventorydDTO.VehicleNumber);

                    foreach (Inventory inventory in _listInvetory)
                    {
                        InventoryDTO inventoryDTO = new InventoryDTO(); 
                        inventoryDTO.LoadingPointID = inventory.LoadingPointID;
                        inventoryDTO.LoadingPoint = inventory.LoadingPoint;
                        _lstInventoryDTO.Add(inventoryDTO);
                    }

                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Inventory, _lstInventoryDTO), jsonSettings.JsonSerializerSettings));
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
                ExceptionHandling.LogException(excp, _ClassCode + "012");
                return null;
            }
        }


        [HttpPost]
        [Route("GetLocations")]
        public async Task<string> GetLocations(WMSCoreMessage oRequest)
        {
            try
            {
                if (oRequest != null)
                {

             
                    List<InventoryDTO> _lstInventoryDTO = new List<InventoryDTO>();
                    InventoryDTO _inventorydDTO = (InventoryDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);

                    List<Inventory> _listInvetory = new List<Inventory>();

                    _listInvetory = await _InternalTransfer.GetLocations(_inventorydDTO.RefNumber,_inventorydDTO.WarehouseID, _inventorydDTO.TenantID,_inventorydDTO.UserId,_inventorydDTO.InboundID,_inventorydDTO.IsScerario);

                    foreach (Inventory inventory in _listInvetory)
                    {
                        InventoryDTO inventoryDTO = new InventoryDTO();
                        inventoryDTO.LocationID = inventory.LocationID;
                        inventoryDTO.LocationCode = inventory.LocationCode;
                        _lstInventoryDTO.Add(inventoryDTO);
                    }

                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Inventory, _lstInventoryDTO), jsonSettings.JsonSerializerSettings));
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
                ExceptionHandling.LogException(excp, _ClassCode + "012");
                return null;
            }
        }


        [HttpPost]
        [Route("GetLocationsBatchPicking")]
        public async Task<string> GetLocationsBatchPicking(WMSCoreMessage oRequest)
        {
            try
            {
                if (oRequest != null)
                {


                    List<InventoryDTO> _lstInventoryDTO = new List<InventoryDTO>();
                    InventoryDTO _inventorydDTO = (InventoryDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);

                    List<Inventory> _listInvetory = new List<Inventory>();

                    _listInvetory = await _InternalTransfer.GetLocationsBatchPicking(_inventorydDTO.RefNumber, _inventorydDTO.WarehouseID, _inventorydDTO.TenantID, _inventorydDTO.UserId, _inventorydDTO.InboundID, _inventorydDTO.IsScerario,_inventorydDTO.MaterialCode,_inventorydDTO.BatchNo,_inventorydDTO.Grade,_inventorydDTO.VLPDId);

                    foreach (Inventory inventory in _listInvetory)
                    {
                        InventoryDTO inventoryDTO = new InventoryDTO();
                        inventoryDTO.LocationID = inventory.LocationID;
                        inventoryDTO.LocationCode = inventory.LocationCode;
                        _lstInventoryDTO.Add(inventoryDTO);
                    }

                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Inventory, _lstInventoryDTO), jsonSettings.JsonSerializerSettings));
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
                ExceptionHandling.LogException(excp, _ClassCode + "012");
                return null;
            }
        }





        [HttpPost]
        [Route("WorkOrderLineItemComplete")]
        public async Task<string> WorkOrderLineItemComplete(WMSCoreMessage oRequest)
        {
            try
            {
                if (oRequest != null)
                {
                    List<InventoryDTO> _lstInventoryDTO = new List<InventoryDTO>();
                    InventoryDTO _oinventoryDTO = (InventoryDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);
                    await _InternalTransfer.WorkOrderLineItemComplete(_oinventoryDTO.VLPDId);

                    List<Inventory> _listInvetory = new List<Inventory>();
                    _listInvetory = await _InternalTransfer.GetSKUList(_oinventoryDTO.OutboundID.ToString());

                    foreach (Inventory inventory in _listInvetory)
                    {
                        InventoryDTO inventoryDTO = new InventoryDTO();

                        inventoryDTO.MaterialCode = inventory.MaterialCode;
                        inventoryDTO.Quantity = Convert.ToString(inventory.Quantity);
                        inventoryDTO.BatchNo = inventory.BatchNumber;
                        inventoryDTO.VLPDId = inventory.VLPDId.ToString();
                        _lstInventoryDTO.Add(inventoryDTO);
                    }
               
                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Inventory, _lstInventoryDTO), jsonSettings.JsonSerializerSettings));
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
                ExceptionHandling.LogException(excp, _ClassCode + "012");
                return null;
            }
        }


        [HttpPost]
        [Route("MaterialTransferBlockComplete")]
        public async Task<string> MaterialTransferBlockComplete(WMSCoreMessage oRequest)
        {
            try
            {
                if (oRequest != null)
                {
                    List<InventoryDTO> _lstInventoryDTO = new List<InventoryDTO>();
                    InventoryDTO _oinventoryDTO = (InventoryDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);
                    List<Inventory> _listInvetory = new List<Inventory>();
                    _listInvetory = await _InternalTransfer.MaterialTransferBlockComplete(_oinventoryDTO.TransferRefId,_oinventoryDTO.UserID,_oinventoryDTO.BlockReasonID);

                   
                    foreach (Inventory inventory in _listInvetory)
                    {
                        InventoryDTO inventoryDTO = new InventoryDTO();
                        inventoryDTO.Result = inventory.Result.ToString();
                        inventoryDTO.ResponseMessage = inventory.ResponseMessage.ToString();
                        _lstInventoryDTO.Add(inventoryDTO);
                    }

                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Inventory, _lstInventoryDTO), jsonSettings.JsonSerializerSettings));
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
                ExceptionHandling.LogException(excp, _ClassCode + "012");
                return null;
            }
        }







        [HttpPost]
        [Route("TransferPalletToLocation_Putaway")]
        public async Task<string> TransferPalletToLocation_Putaway(WMSCoreMessage oRequest)
        {
            try
            {
                if (oRequest != null)
                {
                    InventoryDTO _oinventoryDTO = (InventoryDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);
                    Inventory _oInventory = new Inventory();
                    if (_oinventoryDTO != null)
                    {
                        _oInventory.ContainerCode = _oinventoryDTO.ContainerCode;
                        _oInventory.LocationCode = _oinventoryDTO.LocationCode;
                        _oInventory.WarehouseID = Convert.ToInt32(_oinventoryDTO.WarehouseId);
                        _oInventory.TenantID = Convert.ToInt32(_oinventoryDTO.TenantID);
                        _oInventory.MaterialCode = _oinventoryDTO.MaterialCode;
                        _oInventory.Quantity = Convert.ToDecimal(_oinventoryDTO.Quantity);

                        await _InternalTransfer.TransferPallettoLocation_Putaway(_oInventory);
                    }
              
                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Inventory, _oInventory), jsonSettings.JsonSerializerSettings));
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
                ExceptionHandling.LogException(excp, _ClassCode + "010");
                return null;
            }
        }


        [HttpPost]
        [Route("UpsertBinToBinTransferItem")]
        public async Task<string> UpsertBinToBinTransferItem(WMSCoreMessage oRequest)
        {
            try
            {
                BO.TransferBO oresponseTransfer = new BO.TransferBO();
                InventoryDTO oInventoryDTO = new InventoryDTO();
                List<InventoryDTO> _lstinventory = new List<InventoryDTO>();
                if (oRequest != null)
                {
                    InventoryDTO _oinventoryDTO = (InventoryDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);
                    if (_oinventoryDTO != null)
                    {
                        BO.TransferBO oTransfer = new BO.TransferBO()
                        {
                            FromLocation = _oinventoryDTO.LocationCode,
                            MfgDate = _oinventoryDTO.MfgDate,
                            ExpDate = _oinventoryDTO.ExpDate,
                            SerialNo = _oinventoryDTO.SerialNo,
                            BatchNo = _oinventoryDTO.BatchNo,
                            ProjectNo = _oinventoryDTO.ProjectNo,
                            UserId = _oinventoryDTO.UserId,
                            FromCartonNo = _oinventoryDTO.ContainerCode,
                            MCode = _oinventoryDTO.MaterialCode,
                            ToLocation = _oinventoryDTO.ToLocationCode,
                            TransferQty = _oinventoryDTO.Quantity,
                            FromSLoc = _oinventoryDTO.SLOC,
                            ToSLoc = _oinventoryDTO.SLOC,
                            ToCartonNo = _oinventoryDTO.ToContainerCode,
                            WarehouseID = _oinventoryDTO.WarehouseId,
                            TenantID = _oinventoryDTO.TenantID,
                            MRP = _oinventoryDTO.MRP,
                            EmpreqNumber = _oinventoryDTO.EmpreqNumber,
                            TransferOrderId = string.IsNullOrEmpty(_oinventoryDTO.IsWorkOrder) ? 0 : Convert.ToInt32(_oinventoryDTO.IsWorkOrder)
                        };

                        oresponseTransfer = await _InternalTransfer.UpsertBinToBinTransferItem(oTransfer);

                        if (oresponseTransfer != null)
                        {
                            oInventoryDTO.Result = oresponseTransfer.Result;
                        }

                        _lstinventory.Add(oInventoryDTO);
                    }

                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Inventory, _lstinventory), jsonSettings.JsonSerializerSettings));
                    return json;
                }
                else
                    return null;
            }
            catch (Exception excp)
            {
                ExceptionHandling.LogException(excp, _ClassCode + "006");
                return null;
            }
        }


        [HttpPost]
        [Route("UpsertPalletBuilding")]
        public async Task<string> UpsertPalletBuilding(WMSCoreMessage oRequest)
        {
            try
            {
                BO.TransferBO oresponseTransfer = new BO.TransferBO();
                InventoryDTO oInventoryDTO = new InventoryDTO();
                List<InventoryDTO> _lstinventory = new List<InventoryDTO>();
                if (oRequest != null)
                {
                    InventoryDTO _oinventoryDTO = (InventoryDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);
                    if (_oinventoryDTO != null)
                    {
                        BO.TransferBO oTransfer = new BO.TransferBO()
                        {
                            FromLocation = _oinventoryDTO.LocationCode,
                            FromCartonNo = _oinventoryDTO.ContainerCode,
                            ToCartonNo = _oinventoryDTO.ToContainerCode,
                            MCode = _oinventoryDTO.MaterialCode,
                            UserId = _oinventoryDTO.UserId,
                            TransferQty = _oinventoryDTO.Quantity,
                            FromSLoc = _oinventoryDTO.SLOC,
                        };

                        oresponseTransfer = await _InternalTransfer.UpsertPalletBuilding(oTransfer);

                        if (oresponseTransfer != null)
                        {
                            oInventoryDTO.Result = oresponseTransfer.Result;
                        }

                        _lstinventory.Add(oInventoryDTO);
                    }

                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Inventory, _lstinventory), jsonSettings.JsonSerializerSettings));
                    return json;
                }
                else
                    return null;
            }
            catch (Exception excp)
            {
                ExceptionHandling.LogException(excp, _ClassCode + "006");
                return null;
            }
        }


        [HttpPost]
        [Route("GetAvailbleQtyList")]
        public async Task<string> GetAvailbleQtyList(WMSCoreMessage oRequest)
        {
            try
            {
                BO.TransferBO oresponseTransfer = new BO.TransferBO();
                InventoryDTO oInventoryDTO = new InventoryDTO();
                List<InventoryDTO> _lstinventory = new List<InventoryDTO>();
                if (oRequest != null)
                {
                    InventoryDTO _oinventoryDTO = (InventoryDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);
                    if (_oinventoryDTO != null)
                    {
                        BO.TransferBO oTransfer = new BO.TransferBO()
                        {
                            AccountId = ConversionUtility.ConvertToInt(_oinventoryDTO.AccountID),
                            MCode = _oinventoryDTO.MaterialCode,
                            FromSLoc = _oinventoryDTO.SLOC,
                            FromLocation = _oinventoryDTO.LocationCode,
                            FromCartonNo = _oinventoryDTO.ContainerCode,
                            MfgDate = _oinventoryDTO.MfgDate,
                            ExpDate = _oinventoryDTO.ExpDate,
                            SerialNo = _oinventoryDTO.SerialNo,
                            BatchNo = _oinventoryDTO.BatchNo,
                            ProjectNo = _oinventoryDTO.ProjectNo,
                            MRP = _oinventoryDTO.MRP,
                            TenantID = _oinventoryDTO.TenantID,
                            WarehouseID = _oinventoryDTO.WarehouseId,
                            UserID = Convert.ToString(_oinventoryDTO.UserId),
                            TransferOrderId = string.IsNullOrEmpty(_oinventoryDTO.IsWorkOrder) ? 0 : Convert.ToInt32(_oinventoryDTO.IsWorkOrder)
                        };

                        oresponseTransfer = await _InternalTransfer.GetAvailbleQtyList(oTransfer);

                        if (oresponseTransfer != null)
                        {
                            if (oresponseTransfer.AvailQty == "0")
                            {
                                throw new WMSExceptionMessage() { WMSExceptionCode = null, WMSMessage = "No stock available in the Location", ShowAsError = true };
                            }
                            else if (oresponseTransfer.AvailQty == "-1")
                            {
                                throw new WMSExceptionMessage() { WMSExceptionCode = null, WMSMessage = "Material does not belong to the Tenant", ShowAsError = true };
                            }
                            oInventoryDTO.Quantity = oresponseTransfer.AvailQty;
                            // oInventoryDTO.SLOC = oresponseTransfer.FromSLoc;
                        }

                        _lstinventory.Add(oInventoryDTO);
                    }

                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Inventory, _lstinventory), jsonSettings.JsonSerializerSettings));
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
                ExceptionHandling.LogException(excp, _ClassCode + "004");
                return null;
            }
        }


        [HttpPost]
        [Route("GetSLocWiseActiveStockInfo")]
        public async Task<string> GetSLocWiseActiveStockInfo(WMSCoreMessage oRequest)
        {
            try
            {
                if (oRequest != null)
                {
                    InventoryDTO _oinventoryDTO = (InventoryDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);
                    Inventory _oInventory = new Inventory();
                    List<InventoryDTO> _lstInventoryDTO = new List<InventoryDTO>();
                    if (_oinventoryDTO != null)
                    {
                        _oInventory.ContainerCode = _oinventoryDTO.ContainerCode;
                        _oInventory.LocationCode = _oinventoryDTO.LocationCode;
                        _oInventory.MaterialCode = _oinventoryDTO.MaterialCode;
                        _oInventory.WarehouseID = Convert.ToInt32(_oinventoryDTO.WarehouseId);
                        _oInventory.TenantID = Convert.ToInt32(_oinventoryDTO.TenantID);
                        _oInventory.BatchNumber = _oinventoryDTO.BatchNo;
                        _oInventory.SerialNo = _oinventoryDTO.SerialNo;
                        _oInventory.Mfg_Date = _oinventoryDTO.MfgDate;
                        _oInventory.ExpDate = _oinventoryDTO.ExpDate;
                        _oInventory.ProjectRefNo = _oinventoryDTO.ProjectNo;
                        _oInventory.MRP = ConversionUtility.ConvertToDecimal(_oinventoryDTO.MRP);

                        List<Inventory> _listInvetory = new List<Inventory>();

                        _listInvetory = await _InternalTransfer.GetSlocWiseActiveStock(_oInventory);

                        foreach (Inventory inventory in _listInvetory)
                        {
                            InventoryDTO inventoryDTO = new InventoryDTO();

                            inventoryDTO.StorageLocation = inventory.StorageLocation.ToString();
                            inventoryDTO.Quantity = inventory.AvailableQuantity.ToString();
                            _lstInventoryDTO.Add(inventoryDTO);
                        }
                    }
                 
                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Inventory, _lstInventoryDTO), jsonSettings.JsonSerializerSettings));
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
                ExceptionHandling.LogException(excp, _ClassCode + "011");
                return null;
            }
        }


        [HttpPost]
        [Route("UpdateMaterialTransfer")]
        public async Task<string> UpdateMaterialTransfer(WMSCoreMessage oRequest)
        {
            try
            {
                if (oRequest != null)
                {
                    InventoryDTO _oinventoryDTO = (InventoryDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);
                    Inventory _oInventory = new Inventory();
                    List<InventoryDTO> _lstInventoryDTO = new List<InventoryDTO>();
                    if (_oinventoryDTO != null)
                    {
                        _oInventory.ContainerCode = _oinventoryDTO.ContainerCode;
                        _oInventory.LocationCode = _oinventoryDTO.LocationCode;
                        _oInventory.MaterialCode = _oinventoryDTO.MaterialCode;
                        _oInventory.WarehouseID = Convert.ToInt32(_oinventoryDTO.WarehouseId);
                        _oInventory.TenantID = Convert.ToInt32(_oinventoryDTO.TenantID);
                        _oInventory.BatchNumber = _oinventoryDTO.BatchNo;
                        _oInventory.Quantity = ConversionUtility.ConvertToDecimal(_oinventoryDTO.Quantity);
                        _oInventory.StorageLocation = _oinventoryDTO.StorageLocation;
                        _oInventory.ToStorageLocation = _oinventoryDTO.ToStorageLocation;
                        _oInventory.UserId = _oinventoryDTO.UserId;

                        List<Inventory> _listInvetory = new List<Inventory>();

                        _listInvetory = await _InternalTransfer.UpdateMaterialTrasnfer(_oInventory);

                        foreach (Inventory inventory in _listInvetory)
                        {
                            InventoryDTO inventoryDTO = new InventoryDTO();

                            inventoryDTO.Result = inventory.Result.ToString();

                            _lstInventoryDTO.Add(inventoryDTO);
                        }
                    }
               
                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Inventory, _lstInventoryDTO), jsonSettings.JsonSerializerSettings));
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
                ExceptionHandling.LogException(excp, _ClassCode + "012");
                return null;
            }
        }




        [HttpPost]
        [Route("UpsertMaterialTransferItem_HHT")]
        public async Task<string> UpsertMaterialTransferItem_HHT(WMSCoreMessage oRequest)
        {
            try
            {
                if (oRequest != null)
                {
                    InventoryDTO _oinventoryDTO = (InventoryDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);
                    Inventory _oInventory = new Inventory();
                    List<InventoryDTO> _lstInventoryDTO = new List<InventoryDTO>();
                    if (_oinventoryDTO != null)
                    {
                        _oInventory.ContainerCode = _oinventoryDTO.ContainerCode;
                        _oInventory.LocationCode = _oinventoryDTO.LocationCode;
                        _oInventory.MaterialCode = _oinventoryDTO.MaterialCode;
                        _oInventory.WarehouseID = Convert.ToInt32(_oinventoryDTO.WareHouseID);
                        _oInventory.TenantID = Convert.ToInt32(_oinventoryDTO.TenantID);
                        _oInventory.BatchNumber = _oinventoryDTO.BatchNo;
                        _oInventory.Quantity = ConversionUtility.ConvertToDecimal(_oinventoryDTO.Quantity);
                        _oInventory.StorageLocation = _oinventoryDTO.StorageLocation;
                        _oInventory.ToStorageLocation = _oinventoryDTO.ToStorageLocation;
                        _oInventory.UserId = _oinventoryDTO.UserId;
                        _oInventory.TransferRequestedID = _oinventoryDTO.TransferRefId;
                        _oInventory.Grade = _oinventoryDTO.Grade;
                      

                        List<Inventory> _listInvetory = new List<Inventory>();

                        _listInvetory = await _InternalTransfer.UpsertMaterialTransferItem_HHT(_oInventory);

                        foreach (Inventory inventory in _listInvetory)
                        {
                            InventoryDTO inventoryDTO = new InventoryDTO();

                            inventoryDTO.Result = inventory.Result.ToString();
                            inventoryDTO.ResponseMessage = inventory.ResponseMessage.ToString();
                            inventoryDTO.ItemCount = inventory.Count;

                            _lstInventoryDTO.Add(inventoryDTO);
                        }
                    }

                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Inventory, _lstInventoryDTO), jsonSettings.JsonSerializerSettings));
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
                ExceptionHandling.LogException(excp, _ClassCode + "012");
                return null;
            }
        }







        [HttpPost]
        [Route("GetBinToBinStorageLocations")]
        public async Task<string> GetBinToBinStorageLocations(WMSCoreMessage oRequest)
        {
            try
            {
                DataSet _userDataTable = null;
                BO.TransferBO oresponseTransfer = new BO.TransferBO();
                List<InventoryDTO> _lstinventory = new List<InventoryDTO>();
                if (oRequest != null)
                {
                    InventoryDTO _oinventoryDTO = (InventoryDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);
                    if (_oinventoryDTO != null)
                    {
                         _userDataTable = await _InternalTransfer.GetStorageLocations();

                        if (_userDataTable.Tables[0].Rows.Count > 0)
                        {
                            _lstinventory = (from DataRow dr in _userDataTable.Tables[0].Rows
                                             select new InventoryDTO
                                             {
                                                 LocationCode = dr[1].ToString()
                                             }).ToList();
                        }
                    }
               
                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Inventory, _lstinventory), jsonSettings.JsonSerializerSettings));
                    return json;
                }
                else
                    return null;
            }
            catch (Exception excp)
            {
                ExceptionHandling.LogException(excp, _ClassCode + "006");
                return null;
            }
        }

        [HttpPost]
        [Route("GetTransferBlockReasons")]
        public async Task<string> GetTransferBlockReasons(WMSCoreMessage oRequest)
        {
            try
            {
                DataSet userDataTable = null;
                List<InventoryDTO> _lstinventory = new List<InventoryDTO>();

                if (oRequest != null)
                {

                    userDataTable = await _InternalTransfer.GetTransferBlockReasons();
                    foreach (DataRow row in userDataTable.Tables[0].Rows)
                    {
                        InventoryDTO responseDTO = new InventoryDTO();
                        responseDTO.BlockReason = row["BlockReason"].ToString();
                        responseDTO.BlockReasonID = Convert.ToInt32(row["BlockReasonID"]);
                        _lstinventory.Add(responseDTO);
                    }

                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Inventory, _lstinventory), jsonSettings.JsonSerializerSettings));
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
                ExceptionHandling.LogException(excp, _ClassCode + "003");
                return null;
            }
        }


        [HttpPost]
        [Route("GetTransferReqNos")]
        public async Task<string> GetTransferReqNos(WMSCoreMessage oRequest)
        {
            try
            {            
                DataSet userDataTable = null;
                List<InventoryDTO> _lstinventory = new List<InventoryDTO>();

                if (oRequest != null)
                {
                    InventoryDTO _oinventoryDTO = (InventoryDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);

                    if (_oinventoryDTO != null)
                    {
                        BO.TransferBO oTransfer = new BO.TransferBO()
                        {
                            AccountId = ConversionUtility.ConvertToInt(_oinventoryDTO.AccountID),
                            TenantID = _oinventoryDTO.TenantID,
                            IsBlockScreen = _oinventoryDTO.IsBlockScreen

                        };



                        userDataTable = await _InternalTransfer.GetTransferOrderNos(oTransfer);
                    }

                    foreach (DataRow row in userDataTable.Tables[0].Rows)
                    {
                        InventoryDTO responseDTO = new InventoryDTO();
                        responseDTO.TransferRefNo = row["TransferRequestNumber"].ToString();
                        responseDTO.TransferRefId = Convert.ToInt32(row["TransferRequestID"]);
                        _lstinventory.Add(responseDTO);
                    }
                
                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Inventory, _lstinventory), jsonSettings.JsonSerializerSettings));
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
                ExceptionHandling.LogException(excp, _ClassCode + "003");
                return null;
            }
        }



        [HttpPost]
        [Route("BatchTransfertoPick")]
        public async Task<string> BatchTransfertoPick(WMSCoreMessage oRequest)
        {
            try
            {
                DataSet userDataTable = null;
                List<InventoryDTO> _lstinventory = new List<InventoryDTO>();


                if (oRequest != null)
                {
                    InventoryDTO _oinventoryDTO = (InventoryDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);

                    if (_oinventoryDTO != null)
                    {
                        BO.TransferBO oTransfer = new BO.TransferBO()
                        {
                            AccountId = ConversionUtility.ConvertToInt(_oinventoryDTO.AccountID),
                            WarehouseID= (_oinventoryDTO.WareHouseID),
                            TenantID=(_oinventoryDTO.TenantID),
                            UserId=_oinventoryDTO.UserID,
                            TransferOrderId=_oinventoryDTO.TransferRefId,
                            TransferOrderNo=_oinventoryDTO.TransferRefNo
                        };

                        userDataTable = await _InternalTransfer.BatchTransfertoPick(oTransfer);
                        _oinventoryDTO.Result = userDataTable.Tables[0].Rows[0]["N"].ToString();
                        _lstinventory.Add(_oinventoryDTO);
  
                    }

                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Inventory, _lstinventory), jsonSettings.JsonSerializerSettings));
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
        [Route("UpdateBatchGradeTransfer")]
        public async Task<string> UpdateBatchGradeTransfer(WMSCoreMessage oRequest)
        {
            try
            {
                if (oRequest != null)
                {
                    InventoryDTO _oinventoryDTO = (InventoryDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);
                    Inventory _oInventory = new Inventory();
                    List<InventoryDTO> _lstInventoryDTO = new List<InventoryDTO>();
                    if (_oinventoryDTO != null)
                    {
                        _oInventory.ContainerCode = _oinventoryDTO.ContainerCode;
                        _oInventory.LocationCode = _oinventoryDTO.LocationCode;
                        _oInventory.MaterialCode = _oinventoryDTO.MaterialCode;
                        _oInventory.WarehouseID = Convert.ToInt32(_oinventoryDTO.WarehouseId);
                        _oInventory.TenantID = Convert.ToInt32(_oinventoryDTO.TenantID);
                        _oInventory.BatchNumber = _oinventoryDTO.BatchNo;
                        _oInventory.Quantity = ConversionUtility.ConvertToDecimal(_oinventoryDTO.Quantity);
                        _oInventory.StorageLocation = _oinventoryDTO.StorageLocation;
                        _oInventory.ToStorageLocation = _oinventoryDTO.ToStorageLocation;
                        _oInventory.UserId = _oinventoryDTO.UserId;
                        _oInventory.Grade = _oinventoryDTO.Grade;
                        _oInventory.ToGrade = _oinventoryDTO.ToGrade;
                        _oInventory.ToMaterialCode = _oinventoryDTO.ToMaterialCode;
                        _oInventory.ToBatchNumber = _oinventoryDTO.ToBatchNo;
                        _oInventory.TransferRequestedID=_oinventoryDTO.TransferRefId;

                        List<Inventory> _listInvetory = new List<Inventory>();

                        _listInvetory = await _InternalTransfer.UpsertTranferDetails_BG(_oInventory);

                        foreach (Inventory inventory in _listInvetory)
                        {
                            InventoryDTO inventoryDTO = new InventoryDTO();

                            inventoryDTO.Result = inventory.Result.ToString();
                            inventoryDTO.ResponseMessage = inventory.ResponseMessage.ToString();

                            _lstInventoryDTO.Add(inventoryDTO);
                        }
                    }

                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Inventory, _lstInventoryDTO), jsonSettings.JsonSerializerSettings));
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
                ExceptionHandling.LogException(excp, _ClassCode + "012");
                return null;
            }
        }



        [HttpPost]
        [Route("UpsertPalletConsolidationTransfer")]
        public async Task<string> UpsertPalletConsolidationTransfer(WMSCoreMessage oRequest)
        {
            try
            {
                if (oRequest != null)
                {
                    InventoryDTO _oinventoryDTO = (InventoryDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);
                    Inventory _oInventory = new Inventory();
                    List<InventoryDTO> _lstInventoryDTO = new List<InventoryDTO>();
                    if (_oinventoryDTO != null)
                    {
                        _oInventory.ContainerCode = _oinventoryDTO.ContainerCode;
                        _oInventory.LocationCode = _oinventoryDTO.LocationCode;
                        _oInventory.MaterialCode = _oinventoryDTO.MaterialCode;
                        _oInventory.WarehouseID = Convert.ToInt32(_oinventoryDTO.WarehouseId);
                        _oInventory.TenantID = Convert.ToInt32(_oinventoryDTO.TenantID);
                        _oInventory.BatchNumber = _oinventoryDTO.BatchNo;
                        _oInventory.Quantity = ConversionUtility.ConvertToDecimal(_oinventoryDTO.Quantity);
                        _oInventory.StorageLocation = _oinventoryDTO.StorageLocation;
                        _oInventory.ToStorageLocation = _oinventoryDTO.ToStorageLocation;
                        _oInventory.UserId = _oinventoryDTO.UserId;
                        _oInventory.Grade = _oinventoryDTO.Grade;
                        _oInventory.ToGrade = _oinventoryDTO.ToGrade;
                        _oInventory.ToMaterialCode = _oinventoryDTO.ToMaterialCode;
                        _oInventory.ToBatchNumber = _oinventoryDTO.ToBatchNo;
                        _oInventory.TransferRequestedID = _oinventoryDTO.TransferRefId;
                        _oInventory.ToContainerCode = _oinventoryDTO.ToContainerCode;
                        _oInventory.ToLocationCode = _oinventoryDTO.ToLocationCode;
                        _oInventory.StorageLocation = _oinventoryDTO.SLOC;
                        _oInventory.VLPDAssignedID = _oinventoryDTO.VLPDAssignedID;

                        List<Inventory> _listInvetory = new List<Inventory>();

                        _listInvetory = await _InternalTransfer.UpsertPalletConsolidationTransfer(_oInventory);

                        foreach (Inventory inventory in _listInvetory)
                        {
                            InventoryDTO inventoryDTO = new InventoryDTO();

                            inventoryDTO.Result = inventory.Result.ToString();
                            inventoryDTO.ResponseMessage = inventory.ResponseMessage.ToString();

                            _lstInventoryDTO.Add(inventoryDTO);
                        }
                    }

                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Inventory, _lstInventoryDTO), jsonSettings.JsonSerializerSettings));
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
                ExceptionHandling.LogException(excp, _ClassCode + "012");
                return null;
            }
        }





        [HttpPost]
        [Route("ItemMasterPrint")]
        public async Task<string> ItemMasterPrint(WMSCoreMessage oRequest)
        {
            try
            {
                List<PrintModel> _lstPrinterModel = new List<PrintModel>();
                if (oRequest != null)
                {
                    dynamic obj = oRequest.EntityObject;
                    PrintModel _oinventoryDTO = JsonConvert.DeserializeObject<PrintModel>(obj.ToString());

                    if (_oinventoryDTO != null)
                    {
                        DataSet ds = await _InternalTransfer.ItemMasterPrint(_oinventoryDTO.TenantID, _oinventoryDTO.Labeltype);

                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow row in ds.Tables[0].Rows)
                            {
                                PrintModel inventoryDTO = new PrintModel();
                                inventoryDTO.ZPLScript = row["ZPLScript"].ToString();

                                _lstPrinterModel.Add(inventoryDTO);
                            }
                        }
                    }

                    string json = JsonConvert.SerializeObject(JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Inventory, _lstPrinterModel), jsonSettings.JsonSerializerSettings));
                    return json;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception excp)
            {
                ExceptionHandling.LogException(excp, _ClassCode + "006");
                return null;
            }
        }

    }
}
