using Microsoft.EntityFrameworkCore;
using SmartEvent.Core.Interfaces;
using SmartEvent.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SmartEvent.Data.Repositories
{
    public class AttendeeRepository : SmartEvent.Core.Interfaces.IAttendeeRepository, IRepository<Attendee>
    {
        private readonly SmartEventDbContext _context;

        public AttendeeRepository(SmartEventDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Attendee>> GetAttendeesByEventIdAsync(int eventId)
        {
            return await _context.Attendees
                .Where(a => a.EventId == eventId)
                .OrderBy(a => a.RegistrationDate)
                .ToListAsync();
        }

        public async Task<Attendee> GetAttendeeByIdAsync(int id)
        {
            return await _context.Attendees.FindAsync(id);
        }

        public async Task<Attendee> CreateAttendeeAsync(Attendee attendee)
        {
            // The RegistrationCode is now set in the service layer
            
            _context.Attendees.Add(attendee);
            await _context.SaveChangesAsync();
            return attendee;
        }

        public async Task<bool> AttendeeExistsAsync(int eventId, string email)
        {
            return await _context.Attendees
                .AnyAsync(a => a.EventId == eventId && a.Email == email);
        }

        public Task<bool> IsAlreadyRegisteredAsync(int eventId, string email)
        {
            return AttendeeExistsAsync(eventId, email);
        }

        public async Task<int> GetEventAttendeeCountAsync(int eventId)
        {
            return await _context.Attendees
                .CountAsync(a => a.EventId == eventId);
        }

        public async Task<IEnumerable<Attendee>> GetByEmailAsync(string email)
        {
            return await _context.Attendees
                .Where(a => a.Email == email)
                .Include(a => a.Event)
                .ToListAsync();
        }

        public async Task<Attendee> GetByRegistrationCodeAsync(string code)
        {
            return await _context.Attendees
                .Include(a => a.Event)
                .FirstOrDefaultAsync(a => a.RegistrationCode == code);
        }

        // IRepository<Attendee> implementation
        public async Task<Attendee> GetByIdAsync(object id)
        {
            return await GetAttendeeByIdAsync((int)id);
        }

        public async Task<IEnumerable<Attendee>> GetAllAsync()
        {
            return await _context.Attendees.ToListAsync();
        }

        public async Task<IEnumerable<Attendee>> FindAsync(Expression<Func<Attendee, bool>> predicate)
        {
            return await _context.Attendees.Where(predicate).ToListAsync();
        }

        public async Task AddAsync(Attendee entity)
        {
            await _context.Attendees.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<Attendee> entities)
        {
            await _context.Attendees.AddRangeAsync(entities);
        }

        public Task UpdateAsync(Attendee entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return Task.CompletedTask;
        }

        public Task RemoveAsync(Attendee entity)
        {
            _context.Attendees.Remove(entity);
            return Task.CompletedTask;
        }

        public Task RemoveRangeAsync(IEnumerable<Attendee> entities)
        {
            _context.Attendees.RemoveRange(entities);
            return Task.CompletedTask;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
} 