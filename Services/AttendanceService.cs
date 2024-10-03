using System;
using System.Collections.Generic;
using System.Linq;
using calendify.Data;
using calendify_app.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using calendify_app.Models;

public class AttendanceService
{
    private readonly AppDbContext _db;

    public DbSet<calendify_app.Models.Attendance> GetAllAttendance()
    {
        return _db.Attendance;
    }

    public AttendanceService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<bool> CreateAttendance(AttendanceRequest request)
    {
        // Ensure the Date property is in UTC
        request.Date = DateTime.SpecifyKind(request.Date, DateTimeKind.Utc);
        Attendance NewAttendee = new Attendance();
        NewAttendee = request;

        // Add the new attendee to the database context
        _db.Add(request);

        // Save changes to the database
        await _db.SaveChangesAsync();

        return true;
    }

    public bool UserExists(Guid userId)
    {
        return _db.User.FirstOrDefault(e => e.Id == userId) != null;
    }


}

//     public Event ?UpdateEvent(Guid eventId, Event updatedEvent)
//     {
//         var existingEvent = _event.FirstOrDefault(e => e.Id == eventId);
//         if (existingEvent == null)
//         {
//             return null;
//         }

//         existingEvent.Title = updatedEvent.Title;
//         existingEvent.Date = updatedEvent.Date;
//         existingEvent.Location = updatedEvent.Location;
//         existingEvent.Description = updatedEvent.Description;

//         return existingEvent;
//     }

//     public bool DeleteEvent(Guid eventId)
//     {
//         var eventToDelete = _db.Event.FirstOrDefault(e => e.Id == eventId);
//         if (eventToDelete == null)
//         {
//             return false;
//         }

//         _db.Event.Remove(eventToDelete);

//         _db.SaveChanges();

//         return true;
//     }

//     public Event? GetEventById(Guid eventId)
//     {
//         return _event.FirstOrDefault(e => e.Id == eventId);
//     }

//     internal async Task<bool> CreateAttendance(Event newAttendee)
//     {
//         throw new NotImplementedException();
//     }
// }