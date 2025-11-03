using Simpolo_Endpoint.DAO.interfaces;
using Simpolo_Endpoint.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simpolo_Endpoint.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class MasterDataController : ControllerBase
    {
        private readonly IMasterData _MasterData;
        public MasterDataController(IMasterData masterData)
        {
            _MasterData = masterData;

        }


        [Route("DeleteCustomerInfo"), HttpPost]
        public async Task<IActionResult> DeleteCustomerInfo(SaveCustomerDetailsInputModel obj)
        {
            Payload<string> response = await _MasterData.DeleteCustomerInfo(obj);
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


        [Route("DeleteSupplier"), HttpPost]
        public async Task<IActionResult> DeleteSupplier(SaveSupplierDetailsInputModel obj)
        {
            Payload<string> response = await _MasterData.DeleteSupplier(obj);
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




        [Route("GetSupplierList"), HttpPost]
        public async Task<IActionResult> GetSupplierList(GetSupplierlistModel supplierlist)
        {
            Payload<string> response = await _MasterData.GetSupplierList(supplierlist);
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

        [Route("GetSupplierDetails"), HttpPost]
        public async Task<IActionResult> GetSupplierDetails(GetSupplierDetailsModel supplierDetails)
        {
            Payload<string> response = await _MasterData.GetSupplierDetails(supplierDetails);
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

        [Route("UpsertSupplierDetails"), HttpPost]
        public async Task<IActionResult> UpsertSupplierDetails(UpsertSupplierDetailsModel updateSupplier)
        {
            Payload<string> response = await _MasterData.UpsertSupplierDetails(updateSupplier);
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

        [Route("ImportCustomerData"), HttpPost]
        public async Task<IActionResult> ImportCustomerData(SaveCustomerDetailsInputModel obj)
        {
            Payload<string> response = await _MasterData.ImportCustomerData(obj);

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

        [Route("GetCustomerInfo"), HttpPost]
        public async Task<IActionResult> GetCustomerInfo(GetCustomerInfoModel items)
        {
            Payload<string> response = await _MasterData.GetCustomerInfo(items);
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

        [Route("UpsertCustomer"), HttpPost]
        public async Task<IActionResult> UpsertCustomer(UpsertCustomerModel items)
        {
            Payload<string> response = await _MasterData.UpsertCustomer(items);
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

        [Route("GetEmployeeList"), HttpPost]
        public async Task<IActionResult> GetEmployeeList(GetEmployeeListmodel items)
        {
            Payload<string> response = await _MasterData.GetEmployeeList(items);
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

        [Route("UpsertEmployee"), HttpPost]
        public async Task<IActionResult> UpsertEmployee(UpsertEmployeeModel updateEmpInput)
        {
            Payload<string> response = await _MasterData.UpsertEmployee(updateEmpInput);
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
        //17-03-23
        [Route("GetLocationManager"), HttpPost]
        public async Task<IActionResult> GetLocationManager(GetLocationManagerModel items)
        {
            Payload<string> response = await _MasterData.GetLocationManager(items);
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
        [Route("UpsertLocation"), HttpPost]
        public async Task<IActionResult> UpsertLocation([FromBody] UpsertLocationModel items)
        {
            Payload<string> response = await _MasterData.UpsertLocation(items);
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

        [Route("DeleteLocation"), HttpPost]
        public async Task<IActionResult> DeleteLocation(DeleteLocationModel items)
        {
            Payload<string> response = await _MasterData.DeleteLocation(items);
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
        [Route("GetLoadBinDetails"), HttpPost]
        public async Task<IActionResult> GetLoadBinDetails(GetLoadBinDetailsModel GetLoadBinDetails)
        {
            Payload<string> response = await _MasterData.GetLoadBinDetails(GetLoadBinDetails);
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
        [Route("UpDateBulkModify"), HttpPost]
        public async Task<IActionResult> UpDateBulkModify(UpDateBulkModifyModel UpDateBulkModify)
        {
            Payload<string> response = await _MasterData.UpDateBulkModify(UpDateBulkModify);
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
        [Route("Modify_Locations"), HttpPost]
        public async Task<IActionResult> Modify_Locations(Modify_LocationsModel items)
        {
            Payload<string> response = await _MasterData.Modify_Locations(items);
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


        [Route("Modify_LocationPopup"), HttpPost]
        public async Task<IActionResult> Modify_LocationPopup(Modify_LocationPopupModel items)
        {
            Payload<string> response = await _MasterData.Modify_LocationPopup(items);
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

        [Route("ItemMaster_Print"), HttpPost]
        public async Task<IActionResult> ItemMaster_Print(ItemMaster_PrintModel obj)
        {
            Payload<string> response = await _MasterData.ItemMaster_Print(obj);
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

        [Route("SaveCustomerAddressInputModel"), HttpPost]
        public async Task<IActionResult> SaveCustomerAddressInputModel(SaveCustomerAddressInputModel obj)
        {
            Payload<string> response = await _MasterData.SaveCustomerAddressInfo(obj);
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



        [Route("LocationManager_LabelPrint"), HttpPost]
        public async Task<IActionResult> LocationManager_LabelPrint(LocationManager_LabelPrintModel obj)
        {
            Payload<string> response = await _MasterData.LocationManager_LabelPrint(obj);
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

        [Route("LocationManager_Bulk_LabelPrint"), HttpPost]
        public async Task<IActionResult> LocationManager_Bulk_LabelPrint(LocationManager_Bulk_LabelPrintModel obj)
        {
            Payload<string> response = await _MasterData.LocationManager_Bulk_LabelPrint(obj);
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

        [Route("ImportSupplierData"), HttpPost]
        public async Task<IActionResult> ImportSupplierData(SaveSupplierDetailsInputModel obj)
        {
            Payload<string> response = await _MasterData.ImportSupplierData(obj);

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

        [Route("GetTenantList"), HttpPost]
        public async Task<IActionResult> GetTenantList(TenantListInputModel obj)
        {
            Payload<string> response = await _MasterData.GetTenantList(obj);

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


        [Route("GetBarcodeLabelData"), HttpPost]
        public async Task<IActionResult> GetBarcodeLabelData(BarcodeInputModel obj)
        {
            Payload<string> response = await _MasterData.GetBarcodeLabelData(obj);

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

        [Route("GetTenantContractData"), HttpPost]
        public async Task<IActionResult> GetTenantContractData(TenantListInputModel obj)
        {
            Payload<string> response = await _MasterData.GetTenantContractData(obj);

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

        [Route("SaveUpdateTenantContractData"), HttpPost]
        public async Task<IActionResult> SaveUpdateTenantContractData(TenantContractInputModel obj)
        {
            Payload<string> response = await _MasterData.SaveTenantContractData(obj);

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
        [Route("DeleteTenantContractData"), HttpPost]
        public async Task<IActionResult> DeleteTenantContractData(TenantContractInputModel obj)
        {
            Payload<string> response = await _MasterData.DeleteTenantContractData(obj);

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

        [Route("SaveUpdateTenantData"), HttpPost]
        public async Task<IActionResult> SaveUpdateTenantData(SaveTenantInputModel obj)
        {
            Payload<string> response = await _MasterData.SaveUpdateTenantData(obj);

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
    }
}