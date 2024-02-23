
using Loyalty.System.API.Models;
using Loyalty.System.API.Models.Settings;
using Loyalty.System.API.Services;
using Loyalty.System.API.Services.Auth;
using Loyalty.System.API.Services.GoogleAuthService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace Loyalty.System.API.Controllers.Auth;

[AllowAnonymous]
[Route("api/[controller]")]
[ApiController]
public class GoogleAuthController : ControllerBase
{
    private readonly GoogleSettings _googleSettings;
    private readonly GoogleAuthService _googleAuthService;
    private readonly AuthService _authService;
    private readonly UserService _userService;

    public GoogleAuthController(GoogleSettings googleSettings, GoogleAuthService googleAuthService, AuthService authService)
    {
        _googleSettings = googleSettings;
        _googleAuthService = googleAuthService;
        _authService = authService;
    }

    [HttpPost]
    [Route("callback")]
    public async Task<IActionResult> GoogleAuthCallback([FromBody] string request)
    {
        JwtSecurityToken jwtToken = await _googleAuthService.GetGoogleToken(request);
        // Read claims from token
        User entity = await _googleAuthService.CreateUser(jwtToken);
        try
        {
            //create token
            var token = _authService.CreateToken(entity);

            var options = new CookieOptions()
            {
                HttpOnly = true,
                SameSite = SameSiteMode.Strict
            };

            // Set cookie with Token
            Response.Cookies.Append("jwtToken", JsonConvert.SerializeObject(token), new CookieOptions
            {
                Expires = DateTime.UtcNow.AddHours(2)
            });

            return Ok();
        }
        catch
        {
            return BadRequest("Token error");
        }

    }
}
