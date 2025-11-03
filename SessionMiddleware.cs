using Simpolo_Endpoint.DBUtil;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Data.SqlClient;
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
using Microsoft.AspNetCore.Builder;
using Simpolo_Endpoint.DAO.Services;
using Microsoft.Extensions.Options;
using Simpolo_Endpoint.ModelsLibrary;
using System;
using Simpolo_Endpoint.Models;
using System.Web.Http;

namespace Simpolo_Endpoint
{
    public class SessionMiddleware : AppDBService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly RequestDelegate _next;
        private readonly IDBUtility _dbUtility;

        // List of paths to exclude from session validation
        private readonly string[] excludedPaths = { "/Login/LoginUser", "/Login/getnewtoken", "/Login/LogOut", "/Login/UserLogin", "/Login/UserLogout" , "/WMSAPI/PostIDocToFalcon" , "/ShipperAPI/PostIDocToFalcon", "/SAPJSON/MaterialTransfersPosting" };

        public SessionMiddleware(IOptions<AppSettings> appSettings, RequestDelegate next, IConfiguration configuration, IHttpContextAccessor httpContextAccessor) : base(appSettings)
        {
            _next = next;
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public async Task Invoke(HttpContext context)
        {
            var endpoint = context.GetEndpoint();

            if (endpoint?.Metadata?.GetMetadata<AllowAnonymousAttribute>() != null ||
                excludedPaths.Any(path => context.Request.Path.Equals(path, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("Bypassing session validation for excluded endpoint.");
                await _next(context);
                return;
            }

            string deviceID = null;
            string sessionId = null;
            string UserID ="0";
            string ipAddress = context.Connection.RemoteIpAddress?.ToString();


            if (context.Request.ContentType != null &&
                context.Request.ContentType.Contains("application/json", StringComparison.OrdinalIgnoreCase))
            {
                context.Request.EnableBuffering();

                using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true))
                {
                    var body = await reader.ReadToEndAsync();
                    context.Request.Body.Position = 0;

                    var jsonObject = JObject.Parse(body);
                    deviceID = jsonObject["AuthToken"]?["AuthKey"]?.ToString();
                    UserID = jsonObject["AuthToken"]?["UserID"]?.ToString();
                }
            }

            if (!string.IsNullOrEmpty(deviceID))
            {
                Console.WriteLine("Device ID found: " + deviceID);
                var response = await ValidateSessionIdAsync("0", ipAddress, deviceID,UserID);
                if (response.Result == "1")
                {
                    await _next(context);
                    return;
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Invalid session.");
                    return;
                }
            }
            else
            {
                
                var token = GetTokenFromRequest(context);
                if (token != null)
                {
                    var jwtToken = new JwtSecurityTokenHandler().ReadToken(token) as JwtSecurityToken;
                    if (jwtToken != null)
                    {
                        sessionId = jwtToken.Claims.FirstOrDefault(c => c.Type == "SessionId")?.Value;
                        UserID = jwtToken.Claims.FirstOrDefault(c => c.Type == "UserID")?.Value;

                        if (string.IsNullOrEmpty(ipAddress) && context.Request.Headers.ContainsKey("X-Forwarded-For"))
                        {
                            ipAddress = context.Request.Headers["X-Forwarded-For"].ToString();
                        }

                        if (!string.IsNullOrEmpty(sessionId))
                        {
                            var response = await ValidateSessionIdAsync(sessionId, ipAddress, "0", UserID);
                            if (response.Result == "1")
                            {
                                await _next(context);
                                return;
                            }
                            else
                            {
                                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                                await context.Response.WriteAsync("Invalid session.");
                                return;
                            }
                        }
                        else
                        {
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            await context.Response.WriteAsync("Invalid session.");
                            return;
                        }
                    }
                }
                else
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Invalid session.");
                    return;
                }
            }
        }

        private string GetTokenFromRequest(HttpContext context)
        {
            if (context.Request.Headers.TryGetValue("Authorization", out var authHeader) &&
                authHeader.ToString().StartsWith("Bearer "))
            {
                return authHeader.ToString().Substring("Bearer ".Length).Trim();
            }
            return null;
        }

        private async Task<Payload<string>> ValidateSessionIdAsync(string sessionId, string IPAddress, String DeviceID,string UserID)
        {
            Payload<string> response = new Payload<string>();
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility dbUtility = factory.getDBUtility();
                string query = $"EXEC [dbo].[sp_Validate_Conc_Session] @SessionId='{sessionId}'"+",@IPAddress='"+IPAddress+"'"+",@DeviceID='"+DeviceID+"'"+",@UserID='"+UserID+"'";
                var ds = dbUtility.GetDS(query, this.ConnectionString);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    response.Result = ds.Tables[0].Rows[0]["N"].ToString();
                }
                else
                {
                    response.Result = "2";
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
    }
}