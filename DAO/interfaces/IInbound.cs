using Simpolo_Endpoint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simpolo_Endpoint.DAO.interfaces
{
    public interface IInbound
    {
        Task<Payload<string>> GetInboundDetails(GetInboundDetailsModel items);
        Task<Payload<string>> GetInboundTracking_ShipmentTransit(InboundTracking_ShipmentTransitModel items);
        Task<Payload<string>> GetInboundTracking_ShipmentExpected(InboundTracking_ShipmentExpectedModel items);
        Task<Payload<string>> GetInboundTracking_ShipmentInProcess(InboundTracking_ShipmentInProcessModel items);
        Task<Payload<string>> GetRevertInboundList(GetRevertInboundListModel items);
        Task<Payload<string>> GetInBoundPOInvoiceDetails(GetInBoundPOInvoiceDetailsModel items);
        Task<Payload<string>> UpsertInboundBasicData(UpsertInboundBasicDataModel items);
        Task<Payload<string>> AddOrderOrInvoiceItems(AddOrderOrInvoiceItemsModel items);
        Task<Payload<string>> UpsertInBoundPOInvoiceDetails(UpsertInBoundPOInvoiceDetailsModel items);
        Task<Payload<string>> GetASNDetails(GetASNDetailsModel items);
        Task<Payload<string>> GetSearchInboundDetails(GetSearchInboundDetailsModel items);
        Task<Payload<string>> GetRTRDetails(GetRTRDetailsModel items);
        Task<Payload<string>> GetGoodsInSuggestedPutAwayList(GetGoodsInSuggestedPutAwayListModel items);
        Task<Payload<string>> Get_ReceiveMSPsPutawayList(Get_ReceiveMSPsPutawayListModel items);
        Task<Payload<string>> ReceiveMSPsPutawayList(ReceiveMSPsPutawayListModel items);
        Task<Payload<string>> DeleteGoodsInRecieveddetails(DeleteGoodsInRecieveddetailsModel items);
        Task<Payload<string>> UpdateASNDetails(UpdateASNDetailsModel items);
        Task<Payload<string>> UpdateShipmentExpectedDetails(UpdateShipmentExpectedDetailsModel items);
        Task<Payload<string>> UpsertReceivingDockManagement(UpsertReceivingDockManagementModel items);
        Task<Payload<string>> GetReceivingDockManagementDetails(GetReceivingDockManagementDetailsModel items);

        Task<Payload<string>> UNBLOCKPGRInbound(PGRUnblockModel items);

        
        Task<Payload<string>> DeleteReceivingDockManagementDetails(DeleteReceivingDockManagementDetailsModel items);
        Task<Payload<string>> Get_ShipmentReceivedDetails(Get_ShipmentReceivedDetailsModel items);
        Task<Payload<string>> ShipmentReceivedDetails(ShipmentReceivedDetailsModel items);

        Task<Payload<string>> UpsertShipmentDetailsBasedonInwardType(ShipmentReceivedDetailsModel items);

        Task<Payload<string>> GetGRNUpdateDetails(GetGRNUpdateDetailsModel items);
        Task<Payload<string>> FetchGRNDataForInbound(FetchGRNDataForInboundModel items);
        Task<Payload<string>> CheckIsShortGRN(CheckIsShortGRNModel items); 
        Task<Payload<string>> GetDiscrepancyDetails(GetDiscrepancyDetailsModel items);
        Task<Payload<string>> GetDiscrepancyLineItems_PageLoad(GetDiscrepancyLineItems_PageLoadModel items);
        Task<Payload<string>> SaveDiscrepancyDetails(SaveDiscrepancyDetailsModel items);
        Task<Payload<string>> UpsertDiscrepancyDetails(UpsertDiscrepancyDetailsModel items);
        Task<Payload<string>> DeleteDiscrepancyDetails(DeleteDiscrepancyDetailsModel items);
        Task<Payload<string>> CheckDiscrepency_OnPageLoad(CheckDiscrepency_OnPageLoadModel items);
        Task<Payload<string>> UpdateShipmentVerificationDetails(UpdateShipmentVerificationDetailsModel items);
        Task<Payload<string>> GetShipmentVerificationDetails(GetShipmentVerificationDetailsModel items);
        Task<Payload<string>> RevertGRNDetails(RevertGRNDetailsModel items);
        Task<Payload<string>> ShipmentCloseDetails(ShipmentCloseDetailsModel items);
        Task<Payload<string>> GetInboundRevertDetails(GetInboundRevertDetailsModel items);
        Task<Payload<string>> GetRevertGRNDetails(GetRevertGRNDetailsModel items);
        Task<Payload<string>> RevertShipmmentExpected(RevertShipmmentExpectedModel items);
        Task<Payload<string>> RevertShipmmentReceived(RevertShipmmentReceivedModel items);
        //Task<Payload<string>> CreateGRNEntryAndPostDatatoSAP(int InboundId, string PONumber, string InvoiceNumber, int InboundType, string Remarks);
        Task<Payload<string>> CreateGRNEntryAndPostDatatoSAP(CreateGRNEntryAndPostDatatoSAPModel items);
        Task<Payload<string>> RTR_PrintLabels(RTR_PrintLabelModel printobj);


    }
}
