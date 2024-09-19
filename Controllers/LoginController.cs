using Microsoft.AspNetCore.Mvc;

namespace calendify.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginController : ControllerBase
{
    public LoginController()
    {
        
    }

    [HttpGet()]
    public bool Get()
    {
        return false;
    }
}
