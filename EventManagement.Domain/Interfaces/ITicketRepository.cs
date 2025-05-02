using System.Collections.Generic;
using System.Threading.Tasks;
using EventManagement.Domain.Entities;

namespace EventManagement.Domain.Interfaces
{
    public interface ITicketRepository
    {
        Task<Ticket> GetByIdAsync(int id);
        Task<IEnumerable<Ticket>> GetByUserIdAsync(int userId);
        Task<IEnumerable<Ticket>> GetByEventIdAsync(int eventId);
        Task<IEnumerable<Ticket>> GetAllAsync();
        Task AddAsync(Ticket ticket);
        Task UpdateAsync(Ticket ticket);
        Task DeleteAsync(Ticket ticket);
    }
} 