using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Simpolo_Endpoint.Models
{
    public class GetPendingOBDForVLPDCreationModel
    {
        public string OBDNumber { get; set; }
        public string WarehouseId { get; set; }
        public string Tenant { get; set; }
        public int TenantID { get; set; }
        public int AccountID { get; set; }
        public int UserID { get; set; }
    }

    public class GetPick_CheckPendingListModel
    {
        public string OBDNumber { get; set; }
        public string WarehouseId { get; set; }
        public string Tenant { get; set; }
        public int TenantID { get; set; }
        public int AccountID { get; set; }
        public int UserID { get; set; }
    }


    public class GetPGIPendingListModel
    {
        public string OBDNumber { get; set; }
        public string WarehouseId { get; set; }
        public string Tenant { get; set; }
        public int TenantID { get; set; }
        public int AccountID { get; set; }
        public int UserID { get; set; }
    }

    public class GetDeliveriesPendingListModel
    {
        public string OBDNumber { get; set; }
        public string WarehouseId { get; set; }
        public string Tenant { get; set; }
        public int TenantID { get; set; }
        public int AccountID { get; set; }
        public int UserID { get; set; }
    }

    public class GetPODPendingListModel
    {
        public string OBDNumber { get; set; }
        public string WarehouseId { get; set; }
        public string Tenant { get; set; }
        public int TenantID { get; set; }
        public int AccountID { get; set; }
        public int UserID { get; set; }
    }

    public class GetOBDRevertListModel
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string SearchText { get; set; }
        public int WarehouseId { get; set; }
        public int NoofRecords { get; set; }
        public int TenantID { get; set; }
        public int PageNo { get; set; }
        public int CategoryID { get; set; }
        public int StatusId { get; set; }
    }

    public class GetOBDReleaseListModel
    {
        public string OBDNumber { get; set; }
        public int AccountID_New { get; set; }
        public int TenantID_New { get; set; }
        public int UserID_New { get; set; }
        public int UserTypeID_New { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TenantID { get; set; }


    }
    public class GetSOsListModel
    {
        public int TenantID { get; set; }
        public int UserID { get; set; }
    }
    public class GetSearchOutboundDetailsModel
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public int tenantid { get; set; }
        public int Warehouseid { get; set; }
        public int DocumentTypeID { get; set; }
        public int DeliveryStatusID { get; set; }
        public string AWBNo { get; set; }
        public string SearchText { get; set; }
        public int SearchField { get; set; }
        public string DueDate { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int AccountID_New { get; set; }
        public int UserTypeID_New { get; set; }
        public int TenantID_New { get; set; }
        public int UserID_New { get; set; }
        public string DivisionIDs { get; set; }

        public string VehicleNo { get; set; }

    }
    public class saveBulkReleaseItemsForOBDModel
    {
        public int OutboundID { get; set; }
        public int UserID { get; set; }
        public int DockID { get; set; }
        public List<data> items { get; set; }


    }

    public class data
    {
        public int SODetailsID { get; set; }
        public decimal DeliveryQty { get; set; }
        public string BatchNo { get; set; }
    }
    public class GetOBDwiseItemModel
    {
        public string OBDNumber { get; set; }
        public int AccountID { get; set; }

    }
    public class SetOBDRevertModel
    {
        public string OBDNumber { get; set; }
        public int DeliveryTypeID { get; set; }
        public int DeliveryStatusID { get; set; }
        public int OutboundID { get; set; }
        public int UserID { get; set; }
    }
    public class UpsertUpdateDeliveryModel
    {
        public int AccountID { get; set; }
        public int OutboundID { get; set; }
        public string OBDNumber { get; set; }
        public int DocumentTypeID { get; set; }
        public string OBDDate { get; set; }
        public int CustomerID { get; set; }
        //public int RequestedBy { get; set; }
        public int DepartmentID { get; set; }
        public int DivisionID { get; set; }
        public string RemByIni_OnCreation { get; set; }
        //public int InitiatedBy { get; set; }
        public int DeliveryStatusID { get; set; }
        public int IsReservationDelivery { get; set; }
        public int PriorityLevel { get; set; }
        public string PriorityDateTime { get; set; }
        //public int LastModifiedBy { get; set; }
        public int IsDNPublished { get; set; }
        public int TenantID { get; set; }
        public string WarehouseIDs { get; set; }
        public int UserID { get; set; }
    }
    public class UpsertOBDInputModel
    {
        public string SoNumbers { get; set; }
        public int AccountID { get; set; }
        public int TenantId { get; set; }
        public int WareHouseId { get; set; }
        public int DeliveryTypeId { get; set; }
        public int CreatedBy { get; set; }
        public int PriorityTypeID { get; set; }
    }
    public class PickListInputModel
    {
        public int OutboundId { get; set; }
        public string MCode { get; set; }
    }
    public class PickedItemsInputModel
    {
        public int MaterialMasterID { get; set; }
        public string CartonCode { get; set; }
        public int AccountID { get; set; }
        public string OBD { get; set; }
        public string Location { get; set; }
        public string BatchNo { get; set; }
        public string SerialNo { get; set; }
        public string ProjectRefNo { get; set; }
        public string ExpDate { get; set; }
        public string MfgDate { get; set; }
    }
    public class InsertPickItemInputModel
    {
        public string OBDNumber { get; set; }
        public int SOHeaderID { get; set; }
        public int AssignedId { get; set; }
        public int SoDetailsIdnew { get; set; }
        public int AccountID { get; set; }
        public int LineNumber { get; set; }
        public string Location { get; set; }
        public string MCode { get; set; }
        public decimal Quantity { get; set; }
        public string Mfgdate { get; set; }
        public string ExpDate { get; set; }
        public string BatchNo { get; set; }
        public string SerialNo { get; set; }
        public int CreatedBy { get; set; }
        public string CartonCode { get; set; }
        public string ToCartonCode { get; set; }
        public string Projrefno { get; set; }
        public string MRP { get; set; }
    }
    public class DeletePickItemsinputModel
    {
        public int VLPDPickedID { get; set; }
        public int CreatedBy { get; set; }
    }
    public class GetSOLineItemsInputModel
    {
        public int OutboundID { get; set; }
    }
    public class UpdateShipmentDetailsModel
    {
        public int OutboundID { get; set; }
        //public string Result { get; set; }
    }
    public class GetPendingGoodsOutInputModel
    {
        public int OutboundID { get; set; }
    }



    public class UpsertPackingSlipAddMaterialInfo
    {
        public int PSNHeaderId { get; set; }
        public string Material { get; set; }
        public decimal PickedQty { get; set; }
        public decimal PackedQty { get; set; }
        public int CreatedBy { get; set; }
        public string PackedUOM { get; set; }
        public decimal Itemvolume { get; set; }
        public decimal ItemWeight { get; set; }
        public int LoginAccountId { get; set; }
        public int LoginTanentId { get; set; }
        public int LoginUserId { get; set; }
    }
    public class DeliveryPackslipModel
    {
        public int IDs { get; set; }
        public int PSDetailsID { get; set; }
        public int OutboundID { get; set; }
        public string prefix { get; set; }
        public int PSNHeaderId { get; set; }
        public int LoginAccountId { get; set; }
        public int LoginUserId { get; set; }
        public int LoginTanentId { get; set; }
    }
    public class UpertPackingSlipInputModel
    {
        public string OutboundStatus { get; set; }
        public int OutBoundId { get; set; }
        public int HandlingTypeId { get; set; }
        public int Maxweight { get; set; }
        public int Maxvolume { get; set; }
        public string Remarks { get; set; }
        public string PackageCondition { get; set; }
        public int CreatedBy { get; set; }
    }
    public class UpdatePackingSlipInformationModel
    {
        public int OutboundID { get; set; }
    }
    public class GetDeliveryNoteHeaderinputModel
    {
        public int OutboundID { get; set; }
        public string prefix { get; set; }
        public int AccountID { get; set; }
        public int USERID { get; set; }
    }
    public class GetInitiateOutboundDeliveryModel
    {
        public int OutboundID { get; set; }
        public int AccountID { get; set; }
    }
    public class UpsertDDLineItemsModel
    {
        public int OutboundID { get; set; }
        public int AccountID { get; set; }
        public string SONumber { get; set; }
        public string CustPONumber { get; set; }
        public string InvoiceNumber { get; set; }
        public int UserID { get; set; }
        public int TenantID { get; set; }
        public int CustomerPOID { get; set; }
    }
    public class GetPickCheckPickModel
    {
        public int OutboundID { get; set; }
        public int AccountID { get; set; }
    }
    public class UpsertUpdatePGIModel
    {
        public int OutboundID { get; set; }
        public int WarehouseID { get; set; }
        public int VehicleTypeID { get; set; }
        public int FreightID { get; set; }
        public int UserID { get; set; }
        public string VehicleNumber { get; set; }
        public int AccountID { get; set; }
        public int TenantID { get; set; }
        public int m_TransferedtoStoreID { get; set; }
        public int m_StoreInchargeID { get; set; }
        public int m_RefWHID { get; set; }
        public string m_OBDReceivedDT { get; set; }
        public decimal m_TotalQuantity { get; set; }
        public int m_NoofLines { get; set; }
        public int m_PickedBy { get; set; }
        public string m_RemByStoreIncharge { get; set; }
        public int m_CheckedBy { get; set; }
        public string m_SentForPGI_DT { get; set; }
        public int m_OBDTrack_WHID { get; set; }
        public string m_RemByIni_AfterPGI { get; set; }
        public int m_PGIDoneBy { get; set; }
        public string m_PGIDone_DT { get; set; }
        public int DocumentTypeID { get; set; }
        public int INVReportID { get; set; }
    }
    public class Response
    {
        public string Error { get; set; }
        public string Result { get; set; }
    }
    public class GetPackingSlipDataModel
    {
        public int OutboundID { get; set; }
        public int PackslipHeaderID { get;  set; }
    }
    public class GetRouteCodeModel
    {
        public int OutboundID { get; set; }
    }
    public class GetItemMasterLoadModel
    {
        public int TenantID { get; set; }
        public int AccountID { get; set; }
    }
    public class GetPackingSlipNumberDataModel
    {
        public int OutboundID { get; set; }
    }
    public class GetPickMaterialModel
    {
        public int OutboundID { get; set; }
    }
    public class UpdateDeliveryDetailsModel
    {
        public int OutboundID { get; set; }
        public int UserID { get; set; }
        public int TransferedToWarehouseID { get; set; }
        public int InstructionModeID { get; set; }
        public string Requester { get; set; }
        public string DocumentNumber { get; set; }
        public string DocumentReceivedDate { get; set; }
        public string DeliveryDate { get; set; }
        public int DeliveredBy { get; set; }
        public string ReceivedBy { get; set; }
        public string DriverName { get; set; }
        public string RemByDeliveryIncharge { get; set; }
        public int IsPODReceived { get; set; }
    }
    public class WOComponentIssue_GetListModel
    {
        public int OutboundID { get; set; }
    }

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
        public int RID { get; set; }
        public decimal TotalQty { get; set; }
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

    public class AddDelvDocLineItem_ClickModel
    {
        public int DeliveryStatusID { get; set; }
        public int OutboundID { get; set; }
    }

    public class Delete_DeliveryDocLineItemsModel
    {
        public int SOHeaderID { get; set; }
        public int DeliveryStatusID { get; set; }
        public int OutboundID { get; set; }
    }

    public class GeneratePGIInvoiceModel
    {
        public string OBDRefNUmber { get; set; }
        public int OutboundID { get; set; }
    }
    public class InvoiceData
    {
        public string Base64Data { get; set; }
        public string InvoiceNumber { get; set; }
        public bool IsError { get; set; }
    }

    public class SetOBDRevertNewModel
    {
        public int DeliveryStatusId { get; set; }
        public int OutboundId { get; set; }
        public string OBDNumber { get; set; }
        public int CreatedBy { get; set; }
        public int DeliveryStatusTypeID { get; set; }
        public string MCode { get; set; }
        public string WarehouseIDs { get; set; }
        public int LoginAccountId { get; set; }
        public int LoginTanentId { get; set; }
        public int LoginUserId { get; set; }
        public int outbound_customerpoid { get; set; }
        public decimal Quantity { get; set; }
        public int SODetailsID { get; set; }
        public int RevertTypeID { get; set; }
        public decimal RevertQty { get; set; }
        public int AssignedID { get;  set; }
    }

}
