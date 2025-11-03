using Simpolo_Endpoint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simpolo_Endpoint.DAO.interfaces
{
    public interface IHouseKeeping
    {
        Task<Payload<string>> GetFastMovingLocsTransferList(GetFastMovingLocsTransferListModel items);
        Task<Payload<string>> GetTransferRequestNumbers_HHT(TransferRequestModel items);

        Task<Payload<string>> GetTransferRequestNumbers_HHT_PalletConsolidate(TransferRequestModel items);

        
        Task<Payload<string>> DeleteFastMovingLocsTransfer(DeleteFastMovingLocsTransferModel items);

        Task<Payload<string>> RepostMaterialDetails_SAP(InitiateToProcessModel items);
        Task<Payload<string>> ApprovedBatchGradeTransfer(GetInternalTransferHeaderModel items);
        Task<Payload<AuthResponce>> GetInternalTransferHeader(GetInternalTransferHeaderModel items);
        Task<Payload<string>> UpsertInternalTransferHeader(UpsertInternalTransferHeaderModel items);
        Task<Payload<string>> GetInternalTransferDetails(GetInternalTransferDetailsModel items);

        Task<Payload<string>> GetInternalTransferDetails_VLPD(GetInternalTransferDetailsModel items);
        Task<Payload<string>> GetAvailableQty(GetAvailableQtyModel items);
        Task<Payload<string>> UpsertInternalTransferDetails(UpsertInternalTransferDetailsModel items);
        Task<Payload<string>> UpsertExpiryDateTransferRequestDetails(UpsertExpiryDateTransferRequestDetailsModel items);
        Task<Payload<string>> DeleteInternalTransferDetails(DeleteInternalTransferDetailsModel items);
        Task<Payload<string>> GetCycleCountList(GetCycleCountListModel items);
        Task<Payload<string>> DeleteCyclecountlist(DeleteccOrderModel obj);
        Task<Payload<string>> UpsertCycleCountHeader(UpsertCycleCountHeaderModel items);
        Task<Payload<string>> GetCycleCountEntityConfiguration(GetCycleCountEntityConfigurationModel items);
        Task<Payload<string>> GetCycleCountTransactionList(GetCycleCountlistModel obj);
        Task<Payload<string>> GetCycleCountTransactionListByStatus(GetCycleCountTransactionListModel getCycleCountTransaction);
        Task<Payload<string>> GetCCBlockedLocations(GetCCBlockedLocationsModel obj);
        Task<Payload<string>> GetStockAadjustmentNewList(GetStockAadjustmentNewListModel obj);
        Task<Payload<string>> DeleteCyclecountDetails(DeleteccOrderModel obj);
        Task<Payload<string>> CreateCC_EntityConfiguration(CreateCC_EntityConfigurationModel items);
        Task<Payload<string>> GetCC_ShipmentVerificationDetails(GetCC_ShipmentDetailsModel items);
        Task<Payload<string>> SaveCC_ShipmentVerificationDetails(SaveCC_ShipmentDetailsModel items);
        Task<Payload<string>> UpsertCCtransactionInitiate(UpsertCCtransactionModel items);
        Task<Payload<string>> UpsertBinToBinTransferItem(UpsertBinToBinTransferModel obj);
        Task<Payload<string>> GetUnmappedSKUList(GetUnmappedSKUListModel items);
        Task<Payload<string>> SaveUnmappedZoneSKUData(GetUnmappedSKUListModel obj);
    }
}
