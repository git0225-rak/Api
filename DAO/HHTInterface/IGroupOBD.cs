using FWMSC21Core.Entities;
using Simpolo_Endpoint.DTO;
using Simpolo_Endpoint.Entities;
using Simpolo_Endpoint.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Simpolo_Endpoint.DAO.HHTInterface
{
    public interface IGroupOBD
    {

        Task<List<GroupOutbound>> GetVLPDNosByDock(GroupOutbound outbound);

        Task<List<GroupOutbound>> GetZPL_ScriptsforOBDsorting(GroupOutbound outbound);
        Task<List<OutboundModel>> GetItemsAgainstOBD(OutboundModel obj);
        Task<List<GroupOutbound>> GetVLPDNos(GroupOutbound outbound);

        Task<List<GroupOutbound>> GetVLPDNosForSorting(GroupOutbound outbound);

        Task<List<GroupOBDModel>> VLPDOBDItemToPick(GroupOBDModel outbound);
        GroupOBDModel VLPDPickedItems(GroupOBDModel outbound);

        Task<List<OutboundModel>> GetOBDSuggestionForSorting(OutboundModel obj);

        Task<OutboundModel>UpsertOBDSorting(OutboundModel obj);


        Task<List<OutboundModel>> GetLocationSuggestionForSorting(OutboundModel obj);

    }


}