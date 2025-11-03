using Simpolo_Endpoint.BO;
using Simpolo_Endpoint.DTO;
using Simpolo_Endpoint.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Simpolo_Endpoint.DAO.HHTInterface
{
    public interface IHouseKeeping
    {
        Task<DataSet> GetLiveStockData(InventoryDTO liveStock);
        Task<DataSet> GetWarehouse(LiveStock liveStock);
        Task<DataSet> GetTenants(LiveStock liveStock);
        Task<string> CheckLoction(BO.LiveStock stock);
        Task<string> CheckTenatMaterial(string Mcode, int AccountID, string TenantName);
        Task<string> ValidateCartonLiveStock(BO.LiveStock liveStock);
        Task<string> UpsertStock(string XML, string UserID);
        Task<DataSet> GetItemPutawaySuggestion(InventoryDTO liveStock);

        Task<DataSet> GetMachineNos(InventoryDTO items);
        Task<DataSet> GetVehicleNos(InboundDTO obj);

        Task<DataSet> GetVehicleNosDock(InboundDTO obj);
        Task<DataSet> GetVehicleTypes();

        Task<DataSet> GetDocksByTenant(string TenantID);
        Task<DataSet> Upsert_VehicleGateManagement_HHT(InboundDTO items);
    }
}
