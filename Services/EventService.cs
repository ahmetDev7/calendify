using System;
using System.Collections.Generic;
using System.Linq;
using calendify_app.Models;

public class EventService
{
    // slaat events lokaal op
    private static readonly List<Event> _event = new();

  
    public IEnumerable<Event> GetAllEvents()
    {
        return _event;
    }
   
    public Event CreateEvent(Event newEvent)
    {
        if (newEvent == null)
        {
            throw new ArgumentNullException(nameof(newEvent));
        }

        newEvent.Id = Guid.NewGuid();
        _event.Add(newEvent);
        return newEvent;
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
        var eventToDelete = _event.FirstOrDefault(e => e.Id == eventId);
        if (eventToDelete == null)
        {
            return false;
        }

        _event.Remove(eventToDelete);
        return true;
    }

    public Event? GetEventById(Guid eventId)
    {
        return _event.FirstOrDefault(e => e.Id == eventId);
    }
}


