using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FWMSC21Core_BusinessEntities.Entities
{
    public class QADRequestObj
    {
        public string MCode { get; set; }
        public decimal Qty { get; set; }
        public string CurrentDate { get; set; }
        public string ProjectRefNo { get; set; }
        public string BatchNo { get; set; }
        public string ExpDate { get; set; }
        public string FromLoc { get; set; }
        public string ToLoc { get; set; }
        public string SONumber { get; set; }
        public string WoLot { get; set; }
        // public string StatusCode { get; set; }
        // public string QADTransaction { get; set; }
        // public string Remarks { get; set; }
        public int RID { get; set; }
        public string TotalQty { get; set; }
        public string TOQADStatus { get; set; }
        public string FromQADStatus { get; set; }
        public int MMID { get; set; }
        public int SOHeaderID { get; set; }
        public int SODetailsID { get; set; }
        public int OutboundID { get; set; }
        public int UserID { get; set; }
        public int PickID { get; set; }
        public string MfgDate { get; set; }
        public string StatusCode { get; set; }
        public string QADTransaction { get; set; }
        public string Remarks { get; set; }
        public string ISError { get; set; }

    }
}
