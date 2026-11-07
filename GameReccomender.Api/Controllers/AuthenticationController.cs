using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace GameReccomender.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        [HttpGet("login")]
        public IActionResult Login()
        {
            return Challenge(new AuthenticationProperties { RedirectUri = "/api/Authentication/callback" }, "Steam");
        }

        [HttpGet("callback")]
        public async Task<IActionResult> Callback()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            
            if (!result.Succeeded)
            {
                return BadRequest("Steam authentication failed.");
            }
            var steamClaim = result.Principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var steamId = steamClaim?.Split('/').Last();
            var displayName = result.Principal.Identity?.Name;

            return Ok(new 
            { 
                Message = "Authentication successful", 
                SteamId = steamId,
                DisplayName = displayName
            });
        }


    }
}
