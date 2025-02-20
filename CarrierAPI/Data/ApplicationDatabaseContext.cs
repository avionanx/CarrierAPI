using Microsoft.EntityFrameworkCore;
namespace CarrierAPI.Data
{
    public class ApplicationDatabaseContext : DbContext
    {
        public ApplicationDatabaseContext(DbContextOptions<ApplicationDatabaseContext> options) : base(options) 
        {
            Console.WriteLine();
        }
    }
}
