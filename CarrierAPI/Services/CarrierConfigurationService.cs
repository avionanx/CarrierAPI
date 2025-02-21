using CarrierAPI.DTOs;
using CarrierAPI.Models;
using CarrierAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CarrierAPI.Services
{
    public class CarrierConfigurationService : ICarrierConfigurationService
    {
        private readonly ICarrierConfigurationRepository _repository;

        public CarrierConfigurationService(ICarrierConfigurationRepository repository)
        {
            _repository = repository;
        }

        public async Task<CarrierConfiguration> Create([FromBody] CarrierConfigurationCreateDTO carrierCreateDTO)
        {
            var carrierConfigurationParam = new CarrierConfiguration
            {
                CarrierMaxDesi = carrierCreateDTO.CarrierMaxDesi,
                CarrierMinDesi = carrierCreateDTO.CarrierMinDesi,
                CarrierId = carrierCreateDTO.CarrierId,
                CarrierCost = carrierCreateDTO.CarrierCost
            };
            var carrierConfiguration = await _repository.Create(carrierConfigurationParam);

            return carrierConfiguration;
        }

        public async Task<bool> Delete(int id)
        {
            var carrierConfiguration = await _repository.GetById(id);
            if (carrierConfiguration == null) return false;

            await _repository.Delete(id);
            return true;
        }

        public async Task<IEnumerable<CarrierConfiguration>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<CarrierConfiguration> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<bool> Update(int id, [FromBody] CarrierConfigurationUpdateDTO carrierUpdateDTO)
        {
            var carrierConfiguration = await _repository.GetById(id);

            if (carrierConfiguration == null) return false;

            carrierConfiguration.CarrierCost = carrierUpdateDTO.CarrierCost;
            carrierConfiguration.CarrierMinDesi = carrierUpdateDTO.CarrierMinDesi;
            carrierConfiguration.CarrierMaxDesi = carrierUpdateDTO.CarrierMaxDesi;

            await _repository.Update(carrierConfiguration);
            return true;
        }
    }
}
