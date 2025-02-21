using CarrierAPI.Data;
using CarrierAPI.Models;

namespace CarrierAPI.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDatabaseContext context) : base(context) {}
    }
}
