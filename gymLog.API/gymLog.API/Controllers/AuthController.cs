using gymLog.API.Model.DTO.AuthDto;
using gymLog.API.Model.DTO.TokenDto;
using gymLog.API.Services;
using gymLog.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace gymLog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ICookieService _cookieService;
        private readonly IClaimsService _claimsService;

        public AuthController(IAuthService authService, ICookieService cookieService, IClaimsService claimsService)
        {
            _authService = authService;
            _cookieService = cookieService;
            _claimsService = claimsService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthDto>> Login(LoginDto loginDto)
        {
            var result = await _authService.Login(loginDto);
            if (result.IsSuccess)
            {
                _cookieService.SetCookies(Response, result.Data!);
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthDto>> Register(RegisterDto registerDto)
        {
            var result = await _authService.Register(registerDto);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("refresh")]
        public async Task<ActionResult<AuthDto>> Refresh(TokenDto tokenDto)
        {
            var result = await _authService.RefreshToken(tokenDto);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [Authorize]
        [HttpPost("revoke")]
        public async Task<IActionResult> Revoke()
        {
            var result = await _authService.RevokeToken(_claimsService.GetUserEmailFromClaims(User).Data!);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var result = await _authService.RevokeToken(_claimsService.GetUserEmailFromClaims(User).Data!);
            if (result.IsSuccess)
            {
                _cookieService.ClearCookies(Response);
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
    }
}