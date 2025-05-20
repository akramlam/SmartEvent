using SmartEvent.Services.DTOs;
using SmartEvent.Core.Interfaces;
using SmartEvent.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartEvent.Core.DTOs;

namespace SmartEvent.Services
{
    public interface IEventService
    {
        Task<IEnumerable<EventDto>> GetAllEventsAsync();
        Task<IEnumerable<EventDto>> GetUpcomingEventsAsync();
        Task<EventDto> GetEventByIdAsync(int id);
        Task<IEnumerable<EventDto>> GetEventsByOrganizerAsync(string organizerId);
        Task<int> CreateEventAsync(EventDto eventDto);
        Task UpdateEventAsync(EventDto eventDto);
        Task DeleteEventAsync(int id);
        Task<IEnumerable<AttendeeDto>> GetEventAttendeesAsync(int eventId);
        Task<bool> RegisterAttendeeAsync(AttendeeDto attendeeDto);
        Task<AttendeeDto> GetAttendeeByCodeAsync(string code);
        Task MarkAttendeeAsAttendedAsync(int attendeeId);
        //Task<EventDto> GetEventByIdAsync(int id);
    }
} 