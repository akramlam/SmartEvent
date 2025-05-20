using SmartEvent.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartEvent.Core.Interfaces
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetAllEventsAsync();
        Task<Event> GetEventByIdAsync(int id);
        Task<Event> CreateEventAsync(Event eventEntity);
        Task<Event> UpdateEventAsync(Event eventEntity);
        Task<bool> DeleteEventAsync(int id);
        Task<bool> EventExistsAsync(int id);
        Task<IEnumerable<Event>> GetUpcomingEventsAsync();
        Task<IEnumerable<Event>> GetEventsByOrganizerAsync(string organizerId);
    }
} 