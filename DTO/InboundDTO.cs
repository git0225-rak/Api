using System;
using System.Collections.Generic;
using System.Linq;    
using System.Web;

namespace Simpolo_Endpoint.DTO
{
    public class InboundDTO
    {
        private string _InboundID;
        private string _StoreRefNo;
      
        private string _PalletNo;
      
        private string _UserId;
    
        private string _AccountID;
        private string _StorageLocation;
        private string _Result;
        private string _Mcode;
        private string _ReceivedQty;
        private string _ItemPendingQty;
        private string _BatchNo;
        private string _SerialNo;
        private string _MfgDate;
        private string _ExpDate;
        private string _ProjectRefno;
        private string _Qty;
        private string _LineNo;
        private string _IsDam;
        private string _HasDisc;
        private string _CreatedBy;
        private string _CartonNo;
        private string _SkipType;
        private string _SkipReason;
        private string _InvoiceQty;
        private string _IsOutbound;
        private string _MRP;
        private string _Dock;
        private string _VehicleNo;
        private List<EntryDTO> _Entry;
        private string _SupplierInvoiceDetailsID;
        private string _HUNo;
        private string _HUSize;
        private int _POType;
        private string _warehouseID;
        private string _tenantID;
        private string _Lineno;
        private string _MCode;
        private string _SLoc;
        private string _ProjectNo;
        private string _SupplierInvoiceID;
        private string _POSOHeaderId;
        private string _Storerefno;
        private int _TotalPalletNo;
        private int _palletcount;
        private decimal _volume;
        private decimal _weight;
        private string _Grade;
        private string _BoxSerialNo;
        private int _IsStockAdjust;
        private string actualQty;
        private string adjustQty;
        private string _IsStockAdd;
        private int _IsPhysicalEmpty;
        private int _DockId;
        private int _LoadingPointID;

        public string SupplierInvoiceDetailsID { get => _SupplierInvoiceDetailsID; set => _SupplierInvoiceDetailsID = value; }
        public string UserId { get => _UserId; set => _UserId = value;}
      
        public string InboundID { get => _InboundID; set => _InboundID = value; }
        public string StoreRefNo { get => _StoreRefNo; set => _StoreRefNo = value; }
        public string Storerefno { get => _Storerefno; set => _Storerefno = value; }
        public string PalletNo { get => _PalletNo; set => _PalletNo = value; }
    
        public string AccountID { get => _AccountID; set => _AccountID = value; }
        public string StorageLocation { get => _StorageLocation; set => _StorageLocation = value; }
        public string Result { get => _Result; set => _Result = value; }
        public string Mcode { get => _Mcode; set => _Mcode = value; }
        public string MCode { get => _MCode; set => _MCode = value; }
        public string ReceivedQty { get => _ReceivedQty; set => _ReceivedQty = value; }
        public string ItemPendingQty { get => _ItemPendingQty; set => _ItemPendingQty = value; }
        public string BatchNo { get => _BatchNo; set => _BatchNo = value; }
        public string SerialNo { get => _SerialNo; set => _SerialNo = value; }
        public string MfgDate { get => _MfgDate; set => _MfgDate = value; }
        public string ExpDate { get => _ExpDate; set => _ExpDate = value; }
        public string ProjectRefno { get => _ProjectRefno; set => _ProjectRefno = value; }
        public string Qty { get => _Qty; set => _Qty = value; }
        public string LineNo { get => _LineNo; set => _LineNo = value; }
        public string Lineno { get => _Lineno; set => _Lineno = value; }
        public string HasDisc { get => _HasDisc; set => _HasDisc = value; }
        public string CartonNo { get => _CartonNo; set => _CartonNo = value; }
        public string CreatedBy { get => _CreatedBy; set => _CreatedBy = value; }
        public string IsDam { get => _IsDam; set => _IsDam = value; }
        public string SkipType { get => _SkipType; set => _SkipType = value; }
        public string SkipReason { get => _SkipReason; set => _SkipReason = value; }
        public string InvoiceQty { get => _InvoiceQty; set => _InvoiceQty = value; }
        public string IsOutbound { get => _IsOutbound; set => _IsOutbound = value; }
        public string MRP { get => _MRP; set => _MRP = value; }
        public string Dock { get => _Dock; set => _Dock = value; }
        public List<EntryDTO> Entry { get => _Entry; set => _Entry = value; }
        public string VehicleNo { get => _VehicleNo; set => _VehicleNo = value; }
        public string HUNo { get => _HUNo; set => _HUNo = value; }
        public string HUSize { get => _HUSize; set => _HUSize = value; }
        public int POType { get => _POType; set => _POType = value; }
        public string WarehouseID { get => _warehouseID; set => _warehouseID = value; }
        public string TenantID { get => _tenantID; set => _tenantID = value; }
        public string SLoc { get => _SLoc; set => _SLoc = value; }
        public string ProjectNo { get => _ProjectNo; set => _ProjectNo = value; }
        public string SupplierInvoiceID { get => _SupplierInvoiceID; set => _SupplierInvoiceID = value; }
        public string POSOHeaderId { get => _POSOHeaderId; set => _POSOHeaderId = value; }
     
        public int TotalPalletNo { get => _TotalPalletNo; set => _TotalPalletNo = value; }
        public int Palletcount { get => _palletcount; set => _palletcount = value; }
        public decimal Volume { get => _volume; set => _volume = value; }
        public decimal Weight { get => _weight; set => _weight = value; }

        public string Grade { get => _Grade; set => _Grade = value; }

        public string BoxSerialNo { get => _BoxSerialNo; set => _BoxSerialNo = value; }
        public int IsStockAdjust { get => _IsStockAdjust; set => _IsStockAdjust = value; }
        public string ActualQty { get => actualQty; set => actualQty = value; }
        public string AdjustQty { get => adjustQty; set => adjustQty = value; }
        public string IsStockAdd { get => _IsStockAdd; set => _IsStockAdd = value; }

        public int TransactionId { get; set; }
        public int VehicleTypeID { get; set; }
        public int TransactionType { get; set; }
        public int ReceivingStatus { get; set; }
        public string VehiclePreLoadWeight { get; set; }
        public string VehiclePostLoadWeight { get; set; }
        public int VehicleTypeId { get; set; }
        public string VehicleType { get; set; }
        public string DriverNumber { get; set; }
        public string ActionType { get; set; }
        public string VehicleNumber { get; set; }
        public string Message { get; set; }
        public int IsProductionOrder { get; set; }
        public int ReasonId { get; set; }
        public int IsPhysicalEmpty { get => _IsPhysicalEmpty; set => _IsPhysicalEmpty = value; }
        public int DockId { get => _DockId; set => _DockId = value; }
        public int LoadingPointID { get => _LoadingPointID; set => _LoadingPointID = value; }
    }
}