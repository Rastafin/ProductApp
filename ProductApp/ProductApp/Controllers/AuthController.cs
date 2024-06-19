using Microsoft.AspNetCore.Mvc;
using ProductApp.Models;
using ProductApp.Services.Interfaces;
using ProductApp.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            _authService.Register(user);
            return Ok();
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var token = _authService.Login(model.Username, model.Password);

            if(token == null)
            {
                return Unauthorized();
            }

            return Ok(new { Token = token });
        }
    }
}
