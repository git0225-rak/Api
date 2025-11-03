using Simpolo_Endpoint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simpolo_Endpoint.DAO.interfaces
{
    public interface IOutbound
    {
        Task<Payload<string>> GetPendingOBDForVLPDCreation(GetPendingOBDForVLPDCreationModel items);
        Task<Payload<string>> GetPick_CheckPendingList(GetPick_CheckPendingListModel items);
        Task<Payload<string>> GetPGIPendingList(GetPGIPendingListModel items);
        Task<Payload<string>> GetDeliveriesPendingList(GetDeliveriesPendingListModel items);
        Task<Payload<string>> GetPODPendingList(GetPODPendingListModel items);
        Task<Payload<string>> GetOBDRevertList(GetOBDRevertListModel items);
        Task<Payload<string>> GetOBDReleaseList(GetOBDReleaseListModel items);
        Task<Payload<string>> GetSOsList(GetSOsListModel getSOsList);
        Task<Payload<string>> saveBulkReleaseItemsForOBD(saveBulkReleaseItemsForOBDModel items);
        Task<Payload<string>> GetOBDwiseItem(GetOBDwiseItemModel items);
        Task<Payload<string>> SetOBDRevert(SetOBDRevertModel items);
        Task<Payload<string>> UpsertUpdateDelivery(UpsertUpdateDeliveryModel items);
        Task<Payload<string>> UpsertOBD(UpsertOBDInputModel upsertOBDInput);
        Task<Payload<string>> GetPickList(PickListInputModel pickListInput);
        Task<Payload<string>> GetPickedItems(PickedItemsInputModel pickedItemsInput);
        Task<Payload<string>> InsertPickItem(InsertPickItemInputModel insertPickItemInput);
        Task<Payload<string>> DeletePickitem(DeletePickItemsinputModel deletePickItemsinput);
        Task<Payload<string>> GetSOLineItems(GetSOLineItemsInputModel sOLineItemsInput);
        Task<Payload<string>> GetPSNMaterialDetails(DeliveryPackslipModel items);
        Task<Payload<string>> LoadUOMs(DeliveryPackslipModel items);
        Task<Payload<string>> LoadPSNMaterialItems(DeliveryPackslipModel items);
        Task<Payload<string>> UpsertPackingSlipAddMaterialInfo(UpsertPackingSlipAddMaterialInfo items);
        Task<Payload<string>> GetSearchOutboundDetails(GetSearchOutboundDetailsModel getSearchOutboundDetails);
        Task<Payload<string>> UpdateShipmentDetails(UpdateShipmentDetailsModel updateShipmentDetails);
        Task<Payload<string>> GetPendingGoodsOutList(GetPendingGoodsOutInputModel getPendingGoodsOutInput);
        Task<Payload<string>> UpsertPackingSlipNumber(UpertPackingSlipInputModel insertPackingSlipInput);
        Task<Payload<string>> UpdatePackingSlipInformation(UpdatePackingSlipInformationModel updatePackingSlipInformation);
        Task<Payload<string>> GetDeliveryNoteHeader(GetDeliveryNoteHeaderinputModel getDeliveryNoteHeaderinput);
        Task<Payload<string>> GetInitiateOutboundDelivery(GetInitiateOutboundDeliveryModel items);
        Task<Payload<string>> UpsertDDLineItems(UpsertDDLineItemsModel items);
        Task<Payload<string>> GetPickCheckPick(GetPickCheckPickModel items);
        Task<Payload<string>> UpsertUpdatePGI(UpsertUpdatePGIModel items);
        Task<Payload<string>> GetPackingSlipData(GetPackingSlipDataModel items);
        Task<Payload<string>> GetRouteCode(GetRouteCodeModel items);
        Task<Payload<string>> GetItemMasterLoad(GetItemMasterLoadModel items);
        Task<Payload<string>> GetPackingSlipNumberData(GetPackingSlipNumberDataModel items);
        Task<Payload<string>> GetPickMaterial(GetPickMaterialModel items);
        Task<Payload<string>> UpdateDeliveryDetails(UpdateDeliveryDetailsModel items);
        Task<Payload<string>> WOComponentIssue_GetList(WOComponentIssue_GetListModel items);
        Task<Payload<string>> WOComponent_Initiate(QADRequestObj items);
        Task<Payload<string>> AddDelvDocLineItem_Click(AddDelvDocLineItem_ClickModel items);
        Task<Payload<string>> Delete_DeliveryDocLineItems(Delete_DeliveryDocLineItemsModel items);
        Task<Payload<string>> GeneratePGIInvoiceData(GeneratePGIInvoiceModel items);
        Task<Payload<string>> DeletePSNMaterialitemDetail(DeliveryPackslipModel items);
        Task<Payload<string>> DeletePSNMaterialItems(DeliveryPackslipModel items);
        Task<Payload<string>> GetPickingRevertData(SetOBDRevertNewModel items);
        Task<Payload<string>> OBDLinePickRevert(SetOBDRevertNewModel items);
        Task<Payload<string>> GenerateDeliveryPackSlip(GetPackingSlipDataModel items);
        Task<Payload<string>> UpsertDockManagementData(ReceivingDockManagementDataModel items);
        Task<Payload<string>> GetDockManagementData(DashBoardInputModel items);

    }
}

