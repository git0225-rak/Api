namespace Simpolo_Endpoint.DTO
{
    public class AuthTokenDTO
    {
        public string AuthKey { get; set; }
        public string AuthToken { get; set; }
        public string AuthValue { get; set; }
        public string LoginTimeStamp { get; set; }
        public int RequestNumber { get; set; }
        public int UserID { get; set; }
    }

    public class EntityObjectDTO
    {
        public int FetchNextItem { get; set; }
        public string IsPicking { get; set; }
        public bool IsVstore { get; set; }
        public int RID { get; set; }
        public string WareHouseID { get; set; }
        public string AccountId { get; set; }
        public bool IsChecked { get; set; }
        public string UserId { get; set; }

        public string TenantID { get; set; }
    }

    public class GroupOutbondDTO
    {
        public string Type { get; set; }
        public AuthTokenDTO AuthToken { get; set; }
        public EntityObjectDTO EntityObject { get; set; }
    }


}
