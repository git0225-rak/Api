using Simpolo_Endpoint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simpolo_Endpoint.DAO.interfaces
{
    public interface IOrders
    {
        Task<Payload<string>> GetStockPosting(GetStockPostingModel obj);
        Task<Payload<string>> GetCurrentStockDynamicData(GetCurrentStockDynamicDataModel obj);
        Task<Payload<string>> GetSuppliersReturns(GetSuppliersReturnsModel obj);
        Task<Payload<string>> GetStockPostingDetails(GetStockPostingInputModel getStockPostingInput);
        Task<Payload<string>> UpdateEmployeeHeaderData(UpdateEmployeeHeaderDataModel updateEmployee);
        Task<Payload<string>> SaveTransferRequest(SaveTransferRequestModel updateEmployee);
        Task<Payload<string>> GetSupplierReturnlist(GetSupplierReturnlistModel items);
        Task<Payload<string>> StockPosting(StockPostingModel items);
        Task<Payload<string>> InitiateStock(InitiateStockModel items);
        Task<Payload<string>> InitiateToInProcess(InitiateToProcessModel items);
        Task<Payload<string>> MaterialQtyUpdate(MaterialQtyUpdateModel obj);
        Task<Payload<string>> EmployeeRequestConfirmation(EmployeeRequestConfirmationModel obj);
        Task<Payload<string>> GetEmployeeRequestForm(EmployeeRequestVerificationModel obj);
        Task<Payload<string>> CompleteMasterDetailsSetLFO(MasterDetailsSetLFOModel items);
        Task<Payload<string>> GetSuccessInfoCapture(GetSuccessInfoCaptureModel items);
        Task<Payload<string>> UpsertQualityVerification(UpsertQualityModel items);
        Task<Payload<string>> LabSampleRequest_InitiatePick(LabSampleRequest_InitiatePickModel items);
        Task<Payload<string>> MaterialPickQtyUpdate(MaterialPickQtyUpdateModel items);
        Task<Payload<string>> TransferSupplierReturns(TransferSupplierReturnsModel items);
        Task<Payload<string>> CurrentStock_PrintLabel(CurrentStock_PrintLabelModel printobj);
        Task<Payload<string>> UpsertCycleCountDetails(UpsertCycleCountDetailsModel items);

    }
}

       


