using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simpolo_Endpoint.Models
{

    public class GetMaterialListModel
    {

        public string OEMPartNumber { get; set; }
        public int SupplierID { get; set; }
        public string Supplier { get; set; }
        public string MCode { get; set; }
        public string Description { get; set; }
        public int  IsMMAdminApproved { get; set; }
        public int MTypeID { get; set; }
        public string TenantName { get; set; }
        public int AccountID_New { get; set; }
        public int TenantID_New      { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }

    public class GetItemMasterAutocompletesModel
    {
        public int TenantID { get; set; }
        public int AccountID { get; set; }
    }   

    public class GetMaterailParametersInfoModel
    {
        public int  MaterialMasterID { get; set; }
        public int AccountID_New { get; set; }

        public int TenantID { get; set; }
    }

    public class UpsertItemMasterBasicDetailsModel
    {
        public XMLData XmlData { get; set; }
        public int LoggedInUserID { get; set; }
        public int TenantID { get; set; }
        public int AccountID { get; set; }
        public int MaterialMasterID { get; set; }
        public string Mcode { get; set; }
    }


    public class SaveUpdateSupplierDetailsModel
    {
        public int MaterialMaster_SupplierID { get; set; }
        public int MaterialMasterID { get; set; }
        public int SupplierID { get; set; }
        public int TenantID { get; set; }
        public float ExpectedUnitCost { get; set; }
        public int PlannedDeliveryTime { get; set; }
        public float InitialOrderQuantity { get; set; }
        public int CurrencyID { get; set; }
        public int CreatedBy { get; set; }
        public string SupplierPartNo { get; set; }
        public int AccountId { get; set; }
        public int UserId { get; set; }

    }
    public class XMLData
    {
        public int AccountID { get; set; }
        public int Tenant { get; set; }
        public string MCode { get; set; }
        public int MGroupID { get; set; }
        public string MDescription { get; set; }
        public string MDrawType { get; set; }
        public string MOP { get; set; }
        public int MTypeID { get; set; }
        public int StorageConditionID { get; set; }
        public int MinShelfLifeinDays { get; set; }
        public int TotalShelfLifeinDays { get; set; }
        public decimal MLength { get; set; }
        public decimal MHeight { get; set; }
        public decimal MWidth { get; set; }
        public decimal MWeight { get; set; }
        public decimal CapacityPerBin { get; set; }
        public int ProductCategoryID { get; set; }
        public string OEMPartNo { get; set; }
        public string MCodeAlternative1 { get; set; }
        public string MCodeAlternative2 { get; set; }
        public string ProductCategoryID1 { get; set; }
        public string MDescriptionLong { get; set; }
        public int MaterialMasterID { get; set; }
        public string Manufacturernumber { get; set; }
        public string Plant { get; set; }
        public string StorageLocation { get; set; }
        public string Salesorganization { get; set; }
        public string Distributionchannel { get; set; }
        public string Taxcategory { get; set; }
        public string Taxclassification { get; set; }
        public string zoneID { get; set; }
        public decimal GrossWeight { get; set; }
        public string UnitOfWeight { get; set; }
    }

    public class UpsertUoMInfoModel
    {
        public int MaterialMaster_UoMID { get; set; }
        public int TenantID { get; set; }
        public int MaterialMasterID { get; set; }
        public int UoMTypeID { get; set; }
        public int UoMID { get; set; }
        public decimal UoMQty { get; set; }
        public int CreatedBy { get; set; }   
    }
    public class SetMspsModel
    {
        public List<XMLService> data { get; set; }
        public int UserID { get; set; }
    }
    public class XMLService
    {
        public int MaterialMasterID { get; set; }
        public int MaterialMaster_MaterialStorageParameterID { get; set; }
        public int MaterialStorageParameterID { get; set; }
        public int IsActive { get; set; }
        public int IsDeleted { get; set; }
        public int IsRequired { get; set; }
    }

    public class GetMCodeCheck
    {
        public int TenantID { get; set; }
        public string MCode { get; set; }
    }

    public class GetItemPictureDetailsModel
    {
        public int MaterialMasterID { get; set; }
    }

    public class UpsertItemPictureDetailsModel
    {
        public int MaterialMasterID { get; set; }
        public string fileName { get; set; }
        public string imageurl { get; set; }
        public string Type { get; set; }
    }


    public class ImportXmlInput
    {
        public List<XMLDATA> DataJson { get; set; }
        public int UserID { get; set; }
        public int AccountID { get; set; }
        public int TenantId { get; set; }
        public int LoginAccountId { get; set; }
        public int LoginUserId { get; set; }
        public int LoginTenantId { get; set; }
    }


    public class XMLDATA
    {
        public string BUoM { get; set; }
        public int BUoM_Quantity { get; set; }
        public string CapacityPerBin { get; set; }
        public int CreatedBy { get; set; }
        public string MaterialGroupCode { get; set; }
        public int IsActive { get; set; }
        public int IsDeleted { get; set; }
        public string ItemDescriptionLong { get; set; }
        public string ItemDescriptionShort { get; set; }
        public string MCodeAlternative1 { get; set; }
        public string MCodeAlternative2 { get; set; }
        public int MHeight { get; set; }
        public int MLength { get; set; }
        public string MUoM { get; set; }
        public string MUoM_Quantity { get; set; }
        public int MWeight { get; set; }
        public int MWidth { get; set; }
        public string MaterialType { get; set; }
        public string OEMPartNumber { get; set; }
        public string PartNumber { get; set; }
        public string ProductCategory { get; set; }
        public string StorageCondition { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierName { get; set; }
        public string TenantCode { get; set; }
        public string TenantName { get; set; }
        public int UpdatedBy { get; set; }
        public string ishurequired { get; set; }
    }

}
