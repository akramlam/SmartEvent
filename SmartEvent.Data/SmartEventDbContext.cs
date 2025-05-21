using Microsoft.EntityFrameworkCore;
using SmartEvent.Core.Models;
using System;

namespace SmartEvent.Data
{
    public class SmartEventDbContext : DbContext
    {
        public SmartEventDbContext(DbContextOptions<SmartEventDbContext> options)
            : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Participant> Participants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships
            modelBuilder.Entity<Event>()
                .HasOne<User>()
                .WithMany(u => u.OrganizedEvents)
                .HasForeignKey(e => e.OrganizerId);

            modelBuilder.Entity<Participant>()
                .HasOne(a => a.Event)
                .WithMany()
                .HasForeignKey(a => a.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            // Add unique constraint to prevent double registrations
            modelBuilder.Entity<Participant>()
                .HasIndex(a => new { a.EventId, a.Email })
                .IsUnique();
        }
    }
} 