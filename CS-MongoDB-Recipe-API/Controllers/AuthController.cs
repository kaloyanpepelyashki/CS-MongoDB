using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CS_MongoDB_Recipe_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IConfiguration _configuration;
        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost]
        public IActionResult SignIn([FromBody] AuthDTO authDto)
        {
            if(authDto.Password == "correctPass" && authDto.Email == "correctEmail")
            {
                var jwt = GenerateJWTToken(authDto);
                if (jwt != null) 
                {
                    return Ok(new { access_token = jwt }); 
                }

                throw new Exception($"Error generating JWT");
            }

            return Unauthorized();
        }

        private string GenerateJWTToken(AuthDTO authDto)
        {
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
            new Claim(JwtRegisteredClaimNames.Sub, authDto.Email),
            new Claim("myCustomClaim", "myCustomValue"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

                var token = new JwtSecurityToken(
                    issuer: _configuration["JwtSettings:Issuer"],
                    audience: _configuration["JwtSettings:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["JwtSettings:ExpiryMinutes"])),
                    signingCredentials: credentials
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            } catch(Exception e)
            {
                Console.WriteLine($"Error generating token {e}");
                return null;
            }
        }

        public class AuthDTO
        {
            public string Email { get; set;}
            public string Password { get; set;}
        }
    }
}
