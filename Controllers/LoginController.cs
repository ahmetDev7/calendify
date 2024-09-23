using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/login")]
public class LoginController : ControllerBase
{
    private readonly LoginService _loginService;

    public LoginController(LoginService loginService)
    {
        _loginService = loginService;
    }

    [HttpPost()]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        bool loginExists = _loginService.AuthenticateUser(request);

        if(!loginExists) return Unauthorized("username or password incorrect or not found.");

        return Ok($"{request.Username} logged in!");
    }

        [HttpPost("/logout")]
    public IActionResult Logout([FromBody] LogoutRequest request)
    {
        return Ok($"TODO LOGOUT");

    }
}

public class LoginRequest
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}

public class LogoutRequest
{
    public required string SessionId { get; set; }
}