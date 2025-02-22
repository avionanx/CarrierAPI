using CarrierAPI.DTOs;
using CarrierAPI.Models;
using CarrierAPI.Repositories;

namespace CarrierAPI.Services
{
    public class CarrierReportService : ICarrierReportService
    {

        private readonly ICarrierReportRepository _carrierReportRepository;
        private readonly IOrderRepository _orderRepository;
        public CarrierReportService(ICarrierReportRepository carrierReportRepository, IOrderRepository orderRepository)
        {
            _carrierReportRepository = carrierReportRepository;
            _orderRepository = orderRepository;
        }

        public async Task PushLogs()
        {
            var allOrders = await _orderRepository.GetAll();
            var newOrders = allOrders.Where(order => order.OrderDate >= DateTime.Now.AddHours(-1));

            Dictionary<int, decimal> revenues = new Dictionary<int, decimal>();
            foreach(var order in newOrders)
            {
                bool hasValue = revenues.TryGetValue(order.CarrierId, out decimal value);

                if (hasValue)
                {
                    revenues[order.CarrierId] = order.OrderCarrierCost + value;
                }
                else
                {
                    revenues.Add(order.CarrierId, order.OrderCarrierCost);
                }
            }
            foreach(var revenue in revenues)
            {
                var reportParam = new CarrierReport
                {
                    CarrierId = revenue.Key,
                    CarrierCost = revenue.Value
                };
                await _carrierReportRepository.Create(reportParam);
            }
        }

        public async Task<bool> Delete(int id)
        {
            var report = await _carrierReportRepository.GetById(id);
            if (report == null) return false;

            await _carrierReportRepository.Delete(id);
            return true;
        }

        public async Task<IEnumerable<CarrierReport>> GetAll()
        {
            return await _carrierReportRepository.GetAll();
        }

        public async Task<CarrierReport> GetById(int id)
        {
            return await _carrierReportRepository.GetById(id);
        }
    }
}
