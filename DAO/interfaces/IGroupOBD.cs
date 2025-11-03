using Simpolo_Endpoint.DTO;
using Simpolo_Endpoint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Simpolo_Endpoint.DAO.interfaces
{
    public interface IGroupOBD
    {
     

        Task<Payload<string>> GetGroupOutboundDetails(GroupOBDDTO obj);
        Task<Payload<string>> GetOBDDetailsCustomerWise(GroupOBDDTO obj);
        Task<Payload<string>> GetSOType(SOTypeModelItems items);

        Task<Payload<string>> GetDeliverySites(DeliverySitesModelItems items);

        Task<Payload<string>> GroupOBDCreation(GroupOutboundCreationItemsModel items);

        Task<Payload<string>> VLPDDeliveryNoteDetails(VLPDDeliveryNoteModelItems items);

        Task<Payload<string>> GetGroupOBDPopupDetails(GroupOBDPopupModelitems items);

        Task<Payload<string>> GetCartonDetails(CartonModelItems items);

        Task<Payload<string>> GetVLPDPickingItem(vlpdpickitemfrombin items);

        Task<Payload<string>> GetVLPDPickingByGroupOBDNumber(vlpddeliverypicknote items);

        Task<Payload<string>> UpdateOBDQty(UpdateOBDQtyinViewPopup items);

        Task<Payload<string>> VerifyPopUp(Vlpdverifyopup items);

        Task<Payload<string>> GetPendingSKUList(Pendingreleaselist items);

        Task<Payload<string>> GetReservedSKUList(Pendingreleaselist items);


        Task<Payload<string>> VLPDViewPickList(VLPDViewPickList items);

        Task<Payload<string>> VLPDViewPickListSummarize(VLPDViewPickList items);


        Task<Payload<string>> VLPDReleaseItems(VLPDReleaseModelItems items);



        Task<Payload<string>> VLPDRegenerateReleaseItems(VLPDReleaseModelItems items);

        Task<Payload<string>> UpsertOBDLoadPointData(GetLoadingPoints items);


        Task<Payload<string>> UpdateOBDLoadPointData(GetLoadingPoints items);



        Task<Payload<string>> GetPickingListDetails(PickingListItemsModel items);

        Task<Payload<string>> GetGroupOBDNumber(GroupOBDNumber items);

        Task<bool> PostPGIDataToSAP(PgipostingDTO items);















    }


}