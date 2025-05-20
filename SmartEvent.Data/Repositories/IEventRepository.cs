using SmartEvent.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartEvent.Data.Repositories
{
    public interface IEventRepository : IRepository<Event>
    {
        Task<IEnumerable<Event>> GetUpcomingEventsAsync();
        Task<IEnumerable<Event>> GetEventsByOrganizerAsync(string organizerId);
        Task<IEnumerable<Attendee>> GetEventAttendeesAsync(int eventId);
    }
} 