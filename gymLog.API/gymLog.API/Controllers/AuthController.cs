using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using gymLog.API.Model;
using gymLog.API.Model.DTO.AuthDto;
using gymLog.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace gymLog.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthController(IAuthService authService, IJwtTokenGenerator jwtTokenGenerator)
    {
        _authService = authService;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto login)
    {
        var loginResult = await _authService.Login(login);
        if (loginResult.IsSuccess)
        {
            var token = _jwtTokenGenerator.GenerateToken(loginResult.Data);
            return Ok(new
            {
                Token = token
            });
        }
        return BadRequest(loginResult.Message);

    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        var registerResult = await _authService.Register(registerDto);
        if (registerResult.IsSuccess)
        {
            var token = _jwtTokenGenerator.GenerateToken(registerResult.Data);
            return Ok(new
            {
                Token = token
            });
        }
        return BadRequest(registerResult.Message);
    }
}