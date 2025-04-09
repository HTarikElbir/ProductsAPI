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
        private UserManager<AppUser> _userManager;

        public UsersController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
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
    }
}
