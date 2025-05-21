using Microsoft.EntityFrameworkCore;
using SmartEvent.Core.Interfaces;
using SmartEvent.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SmartEvent.Data.Repositories
{
    public class EventRepository : SmartEvent.Core.Interfaces.IEventRepository, IRepository<Event>
    {
        private readonly AppDbContext _context;

        public EventRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync()
        {
            return await _context.Events
                .OrderByDescending(e => e.StartDate)
                .ToListAsync();
        }

        public async Task<Event> GetEventByIdAsync(int id)
        {
            return await _context.Events.FindAsync(id);
        }

        public async Task<Event> CreateEventAsync(Event eventEntity)
        {
            _context.Events.Add(eventEntity);
            await _context.SaveChangesAsync();
            return eventEntity;
        }

        public async Task<Event> UpdateEventAsync(Event eventEntity)
        {
            _context.Entry(eventEntity).State = EntityState.Modified;
            eventEntity.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();
            return eventEntity;
        }

        public async Task<bool> DeleteEventAsync(int id)
        {
            var eventEntity = await _context.Events.FindAsync(id);
            if (eventEntity == null)
                return false;

            _context.Events.Remove(eventEntity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EventExistsAsync(int id)
        {
            return await _context.Events.AnyAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Event>> GetUpcomingEventsAsync()
        {
            return await _context.Events
                .Where(e => e.StartDate > DateTime.Now && e.IsPublic)
                .OrderBy(e => e.StartDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Event>> GetEventsByOrganizerAsync(string organizerId)
        {
            return await _context.Events
                .Where(e => e.OrganizerId == organizerId)
                .OrderByDescending(e => e.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Participant>> GetEventParticipantsAsync(int eventId)
        {
            return await _context.Participants
                .Where(a => a.EventId == eventId)
                .ToListAsync();
        }

        // IRepository<Event> implementation
        public async Task<Event> GetByIdAsync(object id)
        {
            return await GetEventByIdAsync((int)id);
        }

        public async Task<IEnumerable<Event>> GetAllAsync()
        {
            return await GetAllEventsAsync();
        }

        public async Task<IEnumerable<Event>> FindAsync(Expression<Func<Event, bool>> predicate)
        {
            return await _context.Events.Where(predicate).ToListAsync();
        }

        public async Task AddAsync(Event entity)
        {
            await _context.Events.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<Event> entities)
        {
            await _context.Events.AddRangeAsync(entities);
        }

        public Task UpdateAsync(Event entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return Task.CompletedTask;
        }

        public Task RemoveAsync(Event entity)
        {
            _context.Events.Remove(entity);
            return Task.CompletedTask;
        }

        public Task RemoveRangeAsync(IEnumerable<Event> entities)
        {
            _context.Events.RemoveRange(entities);
            return Task.CompletedTask;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
} 