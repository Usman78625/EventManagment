using System;
using System.Collections.Generic;

namespace EventManagement.Domain.Entities
{
    public class Event
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
        public DateTime? UpdatedAt { get; set; }
        
        public virtual User? Organizer { get; set; }
        public virtual ICollection<Ticket>? Tickets { get; set; }
    }
} 