using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Simpolo_Endpoint.Models
{
   
    public class GetAccountListModel
    {
        public int UserTypeID_New { get; set; }
        public int TenantID_New { get; set; }
        public int UserID_New { get; set; }
        public int AccountID_New { get; set; }
    }

    public class GetAccountDetailsModel
    {
        public int UserID_New { get; set; }
        public int AccountID_New { get; set; }
        public int AccountID { get; set; }
    }

    public class UpsertAccountModel
    {
        public string Type { get; set; } 
        public string AccountName { get; set; }
        public int AccountID { get; set; }
        public string accountCode { get; set; }
        public string CompanyLegalName { get; set; }
        public int UserID { get; set; }
        public string LogoPath { get; set; }
        public string ZohoAccountId { get; set; }
        public int SSOAccountID { get; set; }
        public string imageurl { get; set; }
    }


    public class GetUserListModel
    {
        public int AccountID { get; set; }
        public int UserIDNew { get; set; }
        public int UserID { get; set; }
        public int TenantId { get; set; }

    }

    public class UpsertUserModel
    {
        public int UserID { get; set; }
        public int TenantID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
        public string Sex { get; set; }
        public string AlternateEmail1 { get; set; }
        public string AlternateEmail2 { get; set; }
        public string Password { get; set; }
        public string UserRoleIDs { get; set; }
        public string WarehouseIDs { get; set; }
        public int IsActive { get; set; }
        // public string EnPassword { get; set; }
        public string Mobile { get; set; }
        public int CreatedBy { get; set; }
        public string EmployeeCode { get; set; }
        public int AccountID { get; set; }
        public int UserTypeID { get; set; }
        // public string HashPWD { get; set; }
        public int SSOUserID { get; set; }
    }

    public class GetWarehouseListModel
    {
        public int UserID { get; set; }
        public int Accountid { get; set; }
    }
    public class IFormFile
    {
        public string ImageURL { get; set; }
    }
    public class DeleteWarehouseModel
    {
        public string WarehouseID { get; set; }
    }

    public class UpsertWarehouseModel
    {
        public int Warehouseid { get; set; }
        public string WHName { get; set; }
        public string WHCode { get; set; }
        public int WHGroupcode { get; set; }
        public string Location { get; set; }
        public int RackingRType { get; set; }
        public int WHtype { get; set; }
        public string WHAddress { get; set; }
        public string FloorSpace { get; set; }
        public string Measurements { get; set; }
        public int PIN { get; set; }
        public string GeoLocation { get; set; }
        public int CountryId { get; set; }
        public int CurrencyId { get; set; }
        public string InoutId { get; set; }
        public string pName { get; set; }
        public string Pmobile { get; set; }
        public string pEmail { get; set; }
        public string PAddress { get; set; }
        public string sname { get; set; }
        public string SMobile { get; set; }
        public string SEmail { get; set; }
        public string SAddress { get; set; }
        public int UserID { get; set; }
        public int AccountId { get; set; }
        public int length { get; set; }
        public int Width { get; set; }
        public int height { get; set; }
        public string Latitude { get; set; }
        public string Langitude { get; set; }
        public int stateid { get; set; }
        public int cityid { get; set; }
        public int TimeZoneId { get; set; }
        public int IsDeleted { get; set; }
        public int IsActive { get; set; }
    }

    public class UpsertDockModel
    {
        public string DockNo { get; set; }
        public string DockName { get; set; }
        public int DockTypeID { get; set; }
        public int WarehouseID { get; set; }
        public int DockID { get; set; }

    }

    public class UpsertZoneModel
    {
        public string ZoneCode { get; set; }
        public string ZoneDesc { get; set; }
        public int WarehouseID { get; set; }
        public int ZoneID { get; set; }
    }

    public class DeleteZoneModel
    {
        public int WarehouseID { get; set; }
        public int ZoneID { get; set; }
    }
}