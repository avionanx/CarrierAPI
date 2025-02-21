using System.Text.Json.Serialization;

namespace CarrierAPI.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int OrderDesi { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal OrderCarrierCost { get; set; }

        public int CarrierId { get; set; }
        [JsonIgnore]
        public Carrier Carrier { get; set; } = null!;

        public Order()
        {
            OrderDate = DateTime.Now;
        }
    }
}
