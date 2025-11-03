using Simpolo_Endpoint.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Simpolo_Endpoint.DAO.interfaces
{
    public interface IAdministration
    {
        Task<Payload<string>> GetMaterialType(GetMaterialTypeModel items);

        Task<Payload<string>> GetTenantsInMaterialType(TenantsInputModel items);
        Task<Payload<string>> UpsertMaterialType(UpsertMaterialTypeModel items);
        Task<Payload<string>> DeleteMaterialType(DeleteMaterialInputModel items);
        Task<Payload<string>> GetMaterialGroup(GetMaterialGroupModel items);
        Task<Payload<string>> UpsertMaterialGroup(UpsertMaterialGroupModel items);
        Task<Payload<string>> DeleteMaterialGroup(DeleteMaterialGroupModel items);
        Task<Payload<string>> EditMaterialGroup(EditMaterialGroupModel items);
        Task<Payload<string>> GetContainerData(GetContainerDataModel items);

        Task<DataSet> GetPalletData(ContainercodeQrLables items);
        Task<Payload<string>> CreateNewCartons(CreateNewCartonsModel items);
        Task<Payload<string>> GetContainerPrint(PrintInputModel items);

        Task<Payload<string>> GetContainerPrint_Network(PrintInputModel printobj);

    }
}
