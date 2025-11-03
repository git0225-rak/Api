using Simpolo_Endpoint.DAO.interfaces;
using Simpolo_Endpoint.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Simpolo_Endpoint
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class ItemMastercontroller : ControllerBase
    {
        private readonly IItemMasterData _ItemMasterData;
        public ItemMastercontroller(IItemMasterData ItemmasterData)
        {
            _ItemMasterData = ItemmasterData;
        }
        [Route("GetMaterialList"), HttpPost]
        public async Task<IActionResult> GetMaterialList(GetMaterialListModel items)
        {
            Payload<string> response = await _ItemMasterData.GetMaterialList(items);
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

        [Route("GetItemMasterAutocompletes"), HttpPost]
        public async Task<IActionResult> GetItemMasterAutocompletes(GetItemMasterAutocompletesModel items)
        {
            Payload<string> response = await _ItemMasterData.GetItemMasterAutocompletes(items);
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

        [Route("GetMaterailParametersInfo"), HttpPost]
        public async Task<IActionResult> GetMaterailParametersInfo(GetMaterailParametersInfoModel items)
        {
            Payload<string> response = await _ItemMasterData.GetMaterailParametersInfo(items);
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

        //save Basic Material Details
        [Route("UpsertItemMasterBasicDetails"), HttpPost]
        public async Task<IActionResult> UpsertItemMasterBasicDetails(UpsertItemMasterBasicDetailsModel items)
        {
            Payload<string> response = await _ItemMasterData.UpsertItemMasterBasicDetails(items);
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


        [Route("SaveUpdateSupplierDetails"), HttpPost]

        public async Task<IActionResult> SaveUpdateSupplierDetails(SaveUpdateSupplierDetailsModel SaveUpdateSupplier)
        {
            Payload<string> response = await _ItemMasterData.SaveUpdateSupplierDetails(SaveUpdateSupplier);
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


        // Save UOM Panel 
        [Route("UpsertUoMInfo"), HttpPost]
        public async Task<IActionResult> UpsertUoMInfo(UpsertUoMInfoModel items)
        {
            Payload<string> response = await _ItemMasterData.UpsertUoMInfo(items);
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

        [Route("SetMsps"), HttpPost]
        public async Task<IActionResult> SetMsps(SetMspsModel items)
        {
            Payload<string> response = await _ItemMasterData.SetMsps(items);
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

        //MCode Check 
        [Route("Check_MCodeExists"), HttpPost]
        public async Task<IActionResult> Check_MCodeExists(GetMCodeCheck items)
        {
            Payload<string> response = await _ItemMasterData.Check_MCodeExists(items);
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

         [Route("GetItemPictureDetails"), HttpPost]
        public async Task<IActionResult> GetItemPictureDetails(GetItemPictureDetailsModel items)
        {
            Payload<AuthResponce> response = await _ItemMasterData.GetItemPictureDetails(items);
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

        [Route("UpsertItemPictureDetails"), HttpPost]
        public async Task<IActionResult> UpsertItemPictureDetails(UpsertItemPictureDetailsModel items)
        {
            Payload<string> response = await _ItemMasterData.UpsertItemPictureDetails(items);
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
