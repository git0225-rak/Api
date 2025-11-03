using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simpolo_Endpoint.Models
{
    public class GetSupplierlistModel
    {
        public string SupplierCode { get; set; }
        public string SupplierName { get; set; }
        public string TenantName { get; set; }
        public int AccountID_New { get; set; }
        public int UserTypeID { get; set; }
        public int TenantID_New { get; set; }
        public int TenantID { get; set; }
    }
    public class GetSupplierDetailsModel
    {
        public int SupplierID { get; set; }
        public int TenantID { get; set; }
        public int AccountID_New { get; set; }
        public int TenantID_New { get; set; }
    }
    public class UpsertSupplierDetailsModel
    {
        public string SupplierName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public int CountryMasterID { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Mobile { get; set; }
        public string Fax { get; set; }
        public string EmailAddress { get; set; }
        public string PCPContactNumber { get; set; }
        public string PCPEmail { get; set; }
        public string BankName { get; set; }
        public string BankAddress { get; set; }
        public int BankCountryID { get; set; }
        public string AccountNo { get; set; }
        public int CurrencyID { get; set; }
        public string SupplierCode { get; set; }
        public int CreatedBy { get; set; }
        public int TenantID { get; set; }
        public int AccountId { get; set; }
        public int SupplierID { get; set; }
        public int RequestedBy { get; set; }
        public string SearchTerm { get; set; }
        public int IsApproved { get; set; }
        public int IsActive { get; set; }
        public int IsFirstEdit { get; set; }
        public int LastEditedByID { get; set; }
        public int SupplierCodeAprEditCount { get; set; }
        public string PCPTitle { get; set; }
        public string PCP { get; set; }
    }

    public class SaveCustomerDetailsInputModel
    {
        public int CustomerID { get; set; }
        public int TenantID { get; set; }
        public int AccountID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerCode { get; set; }
        public string EmailAddress { get; set; }
        public string Mobile { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public string DataJson { get; set; }
        public int LoginAccountId { get; set; }
        public int LoginUserId { get; set; }
        public int LoginTanentId { get; set; }
    }


    public class SaveCustomerAddressInputModel
    {
        public int AddressID { get; set; }
        public int AddressTypeID { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public int CityID { get; set; }
        public int StateID { get; set; }
        public int CountryID { get; set; }
        public int ZipCodeID { get; set; }
        public string DeliveryPoint { get; set; }
        public int IsActive { get; set; }
        public int IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public string LanguageType { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string EntityID { get; set; }
        public int LoginAccountId { get; set; }
        public int LoginUserId { get; set; }
        public int LoginTanentId { get; set; }
        public List<addressinfo> items { get; set; }
    }

    public class addressinfo
    {
        public int AddressTypeID { get; set; }
        public int CreatedBy { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public int CityID { get; set; }
        public int StateID { get; set; }
        public int CountryID { get; set; }
        public int IsActive { get; set; }
        public int IsDeleted { get; set; }
        public string EntityID { get; set; }
        public int ZipCodeID { get; set; }
    }


    public class GetCustomerInfoModel
    {
        public int NofRecordsPerPage { get; set; }
        public int PageIndex { get; set; }
        public int AccountID_New { get; set; }
        public int UserTypeID_New { get; set; }
        public int TenantID_New { get; set; }
        public int UserID_New { get; set; }
        public string SearchText { get; set; }
        public int CustomerID { get; set; }
    }
    public class UpsertCustomerModel
    {
        public CustomerXMLModel Inxml { get; set; }
        public int CusId { get; set; }

        public int UserID { get; set; }

    }
    public class CustomerXMLModel
    {
        public int AccountID { get; set; }
        public int TenantID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerCode { get; set; }
        public string EmailAddress { get; set; }
        public string Mobile { get; set; }
        public int CustomerID { get; set; }
        public int CreatedBy { get; set; }
    }
    public class GetEmployeeListmodel
    {
        public int IsExcel { get; set; }
        public int EmpID { get; set; }
        public int Rownumber { get; set; }
        public int NofRecordsPerPage { get; set; }
    }
    public class UpsertEmployeeModel
    {
        public string SupplierName { get; set; }
        public string SupplierCode { get; set; }
        public int SupplierID { get; set; }
    }

    public class ItemMasteinfoModel
    {
        public string OEMPartNumber { get; set; }
        public string Supplier { get; set; }
        public int SupplierID { get; set; }
        public string MCode { get; set; }
        public string Description { get; set; }
        public int IsMMAdminApproved { get; set; }
        public int MTypeID { get; set; }
        public string TenantName { get; set; }
        public int AccountID_New { get; set; }
        public int TenantID_New { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
    public class GetLocationManagerModel
    {
        public string ZoneID { get; set; }
        public int TenantID { get; set; }
    }
    public class UpsertLocationModel
    {
        public string PhaseName { set; get; }
        public string AisleName { set; get; }
        public string RackFrom { set; get; }
        public string RackTo { set; get; }
        public string ColumnFrom { set; get; }
        public string ColumnTo { set; get; }
        public string LevelFrom { set; get; }
        public string LevelTo { set; get; }
        public string BinFrom { set; get; }
        public string BinTo { set; get; }
        public string IsFastMoving { get; set; }
        public string TenantID { get; set; }
        public string SupList { get; set; }
        public string WhCode { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string Length { get; set; }
        public string LocationType { get; set; }
        public string ZoneID { get; set; }
        public string TenantName { get; set; }
        public List<DropDownData> RackList { get; set; }
        public List<DropDownData> ColumnList { get; set; }
        public List<DropDownData> LevelList { get; set; }
        public List<DropDownData> BinList { get; set; }
        public int AccountID { get; set; }
        public int UserID { get; set; }
    }
    public class DropDownData
    {
        public int ID { set; get; }
        public string Value { set; get; }
    }


    public class Locations
    {
        public int Aisle { get; set; }
        public int Rack { get; set; }
        public int Level { get; set; }
        public int Column { get; set; }
        public int Bin { get; set; }
        public string Location { get; set; }
        public string LableDisplayCode { get; set; }
    }
    public class DeleteLocationModel
    {
        public string Locations { get; set; }
        public int UpdatedBy { get; set; }
    }

    public class Rack
    {
        public string RackName { set; get; }
        public List<Column> ColumnList { set; get; }
    }

    public class Column
    {
        public string ColumnName { set; get; }
        public List<Level> LevelList { set; get; }

    }

    public class Level
    {
        public string LevelName { set; get; }
        public List<Bin> binList { set; get; }
    }
    public class Bin
    {
        public string BinName { set; get; }
        public string FullLocation { set; get; }
        public int Status { set; get; }
        public string Tenant { set; get; }
        public string Account { set; get; }
        public string bindata { set; get; }
        public string binRepdata { set; get; }
        public string MCode { set; get; }
        public string LocationID { get; set; }
        public string TenantID { get; set; }
        public string Zone { set; get; }
        public int IsActive { get; set; }
        public int IsQuarantine { get; set; }
        public int IsMixedMaterialOK { get; set; }
        public bool IsFastMoving { get; set; }
        public string Suppliers { get; set; }


    }
    public class GetLoadBinDetailsModel
    {
        public int Location { get; set; }

    }
    public class UpDateBulkModifyModel
    {
        public string Location { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int MaxWeight { get; set; }
        public int IsMixedMaterialOK { get; set; }
        public int IsFastMoving { get; set; }
        public String MCode { get; set; }
        public int IsActive { get; set; }
        public int IsQuarantine { get; set; }
        public int Tenant { get; set; }
        public int Supplier { get; set; }
        public int CreatedBy { get; set; }
        public int ZoneID { get; set; }
        public int LocationTypeID { get; set; }
        public int AccountID { get; set; }
    }
    public class Modify_LocationsModel
    {
        public string RackCode { get; set; }
        public string Column { get; set; }
        public string Level { get; set; }
        public int ZoneID { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int MaxWeight { get; set; }
        public int IsMixedMaterialOK { get; set; }
        public int IsFastMoving { get; set; }
        public int IsActive { get; set; }
        public int IsQuarantine { get; set; }
        public string Tenant { get; set; }
        public string Supplier { get; set; }
        public string MCode { get; set; }
        public int UserID { get; set; }
        public int LocationTypeID { get; set; }
        public int AccountID { get; set; }
        public string LocationID { get; set; }
    }

    public class Modify_LocationPopupModel
    {
        public int LocationID { get; set; }
    }

    public class ItemMaster_PrintModel
    {
        public string MCode { get; set; }
        public string MDesc { get; set; }
        public string SerialNo { get; set; }
        public string BatchNo { get; set; }
        public string MfgDate { get; set; }
        public string ExpDate { get; set; }
        public string ProjectRefNo { get; set; }
        public string SupplierLot { get; set; }
        public int LabelID { get; set; }
        public string PrintQty { get; set; }
        public string MRP { get; set; }
        public string HUSize { get; set; }
        public string HUNo { get; set; }
        public string ipaddress { get; set; }
        public string Zone { get; set; }
        public int WarehouseID { get; set; }
        public int port { get; set; }
        public int PrinterType { get; set; }

        public string Grade { get; set; }
    }

    public class PrintBarcode_MLabelModel
    {
        public string PrintQty { get; set; }
        public string Duplicateprints { get; set; }
        public string Location { get; set; }
        public string ProjectNo { get; set; }
        public string SupplierLot { get; set; }
        public string MCode { get; set; }
        public string KitCode { get; set; }
        public string ParentMCode { get; set; }
        public string Description { get; set; }
        public string BatchNo { get; set; }
        public string SerialNo { get; set; }
        public int KitPlannerID { get; set; }
        public int KitChildrenCount { get; set; }
        public string MfgDate { get; set; }
        public string ExpDate { get; set; }
        public string HUNo { get; set; }
        public string HUSize { get; set; }
        public string GRNDate { get; set; }
        public string PrinterIP { get; set; }
        public bool IsBoxLabelReq { get; set; }
        public int Dpi { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }
        public string LabelType { get; set; }
        public string Lineno { get; set; }
        public string Mrp { get; set; }
        public string Zone { get; set; }
        public int WarehouseID { get; set; }
        public string Grade { get; set; }
    }
    public class LocationManager_LabelPrintModel
    {
        public string Location { get; set; }
        public string ipaddress { get; set; }
        public int LabelTypeID { get; set; }
        public int port { get; set; }
        public int PrinterType { get; set; }
    }

    public class LocationManager_Bulk_LabelPrintModel
    {
        public int ZoneID { get; set; }
        public string Zone { get; set; }
        public string Column { get; set; }
        public string Level { get; set; }
        public string Bin { get; set; }
        public string Rack { get; set; }
        public string ipaddress { get; set; }
        public int port { get; set; }
        public int LabelTypeID { get; set; }
        public int PrinterType { get; set; }
    }

    public class SaveSupplierDetailsInputModel
    {
        public int SupplierID { get; set; }
        public int AddressBookID { get; set; }
        public int SupplierTypeID { get; set; }
        public string SupplierName { get; set; }
        public int TenantID { get; set; }
        public string SearchTerm { get; set; }
        public string SupplierCode { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public int CountryMasterID { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Mobile { get; set; }
        public string Fax { get; set; }
        public string EmailAddress { get; set; }
        public string PCP { get; set; }
        public string PCPTitle { get; set; }
        public string PCPContactNumber { get; set; }
        public string PCPEmail { get; set; }
        public string BankName { get; set; }
        public string BankAddress { get; set; }
        public int BankCountryID { get; set; }
        public string AccountNo { get; set; }
        public string SortCodeORBLZCode { get; set; }
        public string IBANNo { get; set; }
        public string SwiftCode { get; set; }
        public int CurrencyID { get; set; }
        public int RequestedBy { get; set; }
        public int IsApproved { get; set; }
        public int IsFirstEdit { get; set; }
        public int LastEditedByID { get; set; }
        public int SupplierCodeAprEditCount { get; set; }
        public int CreatedBy { get; set; }
        public int IsActive { get; set; }
        public int AccountId { get; set; }
        public int UpdatedBy { get; set; }
        public string DataJson { get; set; }
        public int LoginAccountId { get; set; }
        public int LoginUserId { get; set; }
        public int LoginTanentId { get; set; }
    }

    public class TenantListInputModel
    {
        public int orderby { get; set; }
        public string orderbyText { get; set; }
        public int TenantID { get; set; }
        public int AccountID_New { get; set; }
        public int UserTypeID_New { get; set; }
        public int TenantID_New { get; set; }
        public int UserID_New { get; set; }
        public string searchData { get; set; }
        public int LoginAccountId { get; set; }
        public int LoginUserId { get; set; }
        public int LoginTanentId { get; set; }

    }
    public class BarcodeInputModel
    {
        public int TenantID { get; set; }
        public int BarcodeTypeID { get; set; }
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public string LableType { get; set; }
        public decimal DPI { get; set; }
        public string ZPLScript { get; set; }
        public int CreatedBy { get; set; }
        public int ActionType { get; set; }
        public int TenantBarcodeTypeID { get; set; }
        public string PrintDesc { get; set; }
        public int WarehouseID { get; set; }
        public int flag { get; set; }

    }

    public class SaveTenantInputModel
    {
        public int AccountID { get; set; }
        public int TenantID { get; set; }
        public string TenantName { get; set; }
        public string TenantCode { get; set; }
        public string CompanyDBA { get; set; }
        public int BusinessTypeID { get; set; }
        public string TenantRegistrationNo { get; set; }
        public string PCPFirstName { get; set; }
        public string PCPLastName { get; set; }
        public string PCPContactNo { get; set; }
        public string PCPEmail { get; set; }
        public string Website { get; set; }
        public int IsInsurance { get; set; }
        public int CreatedBy { get; set; }
        public int? AddressBookTypeID_CI { get; set; }
        public string Address1_CI { get; set; }
        public string Address2_CI { get; set; }
        public string City_CI { get; set; }
        public string State_CI { get; set; }
        public string ZIP_CI { get; set; }
        public int CountryMasterID_CI { get; set; }
        public int CurrencyID_CI { get; set; }
        public string Phone1_CI { get; set; }
        public string Phone2_CI { get; set; }
        public string Mobile_CI { get; set; }
        public string Fax_CI { get; set; }
        public string EMail_CI { get; set; }
        public int IsTaxApplicable { get; set; }
        public string GSTNumber { get; set; }
        public string Latitude_CI { get; set; }
        public string Longitude_CI { get; set; }
        public int ZipCodeId_CI { get; set; }
        public int IsSameAddress_CI { get; set; }
        public int AddressBookTypeID_BI { get; set; }
        public string Address1_BI { get; set; }
        public string Address2_BI { get; set; }
        public string City_BI { get; set; }
        public string State_BI { get; set; }
        public string ZIP_BI { get; set; }
        public int CountryMasterID_BI { get; set; }
        public int CurrencyID_BI { get; set; }
        public string Phone1_BI { get; set; }
        public string Phone2_BI { get; set; }
        public string Mobile_BI { get; set; }
        public string Fax_BI { get; set; }
        public string EMail_BI { get; set; }
        public string Latitude_BI { get; set; }
        public string Longitude_BI { get; set; }
        public int ZipCodeId_BI { get; set; }
        public int IsSameAddress_BI { get; set; }
        public int BillTypeID { get; set; }
        public int Invoice { get; set; }
        public int TenantActivityRateID { get; set; }
        public DateTime InvoiceFrom { get; set; }
        public DateTime InvoiceTo { get; set; }
        public int Active { get; set; }
        public int Tid { get; set; }
        public int LoginAccountId { get; set; }
        public int LoginUserId { get; set; }
        public int LoginTanentId { get; set; }

    }
    public class TenantContractInputModel
    {
        public int TenantContractID { get; set; }
        public string TenantContract { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public DateTime EffectiveTo { get; set; }
        public int TenantID { get; set; }
        public string ResourcePath { get; set; }
        public string Remarks { get; set; }
        public int WarehouseID { get; set; }
        public int SpaceRental { get; set; }
        public int IsActive { get; set; }
        public int SquareUnits { get; set; }
        public int CreatedBy { get; set; }
        public string TenantContractIDs { get; set; }
        public string? bitecode { get; set; }
        public string? fileName { get; set; }
        public string? Type { get; set; }
        public string? Extention { get; set; }
        public int LoginAccountId { get; set; }
        public int LoginUserId { get; set; }
        public int LoginTanentId { get; set; }
        public int AppSSOAccountID { get; set; }
    }
}