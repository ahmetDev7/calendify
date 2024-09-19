using Microsoft.AspNetCore.Mvc;

namespace calendify.Controllers;

[ApiController]
[Route("[controller]")]
public class AttendanceController : ControllerBase
{
    public AttendanceController()
    {
        
    }

    [HttpGet()]
    public bool Get()
    {
        return false;
    }
}
