using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace Simpolo_Endpoint.Entities.BO
{
    [DataContract]
    public class GroupOBD
    {
        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public int MaterialMasterID { get; set; }

        [DataMember]
        public string Mcode { get; set; }

        [DataMember]
        public decimal SOQty { get; set; }

        [DataMember]
        public decimal PackedQty { get; set; }

        [DataMember]
        public int OutboundID { get; set; }

        [DataMember]
        public string CustomerName { get; set; }

        [DataMember]
        public int SoDetailsID { get; set; }

        [DataMember]
        public string MFGDate { get; set; }

        [DataMember]
        public string EXPDate { get; set; }

        [DataMember]
        public string SerialNo { get; set; }

        [DataMember]
        public string ProjectRefNo { get; set; }

        [DataMember]
        public string MRP { get; set; }

        [DataMember]
        public string BatchNo { get; set; }

        [DataMember]
        public string BusinessType { get; set; }

        [DataMember]
        public int PSNID { get; set; }

        [DataMember]
        public decimal PickedQty { get; set; }

        [DataMember]
        public string CartonSerialNo { get; set; }

        [DataMember]
        public int PSNDetailsID { get; set; }

        [DataMember]
        public string OBDNumber { get; set; }

        [DataMember]
        public string PackType { get; set; }

        [DataMember]
        public int SOHeaderID { get; set; }

        [DataMember]
        public string PackComplete { get; set; }

        [DataMember]
        public string SONumber { get; set; }

        [DataMember]
        public string DriverName { get; set; }

        [DataMember]
        public string DriverNo { get; set; }

        [DataMember]
        public string LRnumber { get; set; }

        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public string TenantId { get; set; }

        [DataMember]
        public string Vehicle { get; set; }

        [DataMember]
        public List<string> LstSalesOrders { get; set; }

        [DataMember]
        public string LoadRefNo { get; set; }

        [DataMember]
        public int TotalSOCount { get; set; }

        [DataMember]
        public int ScannedSOCount { get; set; }

        [DataMember]
        public string CustomerCode { get; set; }

        [DataMember]
        public string CustomerAddress { get; set; }

        [DataMember]
        public string WareHouseID { get; set; }

        [DataMember]
        public string Warehouseid { get; set; }

        [DataMember]
        public int AccountID { get; set; }

        [DataMember]
        public string HUNo { get; set; }

        [DataMember]
        public string HUSize { get; set; }

        [DataMember]
        public string IsPicking { get; set; }

        [DataMember]
        public string UOM { get; set; }

        [DataMember]
        public int Vlpdid { get; set; }

        [DataMember]
        public string Vlpdnumber { get; set; }

        [DataMember]
        public string IsCustomLabel { get; set; }

        [DataMember]
        public string dock { get; set; }

        [DataMember]
        public string ZplScript { get; set; }

        [DataMember]
        public int NoOfPrints { get; set; }
    }
}
