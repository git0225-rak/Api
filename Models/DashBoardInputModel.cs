namespace Simpolo_Endpoint.Models
{
    public class DashBoardInputModel
    {
            public int WareHouseid { get; set; }
            public int LoginAccountId { get; set; }
            public int LoginUserId { get; set; }
            public int LoginTanentId { get; set; }
            public int OutboundId { get; set; }
            public int DockId { get; set; }
            public string VehicleNo { get; set; }
            public string fromdate { get; set; }
            public string todate { get; set; }
            public int tenantID { get; set; }
            public int IsExcel { get; set; }
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
            public int CartonId { get; set; }
            public int LocationId { get; set; }
            public int MMID { get; set; }

            public int LoadingPointID { get; set; }

    }
}
