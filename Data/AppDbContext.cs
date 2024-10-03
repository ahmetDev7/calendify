using calendify_app.Models;
using Microsoft.EntityFrameworkCore;

namespace calendify.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Admin> Admin { get; set; }
        public DbSet<calendify_app.Models.Attendance> Attendance { get; set; }
        public DbSet<Event> Event { get; set; }
        public DbSet<EventAttendance> Event_Attendance { get; set; }
        public DbSet<User> User { get; set; }
        public object AttendanceRequest { get; internal set; }

        // On migration seed database table Event with events
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Event>().HasData(
                new Event
                {
                    Id = Guid.NewGuid(),
                    Title = "Dinner op kantoor",
                    Description = "baba",
                    Date = DateTime.SpecifyKind(new DateTime(2024, 10, 1, 21, 0, 0), DateTimeKind.Utc),
                    StartTime = DateTime.SpecifyKind(new DateTime(2024, 9, 1, 10, 0, 0), DateTimeKind.Utc),
                    EndTime = DateTime.SpecifyKind(new DateTime(2024, 9, 1, 10, 0, 0), DateTimeKind.Utc),
                    Location = "Rotterdam",
                    AdminApproval = true
                },
                new Event
                {
                    Id = Guid.NewGuid(),
                    Title = "Workshop Productiviteit",
                    Description = "Een interactieve sessie over het verbeteren van productiviteit op de werkvloer.",
                    Date = DateTime.SpecifyKind(new DateTime(2024, 10, 5, 13, 0, 0), DateTimeKind.Utc),
                    StartTime = DateTime.SpecifyKind(new DateTime(2024, 10, 5, 13, 0, 0), DateTimeKind.Utc),
                    EndTime = DateTime.SpecifyKind(new DateTime(2024, 10, 5, 15, 0, 0), DateTimeKind.Utc),
                    Location = "Amsterdam",
                    AdminApproval = true
                },
                new Event
                {
                    Id = Guid.NewGuid(),
                    Title = "Teambuilding Dag",
                    Description = "Een dag vol activiteiten om de samenwerking tussen teams te verbeteren.",
                    Date = DateTime.SpecifyKind(new DateTime(2024, 10, 10, 9, 0, 0), DateTimeKind.Utc),
                    StartTime = DateTime.SpecifyKind(new DateTime(2024, 10, 10, 9, 0, 0), DateTimeKind.Utc),
                    EndTime = DateTime.SpecifyKind(new DateTime(2024, 10, 10, 17, 0, 0), DateTimeKind.Utc),
                    Location = "Den Haag",
                    AdminApproval = true
                },
                new Event
                {
                    Id = Guid.NewGuid(),
                    Title = "Brainstorm Sessie",
                    Description = "Een creatieve sessie om nieuwe ideeën en innovaties te bedenken voor het bedrijf.",
                    Date = DateTime.SpecifyKind(new DateTime(2024, 10, 12, 11, 0, 0), DateTimeKind.Utc),
                    StartTime = DateTime.SpecifyKind(new DateTime(2024, 10, 12, 11, 0, 0), DateTimeKind.Utc),
                    EndTime = DateTime.SpecifyKind(new DateTime(2024, 10, 12, 13, 0, 0), DateTimeKind.Utc),
                    Location = "Rotterdam",
                    AdminApproval = false
                },
                new Event
                {
                    Id = Guid.NewGuid(),
                    Title = "Lunch & Learn",
                    Description = "Een informele lunch waarbij een spreker zijn expertise deelt over een vakgebied.",
                    Date = DateTime.SpecifyKind(new DateTime(2024, 10, 15, 12, 0, 0), DateTimeKind.Utc),
                    StartTime = DateTime.SpecifyKind(new DateTime(2024, 10, 15, 12, 0, 0), DateTimeKind.Utc),
                    EndTime = DateTime.SpecifyKind(new DateTime(2024, 10, 15, 13, 0, 0), DateTimeKind.Utc),
                    Location = "Utrecht",
                    AdminApproval = true
                },
                new Event
                {
                    Id = Guid.NewGuid(),
                    Title = "Presentatie Klantproject",
                    Description = "Presentatie van het nieuwe project voor een belangrijke klant.",
                    Date = DateTime.SpecifyKind(new DateTime(2024, 10, 20, 14, 0, 0), DateTimeKind.Utc),
                    StartTime = DateTime.SpecifyKind(new DateTime(2024, 10, 20, 14, 0, 0), DateTimeKind.Utc),
                    EndTime = DateTime.SpecifyKind(new DateTime(2024, 10, 20, 15, 30, 0), DateTimeKind.Utc),
                    Location = "Eindhoven",
                    AdminApproval = true
                },
                new Event
                {
                    Id = Guid.NewGuid(),
                    Title = "Vrijmibo op kantoor",
                    Description = "Vrijdagmiddagborrel om de werkweek af te sluiten met collega's.",
                    Date = DateTime.SpecifyKind(new DateTime(2024, 10, 25, 16, 30, 0), DateTimeKind.Utc),
                    StartTime = DateTime.SpecifyKind(new DateTime(2024, 10, 25, 16, 30, 0), DateTimeKind.Utc),
                    EndTime = DateTime.SpecifyKind(new DateTime(2024, 10, 25, 18, 30, 0), DateTimeKind.Utc),
                    Location = "Rotterdam",
                    AdminApproval = true
                }
            );
        }
    }
}