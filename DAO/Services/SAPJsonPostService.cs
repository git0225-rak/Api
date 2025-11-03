using Simpolo_Endpoint.DAO.interfaces;
using Simpolo_Endpoint.DBUtil;
using Simpolo_Endpoint.Models;
using Simpolo_Endpoint.ModelsLibrary;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Nancy.Responses;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using System.Data;
using System.IO;

namespace Simpolo_Endpoint.DAO.Services
{
    public class SAPJsonPostService : AppDBService, ISAPJsonPostService
    {

        private readonly HttpClient _httpClient;
        private string _sapUsername;
        private string _sapPassword;
        private string _sapUrl;
        private string _sapPGIPostingURL;
        public SAPJsonPostService(IOptions<AppSettings> appSettings, HttpClient httpclient) : base(appSettings)
        {
            _httpClient = httpclient;
        }

        public async Task<string> GetSAPCsrfTokenAsync(int type, int OBDID)
        {
            string OBDNUMBER = "";
            try
            {
                DBFactory factory = new DBFactory();
                IDBUtility DbUtility = factory.getDBUtility();
                DataSet _SAPUserDetails = DbUtility.GetDS("SP_GetUserNamePassword", this.ConnectionString);

                if (_SAPUserDetails != null && _SAPUserDetails.Tables.Count > 0 && _SAPUserDetails.Tables[0].Rows.Count > 0)
                {
                    _sapUsername = _SAPUserDetails.Tables[0].Rows[0]["UserName"].ToString();
                    _sapPassword = _SAPUserDetails.Tables[0].Rows[0]["Password"].ToString();
                }
                else
                {
                    _sapUsername = string.Empty;
                    _sapPassword = string.Empty;
                }

                var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_sapUsername}:{_sapPassword}"));
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
                _httpClient.DefaultRequestHeaders.Add("X-CSRF-Token", "Fetch");

                string sql = "Exec SP_GetSAPURLBasedonScenario @Scenario=" + type;

                if (type == 3)
                {
                    OBDNUMBER = DbUtility.GetSqlS("Exec SP_GetOBDNumberBasedonOBDID @OutboundID=" + OBDID, this.ConnectionString);
                }


                _sapUrl = DbUtility.GetSqlS(sql, this.ConnectionString);

                _sapUrl = _sapUrl.Replace("PGI_ReversalSet('')", $"PGI_ReversalSet('{OBDNUMBER}')");

                var response = await _httpClient.GetAsync(_sapUrl);

                if (response.IsSuccessStatusCode && response.Headers.Contains("X-CSRF-Token"))
                {
                    string csrfToken = response.Headers.GetValues("X-CSRF-Token").FirstOrDefault();
                    return csrfToken ?? string.Empty;
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }


        public async Task<PGIResponse> SendPGIJSONDatatoSAP(int OutboundID)
            {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            PGIResponse pgiresponse = new PGIResponse();
            try
            {
                string SAPcsrfToken = await GetSAPCsrfTokenAsync(1, 0);
                if (string.IsNullOrEmpty(SAPcsrfToken))
                {
                    pgiresponse.SAPError = "Failed to fetch CSRF Token.";
                }
                var jsonResponse = "";

                var outboundData = DbUtility.GetDS("Exec [dbo].[SP_Get_APIJSONPostingData_PGI] @OutboundID=" + OutboundID, this.ConnectionString);
                if (outboundData != null && outboundData.Tables.Count > 0 && outboundData.Tables[0].Rows.Count > 0)
                {
                    jsonResponse = outboundData.Tables[0].Rows[0][0].ToString();
                }
                if (string.IsNullOrWhiteSpace(jsonResponse))
                {
                    throw new Exception("Invalid or empty JSON response.");
                }

                using var jsonDocument = JsonDocument.Parse(jsonResponse);
                var jsonObject = jsonDocument.RootElement;

                using var stream = new MemoryStream();
                using (var writer = new Utf8JsonWriter(stream, new JsonWriterOptions { Indented = false })) // Ensure compact JSON
                {
                    writer.WriteStartObject();
                    writer.WritePropertyName("d");
                    jsonObject.WriteTo(writer);
                    writer.WriteEndObject();
                }

                string modifiedJson = Encoding.UTF8.GetString(stream.ToArray());

                _httpClient.DefaultRequestHeaders.Clear();
                var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_sapUsername}:{_sapPassword}"));
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
                _httpClient.DefaultRequestHeaders.Add("X-CSRF-Token", SAPcsrfToken);
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var content = new StringContent(modifiedJson, Encoding.UTF8, "application/json");

                string sql = "Exec SP_GetSAPURLBasedonScenario @Scenario = " + 2;
                _sapPGIPostingURL = DbUtility.GetSqlS(sql, this.ConnectionString);

                // Send request
                var response = await _httpClient.PostAsync(_sapPGIPostingURL, content);
                int statusCode = (int)response.StatusCode;
                var responseText = await response.Content.ReadAsStringAsync();



                if (statusCode == 500)
                {
                    pgiresponse.SAPRefNumber = "Error : Issue with SAP Please Contact SAP Team";
                }
                else
                {
                    using JsonDocument doc = JsonDocument.Parse(responseText);
                    JsonElement root = doc.RootElement;
                    string responseMessage = "";

                    if (root.TryGetProperty("d", out JsonElement dElement) &&
                        dElement.TryGetProperty("PgiStat", out JsonElement pgiStatElement) &&
                        dElement.TryGetProperty("PgiDate", out JsonElement pgiDateElement))
                    {
                        pgiresponse.SAPRefNumber = pgiStatElement.GetString() ?? "No PGI Status";
                        pgiresponse.PGIPostingDate = ConvertEpochToDate(pgiDateElement.GetString());
                    }
                }

               
                return pgiresponse;


            }
            catch (Exception ex)
            {
                pgiresponse.SAPError = $"Error: {ex.Message}";
                return pgiresponse;
            }
            }







        public static string ConvertEpochToDate(string pgiDateEpoch)
        {
            pgiDateEpoch = pgiDateEpoch.Replace("/Date(", "").Replace(")/", "");
            if (long.TryParse(pgiDateEpoch, out long epoch))
            {
                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(epoch);
                DateTime dateTime = dateTimeOffset.UtcDateTime;
                return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
            }

            return "Invalid Date";
        }




        public async Task<PGIRevertResponse> SendPGIRevertJSONDatatoSAP(int OutboundID)
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            PGIRevertResponse pgiresponse = new PGIRevertResponse();
            try
            {
                string SAPcsrfToken = await GetSAPCsrfTokenAsync(3, OutboundID);
                if (string.IsNullOrEmpty(SAPcsrfToken))
                {
                    pgiresponse.SAPError = "Failed to fetch CSRF Token.";
                }


                var jsonResponse = "";

                var outboundData = DbUtility.GetDS("Exec [dbo].[SP_Get_APIJSONPostingData_PGIRevert] @OutboundID=" + OutboundID, this.ConnectionString);
                if (outboundData != null && outboundData.Tables.Count > 0 && outboundData.Tables[0].Rows.Count > 0)
                {
                    jsonResponse = outboundData.Tables[0].Rows[0][0].ToString();
                }
                if (string.IsNullOrWhiteSpace(jsonResponse))
                {
                    throw new Exception("Invalid or empty JSON response.");
                }

                using var jsonDocument = JsonDocument.Parse(jsonResponse);
                var jsonObject = jsonDocument.RootElement;

                using var stream = new MemoryStream();
                using (var writer = new Utf8JsonWriter(stream, new JsonWriterOptions { Indented = false })) // Ensure compact JSON
                {
                    writer.WriteStartObject();
                    writer.WritePropertyName("d");
                    jsonObject.WriteTo(writer);
                    writer.WriteEndObject();
                }

                string modifiedJson = Encoding.UTF8.GetString(stream.ToArray());
                _httpClient.DefaultRequestHeaders.Clear();
                var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_sapUsername}:{_sapPassword}"));
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
                _httpClient.DefaultRequestHeaders.Add("X-CSRF-Token", SAPcsrfToken);
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var content = new StringContent(modifiedJson, Encoding.UTF8, "application/json");

                string sql = "Exec SP_GetSAPURLBasedonScenario @Scenario = " + 4;
                _sapUrl = DbUtility.GetSqlS(sql, this.ConnectionString);

                // Send request
                var response = await _httpClient.PostAsync(_sapUrl, content);
                int statusCode = (int)response.StatusCode;
                var responseText = await response.Content.ReadAsStringAsync();

                if(statusCode==500)
                {
                    pgiresponse.SAPError = "Error : Issue with SAP Please Contact SAP Team";
                }
                else
                {
                    using JsonDocument doc = JsonDocument.Parse(responseText);
                    JsonElement root = doc.RootElement;
                    if (root.TryGetProperty("d", out JsonElement dElement) &&
                    dElement.TryGetProperty("DeliveryNo", out JsonElement deliveryNoElement))
                    {
                        string deliveryNo = deliveryNoElement.GetString() ?? "No Delivery Number";
                        pgiresponse.SAPRefNumber = deliveryNo;
                        pgiresponse.SAPError = "";
                    }

                    if (dElement.TryGetProperty("PgiDate", out JsonElement pgiDateElement))
                    {
                        pgiresponse.PGIPostingDate = ConvertEpochToDate(pgiDateElement.GetString());
                    }
                }
                return pgiresponse;
            }
            catch (Exception ex)
            {
                pgiresponse.SAPError = $"Error: {ex.Message}";
                return pgiresponse;
            }
        }

        public async Task<string> SendMaterialTransferJSONDatatoSAP(int TransferRequestID)
            {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                string SAPcsrfToken = await GetSAPCsrfTokenAsync(5, 0);
                if (string.IsNullOrEmpty(SAPcsrfToken))
                {
                    return "Failed to fetch CSRF Token.";
                }
                var jsonResponse = "";

                var MaterialTransferData = DbUtility.GetDS("Exec [dbo].[SP_Get_MaterialTransferDataJSON_SAP] @TransferRequestID=" + TransferRequestID, this.ConnectionString);
                if (MaterialTransferData != null && MaterialTransferData.Tables.Count > 0 && MaterialTransferData.Tables[0].Rows.Count > 0)
                {

                    jsonResponse = MaterialTransferData.Tables[0].Rows[0][0].ToString();
                }
                if (string.IsNullOrWhiteSpace(jsonResponse))
                {
                    throw new Exception("Invalid or empty JSON response.");
                }

                using var jsonDocument = JsonDocument.Parse(jsonResponse);
                var jsonObject = jsonDocument.RootElement;

                using var stream = new MemoryStream();
                using (var writer = new Utf8JsonWriter(stream, new JsonWriterOptions { Indented = false })) // Ensure compact JSON
                {
                    writer.WriteStartObject();
                    writer.WritePropertyName("d");
                    jsonObject.WriteTo(writer);
                    writer.WriteEndObject();
                }

                string modifiedJson = Encoding.UTF8.GetString(stream.ToArray());
                

                _httpClient.DefaultRequestHeaders.Clear();
                var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_sapUsername}:{_sapPassword}"));
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
                _httpClient.DefaultRequestHeaders.Add("X-CSRF-Token", SAPcsrfToken);
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var content = new StringContent(modifiedJson, Encoding.UTF8, "application/json");

                string sql = "Exec SP_GetSAPURLBasedonScenario @Scenario = " + 6;
                _sapUrl = DbUtility.GetSqlS(sql, this.ConnectionString);

                // Send request
                var response = await _httpClient.PostAsync(_sapUrl, content);
                int statusCode = (int)response.StatusCode;
                var responseText = await response.Content.ReadAsStringAsync();
                string responseMessage = "Unknown Response";

                if (statusCode == 500)
                {
                    responseMessage = "Error : Issue with SAP Please Contact SAP Team";
                    return responseMessage;
                }
                else
                {
                    using JsonDocument doc = JsonDocument.Parse(responseText);
                    JsonElement root = doc.RootElement;
                   

                    if (root.TryGetProperty("d", out JsonElement dElement) &&
                        dElement.TryGetProperty("HeaderStat", out JsonElement pgiStatElement))
                    {
                        responseMessage = pgiStatElement.GetString() ?? "No PGI Status";
                    }

                    return (statusCode == 200 || statusCode == 201) ? responseMessage : $"Error: {responseMessage}";
                }

              
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public async Task<string> SendMaterialTransferJSONDatatoSAP_ReturnPGR(int InboundID)
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                string SAPcsrfToken = await GetSAPCsrfTokenAsync(5, 0);
                if (string.IsNullOrEmpty(SAPcsrfToken))
                {
                    return "Failed to fetch CSRF Token.";
                }
                var jsonResponse = "";

                var MaterialTransferData = DbUtility.GetDS("Exec [dbo].[SP_Get_MaterialTransferDataJSON_SAP_Return_PGR] @InboundID=" + InboundID, this.ConnectionString);
                if (MaterialTransferData != null && MaterialTransferData.Tables.Count > 0 && MaterialTransferData.Tables[0].Rows.Count > 0)
                {
                    jsonResponse = MaterialTransferData.Tables[0].Rows[0][0].ToString();
                }
                if (string.IsNullOrWhiteSpace(jsonResponse))
                {
                    throw new Exception("Invalid or empty JSON response.");
                }

                using var jsonDocument = JsonDocument.Parse(jsonResponse);
                var jsonObject = jsonDocument.RootElement;

                using var stream = new MemoryStream();
                using (var writer = new Utf8JsonWriter(stream, new JsonWriterOptions { Indented = false })) // Ensure compact JSON
                {
                    writer.WriteStartObject();
                    writer.WritePropertyName("d");
                    jsonObject.WriteTo(writer);
                    writer.WriteEndObject();
                }

                string modifiedJson = Encoding.UTF8.GetString(stream.ToArray());


                _httpClient.DefaultRequestHeaders.Clear();
                var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_sapUsername}:{_sapPassword}"));
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
                _httpClient.DefaultRequestHeaders.Add("X-CSRF-Token", SAPcsrfToken);
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var content = new StringContent(modifiedJson, Encoding.UTF8, "application/json");

                string sql = "Exec SP_GetSAPURLBasedonScenario @Scenario = " + 6;
                _sapUrl = DbUtility.GetSqlS(sql, this.ConnectionString);

                // Send request
                var response = await _httpClient.PostAsync(_sapUrl, content);
                int statusCode = (int)response.StatusCode;
                var responseText = await response.Content.ReadAsStringAsync();
                string responseMessage = "Unknown Response";

                if (statusCode == 500)
                {
                    responseMessage = "Error : Issue with SAP Please Contact SAP Team";
                    return responseMessage;
                }
                else
                {
                    using JsonDocument doc = JsonDocument.Parse(responseText);
                    JsonElement root = doc.RootElement;


                    if (root.TryGetProperty("d", out JsonElement dElement) &&
                        dElement.TryGetProperty("HeaderStat", out JsonElement pgiStatElement))
                    {
                        responseMessage = pgiStatElement.GetString() ?? "No PGI Status";
                    }

                    return (statusCode == 200 || statusCode == 201) ? responseMessage : $"Error: {responseMessage}";
                }


            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        public async Task<string> SendMaterialTransferJSONDatatoSAP_Unblock(int TransferRequestID)
        {
            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            try
            {
                string SAPcsrfToken = await GetSAPCsrfTokenAsync(5, 0);
                if (string.IsNullOrEmpty(SAPcsrfToken))
                {
                    return "Failed to fetch CSRF Token.";
                }
                var jsonResponse = "";

                var MaterialTransferData = DbUtility.GetDS("Exec [dbo].[SP_Get_MaterialTransferDataJSON_SAP_Unblock] @TransferRequestID=" + TransferRequestID, this.ConnectionString);
                if (MaterialTransferData != null && MaterialTransferData.Tables.Count > 0 && MaterialTransferData.Tables[0].Rows.Count > 0)
                {
                    jsonResponse = MaterialTransferData.Tables[0].Rows[0][0].ToString();
                }
                if (string.IsNullOrWhiteSpace(jsonResponse))
                {
                    throw new Exception("Invalid or empty JSON response.");
                }

                using var jsonDocument = JsonDocument.Parse(jsonResponse);
                var jsonObject = jsonDocument.RootElement;

                using var stream = new MemoryStream();
                using (var writer = new Utf8JsonWriter(stream, new JsonWriterOptions { Indented = false })) // Ensure compact JSON
                {
                    writer.WriteStartObject();
                    writer.WritePropertyName("d");
                    jsonObject.WriteTo(writer);
                    writer.WriteEndObject();
                }

                string modifiedJson = Encoding.UTF8.GetString(stream.ToArray());


                _httpClient.DefaultRequestHeaders.Clear();
                var credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{_sapUsername}:{_sapPassword}"));
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
                _httpClient.DefaultRequestHeaders.Add("X-CSRF-Token", SAPcsrfToken);
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var content = new StringContent(modifiedJson, Encoding.UTF8, "application/json");

                string sql = "Exec SP_GetSAPURLBasedonScenario @Scenario = " + 6;
                _sapUrl = DbUtility.GetSqlS(sql, this.ConnectionString);

                // Send request
                var response = await _httpClient.PostAsync(_sapUrl, content);
                int statusCode = (int)response.StatusCode;
                var responseText = await response.Content.ReadAsStringAsync();
                string responseMessage = "Unknown Response";

                if (statusCode == 500)
                {
                    responseMessage = "Error : Issue with SAP Please Contact SAP Team";
                    return responseMessage;
                }
                else
                {
                    using JsonDocument doc = JsonDocument.Parse(responseText);
                    JsonElement root = doc.RootElement;


                    if (root.TryGetProperty("d", out JsonElement dElement) &&
                        dElement.TryGetProperty("HeaderStat", out JsonElement pgiStatElement))
                    {
                        responseMessage = pgiStatElement.GetString() ?? "No PGI Status";
                    }

                    return (statusCode == 200 || statusCode == 201) ? responseMessage : $"Error: {responseMessage}";
                }


            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

    }
}
