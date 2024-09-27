using FlightProjectManagementApi.DataModels;
using FlightProjectManagementApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlightProjectManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        // Kayıt işlemi
        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            return _authService.Register(user);
        }

        // Giriş işlemi
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            return _authService.Login(model);
        }
    }
}
