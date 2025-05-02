using System.Collections.Generic;
using System.Threading.Tasks;
using EventManagement.Application.DTOs;
using EventManagement.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EventManagement.Domain.Entities;

namespace EventManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<EventDTO>>> GetAllEvents()
        {
            var events = await _eventService.GetAllEventsAsync();
            return Ok(events);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<EventDTO>> GetEvent(int id)
        {
            var @event = await _eventService.GetEventByIdAsync(id);
            if (@event == null)
                return NotFound();

            return Ok(@event);
        }

        [HttpGet("organizer/{organizerId}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<EventDTO>>> GetEventsByOrganizer(int organizerId)
        {
            var events = await _eventService.GetEventsByOrganizerAsync(organizerId);
            return Ok(events);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateEvent([FromBody] Event eventObj)
        {
            var created = await _eventService.CreateEventAsync(eventObj);
            return Ok(created);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditEvent(int id, [FromBody] Event eventObj)
        {
            var updated = await _eventService.UpdateEventAsync(id, eventObj);
            return Ok(updated);
        }
    }
} 