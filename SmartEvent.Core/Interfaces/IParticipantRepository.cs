using SmartEvent.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartEvent.Core.Interfaces
{
    public interface IParticipantRepository
    {
        Task<IEnumerable<Participant>> GetParticipantsByEventIdAsync(int eventId);
        Task<Participant> GetParticipantByIdAsync(int id);
        Task<Participant> CreateParticipantAsync(Participant Participant);
        Task<bool> ParticipantExistsAsync(int eventId, string email);
        Task<int> GetEventParticipantCountAsync(int eventId);
        Task<IEnumerable<Participant>> GetByEmailAsync(string email);
        Task<Participant> GetByRegistrationCodeAsync(string code);
    }
} 