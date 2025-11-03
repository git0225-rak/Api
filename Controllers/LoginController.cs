using Simpolo_Endpoint.DAO.interfaces;
using Simpolo_Endpoint.DTO;
using Simpolo_Endpoint.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simpolo_Endpoint.Controllers
{ //FWMSC21_GSK_API/

    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILogin _iLogin;
        private string _ClassCode = string.Empty;
        JsonSettings jsonSettings = new JsonSettings();

        public LoginController(ILogin login)
        {
            _iLogin = login;
        }


        [Route("GetMainMenus_Generic"), HttpPost]
        public async Task<IActionResult> GetMainMenus_Generic(GetMainMenus_GenericModel getMainMenus_GenericModel)
        {
            Payload<string> response = await _iLogin.GetMainMenus_Generic(getMainMenus_GenericModel);
            if (response != null)
            {
                if (!response.HasErrors && !response.HasWarnings)
                {
                    return Ok(response);
                }
                else if (response.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response.GetErrors());
                }
                else
                {
                    return StatusCode(StatusCodes.Status202Accepted, response.GetWarnings());
                }
            }
            else
            {
                return BadRequest("Failed to retrieve data");
            }
        }


        [Route("ChangePassword"), HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel items)
        {
            Payload<string> response = await _iLogin.ChangePassword(items);
            if (response != null)
            {
                if (!response.HasErrors && !response.HasWarnings)
                {
                    return Ok(response);
                }
                else if (response.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response.GetErrors());
                }
                else
                {
                    return StatusCode(StatusCodes.Status202Accepted, response.GetWarnings());
                }
            }
            else
            { 
                return BadRequest("Failed to retrieve data");
            }
        }

        [Route("GetDashBordReportData"), HttpPost]
        public async Task<IActionResult> GetDashBordReportData(GetDashBordReportDataModel items)
        {
            Payload<string> response = await _iLogin.GetDashBordReportData(items);
            if (response != null)
            {
                if (!response.HasErrors && !response.HasWarnings)
                {
                    return Ok(response);
                }
                else if (response.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response.GetErrors());
                }
                else
                {
                    return StatusCode(StatusCodes.Status202Accepted, response.GetWarnings());
                }
            }
            else
            {
                return BadRequest("Failed to retrieve data");
            }
        }

        [AllowAnonymous]
        [Route("LoginUser"), HttpPost]
        public async Task<IActionResult> LoginUser(LoginModel items)
        {
            Payload<AuthResponce> response = await _iLogin.LoginUser(items);
            if (response != null)
            {
                if (!response.HasErrors && !response.HasWarnings)
                {
                    return Ok(response);
                }
                else if (response.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response.GetErrors());
                }
                else
                {
                    return StatusCode(StatusCodes.Status202Accepted, response.GetWarnings());
                }
            }
            else
            {
                return BadRequest("Failed to retrieve data");
            }
        }

        [AllowAnonymous]
        [Route("GetNewToken"), HttpPost]
        public async Task<IActionResult> GetNewToken()
        {
            Payload<AuthResponce> response = await _iLogin.GetNewToken();
            if (response != null)
            {
                if (!response.HasErrors && !response.HasWarnings)
                {
                    return Ok(response);
                }
                else if (response.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response.GetErrors());
                }
                else
                {
                    return StatusCode(StatusCodes.Status202Accepted, response.GetWarnings());
                }
            }
            else
            {
                return BadRequest("Failed to retrieve data");
            }
        }
        [Route("GetFilePath"), HttpPost]
        public async Task<IActionResult> GetFilePath(FilePathModel items)
        {
            Payload<AuthResponce> response = await _iLogin.GetFilePath(items);
            if (response != null)
            {
                if (!response.HasErrors && !response.HasWarnings)
                {
                    return Ok(response);
                }
                else if (response.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response.GetErrors());
                }
                else
                {
                    return StatusCode(StatusCodes.Status202Accepted, response.GetWarnings());
                }
            }
            else
            {
                return BadRequest("Failed to retrieve data");
            }
        }


        [AllowAnonymous]
        [Route("UserLogin")]
        [HttpPost]
        public async Task<string> UserLogin(WMSCoreMessage oRequest)
        {
            try
            {
                if (oRequest == null) return null;

                LoginUserDTO _oLoginUserDTO = (LoginUserDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);
                if (_oLoginUserDTO == null) return null;
                var profileDTO = await _iLogin.Login(new ProfileDTO
                {
                    MailID = _oLoginUserDTO.MailID,
                    Password = _oLoginUserDTO.PasswordEncrypted,
                    PrinterIP = _oLoginUserDTO.PrinterIP,
                    SessionIdentifier = _oLoginUserDTO.SessionIdentifier,
                    DeviceID = oRequest.AuthToken.AuthKey,
                    LoginTimeStamp = oRequest.AuthToken.LoginTimeStamp,
                    IsForceLogin = _oLoginUserDTO.IsForceLogin
                });
                string json1 = JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.LoginUserDTO, profileDTO), jsonSettings.JsonSerializerSettings);
                string json = JsonConvert.SerializeObject(json1);
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

        [AllowAnonymous]
        [Route("LogOut"), HttpPost]
        public async Task<IActionResult> LogOut(LoginModel items)
        {
            Payload<string> response = await _iLogin.LogOut(items);
            if (response != null)
            {
                if (!response.HasErrors && !response.HasWarnings)
                {
                    return Ok(response);
                }
                else if (response.Errors.Count > 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, response.GetErrors());
                }
                else
                {
                    return StatusCode(StatusCodes.Status202Accepted, response.GetWarnings());
                }
            }
            else
            {
                return BadRequest("Failed to retrieve data");
            }
        }

        [AllowAnonymous]
        [Route("UserLogout")]
        [HttpPost]
        public async Task<string> UserLogout(WMSCoreMessage oRequest)
        {
            try
            {
                if (oRequest == null) return null;

                ProfileDTO _oLoginUserDTO = (ProfileDTO)WMSCoreEndpointSecurity.ValidateRequest(oRequest);
                if (_oLoginUserDTO == null) return null;
                _oLoginUserDTO.UserID = oRequest.AuthToken.UserID;
                _oLoginUserDTO.DeviceID = oRequest.AuthToken.AuthKey;
                var profileDTO = await _iLogin.UserLogout(_oLoginUserDTO);
                
                string json1 = JsonConvert.SerializeObject(WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.ProfileDTO, profileDTO), jsonSettings.JsonSerializerSettings);
                string json = JsonConvert.SerializeObject(json1);
                return json;
            }
            catch (WMSExceptionMessage ex)
            {
                var response = WMSCoreEndpointSecurity.PrepareResponse(oRequest.AuthToken, EndpointConstants.DTO.Exception, new List<WMSExceptionMessage> { ex });
                return JsonConvert.SerializeObject(response, jsonSettings.JsonSerializerSettings);
            }
            catch (Exception excp)
            {
                ExceptionHandling.LogException(excp, _ClassCode + "001");
                return null;
            }
        }


    }
}
