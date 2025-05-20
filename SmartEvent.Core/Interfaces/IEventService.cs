using SmartEvent.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartEvent.Core.Interfaces
{
    public interface IEventService
    {
        Task<IEnumerable<EventDto>> GetAllEventsAsync();
        Task<EventDto> GetEventByIdAsync(int id);
        Task<EventDto> CreateEventAsync(EventCreateDto eventDto);
        Task<EventDto> UpdateEventAsync(int id, EventCreateDto eventDto);
        Task<bool> DeleteEventAsync(int id);
    }
} 