using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        public UsersController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
        [HttpGet("login")]
        public async Task<IActionResult> Login(LoginDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return BadRequest(); 
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (result.Succeeded)
            {
                return Ok();
            }
            return Ok();
        }
    }
}
