using FWMSC21Core.Entities;
using Simpolo_Endpoint.BO;
using Simpolo_Endpoint.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Simpolo_Endpoint.DAO.HHTInterface
{
    public interface ICycleCount
    {
        Task<DataSet> GetCCNames(BO.CycleCount cycleCount);
        Task<DataSet> GetCCBlockedLocations(BO.CycleCount cycleCount);
        Task<BO.CycleCount> IsBlockedLocation(BO.CycleCount cycleCount);
        Task<BO.CycleCount> ReleaseCycleCountLocation(BO.CycleCount cycleCount);
        Task<BO.CycleCount> BlockLocationForCycleCount(BO.CycleCount cycleCount);
        Task<BO.CycleCount> CheckMaterialAvailablilty(BO.CycleCount cycleCount);
        Task<List<BO.CycleCount>> GetCycleCountInformation(BO.CycleCount cycleCount);
        Task<bool> ClearLocationBlock(Location oLocation, Entities.CycleCount oCycleCount, Inventory oInventory, bool isConsolidationRequired);
        Task<string> GetConatinerLocationBin(string cartoncode, string WarehouseId, string UserID, string LocationCheck);
        Task<BO.CycleCount> UpsertCycleCount(BO.CycleCount cyclecountinfo);
    }
}
