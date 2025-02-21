using System.Text.Json.Serialization;

namespace CarrierAPI.Models
{
    public class CarrierConfiguration
    {
        public int CarrierConfigurationId { get; set; }
        public int CarrierMaxDesi { get; set; }
        public int CarrierMinDesi { get; set; }
        public decimal CarrierCost { get; set; }

        public int CarrierId { get; set; }
        [JsonIgnore]
        public Carrier Carrier { get; set; } = null!;
    }
}
