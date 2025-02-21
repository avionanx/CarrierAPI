using System.ComponentModel.DataAnnotations;

namespace CarrierAPI.DTOs
{
    public class CarrierUpdateDTO
    {
        [Required(AllowEmptyStrings = false)]
        public string CarrierName { get; set; }
        [Required]
        public bool? CarrierIsActive { get; set; }

        [Required]

        [Range(0, Double.PositiveInfinity)]
        public decimal? CarrierPlusDesiCost { get; set; }
    }
}
