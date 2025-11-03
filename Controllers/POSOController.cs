using Simpolo_Endpoint.DAO.interfaces;
using Simpolo_Endpoint.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Simpolo_Endpoint.Models.POSOModel;
using Microsoft.AspNetCore.Authorization;

namespace Simpolo_Endpoint.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class POSOController : ControllerBase
    {
        private readonly IPOSO _iposo;
        public POSOController(IPOSO iposo)
        {
            _iposo = iposo;
        }
        [Route("GetSOList"), HttpPost]
        public async Task<IActionResult> GetSOList(SOListModel sOList)
        {
            Payload<string> response = await _iposo.GetSOList(sOList);
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

        [Route("UpsertSoHeaderDetails"), HttpPost]
        public async Task<IActionResult> UpsertSoHeaderDetails(UpdateSOModel updateSO)
        {
            Payload<string> response = await _iposo.UpsertSoHeaderDetails(updateSO);
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

        [Route("GetCustomerPODetailsList"), HttpPost]
        public async Task<IActionResult> GetCustomerPODetailsList(CustomerPOListModel customerPOList)
        {
            Payload<string> response = await _iposo.GetCustomerPODetailsList(customerPOList);
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

        [Route("UpsertCustomerPO"), HttpPost]
        public async Task<IActionResult> UpsertCustomerPO(UpsertCustomerPOModel upsertCustomerPO)
        {
            Payload<string> response = await _iposo.UpsertCustomerPO(upsertCustomerPO);
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

        [Route("GetMaterialSODetailsList"), HttpPost]
        public async Task<IActionResult> GetMaterialSODetailsList(MaterialSODetailsListModel materialSODetailsList)
        {
            Payload<string> response = await _iposo.GetMaterialSODetailsList(materialSODetailsList);
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
        [Route("GetSoHeaderDetails"), HttpPost]
        public async Task<IActionResult> GetSoHeaderDetails(GetSoHeaderDetailsModel getSoHeaderDetails)
        {
            Payload<string> response = await _iposo.GetSoHeaderDetails(getSoHeaderDetails);
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

        [Route("DeleteCustomerPO"), HttpPost]
        public async Task<IActionResult> DeleteCustomerPO(DeleteCustomerPOModel deleteCustomerPO)
        {
            Payload<string> response = await _iposo.DeleteCustomerPO(deleteCustomerPO);
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

        [Route("UpsertMaterialSODetails"), HttpPost]
        public async Task<IActionResult> UpsertMaterialSODetails(UpsertMaterialSODetailsModel upsertMaterialSODetails)
        {
            Payload<string> response = await _iposo.UpsertMaterialSODetails(upsertMaterialSODetails);
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
        //po
        [Route("GetPOHeaderList"), HttpPost]
        public async Task<IActionResult> GetPOHeaderList(POHeaderListInputModel items)
        {
            Payload<string> response = await _iposo.GetPOHeaderList(items);
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


        [Route("GetPOList"), HttpPost]
        public async Task<IActionResult> GetPOList(POHeaderListInputModel items)
        {
            Payload<string> response = await _iposo.GetPOList(items);
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

        //save in po
        [Route("UpsertPOHeaderData"), HttpPost]
        public async Task<IActionResult> UpsertPOHeaderData(POHeaderModel items)
        {
            Payload<string> response = await _iposo.UpsertPOHeaderData(items);
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
        [Route("GetPOMaterialDetailsList"), HttpPost]
        public async Task<IActionResult> GetPOMaterialDetailsList(GetPOMaterialDetailsDataModel items)
        {
            Payload<string> response = await _iposo.GetPOMaterialDetailsList(items);
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

        [Route("SavePOMaterialDetailsData"), HttpPost]
        public async Task<IActionResult> SavePOMaterialDetailsData(POMaterialDetailsDataModel items)
        {
            Payload<string> response = await _iposo.SavePOMaterialDetailsData(items);
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

        [Route("GetPODetailsList"), HttpPost]
        public async Task<IActionResult> GetPODetailsList(POMaterialDetailsDataModel items)
        {
            Payload<string> response = await _iposo.GetPODetailsList(items);
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

        [Route("UpsertSupplierInvoice"), HttpPost]
        public async Task<IActionResult> UpsertSupplierInvoice(SupplierInvoiceInputModel items)
        {
            Payload<string> response = await _iposo.UpsertSupplierInvoice(items);
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

        [Route("GetSupplierInvoice"), HttpPost]
        public async Task<IActionResult> GetSupplierInvoice(SupplierInvoiceInputModel items)
        {
            Payload<string> response = await _iposo.GetSupplierInvoice(items);
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

        [Route("DeleteSku_SupplerInvoiceDetails"), HttpPost]
        public async Task<IActionResult> DeleteSku_SupplerInvoiceDetails(DeleteSku_SupplerInvoiceDetailsModel items)
        {
            Payload<string> response = await _iposo.DeleteSku_SupplerInvoiceDetails(items);
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

        [Route("UpsertSku_SupplerInvoiceDetails"), HttpPost]
        public async Task<IActionResult> UpsertSku_SupplerInvoiceDetails(UpsertSku_SupplerInvoiceDetailsModel items)
        {
            Payload<string> response = await _iposo.UpsertSku_SupplerInvoiceDetails(items);
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
        [Route("Getmspscheckboxs"), HttpPost]
        public async Task<IActionResult> Getmspscheckboxs(GetmspscheckboxsModel items)
        {
            Payload<string> response = await _iposo.Getmspscheckboxs(items);
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
        [Route("GetSku_SupplerInvoiceDetails"), HttpPost]
        public async Task<IActionResult> GetSku_SupplerInvoiceDetails(GetSku_SupplerInvoiceDetailsModel items)
        {
            Payload<string> response = await _iposo.GetSku_SupplerInvoiceDetails(items);
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
        [Route("DeleteSupplierInvoice"), HttpPost]
        public async Task<IActionResult> DeleteSupplierInvoice(DeleteSupplierInvoiceInputModel items)
        {
            Payload<string> response = await _iposo.DeleteSupplierInvoice(items);
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

        [Route("SupplerInvoiceDetailsRowUpdating"), HttpPost]
        public async Task<IActionResult> SupplerInvoiceDetailsRowUpdating(SupplerInvoiceMaterialListModel items)
        {
            Payload<string> response = await _iposo.SupplerInvoiceDetailsRowUpdating(items);
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


        [Route("DeleteSupplerInvoiceDetails"), HttpPost]
        public async Task<IActionResult> DeleteSupplerInvoiceDetails(SupplerInvoiceMaterialListModel items)
        {
            Payload<string> response = await _iposo.DeleteSupplerInvoiceDetails(items);
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

        [Route("GetPOHeaderDetails"), HttpPost]
        public async Task<IActionResult> GetPOHeaderDetails(GetPOHeaderDetailsModel items)
        {
            Payload<string> response = await _iposo.GetPOHeaderDetails(items);
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

        [Route("DownloadASNIO"), HttpPost]
        public async Task<IActionResult> DownloadASNIO(DownloadASNIOModel downloadASNIO)
        {
            Payload<string> response = await _iposo.DownloadASNIO(downloadASNIO);
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
        [Route("ImportASNIO"), HttpPost]
        public async Task<IActionResult> ImportASNIO(ImportASNIOModel ImportASN)
        {
            Payload<string> response = await _iposo.ImportASNIO(ImportASN);

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

        [Route("ImportASNCheckMandatory"), HttpPost]
        public async Task<IActionResult> ImportASNCheckMandatory(ImportASNCheckMandatorModel ImportASNCheckMandatory)
        {
            Payload<string> response = await _iposo.ImportASNCheckMandatory(ImportASNCheckMandatory);

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

        [Route("SODetails_UpdateStorageLoaction"), HttpPost]
        public async Task<IActionResult> SODetails_UpdateStorageLoaction(SODetails_UpdateStorageLoactionModel items)
        {
            Payload<string> response = await _iposo.SODetails_UpdateStorageLoaction(items);

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

        [Route("GetKitStoreRefNo"), HttpPost]
        public async Task<IActionResult> GetKitStoreRefNo(KitModel items)
        {
            Payload<string> response = await _iposo.GetKitStoreRefNo(items);
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

        [Route("GetKittingList"), HttpPost]
        public async Task<IActionResult> GetKittingList(KitModel items)
        {
            Payload<string> response = await _iposo.GetKittingList(items);
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

        [Route("GetChildItemsForKitting"), HttpPost]
        public async Task<IActionResult> GetChildItemsForKitting(KitModel items)
        {
            Payload<string> response = await _iposo.GetChildItemsForKitting(items);
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
