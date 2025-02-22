using CarrierAPI.DTOs;
using CarrierAPI.Models;
using CarrierAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CarrierAPI.Services
{
    public class CarrierConfigurationService : ICarrierConfigurationService
    {
        private readonly ICarrierConfigurationRepository _carrierConfigurationRepository;
        private readonly ICarrierRepository _carrierRepository;

        public CarrierConfigurationService(ICarrierConfigurationRepository carrierConfigurationRepository, ICarrierRepository carrierRepository)
        {
            _carrierConfigurationRepository = carrierConfigurationRepository;
            _carrierRepository = carrierRepository;
        }

        public async Task<CarrierConfiguration?> Create([FromBody] CarrierConfigurationCreateDTO carrierCreateDTO)
        {
            var existingCarrier = await _carrierRepository.GetById(carrierCreateDTO.CarrierId);
            if (existingCarrier is null) return null;
            var carrierConfigurationParam = new CarrierConfiguration
            {
                CarrierMaxDesi = carrierCreateDTO.CarrierMaxDesi,
                CarrierMinDesi = carrierCreateDTO.CarrierMinDesi,
                CarrierId = carrierCreateDTO.CarrierId,
                CarrierCost = carrierCreateDTO.CarrierCost
            };
            var carrierConfiguration = await _carrierConfigurationRepository.Create(carrierConfigurationParam);

            return carrierConfiguration;
        }

        public async Task<bool> Delete(int id)
        {
            var carrierConfiguration = await _carrierConfigurationRepository.GetById(id);
            if (carrierConfiguration is null) return false;

            await _carrierConfigurationRepository.Delete(id);
            return true;
        }

        public async Task<IEnumerable<CarrierConfiguration>> GetAll()
        {
            return await _carrierConfigurationRepository.GetAll();
        }

        public async Task<CarrierConfiguration> GetById(int id)
        {
            return await _carrierConfigurationRepository.GetById(id);
        }

        public async Task<bool> Update(int id, [FromBody] CarrierConfigurationUpdateDTO carrierUpdateDTO)
        {
            var carrierConfiguration = await _carrierConfigurationRepository.GetById(id);

            if (carrierConfiguration is null) return false;

            carrierConfiguration.CarrierCost = carrierUpdateDTO.CarrierCost;
            carrierConfiguration.CarrierMinDesi = carrierUpdateDTO.CarrierMinDesi;
            carrierConfiguration.CarrierMaxDesi = carrierUpdateDTO.CarrierMaxDesi;

            await _carrierConfigurationRepository.Update(carrierConfiguration);
            return true;
        }
    }
}
