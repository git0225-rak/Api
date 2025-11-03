using Simpolo_Endpoint.DAO.interfaces;
using Simpolo_Endpoint.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;
using Microsoft.OpenApi.Writers;

namespace Simpolo_Endpoint.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class CommonController : ControllerBase
    {

        private readonly ICommon _iCommon;

        public CommonController(ICommon common)
        {
            _iCommon = common;
        }
        //1

        [Route("GetUserDropdown"), HttpPost]
        public async Task<IActionResult> GetUserDropdown(GetUserDropdownModel getUserDropdownModel)
        {
            Payload<string> response = await _iCommon.GetUserDropdown(getUserDropdownModel);
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
        //2
        [Route("GetUserTypes"), HttpPost]

        public async Task<IActionResult> GetUserTypes()
        {
            Payload<string> response = await _iCommon.GetUserTypes();
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
        //03
        [Route("GetTenantDropdown_Account"), HttpPost]
        public async Task<IActionResult> GetTenantDropdown_Account(GetTenantDropdown_AccountModel getTenantDropdown_AccountModel)
        {
            Payload<string> response = await _iCommon.GetTenantDropdown_Account(getTenantDropdown_AccountModel);
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


        [Route("GetAddressTypes"), HttpPost]
        public async Task<IActionResult> GetAddressTypes(SaveCustomerDetailsInputModel obj)
        {
            Payload<string> response = await _iCommon.GetAddressTypes(obj);
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




        [Route("GetTenantDropdown_Account_Tenant"), HttpPost]
        public async Task<IActionResult> GetTenantDropdown_Account_Tenant(GetTenantDropdown_Account_TenantModel getTenantDropdown_Account_TenantModel)
        {
            Payload<string> response = await _iCommon.GetTenantDropdown_Account_Tenant(getTenantDropdown_Account_TenantModel);
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

        [Route("GetTenantDropdown_Account_User_Tenant"), HttpPost]
        public async Task<IActionResult> GetTenantDropdown_Account_User_Tenant(GetTenantDropdown_Account_User_TenantModel getTenantDropdown_Account_User_TenantModel)
        {
            Payload<string> response = await _iCommon.GetTenantDropdown_Account_User_Tenant(getTenantDropdown_Account_User_TenantModel);
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
        //4
        [Route("GetUserRoles"), HttpPost]
        public async Task<IActionResult> GetUserRoles(GetUserRolesModel getUserRolesModel)
        {
            Payload<string> response = await _iCommon.GetUserRoles(getUserRolesModel);
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
        //5
        [Route("GetWHDropdown_Account"), HttpPost]
        public async Task<IActionResult> GetWHDropdown_Account(GetWHDropdown_AccountModel getWHDropdown_AccountModel)
        {
            Payload<string> response = await _iCommon.GetWHDropdown_Account(getWHDropdown_AccountModel);
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
        //6
        [Route("GetWHDropdown_User"), HttpPost]
        public async Task<IActionResult> GetWHDropdown_User(GetWHDropdown_UserModel getWHDropdown_UserModel)
        {
            Payload<string> response = await _iCommon.GetWHDropdown_User(getWHDropdown_UserModel);
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
        //7

        //8

        [Route("GetLocDropdown"), HttpPost]
        public async Task<IActionResult> GetLocDropdown(GetLocDropdown_Model getLocDropdown_Model)
        {
            Payload<string> response = await _iCommon.GetLocDropdown(getLocDropdown_Model);
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


        [Route("GetContainerType"), HttpPost]
        public async Task<IActionResult> GetContainerType(GetContainerTypeModel getContainerTypeModel)
        {
            Payload<string> response = await _iCommon.GetContainerType(getContainerTypeModel);
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




        [Route("GetSeriesType"), HttpPost]
        public async Task<IActionResult> GetSeriesType(GetSeriesTypeModel getSeriesTypeModel)
        {
            Payload<string> response = await _iCommon.GetSeriesType(getSeriesTypeModel);
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





        //9
        [Route("GetDockTypes"), HttpPost]
        public async Task<IActionResult> GetDockTypes()
        {
            Payload<string> response = await _iCommon.GetDockTypes();
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
        //10
        [Route("GetCountries"), HttpPost]
        public async Task<IActionResult> GetCountries()
        {
            Payload<string> response = await _iCommon.GetCountries();
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
        //11
        [Route("GetCurrency"), HttpPost]
        public async Task<IActionResult> GetCurrency(GetCurrencyModel getCurrencyModel)
        {
            Payload<string> response = await _iCommon.GetCurrency(getCurrencyModel);
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
        //12
        [Route("GetStates"), HttpPost]
        public async Task<IActionResult> GetStates(GetStatesModel getStatesModel)
        {
            Payload<string> response = await _iCommon.GetStates(getStatesModel);
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
            //13
        }
        [Route("GetCities"), HttpPost]
        public async Task<IActionResult> GetCities(GetCitiesModel getCitiesModel)
        {
            Payload<string> response = await _iCommon.GetCities(getCitiesModel);
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
        //14
        [Route("GetZipCodes"), HttpPost]
        public async Task<IActionResult> GetZipCodes(GetZipCodesModel getZipCodesModel)
        {
            Payload<string> response = await _iCommon.GetZipCodes(getZipCodesModel);
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
        //15
        [Route("GetTimePreferences"), HttpPost]

        public async Task<IActionResult> GetTimePreferences()
        {
            Payload<string> response = await _iCommon.GetTimePreferences();

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
        //16
        [Route("GetDockList"), HttpPost]
        public async Task<IActionResult> GetDockList(GetDockListModel getDockListModel)
        {
            Payload<string> response = await _iCommon.GetDockList(getDockListModel);
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
        //17
        [Route("GetZoneList"), HttpPost]
        public async Task<IActionResult> GetZoneList(GetZoneListModel getZoneListModel)
        {
            Payload<string> response = await _iCommon.GetZoneList(getZoneListModel);
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
        //18
        [Route("GetRackTypes"), HttpPost]

        public async Task<IActionResult> GetRackTypes()
        {
            Payload<string> response = await _iCommon.GetRackTypes();
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
        //19
        [Route("GetWarehouseTypes"), HttpPost]
        public async Task<IActionResult> GetWarehouseTypes(GetWarehouseTypesModel getWarehouseTypesModel)
        {
            Payload<string> response = await _iCommon.GetWarehouseTypes(getWarehouseTypesModel);
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
        [Route("GetSupplierDropdown_Tenant"), HttpPost]
        public async Task<IActionResult> GetSupplierDropdown_Tenant(GetSupplierDropdown_TenantModel getSupplierDropdown_TenantModel)
        {
            Payload<string> response = await _iCommon.GetSupplierDropdown_Tenant(getSupplierDropdown_TenantModel);
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
        [Route("GetMCodeDropdown"), HttpPost]
        public async Task<IActionResult> GetMCodeDropdown(GetMCodeDropdownModel getMCodeDropdownModel)
        {
            Payload<string> response = await _iCommon.GetMCodeDropdown(getMCodeDropdownModel);
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
        [Route("GetMTypeData"), HttpPost]
        public async Task<IActionResult> GetMTypeData(GetMTypeDataModel getMTypeDataModel)
        {
            Payload<string> response = await _iCommon.GetMTypeData(getMTypeDataModel);
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

        [Route("GetMGroupData"), HttpPost]
        public async Task<IActionResult> GetMGroupData(GetMGroupDataModel getMGroupDataModel)
        {
            Payload<string> response = await _iCommon.GetMGroupData(getMGroupDataModel);
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

        [Route("LoadEmployeeList"), HttpPost]
        public async Task<IActionResult> LoadEmployeeList(LoadEmployeeListModel loadEmployeeListModel)
        {
            Payload<string> response = await _iCommon.LoadEmployeeList(loadEmployeeListModel);
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
        [Route("GetRackList"), HttpPost]

        public async Task<IActionResult> GetRackList(GetlistModel obj)
        {
            Payload<string> response = await _iCommon.GetRackList(obj);
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
        [Route("GetBayList"), HttpPost]

        public async Task<IActionResult> GetBayList(GetlistModel obj)
        {
            Payload<string> response = await _iCommon.GetBayList(obj);
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
        [Route("GetColumnList"), HttpPost]

        public async Task<IActionResult> GetColumnList(GetlistModel obj)
        {
            Payload<string> response = await _iCommon.GetColumnList(obj);
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
        [Route("GetBinList"), HttpPost]

        public async Task<IActionResult> GetBinList()
        {
            Payload<string> response = await _iCommon.GetBinList();
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
        [Route("GetWarehouse_LM"), HttpPost]
        public async Task<IActionResult> GetWarehouse(GetWarehouseDataModel getWarehouseDataModel)
        {
            Payload<string> response = await _iCommon.GetWarehouse(getWarehouseDataModel);
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
        [Route("GetLocationZonesByWHID"), HttpPost]
        public async Task<IActionResult> GetLocationZonesByWHID(GetLocationZonesByWHIDModel getLocationZonesByWHIDModel)
        {
            Payload<string> response = await _iCommon.GetLocationZonesByWHID(getLocationZonesByWHIDModel);
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
        [Route("LoadLocationTypes"), HttpPost]

        public async Task<IActionResult> LoadLocationTypes()
        {
            Payload<string> response = await _iCommon.LoadLocationTypes();
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
        [Route("GetInwardStatus"), HttpPost]

        public async Task<IActionResult> GetInwardStatus()
        {
            Payload<string> response = await _iCommon.GetInwardStatus();
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
        [Route("LoadPOTypes"), HttpPost]
        public async Task<IActionResult> LoadPOTypes(LoadPOTypesModel loadPOTypesModel)
        {
            Payload<string> response = await _iCommon.LoadPOTypes(loadPOTypesModel);
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

        [Route("LoadINBTypes"), HttpPost]
        public async Task<IActionResult> LoadINBTypes(LoadINBTypesModel loadPOTypesModel)
        {
            Payload<string> response = await _iCommon.LoadINBTypes(loadPOTypesModel);
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








        [Route("GetSuppliers_POType"), HttpPost]
        public async Task<IActionResult> GetSuppliers_POType(GetSuppliers_POTypeModel getSuppliers_POTypeModel)
        {
            Payload<string> response = await _iCommon.GetSuppliers_POType(getSuppliers_POTypeModel);
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
        [Route("GetPONumbers"), HttpPost]
        public async Task<IActionResult> GetPONumbers(GetPONumbersModel getPONumbersModel)
        {
            Payload<string> response = await _iCommon.GetPONumbers(getPONumbersModel);
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
        [Route("SearchPartNumber"), HttpPost]
        public async Task<IActionResult> SearchPartNumber(SearchPartNumberModel searchPartNumberModel)
        {
            Payload<string> response = await _iCommon.SearchPartNumber(searchPartNumberModel);
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
        [Route("GetSKUPO"), HttpPost]
        public async Task<IActionResult> GetSKUPO(GetSKUPOModel getSKUPOModel)
        {
            Payload<string> response = await _iCommon.GetSKUPO(getSKUPOModel);
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
        [Route("GetUoMQty"), HttpPost]
        public async Task<IActionResult> GetUoMQty(UoMQtyPOModel uoMQtyPOModel)
        {
            Payload<string> response = await _iCommon.GetUoMQty(uoMQtyPOModel);
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
        [Route("InvNumbersSI"), HttpPost]
        public async Task<IActionResult> InvNumbersSI(InvNumbersSIModel invNumbersSIModel)
        {
            Payload<string> response = await _iCommon.InvNumbersSI(invNumbersSIModel);
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
        [Route("GetSOStatus"), HttpPost]

        public async Task<IActionResult> GetSOStatus()
        {
            Payload<string> response = await _iCommon.GetSOStatus();
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
        [Route("GetSONumbersList"), HttpPost]
        public async Task<IActionResult> GetSONumbersList(GetSONumbersListModel getSONumbersListModel)
        {
            Payload<string> response = await _iCommon.GetSONumbersList(getSONumbersListModel);
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
        [Route("GetSoType"), HttpPost]
        public async Task<IActionResult> GetSoType(GetSoTypeModel getSoTypeModel)
        {
            Payload<string> response = await _iCommon.GetSoType(getSoTypeModel);
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
        [Route("GetCustomersTenant"), HttpPost]
        public async Task<IActionResult> GetCustomersTenant(GetCustomersTenantModel getCustomersTenantModel)
        {
            Payload<string> response = await _iCommon.GetCustomersTenant(getCustomersTenantModel);
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
        [Route("GetMCodeForSaleOrder"), HttpPost]
        public async Task<IActionResult> GetMCodeForSaleOrder(GetMCodeForSaleOrderModel getMCodeForSaleOrderModel)
        {
            Payload<string> response = await _iCommon.GetMCodeForSaleOrder(getMCodeForSaleOrderModel);
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
        [Route("GetCustomerPOdata"), HttpPost]
        public async Task<IActionResult> GetCustomerPOdata(GetCustomerPOdataModel getCustomerPOdataModel)
        {
            Payload<string> response = await _iCommon.GetCustomerPOdata(getCustomerPOdataModel);
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
        [Route("GetStorageLocations"), HttpPost]

        public async Task<IActionResult> GetStorageLocations()
        {
            Payload<string> response = await _iCommon.GetStorageLocations();
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
        [Route("GetMCodeForSOWithOEM"), HttpPost]
        public async Task<IActionResult> GetMCodeForSOWithOEM(GetMCodeForSOWithOEMModel getMCodeForSOWithOEMModel)
        {
            Payload<string> response = await _iCommon.GetMCodeForSOWithOEM(getMCodeForSOWithOEMModel);
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
        [Route("GetOrderforIssue"), HttpPost]
        public async Task<IActionResult> GetOrderforIssue(GetOrderforIssueModel getOrderforIssueModel)
        {
            Payload<string> response = await _iCommon.GetOrderforIssue(getOrderforIssueModel);
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
        [Route("GetQONumber"), HttpPost]
        public async Task<IActionResult> GetQONumber(GetQONumberModel getQONumberModel)
        {
            Payload<string> response = await _iCommon.GetQONumber(getQONumberModel);
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
        [Route("GetStatusForEmp"), HttpPost]
        public async Task<IActionResult> GetStatusForEmp(GetStatusForEmpModel getStatusForEmpModel)
        {
            Payload<string> response = await _iCommon.GetStatusForEmp(getStatusForEmpModel);
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
        [Route("GetTenantbyUser_Emp"), HttpPost]
        public async Task<IActionResult> GetTenantbyUser_Emp(GetTenantbyUser_EmpModel getTenantbyUser_EmpModel)
        {
            Payload<string> response = await _iCommon.GetTenantbyUser_Emp(getTenantbyUser_EmpModel);
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


        [Route("GetTopallets"), HttpPost]
        public async Task<IActionResult> GetTopallets(ItemsModel obj)
        {
            Payload<string> response = await _iCommon.GetTopallets(obj);
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




        [Route("GetGrades"), HttpPost]
        public async Task<IActionResult> GetGrades(ItemsModel obj)
        {
            Payload<string> response = await _iCommon.GetGrades(obj);
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





        [Route("GetLabSampleLocation"), HttpPost]
        public async Task<IActionResult> GetLabSampleLocation(GetLabSampleLocationModel getLabSampleLocationModel)
        {
            Payload<string> response = await _iCommon.GetLabSampleLocation(getLabSampleLocationModel);
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
        [Route("GetEmployeeRequestDetails"), HttpPost]
        public async Task<IActionResult> GetEmployeeRequestDetails(GetEmployeeRequestDetailsModel getEmployeeRequestDetailsModel)
        {
            Payload<string> response = await _iCommon.GetEmployeeRequestDetails(getEmployeeRequestDetailsModel);
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
        [Route("GetPONumbersLabSampleRequest"), HttpPost]
        public async Task<IActionResult> GetPONumbersLabSampleRequest(GetPONumbersLabSampleRequestModel getPONumbersLabSampleRequestModel)
        {
            Payload<string> response = await _iCommon.GetPONumbersLabSampleRequest(getPONumbersLabSampleRequestModel);
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
        [Route("GetMCodesBasedOnPO"), HttpPost]
        public async Task<IActionResult> GetMCodesBasedOnPO(GetMCodesBasedOnPOModel getMCodesBasedOnPOModel)
        {
            Payload<string> response = await _iCommon.GetMCodesBasedOnPO(getMCodesBasedOnPOModel);
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

        [Route("GetStorageLocationForStockPosting"), HttpPost]
        public async Task<IActionResult> GetStorageLocationForStockPosting(GetStorageLocationForStockPostingModel obj)
        {
            Payload<string> response = await _iCommon.GetStorageLocationForStockPosting(obj);
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

        [Route("Get_inv_cs_Locations_tolocation"), HttpPost]
        public async Task<IActionResult> Get_inv_cs_Locations_tolocation(HouseKeepingInputModel obj)
        {
            Payload<string> response = await _iCommon.Get_inv_cs_Locations_tolocation(obj);
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

        [Route("GetDeliveryPointData"), HttpPost]
        public async Task<IActionResult> GetDeliveryPointData(GetDeliveryPointDataModel getDeliveryPointData)
        {
            Payload<string> response = await _iCommon.GetDeliveryPointData(getDeliveryPointData);
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
                {
                    return BadRequest("Failed to retrieve data");
                }

            }
        }
        [Route("GetFastMoveData"), HttpPost]

        public async Task<IActionResult> GetFastMoveData()
        {
            Payload<string> response = await _iCommon.GetFastMoveData();
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

        [Route("GetIndustryMaterialAttributes"), HttpPost]
        public async Task<IActionResult> GetIndustryMaterialAttributes(IndustryMaterialAttributesInputModel items)
        {
            Payload<string> response = await _iCommon.GetIndustryMaterialAttributes(items);
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



        [Route("GetIndustries"), HttpPost]
        public async Task<IActionResult> GetIndustry(MaterialDataInputModel items)
        {
            Payload<string> response = await _iCommon.GetIndustry(items);
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



        [Route("GetLoadSAPReference"), HttpPost]
        public async Task<IActionResult> GetLoadSAPReference(GetLoadSAPReferenceModel getLoadSAPReferenceModel)
        {
            Payload<string> response = await _iCommon.GetLoadSAPReference(getLoadSAPReferenceModel);
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
        [Route("GetFastMovingWH"), HttpPost]
        public async Task<IActionResult> GetFastMovingWH(GetFastMovingWHModel getFastMovingWHModel)
        {
            Payload<string> response = await _iCommon.GetFastMovingWH(getFastMovingWHModel);
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
        [Route("GetTenantsDropdown_Warehouse"), HttpPost]
        public async Task<IActionResult> GetTenantsDropdown_Warehouse(GetTenantsDropdown_WarehouseModel items)
        {
            Payload<string> response = await _iCommon.GetTenantsDropdown_Warehouse(items);
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
        [Route("GetPOHeaderListTenant"), HttpPost]
        public async Task<IActionResult> GetPOHeaderListTenant(GetPOHeaderListTenantModel getPOHeaderListTenantModel)
        {
            Payload<string> response = await _iCommon.GetPOHeaderListTenant(getPOHeaderListTenantModel);
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
        [Route("GetInvoiceListForPONumber"), HttpPost]
        public async Task<IActionResult> GetInvoiceListForPONumber(GetInvoiceListForPONumberModel getInvoiceListForPONumberModel)
        {
            Payload<string> response = await _iCommon.GetInvoiceListForPONumber(getInvoiceListForPONumberModel);
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
        [Route("GetDocks"), HttpPost]
        public async Task<IActionResult> GetDocks(GetDocksModel getDocksModel)
        {
            Payload<string> response = await _iCommon.GetDocks(getDocksModel);
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



        [Route("GetLoadingPoints"), HttpPost]
        public async Task<IActionResult> GetLoadingPoints(GetLoadingPoints GetLoadingPoint)
        {
            Payload<string> response = await _iCommon.GetLoadingPoints(GetLoadingPoint);
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



        [Route("GetTokenNumberOBD"), HttpPost]
        public async Task<IActionResult> GetTokenNumberOBD(GetLoadingPoints GetLoadingPoint)
        {
            Payload<string> response = await _iCommon.GetTokenNumberOBD(GetLoadingPoint);
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



        [Route("GetVehicleTypesWeb"), HttpPost]

        public async Task<IActionResult> GetVehicleTypesWeb(GetVehicleTypes vehicle)
        {
            Payload<string> response = await _iCommon.GetVehicleTypesWeb(vehicle);
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





        [Route("GetVehicleTypes"), HttpPost]

        public async Task<IActionResult> GetVehicleTypes()
        {
            Payload<string> response = await _iCommon.GetVehicleTypes();
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
        [Route("GetEmployeeReturnList"), HttpPost]
        public async Task<IActionResult> GetEmployeeReturnList(GetEmployeeReturnListModel getEmployeeReturnListModel)
        {
            Payload<string> response = await _iCommon.GetEmployeeReturnList(getEmployeeReturnListModel);
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
        [Route("GetDeleteItemsById"), HttpPost]
        public async Task<IActionResult> GetDeleteItemsById(GetDeleteItemsByIdModel getDeleteItemsByIdModel)
        {
            Payload<string> response = await _iCommon.GetDeleteItemsById(getDeleteItemsByIdModel);
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
        ////material transfer

        [Route("GetMaterial_Internal"), HttpPost]
        public async Task<IActionResult> GetMaterial_Internal(GetMaterial_InternalModel items)
        {
            Payload<string> response = await _iCommon.GetMaterial_Internal(items);
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


        [Route("GetLocationsDropDown"), HttpPost]
        public async Task<IActionResult> GetLocationsDropDown(GetLocationsDropDownModel obj)
        {
            Payload<string> response = await _iCommon.GetLocationsDropDown(obj);
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


        [Route("ToLocationsDropDown"), HttpPost]
        public async Task<IActionResult> ToLocationsDropDown(ToLocationsDropDownModel obj)
        {
            Payload<string> response = await _iCommon.ToLocationsDropDown(obj);
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



        [Route("GetBatchListDropDown"), HttpPost]
        public async Task<IActionResult> GetBatchListDropDown(GetBatchListDropDownModel items)
        {
            Payload<string> response = await _iCommon.GetBatchListDropDown(items);
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



        [Route("GetBatchGradeList"), HttpPost]
        public async Task<IActionResult> GetBatchGradeList(GetBatchListDropDownModel items)
        {
            Payload<string> response = await _iCommon.GetBatchGradeList(items);
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



        [Route("GetGradeListByBatch"), HttpPost]
        public async Task<IActionResult> GetGradeListByBatch(GetGrdaeListDropDown items)
        {
            Payload<string> response = await _iCommon.GetGradeListByBatch(items);
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




        [Route("GetSLOCDropDown"), HttpPost]
        public async Task<IActionResult> GetSLOCDropDown(GetSLOCDropDownModel items)
        {
            Payload<string> response = await _iCommon.GetSLOCDropDown(items);
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


        [Route("GetSLOCDropDownBatchGrade"), HttpPost]
        public async Task<IActionResult> GetSLOCDropDownBatchGrade(GetSLOCDropDownModel items)
        {
            Payload<string> response = await _iCommon.GetSLOCDropDownBatchGrade(items);
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

        [Route("GetPalletsDropDown"), HttpPost]
        public async Task<IActionResult> GetPalletsDropDown(GetPalletsDropDownModel items)
        {
            Payload<string> response = await _iCommon.GetPalletsDropDown(items);
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

        [Route("GetProjectBasedSKUDropdown"), HttpPost]
        public async Task<IActionResult> GetProjectBasedSKUDropdown(GetProjectBasedSKUDropdownModel items)
        {
            Payload<string> response = await _iCommon.GetProjectBasedSKUDropdown(items);
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


        [Route("GetProjectRefNoDropDown"), HttpPost]
        public async Task<IActionResult> GetProjectRefNoDropDown(GetProjectRefNoDropDownModel items)
        {
            Payload<string> response = await _iCommon.GetProjectRefNoDropDown(items);
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

        [Route("GetZonesDropDown"), HttpPost]
        public async Task<IActionResult> GetZonesDropDown(GetZonesDropDownModel items)
        {
            Payload<string> response = await _iCommon.GetZonesDropDown(items);
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

        [Route("GetCCListData"), HttpPost]
        public async Task<IActionResult> GetCCListData(GetCCListDataModel items)
        {
            Payload<string> response = await _iCommon.GetCCListData(items);
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

        [Route("GetCC_UserDropDown"), HttpPost]
        public async Task<IActionResult> GetCC_UserDropDown(GetCC_UserDropDownModel items)
        {
            Payload<string> response = await _iCommon.GetCC_UserDropDown(items);
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

        [Route("GetCC_Materials"), HttpPost]
        public async Task<IActionResult> GetCC_Materials(GetCC_MaterialsModel items)
        {
            Payload<string> response = await _iCommon.GetCC_Materials(items);
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
        [Route("GetCC_RackDetails"), HttpPost]
        public async Task<IActionResult> GetCC_RackDetails(GetCC_RackDetailsModel items)
        {
            Payload<string> response = await _iCommon.GetCC_RackDetails(items);
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
        //inbound
        [Route("GetTenantDropdown_Warehouse"), HttpPost]
        public async Task<IActionResult> GetTenantDropdown_Warehouse(GetTenantDropdown_WarehouseModel getTenantDropdown_WarehouseModel)
        {
            Payload<string> response = await _iCommon.GetTenantDropdown_Warehouse(getTenantDropdown_WarehouseModel);
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
        [Route("GetShipmentTypes"), HttpPost]
        public async Task<IActionResult> GetShipmentTypes(GetShipmentTypesModel getShipmentTypesModel)
        {
            Payload<string> response = await _iCommon.GetShipmentTypes(getShipmentTypesModel);
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
        [Route("GetSupplierForInbound"), HttpPost]
        public async Task<IActionResult> GetSupplierForInbound(GetSupplierForInboundModel getSupplierForInboundModel)
        {
            Payload<string> response = await _iCommon.GetSupplierForInbound(getSupplierForInboundModel);
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
        [Route("GetPONumbersForInbound"), HttpPost]
        public async Task<IActionResult> GetPONumbersForInbound(GetPONumbersForInboundModel getPONumbersForInboundModel)
        {
            Payload<string> response = await _iCommon.GetPONumbersForInbound(getPONumbersForInboundModel);
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
        [Route("GetInvoiceNoForInbound"), HttpPost]
        public async Task<IActionResult> GetInvoiceNoForInbound(GetInvoiceNoForInboundModel getInvoiceNoForInboundModel)
        {
            Payload<string> response = await _iCommon.GetInvoiceNoForInbound(getInvoiceNoForInboundModel);
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
        [Route("GetPONumberForGRN"), HttpPost]
        public async Task<IActionResult> GetPONumberForGRN(GetPONumberForGRNModel getPONumberForGRNModel)
        {
            Payload<string> response = await _iCommon.GetPONumberForGRN(getPONumberForGRNModel);
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
        [Route("GetInvoiceNumberForGRN"), HttpPost]
        public async Task<IActionResult> GetInvoiceNumberForGRN(GetInvoiceNumberForGRNModel getInvoiceNumberForGRNModel)
        {
            Payload<string> response = await _iCommon.GetInvoiceNumberForGRN(getInvoiceNumberForGRNModel);
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
        [Route("GetInvoiceNumberForDisc"), HttpPost]
        public async Task<IActionResult> GetInvoiceNumberForDisc(GetInvoiceNumberForDiscModel getInvoiceNumberForDiscModel)
        {
            Payload<string> response = await _iCommon.GetInvoiceNumberForDisc(getInvoiceNumberForDiscModel);
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
        [Route("GetPONumberForDisc"), HttpPost]
        public async Task<IActionResult> GetPONumberForDisc(GetPONumberForDiscModel getPONumberForDiscModel)
        {
            Payload<string> response = await _iCommon.GetPONumberForDisc(getPONumberForDiscModel);
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
        [Route("GetMCodesForDisc"), HttpPost]
        public async Task<IActionResult> GetMCodesForDisc(GetMCodesForDiscModel getMCodesForDiscModel)
        {
            Payload<string> response = await _iCommon.GetMCodesForDisc(getMCodesForDiscModel);
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
        [Route("GetPOLineNumbersForDisc"), HttpPost]
        public async Task<IActionResult> GetPOLineNumbersForDisc(GetPOLineNumbersForDiscModel getPOLineNumbersForDiscModel)
        {
            Payload<string> response = await _iCommon.GetPOLineNumbersForDisc(getPOLineNumbersForDiscModel);
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
        [Route("GetUsersDataForInbound"), HttpPost]
        public async Task<IActionResult> GetUsersDataForInbound(GetUsersDataForInboundModel getUsersDataForInboundModel)
        {
            Payload<string> response = await _iCommon.GetUsersDataForInbound(getUsersDataForInboundModel);
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
        [Route("GetRTRMCodes"), HttpPost]
        public async Task<IActionResult> GetRTRMCodes(GetRTRMCodesModel getRTRMCodesModel)
        {
            Payload<string> response = await _iCommon.GetRTRMCodes(getRTRMCodesModel);
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
        [Route("GetLoactionsForGoodsIn"), HttpPost]
        public async Task<IActionResult> GetLoactionsForGoodsIn(GetLoactionsForGoodsInModel getLoactionsForGoodsInModel)
        {
            Payload<string> response = await _iCommon.GetLoactionsForGoodsIn(getLoactionsForGoodsInModel);
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
        [Route("GetPalletsForGoodsIn"), HttpPost]
        public async Task<IActionResult> GetPalletsForGoodsIn(GetPalletsForGoodsInModel getPalletsForGoodsInModel)
        {
            Payload<string> response = await _iCommon.GetPalletsForGoodsIn(getPalletsForGoodsInModel);
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
        [Route("GetStorageLoactionsForGoodsIn"), HttpPost]

        public async Task<IActionResult> GetStorageLoactionsForGoodsIn()
        {
            Payload<string> response = await _iCommon.GetStorageLoactionsForGoodsIn();
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
        [Route("GetVehicleListForGoodsIn"), HttpPost]
        public async Task<IActionResult> GetVehicleListForGoodsIn(GetVehicleListForGoodsInModel getVehicleListForGoodsInModel)
        {
            Payload<string> response = await _iCommon.GetVehicleListForGoodsIn(getVehicleListForGoodsInModel);
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

        [Route("GetVehicleListForGroupOBD"), HttpPost]
        public async Task<IActionResult> GetVehicleListForGroupOBD(GetVehicleListForGoodsInModel getVehicleListForGoodsInModel)
        {
            Payload<string> response = await _iCommon.GetVehicleListForGroupOBD(getVehicleListForGoodsInModel);

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

        

        [Route("GetSITStoreRefNumbers"), HttpPost]
        public async Task<IActionResult> GetSITStoreRefNumbers(GetStoreRefNumbersModel getSITStoreRefNumbersModel)
        {
            Payload<string> response = await _iCommon.GetSITStoreRefNumbers(getSITStoreRefNumbersModel);
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
        [Route("GetSIEStoreRefNumbers"), HttpPost]
        public async Task<IActionResult> GetSIEStoreRefNumbers(GetStoreRefNumbersModel getSIEStoreRefNumbersModel)
        {
            Payload<string> response = await _iCommon.GetSIEStoreRefNumbers(getSIEStoreRefNumbersModel);
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

        [Route("GetSIPStoreRefNumbers"), HttpPost]
        public async Task<IActionResult> GetSIPStoreRefNumbers(GetStoreRefNumbersModel getSIEStoreRefNumbersModel)
        {
            Payload<string> response = await _iCommon.GetSIPStoreRefNumbers(getSIEStoreRefNumbersModel);
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
        [Route("GetInboundStatus"), HttpPost]
        public async Task<IActionResult> GetInboundStatus(GetInboundStatusModel getInboundStatusModel)
        {
            Payload<string> response = await _iCommon.GetInboundStatus(getInboundStatusModel);
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

        [Route("GetShipmentTypes_InboundSearch"), HttpPost]
        public async Task<IActionResult> GetShipmentTypes_InboundSearch(GetShipmentTypes_InboundSearchModel getShipmentTypes_InboundSearchModel)
        {
            Payload<string> response = await _iCommon.GetShipmentTypes_InboundSearch(getShipmentTypes_InboundSearchModel);
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
        [Route("GetLoadOBDNumbers"), HttpPost]
        public async Task<IActionResult> GetLoadOBDNumbers(GetLoadOBDNumbersModel obj)
        {
            Payload<string> response = await _iCommon.GetLoadOBDNumbers(obj);
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
        [Route("GetRevertStoreRefNo"), HttpPost]
        public async Task<IActionResult> GetRevertStoreRefNo(GetRevertStoreRefNoModel obj)
        {
            Payload<string> response = await _iCommon.GetRevertStoreRefNo(obj);
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
        [Route("GetLoadVLPDDelvDocNo"), HttpPost]
        public async Task<IActionResult> GetLoadVLPDDelvDocNo(GetLoadVLPDDelvDocNoModel obj)
        {
            Payload<string> response = await _iCommon.GetLoadVLPDDelvDocNo(obj);
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
        [Route("GetLoadPNCPendingDelvDocNo_TC"), HttpPost]
        public async Task<IActionResult> GetLoadPNCPendingDelvDocNo_TC(GetLoadPNCPendingDelvDocNo_TCModel obj)
        {
            Payload<string> response = await _iCommon.GetLoadPNCPendingDelvDocNo_TC(obj);
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
        [Route("GetLoadPGIPendingDelvDocNo"), HttpPost]
        public async Task<IActionResult> GetLoadPGIPendingDelvDocNo(GetLoadPGIPendingDelvDocNoModel obj)
        {
            Payload<string> response = await _iCommon.GetLoadPGIPendingDelvDocNo(obj);
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
        [Route("GetLoadPODDelvDocNo"), HttpPost]
        public async Task<IActionResult> GetLoadPODDelvDocNo(GetLoadPODDelvDocNoModel obj)
        {
            Payload<string> response = await _iCommon.GetLoadPODDelvDocNo(obj);
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
        [Route("GetShipmentVerificationRef"), HttpPost]
        public async Task<IActionResult> GetShipmentVerificationRef(GetShipmentVerificationRefModel obj)
        {
            Payload<string> response = await _iCommon.GetShipmentVerificationRef(obj);
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
        [Route("GetSkipReason"), HttpPost]
        public async Task<IActionResult> GetSkipReason(GetSkipReasonModel obj)
        {
            Payload<string> response = await _iCommon.GetSkipReason(obj);
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
        [Route("GetDeliveryDetails"), HttpPost]
        public async Task<IActionResult> GetDeliveryDetails(GetDeliveryDetailsModel obj)
        {
            Payload<string> response = await _iCommon.GetDeliveryDetails(obj);
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
        [Route("GetDocumentType"), HttpPost]

        public async Task<IActionResult> GetDocumentType()
        {
            Payload<string> response = await _iCommon.GetDocumentType();
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

        [Route("GetLoadMaterialsForCurrentStock"), HttpPost]
        public async Task<IActionResult> GetLoadMaterialsForCurrentStock(GetLoadMaterialsForCurrentStockModel obj)
        {
            Payload<string> response = await _iCommon.GetLoadMaterialsForCurrentStock(obj);
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
        [Route("GetLoadMaterialTypesForCurrentStock"), HttpPost]
        public async Task<IActionResult> GetLoadMaterialTypesForCurrentStock(GetLoadMaterialTypesForCurrentStockModel obj)
        {
            Payload<string> response = await _iCommon.GetLoadMaterialTypesForCurrentStock(obj);
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
        [Route("GetLoadMaterialDrawTypesForCurrentStock"), HttpPost]
        public async Task<IActionResult> GetLoadMaterialDrawTypesForCurrentStock(GetLoadMaterialDrawTypesForCurrentStockModel obj)
        {
            Payload<string> response = await _iCommon.GetLoadMaterialDrawTypesForCurrentStock(obj);
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
        [Route("GetSlocforCurrentstock"), HttpPost]
        public async Task<IActionResult> GetSlocforCurrentstock(GetSlocforCurrentstockModel obj)
        {
            Payload<string> response = await _iCommon.GetSlocforCurrentstock(obj);
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
        [Route("Getlabel"), HttpPost]
        public async Task<IActionResult> Getlabel(GetLabelModel obj)
        {
            Payload<string> response = await _iCommon.Getlabel(obj);

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
        [Route("GetLoadLocationsForCurrentStock"), HttpPost]
        public async Task<IActionResult> GetLoadLocationsForCurrentStock(GetLoadLocationsForCurrentStockModel obj)
        {
            Payload<string> response = await _iCommon.GetLoadLocationsForCurrentStock(obj);
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
        [Route("GetKitPlannerId"), HttpPost]
        public async Task<IActionResult> GetKitPlannerId(GetKitPlannerIdModel obj)
        {
            Payload<string> response = await _iCommon.GetKitPlannerId(obj);
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
        [Route("GetLoadIndustries_Auto"), HttpPost]
        public async Task<IActionResult> GetLoadIndustries_Auto(GetLoadIndustries_AutoModel obj)
        {
            Payload<string> response = await _iCommon.GetLoadIndustries_Auto(obj);
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
        [Route("GetContainersForCurrentStock"), HttpPost]
        public async Task<IActionResult> GetContainersForCurrentStock(GetContainersForCurrentStockModel obj)
        {
            Payload<string> response = await _iCommon.GetContainersForCurrentStock(obj);
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
        [Route("GetLoadSOCustomerNames"), HttpPost]
        public async Task<IActionResult> GetLoadSOCustomerNames(GetLoadSOCustomerNamesModel obj)
        {
            Payload<string> response = await _iCommon.GetLoadSOCustomerNames(obj);
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
        [Route("GetLoadUsersData"), HttpPost]
        public async Task<IActionResult> GetLoadUsersData(GetLoadUsersDataModel obj)
        {
            Payload<string> response = await _iCommon.GetLoadUsersData(obj);
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
        [Route("GetPalletCode"), HttpPost]
        public async Task<IActionResult> GetPalletCode(GetPalletCodeModel obj)
        {
            Payload<string> response = await _iCommon.GetPalletCode(obj);
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
        [Route("GetAllLocationsUnderWarehouse"), HttpPost]
        public async Task<IActionResult> GetAllLocationsUnderWarehouse(GetAllLocationsUnderWarehouseModel obj)
        {
            Payload<string> response = await _iCommon.GetAllLocationsUnderWarehouse(obj);
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


        [Route("GetMaterialForMiscReceipt"), HttpPost]
        public async Task<IActionResult> GetMaterialForMiscReceipt(GetMaterialForMiscReceiptModel obj)
        {
            Payload<string> response = await _iCommon.GetMaterialForMiscReceipt(obj);
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
        [Route("GetMaterialForMiscIssue"), HttpPost]
        public async Task<IActionResult> GetMaterialForMiscIssue(GetMaterialForMiscIssueModel obj)
        {
            Payload<string> response = await _iCommon.GetMaterialForMiscIssue(obj);
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

        [Route("GetStoreRefNumbers_RPRT"), HttpPost]
        public async Task<IActionResult> GetStoreRefNumbers_RPRT(GetStoreRefNumbers_RPRT_Model obj)
        {
            Payload<string> response = await _iCommon.GetStoreRefNumbers_RPRT(obj);
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

        [Route("GetOutboundNumbers_RPRT"), HttpPost]
        public async Task<IActionResult> GetOutboundNumbers_RPRT(GetOutboundNumbers_RPRT_Model obj)
        {
            Payload<string> response = await _iCommon.GetOutboundNumbers_RPRT(obj);
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

        [Route("GetCustomerData_RPRT"), HttpPost]
        public async Task<IActionResult> GetCustomerData_RPRT(GetCustomerData_RPRT_Model obj)
        {
            Payload<string> response = await _iCommon.GetCustomerData_RPRT(obj);
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

        [Route("GetSONumbers_RPRT"), HttpPost]
        public async Task<IActionResult> GetSONumbers_RPRT(GetSONumbers_RPRT_Model obj)
        {
            Payload<string> response = await _iCommon.GetSONumbers_RPRT(obj);
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
        [Route("GetSONumbersForSO_RPRT"), HttpPost]
        public async Task<IActionResult> GetSONumbersForSO_RPRT(GetSONumbersForSO_RPRT_Model obj)
        {
            Payload<string> response = await _iCommon.GetSONumbersForSO_RPRT(obj);
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
        [Route("GetReplenishedMaterialCode_RPRT"), HttpPost]
        public async Task<IActionResult> GetReplenishedMaterialCode_RPRT(GetReplenishedMaterialCode_RPRTModel obj)
        {
            Payload<string> response = await _iCommon.GetReplenishedMaterialCode_RPRT(obj);
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
        [Route("GetMaterialsForMaterialTracking_RPRT"), HttpPost]
        public async Task<IActionResult> GetMaterialsForMaterialTracking_RPRT(GetMaterialsForMaterialTracking_RPRTModel obj)
        {
            Payload<string> response = await _iCommon.GetMaterialsForMaterialTracking_RPRT(obj);
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

        [Route("GetMaterialsForBinReplenishment_RPRT"), HttpPost]
        public async Task<IActionResult> GetMaterialsForBinReplenishment_RPRT(GetMaterialsForBinReplenishment_RPRTModel obj)
        {
            Payload<string> response = await _iCommon.GetMaterialsForBinReplenishment_RPRT(obj);
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
        [Route("GetPOInvoiceNumbers_RPRT"), HttpPost]
        public async Task<IActionResult> GetPOInvoiceNumbers_RPRT(GetPOInvoiceNumbers_RPRTModel obj)
        {
            Payload<string> response = await _iCommon.GetPOInvoiceNumbers_RPRT(obj);
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
        [Route("GetAuditLogReferenceNo_RPRT"), HttpPost]
        public async Task<IActionResult> GetAuditLogReferenceNo_RPRT(GetAuditLogReferenceNo_RPRTModel obj)
        {
            Payload<string> response = await _iCommon.GetAuditLogReferenceNo_RPRT(obj);
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
        //16-03-23
        [Route("GetMCodeforGRN_RPRT"), HttpPost]
        public async Task<IActionResult> GetMCodeforGRN_RPRT(GetMCodeforGRN_RPRTModel obj)
        {
            Payload<string> response = await _iCommon.GetMCodeforGRN_RPRT(obj);
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

        [Route("GetUsers_RPRT"), HttpPost]
        public async Task<IActionResult> GetUsers_RPRT(GetUsers_RPRTModel obj)
        {
            Payload<string> response = await _iCommon.GetUsers_RPRT(obj);
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

        [Route("GetMaterialsForExpiryDate_RPRT"), HttpPost]
        public async Task<IActionResult> GetMaterialsForExpiryDate_RPRT(GetMaterialsForExpiryDate_RPRTModel obj)
        {
            Payload<string> response = await _iCommon.GetMaterialsForExpiryDate_RPRT(obj);
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

        [Route("GetOperatorSummaryUsers_RPRT"), HttpPost]
        public async Task<IActionResult> GetOperatorSummaryUsers_RPRT(GetOperatorSummaryUsers_RPRTModel obj)
        {
            Payload<string> response = await _iCommon.GetOperatorSummaryUsers_RPRT(obj);
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
        [Route("GetOperatorSummaryOBDNumbers_RPRT"), HttpPost]
        public async Task<IActionResult> GetOperatorSummaryOBDNumbers_RPRT(GetOperatorSummaryOBDNumbers_RPRTModel obj)
        {
            Payload<string> response = await _iCommon.GetOperatorSummaryOBDNumbers_RPRT(obj);
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
        [Route("GetOperatorSummarySONumbers_RPRT"), HttpPost]
        public async Task<IActionResult> GetOperatorSummarySONumbers_RPRT(GetOperatorSummarySONumbers_RPRTModel obj)
        {
            Payload<string> response = await _iCommon.GetOperatorSummarySONumbers_RPRT(obj);
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
        [Route("GetMCodeforExpiredMaterial_RPRT"), HttpPost]
        public async Task<IActionResult> GetMCodeforExpiredMaterial_RPRT(GetMCodeforExpiredMaterial_RPRTModel obj)
        {
            Payload<string> response = await _iCommon.GetMCodeforExpiredMaterial_RPRT(obj);
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
        [Route("GetCycleCountNames_RPRT"), HttpPost]
        public async Task<IActionResult> GetCycleCountNames_RPRT(GetCycleCountNames_RPRTModel obj)
        {
            Payload<string> response = await _iCommon.GetCycleCountNames_RPRT(obj);
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
        [Route("GetCycleCountCodes_RPRT"), HttpPost]
        public async Task<IActionResult> GetCycleCountCodes_RPRT(GetCycleCountCodes_RPRTModel obj)
        {
            Payload<string> response = await _iCommon.GetCycleCountCodes_RPRT(obj);
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
        [Route("GetMaterialsForCycleCount_RPRT"), HttpPost]
        public async Task<IActionResult> GetMaterialsForCycleCount_RPRT(GetMaterialsForCycleCount_RPRTModel obj)
        {
            Payload<string> response = await _iCommon.GetMaterialsForCycleCount_RPRT(obj);
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
        [Route("GetLocationManager_TenantList"), HttpPost]
        public async Task<IActionResult> GetLocationManager_TenantList()
        {
            Payload<string> response = await _iCommon.GetLocationManager_TenantList();
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
        [Route("GetLocationManager_Supplier"), HttpPost]
        public async Task<IActionResult> GetLocationManager_Supplier(GetLocationManager_Supplier obj)
        {
            Payload<string> response = await _iCommon.GetLocationManager_Supplier(obj);
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
        [Route("GetRacks_BulkModify"), HttpPost]
        public async Task<IActionResult> GetRacks_BulkModify(GetRacks_BulkModifyModel items)
        {
            Payload<string> response = await _iCommon.GetRacks_BulkModify(items);
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

        [Route("GetColumns_Levels_BulkModify"), HttpPost]
        public async Task<IActionResult> GetColumns_Levels_BulkModify(GetColumns_Levels_BulkModifyModel items)
        {
            Payload<string> response = await _iCommon.GetColumns_Levels_BulkModify(items);
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

        [Route("GetBins_BulkModify"), HttpPost]
        public async Task<IActionResult> GetBins_BulkModify(GetBins_BulkModifyModel items)
        {
            Payload<string> response = await _iCommon.GetBins_BulkModify(items);
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

        [Route("GetAvlBatchQty"), HttpPost]
        public async Task<IActionResult> GetAvlBatchQty(GetAvlBatchQtyModel items)
        {
            Payload<string> response = await _iCommon.GetAvlBatchQty(items);
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
        [Route("GetMCodesList"), HttpPost]
        public async Task<IActionResult> GetMCodesList(GetMCodesListModel MCodesListModel)
        {
            Payload<string> response = await _iCommon.GetMCodesList(MCodesListModel);
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
        [Route("GetLabels"), HttpPost]
        public async Task<IActionResult> GetLabels(GetLabelModel items)
        {
            Payload<string> response = await _iCommon.GetLabels(items);
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

        

        [Route("OBDWareHouse"), HttpPost]
        public async Task<IActionResult> OBDWareHouse(OBDWareHouseModel items)
        {
            Payload<string> response = await _iCommon.OBDWareHouse(items);
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

        [Route("GetLoadHHTypes"), HttpPost]
        public async Task<IActionResult> GetLoadHHTypes(GetLoadHHTypesModel getLoadHHTypesModel)
        {
            Payload<string> response = await _iCommon.GetLoadHHTypes(getLoadHHTypesModel);
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

        [Route("GetMCodes_DeliveryPicNote"), HttpPost]
        public async Task<IActionResult> GetMCodes_DeliveryPicNote(GetMCodes_DeliveryPicNoteModel items)
        {
            Payload<string> response = await _iCommon.GetMCodes_DeliveryPicNote(items);
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
        public async Task<IActionResult> GetSupplierList(GetSupplierListModel getSupplierListModel)
        {
            Payload<string> response = await _iCommon.GetSupplierList(getSupplierListModel);
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

        [Route("GetPO_OrderType"), HttpPost]
        public async Task<IActionResult> GetPO_OrderType(GetPO_OrderTypeModel items)
        {
            Payload<string> response = await _iCommon.GetPO_OrderType(items);
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

        [Route("GetSO_OrderType"), HttpPost]
        public async Task<IActionResult> GetSO_OrderType(GetSO_OrderTypeModel items)
        {
            Payload<string> response = await _iCommon.GetSO_OrderType(items);
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

        [Route("GetSKUList"), HttpPost]
        public async Task<IActionResult> GetSKUList(GetSKUListModel items)
        {
            Payload<string> response = await _iCommon.GetSKUList(items);
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

        [Route("GetCC_ShopFloor_Locations"), HttpPost]
        public async Task<IActionResult> GetCC_ShopFloor_Locations()
        {
            Payload<string> response = await _iCommon.GetCC_ShopFloor_Locations();
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

        [Route("GetCC_Capture_Containers"), HttpPost]
        public async Task<IActionResult> GetCC_Capture_Containers(GetCC_Capture_ContainersModel items)
        {
            Payload<string> response = await _iCommon.GetCC_Capture_Containers(items);
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

        [Route("GetCC_Capture_Materials"), HttpPost]
        public async Task<IActionResult> GetCC_Capture_Materials(GetCC_Capture_MaterialsModel items)
        {
            Payload<string> response = await _iCommon.GetCC_Capture_Materials(items);
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

        [Route("Get_PrinterIPList"), HttpPost]
        public async Task<IActionResult> Get_PrinterIPList()
        {
            Payload<string> response = await _iCommon.Get_PrinterIPList();
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


        //Below 4 Services from Outbound Tracking > Deliveries Pending > Changes > Delivery Document Line Items 

        [Route("LoadSONumbers"), HttpPost]
        public async Task<IActionResult> LoadSONumbers(LoadSONumbersModel items)
        {
            Payload<string> response = await _iCommon.LoadSONumbers(items);
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


        [Route("LoadCustomerPONumbers"), HttpPost]
        public async Task<IActionResult> LoadCustomerPONumbers(LoadCustomerPONumbersModel items)
        {
            Payload<string> response = await _iCommon.LoadCustomerPONumbers(items);
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


        [Route("GetCusPOInvoiceNoList"), HttpPost]
        public async Task<IActionResult> GetCusPOInvoiceNoList(GetCusPOInvoiceNoListModel items)
        {
            Payload<string> response = await _iCommon.GetCusPOInvoiceNoList(items);
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


        [Route("Delete_DeliveryDoc_LineItems"), HttpPost]
        public async Task<IActionResult> Delete_DeliveryDoc_LineItems(Delete_DeliveryDoc_LineItemsModel items)
        {
            Payload<string> response = await _iCommon.Delete_DeliveryDoc_LineItems(items);
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


        [Route("GetBusinessTypes"), HttpPost]
        public async Task<IActionResult> GetBusinessTypes(GetSoTypeModel items)
        {
            Payload<string> response = await _iCommon.GetBusinessTypes(items);
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

        [Route("GetBillTypes"), HttpPost]
        public async Task<IActionResult> GetBillTypes()
        {
            Payload<string> response = await _iCommon.GetBillTypes();
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

        [Route("GetCustomers"), HttpPost]
        public async Task<IActionResult> GetCustomers(Getcustomerobj items)
        {
            Payload<string> response = await _iCommon.GetCustomers(items);
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
        [Route("GetSecondLabelInputs"), HttpPost]
        public async Task<IActionResult> GetSecondLabelInputs()
        {
            Payload<string> response = await _iCommon.GetSecondLabelInputs();
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
        [Route("GetInOutVehicleNos"), HttpPost]
        public async Task<IActionResult> GetInOutVehicleNos(Getcustomerobj obj)
        {
            Payload<string> response = await _iCommon.GetInOutVehicleNos(obj);
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
        [Route("GetOBDStatus"), HttpPost]
        public async Task<IActionResult> GetOBDStatusList()
        {
            Payload<string> response = await _iCommon.GetOBDStatusList();
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


