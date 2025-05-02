using System;

namespace EventManagement.Domain.Entities
{
    public class Ticket
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public int UserId { get; set; }
        public string TicketNumber { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
        
        public virtual Event Event { get; set; }
        public virtual User User { get; set; }
    }
} 