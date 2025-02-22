using CarrierAPI.DTOs;
using CarrierAPI.Models;
using CarrierAPI.Repositories;

namespace CarrierAPI.Services
{
    public class CarrierService : ICarrierService
    {

        private readonly ICarrierRepository _carrierRepository;

        public CarrierService(ICarrierRepository repository)
        {
            _carrierRepository = repository;
        }

        async Task<Carrier> ICarrierService.Create(CarrierCreateDTO carrierCreateDTO)
        {
            var carrier = new Carrier
            {
                CarrierName = carrierCreateDTO.CarrierName,
                CarrierPlusDesiCost = carrierCreateDTO.CarrierPlusDesiCost ?? 0
            };

            var result = await _carrierRepository.Create(carrier);
            return result;
        }

        async Task<bool> ICarrierService.Delete(int id)
        {
            var carrier = await _carrierRepository.GetById(id);
            if (carrier is null) return false;

            await _carrierRepository.Delete(id);
            return true;
        }

        async Task<IEnumerable<Carrier>> ICarrierService.GetAll()
        {
            return await _carrierRepository.GetAll();
        }

        async Task<Carrier> ICarrierService.GetById(int id)
        {
            return await _carrierRepository.GetById(id);
        }

        async Task<bool> ICarrierService.Update(int id, CarrierUpdateDTO carrierUpdateDTO)
        {
            var carrier = await _carrierRepository.GetById(id);

            if (carrier is null) return false;

            carrier.CarrierName = carrierUpdateDTO.CarrierName;
            carrier.CarrierPlusDesiCost = carrierUpdateDTO.CarrierPlusDesiCost ?? 0;
            carrier.CarrierIsActive = carrierUpdateDTO.CarrierIsActive ?? false;

            await _carrierRepository.Update(carrier);
            return true;
        }
    }
}
