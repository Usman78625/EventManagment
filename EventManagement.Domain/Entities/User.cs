using System;
using System.Collections.Generic;

namespace EventManagement.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? AvatarUrl { get; set; }
        public virtual ICollection<Event> OrganizedEvents { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
    }
} 