using System.Collections.Generic;
using System.Threading.Tasks;
using EventManagement.Application.DTOs;
using EventManagement.Domain.Entities;

namespace EventManagement.Application.Services
{
    public interface IEventService
    {
        Task<IEnumerable<EventDTO>> GetAllEventsAsync();
        Task<EventDTO> GetEventByIdAsync(int id);
        Task<IEnumerable<EventDTO>> GetEventsByOrganizerAsync(int organizerId);
        Task<EventDTO> CreateEventAsync(EventDTO eventDTO);
        Task<Event> CreateEventAsync(Event eventObj);
        Task<Event> UpdateEventAsync(int id, Event eventObj);
    }
}