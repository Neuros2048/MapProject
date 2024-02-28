using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Controllers.Helper;
using Server.Services;
using Shared.DTO;

namespace Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(AuthService authService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto login)
    {
        return (await authService.Login(login)).Match(Ok,this.ErrorResult);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto register)
    {
        return (await authService.Register(register)).Match(Ok,this.ErrorResult);
    }

    [HttpPut("change-password"), Authorize]
    public async Task<IActionResult> ChangePassword(ChangePasswordDto changePasswordDto)
    {
           
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return (await authService.ChangePassword(changePasswordDto, int.Parse(userId!))).Match(Ok,this.ErrorResult);
    }
}