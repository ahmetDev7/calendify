using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace calendify.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventController : ControllerBase
    {
        private readonly EventService _eventService;

        public EventController()
        {
            _eventService = new EventService();
        }

        [HttpGet("all")]
        public IEnumerable<Event> GetAllEvents()
        {
            return _eventService.GetAllEvents();
        }

        [HttpPost()]
        public ActionResult<Event> CreateEvent([FromBody] Event newEvent)
        {
            var createdEvent = _eventService.CreateEvent(newEvent);
            return CreatedAtAction(nameof(GetEventById), new { id = createdEvent.Id }, createdEvent);
        }

        [HttpPut("{id}")]
        public ActionResult<Event> UpdateEvent(int id, [FromBody] Event updatedEvent)
        {
            var eventToUpdate = _eventService.UpdateEvent(id, updatedEvent);
            if (eventToUpdate == null)
            {
                return NotFound();
            }
            return Ok(eventToUpdate);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEvent(int id)
        {
            var isDeleted = _eventService.DeleteEvent(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet("{id}")]
        public ActionResult<Event> GetEventById(int id)
        {
            var eventItem = _eventService.GetEventById(id);
            if (eventItem == null)
            {
                return NotFound();
            }
            return Ok(eventItem.id);
        }
    }
}