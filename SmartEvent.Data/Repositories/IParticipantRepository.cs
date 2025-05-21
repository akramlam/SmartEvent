using SmartEvent.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartEvent.Data.Repositories
{
    public interface IParticipantRepository : IRepository<Participant>
    {
        Task<IEnumerable<Participant>> GetByEmailAsync(string email);
        Task<Participant> GetByRegistrationCodeAsync(string code);
        Task<bool> IsAlreadyRegisteredAsync(int eventId, string email);
    }
} 