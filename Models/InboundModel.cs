using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simpolo_Endpoint.Models
{
    public class InboundTracking_ShipmentTransitModel
    {
        public string StoreRefNo { get; set; }
        public string WarehouseId { get; set; }
        public string Tenant { get; set; }
        public int TenantID { get; set; }
        public int AccountID { get; set; }
    }

    public class GetInboundDetailsModel
    {
        public int TenantID { get; set; }
        public int AccountID { get; set; }
        public int InboundID { get; set; }
    }
    public class InboundTracking_ShipmentExpectedModel
    {
        public string StoreRefNo { get; set; }
        public string WarehouseId { get; set; }
        public string Tenant { get; set; }
        public int TenantID { get; set; }
        public int AccountID { get; set; }
        public int InbPanelId { get; set; }
    }

    public class InboundTracking_ShipmentInProcessModel
    {
        public string StoreRefNo { get; set; }
        public string WarehouseId { get; set; }
        public string Tenant { get; set; }
        public int TenantID { get; set; }
        public int AccountID { get; set; }
    }

    public class GetRevertInboundListModel
    {
        public string StoreRefNo { get; set; }
        public int UserID { get; set; }
        public int TenantID { get; set; }
        public int AccountID { get; set; }
    }

    public class GetInBoundPOInvoiceDetailsModel
    {
        public int TenantID { get; set; }
        public int InboundID { get; set; }
    }

    public class AddOrderOrInvoiceItemsModel
    {
        public int InboundID { get; set; }
        public int SupplierID { get; set; }
        public int TenantID { get; set; }
    }
    public class UpsertInboundBasicDataModel
    {
        public string StoreRefNo { get; set; }
        public string DocReceivedDate { get; set; }
        public int ShipmentTypeID { get; set; }
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public string? IsChargesRequired { get; set; }
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public int SupplierID { get; set; }
        public string ConsignmentNoteTypeID { get; set; }
        public string ConsignmentNoteTypeValue { get; set; }
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public string? ConsignmentNoteTypeDate { get; set; }
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public string NoofPackagesInDocument { get; set; }
        public string GrossWeight { get; set; }
        public string CBM { get; set; }
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public string? ClearanceCompanyID { get; set; }
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public string ClearanceInvoiceNo { get; set; }
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public string? ClearanceInvoiceDate { get; set; }
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public string ClearanceAmount { get; set; }
        public string FreightCompanyID { get; set; }
        public string FreightInvoiceNo { get; set; }
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public string? FreightInvoiceDate { get; set; }
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public string FreightAmount { get; set; }
        public string PriorityLevel { get; set; }
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public string? PriorityDateTime { get; set; }
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
        public string RemarksBy_Ini { get; set; }
        public int CreatedBy { get; set; }
        //public string UpdatedOn { get; set; }
        public int UpdatedBy { get; set; }
        public int AccountID { get; set; }
        public int TenantID { get; set; }
        public string WarehouseIDs { get; set; }
        public int InboundID { get; set; }
        public int InboundStatusID { get; set; }
        public string fileName { get; set; }
        public string bitecode { get; set; }
        public int LoginAccountId { get; set; }
        public int LoginTanentId { get; set; }
        public int LoginUserId { get; set; }
        public int AppSSOAccountID { get; set; }
    }

    public class UpsertInBoundPOInvoiceDetailsModel
    {
        public int Inbound_SupplierInvoiceID { get; set; }
        public int InboundID { get; set; }
        public int SupplierInvoiceID { get; set; }
        public int UserID { get; set; }
        public int POHeaderID { get; set; }
    }

    public class GetASNDetailsModel
    {
        public int InboundID { get; set; }
    }

    public class GetSearchInboundDetailsModel
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int TenantID { get; set; }
        public int AccountID { get; set; }
        public int UserID { get; set; }
        public int ShipmentTypeID { get; set; }
        public int ClearenceCompanyID { get; set; }
        public int ShipmentStatusID { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int WarehouseId { get; set; }
        public string SearchText { get; set; }
        public int SearchField { get; set; }
        public decimal Quantity { get; set; }
        public decimal GrossWeight { get; set; }
        public int OrderTypeID { get; set; }
    }

    public class GetRTRDetailsModel
    {
        public int InboundID { get; set; }
        public string MCode { get; set; }
    }

    public class GetGoodsInSuggestedPutAwayListModel
    {
        public int InboundID { get; set; }
        public int POHeaderID { get; set; }
        public int LineNumber { get; set; }
        public int MaterialMasterID { get; set; }
        public int SupplierInvoiceID { get; set; }
        public int SupplierInvoiceDetailsID { get; set; }
        public int AccountID { get; set; }
    }

    public class Get_ReceiveMSPsPutawayListModel
    {
        public int InboundID { get; set; }
        public int POHeaderID { get; set; }
        public int LineNumber { get; set; }
        public int MaterialMasterID { get; set; }
        public int SupplierInvoiceID { get; set; }
        public int SupplierInvoiceDetailsID { get; set; }
    }

    public class ReceiveMSPsPutawayListModel
    {
        public int TransactionID { get; set; }
        public string LineNumber { get; set; }
        public string MCode { get; set; }
        public int POHeaderID { get; set; }
        public decimal DocQty { get; set; }
        public decimal Quantity { get; set; }
        public string MfgDate { get; set; }
        public string ExpDate { get; set; }
        public string SerialNo { get; set; }
        public string BatchNo { get; set; }
        public string ProjectRefNo { get; set; }
        public string StorageLocation { get; set; }
        public string Location { get; set; }
        public string CartonCode { get; set; }
        public int GoodsMovementTypeID { get; set; }
        public int UserID { get; set; }
        public int SupplierInvoiceID { get; set; }
        public int IsRequestFromPC { get; set; }
        public int MRP { get; set; }
        public int SuggestedPutawayID { get; set; }
    }

    public class DeleteGoodsInRecieveddetailsModel
    {
        public int UserID { get; set; }
        public List<items> items { get; set; }
    }


    public class items
    {
        public int LocationID { get; set; }
        public string Location { get; set; }
        public string CartonCode { get; set; }
        public int POSODetailsID { get; set; }
        public int CapturingQc { get; set; }
        public int GoodsMovementDetailsID { get; set; }
        public int QCReq { get; set; }
        public int Isdisplay { get; set; }
        public string MfgDate { get; set; }
        public string ExpDate { get; set; }
        public string BatchNo { get; set; }
        public string SerialNo { get; set; }
        public int IsActive { get; set; }
        public string StorageLocations { get; set; }
        public bool IsSelected { get; set; }
        public int QCStatus { get; set; }
        public int MaterialTransactionID { get; set; }
        public decimal Quantity { get; set; }
        public int MaterialMasterID { get; set; }
        public string ProjectRefNo { get; set; }
    }

    public class UpdateASNDetailsModel
    {
        public int SupplierInvoiceDetailsID { get; set; }
        public decimal InvoiceQuantity { get; set; }
        public int UserID { get; set; }
        public int StorageLocationID { get; set; }
    }

    public class UpdateShipmentExpectedDetailsModel
    {
        public int InboundID { get; set; }
        public string ShipmentExpectedDate { get; set; }
    }

    public class GetReceivingDockManagementDetailsModel
    {
        public int InboundID { get; set; }
        public int TransactionType { get; set; }
        public string ActionType { get; set; }
    }
    public class UpsertReceivingDockManagementModel
    {
        public int InboundID { get; set; }
        public int DockID { get; set; }
        public int InboundDockID { get; set; }
        public string VehicleRegNo { get; set; }
        public string DriverName { get; set; }
        public int UserID { get; set; }
        public string RecievingStatus { get; set; }
        public string DriverContactNo { get; set; }
        public string VehicleWeight { get; set; }

    }

    public class DeleteReceivingDockManagementDetailsModel
    {
        public int InboundDockID { get; set; }
        public int UserID { get; set; }
    }

    public class Get_ShipmentReceivedDetailsModel
    {
        public int InboundID { get; set; }

        public int UserID { get; set; }
    }

    public class GetGRNUpdateDetailsModel
    {
        public int InboundID { get; set; }
    }

    public class PGRUnblockModel
    {
        public int InboundID { get; set; }
        public int UserID { get; set; }
    }

    public class ShipmentReceivedDetailsModel
    {
        public int InboundID { get; set; }
        public int IB_RefWarehouse_DetailsID { get; set; }
        public int UserID { get; set; }
        public int InboundTracking_WarehouseID { get; set; }
        public string ShipmentReceivedOn { get; set; }
        public string Offloadtime { get; set; }
        public int HasDiscrepancy { get; set; }

        public int INBTypeID { get; set; }

    }

    public class FetchGRNDataForInboundModel
    {
        public int InboundID { get; set; }
        public int SupplierInvoiceID { get; set; }
        public int POHeaderID { get; set; }
    }

    public class CheckIsShortGRNModel
    {
        public int InboundId { get; set; }
        public string PONumber { get; set; }
        public string Remarks { get; set; }
        public int InboundType { get; set; }
        public int flag { get; set; }
        public string InvoiceNumber { get; set; }


    }

    public class GetDiscrepancyDetailsModel
    {
        public int InboundID { get; set; }
        public int WarehouseId { get; set; }
    }


    //public class DeleteGRNHeaderModel
    //{
    //    public int GRNUpdateID { get; set; }
    //}

    public class GetDiscrepancyLineItems_PageLoadModel
    {
        public int InboundID { get; set; }
        //public int POHeaderID { get; set; }
    }


    public class SaveDiscrepancyDetailsModel
    {
        public int InboundID { get; set; }
        public string Disc_Remarks { get; set; }
        public string Disc_CheckedBy { get; set; }
        public string Disc_CheckedDate { get; set; }
        public string Disc_VerifiedBy { get; set; }
        public string Disc_VerifiedDate { get; set; }
    }


    public class DeleteDiscrepancyDetailsModel
    {
        public int DiscrepancyID { get; set; }
    }

    public class CheckDiscrepency_OnPageLoadModel
    {
        public int InboundID { get; set; }
    }
    public class UpsertDiscrepancyDetailsModel
    {
        public int DiscrepancyID { get; set; }
        //public int IB_RefWarehouse_DetailsID { get; set; }
        public int MaterialMasterID { get; set; }
        public int SupplierInvoiceID { get; set; }
        public int LineNumber { get; set; }
        public decimal ReceivedQuantity { get; set; }
        public string DiscrepancyDescription { get; set; }
        public int UserID { get; set; }
        public int InboundID { get; set; }
        public string WarehouseId { get; set; }
        public string PONumber { get; set; }
        public int TenantID { get; set; }
        public int SupplierID { get; set; }
        public int POHeaderID { get; set; }
        public string InvoiceNumber { get; set; }
        public string MCode { get; set; }
    }
    public class UpdateShipmentVerificationDetailsModel
    {
        public int InboundID { get; set; }
        public string WarehouseId { get; set; }
        public int SupplierInvoiceID { get; set; }
        public string Remarks { get; set; }
        public int ShipmentTypeID { get; set; }
        public int UserID { get; set; }
    }

    public class GetShipmentVerificationDetailsModel
    {
        public int InboundID { get; set; }
    }

    public class RevertGRNDetailsModel
    {
        public string GRNHeaderIDs { get; set; }
        public int UserID { get; set; }
        public int InboundID { get; set; }
    }

    public class ShipmentCloseDetailsModel
    {
        public int InboundID { get; set; }
        public int IB_RefWarehouse_DetailsID { get; set; }
    }

    public class GetInboundRevertDetailsModel
    {
        public int UserID { get; set; }
        public int TenantID { get; set; }
        public int AccountID { get; set; }
        public string StoreRefNo { get; set; }
    }
    public class GetRevertGRNDetailsModel
    {
        public int InboundID { get; set; }
    }

    public class RevertShipmmentExpectedModel
    {
        public int vInboundTrackingID { get; set; }

        public int UserID { get; set; }

        public int RefWHID { get; set; }
    }
    public class TableRow
    {
        public string S { get; set; }
    }
    public class RevertShipmmentReceivedModel
    {
        public int vInboundTrackingID { get; set; }

        public int UserID { get; set; }
        public int AccountID { get; set; }

        public int TenantID { get; set; }
        public int RefWHID { get; set; }

    }
    public class CreateGRNEntryAndPostDatatoSAPModel
    {
        public List<GRNDetails> GRNdetails { get; set; }
        public int InboundID { get; set; }
        public int POHeaderID { get; set; }
        public int SupplierInvoiceID { get; set; }
        public int UserID { get; set; }
        //public int IsShortSTO { get; set; }
        public int GRNHeaderID { get; set; }
        public string Remarks { get; set; }

        public string PONumber { get; set; }

        public int InboundType { get; set; }
        public int flag { get; set; }
        public string InvoiceNumber { get; set; }
    }

    public class GRNDetails
    {
        public string PONumber { get; set; }
        public string InvoiceNumber { get; set; }
        public string MCode { get; set; }
        public string MDescription { get; set; }
        public decimal Quantity { get; set; }
        public int MaterialTransactionID { get; set; }
        public string ReceivedDate { get; set; }
        public string VehicleNumber { get; set; }
        public string result { get; set; }
        public string errorcode { get; set; }
        public string PoType { get; set; }
        public string Xml { get; set; }
        public int GRNHeaderID { get; set; }
        public string ISError { get; set; }
    }

    public class RTR_PrintLabelModel
    {
        public List<RTR_LabelPrint> RTR_labelprint { get; set; }
        public RTRSecondLabelPrint RtrSecondLabelPrint { get; set; }
        public string LabelID { get; set; }
        public string ipaddress { get; set; }
        public int WarehouseID { get; set; }
        public int PrinterType { get; set; }
        public int port { get; set; }
        public int IsSecondaryLabelprint { get; set; }//Added By Ramsai

    }

    public class RTR_LabelPrint
    {
        public string MfgDate { get; set; }
        public string ExpDate { get; set; }
        public string SerialNo { get; set; }
        public string BatchNo { get; set; }
        public string ProjectRefNo { get; set; }
        public string SupplierLot { get; set; }
        public string MCode { get; set; }
        public string KitCode { get; set; }
        public string PrintQty { get; set; }
        public string MRP { get; set; }
        public string LineNo { get; set; }
        public string HUNo { get; set; }
        public string HUSize { get; set; }

        public string Grade { get; set; }

        public string BoxSerialNo { get; set; }
        public string Size { get; set; }

    }

    public class Print_RTRMLabelModel
    {
        public string Zone { get; set; }
        public string PrintQty { get; set; }
        public string Duplicateprints { get; set; }
        public string Location { get; set; }
        public string ProjectNo { get; set; }
        public string SupplierLot { get; set; }
        public string MCode { get; set; }
        public string KitCode { get; set; }
        public string Description { get; set; }
        public string BatchNo { get; set; }
        public string SerialNo { get; set; }
        public int KitPlannerID { get; set; }
        public string MfgDate { get; set; }
        public string ExpDate { get; set; }
        public string HUNo { get; set; }
        public string HUSize { get; set; }
        public string GRNDate { get; set; }
        public string PrinterIP { get; set; }
        public bool IsBoxLabelReq { get; set; }
        public int Dpi { get; set; }
        public string Length { get; set; }
        public string Width { get; set; }
        public string LabelType { get; set; }
        public string Lineno { get; set; }
        public string Mrp { get; set; }
        public int WarehouseID { get; set; }
        public string Grade { get; set; }

        public string BoxSerialNo { get; set; }
        public string Size { get; set; }
        public string DesignName { get; set; }
        public string Matt { get; set; }
        public string BoxQty { get; set; }
        public string LineNo { get; set; }
        public string ShiftTime { get; set; }
        public string SorterId { get; set; }
        public string Wapis { get; set; }
        public string Glazed { get; set; }
        public string UnPolished { get; set; }
        public string Rectified { get; set; }
        public int IsSecondaryLabelprint { get; set; }//Added By Ramsai
    }
    public class RTRSecondLabelPrint
    {
        public string DesignName { get; set; }
        public string Series { get; set; }
        public string BoxQty { get; set; }
        public string LineNumber { get; set; }
        public string Shift { get; set; }
        public string SorterID { get; set; }
        public string WAPIS { get; set; }
        public string Glazed { get; set; }
        public string UnPolished { get; set; }
        public string Rectified { get; set; }
    } 


}
