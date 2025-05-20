using SmartEvent.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartEvent.Data.Repositories
{
    public interface IAttendeeRepository : IRepository<Attendee>
    {
        Task<IEnumerable<Attendee>> GetByEmailAsync(string email);
        Task<Attendee> GetByRegistrationCodeAsync(string code);
        Task<bool> IsAlreadyRegisteredAsync(int eventId, string email);
    }
} 