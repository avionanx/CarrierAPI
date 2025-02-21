using CarrierAPI.DTOs;
using CarrierAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarrierAPI.Services
{
    public interface ICarrierService
    {
        Task<IEnumerable<Carrier>> GetAll();
        Task<Carrier> GetById(int id);
        Task<Carrier> Create([FromBody] CarrierCreateDTO carrierCreateDTO);
        Task<bool> Update(int id, [FromBody] CarrierUpdateDTO carrierUpdateDTO);
        Task<bool> Delete(int id);
    }
}
