using Simpolo_Endpoint.DAO.interfaces;
using Simpolo_Endpoint.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using System.Reflection.Metadata;
using QRCoder;
using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Kernel.Geom;
using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Layout.Borders;

namespace Simpolo_Endpoint.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class AdministrationController : ControllerBase
    {
        private readonly IAdministration _Administration;
        public AdministrationController(IAdministration administration)
        {
            _Administration = administration;
        }

        [Route("GetMaterialType"), HttpPost]
        public async Task<IActionResult> GetMaterialType(GetMaterialTypeModel items)
        {
            Payload<string> response = await _Administration.GetMaterialType(items);
            if (response != null)
            {
                if (!response.HasErrors && !response.HasWarnings)
                {
                    return Ok(response);
                }
                else if (response.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response.GetErrors());
                }
                else
                {
                    return StatusCode(StatusCodes.Status202Accepted, response.GetWarnings());
                }
            }
            else
            {
                return BadRequest("Failed to retrieve data");
            }
        }

        [Route("GetTenantsInMaterialType"), HttpPost]
        public async Task<IActionResult> GetTenantsInMaterialType(TenantsInputModel items)
        {
            Payload<string> response = await _Administration.GetTenantsInMaterialType(items);
            if (response != null)
            {
                if (!response.HasErrors && !response.HasWarnings)
                {
                    return Ok(response);
                }
                else if (response.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response.GetErrors());
                }
                else
                {
                    return StatusCode(StatusCodes.Status202Accepted, response.GetWarnings());
                }
            }
            else
            {
                return BadRequest("Failed to retrieve data");
            }
        }


        [Route("UpsertMaterialType"), HttpPost]
        public async Task<IActionResult> UpsertMaterialType(UpsertMaterialTypeModel items)
        {
            Payload<string> response = await _Administration.UpsertMaterialType(items);
            if (response != null)
            {
                if (!response.HasErrors && !response.HasWarnings)
                {
                    return Ok(response);
                }
                else if (response.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response.GetErrors());
                }
                else
                {
                    return StatusCode(StatusCodes.Status202Accepted, response.GetWarnings());
                }
            }
            else
            {
                return BadRequest("Failed to retrieve data");
            }
        }


        [Route("DeleteMaterialType"), HttpPost]
        public async Task<IActionResult> DeleteMaterialType(DeleteMaterialInputModel items)
        {
            Payload<string> response = await _Administration.DeleteMaterialType(items);
            if (response != null)
            {
                if (!response.HasErrors && !response.HasWarnings)
                {
                    return Ok(response);
                }
                else if (response.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response.GetErrors());
                }
                else
                {
                    return StatusCode(StatusCodes.Status202Accepted, response.GetWarnings());
                }
            }
            else
            {
                return BadRequest("Failed to retrieve data");
            }
        }


        [Route("GetMaterialGroup"), HttpPost]
        public async Task<IActionResult> GetMaterialGroup(GetMaterialGroupModel items)
        {
            Payload<string> response = await _Administration.GetMaterialGroup(items);
            if (response != null)
            {
                if (!response.HasErrors && !response.HasWarnings)
                {
                    return Ok(response);
                }
                else if (response.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response.GetErrors());
                }
                else
                {
                    return StatusCode(StatusCodes.Status202Accepted, response.GetWarnings());
                }
            }
            else
            {
                return BadRequest("Failed to retrieve data");
            }
        }


        [Route("UpsertMaterialGroup"), HttpPost]
        public async Task<IActionResult> UpsertMaterialGroup(UpsertMaterialGroupModel items)
        {
            Payload<string> response = await _Administration.UpsertMaterialGroup(items);
            if (response != null)
            {
                if (!response.HasErrors && !response.HasWarnings)
                {
                    return Ok(response);
                }
                else if (response.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response.GetErrors());
                }
                else
                {
                    return StatusCode(StatusCodes.Status202Accepted, response.GetWarnings());
                }
            }
            else
            {
                return BadRequest("Failed to retrieve data");
            }
        }


        [Route("DeleteMaterialGroup"), HttpPost]
        public async Task<IActionResult> DeleteMaterialGroup(DeleteMaterialGroupModel items)
        {
            Payload<string> response = await _Administration.DeleteMaterialGroup(items);
            if (response != null)
            {
                if (!response.HasErrors && !response.HasWarnings)
                {
                    return Ok(response);
                }
                else if (response.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response.GetErrors());
                }
                else
                {
                    return StatusCode(StatusCodes.Status202Accepted, response.GetWarnings());
                }
            }
            else
            {
                return BadRequest("Failed to retrieve data");
            }
        }


        [Route("EditMaterialGroup"), HttpPost]
        public async Task<IActionResult> EditMaterialGroup(EditMaterialGroupModel items)
        {
            Payload<string> response = await _Administration.EditMaterialGroup(items);
            if (response != null)
            {
                if (!response.HasErrors && !response.HasWarnings)
                {
                    return Ok(response);
                }
                else if (response.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response.GetErrors());
                }
                else
                {
                    return StatusCode(StatusCodes.Status202Accepted, response.GetWarnings());
                }
            }
            else
            {
                return BadRequest("Failed to retrieve data");
            }
        }


        [Route("GetContainerData"), HttpPost]
        public async Task<IActionResult> GetContainerData(GetContainerDataModel items)
        {
            Payload<string> response = await _Administration.GetContainerData(items);
            if (response != null)
            {
                if (!response.HasErrors && !response.HasWarnings)
                {
                    return Ok(response);
                }
                else if (response.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response.GetErrors());
                }
                else
                {
                    return StatusCode(StatusCodes.Status202Accepted, response.GetWarnings());
                }
            }
            else
            {
                return BadRequest("Failed to retrieve data");
            }
        }



        [Route("generatepdf"), HttpPost]
        public async Task<IActionResult>  generatepdf(ContainercodeQrLables items)
        {
            Task<DataSet> palletDataTask = _Administration.GetPalletData(items);
            DataSet palletData = palletDataTask.Result;
            string[] qrDataArray = [];

            if (palletData != null && palletData.Tables.Count > 0)
            {
                DataTable table = palletData.Tables[0];
                qrDataArray = table.AsEnumerable()
                                   .Select(row => row["CartonCode"].ToString()) 
                                   .ToArray();
            }

            using (MemoryStream pdfStream = new MemoryStream())
            {
                using (PdfWriter writer = new PdfWriter(pdfStream))
                using (PdfDocument pdf = new PdfDocument(writer))
                using (iText.Layout.Document document = new iText.Layout.Document(pdf))
                {
                    PdfFont boldFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);

                    // Create a 3-column table layout
                    Table table = new Table(UnitValue.CreatePercentArray(3)).UseAllAvailableWidth();

                    foreach (var item in qrDataArray)
                    {
                        try
                        {
                            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
                            {
                                QRCodeData qrCodeData = qrGenerator.CreateQrCode(item, QRCodeGenerator.ECCLevel.Q);
                                PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
                                byte[] qrCodeBytes = qrCode.GetGraphic(20);

                                ImageData imageData = ImageDataFactory.Create(qrCodeBytes);
                                Image qrImage = new Image(imageData)
                                    .SetWidth(56.6f)  // ≈ 20mm
                                    .SetHeight(56.6f)
                                    .SetHorizontalAlignment(HorizontalAlignment.CENTER);

                                Paragraph textParagraph = new Paragraph(item)
                                    .SetFont(boldFont)
                                    .SetFontSize(9)
                                    .SetTextAlignment(TextAlignment.CENTER)
                                    .SetMarginTop(-5)
                                    .SetMultipliedLeading(1.3f);

                                Cell cell = new Cell()
                                    .SetTextAlignment(TextAlignment.CENTER)
                                    .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                                    .SetPadding(5)
                                    .Add(qrImage)
                                    .Add(textParagraph);

                                table.AddCell(cell);
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error generating QR Code for {item}: {ex.Message}");
                        }
                    }

                    // Fill the last row if not a multiple of 3
                    int remaining = qrDataArray.Length % 3;
                    if (remaining != 0)
                    {
                        for (int i = 0; i < 3 - remaining; i++)
                        {
                            table.AddCell(new Cell().Add(new Paragraph("")));
                        }
                    }

                    document.Add(table);
                }

                return File(pdfStream.ToArray(), "application/pdf", "QRCodeOutput.pdf");
            }
        }




        [Route("CreateNewCartons"), HttpPost]
        public async Task<IActionResult> CreateNewCartons(CreateNewCartonsModel items)
        {
            Payload<string> response = await _Administration.CreateNewCartons(items);
            if (response != null)
            {
                if (!response.HasErrors && !response.HasWarnings)
                {
                    return Ok(response);
                }
                else if (response.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response.GetErrors());
                }
                else
                {
                    return StatusCode(StatusCodes.Status202Accepted, response.GetWarnings());
                }
            }
            else
            {
                return BadRequest("Failed to retrieve data");
            }
        }

        [Route("GetContainerPrint"), HttpPost]
        public async Task<IActionResult> GetContainerPrint(PrintInputModel items)
        {
            Payload<string> response = await _Administration.GetContainerPrint(items);
            if (response != null)
            {
                if (!response.HasErrors && !response.HasWarnings)
                {
                    return Ok(response);
                }
                else if (response.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response.GetErrors());
                }
                else
                {
                    return StatusCode(StatusCodes.Status202Accepted, response.GetWarnings());
                }
            }
            else
            {
                return BadRequest("Failed to retrieve data");
            }
        }


        [Route("GetContainerPrint_Network"), HttpPost]
        public async Task<IActionResult> GetContainerPrint_Network(PrintInputModel items)
        {
            Payload<string> response = await _Administration.GetContainerPrint_Network(items);
            if (response != null)
            {
                if (!response.HasErrors && !response.HasWarnings)
                {
                    return Ok(response);
                }
                else if (response.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response.GetErrors());
                }
                else
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
