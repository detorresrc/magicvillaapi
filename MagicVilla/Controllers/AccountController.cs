using System.Security.Claims;
using MagicVilla.Models;
using MagicVilla.Models.DTOs;
using MagicVilla.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla.Controllers;

[Microsoft.AspNetCore.Components.Route("api/account")]
public class AccountController : BaseApiController
{
    private readonly UserManager<LocalUser> _userManager;
    private readonly SignInManager<LocalUser> _signInManager;
    private readonly TokenService _tokenService;

    public AccountController(
        UserManager<LocalUser> userManager,
        SignInManager<LocalUser> signInManager,
        TokenService tokenService
    )
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<UserDTO>> Login([FromBody]LoginDTO loginDto)
    {
        var user = _userManager.Users.SingleOrDefault(x => x.UserName == loginDto.Username);
        if (user==null) return BadRequest("Invalid username or password");
        
        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
        if (!result.Succeeded) return Unauthorized("Invalid username or password");
                    
        await SetRefreshToken(user);
        return CreateUserObject(user);
    }
    
    [Authorize]
    [HttpPost("refreshToken")]
    public async Task<ActionResult<UserDTO>> RefreshToken()
    {
        var refreshToken = Request.Cookies["refreshToken"];
        Console.WriteLine($"RefreshToken: {refreshToken}");

        var user = await _userManager.Users
            .Include(r => r.RefreshTokens)
            .FirstOrDefaultAsync(x => x.UserName == User.FindFirstValue(ClaimTypes.Name));

        if(user == null) return Unauthorized();

        var oldToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken);

        if (oldToken != null && !oldToken.IsActive)
        {
            oldToken.Revoked = DateTime.UtcNow;
            await _userManager.UpdateAsync(user);
            return Unauthorized();
        }

        return CreateUserObject(user);
    }
    
    private ActionResult<UserDTO> CreateUserObject(LocalUser user)
    {
        return new UserDTO
        {
            Token = _tokenService.CreateToken(user),
            Username = user.UserName,
            Email = user.Email
        };
    }
    
    private async Task SetRefreshToken(LocalUser appUser)
    {
        var refreshToken = _tokenService.GenerateRefreshToken();
        appUser.RefreshTokens.Add(refreshToken);
        await _userManager.UpdateAsync(appUser);

        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTime.UtcNow.AddDays(1)
        };
        
        Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);
    }
}