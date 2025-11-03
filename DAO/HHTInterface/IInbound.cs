using FWMSC21Core.Entities;
using Simpolo_Endpoint.DTO;
using Simpolo_Endpoint.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Simpolo_Endpoint.DAO.HHTInterface
{
    public interface IInbound
    {
        Task<Payload<string>> GET_INB_GRNList(InboundTrackingModel obj);
        Task<List<Inbound>> GetStoreRefNos(Inbound items);
        Task<BO.Inbound> UpdateReceiveItem(BO.Inbound inbound);
        Task<DataSet> GetSkipReasonList(string type);
        Task<string> CheckContainer(string CartonNo, string InboundID);
        Task<BO.Inbound> GetReceivedQty(BO.Inbound inbound);
        Task<BO.Inbound> CheckDock(BO.Inbound inbound);
        Task<DataSet> GetStorageLocations();
        Task<BO.PutAway> GetItemTOPutAway(BO.PutAway PutAway);
        Task<BO.PutAway> SkipItem(BO.PutAway PutAway);
        Task<BO.PutAway> UpsertPutAwayItem(BO.PutAway PutAway);
        Task<BO.PutAway> CheckPutAwayItemQty(BO.PutAway PutAway);
        Task<List<Suggestion>> GeneratePutawaySuggestion(SearchCriteria oCriteria);
        Task<string> ChekContainerLocation(string cartoncode, string WarehouseID);
        Task<GRNDetails> GetMiscXMLData(string batchNo, string projectRefNo, string Qty, string remks, string part, string um, string conv, string qadAccount, string QADLocation, string uniqueID = "");
        Task<GRNDetails> GetGRNXMLData(string InboundId, string InvoiceNumber, string PONumber, int InboundType, string Remarks);
        Task<GRNDetails> GetGRNRevertXMLData(int grnHeaderID, int isSupplierRtn);
        Task<GRNDetails> GetCycleCountXMLData(InitiateStockModel stockObj);
        Task<GRNDetails> QADUpdateInventorystatus(int TransferRequestID, int TransferTypeID, string uniqueID);
        Task<GRNDetails> GetSOPGIXMLDATA(int OutboundID);
        Task<GRNDetails> QADShipmentRequest(int OutboundID);
        Task<WorkOrderResponse> GetItemLevelWOPGIXMLDATA(QADRequestObj qadWOData);
    }
}

