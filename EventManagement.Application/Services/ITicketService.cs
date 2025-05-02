using System.Collections.Generic;
using System.Threading.Tasks;
using EventManagement.Domain.Entities;

namespace EventManagement.Application.Services
{
    public interface ITicketService
    {
        Task<Ticket> CreateTicketAsync(Ticket ticket);
        Task<IEnumerable<Ticket>> GetTicketsByUserIdAsync(int userId);
        Task<IEnumerable<Ticket>> GetAllTicketsAsync();
        // Add more methods as needed
    }
} 