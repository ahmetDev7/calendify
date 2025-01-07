namespace calendify_app.Models;

public class Attendance
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public Guid UserId { get; set; } // FK FROM USERS TABLE
    public User User { get; set; } = null!; // Required reference navigation to principal
}