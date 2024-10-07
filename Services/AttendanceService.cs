using System;
using System.Collections.Generic;
using System.Linq;
using calendify.Data;
using calendify_app.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using calendify.Controllers;
public class AttendanceService
{
    private readonly AppDbContext _db;
    public AttendanceService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Attendance?> UpdateAttendance(AttendanceRequest updateRequest, Guid userId)
    {
        // Controleer of de Attendance-record bestaat
        var existingAttendance = await _db.Attendance
        .FirstOrDefaultAsync(a => a.UserId == userId);

        if (existingAttendance == null)
        {
            throw new InvalidOperationException("Attendance record not found for the given user.");
        }

        // Werk de Attendance-record bij met de nieuwe gegevens
        existingAttendance.Date = DateTime.SpecifyKind(updateRequest.Date, DateTimeKind.Utc);
        // Werk andere velden bij indien nodig
        // existingAttendance.OtherField = updateRequest.OtherField;

        // Sla de wijzigingen op in de database
        await _db.SaveChangesAsync();

        return existingAttendance;
    }


    public async Task<bool> CreateAttendance(AttendanceRequest request, Guid userId)
    {
        // Check if the user has already attended on the given date
        var alreadyAttended = await _db.Attendance.AnyAsync(a => a.UserId == userId && a.Date == request.Date);

        if (alreadyAttended)
        {
            throw new InvalidOperationException("User has already attended on this date.");
        }

        // Ensure the Date property is in UTC
        request.Date = DateTime.SpecifyKind(request.Date, DateTimeKind.Utc);

        // Create a new Attendance entity
        var newAttendee = new Attendance
        {
            UserId = userId,
            Date = request.Date,
            // Map other properties from request to newAttendee as needed
        };

        // Add the new attendee to the database context
        _db.Attendance.Add(newAttendee);

        // Save changes to the database
        await _db.SaveChangesAsync();

        return true;
    }

    public List<AttendanceResult> GetAllAttendance()
    {
        return _db.Attendance
                  .Select(a => new AttendanceResult
                  {
                      Id = a.Id,
                      Date = a.Date,
                      UserId = a.UserId
                  })
                  .ToList();
    }

    // get single attendance by attendance_id
    public AttendanceResult? GetAttendanceById(Guid id)
    {
        return _db.Attendance.Select(a => new AttendanceResult
        {
            Id = a.Id,
            Date = a.Date,
            UserId = a.UserId
        }).FirstOrDefault(e => e.Id == id);
    }

    // get all attendances based on user id
    // SELECT id, date, userId FROM attendance WHERE UserId = "uuid-388735"
    // Get all attendances based on user id (returning full Attendance objects)
    public List<AttendanceResult> GetAttendancesByUserId(Guid userId)
    {
        return _db.Attendance.Select(a => new AttendanceResult
        {
            Id = a.Id,
            Date = a.Date,
            UserId = a.UserId
        }).Where(e => e.UserId == userId).ToList();
    }



    public bool DeleteAttendance(Guid attendanceId)
    {
        var attendanceToDelete = _db.Attendance.FirstOrDefault(e => e.Id == attendanceId);
        if (attendanceToDelete == null)
        {
            return false;
        }

        _db.Attendance.Remove(attendanceToDelete);

        _db.SaveChanges();

        return true;
    }

    public class AttendanceResult
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public Guid UserId { get; set; }
    }

}
