using Microsoft.AspNetCore.Mvc;
using EventManagement.Application.Services;
using EventManagement.Domain.Entities;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace EventManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        public class CreateTicketRequest
        {
            public int EventId { get; set; }
            public int UserId { get; set; }
            public int Quantity { get; set; } // Optional, for future use
        }

        public class TicketResponse
        {
            public int Id { get; set; }
            public int EventId { get; set; }
            public string EventTitle { get; set; }
            public int UserId { get; set; }
            public string UserName { get; set; }
            public string TicketNumber { get; set; }
            public DateTime PurchaseDate { get; set; }
            public decimal Price { get; set; }
            public string Status { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket([FromBody] CreateTicketRequest request)
        {
            var ticket = new Ticket
            {
                EventId = request.EventId,
                UserId = request.UserId,
                // Set other properties as needed, e.g. PurchaseDate, Status, etc.
                PurchaseDate = System.DateTime.UtcNow,
                Status = "Booked",
                TicketNumber = Guid.NewGuid().ToString()
            };
            var created = await _ticketService.CreateTicketAsync(ticket);
            return Ok(created);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetTicketsByUser(int userId)
        {
            var tickets = await _ticketService.GetTicketsByUserIdAsync(userId);
            var result = tickets.Select(t => new TicketResponse
            {
                Id = t.Id,
                EventId = t.EventId,
                EventTitle = t.Event?.Title,
                UserId = t.UserId,
                UserName = t.User?.Username,
                TicketNumber = t.TicketNumber,
                PurchaseDate = t.PurchaseDate,
                Price = t.Price,
                Status = t.Status
            }).ToList();
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllTickets()
        {
            var tickets = await _ticketService.GetAllTicketsAsync();
            var result = tickets.Select(t => new TicketResponse
            {
                Id = t.Id,
                EventId = t.EventId,
                EventTitle = t.Event?.Title,
                UserId = t.UserId,
                UserName = t.User?.Username,
                TicketNumber = t.TicketNumber,
                PurchaseDate = t.PurchaseDate,
                Price = t.Price,
                Status = t.Status
            }).ToList();
            return Ok(result);
        }
    }
} 