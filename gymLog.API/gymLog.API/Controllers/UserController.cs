using gymLog.API.Model.DTO.AuthDto;
using gymLog.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace gymLog.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IClaimsService _claimsService;

    public UserController(IUserService userService, IClaimsService claimsService)
    {
        _userService = userService;
        _claimsService = claimsService;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<AuthDto>> GetCurrentUser()
    {
        var result = await _userService.GetCurrentUser(_claimsService.GetUserIdFromClaims(User).Data);
        if (result.IsSuccess)
        {
            return Ok(result.Data);
        }
        return BadRequest(result.Message);
    }
}