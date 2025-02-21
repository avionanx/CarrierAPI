using CarrierAPI.DTOs;
using CarrierAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarrierAPI.Services
{
    public interface ICarrierConfigurationService
    {
        Task<IEnumerable<CarrierConfiguration>> GetAll();
        Task<CarrierConfiguration> GetById(int id);
        Task<CarrierConfiguration> Create([FromBody] CarrierConfigurationCreateDTO carrierCreateDTO);
        Task<bool> Update(int id, [FromBody] CarrierConfigurationUpdateDTO carrierUpdateDTO);
        Task<bool> Delete(int id);
    }
}
