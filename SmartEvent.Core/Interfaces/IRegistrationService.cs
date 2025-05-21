using SmartEvent.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using SmartEvent.Core.Models;

namespace SmartEvent.Core.Interfaces
{
    public interface IRegistrationService
    {
        Task<ParticipantDto> RegisterForEventAsync(int eventId, RegistrationDto registrationDto);
        Task<IEnumerable<ParticipantDto>> GetEventParticipantsAsync(int eventId);
        Task<bool> IsEventFullAsync(int eventId);
    }
} 