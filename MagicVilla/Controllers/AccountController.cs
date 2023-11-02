using MagicVilla.Models;
using MagicVilla.Models.DTOs;
using MagicVilla.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
}