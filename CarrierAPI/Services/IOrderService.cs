using CarrierAPI.DTOs;
using CarrierAPI.Models;

namespace CarrierAPI.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAll();
        Task<Order> GetById(int id);
        Task<Order?> Create(OrderCreateDTO orderCreateDTO);
        Task<bool> Delete(int id);
    }
}
