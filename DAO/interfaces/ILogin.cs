using Simpolo_Endpoint.DTO;
using Simpolo_Endpoint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Simpolo_Endpoint.DAO.interfaces
{
    public interface ILogin 
    {   
        Task<Payload<string>> GetMainMenus_Generic(GetMainMenus_GenericModel obj);
        Task<Payload<string>> ChangePassword(ChangePasswordModel items);
        Task<Payload<string>> GetDashBordReportData(GetDashBordReportDataModel items);
        Task<Payload<AuthResponce>> LoginUser(LoginModel items);
        Task<Payload<AuthResponce>> GetNewToken();
        Task<Payload<AuthResponce>> GetFilePath(FilePathModel items);
        Task<ProfileDTO> Login(ProfileDTO items);
        Task<Payload<string>> LogOut(LoginModel items);
        Task<ProfileDTO> UserLogout(ProfileDTO items);

    }
}
