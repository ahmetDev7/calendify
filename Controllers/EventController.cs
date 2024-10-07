using System.Security.Claims;
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

        [Authorize(Roles = "admin")]
        [HttpPost()]
        public IActionResult CreateEvent([FromBody] Event newEvent)
        {
            Event? eventCreated = _eventService.CreateEvent(newEvent);

            if (eventCreated == null) return BadRequest(new { message = "Event could not be created." });

            return Ok(new { message = "Event created!", created_event = eventCreated });
        }

        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public ActionResult<Event> UpdateEvent(Guid id, [FromBody] UpdateEventDto request)
        {
            Event? eventToUpdate = _eventService.UpdateEvent(id, request);

            if (eventToUpdate == null) return NotFound(new { message = "Event id not found." });


            return Ok(new { message = "Event updated!", updated_event = eventToUpdate });
        }


        [Authorize(Roles = "admin")]
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

        [Authorize]
        [HttpPost("attend-event")]
        public ActionResult AttendEvent([FromBody] AttendEventDto request)
        {
            string? claimNameIdentifier = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            User? authenticatedUser = _userService.GetUserByClaimNameIdentifier(claimNameIdentifier);
            
            if (authenticatedUser == null) return NotFound(new { message = "User not found" });
            if (!_eventService.EventExists(request.EventId)) return NotFound(new { message = "Event not found" });

            if (_eventService.UserAttendanceExists(request.EventId, authenticatedUser.Id)) return BadRequest(new { message = "You already attended this event." });


            int attendanceWindow = _eventService.EventAttendanceWindowIsOpen(request.EventId);

            if (attendanceWindow == -1)
            {
                return BadRequest(new { message = "The event is not yet started, You can not yet attend this event." });
            }

            if (attendanceWindow == 1)
            {
                return BadRequest(new { message = "The event is already finished, You can no longer attend this event." });
            }

            EventAttendance? attendanceCreated = _eventService.AttendEvent(request, authenticatedUser.Id);
            if (attendanceCreated == null) return BadRequest(new { message = "Someting went wrong while storing the event attendance." });


            Event? foundEvent = _eventService.GetEventById(request.EventId);


            return Ok(new
            {
                message = "Attended event!",
                attended_event = new
                {
                    event_id = attendanceCreated.EventId,
                    title = foundEvent.Title,
                    description = foundEvent.Description,
                    date = foundEvent.Date,
                    startTime = foundEvent.StartTime,
                    endTime = foundEvent.EndTime,
                    location = foundEvent.Location,
                    user_id = attendanceCreated.UserId
                }
            });
        }


        // add rating or feedback
        [Authorize]
        [HttpPut("update/attended-event")]
        public ActionResult UpdateAttendEvent([FromBody] UpdateAttendedEvent request)
        {
            string? claimNameIdentifier = User.FindFirstValue(ClaimTypes.NameIdentifier);
            User? authenticatedUser = _userService.GetUserByClaimNameIdentifier(claimNameIdentifier);
            if (authenticatedUser == null) return NotFound(new { message = "User not found" });

            if (!_userService.UserExists(authenticatedUser.Id)) return NotFound(new { message = "User not found" });
            if (!_eventService.EventExists(request.EventId)) return NotFound(new { message = "Event not found" });

            if (!_eventService.UserAttendanceExists(request.EventId, authenticatedUser.Id)) return BadRequest(new { message = "You first must sign in for this event." });


            EventAttendance? eventAttendanceUpdated = _eventService.UpdateEventAttendanceByUser(request, authenticatedUser.Id);
            if (eventAttendanceUpdated == null) return BadRequest(new { message = "Someting went wrong while updating the selected event attendance" });

            Event foundEvent = _eventService.GetEventById(request.EventId);

            return Ok(new
            {
                message = "Updated attendance event!",
                attended_event = new
                {
                    event_id = eventAttendanceUpdated.EventId,
                    title = foundEvent.Title,
                    description = foundEvent.Description,
                    date = foundEvent.Date,
                    startTime = foundEvent.StartTime,
                    endTime = foundEvent.EndTime,
                    location = foundEvent.Location,
                    user_id = eventAttendanceUpdated.UserId,
                    rating = eventAttendanceUpdated.Rating,
                    feedback = eventAttendanceUpdated.Feedback,
                }
            });
        }


        // ❌ Inside the same controller there should be a protected GET endpoint that allows a logged-in user to view the list of attendees.

        [Authorize]
        [HttpDelete("leave/{eventId}")]
        public async Task<IActionResult> leaveEvent(Guid eventId)
        {
            User? authenticatedUser = _userService.GetUserByClaimNameIdentifier(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (authenticatedUser == null) return NotFound(new {message ="User not found."});

            Event? foundEvent = _eventService.GetEventById(eventId);
            if (foundEvent == null) return NotFound(new {message ="Event not found"});

            bool attendedEvent = _eventService.UserAttendanceExists(eventId: eventId, userId: authenticatedUser.Id);
            if (!attendedEvent) return NotFound(new {message ="You did not attend this event."});

            bool leftEvent = await _eventService.LeaveAttendedEvent(eventId, authenticatedUser.Id);

            if (!leftEvent) return UnprocessableEntity(new {message = "Something went wrong while leaving event"});


            return Ok(new { message = "Event successfully exited.", left_event =  foundEvent});
        }

        // ALL DTO'S
        public class AttendEventDto
        {
            public Guid EventId { get; set; }
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