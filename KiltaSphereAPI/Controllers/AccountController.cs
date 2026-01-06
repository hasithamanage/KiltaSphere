using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using KiltaSphereAPI.Data;
using KiltaSphereAPI.DTOs;
using KiltaSphereAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KiltaSphereAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;

        public AccountController(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == request.Username);

            // Check user existence and verify hashed password
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return Unauthorized(new { message = "Virheellinen käyttäjätunnus tai salasana." });
            }

            var token = GenerateJwtToken(user);

            return Ok(new LoginResponseDTO
            {
                Token = token,
                User = new UserDTO
                {
                    Username = user.Username,
                    Role = user.Role.ToString(),
                    MemberId = user.MemberId
                }
            });
        }

        private string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // A claim is a key–value pair that represents identity data
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim("userId", user.UserId.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(8), // because professional ERPs usually have 8-12h sessions
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpPost("seed-admin")]
        public async Task<IActionResult> SeedAdmin()
        {
            // Check if any admin exists already to prevent duplicates
            if (await _context.Users.AnyAsync(u => u.Role == UserRole.Admin))
            {
                return BadRequest("Järjestelmänvalvoja on jo olemassa.");
            }

            var admin = new User
            {
                Username = "admin",
                // strictly no store plain passwords. BCrypt hashes it into a long string.
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                Role = UserRole.Admin
            };

            _context.Users.Add(admin);
            await _context.SaveChangesAsync();

            return Ok("Admin-käyttäjä luotu tunnuksella: admin / salasana: Admin123!");
        }
    }
}