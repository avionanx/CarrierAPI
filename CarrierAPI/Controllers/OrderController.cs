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
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrderController(IOrderService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] OrderCreateDTO orderCreateDTO)
        {
            if (orderCreateDTO == null) return BadRequest(new { message = "Girilen parametreler geçerli değil" });

            var order = await _service.Create(orderCreateDTO);
            if (order is null)
            {
                return NotFound(new { message = "Sipariş için herhangi bir kargo firması veya konfigürasyon bulunamadı" });
            }
            else
            {
                return Ok(new { message = "Sipariş başarıyla verildi", order });
            }
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var orders = await _service.GetAll();
            if (orders.IsNullOrEmpty())
            {
                return NotFound(new { message = "Herhangi bir sipariş bulunamadı" });
            }
            else
            {
                return Ok(new { message = "Siparişler başarıyla döndürüldü", orders });
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var order = await _service.GetById(id);
            if (order is null)
            {
                return NotFound(new { message = $"{id} ID'li herhangi bir sipariş bulunamadı" });
            }
            else
            {
                return Ok(new { message = "Sipariş başarıyla bulundu", order });
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _service.Delete(id);
            if (result == false)
            {
                return NotFound(new { message = $"{id} ID'li sipariş bulunamadı" });
            }
            else
            {
                return Ok(new { message = "Sipariş başarıyla silindi" });
            }
        }
    }
}
