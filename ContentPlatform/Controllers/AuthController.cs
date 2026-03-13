using ContentPlatform.Dto_s;
using ContentPlatform.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContentPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [HttpPost("/register")]
        public async Task<IActionResult> RegisterAsync(RegisterDto registerDto)
        {
            var result = await authService.RegisterAsync(registerDto);
            if(result == false)
            {
                return BadRequest(new { message = "You are registered already" });
            }
            return Ok(result);
        }
        [HttpPost("/login")]
        public async Task<IActionResult> LoginAsync(LoginDto loginDto)
        {
            var result = await authService.LoginAsync(loginDto);
            if (result == null)
            {
                return BadRequest(new { message = "Wrong email or password" });
            }
            return Ok(result);
        }
        [HttpDelete("/logout")]
        public async Task<IActionResult> LogoutAsync(string token)
        {
            var result = await authService.LogoutAsync(token);
            if(result == false)
            {
                return BadRequest(new { message = "Invalid refresh token" });
            }
            return Ok(new { message = "Logout successfull" });
        }
        [HttpGet("/default-test")]
        [Authorize]
        public async Task<string> DefaultTest()
        {
            return "Authorize";
        }
        [HttpGet("/admin-test")]
        [Authorize(Roles = "Admin")]
        public async Task<string> AdminTest()
        {
            return "Admin-Authorize";
        }
        [HttpGet("/deploy-test")]
        public async Task<string> TTTEEESSSTTT()
        {
            return "It works!";
        }
    }
}
