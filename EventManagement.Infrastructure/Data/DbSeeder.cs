using EventManagement.Domain.Entities;

namespace EventManagement.Infrastructure.Data
{
    public static class DbSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            // Seed Users
            if (!context.Users.Any())
            {
                var user1 = new User { Username = "admin", Email = "admin@example.com", PasswordHash = "adminpass", Role = "Admin" };
                var user2 = new User { Username = "organizer", Email = "organizer@example.com", PasswordHash = "organizerpass", Role = "Organizer" };
                var user3 = new User { Username = "user", Email = "user@example.com", PasswordHash = "userpass", Role = "User" };
                context.Users.AddRange(user1, user2, user3);
                context.SaveChanges();
            }

            // Seed Events
            if (!context.Events.Any())
            {
                var organizer = context.Users.FirstOrDefault(u => u.Role == "Organizer");
               var event1 = new Event
{
    Title = "Tech Conference",
    Description = "A big tech event",
    Location = "NYC",
    StartDate = DateTime.UtcNow,
    EndDate = DateTime.UtcNow.AddDays(1),
    TicketPrice = 100,
    AvailableTickets = 200,
    OrganizerId = organizer?.Id ?? 1
};
var event2 = new Event
{
    Title = "Music Festival",
    Description = "Live music!",
    Location = "LA",
    StartDate = DateTime.UtcNow.AddDays(10),
    EndDate = DateTime.UtcNow.AddDays(12),
    TicketPrice = 150,
    AvailableTickets = 500,
    OrganizerId = organizer?.Id ?? 1
};
                context.Events.AddRange(event1, event2);
                context.SaveChanges();
            }

            // Seed Tickets
            if (!context.Tickets.Any())
            {
                var user = context.Users.FirstOrDefault(u => u.Role == "User");
                var event1 = context.Events.FirstOrDefault();
                if (user != null && event1 != null)
                {
                    var ticket = new Ticket
                    {
                        EventId = event1.Id,
                        UserId = user.Id,
                        TicketNumber = Guid.NewGuid().ToString(),
                        Status = "Booked"
                    };
                    context.Tickets.Add(ticket);
                    context.SaveChanges();
                }
            }
        }
    }
} 