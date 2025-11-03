using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simpolo_Endpoint.DAO.interfaces
{
    public interface ISAPIDocReceiver
    {
        Task<string> POSTXML(string request, string Idocname, string Client);
       
    }

}
