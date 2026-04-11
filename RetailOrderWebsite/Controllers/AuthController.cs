using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RetailOrderWebsite.DTOs;
using RetailOrderWebsite.Services.Interfaces;

namespace RetailOrderWebsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        
            private readonly IAuthService _service;

            public AuthController(IAuthService service)
            {
                _service = service;
            }

            //POST api/auth/register
            [HttpPost("register")]
            public async Task<IActionResult> Register([FromBody] RegisterDto dto)
            {
                try
                {
                    var result = await _service.RegisterAsync(dto);
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    return BadRequest(new { message = ex.Message });
                }
            }

            // POST api/auth/login
            [HttpPost("login")]
            public async Task<IActionResult> Login([FromBody] LoginDto dto)
            {
                try
                {
                    var result = await _service.LoginAsync(dto);
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    return Unauthorized(new { message = ex.Message });
                }
            }
        }
    }
