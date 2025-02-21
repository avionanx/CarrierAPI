using CarrierAPI.DTOs;
using CarrierAPI.Models;
using Microsoft.AspNetCore.Mvc;

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
