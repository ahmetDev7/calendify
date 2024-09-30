// Controllers/AuthController.cs
using calendify.Services;
using calendify_app.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace calendify.Controllers
{
    [Route("api/")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;

        public AuthController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var result = await _userService.Register(request.FirstName, request.LastName, request.Email, request.Password, request.RecurringDays, request.Role);
            if (result.StartsWith("User already exists"))
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var token = await _userService.Login(request.Email, request.Password);
            if (token.StartsWith("Invalid") || token.StartsWith("User"))
                return BadRequest(token);

            return Ok(new { Token = token });
        }

        [HttpGet("isloggedin")]
        [Authorize]
        public async Task<IActionResult> IsLoggedIn()
        {
            var email = User.Identity.Name;
            var isLoggedIn = await _userService.IsLoggedIn(email);
            return Ok(new { IsLoggedIn = isLoggedIn, Email = email });
        }

        [Authorize(Roles = "admin")]
        [HttpGet("admin-only")]
        public IActionResult AdminOnly()
        {
            return Ok("Only admins can use this endpoint.");
        }
    }

    public class RegisterRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RecurringDays { get; set; }
        public string Role { get; set; } = "user";
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
