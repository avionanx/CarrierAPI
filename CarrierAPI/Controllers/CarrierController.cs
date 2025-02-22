using Microsoft.AspNetCore.Mvc;
using CarrierAPI.Models;
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
        /// <summary>
        /// Creates a new carrier. Carrier names do not need to be unique
        /// </summary>
        /// <param name="carrierCreateDTO"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CarrierCreateDTO carrierCreateDTO)
        {
            if (carrierCreateDTO == null) return BadRequest(new { message = "Girilen parametreler geçerli değil" });

            var carrier = await _service.Create(carrierCreateDTO);

            return Ok(new { message = "Kargo firması başarıyla oluşturuldu", carrier });
        }
        /// <summary>
        /// Returns all carriers
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Returns specific carrier
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Deletes a carrier
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Updates a carrier
        /// </summary>
        /// <param name="id"></param>
        /// <param name="carrierUpdateDTO"></param>
        /// <returns></returns>
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
