using Microsoft.AspNetCore.Mvc;
using CarrierAPI.Models;
using CarrierAPI.Repositories;
using CarrierAPI.DTOs;
using CarrierAPI.Services;
using Microsoft.IdentityModel.Tokens;

namespace CarrierAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CarrierController : ControllerBase
    {
        private readonly ICarrierService _service;

        public CarrierController(ICarrierService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CarrierCreateDTO carrierCreateDTO)
        {
            if (carrierCreateDTO == null) return BadRequest(new { message = "Girilen parametreler geçerli değil" });

            var carrier = await _service.Create(carrierCreateDTO);

            return Ok(new { message = "Kargo firması başarıyla oluşturuldu", carrier });
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Carrier>>> GetAll()
        {
            var carriers = await _service.GetAll();
            if (carriers.IsNullOrEmpty())
            {
                return NotFound(new{ message = "Herhangi bir kargo firması bulunamadı" });
            }
            else
            {
                return Ok(new { message = "Kargo firmaları başarıyla getirildi", carriers });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Carrier>> GetSingle(int id)
        {
            var carrier = await _service.GetById(id);
            if (carrier == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(new { message = $"{id} ID'li firması bulundu", carrier });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSingle(int id)
        {
            var result = await _service.Delete(id);
            if (result == false)
            {
                return NotFound(new { message = $"{id} ID'li kargo firması bulunamadı" });
            }
            else
            {
                return Ok(new { message = "Kargo firması başarıyla silindi" });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] CarrierUpdateDTO carrierUpdateDTO)
        {
            if (carrierUpdateDTO == null) return BadRequest(new { message = "Girilen parametreler geçerli değil" });
            
            var result = await _service.Update(id, carrierUpdateDTO);

            if (result)
            {
                return NotFound(new { message = $"{id} ID'li kargo firması bulunamadı" });
            }
            else
            {

                return Ok(new { message = "Kargo firması bilgileri başarıyla güncellendi" });
            }
        }
    }
}
