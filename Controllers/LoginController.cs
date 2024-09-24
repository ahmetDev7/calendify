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

    [HttpPost("auth")]
    public IActionResult IsLoggedIn([FromBody] IsLoggedInRequest request)
    {
        if(!_loginService.IsLoggedIn(request)) return BadRequest("Session ID not found");


        return Ok($"User logged in");
    }


    [HttpPost("logout")]
    public IActionResult Logout([FromBody] LogoutRequest request)
    {
        if(_loginService.LogoutUser(request)) return BadRequest("Could not log out.");


        return Ok($"Logged out.");

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

public class IsLoggedInRequest
{
    public required string SessionId { get; set; }
}