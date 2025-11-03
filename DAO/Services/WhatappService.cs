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
using Microsoft.AspNetCore.Identity;
using Simpolo_Endpoint.DTO;
using Simpolo_Endpoint.DAO.Services;
using Newtonsoft.Json;

public class WhatsAppService : AppDBService, IWhatappInterface
{
    private readonly HttpClient _httpClient;
    private readonly string _apiUrl;
    private readonly string _authToken;
    public WhatsAppService(IOptions<AppSettings> appSettings, HttpClient httpClient) : base(appSettings)
    {
        _authToken = AppSettings.WhatAppAuthToken;
        _apiUrl = AppSettings.WhatAppUrl;
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_authToken}");

    }
    public async Task<string> SendWAMBasedOnActivity(WhatAppNotes whatappnotes)
    {
        try
        {

            DBFactory factory = new DBFactory();
            IDBUtility DbUtility = factory.getDBUtility();
            string query = "Exec SP_Get_VechileDetails_MobileNumbers_Customer @OutboundID="+whatappnotes.OutboundID+",@VehicleNumber=" + DBLibrary.SQuote (whatappnotes.VechileNumber) + ",@ScenarioID=" + whatappnotes.ScenarioID+"";
            DataSet DSWAS = DbUtility.GetDS(query, this.ConnectionString);

            if (DSWAS.Tables.Count == 0 || DSWAS.Tables[0].Rows.Count == 0)
            {
                return "No data found for the given OutboundID.";
            }

            var row = DSWAS.Tables[0].Rows[0];
            string phoneNumbersStr = row["PhoneNumbers"].ToString();
            string templateName = row["TemplateName"].ToString();
            string vehicleNumber = row["VehicleNo"].ToString();
            string Date = row["Datetime"].ToString();

            var phoneNumbers = phoneNumbersStr.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            var tasks = phoneNumbers.Select(async phoneNumber =>
            {
                var parametersList = new List<object>
            {
                new { type = "text", text = vehicleNumber }
            };

                if (whatappnotes.ScenarioID != 1)
                {
                    parametersList.Add(new { type = "text", text = Date });
                }

                var requestData = new
                {
                    to = "91"+phoneNumber,
                    type = "template",
                    source = "external",
                    template = new
                    {
                        name = templateName,
                        language = new { code = "en" },
                        components = new object[]
                        {
                        new
                        {
                            type = "body",
                            parameters = parametersList
                        }
                        }
                    }
                };

                string jsonContent = JsonConvert.SerializeObject(requestData);
                using var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                return await SendMessageAsync(content);
            });

            var results = await Task.WhenAll(tasks);
            return string.Join(", ", results);
        }
        catch (Exception ex)
        {
            return $"Error: {ex.Message}";
        }
    }


    private async Task<string> SendMessageAsync(StringContent content)
    {
        var response = await _httpClient.PostAsync(_apiUrl, content);
        return await response.Content.ReadAsStringAsync();
    }

}

