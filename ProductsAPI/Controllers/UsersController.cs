using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProductsAPI.DTO;
using ProductsAPI.Models;

namespace ProductsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _configuration;

        public UsersController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }
        
        [HttpPost("register")]
        public async Task<IActionResult> CreateUser(UserDTO dtoModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = new AppUser
            {
                UserName = dtoModel.UserName,
                FullName = dtoModel.FullName,
                Email = dtoModel.Email,
                DateAdded = DateTime.Now
            };
            var result = await _userManager.CreateAsync(user, dtoModel.Password);
            
            if (result.Succeeded)
            {
                return StatusCode(201);
                
            }
            
            return BadRequest(result.Errors);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return BadRequest(new {message = "User not found"}); 
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (result.Succeeded)
            {
                return Ok(new {token = GenerateJWT(user)});
            }
            return Unauthorized();
        }

        private object GenerateJWT(AppUser user)
        {
            // Create a token handler to manage the creation of JSON Web Tokens (JWTs).
            var tokenHandler = new JwtSecurityTokenHandler();

            // Retrieve the "Secret" key from AppSettings and convert it to a byte array.
            // This secret key is used to sign the JWT using HMAC algorithm.
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Secret").Value ?? "");

            // Configure the token with necessary details.
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                // Include claims (identity information) into the token.
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),   // User's unique identifier (ID)
                    new Claim(ClaimTypes.Name, user.UserName ?? "")            // User's username
                }),

                // Set the token's expiration time (1 day from now).
                Expires = DateTime.UtcNow.AddDays(1),

                // Define the signing credentials for the token (HMAC SHA256 + secret key).
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            // Generate the JWT based on the above descriptor.
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Return the token as a string.
            return tokenHandler.WriteToken(token);
        }

    }
}
