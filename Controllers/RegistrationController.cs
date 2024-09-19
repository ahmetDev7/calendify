using Microsoft.AspNetCore.Mvc;

namespace calendify.Controllers;

[ApiController]
[Route("[controller]")]
public class RegistrationController : ControllerBase
{
    public RegistrationController()
    {
        
    }

    [HttpGet()]
    public bool Get()
    {
        return false;
    }
}
