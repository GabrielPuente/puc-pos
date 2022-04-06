using CBF.Api.AuthenticationService;
using CBF.Application.Commands;
using CBF.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CBF.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        public UserController(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate(LoginUserCommand command)
        {
            var response = await _userService.LoginUser(command);

            if (!response.Valid)
            {
                return BadRequest(response);
            }

            var token = TokenService.GenerateToken(response.Data.Name, response.Data.Role, response.Data.Email, _configuration);

            return Ok(new
            {
                user = response.Data,
                token
            });

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate(CreateUserCommand command)
        {
            var response = await _userService.CreateUser(command);

            if (!response.Valid)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpGet]
        [Route("player")]
        [AllowAnonymous]
        public string Employee() => "Player " + User.FindFirstValue(ClaimTypes.Email);

        [HttpGet]
        [Route("coach")]
        [Authorize(Roles = "Coach")]
        public string Manager() => "Coach " + User.FindFirstValue(ClaimTypes.Role);

        [HttpGet("Logar")]
        public async Task<IActionResult> Logar()
        {
            var response = await _userService.LoginUser(new LoginUserCommand { Email = "gapuente96@gmail.com", Password = "eng4682507895"});

            if (!response.Valid)
            {
                return BadRequest(response);
            }

            var token = TokenService.GenerateToken(response.Data.Name, response.Data.Role, response.Data.Email, _configuration);

            return Ok(new
            {
                user = response.Data,
                token
            });
        }
    }
}