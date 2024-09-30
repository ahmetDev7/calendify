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

    public Event? GetEventById(Guid eventId) => _db.Event.FirstOrDefault(e => e.Id == eventId);
}

