using eCommerce.Core.DTO;
using eCommerce.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.API.Controllers;

[Route("api/v1/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    // User registration endpoint
    [HttpPost("register")]
    public async Task<ActionResult<AuthenticationResponse>> Register(RegisterRequest registerRequest)
    {
        // Call the userService to handle registration
        AuthenticationResponse? regUser = await _userService.Register(registerRequest);

        // Check the response
        if (regUser == null || regUser.Success == false)
        {
            return BadRequest(regUser);
        }
        return Ok(regUser);
    }

    // User login endpoint
    [HttpPost("login")]
    public async Task<ActionResult<AuthenticationResponse>> Login(LoginRequest loginRequest)
    {
        var user = await _userService.Login(loginRequest);

        if (user == null || user.Success == false)
        {
            return Unauthorized(user);
        }

        return Ok(user);
    }
}
