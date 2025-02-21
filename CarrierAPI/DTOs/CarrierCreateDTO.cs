using System.ComponentModel.DataAnnotations;

namespace CarrierAPI.DTOs
{
    public class CarrierCreateDTO
    {
        [Required(AllowEmptyStrings = false)]
        public string CarrierName { get; set; }

        [Required]

        [Range(0, Double.PositiveInfinity)]
        public decimal? CarrierPlusDesiCost { get; set; }
    }
}
