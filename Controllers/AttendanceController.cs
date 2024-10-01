using System.Data.Common;
using calendify.Data;
using calendify_app.Models;
using Microsoft.AspNetCore.Mvc;
using static AttendanceService;

namespace calendify.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AttendanceController : ControllerBase
{
    private readonly AttendanceService _attendanceService;
    public AttendanceController(AppDbContext db)
    {
        _attendanceService = new AttendanceService(db);
    }

    [HttpGet("all")]
    public IActionResult GetAllAttendance()
    {
        return Ok(_attendanceService.GetAllAttendance());
    }

    [HttpGet("{id}")]
    public ActionResult<AttendanceResult> GetAttendanceByID(Guid id)
    {
        var attendanceItem = _attendanceService.GetAttendanceById(id);
        if (attendanceItem == null) 
            {
                return NotFound(new {message = "Attendancee not found."});
            }
            return Ok(attendanceItem);
    }

    [HttpGet("user-id/{userId}")]
    public ActionResult<List<AttendanceResult>> GetAttendanceByUserId(Guid userId)
    {
        var attendanceItem = _attendanceService.GetAttendancesByUserId(userId);
        if (attendanceItem == null) 
            {
                return NotFound(new {message = "Attendancee not found. Based on UserId"});
            }
            return Ok(attendanceItem);
    }

    [HttpDelete("{id}")]
        public IActionResult DeleteAttendance(Guid id)
        {
            bool isDeleted = _attendanceService.DeleteAttendance(id);
            if (!isDeleted)
            {
                return BadRequest("Attendance id not found!");
            }
            return Ok(new {message = "Attendance succesfully deleted."});
        }
    [HttpPut("{id}")]
    public ActionResult<Attendance> UpdateAttendance(Guid id, [FromBody] Attendance updateAttendance)
    {
        var attendanceUpdate = _attendanceService.UpdateAttendance(id,updateAttendance);
        if (updateAttendance == null){
            return NotFound();
        }
        return Ok(attendanceUpdate);

    }

}
