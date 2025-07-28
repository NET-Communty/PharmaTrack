using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Abstractions.Dtos.AccountDto;
using Service.Abstractions.IServices;

namespace PharmaTrack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountServices _accountServices;
        public AccountController(IAccountServices accountServices)
        {
            _accountServices=accountServices;
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(a => a.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(new { Message = "Failed to login..", Errors = errors });
            }
            var result = await _accountServices.Login(loginDto);
            if (!result.IsAuthenticated)
            {
                return Unauthorized(new { Message = result.Message });
            }
            if (!string.IsNullOrEmpty(result.RefreshToken))
                SetRfreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);
            return Ok(result);
        }
        [HttpGet("RefreshToken")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var result = await _accountServices.RefreshTokenAsync(refreshToken);
            if (!result.IsAuthenticated)
            {
                return Unauthorized(new { Message = result.Message });
            }
            SetRfreshTokenInCookie(result.RefreshToken, result.RefreshTokenExpiration);
            return Ok(result);
        }
        [HttpPost("RevokeToken")]
        public async Task<IActionResult> RevokeToken([FromBody] RevokeTokenDto revokeTokenDto)
        {
            var token = revokeTokenDto.Token??Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(token))
                return BadRequest("Token is required !");

            var result = await _accountServices.RevokeTokenAsync(token);
            if(!result)
                return BadRequest("Token is invalid !");
            return Ok("Token revoked success");
        }
        private void SetRfreshTokenInCookie(string refreshToken,DateTime expires)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly =true,
                Expires = expires.ToLocalTime()
            };
            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
        }
    }
}
