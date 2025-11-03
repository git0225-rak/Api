using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simpolo_Endpoint.DTO
{
    public class GoodsInDTO
    {
        public string InboundID { set; get; }
        public string Storerefno { set; get; }
        public string MCode { set; get; }
        public string Location { set; get; }
        public string CartonCode { set; get; }
        public string DocumentQuantity { set; get; }
        public string PoHeaderID { set; get; }
        public string StorageLocation { set; get; }
        public string HUNo { set; get; }
        public string HUsize { set; get; }
        public string LineNumber { set; get; }
        public string BatchNumber { set; get; }
        public string ExpDate { set; get; }
        public string MfgDate { set; get; }
        public string SerialNumber { set; get; }
        public string ProjectRefNo { set; get; }
        public string MRP { set; get; }

        public string Grade { set; get; }
        public int LoggedinUserID { set; get; }
        public string SupplierInvoiceId { set; get; }
        public int IsRequestFromPC { set; get; }
        public string ConvertedQuantity { set; get; }

        public int IsStockAdjust { set; get; }

        public decimal AdjustQty { set; get; }

        public decimal ActualQty { set; get; }

        public string IsStockAdd { set; get; }

        public int IsPhysicalEmpty { get; set; }
    }
}
