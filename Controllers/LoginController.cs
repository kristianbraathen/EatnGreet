using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserRegwebapi.Data;
using webapi.Models;
using webapi.Models.LoginDto;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly UserRegDbContext _context;
        

        public LoginController(UserRegDbContext context)
        {
            _context = context;
            
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await _context.UserRegistrations
                .FirstOrDefaultAsync(u => u.Email == loginDto.Email);

            if (user == null || !VerifyPassword(loginDto.Password, user.Password))
            {
                return Unauthorized("Invalid email or password");
            }
            var userInfo = new
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone,
                Address= user.Address,
                City = user.City,
                Password = user.Password,
               
            };


            // Login was successful
            // Generate a JWT
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("your_secret_key_here"); // Replace with your secret key
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                     new Claim(ClaimTypes.Name, user.Id.ToString())
                    // Add other claims as needed
                }),
                Expires = DateTime.UtcNow.AddDays(1), // Set token expiration as needed
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // Return the JWT
            return Ok(new { Token = tokenString, User = userInfo });
        }
    

        private static bool VerifyPassword(string enteredPassword, string storedPassword)
        {
           
            return BCrypt.Net.BCrypt.Verify(enteredPassword, storedPassword);
        }
    }
}


   