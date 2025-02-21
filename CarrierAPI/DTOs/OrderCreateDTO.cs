using System.ComponentModel.DataAnnotations;

namespace CarrierAPI.DTOs
{
    public class OrderCreateDTO
    {
        [Range(1, Double.PositiveInfinity)]
        [Required]
        public int OrderDesi { get; set; }
    }
}
