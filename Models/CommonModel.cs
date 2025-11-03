using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Simpolo_Endpoint.Models
{
    public class GetUserDropdownModel
    {
        public int AccountID { get; set; }
        public int UserIDNew { get; set; }
        public int TenantId { get; set; }
    }

    public class GetTenantDropdown_AccountModel
    {
        public int Flag { get; set; }
        public int AccountID { get; set; }

    }

    public class GetTenantDropdown_Account_TenantModel
    {
        public string prefix { get; set; }
        public int AccountID { get; set; }
        public int TenantID { get; set; }
    }


    public class GetUserRolesModel
    {
        public int UserTypeID { get; set; }

    }

    public class GetWHDropdown_AccountModel
    {
        public int AccountID { get; set; }

    }

    public class GetWHDropdown_UserModel
    {
        public int USERID { get; set; }
        public string PRIFIX { get; set; }

    }

    public class GetLocDropdown_Model
    {
        public int USERID { get; set; }
        public string PREFIX { get; set; }

        public int AccountID { get; set; }

    }

    public class GetTenantDropdown_Account_User_TenantModel
    {
        public string prefix { get; set; }
        public int AccountID { get; set; }
        public int USERID { get; set; }
        public int TenantID { get; set; }
    }
    public class GetContainerTypeModel
    {
        public string prefix { get; set; }
    }


    public class GetSeriesTypeModel
    {
        public int SeriesTypeID { get; set; }
        public string SeriesType { get; set; }

        public string prefix { get; set; }
    }

    public class GetCurrencyModel
    {
        public string CountryID { get; set; }
    }

    public class GetStatesModel
    {
        public string Flag { get; set; }
        public int CountryID { get; set; }
    }

    public class GetCitiesModel
    {
        public int StateID { get; set; }
        public string Flag { get; set; }
    }

    public class GetZipCodesModel
    {
        public int CityID { get; set; }

    }
    public class GetDockListModel
    {
        public int AccountID { get; set; }
        public int WarehouseID { get; set; }

    }
    public class GetZoneListModel
    {
        public int AccountID { get; set; }
        public int WarehouseID { get; set; }

    }

    public class GetWarehouseTypesModel
    {
        public string Flag { get; set; }

    }

    public class GetSupplierDropdown_TenantModel
    {
        public string prefix { get; set; }
        public int TenantID { get; set; }
        public int AccountID { get; set; }

    }

    public class GetMCodeDropdownModel
    {
        public string prefix { get; set; }
        public int AccountID { get; set; }
        public int SupplierID { get; set; }

    }

    public class GetMTypeDataModel
    {
        public string prefix { get; set; }
        public int TenantID { get; set; }
    }

    public class GetMGroupDataModel
    {
        public string prefix { get; set; }
        public int TenantID { get; set; }
    }

    public class LoadEmployeeListModel
    {
        public string prefix { get; set; }
    }

    public class GetWarehouseDataModel
    {
        public string WarehouseIDs { get; set; }
        public int InOutID { get; set; }
        public int AccountID_New { get; set; }
        public int UserTypeID_New { get; set; }
        public int TenantID_New { get; set; }
        public int UserID_New { get; set; }
        public int ZoneID { get; set; }
    }

    public class GetlistModel 
    { 
        public int ZoneID { get; set; }
    }


    public class GetLocationZonesByWHIDModel
    {
       
        public string WarehouseID { get; set; }
        public int AccountID { get; set; }
    }
    public class LoadPOTypesModel
    {
        public string prefix { get; set; }
    }


    public class LoadINBTypesModel
    {
        public string prefix { get; set; }
    }




    public class GetSuppliers_POTypeModel
    {
        public int TenantID { get; set; }
        public int AccountID { get; set; }
        public string prefix { get; set; }
        public int LogTenantID { get; set; }
    }
    public class GetPONumbersModel
    {
        public int TenantID { get; set; }
        public int AccountID { get; set; }
        public string prefix { get; set; }
        public int LogTenantID { get; set; }
    }

    public class HouseKeepingInputModel
    {
        public int AccountID { get; set; }
        public int UserID { get; set; }
        public string prefix { get; set; }
        public int TransferRequestID { get; set; }
        public int WarehouseID { get; set; }
        public int MaterialMasterId { get; set; }
        public int WorkOrderRequestID { get; set; }
        public int TenantID { get; set; }
    }
    public class SearchPartNumberModel
    {
        public string prefix { get; set; }
        // public int SuppleirID { get; set; }
        //public int TenantID { get; set; }
        public int POHeaderID { get; set; }

    }
    public class GetSKUPOModel
    {
        public string prefix { get; set; }
        public int TenantID { get; set; }
        public int SupplierID { get; set; }
    }
    public class UoMQtyPOModel
    {
        public int MaterialID { get; set; }

    }
    public class InvNumbersSIModel
    {
        public int SupplierInvoiceID { get; set; }
        public int POHeaderID { get; set; }
        public int PODetailsID { get; set; }

    }
    public class GetSONumbersListModel
    {
        public int LogTenantID { get; set; }
        public int LogAccountID { get; set; }
        public string prefix { get; set; }
        public int IsToolItem { get; set; }
    }
    public class GetSoTypeModel
    {
        public string prefix { get; set; }
    }
    public class GetCustomersTenantModel
    {
        public string prefix { get; set; }
        public int TenantID { get; set; }
        public int LogTenantID { get; set; }

    }
    public class GetMCodeForSaleOrderModel
    {
        public string prefix { get; set; }
        public int TenantID { get; set; }
    }
    public class GetCustomerPOdataModel
    {
        public int SOHeaderID { get; set; }
    }
    public class GetMCodeForSOWithOEMModel
    {
        public string prefix { get; set; }
        public int SOHeaderID { get; set; }
    }
    public class GetOrderforIssueModel
    {
        public string prefix { get; set; }

    }
    public class GetQONumberModel
    {
        public string prefix { get; set; }

    }
    public class GetStatusForEmpModel
    {
        public string prefix { get; set; }

    }
    public class GetTenantbyUser_EmpModel
    {
        public int TenantID { get; set; }
        public string prefix { get; set; }

    }

    public class ItemsModel
    {
        public string prefix { get; set; }
        public int TenantId { get; set; }
        public int MaterialMasterID { get; set; }
        public int LocationID { get; set; }
        public int Warehouseid { get; set; }
    }

    public class GetLabSampleLocationModel
{
    public string prefix { get; set; }


}
public class GetEmployeeRequestDetailsModel
    {
        public int EmployeeID { get; set; }
        public int RequestHeaderID { get; set; }
        public int RequestTypeID { get; set; }
        public int SAP_MDNumber { get; set; }
        public int EmpRequestStatusID { get; set; }
        public int TenantId { get; set; }
        public int Rownumber { get; set; }
        public int NofRecordsPerPage { get; set; }
    }
    public class GetPONumbersLabSampleRequestModel
    {
        public string TenantID { get; set; }
        public string prefix { get; set; }
    }

    public class GetMCodesBasedOnPOModel
    {
        public int POHeaderID { get; set; }
        public string prefix { get; set; }
    }
    public class GetStorageLocationForStockPostingModel
    {
        public string prefix { get; set; }
    }
    public class GetDeliveryPointDataModel
    {
        public int EntityID { get; set; }
    }
    public class GetLoadSAPReferenceModel
    {
        public string prefix { get; set; }
    }
    public class GetFastMovingWHModel
    {
        public int AccountID { get; set; }
        public int UserID { get; set; }
        public string prefix { get; set; }
    }
    public class GetTenantsDropdown_WarehouseModel
    {
        public string prefix { get; set; }
        public int whid { get; set; }

    }
    public class GetPOHeaderListTenantModel
    {
        public string prefix { get; set; }
        public int TenantID { get; set; }

    }
    public class GetInvoiceListForPONumberModel
    {
        public int POHeaderID { get; set; }
    }
    public class GetDocksModel
    {
        public int IsOutbound { get; set; }
        public int Warehouseid { get; set; }

        public int InboundID { get; set; }
    }


    public class GetLoadingPoints
    {
        public int Warehouseid { get; set; }
        public int TenantID { get; set; }
        public int UserID { get; set; }
        public string OutboundIDs { get; set; }

        public int OutboundID { get; set; }
        public string inputjson { get; set; }

        public int LoadPointID { get; set; }

 
    }
    public class GetEmployeeReturnListModel
    {
        public int EmployeeID { get; set; }
        public int RequestHeaderID { get; set; }
        public int RequestTypeID { get; set; }
        public int SAP_MDNumber { get; set; }
        public int EmpRequestStatusID { get; set; }
        public int TenantId { get; set; }
        public int Rownumber { get; set; }
        public int NofRecordsPerPage { get; set; }
    }

    public class MaterialDataInputModel
    {
        public int LoginAccountId { get; set; }
        public int LoginUserId { get; set; }
        public int LoginTanentId { get; set; }
        public string prefix { get; set; }
    }



    public class IndustryMaterialAttributesInputModel
    {
        public int MaterialMasterID { get; set; }
        public string LanguageType { get; set; }
        public int GEN_IndustryID { get; set; }
        public int AccountID { get; set; }
        public int TenantId { get; set; }
        public int LoginAccountId { get; set; }
        public int LoginUserId { get; set; }
        public int LoginTanentId { get; set; }

    }

    public class GetDeleteItemsByIdModel
    {
        public int ID { get; set; }
    }
    //material transfer
    public class GetMaterial_InternalModel
    {
        public string prefix { get; set; }
        public int TenantID { get; set; }
    }
    public class GetLocationsDropDownModel
    {
        public int AccountID { get; set; }
        public int TenantID { get; set; }
        public int WarehouseID { get; set; }
        public int MaterialMasterID { get; set; }
        public string prefix { get; set; }

        public int IsProdOrder { get; set; }
    }

    public class ToLocationsDropDownModel
    {
        public int AccountID { get; set; }
        public int UserID { get; set; }
        public string prefix { get; set; }
        public int TransferRequestID { get; set; }
        public int WarehouseID { get; set; }
    }
    public class GetBatchListDropDownModel
    {
        public int MaterialMasterID { get; set; }
        public int LocationID { get; set; }
        public int CartonID { get; set; }
        public string prefix { get; set; }
        public int TenantID { get; set; }

        public int IsProdOrder { get; set; }
    }


    public class GetGrdaeListDropDown
    {
        public int MaterialMasterID { get; set; }
        public int LocationID { get; set; }
        public int CartonID { get; set; }
        public string prefix { get; set; }
        public int TenantID { get; set; }

        public int IsProdOrder { get; set; }

        public string BatchNo { get; set; }
    }
    public class GetSLOCDropDownModel
    {
        public string prefix { get; set; }
        public int MaterialMasterID { get; set; }
        public int LocationID { get; set; }
        public int CartonID { get; set; }

        public int TenantID { get; set; }

        public int GradeID { get; set; }

        public string BatchNo { get; set; }

        public int IsProdOrder { get; set; }
    }

    public class GetPalletsDropDownModel
    {
        public string prefix { get; set; }
        public int MaterialMasterID { get; set; }
        public int LocationID { get; set; }
        public int WarehouseID { get; set; }

        public int IsProdOrder { get; set; }
    }

    public class GetProjectBasedSKUDropdownModel
    {
        public string prefix { get; set; }
        public int TenantID { get; set; }
        public string Projectrefno { get; set; }
    }



    public class GetProjectRefNoDropDownModel
    {
        public string prefix { get; set; }
        public int TenantID { get; set; }
        public int CartonID { get; set; }
        public int MaterialMasterID { get; set; }
        public int LocationID { get; set; }
        public string BatchNo { get; set; }
    }

    public class GetZonesDropDownModel
    {
        public string prefix { get; set; }
        public int WarehouseID { get; set; }
    }

    public class GetCCListDataModel
    {
        public int CycleCount_ID { get; set; }
    }
    public class GetCC_UserDropDownModel
    {
        public string prefix { get; set; }
        public int TenantID { get; set; }
        public int WarehouseID { get; set; }
        public int AccountID { get; set; }
    }
    public class GetCC_MaterialsModel
    {
        public string prefix { get; set; }
        public int TenantID { get; set; }
    }
    public class GetCC_RackDetailsModel
    {
        public int UserID { get; set; }
        public int AccountID { get; set; }
        public int CycleCount_ID { get; set; }
    }
    //inbound//07//03//23
    public class GetTenantDropdown_WarehouseModel
    {
        public string prefix { get; set; }
        public int WHID { get; set; }

    }
    public class GetShipmentTypesModel
    {
        public string prefix { get; set; }
    }
    public class GetSupplierForInboundModel
    {
        public string prefix { get; set; }
        public int TenantID { get; set; }
        public int ShipmentTypeID { get; set; }
    }
    public class GetPONumbersForInboundModel
    {
        public string prefix { get; set; }
        public int SupplierID { get; set; }
        public int TenantID { get; set; }
    }
    public class GetInvoiceNoForInboundModel
    {
        public string prefix { get; set; }
        public int SupplierID { get; set; }
        public int POHeaderID { get; set; }
        public int SupplierInvoiceID { get; set; }
    }
    public class GetPONumberForGRNModel
    {
        public string prefix { get; set; }
        public int InboundID { get; set; }
    }
    public class GetInvoiceNumberForGRNModel
    {
        public string prefix { get; set; }
        public int InboundID { get; set; }
        public int POHeaderID { get; set; }
    }
    public class GetInvoiceNumberForDiscModel
    {
        public string prefix { get; set; }
        public int InboundID { get; set; }
    }
    public class GetPONumberForDiscModel
    {
        public string prefix { get; set; }
        public int TenantID { get; set; }
        public int InboundID { get; set; }
        public int SupplierInvoiceID { get; set; }
    }
    public class GetMCodesForDiscModel
    {
        public string prefix { get; set; }
        public int InboundID { get; set; }
        public int SupplierInvoiceID { get; set; }
    }
    public class GetPOLineNumbersForDiscModel
    {
        public int POHeaderID { get; set; }
        public int SupplierInvoiceID { get; set; }
        public int InboundID { get; set; }
        public int MaterialMasterID { get; set; }
    }
    public class GetUsersDataForInboundModel
    {
        public string prefix { get; set; }
        public int TenantID { get; set; }
        public int LogAccountID { get; set; }

    }
    public class GetRTRMCodesModel
    {
        public int InboundID { get; set; }
        public int MCode { get; set; }
    }
    public class GetLoactionsForGoodsInModel
    {
        public string prefix { get; set; }
        public int InboundID { get; set; }
    }
    public class GetPalletsForGoodsInModel
    {
        public string prefix { get; set; }
        public int InboundID { get; set; }
        public int Location { get; set; }
    }
    public class GetVehicleListForGoodsInModel
    {
        public int InboundID { get; set; }

        public int CustomerID { get; set; }

        public int VechileNumber { get; set; }

        public int TenantID { get; set; }

        public int WarehouseID { get; set; }
    }
    public class GetStoreRefNumbersModel
    {
        public string StoreRefNo { get; set; }
        public int WarehouseIDs { get; set; }
        public int AccountID_New { get; set; }
        public int TenantID { get; set; }
        public int InbPanelId { get; set; }
    }
    public class GetInboundStatusModel
    {
        public string prefix { get; set; }
    }
    public class GetShipmentTypes_InboundSearchModel
    {
        public string prefix { get; set; }
    }
    public class GetRevertStoreRefNoModel
    {
        public string StoreRefNo { get; set; }
        public int AccountID_New { get; set; }
        public int UserID_New { get; set; }
        public int TenantID { get; set; }
    }
    //09_03_23
    public class GetLoadOBDNumbersModel
    {
        public int TenantID { get; set; }
        public int AccountID { get; set; }
        public string prefix { get; set; }
        public int Isreport { get; set; }
    }

    public class GetLoadVLPDDelvDocNoModel
    {
        public string Accountid { get; set; }
        public int Tenantid { get; set; }
        public string prefix { get; set; }
    }
    public class GetLoadPNCPendingDelvDocNo_TCModel
    {
        public string OBDNumber { get; set; }
        public string WarehouseIDs { get; set; }
        public int TenantID { get; set; }
        public int AccountID_New { get; set; }

    }
    public class GetLoadPGIPendingDelvDocNoModel
    {
        public string OBDNumber { get; set; }
        public string WarehouseIDs { get; set; }
        public int TenantID { get; set; }
        public int AccountID_New { get; set; }


    }
    public class GetLoadPODDelvDocNoModel
    {
        public string prefix { get; set; }
        public string WarehouseIDs { get; set; }
        public int TenantID { get; set; }
        public int AccountID_New { get; set; }

    }
    public class GetShipmentVerificationRefModel
    {
        public int InboundID { get; set; }
        public int IB_RefWarehouse_DetailsID { get; set; }
    }
    public class GetSkipReasonModel
    {
        public int ReasonID { get; set; }
        //public int IsActive { get; set; }
        //public int IsDeleted { get; set; }
    }
    public class GetDeliveryDetailsModel
    {
        public int WarehouseID { get; set; }
        public int OutboundID { get; set; }
    }
    //inventory//currentstock //SKU
    public class GetLoadMaterialsForCurrentStockModel
    {
        public int TenantID { get; set; }
        public String Prefix { get; set; }
    }
    //inventory//currentstock //materialtype
    public class GetLoadMaterialTypesForCurrentStockModel
    {
        public int TenantID { get; set; }
        public String Prefix { get; set; }
    }
    //inventory//currentstock //materialdraw type
    public class GetLoadMaterialDrawTypesForCurrentStockModel
    {
        public String Prefix { get; set; }
    }
    //inventory//currentstock //storage location
    public class GetSlocforCurrentstockModel
    {
        public String Prefix { get; set; }
    }


    public class GetLoadLocationsForCurrentStockModel
    {
        public String Prefix { get; set; }
        public int AccountID { get; set; }
        public int WarehouseID { get; set; }
    }
    public class GetKitPlannerIdModel
    {
        public String Prefix { get; set; }
        public int TenantID { get; set; }
    }
    //industry
    public class GetLoadIndustries_AutoModel
    {
        public String Prefix { get; set; }
    }
    public class GetContainersForCurrentStockModel
    {
        public String Prefix { get; set; }
        public int WarehouseID { get; set; }
    }
    public class GetLoadSOCustomerNamesModel
    {
        public String Prefix { get; set; }
        public int TenantID { get; set; }
    }
    public class GetLoadUsersDataModel
    {
        public String Prefix { get; set; }
        public int TenantID { get; set; }
        public int WarehouseID { get; set; }
        //public int flag { get; set; }
        public int AccountID { get; set; }
    }
    public class GetPalletCodeModel
    {
        public String Prefix { get; set; }
        public int Location { get; set; }
        public int WarehouseId { get; set; }
        public int TenantID { get; set; }
        public int UserId { get; set; }
    }
    public class GetAllLocationsUnderWarehouseModel
    {
        public int MaterialMasterID { get; set; }
        public String Prefix { get; set; }
        public int WarehouseId { get; set; }

    }

    public class GetAuditLogReferenceNo_RPRTModel
    {
        public int CategoryID { get; set; }
        public String Prefix { get; set; }
        

    }
    public class GetMaterialForMiscReceiptModel
    {
        public String Prefix { get; set; }
        public int TenantID { get; set; }
        public int AccountID { get; set; }

    }
    public class GetMaterialForMiscIssueModel
    {
        public String Prefix { get; set; }
        public int TenantID { get; set; }
        public int AccountID { get; set; }
    }
    public class GetStoreRefNumbers_RPRT_Model
    {
        public int AccountID { get; set; }
        public int WarehouseID { get; set; }
        public int TenantID { get; set; }
    }
    public class GetOutboundNumbers_RPRT_Model
    {
        public int WareHouseId { get; set; }
        public int CustomerId { get; set; }
        public int TenantID { get; set; }
        public String Prefix { get; set; }

    }
    public class GetCustomerData_RPRT_Model
    {
        public int TenantId { get; set; }
        public String Prefix { get; set; }
    }
    public class GetSONumbers_RPRT_Model
    {
        public int OutboundID { get; set; }
        public String prefix { get; set; }

    }
    public class GetSONumbersForSO_RPRT_Model
    {
        public int AccountID { get; set; }
        public int TenantID { get; set; }
        public int LoginAccountId { get; set; }
        public int LoginTanentId { get; set; }
        public int LoginUserId { get; set; }
        public String prefix { get; set; }

    }
    public class GetReplenishedMaterialCode_RPRTModel
    {
        public string prefix { get; set; }
        public int AccountID { get; set; }
        public int TenantID { get; set; }
    }
    public class GetMaterialsForMaterialTracking_RPRTModel
    {
        public int TenantID { get; set; }
        public String prefix { get; set; }
    }

    public class GetMaterialsForBinReplenishment_RPRTModel
    {
        public int AccountID { get; set; }
        public int TenantID { get; set; }
        public String prefix { get; set; }
    }
    public class GetPOInvoiceNumbers_RPRTModel
    {
        public string prefix { get; set; }
        public int POHeaderID { get; set; }
    }
    public class GetMCodeforGRN_RPRTModel
    {
        public int TenantID { get; set; }
        public string prefix { get; set; }
    }
    public class GetUsers_RPRTModel
    {
        public int TenantID { get; set; }
        public int WarehouseID { get; set; }
        public int flag { get; set; }
        public int LogAccountID { get; set; }
        public string Prefix { get; set; }
    }
    public class GetMaterialsForExpiryDate_RPRTModel
    {
        public int TenantID { get; set; }
        public string Prefix { get; set; }
    }
    public class GetOperatorSummaryUsers_RPRTModel
    {
        public string Prefix { get; set; }
    }
    public class GetOperatorSummaryOBDNumbers_RPRTModel
    {
       public int UserID { get; set; }
        public string Prefix { get; set; }
    }
    public class GetOperatorSummarySONumbers_RPRTModel
    {
        public int OBDID { get; set; }
        public string prefix { get; set; }
    }
    public class GetMCodeforExpiredMaterial_RPRTModel
    {
        public string TenantID { get; set; }
        public string prefix { get; set; }
        
    }
    public class GetCycleCountNames_RPRTModel
    {
        public int AccountID { get; set; }
        public int WarehouseID { get; set; }
        public int CCM_MST_CycleCount_ID { get; set; }
        public string prefix { get; set; }
        
    }
    public class GetCycleCountCodes_RPRTModel
    {
        public int CCM_CNF_AccountCycleCount_ID { get; set; }
        public string prefix { get; set; }

    }
    public class GetMaterialsForCycleCount_RPRTModel
    {
        public int UserID { get; set; }
        public int WarehouseId { get; set; }
        public string prefix { get; set; }
    }
    public class GetLocationManager_Supplier
    {
        public int TenantID { get; set; }
    }
    public class GetRacks_BulkModifyModel
    {
        public int ZoneID { get; set; }
    }

    public class GetColumns_Levels_BulkModifyModel
    {
        public int ZoneID { get; set; }
        public string RackCode { get; set; }
    }

    public class GetBins_BulkModifyModel
    {
        public int ZoneID { get; set; }
        public string RackCode { get; set; }
        public string ColumnCode { get; set; }
        public string LevelCode { get; set; }
    }
    public class GetAvlBatchQtyModel
    {
        public int whid { get; set; }
        public decimal pendingQty { get; set; }
        public string prefix { get; set; }
        public int SODetailsID { get; set; }
    }

    public class GetMCodesListModel
    {
        public int AccountID { get; set; }
        public string prefix { get; set; }
        public int TenantId { get; set; }
    }
    public class GetLabelModel
    {
        public int ActionType { get; set; }
    }
    public class MCodeOEMData
    {
        public int OutboundID { get; set; }
        public string prefix { get; set; }
    }

    public class OBDWareHouseModel
    {
        public int OutboundID { get; set; }
    }
    public class GetLoadHHTypesModel
    {
        public int HandlingTypeID { get; set; }
        public string HandlingType { get; set; }
    }

    public class GetMCodes_DeliveryPicNoteModel
    {
        public int OutboundID { get; set; }
        public string prefix { get; set; }
    }
    public class GetSupplierListModel
    {
        public int MaterialMasterID { get; set; }
    }
    public class GetPO_OrderTypeModel
    {
        public string prefix { get; set; }
    }
    public class GetSO_OrderTypeModel
    {
        public string prefix { get; set; }
    }

    public class GetSKUListModel
    {
        public int TenantID { get; set; }
        public string prefix { get; set; }
    }

    public class GetCC_Capture_ContainersModel
    {
        public string prefix { get; set; }
    }

    public class GetCC_Capture_MaterialsModel
    {
        public string prefix { get; set; }
    }

    public class LoadSONumbersModel
    {
        public string prefix { get; set; }
        public int TenantID { get; set; }
        public int CustomerId { get; set; }
    }

    public class LoadCustomerPONumbersModel
    {
        public string prefix { get; set; }
        public string InvoiceNo { get; set; }
        public int SOHeaderID { get; set; } 
    }

    public class GetCusPOInvoiceNoListModel
    {
        public string prefix { get; set; }
        public string InvoiceNo { get; set; }
        public int CustomerPOID { get; set; }
    }

    public class Delete_DeliveryDoc_LineItemsModel
    {
        public int SOHeaderID { get; set; }
    }

    public class GetVehicleTypes
    {
        public string prefix { set; get; }
    }

    public class Getcustomerobj
    {
        public string prefix { get; set; }

        public int TenantID { get; set; }

        public int LoggedTenantID { get; set; }
        public int IsInbound { get; set; }
    }
}





