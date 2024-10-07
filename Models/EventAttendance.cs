namespace calendify_app.Models;

// https://learn.microsoft.com/en-us/ef/core/modeling/relationships/many-to-many
public class EventAttendance
{
    public Guid Id { get; set; }
    
    public Guid UserId { get; set; } // FK
    public Guid EventId { get; set; } // FK
    public Event Event { get; set; } = null!;
    public User User { get; set; } = null!;
    
    private int? _rating;
    public int? Rating {
        get => _rating;
        set {
            if(value == null || value < 0){
                _rating = null;
                return;
            }

            if(value > 10){
                _rating = 10;
                return;
            }

            _rating = value;
        }
    }
    public string? Feedback { get; set; }
}