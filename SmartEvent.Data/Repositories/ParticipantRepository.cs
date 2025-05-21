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
    public class ParticipantRepository : SmartEvent.Core.Interfaces.IParticipantRepository, IRepository<Participant>
    {
        private readonly AppDbContext _context;

        public ParticipantRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Participant>> GetParticipantsByEventIdAsync(int eventId)
        {
            return await _context.Participants
                .Where(a => a.EventId == eventId)
                .OrderBy(a => a.RegistrationDate)
                .ToListAsync();
        }

        public async Task<Participant> GetParticipantByIdAsync(int id)
        {
            return await _context.Participants.FindAsync(id);
        }

        public async Task<Participant> CreateParticipantAsync(Participant Participant)
        {
            // The RegistrationCode is now set in the service layer
            
            _context.Participants.Add(Participant);
            await _context.SaveChangesAsync();
            return Participant;
        }

        public async Task<bool> ParticipantExistsAsync(int eventId, string email)
        {
            return await _context.Participants
                .AnyAsync(a => a.EventId == eventId && a.Email == email);
        }

        public Task<bool> IsAlreadyRegisteredAsync(int eventId, string email)
        {
            return ParticipantExistsAsync(eventId, email);
        }

        public async Task<int> GetEventParticipantCountAsync(int eventId)
        {
            return await _context.Participants
                .CountAsync(a => a.EventId == eventId);
        }

        public async Task<IEnumerable<Participant>> GetByEmailAsync(string email)
        {
            return await _context.Participants
                .Where(a => a.Email == email)
                .Include(a => a.Event)
                .ToListAsync();
        }

        public async Task<Participant> GetByRegistrationCodeAsync(string code)
        {
            return await _context.Participants
                .Include(a => a.Event)
                .FirstOrDefaultAsync(a => a.RegistrationCode == code);
        }

        // IRepository<Participant> implementation
        public async Task<Participant> GetByIdAsync(object id)
        {
            return await GetParticipantByIdAsync((int)id);
        }

        public async Task<IEnumerable<Participant>> GetAllAsync()
        {
            return await _context.Participants.ToListAsync();
        }

        public async Task<IEnumerable<Participant>> FindAsync(Expression<Func<Participant, bool>> predicate)
        {
            return await _context.Participants.Where(predicate).ToListAsync();
        }

        public async Task AddAsync(Participant entity)
        {
            await _context.Participants.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<Participant> entities)
        {
            await _context.Participants.AddRangeAsync(entities);
        }

        public Task UpdateAsync(Participant entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return Task.CompletedTask;
        }

        public Task RemoveAsync(Participant entity)
        {
            _context.Participants.Remove(entity);
            return Task.CompletedTask;
        }

        public Task RemoveRangeAsync(IEnumerable<Participant> entities)
        {
            _context.Participants.RemoveRange(entities);
            return Task.CompletedTask;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
} 