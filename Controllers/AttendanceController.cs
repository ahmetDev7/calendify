using System.Data.Common;
using calendify.Data;
using calendify_app.Models;
using Microsoft.AspNetCore.Mvc;
using static AttendanceService;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace calendify.Controllers
{
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
                return NotFound(new { message = "Attendancee not found." });
            }
            return Ok(attendanceItem);
        }

        [HttpGet("user-id/{userId}")]
        public ActionResult<List<AttendanceResult>> GetAttendanceByUserId(Guid userId)
        {
            var attendanceItem = _attendanceService.GetAttendancesByUserId(userId);
            if (attendanceItem == null)
            {
                return NotFound(new { message = "Attendancee not found. Based on UserId" });
            }
            return Ok(attendanceItem);
        }

        [Authorize]
        [HttpPost()]
        public async Task<IActionResult> CreateAttendeeAsync([FromBody] AttendanceRequest request)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized(new { message = "Invalid token: UserId not found" });
            }
            if (!Guid.TryParse(userIdClaim.Value, out var userId))
            {
                return BadRequest(new { message = "Invalid UserId format" });
            }
            try
            {
                var result = await _attendanceService.CreateAttendance(request, userId);
                return Ok(new { message = "New Attendance created! 🚀", new_attendance = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the attendance", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAttendance(Guid id)
        {
            bool isDeleted = _attendanceService.DeleteAttendance(id);
            if (!isDeleted)
            {
                return BadRequest("Attendance id not found!");
            }
            return Ok(new { message = "Attendance succesfully deleted." });
        }
        [HttpPut("{id}")]
        public ActionResult<Attendance> UpdateAttendance(Guid id, [FromBody] Attendance updateAttendance)
        {
            var attendanceUpdate = _attendanceService.UpdateAttendance(id, updateAttendance);
            if (updateAttendance == null)
            {
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
}