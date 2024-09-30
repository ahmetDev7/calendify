using calendify.Data;
using calendify_app.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace calendify.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly EventService _eventService;

        public EventController(AppDbContext db)
        {
            _eventService = new EventService(db);
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
            var isDeleted = _eventService.DeleteEvent(id);
            if (!isDeleted)
            {
                return NotFound(new { message = "Event not found." });
            }
            return Ok(new { message = "Event deleted." });
        }

        [HttpGet("{id}")]
        public ActionResult<Event> GetEventById(Guid id)
        {
            var eventItem = _eventService.GetEventById(id);
            if (eventItem == null)
            {
                return NotFound(new { message = "Event not found." });
            }

            return Ok(eventItem);
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