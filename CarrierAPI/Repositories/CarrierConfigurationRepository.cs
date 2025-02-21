using CarrierAPI.Data;
using CarrierAPI.Models;

namespace CarrierAPI.Repositories
{
    public class CarrierConfigurationRepository : BaseRepository<CarrierConfiguration>, ICarrierConfigurationRepository
    {
        public CarrierConfigurationRepository(ApplicationDatabaseContext context) : base(context) {}
    }
}
