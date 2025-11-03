using Simpolo_Endpoint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Simpolo_Endpoint.DAO.interfaces
{
    public interface ISAPJsonPostService
    {
        public  Task<PGIResponse> SendPGIJSONDatatoSAP(int OutboundID);
        public  Task<PGIRevertResponse> SendPGIRevertJSONDatatoSAP(int OutboundID);

        public Task<string> SendMaterialTransferJSONDatatoSAP(int TransferRequestID);

        public Task<string> SendMaterialTransferJSONDatatoSAP_Unblock(int TransferRequestID);

        public Task<string> SendMaterialTransferJSONDatatoSAP_ReturnPGR(int TransferRequestID);
    }
}
