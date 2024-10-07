// Services/UserService.cs
using calendify.Data;
using calendify_app.Models;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace calendify.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string> Register(string firstName, string lastName, string email, string password, int recurringDays, string role = "user")
        {
            var existingUser = await _context.User.FirstOrDefaultAsync(u => u.Email == email);
            if (existingUser != null)
                return "User already exists.";

            var newUser = new User(
                Guid.NewGuid(),
                firstName,
                lastName,
                email,
                password,
                recurringDays,
                role
            );

            _context.User.Add(newUser);
            await _context.SaveChangesAsync();

            return "User registered successfully.";
        }

        public async Task<string> Login(string email, string password)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
                return "User not found.";

            if (user.Password != password)
                return "Invalid password.";

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes("SuperSecretKeyThatIs32BytesLongX");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.Role, user.Role),
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<bool> IsLoggedIn(string email)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.Email == email);
            return user != null;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.User.FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
