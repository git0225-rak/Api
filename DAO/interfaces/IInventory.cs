using Simpolo_Endpoint.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Simpolo_Endpoint.Models.InventoryModel;

namespace Simpolo_Endpoint.DAO.interfaces
{
    public interface IInventory
    {
        Task<Payload<string>> GetCurrentStock(GetCurrentStockInputModel getCurrentStockInput);

        Task<Payload<string>> GetMovementStock(GetCurrentStockInputModel getCurrentStockInput);


        Task<Payload<string>> UpsertMovementStock(GetCurrentStockInputModel getCurrentStockInput);


        Task<Payload<string>> GetMiscellaneousReceipt(GetMiscellaneousReceiptModel getMiscellaneousReceipt);
        Task<Payload<string>> UpdateMisslleniousReceipt(UpdateMisslleniousReceiptIputModel updateMisslleniousReceipt);
        Task<Payload<string>> GetMiscellaneousIssue(GetMiscellaneousIssueModel getMiscellaneousIssue);
        Task<Payload<string>> GetColumns(GetColumnsInputModel getColumnsInput);
        Task<Payload<string>> UpsertColumns([FromBody] UpsertColumnsInputModel getColumnsInput);
        Task<Payload<string>> PickingItem(PickingItemModel items);
        Task<Payload<string>> GetMiscellaneousReceiptTableData(GetMiscellaneousReceiptTableDataModel obj);
        Task<Payload<string>> GetProjectStockList(GetProjectStockListModel items);
        Task<Payload<string>> UpdateProjectstockTransferQty(UpdateProjectstockTransferQtyModel items);
        Task<Payload<string>> GetCurrentStockReportBySearch(GetCurrentStockReportInputModel obj);
        Task<Payload<string>> GetCurrentStockReport(GetCurrentStockInputModel getCurrentStockInput);
        Task<Payload<string>> GetReserveStockReport(ReserveStockModelItems StockItems);


    }
}
