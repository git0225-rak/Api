using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simpolo_Endpoint.Models
{
    public class POSOModel
    {

        public class SOListModel
        {

            public string SONumber { get; set; }
            public int SOStatusID { get; set; }
            public string Tenant { get; set; }
            public int AccountID_New { get; set; }
            public int TenantID_New { get; set; }
            public int UserTypeID_New { get; set; }
            public int UserID_New { get; set; }
            public string fromdate { get; set; }
            public string Todate { get; set; }
            public int SOTypeID { get; set; }
        }
        public class UpdateSOModel
        {

            public int SOHeaderID { get; set; }
            public string SONumber { get; set; }
            public int AccountID { get; set; }
            public int TenantID { get; set; }
            public int CustomerID { get; set; }
            public string ProjectCode { get; set; }
            public string ShipmentAddress1 { get; set; }
            public string ShipmentAddress2 { get; set; }
            public string City { get; set; }
            public string Province { get; set; }
            public int CountryMasterID { get; set; }
            public string Zip { get; set; }
            public string Mobile { get; set; }
            public int CurrencyID { get; set; }
            public int DepartmentID { get; set; }
            public int DivisionID { get; set; }
            public string ShipmentCharges { get; set; }
            public int FreightCompanyID { get; set; }
            public int SOTypeID { get; set; }
            public int RequestedBy { get; set; }
            public string SODate { get; set; }
            public string DeliveryDueDate { get; set; }
            public string RequirementNumber { get; set; }
            public int CreatedBy { get; set; }
            public int GEN_MST_Address_ID { get; set; }
            public string Remarks { get; set; }
            public int LastModifiedBy { get; set; }
            public string SOTax { get; set; }
            public string NetValue { get; set; }
            public string GrossValue { get; set; }
            public int IsActive { get; set; }
            public int IsDeleted { get; set; }
            public int SOStatusID { get; set; }

        }
        public class CustomerPOListModel
        {
            public int SOHeaderID { get; set; }
        }

        public class UpsertCustomerPOModel
        {
            public int CustomerPOID { get; set; }
            public int SOHeaderID { get; set; }
            public string CustPONumber { get; set; }
            public string CustPODate { get; set; }
            public int CurrencyID { get; set; }
            public decimal ExchangeRate { get; set; }
            public decimal CustPOValue { get; set; }
            public int CreatedBy { get; set; }
        }
        public class MaterialSODetailsListModel
        {
            public int SOHeaderID { get; set; }
            public string MCode { get; set; }
        }
        //po
        public class POHeaderListInputModel
        {
            public string Todate { get; set; }
            public string fromdate { get; set; }
            public int UserID { get; set; }
            public int TenantID { get; set; }
            public int UserTypeID { get; set; }
            public int AccountID { get; set; }
            public string Tenant { get; set; }
            public int POStatusID { get; set; }
            public string PONumber { get; set; }
            public int POTypeID { get; set; }
            public int UserID_New { get; set; }
        }

        public class UpsertMaterialSODetailsModel
        {
            public int SOHeaderID { get; set; }
            public int SODetailsID { get; set; }
            public int LineNumber { get; set; }
            public int MaterialMasterID { get; set; }
            public int MaterialMaster_SUoMID { get; set; }
            public string KitPlannerID { get; set; }
            public decimal SOQuantity { get; set; }
            public string UnitPrice { get; set; }
            public int CreatedBy { get; set; }
            public int MaterialMaster_CustPOUoMID { get; set; }
            public int CustomerPOID { get; set; }
            public decimal CustPOQuantity { get; set; }
            public string SODiscountInPercentage { get; set; }
            public string VATCode { get; set; }
            public string InvoiceNo { get; set; }
            public string StoreParameterIDS { get; set; }
            public string StoreParameterValues { get; set; }
            public string StorageLocationId { get; set; }
            public string GEN_MST_Address_ID { get; set; }
            public string MfgDate { get; set; }
            public string InvoiceDate { get; set; }
            public string ExpDate { get; set; }
            public string SerialNo { get; set; }
            public string BatchNo { get; set; }
            public string ProjectRefNo { get; set; }

        }

        public class GetStockPostingInputModel
        {
            public int POHeaderID { get; set; }
            public int MMID { get; set; }
        }

        public class DeleteCustomerPOModel
        {
            public string CustomerPOIDs { get; set; }
            public int UpdatedBy { get; set; }
        }
        public class UpdateEmployeeHeaderDataModel
        {
            public int RequestHeaderID { get; set; }
            public int RequestTypeID { get; set; }
            public string SAPIssuePosting_MDNo { get; set; }
            public int SupplierID { get; set; }
            public int DockLocationID { get; set; }
            public int EmpRequestStatusID { get; set; }
            public int AccountID { get; set; }
            public int TenantID { get; set; }
            public int CreatedBy { get; set; }
            public int PONumber { get; set; }
        }

        public class GetSoHeaderDetailsModel
        {
            public int SOHeaderID { get; set; }
            public int AccountID_New { get; set; }
        }
        public class POHeaderModel
        {
            //public int PODetailsID { get; set; }
            //public int SOHeaderID { get; set; }
            //public int SupplierInvoiceID { get; set; }
            //public int SupplierInvoiceDetailsID { get; set; }

            public int POHeaderID { get; set; }
            public int UserID { get; set; }
            public string PONumber { get; set; }
            public string PODate { get; set; }
            public int POStatusID { get; set; }
            public int POTypeID { get; set; }
            public int SupplierID { get; set; }
            public string DateRequested { get; set; }
            public string DateDue { get; set; }
            //public string DepartmentID { get; set; }
            //public string DivisionID { get; set; }
            public int RequestedBy { get; set; }
            public decimal TotalValue { get; set; }
            public string CurrencyID { get; set; }
            public decimal ExchangeRate { get; set; }
            public decimal POTax { get; set; }
            public string Instructions { get; set; }
            public string Remarks { get; set; }
            public int LastModifiedBy { get; set; }
            public int AccountID { get; set; }
            public int TenantID { get; set; }
            public int IsActive { get; set; }
            public int IsDeleted { get; set; }
            public string UpdatedOn { get; set; }
            public int NewPOHeaderID { get; set; }
        }
        public class GetPOMaterialDetailsDataModel
        {
            public int POHeaderID { get; set; }
            public string MCode { get; set; }
        }


        public class POMaterialDetailsDataModel
        {
            public int POHeaderID { get; set; }
            public int PODetailsID { get; set; }
            public int LineNumber { get; set; }
            public int MaterialMasterID { get; set; }
            public decimal POQuantity { get; set; }
            public int MaterialMaster_PUoMID { get; set; }
            public string KitPlannerID { get; set; }
            public string MfgDate { get; set; }
            public string ExpDate { get; set; }
            public string BatchNo { get; set; }
            public string ProjectRefNo { get; set; }
            public string SerialNo { get; set; }
            public int UserID { get; set; }
            public string RequirementNumber { get; set; }
            //public string UpdatedOn { get; set; }
            public string StoreParameterValues { get; set; }
            public string StoreParameterIDS { get; set; }

            public string MCode { get; set; }

        }


        public class SupplierInvoiceInputModel
        {
            public int SupplierInvoiceID { get; set; }
            public int POHeaderID { get; set; }
            public string InvoiceNumber { get; set; }
            public string InvoiceDate { get; set; }
            public int CurrencyID { get; set; }
            //public int ExchangeRate { get; set; }
            public decimal InvoiceValue { get; set; }
            public int NoofPackages { get; set; }
            public decimal NetWeight { get; set; }
            public decimal GrossWeight { get; set; }
            //public int InvVATCode { get; set; }
            public int InvCountryofOriginID { get; set; }
            public int UserID { get; set; }

        }
        public class DeleteSupplierInvoiceInputModel
        {
            public string SupplierInvoiceID { get; set; }
            public int UserID { get; set; }
        }

        public class GetSku_SupplerInvoiceDetailsModel
        {
            public int PODetailsID { get; set; }
        }
        public class SupplerInvoiceMaterialListModel
        {
            public int SupplierInvoiceDetailsID { get; set; }
            public int PODetailsID { get; set; }
            public int SupplierInvoiceID { get; set; }
            public decimal InvoiceQuantity { get; set; }
            public decimal InvDiscountInPercentage { get; set; }
            public decimal UnitPrice { get; set; }
            public decimal Tax { get; set; }
            public int MaterialMaster_InvUoMID { get; set; }
            public int POHeaderID { get; set; }
            public int KitPlannerID { get; set; }
            public string MfgDate { get; set; }
            public string ExpDate { get; set; }
            public string BatchNo { get; set; }
            public string ProjectRefNo { get; set; }
            public string SerialNo { get; set; }
            public string SupplierLot { get; set; }
            public int UserID { get; set; }
            public int LineNumber { get; set; }
            public int MRP { get; set; }
            public int MaterialMasterID { get; set; }
        }

    }
    public class GetmspscheckboxsModel
    {
        public int MaterialMasterID { get; set; }
    }

    public class DeleteSku_SupplerInvoiceDetailsModel
    {
        public string POHeaderID { get; set; }
        public int SupplierInvoiceID { get; set; }
        public string SupplierInvoiceDetailsID { get; set; }
        public int UserID { get; set; }
        public int LineNumber { get; set; }
    }

    public class GetPOHeaderDetailsModel
    {
        public int POHeaderID { get; set; }
    }
    public class UpsertSku_SupplerInvoiceDetailsModel
    {
        public int SupplierInvoiceDetailsID { get; set; }
        public int PODetailsID { get; set; }
        public int SupplierInvoiceID { get; set; }
        public decimal InvoiceQuantity { get; set; }
        public string InvDiscountInPercentage { get; set; }
        public string UnitPrice { get; set; }
        public string Tax { get; set; }
        public int MaterialMaster_InvUoMID { get; set; }
        public int POHeaderID { get; set; }
        public string KitPlannerID { get; set; }
        public string MfgDate { get; set; }
        public string ExpDate { get; set; }
        public string BatchNo { get; set; }
        public string ProjectRefNo { get; set; }
        public string SerialNo { get; set; }
        public string SupplierLot { get; set; }
        public int UserID { get; set; }
        public int LineNumber { get; set; }
        public decimal MRP { get; set; }
    }
    public class DownloadASNIOModel
    {
        public int Poheaderid { get; set; }
    }
    public class ImportASNIOModel
    {
        public int AccountID { get; set; }
        public object DataJson { get; set; }
        public int LoginAccountId { get; set; }
        public int LoginTanentId { get; set; }
        public int LoginUserId { get; set; }
        public int Poheaderid { get; set; }
        public string PONumber { get; set; }
        public string suppliercode { get; set; }
    }

    public class ImportASNCheckMandatorModel
    {
        public string MaterialCode { get; set; }
    }

    public class SODetails_UpdateStorageLoactionModel
    {
        public int SLOCId { get; set; }
        public int SODetailsID { get; set; }
        public int MaterialMasterID { get; set; }
    }

    public class KitModel
    {
        public int TenantID { get; set; }
        public string Prefix { get; set; }
        public string KitRefNo { get; set; }
        public int KitHeaderID { get; set; }
    }

}
