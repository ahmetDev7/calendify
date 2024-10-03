using calendify.Data;
using calendify_app.Models;
using Microsoft.EntityFrameworkCore;
using static calendify.Controllers.EventController;

public class EventService
{
    private readonly AppDbContext _db;

    public EventService(AppDbContext db)
    {
        _db = db;
    }

    public DbSet<Event> GetAllEvents()
    {
        return _db.Event;
    }

    public Event? CreateEvent(Event newEvent)
    {
        try
        {
            newEvent.Id = Guid.NewGuid();
            newEvent.Date = DateTime.SpecifyKind(newEvent.Date, DateTimeKind.Utc);
            newEvent.StartTime = DateTime.SpecifyKind(newEvent.StartTime, DateTimeKind.Utc);
            newEvent.EndTime = DateTime.SpecifyKind(newEvent.EndTime, DateTimeKind.Utc);

            _db.Event.Add(newEvent);
            _db.SaveChanges();

            return newEvent;
        }
        catch (Exception e)
        {
            // TODO: Log error
            return null;
        }
    }

    public Event? UpdateEvent(Guid eventId, UpdateEventDto updateRequest)
    {
        try
        {
            Event? existingEvent = GetEventById(eventId);
            if (existingEvent == null) return null;

            if (!string.IsNullOrEmpty(updateRequest.Title)) existingEvent.Title = updateRequest.Title;
            if (!string.IsNullOrEmpty(updateRequest.Description)) existingEvent.Title = updateRequest.Description;
            if (updateRequest.Date.HasValue) existingEvent.Date = DateTime.SpecifyKind((DateTime)updateRequest.Date, DateTimeKind.Utc);
            if (updateRequest.StartTime.HasValue) existingEvent.Date = DateTime.SpecifyKind((DateTime)updateRequest.StartTime, DateTimeKind.Utc);
            if (updateRequest.EndTime.HasValue) existingEvent.Date = DateTime.SpecifyKind((DateTime)updateRequest.EndTime, DateTimeKind.Utc);
            if (!string.IsNullOrEmpty(updateRequest.Location)) existingEvent.Location = updateRequest.Location;
            if (!string.IsNullOrEmpty(updateRequest.Title)) existingEvent.Title = updateRequest.Title;
            if (updateRequest.AdminApproval.HasValue) existingEvent.AdminApproval = (bool)updateRequest.AdminApproval;

            _db.SaveChanges();

            return existingEvent;
        }
        catch (Exception e)
        {
            // TODO: Log error
            return null;
        }
    }

    public bool DeleteEvent(Guid eventId)
    {
        try
        {
            var eventToDelete = GetEventById(eventId);
            if (eventToDelete == null)
            {
                return false;
            }

            _db.Event.Remove(eventToDelete);
            _db.SaveChanges();
            return true;
        }
        catch (Exception e)
        {
            // TODO: Log error
            return false;
        }
    }

    // Adds a user to an event
    public EventAttendance AttendEvent(AttendEventDto request)
    {
        DateTime now = DateTime.UtcNow.ToLocalTime();

        var newEventAttendance = new EventAttendance();
        newEventAttendance.EventId = request.EventId;
        newEventAttendance.UserId = request.UserId;

        _db.Event_Attendance.Add(newEventAttendance);
        _db.SaveChanges();

        return newEventAttendance;
    }

    // User can add a rating or feedback to a specific event that he attended [users that never attended this event cannot add a rating or event]
    public EventAttendance? UpdateEventAttendanceByUser(UpdateAttendedEvent request)
    {
        EventAttendance? toUpdateEventAttendance = GetEventAttendance(eventId: request.EventId, userId: request.UserId);

        if (toUpdateEventAttendance == null) return null;

        if (!string.IsNullOrEmpty(request.Feedback))
        {
            toUpdateEventAttendance.Feedback = request.Feedback;
        }

        if (request.Rating.HasValue)
        {
            toUpdateEventAttendance.Rating = request.Rating;
        }

        _db.SaveChanges();

        return toUpdateEventAttendance;
    }

    public Event? GetEventById(Guid eventId) => _db.Event.FirstOrDefault(e => e.Id == eventId);

    
    public EventAttendance? GetEventAttendance(Guid eventId, Guid userId) => _db.Event_Attendance.FirstOrDefault(e => e.EventId == eventId && e.UserId == userId);



    public bool EventExists(Guid eventId) => GetEventById(eventId) != null;

    public bool UserAttendanceExists(Guid eventId, Guid userId) => _db.Event_Attendance.Where(x => x.UserId == userId && x.EventId == eventId).Any();

    public int EventAttendanceWindowIsOpen(Guid eventId)
    {
        return _db.Event.Where(x => x.Id == eventId).First().IsOngoing();
    }


    public EventWithAttendeesDto? GetEventWithAttendees(Guid eventId)
    {
        var eventItem = _db.Event
            .Include(e => e.EventAttendance)  // Eagerly load EventAttendance
            .FirstOrDefault(e => e.Id == eventId);

        if (eventItem == null)
        {
            return null;
        }

        return new EventWithAttendeesDto
        {
            Id = eventItem.Id,
            Title = eventItem.Title,
            Description = eventItem.Description,
            Date = eventItem.Date,
            StartTime = eventItem.StartTime,
            EndTime = eventItem.EndTime,
            Location = eventItem.Location,
            AdminApproval = eventItem.AdminApproval,
            Attendees = eventItem.EventAttendance.Select(i => new EventAttendeesDto
            {
                UserId = i.UserId,
                Rating = i.Rating,
                Feedback = i.Feedback
            }).ToList()
        };
    }

    public class EventWithAttendeesDto
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string? Location { get; set; }
        public bool AdminApproval { get; set; }
        public List<EventAttendeesDto>? Attendees { get; set; }
    }

    public class EventAttendeesDto()
    {
        public Guid UserId { get; set; }
        public int? Rating { get; set; }
        public string? Feedback { get; set; }
    }
}

