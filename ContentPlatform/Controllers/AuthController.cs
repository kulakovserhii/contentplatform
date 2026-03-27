using ContentPlatform.Dto_s;
using ContentPlatform.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
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
            if(result != "Account created")
            {
                return BadRequest(new { message = result });
            }
            return Ok(new { message = result});
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
        [HttpGet("google-login")]
        public IActionResult GoogleLogin()
        {
            var redirectUrl = Url.Action(nameof(GoogleLoginCallback));
            var properties = new AuthenticationProperties
            {
                RedirectUri = redirectUrl,
            };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }
        [HttpGet("google-login-callback")]
        public async Task<IActionResult> GoogleLoginCallback()
        {
            var authenticateResult = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
            if (!authenticateResult.Succeeded)
                return BadRequest(new { message = "Google authenrication failed" });
            var email = authenticateResult.Principal?.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;
            var name = authenticateResult.Principal?.FindFirst(System.Security.Claims.ClaimTypes.Name)?.Value;
            var lastname = authenticateResult.Principal?.FindFirst(System.Security.Claims.ClaimTypes.Surname)?.Value;
            var user = await authService.GetOrCreateUserAsync(email, name, lastname);
            if(user == null)
                return StatusCode(500, new { message = "Failed to create or find user"});
            var tokens = await authService.GetTokensAsync(user);
            Response.Headers.Append("Access-Control-Expose-Headers", "Authorization, Refresh-Token");
            Response.Headers.Append("Authorization", $"Bearer {tokens.JwtToken}");
            Response.Headers.Append("Refresh-Token", tokens.RefreshToken);
            return Ok(new
            {
                message = "Google login successfull",
                jwtToken = tokens.JwtToken,
                refreshToken = tokens.RefreshToken,
                user = new
                {
                    email = user.Email,
                    firstName = user.FirstName,
                    lastName = user.LastName,
                }
            });
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
        public async Task<IActionResult> DefaultTest()
        {
            return Ok(new { message = "Authorize" });
        }
        [HttpGet("/admin-test")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdminTest()
        {
            return Ok(new { message = "Admin-Authorize" });
        }
    }
}
