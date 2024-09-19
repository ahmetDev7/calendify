namespace calendify_app.Models;

public class Attendance
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; } // TODO: FK CHECK CORRECT IMPLEMENTATION
    public DateTime Date { get; set; }
}