using FWMSC21Core.Entities;
using FWMSC21Core_BusinessEntities.Entities;
using Simpolo_Endpoint.DTO;
using Simpolo_Endpoint.Entities;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Simpolo_Endpoint.DAO.HHTInterface
{
    public interface IOutbound
    {
        Task<List<Outbound>> GetOBDRefNos(Outbound outbound);

        Task<List<Outbound>> GetVehicleNumbers_Loading(Outbound outbound);

        
        Task<OutboundDTO> GetOBDItemToPick(OutboundDTO outbound);

        Task<OutboundDTO> GetOBDItemToPick_BatchGrade(OutboundDTO outbound);

        Task<OutboundDTO> GetOBDItemToPick_PalletConsolidate(OutboundDTO outbound);
        

        Task<OutboundDTO> OBDSkipItem(OutboundDTO outbound);
        Task<BO.Outbound> UpdatePickItem(BO.Outbound outbound);
        Task<bool> CheckOBDSO(Outbound outbound);
        Task<List<Outbound>> GetOBDNosUnderSO(Outbound outbound);
        Task<string> CheckContainerOBD(string CartonNo, string OutboundID);
        Task<List<Outbound>> GetOBDItemsForPicking(Outbound outbound);
        Task<DataSet> GetOpenVLPDNos(BO.VLPD vLPD);
        Task<BO.VLPD> GetItemToPick(BO.VLPD VLPD);
        Task<BO.VLPD> VLPDSkipItem(BO.VLPD VLPD);
        Task<BO.VLPD> UpsertPickItem(BO.VLPD VLPD);
        DataSet GetVLPDPickedList(BO.VLPD vLPD, out string Result);
        DataSet GetOBDPickedList(BO.Outbound outbound, out string Result);
        Task<BO.VLPD> DeleteVLPDPickedItems(BO.VLPD vLPD);
        Task<DataSet> GetOpenVLPDNosForSorting(BO.VLPD vLPD);
        Task<DataSet> GetOpenLoadsheetList(string Tenantid, string AccountId);
        Task<DataSet> GetPendingOBDListForLoading(string Tenantid, string AccountId);
        Task<string> UpsertLoadCreated(BO.VLPD VLPD);
        Task<string> UpsertLoad(BO.VLPD VLPD);
        Task<string> LoadVerification(string LoadNumber, string UserId);
        Task<List<Outbound>> GetMaterialsUnderSOForPacking(string SONumber, int AccountID, int UserId);
        Task<List<Outbound>> GetMSPsForPacking(string SodetailsID);
        Task<Outbound> UpsertPackItem(Outbound outbound);
        Task<Outbound> PackComplete(Outbound outbound);
        Task<Outbound> LoadSheetGeneration(Outbound outbound);
        Task<Outbound> GetSOCountUnderLoadSheet(Outbound outbound);
        Task<Outbound> UpsertLoadDetails(Outbound outbound);
        Task<bool> CheckSO(Outbound outbound);
        Task<bool> CheckCarton(Outbound outbound);
        Task<List<Outbound>> GetPackingCartonInfo(Outbound outbound);
        Task<List<Outbound>> GetRevertOBDList(Outbound outbound);
        Task<List<Outbound>> GetRevertSOList(Outbound outbound);
        Task<List<Outbound>> GetRevertSOOBDInfo(Outbound outbound);
        Task<List<Outbound>> GetRevertCartonCheck(Outbound outbound);
        Task<List<Outbound>> GetScanqtyvalidation(Outbound outbound);
        Task<WorkOrderOutbound> WorkOrderPicking(WorkOrderOutbound outbound);
        Task<List<Outbound>> UpsertHHTOBDRevert(Outbound outbound);
        Task<List<Outbound>> Get_WOListToRevert(Outbound outbound);
        Task<List<Outbound>> GetWOItemsForPicking(Outbound outbound);
        Task<List<OutboundDetails>> UpsertHHTWORevert(string xmlstring);
        Task<Outbound> LoadComplete(Outbound outbound);
        Task<List<Outbound>> UpsertLoadItem(Outbound obj);
        Task<List<Outbound>> UpsertLoadedItem(Outbound obj);
        Task<List<Outbound>> GetSkuListForLoading(string OutboundID, int AccountID, int UserId);
    }
}
