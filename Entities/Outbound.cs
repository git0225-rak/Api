using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simpolo_Endpoint.Entities
{
    public class Outbound
    {
        private int _MaterialMasterID;
        private string _Mcode;
        private string _MCode;
        private decimal _SOQty;
        private decimal _PackedQty;
        private int _OutboundID;
        private string _CustomerName;
        private int _SoDetailsID;
        private string _MFGDate;
        private string _EXPDate;
        private string _SerialNo;
        private string _ProjectRefNo;
        private string _BatchNo;
        private string _MRP;
        private string _BusinessType;
        private int _PSNID;
        private decimal _PickedQty;
        private string _CartonSerialNo;
        private int _PSNDetailsID;
        private string _OBDNumber;
        private string _PackType;
        private int _SOHeaderID;
        private string _PackComplete;
        private string _SONumber;
        private string _DriverName;
        private string _DriverNo;
        private string _LRnumber;
        private int _UserId;
        private string _TenantId;
        private string _Vehicle;
        private List<string> _lstSalesOrders;
        private string _LoadRefNo;
        private int _TotalSOCount;
        private int _AccountID;
        private int _ScannedSOCount;
        private string _CustomerCode;
        private string _CustomerAddress;
#pragma warning disable CS0169 // The field 'Outbound._ErroMessage' is never used
        private string _ErroMessage;
#pragma warning restore CS0169 // The field 'Outbound._ErroMessage' is never used
        private string _WareHouseID;
        private string _Status;
        private string _HUNo;
        private string _HUSize;
        private int _Assignedid;
        private string _MDescription;
        private string _CartonNo;
        private int _CartonID;
        private string _Location;
        private int _LocationId;
        private decimal _AssignedQuantity;
        private int _SLocId;
        private string _SLoc;
        private int _Lineno;
        private int _MaterialMaster_IUoMID;
        private int _POSOHeaderId;
        private decimal _PendingQty;
        private int _IsPSN;
        private string _DockLocation;
        private int _isSample;
        private string _IsWorkOrder;
        private string _Result;
        private Decimal _Quantity;
        private Decimal _RevertQty;
        private int _VLPDPickID;

        public int FetchNextItem { get; set; }
        private string _IsLoading;
        private string _LoadComplete;
        private string _ScanInput;
        private Decimal _LoadQty;
        private string _Loadqty;
        private string _LabelSerialNo;
        private string _TotalQty;
        private string _kitID;
        private string _Grade;
        private string _lineNumber;
        private int _IsLoadComplete;
        private string _ActionType;
        private Decimal _UnLoadQty;
        private string _VehicleNumber;


        public string Status { get => _Status; set => _Status = value; }
        public int MaterialMasterID { get => _MaterialMasterID; set => _MaterialMasterID = value; }
        public string Mcode { get => _Mcode; set => _Mcode = value; }
        public decimal SOQty { get => _SOQty; set => _SOQty = value; }
        public decimal PackedQty { get => _PackedQty; set => _PackedQty = value; }
        public int OutboundID { get => _OutboundID; set => _OutboundID = value; }
        public string CustomerName { get => _CustomerName; set => _CustomerName = value; }
        public int SoDetailsID { get => _SoDetailsID; set => _SoDetailsID = value; }
        public string MFGDate { get => _MFGDate; set => _MFGDate = value; }
        public string EXPDate { get => _EXPDate; set => _EXPDate = value; }
        public string SerialNo { get => _SerialNo; set => _SerialNo = value; }
        public string ProjectRefNo { get => _ProjectRefNo; set => _ProjectRefNo = value; }
        public string MRP { get => _MRP; set => _MRP = value; }
        public string BatchNo { get => _BatchNo; set => _BatchNo = value; }
        public string BusinessType { get => _BusinessType; set => _BusinessType = value; }
        public int PSNID { get => _PSNID; set => _PSNID = value; }
        public decimal PickedQty { get => _PickedQty; set => _PickedQty = value; }
        public string CartonSerialNo { get => _CartonSerialNo; set => _CartonSerialNo = value; }
        public int PSNDetailsID { get => _PSNDetailsID; set => _PSNDetailsID = value; }
        public string OBDNumber { get => _OBDNumber; set => _OBDNumber = value; }
        public string PackType { get => _PackType; set => _PackType = value; }
        public int SOHeaderID { get => _SOHeaderID; set => _SOHeaderID = value; }
        public string PackComplete { get => _PackComplete; set => _PackComplete = value; }
        public string SONumber { get => _SONumber; set => _SONumber = value; }
        public string DriverName { get => _DriverName; set => _DriverName = value; }
        public string DriverNo { get => _DriverNo; set => _DriverNo = value; }
        public string LRnumber { get => _LRnumber; set => _LRnumber = value; }
        public int UserId { get => _UserId; set => _UserId = value; }
        public string TenantId { get => _TenantId; set => _TenantId = value; }
        public string Vehicle { get => _Vehicle; set => _Vehicle = value; }
        public List<string> LstSalesOrders { get => _lstSalesOrders; set => _lstSalesOrders = value; }
        public string LoadRefNo { get => _LoadRefNo; set => _LoadRefNo = value; }
        public int TotalSOCount { get => _TotalSOCount; set => _TotalSOCount = value; }
        public int ScannedSOCount { get => _ScannedSOCount; set => _ScannedSOCount = value; }
        public string CustomerCode { get => _CustomerCode; set => _CustomerCode = value; }
        public string CustomerAddress { get => _CustomerAddress; set => _CustomerAddress = value; }
        public string WareHouseID { get => _WareHouseID; set => _WareHouseID = value; }
        public int AccountID { get => _AccountID; set => _AccountID = value; }
        public string HUNo { get => _HUNo; set => _HUNo = value; }
        public string HUSize { get => _HUSize; set => _HUSize = value; }
        public int Assignedid { get => _Assignedid; set => _Assignedid = value; }
        public string MDescription { get => _MDescription; set => _MDescription = value; }
        public string CartonNo { get => _CartonNo; set => _CartonNo = value; }
        public int CartonID { get => _CartonID; set => _CartonID = value; }
        public string Location { get => _Location; set => _Location = value; }
        public int LocationId { get => _LocationId; set => _LocationId = value; }
        public decimal AssignedQuantity { get => _AssignedQuantity; set => _AssignedQuantity = value; }
        public int SLocId { get => _SLocId; set => _SLocId = value; }
        public string SLoc { get => _SLoc; set => _SLoc = value; }
        public int Lineno { get => _Lineno; set => _Lineno = value; }
        public int MaterialMaster_IUoMID { get => _MaterialMaster_IUoMID; set => _MaterialMaster_IUoMID = value; }
        public int POSOHeaderId { get => _POSOHeaderId; set => _POSOHeaderId = value; }
        public decimal PendingQty { get => _PendingQty; set => _PendingQty = value; }
        public int IsPSN { get => _IsPSN; set => _IsPSN = value; }
        public string DockLocation { get => _DockLocation; set => _DockLocation = value; }
        public int IsSample { get => _isSample; set => _isSample = value; }
        public string IsWorkOrder { get => _IsWorkOrder; set => _IsWorkOrder = value; }
        public string Result { get => _Result; set => _Result = value; }
        public decimal Quantity { get => _Quantity; set => _Quantity = value; }
        public decimal RevertQty { get => _RevertQty; set => _RevertQty = value; }
        public int VLPDPickID { get => _VLPDPickID; set => _VLPDPickID = value; }
        public string IsLoading { get => _IsLoading; set => _IsLoading = value; }
        public string LoadComplete { get => _LoadComplete; set => _LoadComplete = value; }
        public string ScanInput { get => _ScanInput; set => _ScanInput = value; }
        public Decimal LoadQty { get => _LoadQty; set => _LoadQty = value; }
        public string LabelSerialNo { get => _LabelSerialNo; set => _LabelSerialNo = value; }
        public string TotalQty { get => _TotalQty; set => _TotalQty = value; }
        public string KitID { get => _kitID; set => _kitID = value; }
        public string Grade { get => _Grade; set => _Grade = value; }
        public string LineNumber { get => _lineNumber; set => _lineNumber = value; }
        public string Loadqty { get => _Loadqty; set => _Loadqty = value; }
        public int IsLoadComplete { get => _IsLoadComplete; set => _IsLoadComplete = value; }
        public string ActionType { get => _ActionType; set => _ActionType = value; }
        public decimal UnLoadQty { get => _UnLoadQty; set => _UnLoadQty = value; }
        public string MCode { get => _MCode; set => _MCode = value; }
        public string VehicleNumber { get => _VehicleNumber; set => _VehicleNumber = value; }
    }
}
