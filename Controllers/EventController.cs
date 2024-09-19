using Microsoft.AspNetCore.Mvc;

namespace calendify.Controllers;

[ApiController]
[Route("[controller]")]
public class EventController : ControllerBase
{
    public EventController()
    {
        
    }

    [HttpGet()]
    public bool Get()
    {
        return false;
    }
}
