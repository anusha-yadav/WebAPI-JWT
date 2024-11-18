using LibraryManagement.Data;
using LibraryManagement.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LibraryManagement.Controllers
{
    /// <summary>
    /// HomeController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        public IConfiguration Configuration;
        private readonly EmployeeDBContext Context;

        public HomeController(IConfiguration config, EmployeeDBContext context)
        {
            Configuration = config;
            Context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(User userData)
        {
            if (userData != null && userData.Email != null && userData.Password != null)
            {
                var user = await GetUser(userData.Email, userData.Password);

                if (user != null)
                {
                    //create claims details based on the user information
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, Configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("UserId", user.UserId.ToString()),
                        new Claim("DisplayName", user.DisplayName),
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim("Email", user.Email),
                        new Claim(ClaimTypes.Role, user.Role),
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        Configuration["Jwt:Issuer"],
                        Configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signIn);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// GetUser
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private async Task<User> GetUser(string email, string password)
        {
            return await Context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }
    }
}
