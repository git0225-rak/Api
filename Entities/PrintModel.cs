using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simpolo_Endpoint.Entities
{
    public class PrintModel
    {
        public string PrintDesc { set; get; }
        public string ZPLScript { set; get; }
        public string TenantID { set; get; }
        public string Labeltype { set; get; }
    }
}
