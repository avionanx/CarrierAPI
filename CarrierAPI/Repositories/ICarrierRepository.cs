using CarrierAPI.Models;

namespace CarrierAPI.Repositories
{
    public interface ICarrierRepository : IBaseRepository<Carrier>
    {
        public Task<(int, decimal)?> GetPrice(int desi); 
    }
}
