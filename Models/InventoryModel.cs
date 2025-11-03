using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simpolo_Endpoint.Models
{
    public class InventoryModel
    {
        public class GetCurrentStockInputModel
        {
            public int SearchType { get; set; }
            public string searchText { get; set; }
            public int TenantID { get; set; }
            public int MaterialMasterID { get; set; }

            public int GradeID { get; set; }
            public int MTypeID { get; set; }
            public string BatchNo { get; set; }
            public int LocationID { get; set; }
            public int KitID { get; set; }
            public int AccountID_New { get; set; }
            public int TenantID_New { get; set; }
            public int UserID_New { get; set; }
            public int UserTypeID_New { get; set; }
            public string IndustryXML { get; set; }
            public int GEN_MST_Industry_ID { get; set; }
            public int WarehouseID { get; set; }
            public string OEMPartNo { get; set; }
            public int MenuID { get; set; }
            public int Rownumber { get; set; }
            public int NofRecordsPerPage { get; set; }
            public int CartonID { get; set; }
            public int slocid { get; set; }
            public int DrawTypeID { get; set; }
            public int IsExcel { get; set; }

            public int ActivestockDetailsID { get; set; }
        }
        public class GetMiscellaneousReceiptModel
        {
            public int MaterialMasterID { get; set; }
            public int WarehouseID { get; set; }
            public int TenantID { get; set; }
        }

        public class GetProjectStockListModel
        {
            public int WareHouseId { get; set; }
            public string ProjectRefNo { get; set; }
            public int MaterialMasterID { get; set; }
            public int TenantID { get; set; }

        }

        public class UpdateProjectstockTransferQtyModel
        {
            public int UserID { get; set; }
            public string ProjectRefNo { get; set; }
            public int WareHouseId { get; set; }
            public int TenantID { get; set; }
            public List<Materialdata> OBJ { get; set; }

        }

        public class Materialdata
        {
            public decimal TransferQuantity { get; set; }
            public int MaterialMasterID { get; set; }
            public int MaterialTransactionID { get; set; }
            public string GRNDate { get; set; }
        }

        public class UpdateMisslleniousReceiptIputModel
        {
            public int MaterialMasterID { get; set; }
            public string POQunatity { get; set; }
            public string LOCATION { get; set; }
            public string MCode { get; set; }
            public int IsDamaged { get; set; }
            public int HasDiscipency { get; set; }
            public string Remarks { get; set; }
            public int StorageLocationID { get; set; }
            public string BatchNo { get; set; }
            public string MfgDate { get; set; }
            public string ExpDate { get; set; }
            public string ProjectRefNo { get; set; }
            public string MRP { get; set; }
            public int UpdatedBy { get; set; }
            public int TenantID { get; set; }
            public int CartonID { get; set; }
            public int WareHouseId { get; set; }
            public string ltBaseUOM { get; set; }
            public string QADLocation { get; set; }

            public string qadAccount { get; set; }
            public string SerialNO { get; set; }

            public decimal DocQuantity { get; set; }

            public string atcMateialCode { get; set; }

            public string Grade { get; set; }


        }

        public class GetMiscellaneousReceiptTableDataModel
        {
            public int MaterialMasterID { get; set; }
            public int WarehouseID { get; set; }
            public int TenantID { get; set; }

        }
        public class GetMiscellaneousIssueModel
        {
            public int MaterialMasterID { get; set; }
            public int WarehouseID { get; set; }
            public int TenantID { get; set; }
        }
        public class GetColumnsInputModel
        {
            public int AccountID { get; set; }
            public int TenantID { get; set; }
            public int UserID { get; set; }
            public int MenuID { get; set; }
        }
        public class UpsertColumnsInputModel
        {
            public List<XMLFormat> data { get; set; }
            public int AccountID { get; set; }
            public int TenantID { get; set; }
            public int UserID { get; set; }
            public int MenuID { get; set; }
        }
        public class XMLFormat
        {
            public int GEN_MST_DynamicColumn_ID { get; set; }
            public int IsDisplay { get; set; }
            public int DisplaySequence { get; set; }
            public int SortSequence { get; set; }
            public int SortType { get; set; }
            public int IsSort { get; set; }
        }
        public class PickingItemModel
        {
            public string BatchNo { get; set; }
            public string ProjectRefNo { get; set; }
            public string Remarks { get; set; }
            public decimal pickQuantity { get; set; }
            public decimal SOQunatity { get; set; }
            public string part { get; set; }
            public string um { get; set; }
            public string qadAccount { get; set; }
            public string QADLocation { get; set; }
            public int MaterialMasterID { get; set; }
            public int ActiveStockDetailsID { get; set; }
            public string Location { get; set; }
            public int GMDID { get; set; }
            public int SLOCID { get; set; }
            public int WarehouseID { get; set; }
            public int AccountID { get; set; }
            public int UserID { get; set; }
            public int TenantID { get; set; }

        }


        public class GetCurrentStockReportInputModel
        {
            public int TenantID { get; set; }
            public int MaterialMasterID { get; set; }
            public int MTypeID { get; set; }
            public int DrawTypeID { get; set; }
            public string BatchNo { get; set; }
            public int LocationID { get; set; }
            public int KitID { get; set; }
            public int AccountID { get; set; }
            public int UserTypeID { get; set; }
            public int UserID { get; set; }
            //public string IndustryXML { get; set; }
            public List<IndustryXML> IndustryXML { get; set; }
            public int IndustryID { get; set; }
            public int WarehouseID { get; set; }
            public string OEMPartNo { get; set; }
            public int MenuID { get; set; }
            public int Rownumber { get; set; }
            public int NofRecordsPerPage { get; set; }
            public string SearchType { get; set; }
            public string searchText { get; set; }
            public int IsExcel { get; set; }
            public int CartonID { get; set; }
            public string DataJson { get; set; }
            public int SlocID { get; set; }
        }

        public class IndustryXML
        {
            public int MM_MST_Attribute_ID { get; set; }
            public int MM_MST_AttributeLookup_ID { get; set; }
            public int GEN_MST_Industry_ID { get; set; }
            public string AttributeValue { get; set; }
        }

        public class MaterialAttribute
        {
            public int MM_MST_Material_ID { get; set; }
            public int MM_MST_AttributeLookup_ID { get; set; }
            public int MM_MST_Attribute_ID { get; set; }
            public string AttributeValue { get; set; }
        }

        

            public class ReserveStockModelItems
            {
            public int tenantid { get; set; }
            public int materialmasterid { get; set; }
            public int warehouseid { get; set; }
            public int rownumber { get; set; }
            public int nofrecordsperpage { get; set; }
            public int isexcel { get; set; }
            public int loginaccountid { get; set; }
            public int loginuserid { get; set; }
            public int logintanentid { get; set; }

            }
    }
}
