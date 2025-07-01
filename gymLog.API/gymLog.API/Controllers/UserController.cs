using gymLog.API.Model.DTO.AuthDto;
using gymLog.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace gymLog.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<AuthDto>> GetCurrentUser()
    {
        var result = await _userService.GetCurrentUser(Guid.Parse(User.FindFirstValue(JwtRegisteredClaimNames.Sub)!));
        if (result.IsSuccess)
        {
            return Ok(result.Data);
        }
        return BadRequest(result.Message);
    }
}