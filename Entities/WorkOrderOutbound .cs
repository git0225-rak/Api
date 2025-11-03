using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FWMSC21Core_BusinessEntities.Entities
{
    public class WorkOrderOutbound
    {
        public string Obdno { set; get; }

        public string Lineno { set; get; }

        public string GoodsMovementDetailsID { set; get; }

        public string GoodsMovementTypeID { set; get; }

        public string CartonNo { set; get; }

        public decimal Qty { set; get; }

        public string MCode { set; get; }

        public string InvoiceNumber { set; get; }

        public string SONumber { set; get; }

        public string POSOHeaderId { set; get; }

        public decimal CF { set; get; }

        public string MaterialMaster_IUoMID { set; get; }

        public string SupplierInvoiceID { set; get; }

        public string OutboundId { set; get; }

        public string CartonId { set; get; }

        public string MaterialMasterId { set; get; }


        public string OEMPartNo { set; get; }

        public string MDescription { set; get; }


        public string HasDisc { set; get; }

        public string IsDam { set; get; }

        public string CreatedBy { set; get; }

        public string MaterialStorageParameterIDs { set; get; }

        public string MaterialStorageParameterValues { set; get; }

        public string KitId { set; get; }

        public string KitNo { set; get; }

        public string InvoiceQty { set; get; }

        public string SLoc { set; get; }

        public string SLocId { set; get; }

        public string SerialNo { set; get; }

        public string MfgDate { set; get; }

        public string ExpDate { set; get; }

        public string BatchNo { set; get; }


        public string ProjectNo { set; get; }

        public string Result { set; get; }

        public int Deliverystatus { set; get; }

        public Boolean CycleCountOn { set; get; }

        public string IsPostiveRecall { set; get; }

        public string Location { set; get; }

        public string AvailQty { set; get; }

        public decimal QtyinBUoM { set; get; }


        public string Msps { set; get; }

        public decimal OBDQty { set; get; }

        public int LocationId { set; get; }

        public string AssignedQuantity { set; get; }

        public string PickedQty { set; get; }

        public string SODetailsID { set; get; }

        public int GoodsmomentDeatilsId { set; get; }


        public string PendingQty { set; get; }

        public int AccountId { set; get; }

        public decimal TotalPickedQty { set; get; }

        public string SkipReason { set; get; }

        public decimal SkipQty { set; get; }

        public int Flag { set; get; }

        public int Assignedid { set; get; }

        public string ToCartonNo { set; get; }

        public string Erocode { set; get; }


        public string MRP { set; get; }


        public int HUSize { set; get; }


        public int HUNo { set; get; }


        public int IsPSN { set; get; }


        public string PSN { get; set; }

        public string CustomerName { get; set; }

        public string DockLocation { get; set; }


        public int RID { get; set; }

        public int FetchNextItem { get; set; }
    }
}
