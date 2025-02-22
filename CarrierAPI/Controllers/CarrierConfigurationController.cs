using Microsoft.AspNetCore.Mvc;
using CarrierAPI.Models;
using CarrierAPI.DTOs;
using CarrierAPI.Services;
using Microsoft.IdentityModel.Tokens;

namespace CarrierAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CarrierConfigurationController : ControllerBase
    {
        private readonly ICarrierConfigurationService _service;

        public CarrierConfigurationController(ICarrierConfigurationService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CarrierConfigurationCreateDTO carrierConfigurationCreateDTO)
        {
            if (carrierConfigurationCreateDTO == null) return BadRequest(new { message = "Girilen parametreler geçerli değil" });
            if (carrierConfigurationCreateDTO.CarrierMinDesi > carrierConfigurationCreateDTO.CarrierMaxDesi) return BadRequest(new { message = "Min desi değeri max üzerinde olamaz" });

            var carrierConfiguration = await _service.Create(carrierConfigurationCreateDTO);

            return Ok(new { message = "Kargo firması konfigürasyonu başarıyla oluşturuldu", carrierConfiguration });
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarrierConfiguration>>> GetAll()
        {
            var carrierConfigurations = await _service.GetAll();
            if (carrierConfigurations.IsNullOrEmpty())
            {
                return NotFound(new{ message = "Herhangi bir kargo firması konfigürasyonu bulunamadı" });
            }
            else
            {
                return Ok(new { message = "Kargo firması konfigürasyonları başarıyla getirildi", carrierConfigurations });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CarrierConfiguration>> GetSingle(int id)
        {
            var carrierConfiguration = await _service.GetById(id);
            if (carrierConfiguration == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(new { message = $"{id} ID'li konfigürasyon bulundu", carrierConfiguration });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSingle(int id)
        {
            var result = await _service.Delete(id);
            if (result == false)
            {
                return NotFound(new { message = $"{id} ID'li konfigürasyon bulunamadı" });
            }
            else
            {
                return Ok(new { message = "Konfigürasyon başarıyla silindi" });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] CarrierConfigurationUpdateDTO carrierConfigurationUpdateDTO)
        {
            if (carrierConfigurationUpdateDTO == null) return BadRequest(new { message = "Girilen parametreler geçerli değil" });
            if (carrierConfigurationUpdateDTO.CarrierMinDesi > carrierConfigurationUpdateDTO.CarrierMaxDesi) return BadRequest(new { message = "Min desi değeri max üzerinde olamaz" });
            var result = await _service.Update(id, carrierConfigurationUpdateDTO);

            if (result == false)
            {
                return NotFound(new { message = $"{id} ID'li konfigürasyon bulunamadı" });
            }
            else
            {

                return Ok(new { message = "Kargo konfigürasyon bilgileri başarıyla güncellendi" });
            }
        }
    }
}
