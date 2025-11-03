using Azure.Core;
using Simpolo_Endpoint.DAO.interfaces;
using Simpolo_Endpoint.DBUtil;
using Simpolo_Endpoint.Models;
using Simpolo_Endpoint.ModelsLibrary;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Nancy;
using Nancy.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;
using Simpolo_Endpoint.DTO;
using Microsoft.AspNetCore.Http;
using Org.BouncyCastle.Tls.Crypto.Impl.BC;

namespace Simpolo_Endpoint.DAO.Services
{
    public class LoginService : AppDBService, ILogin
    {

        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginService(IOptions<AppSettings> appSettings, IHttpContextAccessor httpContextAccessor) : base(appSettings)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }
        private string _ClassCode = string.Empty;
        public async Task<Payload<string>> GetMainMenus_Generic(GetMainMenus_GenericModel obj)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object>  {
                    { "CurrentUserTypeIDs" , obj.CurrentUserTypeIDs },
                    { "TenantID" , obj.TenantID }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "GetMainMenus_Generic", sqlParams).ConfigureAwait(false);
            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;

        }


        public async Task<Payload<string>> GetDashBordReportData(GetDashBordReportDataModel items)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                var sqlParams = new Dictionary<string, object> {
                    {"@WAREHOUSEID" ,items.WAREHOUSEID },
                    {"@Date",items.Date }
                };
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                response.Result = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "sp_KPI_Dashboard", sqlParams).ConfigureAwait(false);

            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }
#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<Payload<string>> ChangePassword(ChangePasswordModel items)
#pragma warning restore CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        {
            Payload<string> response = new Payload<string>();
            try
            {
                byte[] bytes = System.Text.ASCIIEncoding.ASCII.GetBytes(items.Password);
                string encrypted = Convert.ToBase64String(bytes);
                byte[] bytes1 = System.Text.ASCIIEncoding.ASCII.GetBytes(items.NewPassword);
                string newencrypted = Convert.ToBase64String(bytes1);

                StringBuilder sp = new StringBuilder();
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                string query = "SELECT Enpassword AS S FROM GEN_User WHERE UserGUID=@UserGUID";
                object result;
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@UserGUID", items.UserGUID);
                    connection.Open();
                    result = command.ExecuteScalar();

                }
                if (result.ToString() != encrypted)
                {
                    response.Result = "-3";// Sorry, your current password does not match with the one in the database.Please change your current and try again
                    return response;
                }
                if (items.NewPassword != "")
                {
                    try
                    {
                        string UpdateQuery = "UPDATE GEN_User SET mobile=@mobile, Password=@password, Enpassword=@enpassword WHERE UserGUID=@userGUID";
                        using (SqlConnection connection = new SqlConnection(ConnectionString))
                        {
                            using (SqlCommand command = new SqlCommand(UpdateQuery, connection))
                            {
                                command.Parameters.AddWithValue("@mobile", items.mobile);
                                command.Parameters.AddWithValue("@password", items.NewPassword);
                                command.Parameters.AddWithValue("@enpassword", newencrypted);
                                command.Parameters.AddWithValue("@userGUID", items.UserGUID.ToString());
                                connection.Open();
                                command.ExecuteNonQuery();
                            }
                        }

                        response.Result = "1";

                    }
#pragma warning disable CS0168 // The variable 'Ex' is declared but never used
                    catch (Exception Ex)
#pragma warning restore CS0168 // The variable 'Ex' is declared but never used
                    {
                        response.Result = "-1";//Error while updating 
                    }
                }
            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }


        public async Task<Payload<AuthResponce>> LoginUser([FromBody] LoginModel items)
        {
            Payload<AuthResponce> response = new Payload<AuthResponce>();
            try
            {
                AuthResponce authResponce = new AuthResponce();
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.issuerSigningKey));
                var expires = DateTime.UtcNow.AddHours(1);
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                var sqlParams = new Dictionary<string, object> {
                    {"@Email" ,items.Email },
                    {"@Password",items.Password}
                };

                var Data = await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "sp_GEN_CheckUserLogin1", sqlParams).ConfigureAwait(false);
                var serializer = new JavaScriptSerializer();
                dynamic resultObject = serializer.DeserializeObject(Data);
                var table = resultObject["Table"];
                Encryption encryption = new Encryption();
                var enryptedEmail = encryption.Encryptword(items.Email);
                var enryptedPassword = encryption.Encryptword(items.Password);
                if (table == null || table.Count == 0)
                {
                    response.Result = null;
                    return response;
                }

                items.UserID = (int)table[0]["UserId"];
                var TenantID = (int)table[0]["TenantID"];
                if (items.IsForceLogin == 1)
                {

                    var logoutR = LogOut(items);
                }
                if (items.IsForceLogin != 1)
                {
                    string Query = "EXEC sp_validateuser @userid=" + items.UserID + ", @DeviceID=" + '0' + "";
                    DataSet ds = DbUtility.GetDS(Query, ConnectionString);
                    int ID = Convert.ToInt32(ds.Tables[0].Rows[0]["N"]);
                    if (ID == 1 || ID ==2)
                    {
                        response.ResponseCode =ds.Tables[0].Rows[0]["S"].ToString();
                        return response;
                    }
                }

                var ipAddress = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();
                if (_httpContextAccessor.HttpContext.Request.Headers.ContainsKey("X-Forwarded-For"))
                {
                    ipAddress = _httpContextAccessor.HttpContext.Request.Headers["X-Forwarded-For"].ToString();
                }

                var sessionId = Guid.NewGuid().ToString();
                var sessionExpiryTime = DateTime.UtcNow.AddHours(1);
                var DeviceID = '0';

                var sessionParams = new Dictionary<string, object>
                        {
                             {"@UserId", items.UserID},
                             {"@INOUT", 1},
                             {"@LogTime", sessionExpiryTime},
                             {"@TenantID", TenantID},
                             {"@IPAddress", ipAddress},
                             {"@sessionId", sessionId},
                             {"@DeviceID", DeviceID},

                        };
                await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "sp_SEC_InsertLogAudit", sessionParams).ConfigureAwait(false);


                var claims = new[]   {
                   new Claim(ClaimTypes.Name, enryptedPassword),
                    new Claim(ClaimTypes.Email, enryptedEmail),
                    new Claim("UserID",items.UserID.ToString()),
                    new Claim("SessionId", sessionId),
                   new Claim(ClaimTypes.Expiration, expires.ToString("s"))
                    };
                var token = new JwtSecurityToken(
                    issuer: this.validIssuer,
                    audience: this.ValidAudience,
                    claims: claims,
                    expires: expires,
                    signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
                );
                authResponce.UserInfo = table.ToString();
                authResponce.JsonToken = new JwtSecurityTokenHandler().WriteToken(token);
                response.Result = authResponce;

            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }


        public async Task<Payload<AuthResponce>> GetNewToken()
        {
            Payload<AuthResponce> response = new Payload<AuthResponce>();
            try
            {
                AuthResponce authResponce = new AuthResponce();
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.issuerSigningKey));
                var expires = DateTime.UtcNow.Add(TimeSpan.FromHours(1));
                var httpContext = _httpContextAccessor.HttpContext;

                string authHeader = httpContext.Request.Headers["Authorization"];
                if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
                {
                    string token = authHeader.Substring("Bearer ".Length).Trim();
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var validationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = securityKey,
                        ValidateIssuer = true,
                        ValidIssuer = this.validIssuer,
                        ValidateAudience = true,
                        ValidAudience = this.ValidAudience,
                        ValidateLifetime = false
                    };

                    ClaimsPrincipal claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
                    string name = claimsPrincipal.FindFirst(ClaimTypes.Name)?.Value;
                    string email = claimsPrincipal.FindFirst(ClaimTypes.Email)?.Value;
                    string SessionId = claimsPrincipal.FindFirst("SessionId")?.Value;

                    if (validatedToken.ValidTo < DateTime.UtcNow)
                    {
                        if (!string.IsNullOrEmpty(email))
                        {
                            expires = DateTime.UtcNow.AddHours(1);
                            var claims = new[] {
                                new Claim(ClaimTypes.Name, name),
                                new Claim(ClaimTypes.Email, email),
                                new Claim("SessionId", SessionId),
                                new Claim(ClaimTypes.Expiration, expires.ToString("s"))
                            };

                            var newToken = new JwtSecurityToken(
                                issuer: this.validIssuer,
                                audience: this.ValidAudience,
                                claims: claims,
                                expires: expires,
                                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
                            );

                            authResponce.JsonToken = tokenHandler.WriteToken(newToken);
                            authResponce.SessionID = SessionId;
                        }
                        else
                        {
                            response.addError("Invalid UserId in token.");
                            return response;
                        }
                    }
                    else
                    {
                        response.addError("Token is not expired.");
                    }

                    response.Result = authResponce;
                    return response;
                }
                else
                {
                    response.addError("Authorization header is missing or invalid.");
                }
            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }

        public async Task<Payload<AuthResponce>> GetFilePath(FilePathModel items)
        {
            Payload<AuthResponce> response = new Payload<AuthResponce>();
            try
            {
                AuthResponce authResponce = new AuthResponce();
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                string query = "SELECT * FROM Gen_Account WHERE AccountID = @AccountID";
                SqlCommand command = new SqlCommand(query);
                command.Parameters.AddWithValue("@AccountID", items.AccountID);
                var Output = await DbUtility.GetJsonDataFromCommand(this.ConnectionString, command);

                if (!string.IsNullOrEmpty(Output))
                {
                    JObject jsonData = JObject.Parse(Output);
                    JArray tableArray = (JArray)jsonData["Table"];

                    if (tableArray.Count > 0 && tableArray[0]["React_LogoPath"] != null)
                    {
                        string logoPath = (string)tableArray[0]["React_LogoPath"];
                        string url = string.IsNullOrEmpty(logoPath) ? null : Path.Combine(this.FolderPath, logoPath);

                        using (var client = new WebClient())
                        {
                            if (url != null)
                            {
                                byte[] dataBytes = client.DownloadData(new Uri(url));
                                string encodedFileAsBase64 = Convert.ToBase64String(dataBytes);
                                authResponce.Base64Image = encodedFileAsBase64;
                            }
                            else
                            {
                                authResponce.Base64Image = "null";
                            }
                        }
                    }
                    else
                    {
                        authResponce.Base64Image = "null";
                    }
                }
                else
                {
                    authResponce.Base64Image = "null";
                }

                response.Result = authResponce;
            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }

#pragma warning disable CS1998 // This async method lacks 'await' operators and will run synchronously. Consider using the 'await' operator to await non-blocking API calls, or 'await Task.Run(...)' to do CPU-bound work on a background thread.
        public async Task<ProfileDTO> Login(ProfileDTO items)
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            ProfileDTO UserDetails = null;

            try
            {
                string loginQuery = "EXEC [dbo].[sp_GEN_CheckUserLogin] @Email = '" + items.MailID + "', @Password = '" + EnryptString(items.Password) + "'";
                DataSet ds = DbUtility.GetDS(loginQuery, this.ConnectionString);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    UserDetails = new ProfileDTO
                    {
                        UserID = Convert.ToString(ds.Tables[0].Rows[0]["UserID"]),
                        FirstName = Convert.ToString(ds.Tables[0].Rows[0]["FirstName"]),
                        LastName = Convert.ToString(ds.Tables[0].Rows[0]["LastName"]),
                        MailID = items.MailID,
                        Password = EnryptString(items.Password),
                        UserRole = Convert.ToString(ds.Tables[0].Rows[0]["Roles"]),
                        SiteCodes = Convert.ToString(ds.Tables[0].Rows[0]["Zones"]),
                        DepartmentIDs = Convert.ToString(ds.Tables[0].Rows[0]["Departments"]),
                        MachineIPAddress = items.PrinterIP,
                        TenantID = Convert.ToInt32(ds.Tables[0].Rows[0]["TenantID"]),
                        SsoId = Convert.ToInt32(ds.Tables[0].Rows[0]["SSOUserID"]),
                        AccountId = Convert.ToString(ds.Tables[0].Rows[0]["AccountID"]),
                        WarehouseID = Convert.ToInt32(ds.Tables[0].Rows[0]["Warehouses"]),
                        VStoreType = Convert.ToString(ds.Tables[0].Rows[0]["VStoreType"]),
                        VStoreUsername = Convert.ToString(ds.Tables[0].Rows[0]["VStoreUsername"]),
                        VStorePassword = Convert.ToString(ds.Tables[0].Rows[0]["VStorePassword"])
                    };
                    items.UserID = Convert.ToString(ds.Tables[0].Rows[0]["UserID"]);
                    items.TenantID = Convert.ToInt32(ds.Tables[0].Rows[0]["TenantID"]);
                    if (items.IsForceLogin == 1)
                    {
                        await UserLogout(items);
                    }
                    if (items.IsForceLogin != 1)
                    {
                        string validateSessionQuery = "EXEC sp_validateuser @userid = " + items.UserID + ", @DeviceID ='" + items.DeviceID + "'";
                        DataSet ds1 = DbUtility.GetDS(validateSessionQuery, this.ConnectionString);


                        int n = Convert.ToInt32(ds1.Tables[0].Rows[0]["N"]);

                        if (n == 1 || n == 2)
                        {
                            UserDetails.IsSessionActive = n.ToString();
                            UserDetails.Message = ds1.Tables[0].Rows[0]["S"].ToString();
                            return UserDetails;
                        }

                        else
                        {
                            UserDetails.IsSessionActive = "0";
                            UserDetails.Message = ds1.Tables[0].Rows[0]["S"].ToString();
                        }
                    }
                    var auditParams = new Dictionary<string, object>
                        {
                        {"@UserId", items.UserID},
                        {"@INOUT", 1},
                        {"@TenantID", items.TenantID},
                        {"@IPAddress", items.SessionIdentifier},
                        {"@sessionId", 0},
                        {"@DeviceID", items.DeviceID}
                         };

                    await DbUtility.GetjsonDataFromDataset(this.ConnectionString, "sp_SEC_InsertLogAudit", auditParams).ConfigureAwait(false);

                }
                else
                {
                    throw new WMSExceptionMessage() { WMSExceptionCode = "EMC_User_DAL_0001", WMSMessage = ErrorMessages.EMC_User_DAL_0001, ShowAsError = true };
                }

                return UserDetails;
            }
            catch (WMSExceptionMessage ex)
            {
                throw  new WMSExceptionMessage() { WMSExceptionCode = "EMC_User_DAL_0001", WMSMessage = ErrorMessages.EMC_User_DAL_0001, ShowAsError = true };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public string EnryptString(string strEncrypted)
        {
            byte[] b = System.Text.ASCIIEncoding.ASCII.GetBytes(strEncrypted);
            string encrypted = Convert.ToBase64String(b);
            return encrypted;
        }

        public async Task<Payload<string>> LogOut(LoginModel items)
        {
            Payload<string> response = new Payload<string>();
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                string sessionId = GetTokenFromRequest();
                string LoginSessionQuery = "EXEC [dbo].[sp_SessionLogout] @UserID = " + items.UserID + ", @sessionId ='"+sessionId+"'"+",@isforcelogin="+items.IsForceLogin+"";
                DataSet ds = DbUtility.GetDS(LoginSessionQuery.ToString(), this.ConnectionString);
                var SessionData = JsonConvert.SerializeObject(ds);
                response.Result = SessionData;

            }
            catch (SqlException Sqlex)
            {
                response.addError(Sqlex.Message);
            }
            catch (Exception ex)
            {
                response.addError(ex.Message);
            }
            return response;
        }

        public async Task<ProfileDTO> UserLogout(ProfileDTO items)
        {
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();

                string query = "EXEC [dbo].[sp_logout_HHT] @UserID = " + items.UserID + ",@IsForceLogin="+items.IsForceLogin+ ", @DeviceID='" + items.DeviceID+"'";

                DataSet ds = DbUtility.GetDS(query, this.ConnectionString);

                if (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    items.IsLoggedIn = Convert.ToBoolean(ds.Tables[0].Rows[0]["N"]);
                }
                return items;
            }
            catch (WMSExceptionMessage excp)
            {
                throw excp;
            }
            catch (Exception excp)
            {
                ExceptionData oExcpData = new ExceptionData();
                oExcpData.AddInputs("oCriteria", items);

                ExceptionHandling.LogException(excp, _ClassCode + "001", oExcpData);
                throw new WMSExceptionMessage { WMSExceptionCode = ErrorMessages.WMSExceptionCode, WMSMessage = ErrorMessages.WMSExceptionMessage, ShowAsCriticalError = true };
            }
        }

        private string GetTokenFromRequest()
        {
            var context = _httpContextAccessor.HttpContext;
            if (context.Request.Headers.TryGetValue("Authorization", out var authHeader) &&
                authHeader.ToString().StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                string token = authHeader.ToString().Substring("Bearer ".Length).Trim();
                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

                try
                {
                    var jwtToken = handler.ReadToken(token) as JwtSecurityToken;
                    return jwtToken?.Claims.FirstOrDefault(c => c.Type == "SessionId")?.Value;
                }
                catch (Exception)
                {
                    // Log the exception if necessary.
                    return "0";
                }
            }
            return null;
        }


    }
}
