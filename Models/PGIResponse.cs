namespace Simpolo_Endpoint.Models
{
    public class PGIResponse
    {
        public string SAPRefNumber { get; set; } = string.Empty;
        public string SAPError { get; set; } = string.Empty;
        public string PGIPostingDate { get; set; } = string.Empty;
        public string DeliveryNumber { get; set; } = string.Empty;
    }

    public class PGIRevertResponse
    {
        public string SAPRefNumber { get; set; } = string.Empty;
        public string SAPError { get; set; } = string.Empty;
        public string PGIPostingDate { get; set; } = string.Empty;
        public string DeliveryNumber { get; set; } = string.Empty;
    }

}
