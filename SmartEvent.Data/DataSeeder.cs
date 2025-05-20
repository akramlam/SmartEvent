using Microsoft.EntityFrameworkCore;
using SmartEvent.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartEvent.Data
{
    public static class DataSeeder
    {
        public static async Task SeedData(SmartEventDbContext context)
        {
            // Clear existing data if needed
            // await context.Database.ExecuteSqlRawAsync("DELETE FROM Attendees");
            // await context.Database.ExecuteSqlRawAsync("DELETE FROM Events");
            // await context.Database.ExecuteSqlRawAsync("DELETE FROM Users");
            
            // Add users if none exist
            if (!await context.Users.AnyAsync())
            {
                var users = new List<User>
                {
                    new User
                    {
                        Id = "user-1-guid",
                        FirstName = "John",
                        LastName = "Doe",
                        Email = "john.doe@example.com",
                        PasswordHash = "hashed_password_here",
                        IsActive = true,
                        CreatedAt = DateTime.Now.AddDays(-30),
                        LastLogin = DateTime.Now.AddDays(-1)
                    },
                    new User
                    {
                        Id = "user-2-guid",
                        FirstName = "Jane",
                        LastName = "Smith",
                        Email = "jane.smith@example.com",
                        PasswordHash = "hashed_password_here",
                        IsActive = true,
                        CreatedAt = DateTime.Now.AddDays(-25),
                        LastLogin = DateTime.Now.AddDays(-2)
                    },
                    new User
                    {
                        Id = "user-3-guid",
                        FirstName = "Alice",
                        LastName = "Johnson",
                        Email = "alice.johnson@example.com",
                        PasswordHash = "hashed_password_here",
                        IsActive = true,
                        CreatedAt = DateTime.Now.AddDays(-15),
                        LastLogin = DateTime.Now.AddHours(-12)
                    }
                };

                await context.Users.AddRangeAsync(users);
                await context.SaveChangesAsync();
            }

            // Add events if none exist
            if (!await context.Events.AnyAsync())
            {
                var events = new List<Event>
                {
                    new Event
                    {
                        Title = "Tech Conference 2023",
                        Description = "A conference about the latest technologies and industry trends.",
                        StartDate = DateTime.Now.AddDays(30),
                        EndDate = DateTime.Now.AddDays(32),
                        Location = "Convention Center, New York",
                        MaxAttendees = 500,
                        IsPublic = true,
                        OrganizerId = "user-1-guid",
                        CreatedAt = DateTime.Now.AddDays(-20)
                    },
                    new Event
                    {
                        Title = "Web Development Workshop",
                        Description = "Learn how to build modern web applications using the latest frameworks.",
                        StartDate = DateTime.Now.AddDays(15),
                        EndDate = DateTime.Now.AddDays(15).AddHours(8),
                        Location = "Online",
                        MaxAttendees = 100,
                        IsPublic = true,
                        OrganizerId = "user-1-guid",
                        CreatedAt = DateTime.Now.AddDays(-10)
                    },
                    new Event
                    {
                        Title = "AI and Machine Learning Hackathon",
                        Description = "Collaborate with other developers to build AI-powered applications.",
                        StartDate = DateTime.Now.AddDays(45),
                        EndDate = DateTime.Now.AddDays(47),
                        Location = "Tech Hub, San Francisco",
                        MaxAttendees = 200,
                        IsPublic = true,
                        OrganizerId = "user-2-guid",
                        CreatedAt = DateTime.Now.AddDays(-15)
                    },
                    new Event
                    {
                        Title = "Mobile App Development Summit",
                        Description = "Discover the latest trends in mobile app development and design.",
                        StartDate = DateTime.Now.AddDays(60),
                        EndDate = DateTime.Now.AddDays(61),
                        Location = "Innovation Center, Chicago",
                        MaxAttendees = 150,
                        IsPublic = true,
                        OrganizerId = "user-2-guid",
                        CreatedAt = DateTime.Now.AddDays(-5)
                    },
                    new Event
                    {
                        Title = "Cybersecurity Symposium",
                        Description = "Expert talks and workshops about the latest in cybersecurity.",
                        StartDate = DateTime.Now.AddDays(20),
                        EndDate = DateTime.Now.AddDays(21),
                        Location = "Security Complex, Washington DC",
                        MaxAttendees = 300,
                        IsPublic = false,
                        OrganizerId = "user-3-guid",
                        CreatedAt = DateTime.Now.AddDays(-25)
                    }
                };

                await context.Events.AddRangeAsync(events);
                await context.SaveChangesAsync();
            }

            // Add attendees (optional)
            if (!await context.Attendees.AnyAsync())
            {
                // We need to load the events with their full entity
                var events = await context.Events.ToListAsync();
                
                // Process each event - we'll add attendees for each event
                foreach (var eventEntity in events)
                {
                    // Add 5 random attendees to each event
                    for (int i = 0; i < 5; i++)
                    {
                        var attendee = new Attendee
                        {
                            EventId = eventEntity.Id,
                            Event = eventEntity,  // Set the required Event navigation property
                            Name = $"Attendee {i+1}",
                            Email = $"attendee{i+1}_{eventEntity.Id}@example.com",
                            RegistrationDate = DateTime.Now.AddDays(-Random.Shared.Next(1, 10)),
                            HasAttended = false,
                            RegistrationCode = Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper()
                        };
                        
                        await context.Attendees.AddAsync(attendee);
                    }
                }
                
                await context.SaveChangesAsync();
            }
        }
    }
} 