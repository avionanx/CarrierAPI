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
        /// <summary>
        /// Creates a new configuration. Min desi cannot be above max desi
        /// </summary>
        /// <param name="carrierConfigurationCreateDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CarrierConfigurationCreateDTO carrierConfigurationCreateDTO)
        {
            if (carrierConfigurationCreateDTO is null) return BadRequest(new { message = "Girilen parametreler geçerli değil" });
            if (carrierConfigurationCreateDTO.CarrierMinDesi > carrierConfigurationCreateDTO.CarrierMaxDesi) return BadRequest(new { message = "Min desi değeri max üzerinde olamaz" });

            var carrierConfiguration = await _service.Create(carrierConfigurationCreateDTO);
            if(carrierConfiguration is null)
            {
                return NotFound(new { message = $"{carrierConfigurationCreateDTO.CarrierId} ID'li herhangi bir kargo firması bulunamadı" });
            }
            else
            {
                return Ok(new { message = "Kargo firması konfigürasyonu başarıyla oluşturuldu", carrierConfiguration });
            }
            
        }
        /// <summary>
        /// Returns all configurations
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Returns specific configuration
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CarrierConfiguration>> GetSingle(int id)
        {
            var carrierConfiguration = await _service.GetById(id);
            if (carrierConfiguration is null)
            {
                return NotFound(new { message = $"{id} ID'li konfigürasyon bulunamadı" });
            }
            else
            {
                return Ok(new { message = $"{id} ID'li konfigürasyon bulundu", carrierConfiguration });
            }
        }
        /// <summary>
        /// Deletes a configuration
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Updates a configuration. Min desi cannot be above max desi
        /// </summary>
        /// <param name="id"></param>
        /// <param name="carrierConfigurationUpdateDTO"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] CarrierConfigurationUpdateDTO carrierConfigurationUpdateDTO)
        {
            if (carrierConfigurationUpdateDTO is null) return BadRequest(new { message = "Girilen parametreler geçerli değil" });
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
