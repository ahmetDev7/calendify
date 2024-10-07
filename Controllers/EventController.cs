using calendify.Data;
using calendify.Services;
using calendify_app.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace calendify.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly EventService _eventService;
        private readonly UserService _userService;

        public EventController(AppDbContext db)
        {
            _eventService = new EventService(db);
            _userService = new UserService(db);
        }

        [HttpGet("all")]
        public IActionResult GetAllEvents()
        {
            return Ok(_eventService.GetAllEvents());
        }

        [HttpPost()]
        public IActionResult CreateEvent([FromBody] Event newEvent)
        {
            Event? eventCreated = _eventService.CreateEvent(newEvent);

            if (eventCreated == null) return BadRequest(new { message = "Event could not be created." });

            return Ok(new { message = "Event created!", created_event = eventCreated });
        }

        [HttpPut("{id}")]
        public ActionResult<Event> UpdateEvent(Guid id, [FromBody] UpdateEventDto request)
        {
            Event? eventToUpdate = _eventService.UpdateEvent(id, request);

            if (eventToUpdate == null) return NotFound(new { message = "Event id not found." });


            return Ok(new { message = "Event updated!", updated_event = eventToUpdate });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEvent(Guid id)
        {
            bool isDeleted = _eventService.DeleteEvent(id);
            if (!isDeleted)
            {
                return NotFound(new { message = "Event not found." });
            }
            return Ok(new { message = "Event deleted." });
        }


        [HttpGet("{id}")]
        public ActionResult GetEventById(Guid id)
        {
            var eventItem = _eventService.GetEventWithAttendees(id);

            if (eventItem == null)
            {
                return NotFound(new { message = "Event not found." });
            }

            return Ok(eventItem);
        }

        [HttpPost("attend-event")]
        public ActionResult AttendEvent([FromBody] AttendEventDto request)
        {
            if (!_userService.UserExists(request.UserId)) return NotFound(new { message = "User not found" });
            if (!_eventService.EventExists(request.EventId)) return NotFound(new { message = "Event not found" });

            if(_eventService.UserAttendanceExists(request.EventId, request.UserId)) return BadRequest(new { message = "You already attended this event." });
            

            int attendanceWindow = _eventService.EventAttendanceWindowIsOpen(request.EventId);

            if(attendanceWindow == -1){
                return BadRequest(new {message = "The event is not yet started, You can not yet attend this event."});
            }

            if(attendanceWindow == 1){
                return BadRequest(new {message = "The event is already finished, You can no longer attend this event."});
            }


            EventAttendance? attendanceCreated = _eventService.AttendEvent(request);
            if (attendanceCreated == null) return BadRequest(new { message = "Someting went wrong while storing the event attendance." });


            return Ok(new
            {
                message = "Attended event!",
                attended_event = new
                {
                    event_id = attendanceCreated.EventId,
                    user_id = attendanceCreated.UserId
                }
            });
        }


        // add rating or feedback
        [HttpPut("update/attended-event")]
        public ActionResult UpdateAttendEvent([FromBody] UpdateAttendedEvent request)
        {
            if (!_userService.UserExists(request.UserId)) return NotFound(new { message = "User not found" });
            if (!_eventService.EventExists(request.EventId)) return NotFound(new { message = "Event not found" });

            if(!_eventService.UserAttendanceExists(request.EventId, request.UserId)) return BadRequest(new { message = "You first must sign in for this event." });


            EventAttendance? eventAttendanceUpdated = _eventService.UpdateEventAttendanceByUser(request);
            if (eventAttendanceUpdated == null) return BadRequest(new { message = "Someting went wrong while updating the selected event attendance" });

            
            return Ok(new
            {
                message = "Updated attendance event!",
                attended_event = new
                {
                    event_id = eventAttendanceUpdated.EventId,
                    user_id = eventAttendanceUpdated.UserId,
                    rating = eventAttendanceUpdated.Rating,
                    feedback = eventAttendanceUpdated.Feedback,
                }
            });
        }


        // ❌ Inside the same controller there should be a protected GET endpoint that allows a logged-in user to view the list of attendees.
        // ❌ The controller also needs to be able to delete events that the user attended, in case the user is not able to attend the event anymore. 

        // ALL DTO'S
        public class AttendEventDto
        {

            public Guid EventId { get; set; }
            public Guid UserId { get; set; }
        }


        public class UpdateAttendedEvent : AttendEventDto
        {
            public int? Rating { get; set; }
            public string? Feedback { get; set; }
        }

        public class UpdateEventDto
        {
            public string? Title { get; set; }
            public string? Description { get; set; }
            public DateTime? Date { get; set; }
            public DateTime? StartTime { get; set; }
            public DateTime? EndTime { get; set; }
            public string? Location { get; set; }
            public bool? AdminApproval { get; set; }
        }
    }
}