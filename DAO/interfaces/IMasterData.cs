using Simpolo_Endpoint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simpolo_Endpoint.DAO.interfaces
{
    public interface IMasterData
    {

        Task<Payload<string>> DeleteSupplier(SaveSupplierDetailsInputModel obj);
        Task<Payload<string>> DeleteCustomerInfo(SaveCustomerDetailsInputModel obj);
        Task<Payload<string>> GetSupplierList(GetSupplierlistModel supplierlist);
        Task<Payload<string>> GetSupplierDetails(GetSupplierDetailsModel supplierDetails);
        Task<Payload<string>> UpsertSupplierDetails(UpsertSupplierDetailsModel updateSupplier);
        Task<Payload<string>> GetCustomerInfo(GetCustomerInfoModel items);
        Task<Payload<string>> UpsertCustomer(UpsertCustomerModel items);
        Task<Payload<string>> GetEmployeeList(GetEmployeeListmodel items);
        Task<Payload<string>> UpsertEmployee(UpsertEmployeeModel updateEmpInput);
        Task<Payload<string>> GetLocationManager(GetLocationManagerModel items);
        Task<Payload<string>> UpsertLocation(UpsertLocationModel items);
        Task<Payload<string>> DeleteLocation(DeleteLocationModel items);
        Task<Payload<string>> GetLoadBinDetails(GetLoadBinDetailsModel items);
        Task<Payload<string>> UpDateBulkModify(UpDateBulkModifyModel items);
        Task<Payload<string>> Modify_Locations(Modify_LocationsModel items);
        Task<Payload<string>> SaveCustomerAddressInfo(SaveCustomerAddressInputModel obj);
        Task<Payload<string>> Modify_LocationPopup(Modify_LocationPopupModel items);
        Task<Payload<string>> ItemMaster_Print(ItemMaster_PrintModel obj);
        Task<Payload<string>> LocationManager_LabelPrint(LocationManager_LabelPrintModel obj);
        Task<Payload<string>> LocationManager_Bulk_LabelPrint(LocationManager_Bulk_LabelPrintModel obj);

        Task<Payload<string>> ImportSupplierData(SaveSupplierDetailsInputModel obj);
        Task<Payload<string>> ImportCustomerData(SaveCustomerDetailsInputModel obj);
        Task<Payload<string>> GetTenantList(TenantListInputModel obj);
        Task<Payload<string>> GetBarcodeLabelData(BarcodeInputModel obj);
        Task<Payload<string>> SaveTenantContractData(TenantContractInputModel obj);
        Task<Payload<string>> GetTenantContractData(TenantListInputModel obj);
        Task<Payload<string>> SaveUpdateTenantData(SaveTenantInputModel obj);
        Task<Payload<string>> DeleteTenantContractData(TenantContractInputModel obj);

    }
}
