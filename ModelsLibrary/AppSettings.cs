using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Simpolo_Endpoint.ModelsLibrary
{
    public class AppSettings
    {
        public string ConnectionString { get; set; }

        public string WhatAppUrl { get; set; }

        public string WhatAppAuthToken { get; set; }

        public string IDOCConectionString { get; set; }
        public string APIURL { get; set; }

        public string ServiceURL { get; set; }
        public string QADSOAP { get; set; }
        public string wsaTo { get; set; }
        public string wsaMessageId { get; set; }
        public string validIssuer { get; set; }
        public string validAudience { get; set; }

        public string issuerSigningKey { get; set; }

        public string FolderPath { get; set; }
        public int port { get; set; }
        public string ipaddress { get; set; }


    }

    public class FileStorage
    {
        public string FolderPath { get; set; }
    }
}