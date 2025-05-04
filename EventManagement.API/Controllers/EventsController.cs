using System.Collections.Generic;
using System.Threading.Tasks;
using EventManagement.Application.DTOs;
using EventManagement.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EventManagement.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using EventManagement.Infrastructure.Data;

namespace EventManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public EventsController(IEventService eventService, ApplicationDbContext context, IWebHostEnvironment env)
        {
            _eventService = eventService;
            _context = context;
            _env = env;
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
        [HttpGet("search")]
        [AllowAnonymous]
                public async Task<IActionResult> SearchEvents([FromQuery] string? title, [FromQuery] string? location, [FromQuery] string? category)
    {
                    var events = await _eventService.SearchEventsAsync(title, location, category);
                    return Ok(events);
                }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditEvent(int id, [FromBody] Event eventObj)
        {
            var updated = await _eventService.UpdateEventAsync(id, eventObj);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            await _eventService.DeleteEventAsync(id);
            return Ok(new { message = "Event deleted" });
        }

        [HttpPost("{id}/images")]
        [Authorize(Roles = "Admin,Organizer")]
        public async Task<IActionResult> UploadEventImages(int id, [FromForm] IFormFileCollection files)
        {
            var @event = await _context.Events.FindAsync(id);
            if (@event == null) return NotFound();
            if (files == null || files.Count == 0) return BadRequest("No files uploaded");

            var uploads = Path.Combine(_env.WebRootPath, "event-images");
            if (!Directory.Exists(uploads)) Directory.CreateDirectory(uploads);
            var urls = new List<string>();
            foreach (var file in files)
            {
                var fileName = $"event_{id}_{Path.GetRandomFileName()}{Path.GetExtension(file.FileName)}";
                var filePath = Path.Combine(uploads, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                urls.Add($"/event-images/{fileName}");
            }
            if (@event.ImageUrls == null) @event.ImageUrls = new List<string>();
            foreach (var url in urls) @event.ImageUrls.Add(url);
            await _context.SaveChangesAsync();
            return Ok(new { imageUrls = @event.ImageUrls });
        }

        // [HttpGet("search")]
        // public async Task<IActionResult> SearchEvents([FromQuery] string? title, [FromQuery] string? location, [FromQuery] string? category)
        // {
        //     var events = await _eventService.SearchEventsAsync(title, location, category);
        //     return Ok(events);
        // }
    }
} 