using System.Text.Json.Serialization;

namespace CarrierAPI.Models
{
    public class CarrierReport
    {
        public int CarrierReportId { get; set; }
        public DateTime CarrierReportDate { get; set; }
        public decimal CarrierCost { get; set; }
        public int CarrierId { get; set; }
        [JsonIgnore]
        public Carrier Carrier { get; set; }
        public CarrierReport()
        {
            CarrierReportDate = DateTime.Now;
        }
    }
}
