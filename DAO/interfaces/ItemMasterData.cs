using Simpolo_Endpoint.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Simpolo_Endpoint.DAO.interfaces
{
    public interface ItemMasterData
    {
        Task<Payload<string>> GetMaterialList(GetMaterialListModel items);
        Task<Payload<string>> GetItemMasterAutocompletes(GetItemMasterAutocompletesModel items);
        Task<Payload<string>> GetMaterailParametersInfo(GetMaterailParametersInfoModel items);
        Task<Payload<string>> UpsertItemMasterBasicDetails(UpsertItemMasterBasicDetailsModel items);
        Task<Payload<string>> Check_MCodeExists(GetMCodeCheck items);
        Task<Payload<string>> UpsertUoMInfo(UpsertUoMInfoModel items);

    }
}
