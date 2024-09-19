using System;
using System.Collections.Generic;
using System.Linq;

public class EventService
{
    // Simuleer in-memory event opslag
    private static readonly List<Event> Events = new();

    public IEnumerable<Event> GetAllEvents()
    {
        return Events;
    }

    public Event CreateEvent(Event newEvent)
    {
        if (newEvent == null)
        {
            throw new ArgumentNullException(nameof(newEvent));
        }

        newEvent.Id = Events.Count > 0 ? Events.Max(e => e.Id) + 1 : 1;
        Events.Add(newEvent);
        return newEvent;
    }

    public Event UpdateEvent(int eventId, Event updatedEvent)
    {
        var existingEvent = Events.FirstOrDefault(e => e.Id == eventId);
        if (existingEvent == null)
        {
            return null;
        }

        existingEvent.Name = updatedEvent.Name;
        existingEvent.Date = updatedEvent.Date;
        existingEvent.Location = updatedEvent.Location;
        existingEvent.Description = updatedEvent.Description;

        return existingEvent;
    }

    public bool DeleteEvent(int eventId)
    {
        var eventToDelete = Events.FirstOrDefault(e => e.Id == eventId);
        if (eventToDelete == null)
        {
            return false;
        }

        Events.Remove(eventToDelete);
        return true;
    }

    public Event GetEventById(int eventId)
    {
        return Events.FirstOrDefault(e => e.Id == eventId);
    }
}


