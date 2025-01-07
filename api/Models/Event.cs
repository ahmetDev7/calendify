using Microsoft.EntityFrameworkCore;

namespace calendify_app.Models;

// https://learn.microsoft.com/en-us/ef/core/modeling/relationships/many-to-many
public class Event
{
    // Prevents the building error while seeding database
    public Event() { }

    public Event(
        Guid id,
        string title,
        string description,
        DateTime date,
        DateTime startTime,
        DateTime endTime,
        string location,
        bool adminApproval
    )
    {
        Id = id;
        Title = title;
        Description = description;
        Date = date;
        StartTime = startTime;
        EndTime = endTime;
        Location = location;
        AdminApproval = adminApproval;
    }


    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public DateTime Date { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public required string Location { get; set; }
    public bool AdminApproval { get; set; }
    public List<EventAttendance> EventAttendance { get; } = [];

    public int IsOngoing()
    {
        DateTime now = DateTime.Now;

        if (now < StartTime)
        {
            return -1;
        }

        if (now > EndTime)
        {
            return 1;
        }

        return 0; // Event is ongoing
    }
}