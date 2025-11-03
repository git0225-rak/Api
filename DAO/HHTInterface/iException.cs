using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simpolo_Endpoint.DAO.HHTInterface
{
    public interface iException
    {
        bool LogException(WMSExceptionMessage oRequest);
    }
}
