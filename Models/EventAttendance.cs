namespace calendify_app.Models;

// https://learn.microsoft.com/en-us/ef/core/modeling/relationships/many-to-many
public class EventAttendance
{
    public Guid Id { get; set; }
    
    public Guid UserId { get; set; } // FK
    public Guid EventId { get; set; } // FK
    public Event Event { get; set; } = null!;
    public User User { get; set; } = null!;
    public int Rating { get; set; }
    public int Feedback { get; set; }
}