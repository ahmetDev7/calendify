namespace calendify_app.Models;

public class EventAttendance
{
    // TODO: Dit is een tussen tabel nalopen voor correcte implentatie [IS MANY TO MANY RELATION]
    
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid EventId { get; set; }
    public int Rating { get; set; }
    public int Feedback { get; set; }
}