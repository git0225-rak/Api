using System;
using System.Collections.Generic;
using System.Text;

namespace FWMSC21Core.Entities
{
    public class OutboundDetails
    {
        private int _MaterialMasterID;
        private string _Mcode;
        private decimal _Quantity;
        private int _AccountID;
        private int _OutboundID;
        private string _Location;
        private string _MFGDate;
        private string _EXPDate;
        private string _SerialNo;
        private string _ProjectRefNo;
        private string _BatchNo;
        private int _UserId;
        private string _CartonNo;
        private string _Result;
        private decimal _PendingQty;
        private decimal _RevertQty;
        private decimal _Qty;
        private int _VLPDPickID;

        public string Mcode { get => _Mcode; set => _Mcode = value; }
        public int MaterialMasterID { get => _MaterialMasterID; set => _MaterialMasterID = value; }
        public decimal Quantity { get => _Quantity; set => _Quantity = value; }
        public int AccountID { get => _AccountID; set => _AccountID = value; }
        public int OutboundID { get => _OutboundID; set => _OutboundID = value; }
        public string Location { get => _Location; set => _Location = value; }
        public string MFGDate { get => _MFGDate; set => _MFGDate = value; }
        public string EXPDate { get => _EXPDate; set => _EXPDate = value; }
        public string SerialNo { get => _SerialNo; set => _SerialNo = value; }
        public string ProjectRefNo { get => _ProjectRefNo; set => _ProjectRefNo = value; }
        public string BatchNo { get => _BatchNo; set => _BatchNo = value; }
        public int UserId { get => _UserId; set => _UserId = value; }
        public string CartonNo { get => _CartonNo; set => _CartonNo = value; }
        public string Result { get => _Result; set => _Result = value; }
        public decimal PendingQty { get => _PendingQty; set => _PendingQty = value; }
        public decimal RevertQty { get => _RevertQty; set => _RevertQty = value; }
        public decimal Qty { get => _Qty; set => _Qty = value; }
        public int VLPDPickID { get => _VLPDPickID; set => _VLPDPickID = value; }
    }
}
