using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FWMSC21Core_BusinessEntities.Entities
{
    public class StockAdjustmentData
    {

        public string MCode { get; set; }
        public string Location { get; set; }
        public string LogicalQuantity { get; set; }
        public string PhysicalQuantity { get; set; }
        public string BatchNo { get; set; }
        public string ProjectRefNo { get; set; }

        public string CartonCode { get; set; }

        public string UniqRef { get; set; }

        public int CCIID { get; set; }
        public string UoM { get; set; }
        public string UoMQty { get; set; }
        public string rmks { get; set; }
        public string effDate { get; set; }
        public string crAcct { get; set; }
        public string yn { get; set; }
        public string crCc { get; set; }
        public string QADLocation { get; set; }
        public int CTRNID { get; set; }

        //public string City { get; set; }
        //public string OrderQty { get; set; }
        //public string OrderDate { get; set; }
        //public string AllocatedQty { get; set; }
        //public string Editable { get; set; }


    }
}
