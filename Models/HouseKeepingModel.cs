using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Simpolo_Endpoint.Models
{
    public class GetFastMovingLocsTransferListModel
    {
        public int TransferTypeId { get; set; }
        public int StatusId { get; set; }
        public int WarehouseID { get; set; }
        public string SAPReferenceNumber { get; set; }
        public int AccountID { get; set; }
        public int UserID { get; set; }
    }


    public class DeleteFastMovingLocsTransferModel
    {
        public int TransferRequestID { get; set; }
    }

    public class GetInternalTransferHeaderModel
    {
        public int TransferReqID { get; set; }

        public int IsApproved { get; set; }

        public int UserID { get; set; }
    }


    public class TransferRequestModel
    {
        public int TenantID { get; set; }
        public int WarehouseId { get; set; }
        public int TransferTypeID { get; set; }
        public string Remarks { get; set; }
        public int IsSuggestedReg { get; set; }
        public int UserID { get; set; }
    }




    public class UpsertInternalTransferHeaderModel
    {
        public int TenantID { get; set; }
        public int WarehouseId { get; set; }
        public int TransferTypeID { get; set; }
        public string Remarks { get; set; }
        public int IsSuggestedReg { get; set; }
        public int UserID { get; set; }
    }

    public class GetInternalTransferDetailsModel
    {
        public int TransferRequestID { get; set; }

    }
    public class UpsertInternalTransferDetailsModel
    {
        public int TransferRequestID { get; set; }
        public string ProjectRefNo { get; set; }
        public int MaterialMasterID { get; set; }
        public string BatchNo { get; set; }

        public string TOBatchNo { get; set; }
        public int FromSLID { get; set; }
        public int ToSL { get; set; }
        public decimal Quantity { get; set; }
        public int cartonID { get; set; }
        public int LocationID { get; set; }
        public int TOMMID { get; set; }
        public int ToLocationID { get; set; }

        public string FromGrade { get; set; }

        public string ToGrade { get; set; }

        public int UserID { get; set; }

        public int IsProdOrder { get; set; }

        public int ToCartonID { get; set; }



    }

    public class UpsertExpiryDateTransferRequestDetailsModel
    {
        public int TransferRequestID { get; set; }
        public string ProjectRefNo { get; set; }
        public int MaterialMasterID { get; set; }
        public string BatchNo { get; set; }
        public int FromSLID { get; set; }
        public string ToExpDate { get; set; }
        public string FromExpDate { get; set; }
    }
    public class GetCycleCountListModel
    {
        public int AccountID { get; set; }
        public int UserID { get; set; }
        public int ZoneID { get; set; }
        public int CCM_CNF_AccountCycleCount_ID { get; set; }
    }
    public class UpsertCycleCountHeaderModel
    {
        public XML XML { get; set; }
        public string LanguageType { get; set; }
        public int UpdatedBy { get; set; }
        public int CCM_CNF_AccountCycleCount_ID { get; set; }
        public int TenantId { get; set; }

    }

    public class XML
    {
        public int AM_MST_Account_ID { get; set; }
        public int WarehouseID { get; set; }
        public int ZoneId { get; set; }
        public int TenantId { get; set; }
        public int CCM_MST_CycleCount_ID { get; set; }
        public string AccountCycleCountName { get; set; }
        public int CCM_MST_Freequency_ID { get; set; }
        public string CycleCountDuration { get; set; }
        public int IsDeleted { get; set; }
        public string Remarks { get; set; }
        public string CompletionRemarks { get; set; }
        public string InitiationRemarks { get; set; }
        public string IsBlindCycleCount { get; set; }
        public int IsActive { get; set; }
        public int CCM_CNF_AccountCycleCount_ID { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public int CreatedOn { get; set; }
        public int UpdatedOn { get; set; }
        public int NewCCM_CNF_AccountCycleCount_ID { get; set; }
        public string ValidFrom { get; set; }
        public string ValidThru { get; set; }

    }

    public class GetCycleCountEntityConfigurationModel
    {
        public int AM_MST_Account_ID { get; set; }
        public int CCM_CNF_AccountCycleCount_ID { get; set; }

    }

    public class GetCycleCountlistModel
    {
        public int AccountID { get; set; }

        public int UserId { get; set; }
    }
    public class GetCycleCountTransactionListModel
    {
        public int AccountID { get; set; }
        public int UserId { get; set; }
    }

    public class DeleteInternalTransferDetailsModel
    {
        public int TransferRequestDetailsID { get; set; }

    }

    public class GetCCBlockedLocationsModel
    {
        public int CCM_CNF_AccountCycleCount_ID { get; set; }
        public int AM_MST_Account_ID { get; set; }
        public int CCM_TRN_CycleCount_ID { get; set; }
        public int LocationID { get; set; }
        public string prefix { get; set; }
    }
    public class GetStockAadjustmentNewListModel
    {
        public int CCM_TRN_CycleCount_ID { get; set; }
        public int Rownumber { get; set; }
        public int NofRecordsPerPage { get; set; }

    }

    public class GetAvailableQtyModel
    {
        public int TenantID { get; set; }
        public int CartonID { get; set; }
        public int LocationID { get; set; }
        public int MaterialMasterID { get; set; }
        public string BatchNo { get; set; }
        public int StorageLocationID { get; set; }
        public string ExpDate { get; set; }
        public string PojectRefNo { get; set; }

        public int IsProdOrder { get; set; }

    }
    public class DeleteccOrderModel
    {
        public int CCID { get; set; }
        public int ID { get; set; }
        public int UserID { get; set; }

    }

    public class CreateCC_EntityConfigurationModel
    {
        public CycleCountEntityData data { get; set; }
        public int CycleCount_ID { get; set; }
        public int MaterialMasterID { get; set; }
        public int UserID { get; set; }
    }

    [XmlRoot("root")]
    public class CCXMLData
    {
        [XmlElement("data")]
        public CycleCountEntityData ccxmldata { get; set; }

    }

    public class CycleCountEntityData
    {
        public int CCM_MST_CycleCountEntity_ID { get; set; }
        public int Entity_ID { get; set; }
        public int FromRackID { get; set; }
        public int FromColumnID { get; set; }
        public int FromLevelID { get; set; }
        public int ToRackID { get; set; }
        public int ToColumnID { get; set; }
        public int ToLevelID { get; set; }
        public int CCM_CNF_AccountCycleCountDetail_ID { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public string CreatedOn { get; set; }
        public string UpdatedOn { get; set; }
        public int NewCCM_CNF_AccountCycleCountDetail_ID { get; set; }
        public int CCM_CNF_AccountCycleCount_ID { get; set; }
        public int CCM_MST_CycleCount_ID { get; set; }
        public int IsActive { get; set; }
        public int IsDeleted { get; set; }
    }
    public class GetCC_ShipmentDetailsModel
    {
        public int CycleCount_ID { get; set; }
        public int AccountID { get; set; }
    }
    public class SaveCC_ShipmentDetailsModel
    {
        public int ccid { get; set; }
        public string Remarks { get; set; }
        public string filename { get; set; }
        public string ShipmentVerifiedDate { get; set; }
    }


    public class UpsertBinToBinTransferModel
    {
        public int TransferRequestID { get; set; }
        public int MaterialMasterID { get; set; }
        public int ToLocationID { get; set; }
        public string MfgDate { get; set; }
        public int CreatedBy { get; set; }
        public int FromCartonID { get; set; }
        public int ToCartonID { get; set; }
        public int IsWorkOrder { get; set; }
        public string TransferOrderNo { set; get; }
        public int TransferOrderId { set; get; }
        public int UserId { set; get; }
        public string Result { set; get; }
        public int AccountId { set; get; }
        public string MCode { set; get; }
        public string FromSLoc { set; get; }
        public string ToSLoc { set; get; }
        public string SerialNo { set; get; }

        public string ExpDate { set; get; }
        public string BatchNo { set; get; }
        public string ProjectNo { set; get; }
        public string FromCartonNo { set; get; }
        public string FromLocation { set; get; }
        public decimal AvailQty { set; get; }
        public decimal TransferQty { set; get; }
        public string ToLocation { set; get; }
        public string ToCartonNo { set; get; }
        public int TenantID { set; get; }
        public int WarehouseID { set; get; }
        public string MRP { get; set; }
        public string EmpreqNumber { get; set; }

        public string FromGrade { get; set; }

    }




    public class UpsertCCtransactionModel
    {
        public string CCM_TRN_CycleCount_ID { get; set; }
        public string CCM_CNF_AccountCycleCount_ID { get; set; }
        public int LoggedInUserID { get; set; }
    }

    public class GetUnmappedSKUListModel
    {
        public int MaterialMasterID { get; set; }
        public int WarehouseID { get; set; }
        public int UserID { get; set; }
        public int TenantID { get; set; }
        public List<SKUDetails> DataJson { get; set; }
    }

    public class SKUDetails
    {
        public string SKU { get; set; }
        public string Desc { get; set; }
        public string Mgroup { get; set; }
        public string MType { get; set; }
        public string UoM { get; set; }
        public string TenantCode { get; set; }
        public string Zone { get; set; }
    }
    }
