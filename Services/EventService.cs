using System;
using System.Collections.Generic;
using System.Linq;
using calendify.Data;
using calendify_app.Models;
using Microsoft.EntityFrameworkCore;

public class EventService
{
    // slaat events lokaal op
    private static readonly List<Event> _event = new(); // FIXME: WEGDENKEN
    private readonly AppDbContext _db;


    public EventService(AppDbContext db){
        _db = db;
    }
  
    public DbSet<Event> GetAllEvents()
    {
        return _db.Event;
    }
   
    public async Task<bool>  CreateEvent(Event newEvent)
    {
        newEvent.Id = Guid.NewGuid();
        // CREATE EVENT IN DB

        newEvent.Date = DateTime.SpecifyKind(newEvent.Date, DateTimeKind.Utc);
        newEvent.StartTime = DateTime.SpecifyKind(newEvent.StartTime, DateTimeKind.Utc);
        newEvent.EndTime = DateTime.SpecifyKind(newEvent.EndTime, DateTimeKind.Utc);

        _db.Event.Add(newEvent);
        
        await _db.SaveChangesAsync();

        return true;
    }
   
    public Event ?UpdateEvent(Guid eventId, Event updatedEvent)
    {
        var existingEvent = _event.FirstOrDefault(e => e.Id == eventId);
        if (existingEvent == null)
        {
            return null;
        }

        existingEvent.Title = updatedEvent.Title;
        existingEvent.Date = updatedEvent.Date;
        existingEvent.Location = updatedEvent.Location;
        existingEvent.Description = updatedEvent.Description;

        return existingEvent;
    }

    public bool DeleteEvent(Guid eventId)
    {
        var eventToDelete = _db.Event.FirstOrDefault(e => e.Id == eventId);
        if (eventToDelete == null)
        {
            return false;
        }

        _db.Event.Remove(eventToDelete);
        
        _db.SaveChanges();

        return true;
    }

    public Event? GetEventById(Guid eventId)
    {
        return _event.FirstOrDefault(e => e.Id == eventId);
    }
}