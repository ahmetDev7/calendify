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
        public async Task<IActionResult> CreateEvent([FromBody] Event newEvent)
        {
            bool eventCreated = await _eventService.CreateEvent(newEvent);
            
            if(!eventCreated) return BadRequest("Event could not be created.");

            return Ok("Event created!");
        }

        [HttpPut("{id}")]
        public ActionResult<Event> UpdateEvent(Guid id, [FromBody] Event updatedEvent)
        {
            var eventToUpdate = _eventService.UpdateEvent(id, updatedEvent);
            if (eventToUpdate == null)
            {
                return NotFound();
            }
            return Ok(eventToUpdate);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEvent(Guid id)
        {
            var isDeleted = _eventService.DeleteEvent(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet("{id}")]
        public ActionResult<Event> GetEventById(Guid id)
        {
            var eventItem = _eventService.GetEventById(id);
            if (eventItem == null)
            {
                return NotFound();
            }
            return Ok(eventItem.Id);
        }
    }
}