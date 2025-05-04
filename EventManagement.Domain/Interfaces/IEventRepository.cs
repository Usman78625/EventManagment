using System.Collections.Generic;
using System.Threading.Tasks;
using EventManagement.Domain.Entities;

namespace EventManagement.Domain.Interfaces
{
    public interface IEventRepository
    {
        Task<Event> GetByIdAsync(int id);
        Task<IEnumerable<Event>> GetAllAsync();
        Task<IEnumerable<Event>> GetByOrganizerIdAsync(int organizerId);
        Task AddAsync(Event @event);
        Task UpdateAsync(Event @event);
        Task DeleteAsync(Event eventObj);
    }
} 