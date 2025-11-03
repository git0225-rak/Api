
using Simpolo_Endpoint.ModelsLibrary;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;


namespace Simpolo_Endpoint.DAO.Services
{

    public abstract class AppDBService
    {

        private readonly IConfiguration _config;
        public AppDBService(IConfiguration config, FileStorage fileStorage)
        {
            _config = config;
            FileStorage = fileStorage;
        }

        public AppSettings AppSettings { get; }

        public FileStorage FileStorage { get; }

        public string IDOCConectionString { get => GetIDOCConnection(); }
        public string ConnectionString { get => GetConnection(); }
        public string APIURL { get => APIURL1(); }

        public string ServiceURL { get => ServiceURL1(); }

        public string QADSOAP { get => QADSOAP1(); }
        public string wsaTo { get => wsaTo1(); }
        public string wsaMessageId { get => wsaMessageId1(); }

        public string validIssuer { get => GetvalidIssuer(); }

        public string issuerSigningKey { get => GetissuerSigningKey(); }

        public string ValidAudience { get => GetValidAudience(); }

        public string FolderPath { get => Getpath(); }


        public string ipaddress { get => ipaddress1(); }

        public int port { get => port1(); }

        public string WhatAppUrl { get => GetWhatAppUrl(); }

        public string WhatAppAuthToken { get => GetWhatAppAuthToken(); }
        public AppDBService(IOptions<AppSettings> appSettings)
        {

            AppSettings = appSettings.Value;


        }
        public string GetConnection()
        {
            string connectionString = AppSettings.ConnectionString;
            return connectionString;
        }

        public string GetIDOCConnection()
        {
            string IDOCConnectionString = AppSettings.IDOCConectionString;
            return IDOCConnectionString;
        }


        public string APIURL1()
        {
            string APIURL = AppSettings.APIURL;
            return APIURL;
        }

        public string ServiceURL1()
        {
            string ServiceURL = AppSettings.ServiceURL;
            return ServiceURL;

        }
        public string QADSOAP1()
        {
            string QADSOAP = AppSettings.QADSOAP;
            return QADSOAP;
        }
        public string wsaTo1()
        {
            string wsaTo = AppSettings.wsaTo;
            return wsaTo;
        }
        public string wsaMessageId1()
        {
            string wsaMessageId = AppSettings.wsaMessageId;
            return wsaMessageId;
        }
        public string GetvalidIssuer()
        {
            string validIssuer = AppSettings.validIssuer;
            return validIssuer;
        }
        public string GetValidAudience()
        {
            string validAudience = AppSettings.validAudience;
            return validAudience;
        }

        public string GetissuerSigningKey()
        {
            string issuerSigningKey = AppSettings.issuerSigningKey;
            return issuerSigningKey;
        }
        public string GetFileStorage()
        {
            string File = FileStorage.FolderPath;
            return File;
        }

        public string Getpath()
        {
            string File = AppSettings.FolderPath;
            return File;
        }

        public string ipaddress1()
        {
            string ipaddress = AppSettings.ipaddress;
            return ipaddress;
        }

        public int port1()
        {
            int port = AppSettings.port;
            return port;
        }

        public string GetWhatAppUrl()
        {
            string whatappurl = AppSettings.WhatAppUrl;
            return whatappurl;
        }
        public string GetWhatAppAuthToken()
        {
            string whatapptoken = AppSettings.WhatAppAuthToken;
            return whatapptoken;
        }
    }
}