using CarrierAPI.Data;
using CarrierAPI.Repositories;
using CarrierAPI.Services;
using Hangfire;
using Microsoft.EntityFrameworkCore;

namespace CarrierAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            string connectionString = builder.Configuration["DB_CONNECTION_STRING"];
            builder.Services.AddDbContext<ApplicationDatabaseContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddScoped<ICarrierRepository, CarrierRepository>();
            builder.Services.AddScoped<ICarrierService, CarrierService>();

            builder.Services.AddScoped<ICarrierConfigurationRepository, CarrierConfigurationRepository>();
            builder.Services.AddScoped<ICarrierConfigurationService, CarrierConfigurationService>();

            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IOrderService, OrderService>();

            builder.Services.AddScoped<ICarrierReportRepository, CarrierReportRepository>();
            builder.Services.AddScoped<ICarrierReportService, CarrierReportService>();

            builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            builder.Services.AddControllers();

            builder.Services.AddHangfire(config => config.UseSqlServerStorage(connectionString));
            builder.Services.AddHangfireServer();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseHangfireDashboard();
            }

            RecurringJob.AddOrUpdate<ICarrierReportService>(
            "carrier-revenue-reports-hourly",
            service => service.PushLogs(),
            Cron.Hourly,
            new RecurringJobOptions
            {
                TimeZone = TimeZoneInfo.Local
            }
            );

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
