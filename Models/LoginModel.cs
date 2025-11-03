using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simpolo_Endpoint.Models
{
    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string IPaddress { get; set; }
        public int UserID { get; set; }
        public int IsForceLogin { get; set; }
        public DateTime LogTime { get; set; }
    }
    public class GetMainMenus_GenericModel
    {
        public string CurrentUserTypeIDs { get; set; }
        public int TenantID { get; set; }
    }



    public class ChangePasswordModel
    {
        public string mobile { get; set; }
        public string Password { get; set; }

        public string UserGUID { get; set; }
      
        public string NewPassword { get; set; }
    }
    public class GetDashBordReportDataModel
    {
        public int WAREHOUSEID { get; set; }
        public string Date { get; set; }
       
    }
    public class GenerateTokenModel
    {
        public string password { get; set; }

        public string Email { get; set; }

    }
    
}
