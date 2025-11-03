using Simpolo_Endpoint.BO;
using Simpolo_Endpoint.DTO;
using Simpolo_Endpoint.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Simpolo_Endpoint.DAO.HHTInterface
{
    public interface IInternalTransfer
    {    
            Task<InventoryDTO> TransferPallettoLocation(InventoryDTO obj, int UserID);
            Task<string> GetConatinerLocationBin(string ContainerCode, string WarehouseId, string UserID , string LocationCheck);
            Task<TransferBO> GetAvailbleQtyList(TransferBO transferBO);
            Task<List<Inventory>> GetActivestockStorageLocations(string Mcode , int UserId);
            Task<TransferBO> UpsertBinToBinTransferItem(TransferBO transferBO);
            Task<List<Inventory>> GetSKUList(string outboundId);
            Task<List<Inventory>> GetLocationsBySKU(string Mcode, string BatchNo, string IsMoreOptions, string IsWorkOrder);
            Task<List<Inventory>> GetDockLocations();

            Task<List<Inventory>> GetLoadingPoints(string TenantID, int UserID, string WarehouseId,string VehicleNumber);

            Task<List<Inventory>> GetLocations(string RefNumber, string WarehouseId, string TenantID,int UserID,int InboundID,string IsScerario);


           Task<List<Inventory>> GetLocationsBatchPicking(string RefNumber, string WarehouseId, string TenantID, int UserID, int InboundID, string IsScerario,string MaterialCode,string BatchNo,string Grade,string vlpdid);


        Task<int> WorkOrderLineItemComplete(string vLPDID);

            Task<List<Inventory>> MaterialTransferBlockComplete(int TransferRequestID,int UserID,int BlockReasonID);
            Task<Inventory> TransferPallettoLocation_Putaway(Inventory obj);
            Task<TransferBO> UpsertPalletBuilding(TransferBO transferBO);
            Task<List<Inventory>> GetSlocWiseActiveStock(Inventory obj);
            Task<List<Inventory>> UpdateMaterialTrasnfer(Inventory obj);

            Task<List<Inventory>> UpsertMaterialTransferItem_HHT(Inventory obj);
             Task<DataSet> GetStorageLocations();
            Task<DataSet> GetTransferOrderNos(BO.TransferBO Transferinfo);
            Task<DataSet> GetTransferBlockReasons();

            Task<DataSet> BatchTransfertoPick(BO.TransferBO Transferinfo); 

            Task<List<Inventory>> UpsertTranferDetails_BG(Inventory obj);

            Task<List<Inventory>> UpsertPalletConsolidationTransfer(Inventory obj);
        

            Task<DataSet> ItemMasterPrint(string TenanatId, string Labletype);
    }
}
