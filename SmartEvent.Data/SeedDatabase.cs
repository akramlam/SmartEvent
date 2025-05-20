using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SmartEvent.Data
{
    public class SeedDatabase
    {
        public static async Task Main(string[] args)
        {
            try
            {
                Console.WriteLine("Starting database seeding...");
                
                // Build configuration
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false)
                    .Build();
                
                // Create DbContext
                var optionsBuilder = new DbContextOptionsBuilder<SmartEventDbContext>();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                
                using (var context = new SmartEventDbContext(optionsBuilder.Options))
                {
                    // Ensure database is created
                    context.Database.EnsureCreated();
                    
                    // Seed data
                    await DataSeeder.SeedData(context);
                    
                    Console.WriteLine("Database seeding completed successfully!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while seeding the database: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }
            
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
} 