using System.Text.Json.Serialization;

namespace CarrierAPI.Models
{
    public class Carrier
    {
        public int CarrierId { get; set; }
        public string CarrierName { get; set; }
        public bool CarrierIsActive { get; set; } = false;
        public decimal CarrierPlusDesiCost { get; set; }
        [JsonIgnore]
        public ICollection<Order> Orders { get; set; } = new List<Order>();
        [JsonIgnore]
        public ICollection<CarrierConfiguration> CarrierConfigurations { get; set; } = new List<CarrierConfiguration>();
        [JsonIgnore]
        public ICollection<CarrierReport> CarrierReports { get; set; } = new List<CarrierReport>();
    }
}
