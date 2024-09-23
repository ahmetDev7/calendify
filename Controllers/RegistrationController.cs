using Microsoft.AspNetCore.Mvc;

namespace calendify.Controllers;

[ApiController]
[Route("api/register")]
public class RegistrationController : ControllerBase
{
    private readonly RegistrationService _registrationService;
    public RegistrationController(RegistrationService registrationService)
    {
        registrationService = _registrationService;
    }

    [HttpPost("/register")]
    public IActionResult Register([FromBody] RegisterRequest request)
    {
        bool registerExists = _registrationService.RegisterUser(request);

        if(!registerExists) return BadRequest("Username or password does not match the registration criteria.");

        return Ok($"{request.Username} has been registered!");
    }
}

public class RegisterRequest
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}