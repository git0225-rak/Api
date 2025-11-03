namespace Simpolo_Endpoint.Models
{
    public class ReceivingDockManagementDataModel
    {
        public int OutboundID { get; set; }
        public int DockID { get; set; }
        public string VehicleRegNo { get; set; }
        public string DriverName { get; set; }
        public int CreatedBy { get; set; }
        public int OutboundDockId { get; set; }
        public int LoginAccountId { get; set; }
        public int LoginTanentId { get; set; }
        public int LoginUserId { get; set; }
        public int VehicleTypeID { get; set; }
        public int FreightCompanyID { get; set; }
        public int ReceivingStatus { get; set; }
        public string Drivercontactno { get; set; }
        public string VehicleWeight { get; set; }

        public int LoadingPointID { get; set; }
    }
}
