using System;
using System.Collections.Generic;
using System.Linq;
using calendify.Data;
using calendify_app.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class AttendanceService
{
    private readonly AppDbContext _db;
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

    public Attendance ?UpdateAttendance(Guid id, Attendance updateAttendance)
    {
        return null;
    }

    public class AttendanceResult
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public Guid UserId { get; set; }
    }

}
