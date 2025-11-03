using Simpolo_Endpoint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simpolo_Endpoint.DAO.interfaces
{
    public interface IAccount
    {
        Task<Payload<string>> GetAccountList(GetAccountListModel items);
        Task<Payload<AuthResponce>> GetAccountDetails(GetAccountDetailsModel items);
        Task<Payload<string>> UpsertAccount(UpsertAccountModel items);
        Task<Payload<string>> GetUserList(GetUserListModel items);
        Task<Payload<string>> UpsertUser(UpsertUserModel items);
        Task<Payload<string>> GetWareHouseList(GetWarehouseListModel items);
        Task<Payload<string>> DeleteWarehouse(DeleteWarehouseModel items);
        Task<Payload<string>> UpsertWarehouse(UpsertWarehouseModel items);
        Task<Payload<string>> UpsertDock(UpsertDockModel dockInputModel);
        Task<Payload<string>> UpsertZone(UpsertZoneModel zoneInputModel);
        Task<Payload<string>> DeleteZone(DeleteZoneModel deleteZoneInput);
    }
}
