using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SmartEvent.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            // TODO: Replace with your actual connection string
            optionsBuilder.UseSqlServer("Server=Akram\\SQLEXPRESS;Database=SmartEventDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;Encrypt=False;");
            return new AppDbContext(optionsBuilder.Options);
        }
    }
} 