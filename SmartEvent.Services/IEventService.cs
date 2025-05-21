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
        Task<IEnumerable<ParticipantDto>> GetEventParticipantsAsync(int eventId);
        Task<bool> RegisterParticipantAsync(ParticipantDto ParticipantDto);
        Task<ParticipantDto> GetParticipantByCodeAsync(string code);
        Task MarkParticipantAsAttendedAsync(int ParticipantId);
        //Task<EventDto> GetEventByIdAsync(int id);
    }
} 