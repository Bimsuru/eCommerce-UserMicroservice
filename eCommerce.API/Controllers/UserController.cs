
using eCommerce.Core.DTO;
using eCommerce.Core.ServiceContracts;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.API.Controllers;

[Route("api/v1/users")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserResponse>> GetUserById(Guid id)
    {
        var userRes = await _userService.GetUserById(id);

        if (userRes == null)
            return NotFound(userRes);

        return Ok(userRes);
    }
}
