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
    
    [HttpPost()]
        public async Task<IActionResult> CreateAttendeeAsync([FromBody] AttendanceRequest request)
        {
            // TODO: Eerst user opzoeken en checken als die bestaat
            bool UserAlreadyAttended = _attendanceService.UserExists(request.UserId);
            // zo niet return user not found
            if (UserAlreadyAttended)
            {
                return BadRequest(new { message = "User already attended" });
            }
            // TODO: _serviceAttendance aanroepen en de UserId en Date verwerken en opslaan in DB
            //adding the new attendee to the database context
            _attendanceService.CreateAttendance(request);


            return Ok(new { message = "New Attendance created! 🚀", new_attendance = "de nieuwe attendee" });
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


//     [HttpPut("{id}")]
//     public Task<AttendanceRequest> UpdateEvent(Guid id, [FromBody] AttendanceRequest updatedAttendee)
//     {
//         var eventToUpdate = _eventService.UpdateEvent(id, updatedEvent);
//         if (eventToUpdate == null)
//         {
//             return NotFound();
//         }
//         return Ok(eventToUpdate);
//     }
// }
//     }
// }

//         [HttpDelete("{id}")]
//         public IActionResult DeleteEvent(Guid id)
//         {
//             bool isDeleted = _eventService.DeleteEvent(id);
//             if (!isDeleted)
//             {
//                 return BadRequest("Event id not found!");
//             }
//             return Ok("Event succesfully deleted.");
//         }

//         [HttpGet("{id}")]
//         public ActionResult<Event> GetEventById(Guid id)
//         {
//             var eventItem = _eventService.GetEventById(id);
//             if (eventItem == null)
//             {
//                 return NotFound();
//             }
//             return Ok(eventItem.Id);
//         }
//     }
// }

public class AttendanceRequest
{
    public required DateTime Date { get; set; }
}