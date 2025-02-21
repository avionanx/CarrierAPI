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
    public class CarrierReportController : ControllerBase
    {
        private readonly ICarrierReportService _service;

        public CarrierReportController(ICarrierReportService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var reports = await _service.GetAll();
            if (reports.IsNullOrEmpty())
            {
                return NotFound(new { message = "Herhangi bir kayıt bulunamadı" });
            }
            else
            {
                return Ok(new { message = "Kayıtlar başarıyla döndürüldü", reports });
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var report = await _service.GetById(id);
            if (report is null)
            {
                return NotFound(new { message = $"{id} ID'li herhangi bir kayıtbulunamadı" });
            }
            else
            {
                return Ok(new { message = "Kayıt başarıyla bulundu", report });
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _service.Delete(id);
            if (result == false)
            {
                return NotFound(new { message = $"{id} ID'li kayıt bulunamadı" });
            }
            else
            {
                return Ok(new { message = "Kayıt başarıyla silindi" });
            }
        }
    }
}
