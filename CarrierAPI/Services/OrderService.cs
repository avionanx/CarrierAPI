using CarrierAPI.DTOs;
using CarrierAPI.Models;
using CarrierAPI.Repositories;

namespace CarrierAPI.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICarrierRepository _carrierRepository;

        public OrderService(IOrderRepository orderRepository, ICarrierRepository carrierRepository)
        {
            _orderRepository = orderRepository;
            _carrierRepository = carrierRepository;
        }

        public async Task<Order?> Create(OrderCreateDTO orderCreateDTO)
        {
            var idAndCost = await _carrierRepository.GetPrice(orderCreateDTO.OrderDesi);
            if (idAndCost is null) return null;
            var orderParam = new Order
            {
                CarrierId = idAndCost.Value.Item1,
                OrderCarrierCost = idAndCost.Value.Item2,
                OrderDesi = orderCreateDTO.OrderDesi
            };
            var result = await _orderRepository.Create(orderParam);
            return result;
        }

        public async Task<bool> Delete(int id)
        {
            var order = await _orderRepository.GetById(id);
            if (order == null) return false;

            await _orderRepository.Delete(id);
            return true;
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            return await _orderRepository.GetAll();
        }

        public async Task<Order> GetById(int id)
        {
            return await _orderRepository.GetById(id);
        }
    }
}
