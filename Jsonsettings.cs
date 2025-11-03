using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simpolo_Endpoint
{
    public class JsonSettings
    {
        public JsonSerializerSettings JsonSerializerSettings { get; }

        public JsonSettings()
        {
            JsonSerializerSettings = new JsonSerializerSettings
            {
                Converters =
                {
                    new StringEnumConverter()
                },
                NullValueHandling = NullValueHandling.Include,
            };
        }
    }
}
