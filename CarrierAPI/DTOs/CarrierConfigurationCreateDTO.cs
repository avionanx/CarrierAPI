using CarrierAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace CarrierAPI.DTOs
{
    public class CarrierConfigurationCreateDTO
    {
        [Range(0, Double.PositiveInfinity)]
        [Required]
        public int CarrierMaxDesi { get; set; }

        [Range(0, Double.PositiveInfinity)]
        [Required]
        public int CarrierMinDesi { get; set; }

        [Range(0, Double.PositiveInfinity)]
        [Required]
        public decimal CarrierCost { get; set; }

        [Required]
        public int CarrierId { get; set; }
    }
}
