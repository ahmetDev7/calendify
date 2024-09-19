using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    private readonly ILoginService _loginService;

    public LoginController(ILoginService loginService)
    {
        _loginService = loginService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        bool isAuthenticated = _loginService.AuthenticateUser(request.Username, request.Password);
        if (isAuthenticated)
        {
            return Ok(new { Message = "Login successful" });
        }
        return Unauthorized(new { Message = "Invalid username or password" });
    }

    [HttpGet("session/{sessionId}")]
    public IActionResult GetSessionInfo(string sessionId)
    {
        string adminName = _loginService.GetAdminName(sessionId);
        if (adminName != null)
        {
            return Ok(new { IsSessionValid = true, AdminName = adminName });
        }
        return NotFound(new { IsSessionValid = false, Message = "Session not found" });
    }
}

public class LoginRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}

public interface ILoginService
{
    bool AuthenticateUser(string username, string password);
    string GetAdminName(string sessionId);
}