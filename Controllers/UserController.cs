using Microsoft.AspNetCore.Mvc;
using Maxula_project.Services.UserService;

namespace Maxula_project.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService activityService)
    {
        _userService = activityService;
    }

    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<GetUserResponseDto>>>> GetAllUsers()
    {
        var response = await _userService.GetAllUsers();

        if (!response.Success)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }

    /// <summary>
    /// Logs in using email and password.
    /// </summary>
    [HttpGet("login")]
    [ProducesResponseType(typeof(ServiceResponse<GetUserResponseDto>), 200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> PerformLogin(string email, string password)
    {
        var response = await _userService.Login(email, password);

        if (!response.Success)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }

}
