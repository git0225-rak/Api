using Simpolo_Endpoint.Controllers;
using Simpolo_Endpoint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Simpolo_Endpoint.DAO.interfaces
{
    public interface ICommon
    {
        Task<Payload<string>> Get_inv_cs_Locations_tolocation(HouseKeepingInputModel obj);
        Task<Payload<string>> GetUserDropdown(GetUserDropdownModel items);
        Task<Payload<string>> GetUserTypes();
        Task<Payload<string>> GetTenantDropdown_Account_Tenant(GetTenantDropdown_Account_TenantModel obj);
        Task<Payload<string>> GetTenantDropdown_Account(GetTenantDropdown_AccountModel obj);
        Task<Payload<string>> GetAddressTypes(SaveCustomerDetailsInputModel obj);
        Task<Payload<string>> GetUserRoles(GetUserRolesModel obj);
        Task<Payload<string>> GetWHDropdown_Account(GetWHDropdown_AccountModel obj);
        Task<Payload<string>> GetWHDropdown_User(GetWHDropdown_UserModel obj);
        Task<Payload<string>> ToLocationsDropDown(ToLocationsDropDownModel obj);
        Task<Payload<string>> GetLocDropdown(GetLocDropdown_Model obj);
        Task<Payload<string>> GetTenantDropdown_Account_User_Tenant(GetTenantDropdown_Account_User_TenantModel obj);
        Task<Payload<string>> GetContainerType(GetContainerTypeModel obj);

        Task<Payload<string>> GetSeriesType(GetSeriesTypeModel obj);
        
        Task<Payload<string>> GetDockTypes();
        Task<Payload<string>> GetCountries();
        Task<Payload<string>> GetCurrency(GetCurrencyModel obj);
        Task<Payload<string>> GetStates(GetStatesModel obj);
        Task<Payload<string>> GetCities(GetCitiesModel obj);
        Task<Payload<string>> GetZipCodes(GetZipCodesModel obj);
        Task<Payload<string>> GetTimePreferences();
        Task<Payload<string>> GetDockList(GetDockListModel obj);
        Task<Payload<string>> GetZoneList(GetZoneListModel obj);
        Task<Payload<string>> GetRackTypes();
        Task<Payload<string>> GetWarehouseTypes(GetWarehouseTypesModel obj);
        Task<Payload<string>> GetSupplierDropdown_Tenant(GetSupplierDropdown_TenantModel obj);
        Task<Payload<string>> GetMCodeDropdown(GetMCodeDropdownModel obj);
        Task<Payload<string>> GetMTypeData(GetMTypeDataModel obj);
        Task<Payload<string>> GetMGroupData(GetMGroupDataModel obj);
        Task<Payload<string>> LoadEmployeeList(LoadEmployeeListModel obj);
        Task<Payload<string>> GetBayList(GetlistModel obj);
        Task<Payload<string>> GetRackList(GetlistModel obj);
        Task<Payload<string>> GetColumnList(GetlistModel obj);
        Task<Payload<string>> GetBinList();
        Task<Payload<string>> GetWarehouse(GetWarehouseDataModel obj);
        Task<Payload<string>> GetLocationZonesByWHID(GetLocationZonesByWHIDModel obj);
        Task<Payload<string>> LoadLocationTypes();
        Task<Payload<string>> GetInwardStatus();
        Task<Payload<string>> LoadPOTypes(LoadPOTypesModel obj);

        Task<Payload<string>> LoadINBTypes(LoadINBTypesModel obj);
        Task<Payload<string>> GetSuppliers_POType(GetSuppliers_POTypeModel obj);
        Task<Payload<string>> GetPONumbers(GetPONumbersModel obj);
        Task<Payload<string>> SearchPartNumber(SearchPartNumberModel obj);
        Task<Payload<string>> GetSKUPO(GetSKUPOModel obj);
        Task<Payload<string>> GetUoMQty(UoMQtyPOModel obj);
        Task<Payload<string>> InvNumbersSI(InvNumbersSIModel obj);
        Task<Payload<string>> GetSOStatus();
        Task<Payload<string>> GetSONumbersList(GetSONumbersListModel obj);
        Task<Payload<string>> GetSoType(GetSoTypeModel obj);
        Task<Payload<string>> GetCustomersTenant(GetCustomersTenantModel obj);
        Task<Payload<string>> GetMCodeForSaleOrder(GetMCodeForSaleOrderModel obj);
        Task<Payload<string>> GetCustomerPOdata(GetCustomerPOdataModel obj);
        Task<Payload<string>> GetStorageLocations();
        Task<Payload<string>> GetMCodeForSOWithOEM(GetMCodeForSOWithOEMModel obj);
        Task<Payload<string>> GetOrderforIssue(GetOrderforIssueModel obj);
        Task<Payload<string>> GetQONumber(GetQONumberModel obj);
        Task<Payload<string>> GetStatusForEmp(GetStatusForEmpModel obj);
        Task<Payload<string>> GetTenantbyUser_Emp(GetTenantbyUser_EmpModel obj);
        Task<Payload<string>> GetLabSampleLocation(GetLabSampleLocationModel obj);
        Task<Payload<string>> GetTopallets(ItemsModel obj);
        Task<Payload<string>> GetEmployeeRequestDetails(GetEmployeeRequestDetailsModel obj);
        Task<Payload<string>> GetPONumbersLabSampleRequest(GetPONumbersLabSampleRequestModel obj);
        Task<Payload<string>> GetMCodesBasedOnPO(GetMCodesBasedOnPOModel obj);
        Task<Payload<string>> GetStorageLocationForStockPosting(GetStorageLocationForStockPostingModel obj);
        Task<Payload<string>> GetDeliveryPointData(GetDeliveryPointDataModel obj);
        Task<Payload<string>> GetIndustry(MaterialDataInputModel obj);

        Task<Payload<string>> GetIndustryMaterialAttributes(IndustryMaterialAttributesInputModel obj);
        Task<Payload<string>> GetFastMoveData();
        Task<Payload<string>> GetLoadSAPReference(GetLoadSAPReferenceModel obj);
        Task<Payload<string>> GetFastMovingWH(GetFastMovingWHModel obj);
        Task<Payload<string>> GetTenantsDropdown_Warehouse(GetTenantsDropdown_WarehouseModel obj);
        Task<Payload<string>> GetPOHeaderListTenant(GetPOHeaderListTenantModel obj);
        Task<Payload<string>> GetInvoiceListForPONumber(GetInvoiceListForPONumberModel obj);
        Task<Payload<string>> GetDocks(GetDocksModel obj);

        Task<Payload<string>> GetLoadingPoints(GetLoadingPoints obj);
        Task<Payload<string>> GetTokenNumberOBD(GetLoadingPoints obj);
        Task<Payload<string>> GetVehicleTypes();
        Task<Payload<string>> GetVehicleTypesWeb(GetVehicleTypes vehicle);
        Task<Payload<string>> GetEmployeeReturnList(GetEmployeeReturnListModel obj);
        Task<Payload<string>> GetDeleteItemsById(GetDeleteItemsByIdModel obj);
        //material transfer
        Task<Payload<string>> GetMaterial_Internal(GetMaterial_InternalModel items);
        Task<Payload<string>> GetLocationsDropDown(GetLocationsDropDownModel items);
        Task<Payload<string>> GetBatchListDropDown(GetBatchListDropDownModel items);

        Task<Payload<string>> GetBatchGradeList(GetBatchListDropDownModel items);

        Task<Payload<string>> GetGradeListByBatch(GetGrdaeListDropDown items);
        Task<Payload<string>> GetSLOCDropDownBatchGrade(GetSLOCDropDownModel items);
        Task<Payload<string>> GetSLOCDropDown(GetSLOCDropDownModel items);
        Task<Payload<string>> GetPalletsDropDown(GetPalletsDropDownModel items);
        Task<Payload<string>> GetProjectRefNoDropDown(GetProjectRefNoDropDownModel items);
  
        Task<Payload<string>> GetZonesDropDown(GetZonesDropDownModel items);
        Task<Payload<string>> GetCCListData(GetCCListDataModel items);
        Task<Payload<string>> GetCC_UserDropDown(GetCC_UserDropDownModel items);
        Task<Payload<string>> GetCC_Materials(GetCC_MaterialsModel items);
        Task<Payload<string>> GetCC_RackDetails(GetCC_RackDetailsModel items);
        //Inbound
        Task<Payload<string>> GetTenantDropdown_Warehouse(GetTenantDropdown_WarehouseModel items);
        Task<Payload<string>> GetShipmentTypes(GetShipmentTypesModel items);
        Task<Payload<string>> GetSupplierForInbound(GetSupplierForInboundModel items);
        Task<Payload<string>> GetPONumbersForInbound(GetPONumbersForInboundModel items);
        Task<Payload<string>> GetInvoiceNoForInbound(GetInvoiceNoForInboundModel items);
        Task<Payload<string>> GetPONumberForGRN(GetPONumberForGRNModel items);
        Task<Payload<string>> GetInvoiceNumberForGRN(GetInvoiceNumberForGRNModel items);
        Task<Payload<string>> GetInvoiceNumberForDisc(GetInvoiceNumberForDiscModel items);
        Task<Payload<string>> GetPONumberForDisc(GetPONumberForDiscModel items);
        Task<Payload<string>> GetMCodesForDisc(GetMCodesForDiscModel items);
        Task<Payload<string>> GetPOLineNumbersForDisc(GetPOLineNumbersForDiscModel items);
        Task<Payload<string>> GetUsersDataForInbound(GetUsersDataForInboundModel items);
        Task<Payload<string>> GetRTRMCodes(GetRTRMCodesModel items);
        Task<Payload<string>> GetPalletsForGoodsIn(GetPalletsForGoodsInModel items);
        Task<Payload<string>> GetStorageLoactionsForGoodsIn();
        Task<Payload<string>> GetLoactionsForGoodsIn(GetLoactionsForGoodsInModel items);
        Task<Payload<string>> GetVehicleListForGoodsIn(GetVehicleListForGoodsInModel items);

        Task<Payload<string>> GetVehicleListForGroupOBD(GetVehicleListForGoodsInModel items);


        Task<Payload<string>> GetSITStoreRefNumbers(GetStoreRefNumbersModel items);
        Task<Payload<string>> GetSIEStoreRefNumbers(GetStoreRefNumbersModel items);
        Task<Payload<string>> GetSIPStoreRefNumbers(GetStoreRefNumbersModel items);
        Task<Payload<string>> GetInboundStatus(GetInboundStatusModel obj);
        Task<Payload<string>> GetShipmentTypes_InboundSearch(GetShipmentTypes_InboundSearchModel obj);
        Task<Payload<string>> GetRevertStoreRefNo(GetRevertStoreRefNoModel obj);
        Task<Payload<string>> GetLoadOBDNumbers(GetLoadOBDNumbersModel obj);
        Task<Payload<string>> GetLoadVLPDDelvDocNo(GetLoadVLPDDelvDocNoModel items);
        Task<Payload<string>> GetLoadPNCPendingDelvDocNo_TC(GetLoadPNCPendingDelvDocNo_TCModel items);
        Task<Payload<string>> GetLoadPGIPendingDelvDocNo(GetLoadPGIPendingDelvDocNoModel items);
        Task<Payload<string>> GetLoadPODDelvDocNo(GetLoadPODDelvDocNoModel items);
        Task<Payload<string>> GetShipmentVerificationRef(GetShipmentVerificationRefModel items);
        Task<Payload<string>> GetSkipReason(GetSkipReasonModel obj);
        Task<Payload<string>> GetDeliveryDetails(GetDeliveryDetailsModel items);
        Task<Payload<string>> GetDocumentType();
        Task<Payload<string>> GetLoadMaterialsForCurrentStock(GetLoadMaterialsForCurrentStockModel obj);
        Task<Payload<string>> GetLoadMaterialTypesForCurrentStock(GetLoadMaterialTypesForCurrentStockModel obj);
        Task<Payload<string>> GetLoadMaterialDrawTypesForCurrentStock(GetLoadMaterialDrawTypesForCurrentStockModel obj);
        Task<Payload<string>> GetSlocforCurrentstock(GetSlocforCurrentstockModel obj);
        Task<Payload<string>> Getlabel(GetLabelModel obj);
        Task<Payload<string>> GetLoadLocationsForCurrentStock(GetLoadLocationsForCurrentStockModel obj);
        Task<Payload<string>> GetKitPlannerId(GetKitPlannerIdModel obj);
        Task<Payload<string>> GetLoadIndustries_Auto(GetLoadIndustries_AutoModel obj);
        Task<Payload<string>> GetContainersForCurrentStock(GetContainersForCurrentStockModel obj);
        Task<Payload<string>> GetLoadSOCustomerNames(GetLoadSOCustomerNamesModel obj);
        Task<Payload<string>> GetLoadUsersData(GetLoadUsersDataModel obj);
        Task<Payload<string>> GetPalletCode(GetPalletCodeModel obj);
        Task<Payload<string>> GetAllLocationsUnderWarehouse(GetAllLocationsUnderWarehouseModel obj);
        Task<Payload<string>> GetAuditLogReferenceNo_RPRT(GetAuditLogReferenceNo_RPRTModel obj);
        Task<Payload<string>> GetMaterialForMiscReceipt(GetMaterialForMiscReceiptModel obj);
        Task<Payload<string>> GetMaterialForMiscIssue(GetMaterialForMiscIssueModel obj);
        Task<Payload<string>> GetStoreRefNumbers_RPRT(GetStoreRefNumbers_RPRT_Model obj);
        Task<Payload<string>> GetOutboundNumbers_RPRT(GetOutboundNumbers_RPRT_Model obj);
        Task<Payload<string>> GetCustomerData_RPRT(GetCustomerData_RPRT_Model obj);
        Task<Payload<string>> GetSONumbers_RPRT(GetSONumbers_RPRT_Model obj);
        Task<Payload<string>> GetSONumbersForSO_RPRT(GetSONumbersForSO_RPRT_Model obj);
        Task<Payload<string>> GetReplenishedMaterialCode_RPRT(GetReplenishedMaterialCode_RPRTModel obj);
        Task<Payload<string>> GetMaterialsForMaterialTracking_RPRT(GetMaterialsForMaterialTracking_RPRTModel obj);
        Task<Payload<string>> GetMaterialsForBinReplenishment_RPRT(GetMaterialsForBinReplenishment_RPRTModel obj);
        Task<Payload<string>> GetPOInvoiceNumbers_RPRT(GetPOInvoiceNumbers_RPRTModel obj);
        //16-03-23
        Task<Payload<string>> GetMCodeforGRN_RPRT(GetMCodeforGRN_RPRTModel obj);
        Task<Payload<string>> GetUsers_RPRT(GetUsers_RPRTModel obj);
        Task<Payload<string>> GetMaterialsForExpiryDate_RPRT(GetMaterialsForExpiryDate_RPRTModel obj);
        Task<Payload<string>> GetOperatorSummaryUsers_RPRT(GetOperatorSummaryUsers_RPRTModel obj);
        Task<Payload<string>> GetOperatorSummaryOBDNumbers_RPRT(GetOperatorSummaryOBDNumbers_RPRTModel obj);
        Task<Payload<string>> GetOperatorSummarySONumbers_RPRT(GetOperatorSummarySONumbers_RPRTModel obj);
        Task<Payload<string>> GetMCodeforExpiredMaterial_RPRT(GetMCodeforExpiredMaterial_RPRTModel obj);
        Task<Payload<string>> GetCycleCountNames_RPRT(GetCycleCountNames_RPRTModel obj);
        Task<Payload<string>> GetCycleCountCodes_RPRT(GetCycleCountCodes_RPRTModel obj);
        Task<Payload<string>> GetMaterialsForCycleCount_RPRT(GetMaterialsForCycleCount_RPRTModel obj);
        Task<Payload<string>> GetLocationManager_TenantList();
        Task<Payload<string>> GetLocationManager_Supplier(GetLocationManager_Supplier obj);
        Task<Payload<string>> GetRacks_BulkModify(GetRacks_BulkModifyModel items);
        Task<Payload<string>> GetColumns_Levels_BulkModify(GetColumns_Levels_BulkModifyModel items);
        Task<Payload<string>> GetBins_BulkModify(GetBins_BulkModifyModel items);
        Task<Payload<string>> GetAvlBatchQty(GetAvlBatchQtyModel items);
        Task<Payload<string>> GetMCodesList(GetMCodesListModel MCodesListModel);
        Task<Payload<string>> GetLabels(GetLabelModel items);
        Task<Payload<string>> OBDWareHouse(OBDWareHouseModel items);
        Task<Payload<string>> GetLoadHHTypes(GetLoadHHTypesModel items);
        Task<Payload<string>> GetMCodes_DeliveryPicNote(GetMCodes_DeliveryPicNoteModel items);
        Task<Payload<string>> GetSupplierList(GetSupplierListModel items);
        Task<Payload<string>> GetPO_OrderType(GetPO_OrderTypeModel items);
        Task<Payload<string>> GetSO_OrderType(GetSO_OrderTypeModel items);
        Task<Payload<string>> GetSKUList(GetSKUListModel items);
        Task<Payload<string>> GetCC_ShopFloor_Locations();
        Task<Payload<string>> GetCC_Capture_Containers(GetCC_Capture_ContainersModel items);
        Task<Payload<string>> GetCC_Capture_Materials(GetCC_Capture_MaterialsModel items);
        Task<Payload<string>> Get_PrinterIPList();
        Task<Payload<string>> LoadSONumbers(LoadSONumbersModel items);
        Task<Payload<string>> LoadCustomerPONumbers(LoadCustomerPONumbersModel items);
        Task<Payload<string>> GetCusPOInvoiceNoList(GetCusPOInvoiceNoListModel items);
        Task<Payload<string>> Delete_DeliveryDoc_LineItems(Delete_DeliveryDoc_LineItemsModel items);
        Task<Payload<string>> GetProjectBasedSKUDropdown(GetProjectBasedSKUDropdownModel items);
        Task<Payload<string>> GetBusinessTypes(GetSoTypeModel items);
        Task<Payload<string>> GetBillTypes();

        Task<Payload<string>> GetGrades(ItemsModel items);

        Task<Payload<string>> GetCustomers(Getcustomerobj items);
        Task<Payload<string>> GetSecondLabelInputs();
        Task<Payload<string>> GetInOutVehicleNos(Getcustomerobj obj);
        Task<Payload<string>> GetOBDStatusList();
    }
}