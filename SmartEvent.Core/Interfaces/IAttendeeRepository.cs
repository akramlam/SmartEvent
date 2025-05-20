using SmartEvent.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartEvent.Core.Interfaces
{
    public interface IAttendeeRepository
    {
        Task<IEnumerable<Attendee>> GetAttendeesByEventIdAsync(int eventId);
        Task<Attendee> GetAttendeeByIdAsync(int id);
        Task<Attendee> CreateAttendeeAsync(Attendee attendee);
        Task<bool> AttendeeExistsAsync(int eventId, string email);
        Task<int> GetEventAttendeeCountAsync(int eventId);
        Task<IEnumerable<Attendee>> GetByEmailAsync(string email);
        Task<Attendee> GetByRegistrationCodeAsync(string code);
    }
} 