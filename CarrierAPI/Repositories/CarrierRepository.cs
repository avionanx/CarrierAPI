using CarrierAPI.Data;
using CarrierAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CarrierAPI.Repositories
{
    public class CarrierRepository : BaseRepository<Carrier>, ICarrierRepository
    {
        public CarrierRepository(ApplicationDatabaseContext context) : base(context) {}

        public override async Task<IEnumerable<Carrier>> GetAll()
        {
            return await _dbSet.Include(e => e.Orders).Include(e => e.CarrierConfigurations).ToListAsync();
        }
        /// <summary>
        /// A helper method that retrieves cheapest Carrier ID and it's total cost
        /// </summary>
        /// <param name="desi"></param>
        /// <returns></returns>
        public async Task<(int, decimal)?> GetPrice(int desi)
        {
            var carriers = await _dbSet
                .AsNoTracking()
                .Include(e => e.CarrierConfigurations)
                .ToListAsync();

            CarrierConfiguration? bestConfig = null;
            CarrierConfiguration? closestConfig = null;
            int closestDistance = int.MaxValue;

            foreach (var carrier in carriers)
            {
                if (!carrier.CarrierIsActive) continue;

                foreach (var config in carrier.CarrierConfigurations)
                {
                    bool inRange = config.CarrierMinDesi <= desi && desi <= config.CarrierMaxDesi;

                    if (inRange)
                    {
                        if (bestConfig == null || config.CarrierCost < bestConfig.CarrierCost)
                        {
                            bestConfig = config;
                        }
                    }
                    else if(bestConfig is null)
                    {
                        int distance = Math.Min(Math.Abs(desi - config.CarrierMinDesi), Math.Abs(desi - config.CarrierMaxDesi));

                        if (closestConfig == null || distance < closestDistance)
                        {
                            closestConfig = config;
                            closestDistance = distance;
                        }
                    }
                }
            }
            //NOTE:
            //If desi is lower than min desi, distance cost calculation is currently made regardless
            
            if (bestConfig is not null) // Return ID and base cost
            {
                return (bestConfig.CarrierId, bestConfig.CarrierCost);
            }
            else if (closestConfig is not null) // Return ID and calculate cost
            {
                var totalCost = closestConfig.CarrierCost + closestConfig.Carrier.CarrierPlusDesiCost * closestDistance;
                return (closestConfig.CarrierId, totalCost);
            }
            else // Either no carriers or no configs
            {
                return null;
            }
        }
    }
}
