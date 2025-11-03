using Simpolo_Endpoint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Simpolo_Endpoint.Models.POSOModel;

namespace Simpolo_Endpoint.DAO.interfaces
{
    public interface IPOSO
    {
        Task<Payload<string>> GetSOList(SOListModel sOHeaderList);
        Task<Payload<string>> GetSoHeaderDetails(GetSoHeaderDetailsModel getSoHeaderDetails);
        Task<Payload<string>> UpsertSoHeaderDetails(UpdateSOModel updateSO);
        Task<Payload<string>> GetCustomerPODetailsList(CustomerPOListModel customerPOList);
        Task<Payload<string>> UpsertCustomerPO(UpsertCustomerPOModel upsertCustomerPO);
        Task<Payload<string>> DeleteCustomerPO(DeleteCustomerPOModel deleteCustomerPO);
        Task<Payload<string>> GetMaterialSODetailsList(MaterialSODetailsListModel materialSODetailsList);
        Task<Payload<string>> UpsertMaterialSODetails(UpsertMaterialSODetailsModel upsertMaterialSODetails);
        Task<Payload<string>> GetPOMaterialDetailsList(GetPOMaterialDetailsDataModel items);
        Task<Payload<string>> GetPOHeaderList(POHeaderListInputModel items);
        Task<Payload<string>> GetCycleCountTransactionListByStatus(GetCycleCountTransactionListModel getCycleCountTransaction);
        Task<Payload<string>> GetPOList(POHeaderListInputModel items);
        Task<Payload<string>> UpsertPOHeaderData(POHeaderModel items);
        Task<Payload<string>> SavePOMaterialDetailsData(POMaterialDetailsDataModel items);
        Task<Payload<string>> GetPODetailsList(POMaterialDetailsDataModel items);
        Task<Payload<string>> UpsertSupplierInvoice(SupplierInvoiceInputModel items);
        Task<Payload<string>> GetSupplierInvoice(SupplierInvoiceInputModel items);
        Task<Payload<string>> DeleteSupplierInvoice(DeleteSupplierInvoiceInputModel items);
        Task<Payload<string>> UpsertSku_SupplerInvoiceDetails(UpsertSku_SupplerInvoiceDetailsModel items);
        Task<Payload<string>> GetSku_SupplerInvoiceDetails(GetSku_SupplerInvoiceDetailsModel items);
        Task<Payload<string>> Getmspscheckboxs(GetmspscheckboxsModel items);
        Task<Payload<string>> SupplerInvoiceDetailsRowUpdating(SupplerInvoiceMaterialListModel items);
        Task<Payload<string>> DeleteSupplerInvoiceDetails(SupplerInvoiceMaterialListModel items);
        Task<Payload<string>> DeleteSku_SupplerInvoiceDetails(DeleteSku_SupplerInvoiceDetailsModel items);
        Task<Payload<string>> GetPOHeaderDetails(GetPOHeaderDetailsModel items);
        Task<Payload<string>> DownloadASNIO(DownloadASNIOModel items);
        Task<Payload<string>> ImportASNIO(ImportASNIOModel items);
        Task<Payload<string>> ImportASNCheckMandatory(ImportASNCheckMandatorModel items);
        Task<Payload<string>> SODetails_UpdateStorageLoaction(SODetails_UpdateStorageLoactionModel items);
        Task<Payload<string>> GetKitStoreRefNo(KitModel items);
        Task<Payload<string>> GetKittingList(KitModel items);
        Task<Payload<string>> GetChildItemsForKitting(KitModel items);
    }
}
