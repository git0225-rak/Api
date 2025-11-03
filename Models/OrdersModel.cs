using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simpolo_Endpoint.Models
{
    public class GetStockPostingModel
    {
        public int POHeaderID { get; set; }
        public int MMID { get; set; }
    }
    public class GetCurrentStockDynamicDataModel
    {
        public int POHeaderID { get; set; }
        public int MMID { get; set; }

    }

    public class GetSuppliersReturnsModel
    {
        public int POHeaderID { get; set; }
        public int SupplierInvoiceID { get; set; }
        public int WarehouseID { get; set; }
    }
   
    public class GetStockPostingInputModel
    {
        public int POHeaderID { get; set; }
        public int MMID { get; set; }
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
        public string PONumber { get; set; }
    }
    public class SaveTransferRequestModel
    {
        public int UserID { get; set; }
        public int PONumber { get; set; }
        public string XML { get; set; }

    }
    public class GetSupplierReturnlistModel
    {
        public int POHeaderID { get; set; }
        public int SupplierInvoiceID { get; set; }
        public int WareHouseID { get; set; }
    }




    //public class items
    //{
    //    public decimal Qty { get; set; }
    //    public decimal TotalQty { get; set; }
    //    public string ToSlocID { get; set; }
    //    public string FromSLoc { get; set; }
    //    public string BatchNo { get; set; }
    //    public string SKU { get; set; }
    //    public string PONumber { get; set; }
    //    public string Location { get; set; }
    //    public string Carton { get; set; }
    //}
    public class InitiateStockModel
    {
        public int CCM_TRN_CycleCount_ID { get; set; }
        public decimal MRP { get; set; }
        public string MfgDate { get; set; }
        public string ExpDate { get; set; }
        public string SLOC { get; set; }
        public string MCode { get; set; }
        public string Location { get; set; }
        public decimal LogicalQuantity { get; set; }
        public decimal PhysicalQuantity { get; set; }
        public string BatchNo { get; set; }
        public string ProjectRefNo { get; set; }
        public string CartonCode { get; set; }
        public string UniqRef { get; set; }
        public int CCID { get; set; }
        public int UoM { get; set; }
        public decimal UoMQty { get; set; }
        public string Remarks { get; set; }
        public string effDate { get; set; }
        public string crAcct { get; set; }
        public string yn { get; set; }
        public int TenantID { get; set; }
        public string TenantCode { get; set; }
        public string crCc { get; set; }
        public string QADLocation { get; set; }
        public int CC_TrnID { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string SerialNo { get; set; }

        public List<Dataitems> DataJson { get; set; }

    }

    public class Dataitems
    { 
        public int CCM_TRN_CycleCount_ID { get; set; }
        public string CartonCode { get; set; }
        public string Location { get; set; }
        public decimal LogicalQuantity { get; set; }
        public string MCode { get; set; }
        public decimal PhysicalQuantity { get; set; }
        public int RID { get; set; }
        public string BatchNo { get; set; }
        public string MRP { get; set; }
        public string ProjectRefNo { get; set; }
        public string SerialNo { get; set; }
        public string MfgDate { get; set; }
        public string ExpDate { get; set; }
        public string SLoc { get; set; }
        public string TenantCode { get; set; }
        public int  TenantID { get; set; }
        public string  Grade { get; set; }
    }




    public class StockPostingModel
    {
        public int UserID { get; set; }
        public string PONumber { get; set; }
        public List<XMLposting> xmlpost { get; set; }
    }


    public class XMLposting
    {
        public string BatchNo { get; set; }
        public string ProjectRefNo { get; set; }
        public string Location { get; set; }
        public string Carton { get; set; }
        public string PONumber { get; set; }
        public string SKU { get; set; }
        public string FromSLoc { get; set; }
        public int ToSlocID { get; set; }
        public decimal  TotalQty { get; set; }
        public decimal  Qty { get; set; }
    }
    public class MaterialQtyUpdateModel
    {
        public decimal ReceivedQuantity { get; set; }
        public decimal RequestedQuantity { get; set; }
        public int HeaderID { get; set; }
        public int LineNumber { get; set; }
        public int MaterialMasterID { get; set; }
        public string BatchNo { get; set; }
        public int FromStorageLocationID { get; set; }
        public int RequestDetailsID { get; set; }
        public int UserID { get; set; }
        public string ProjectRefNo { get; set; }
        public int EmpRequestStatusID { get; set; }

    }
    public class EmployeeRequestConfirmationModel
    {
        public int RequestHeaderID { get; set; }
        public string QONumber { get; set; }
        public string QADAccount { get; set; }
        public string Remarks { get; set; }
        public int UserID { get; set; }

    }
    public class EmployeeRequestVerificationModel
    {
        public int RequestHeaderID { get; set; }
        public int Rownumber { get; set; }
        public int NofRecordsPerPage { get; set; }

    }

    public class InitiateToProcessModel
    {
      
        public string SapMaterialRefno { get; set; }

        public int TransferRequestedID { get; set; }
        public int UserID { get; set; }
        public int TransferTypeID { get; set; }

        

    }
    public class GetSuccessInfoCaptureModel
    {
        public int CID { get; set; }
        public int AccountID { get; set; }
        public int CCM_CNF_AccountCycleCount_ID { get; set; }

    }

    public class MasterDetailsSetLFOModel
    {
        public int CCM_TRN_CycleCount_ID { get; set; }
        public int OperationFlag { get; set; }
        public int UserLoggedId { get; set; }

    }

    public class UpsertQualityModel
    {
        public int RequestHeaderID { get; set; }
    }

    public class LabSampleRequest_InitiatePickModel
    {
        public int TenantID { get; set; }
        public int RequestHeaderID { get; set; }
        public int UserID { get; set; }
        public string Roles { get; set; }
    }
    public class MaterialPickQtyUpdateModel
    {
        public int RequestDetailsID { get; set; }
        public decimal ReceivedQuantity { get; set; }
        public decimal PickedQuantity { get; set; }
        public int UserID { get; set; }
        public int HeaderID { get; set; }
    }

    public class TransferSupplierReturnsModel
    {
        public int POHeaderID { get; set; }
        public int SupplierInvoiceID { get; set; }
        public int WareHouseID { get; set; }
        public int TenantID { get; set; }
        public int DockID { get; set; }
        public int UserID { get; set; }
        public int AccountID { get; set; }
        public List<SupplierRt> SupplierRt { get; set; }
    }

    public class SupplierRt
    {
        public string Line { get; set; }
        public string MCode { get; set; }
        public int MaterialMasterID { get; set; }
        public string MfgDate { get; set; }
        public string ExpDate { get; set; }
        public string SerialNo { get; set; }
        public int MaterialMasterIDUomUID { get; set; }
        public string BatchNo { get; set; }
        public string ProjectRefNo { get; set; }
        public string MRP { get; set; }
        public decimal PickedQty { get; set; }
        public decimal PendingReturnQty { get; set; }
        public decimal ReturnQty { get; set; }
        public string KitplannerID { get; set; }
        public bool Isselected { get; set; }
        public int IsKitParent { get; set; }
        public string Location { get; set; }
        public string CartonCode { get; set; }
        public string StorageLocation { get; set; }
        public int GRNUpdateID { get; set; }
        public int POLineNumber { get; set; }
    }

    public class CurrentStock_PrintLabelModel
    {
        public List<CurrentStock_Report> currentStock_report { get; set; }
        public CurrentStockSecondLabelPrint Currentstocksecondlabelprint { get; set; }
        public string LabelID { get; set; }
        public string ipaddress { get; set; }
        public int port { get; set; }
        public int PrinterType { get; set; }
        public int IsSecondaryLabelprint { get; set; }//Added By Ramsai

    }

    public class CurrentStock_Report
    {
        public string MfgDate { get; set; }
        public string ExpDate { get; set; }
        public string SerialNo { get; set; }
        public string BatchNo { get; set; }
        public string ProjectRefNo { get; set; }
        public string SupplierLot { get; set; }
        public string MCode { get; set; }
        public int TenantID { get; set; }
        public string KitCode { get; set; }
        public string PrintQty { get; set; }
        public string MRP { get; set; }
        public string LineNo { get; set; }
        public string HUNo { get; set; }
        public string HUSize { get; set; }
        public string BoxSerialNo { get; set; }
        public string Grade { get; set; }
        public string Size { get; set; }

    }

    public class Print_MLabelModel
    {
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
        public string Zone { get; set; }
        public string Size { get; set; }
        public string Grade { get; set; }
        public string BoxSerialNo { get; set; }
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

    public class UpsertCycleCountDetailsModel
    {
        public string Result { set; get; }
        public string Location { set; get; }
        public int UserID { set; get; }
        //public int LocationID { set; get; }
        public string ExpDate { set; get; }
        public string MfgDate { set; get; }
        public string SerialNo { set; get; }
        public string BatchNo { set; get; }
        public string MRP { set; get; }
        public string ProjectRefNo { set; get; }
        public string CCName { set; get; }
        public string SKU { set; get; }
        public string CCQty { set; get; }
        public string Count { set; get; }
        //public int CycleCountID { set; get; }
        //public int AccountCycleCountID { set; get; }
        //public int MSTCycleCountID { set; get; }
        //public int CycleCountEntityID { set; get; }
        //public int EntityID { set; get; }
        public string Container { set; get; }
        public int AccountID { set; get; }
        public int TenantID { set; get; }    
        public int WarehouseID { set; get; }
        public string StorageLocation { set; get; }
        //public string CycleCountCode { set; get; }
        public int CCM_TRN_CycleCount_ID { set; get; }
    }
    public class CurrentStockSecondLabelPrint
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
           

   
