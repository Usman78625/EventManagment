using System.Collections.Generic;
using System.Threading.Tasks;
using EventManagement.Application.DTOs;
using EventManagement.Domain.Entities;
using EventManagement.Domain.Interfaces;
using System.Linq;
using System;

namespace EventManagement.Application.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<IEnumerable<EventDTO>> GetAllEventsAsync()
        {
            var events = await _eventRepository.GetAllAsync();
            return MapToDTOs(events);
        }

        public async Task<EventDTO> GetEventByIdAsync(int id)
        {
            var @event = await _eventRepository.GetByIdAsync(id);
            return @event == null ? null : MapToDTO(@event);
        }

        public async Task<IEnumerable<EventDTO>> GetEventsByOrganizerAsync(int organizerId)
        {
            var events = await _eventRepository.GetByOrganizerIdAsync(organizerId);
            return MapToDTOs(events);
        }

        public async Task<EventDTO> CreateEventAsync(EventDTO eventDTO)
        {
            var @event = new Event
            {
                Title = eventDTO.Title,
                Description = eventDTO.Description,
                StartDate = eventDTO.StartDate,
                EndDate = eventDTO.EndDate,
                Location = eventDTO.Location,
                AvailableTickets = eventDTO.AvailableTickets,
                TicketPrice = eventDTO.TicketPrice,
                OrganizerId = eventDTO.OrganizerId,
                CreatedAt = System.DateTime.UtcNow
            };
            await _eventRepository.AddAsync(@event);
            return MapToDTO(@event);
        }

        public async Task<Event> CreateEventAsync(Event @event)
        {
            @event.CreatedAt = System.DateTime.UtcNow;
            @event.StartDate = DateTime.SpecifyKind(@event.StartDate, DateTimeKind.Utc);
            @event.EndDate = DateTime.SpecifyKind(@event.EndDate, DateTimeKind.Utc);
            await _eventRepository.AddAsync(@event);
            return @event;
        }

        public async Task<Event> UpdateEventAsync(int id, Event eventObj)
        {
            var existing = await _eventRepository.GetByIdAsync(id);
            if (existing == null) return null;
            existing.Title = eventObj.Title;
            existing.Description = eventObj.Description;
            existing.StartDate = eventObj.StartDate;
            existing.EndDate = eventObj.EndDate;
            existing.Location = eventObj.Location;
            existing.AvailableTickets = eventObj.AvailableTickets;
            existing.TicketPrice = eventObj.TicketPrice;
            existing.UpdatedAt = System.DateTime.UtcNow;
            await _eventRepository.UpdateAsync(existing);
            return existing;
        }

        public async Task<IEnumerable<EventDTO>> SearchEventsAsync(string? title, string? location, string? category)
        {
            var events = await _eventRepository.GetAllAsync();
            var query = events.AsQueryable();
            if (!string.IsNullOrEmpty(title))
                query = query.Where(e => e.Title.Contains(title));
            if (!string.IsNullOrEmpty(location))
                query = query.Where(e => e.Location.Contains(location));
            if (!string.IsNullOrEmpty(category))
                query = query.Where(e => e.Category == category);
            return MapToDTOs(query.ToList());
        }

        public async Task DeleteEventAsync(int id)
        {
            var existing = await _eventRepository.GetByIdAsync(id);
            if (existing != null)
            {
                await _eventRepository.DeleteAsync(existing);
            }
        }

        private EventDTO MapToDTO(Event @event)
        {
            return new EventDTO
            {
                Id = @event.Id,
                Title = @event.Title,
                Description = @event.Description,
                StartDate = @event.StartDate,
                EndDate = @event.EndDate,
                Location = @event.Location,
                AvailableTickets = @event.AvailableTickets,
                TicketPrice = @event.TicketPrice,
                OrganizerId = @event.OrganizerId,
                CreatedAt = @event.CreatedAt
            };
        }

        private IEnumerable<EventDTO> MapToDTOs(IEnumerable<Event> events)
        {
            foreach (var e in events)
                yield return MapToDTO(e);
        }
    }
} 