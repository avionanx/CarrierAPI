using CarrierAPI.Data;
using CarrierAPI.Models;

namespace CarrierAPI.Repositories
{
    public class CarrierReportRepository : BaseRepository<CarrierReport>, ICarrierReportRepository
    {
        public CarrierReportRepository(ApplicationDatabaseContext context) : base(context) {}
    }
}
