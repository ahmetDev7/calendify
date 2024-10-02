using calendify.Data;
using calendify_app.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace calendify.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttendanceController : ControllerBase
    {

        private readonly Attendance _serviceAttendance;


        public AttendanceController(AppDbContext db)
        {
            _serviceAttendance = new Attendance(db);
        }

        [HttpPost()]
        public async Task<IActionResult> CreateAttendeeAsync([FromBody] AttendanceRequest request)
        {
            // TODO: Eerst user opzoeken en checken als die bestaat
            bool userExists =  _serviceAttendance.UserExists(request.UserId);
            // zo niet return user not found
            if (!userExists)
            {
                return NotFound(new { message = "User not found 🥺" });
            }
            // TODO: _serviceAttendance aanroepen en de UserId en Date verwerken en opslaan in DB
            //adding the new attendee to the database context
             _serviceAttendance.CreateAttendance(request);
            

            return Ok(new { message = "New Attendance created! 🚀", new_attendance = "de nieuwe attendee" });
        }
    }
}

//         [HttpPut("{id}")]
//         public ActionResult<> UpdateEvent(Guid id, [FromBody] Event updatedEvent)
//         {
//             var eventToUpdate = _eventService.UpdateEvent(id, updatedEvent);
//             if (eventToUpdate == null)
//             {
//                 return NotFound();
//             }
//             return Ok(eventToUpdate);
//         }

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
    public required Guid UserId { get; set; }
    public required DateTime Date { get; set; }
}