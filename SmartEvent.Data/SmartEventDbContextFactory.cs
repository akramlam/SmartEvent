using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SmartEvent.Data
{
    public class SmartEventDbContextFactory : IDesignTimeDbContextFactory<SmartEventDbContext>
    {
        public SmartEventDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SmartEventDbContext>();
            // TODO: Replace with your actual connection string
            optionsBuilder.UseSqlServer("Server=Akram\\SQLEXPRESS;Database=SmartEventDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;Encrypt=False;");
            return new SmartEventDbContext(optionsBuilder.Options);
        }
    }
} 