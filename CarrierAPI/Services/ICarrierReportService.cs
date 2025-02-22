using CarrierAPI.Models;

namespace CarrierAPI.Services
{
    public interface ICarrierReportService
    {
        Task<IEnumerable<CarrierReport>> GetAll();
        Task<CarrierReport> GetById(int id);
        Task PushLogs();
        Task<bool> Delete(int id);
    }
}
