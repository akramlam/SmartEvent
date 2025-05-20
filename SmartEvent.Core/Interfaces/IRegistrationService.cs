using SmartEvent.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;
using SmartEvent.Core.Models;

namespace SmartEvent.Core.Interfaces
{
    public interface IRegistrationService
    {
        Task<AttendeeDto> RegisterForEventAsync(int eventId, RegistrationDto registrationDto);
        Task<IEnumerable<AttendeeDto>> GetEventAttendeesAsync(int eventId);
        Task<bool> IsEventFullAsync(int eventId);
    }
} 