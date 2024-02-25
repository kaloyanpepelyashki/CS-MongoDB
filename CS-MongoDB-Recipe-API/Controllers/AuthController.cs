using CS_MongoDB_Recipe_API.Services;
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
    /**This is the Authentication service controller which is only in charge of authenticating users
     * The authentication happens through the UserService service.
     */
    public class AuthController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly UserService _userService;
        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
            _userService = UserService.GetInstance();
        }

        [HttpPost("SignIn")]

        /** This is the SignIn authentication method part of the AuthController. 
         * The method "signs in" the user and calls the GeneratJWTToken method if the authentication is successful.
         * The method checks the validity of sign in credentials by using the AuthSignIn method part of the UserService class. 
         * If the AuthSignIn method returns a User object and not a null, it means a user with those credentials was found in the database, therefore the
         * sign in is successful.
         */
        public async Task<IActionResult> SignIn([FromBody] AuthDTO authDto)
        {
            try
            {
                var _authSignInResult = await _userService.AuthSignIn(authDto.Email, authDto.Password);

                if (_authSignInResult != null)
                {
                    var jwt = GenerateJWTToken(authDto);
                    if (jwt != null)
                    {
                        return Ok(new { access_token = jwt });
                    }

                    throw new Exception("Error generating JWT");
                }

                return Unauthorized();
            } catch (Exception e)
            {
                Console.WriteLine($"Error signing in: {e}");
                throw new Exception($"Error signing in user, error: {e}");
            }
        }
        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] AuthDTO authDto)
        {
            try
            {
                var _authSignUpResult = await _userService.AuthSignUp(authDto.Email, authDto.Password);

                if(_authSignUpResult)
                {
                    return Ok("Account created successfully");
                }

                return Conflict("Email already exists.");
            }
            catch(Exception e)
            {
                Console.WriteLine($"Error signing in: {e}");
                throw new Exception($"Error signing up user, error: {e}");
            }
        }

        /** This method generates a JWToken
         * This method is ONLY accesible whithin this controller
         */
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
