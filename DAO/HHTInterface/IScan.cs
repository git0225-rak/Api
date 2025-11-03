using Simpolo_Endpoint.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMSCore_BusinessEntities.Entities;

namespace Simpolo_Endpoint.DAO.HHTInterface
{
    public interface IScan
    {
        Task<ScannedItem> ValidateLocation(ScannedItem obj);
        Task<ScannedItem> ValidatePallet(ScannedItem obj);
        Task<ScannedItem> ValidateSKU(ScannedItem obj);
        Task<ScannedItem> ValidateCarton(ScannedItem obj);
        Task<ScannedItem> ValidateSO(ScannedItem obj);
    }
}
