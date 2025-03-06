using Microsoft.AspNetCore.Mvc;
using WebApplication1.BLL.DTOs.Account;
using WebApplication1.BLL.Services.Account;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private IAccountService accountService;

        public AuthController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDTO dto)
        {
            var result = await accountService.LoginAsync(dto);

            if (result == null)
                return BadRequest("Invalid login data");

            return Ok();
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterDTO dto)
        {
            var result = await accountService.RegisterAsync(dto);

            if (result == null)
                return BadRequest("User exists or creation failed.");

            return Ok($"User {result.Id} created");
        }
    }
}
