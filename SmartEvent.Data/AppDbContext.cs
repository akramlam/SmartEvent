using Microsoft.EntityFrameworkCore;
using SmartEvent.Core.Models;

namespace SmartEvent.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Attendee> Attendees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships
            modelBuilder.Entity<Event>()
                .HasOne<User>()
                .WithMany(u => u.OrganizedEvents)
                .HasForeignKey(e => e.OrganizerId);

            modelBuilder.Entity<Attendee>()
                .HasOne(a => a.Event)
                .WithMany()
                .HasForeignKey(a => a.EventId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
