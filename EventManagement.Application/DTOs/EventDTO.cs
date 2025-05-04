using System;

namespace EventManagement.Application.DTOs
{
    public class EventDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Location { get; set; }
        public int AvailableTickets { get; set; }
        public decimal TicketPrice { get; set; }
        public int OrganizerId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Category { get; set; }
    }
} 