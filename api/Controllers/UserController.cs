using Microsoft.AspNetCore.Mvc;
using api.Dtos;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    // GET: api/User
    [HttpGet("GetAllUsers")]
    public IActionResult GetAllUsers()
    {
        var result = _userService.GetAllUsers();
        return result.Success ? Ok(result) : BadRequest(result);
    }

    // GET: api/User/5
    [HttpGet("GetUserById/{id}")]
    public IActionResult GetUserById(int id)
    {
        var result = _userService.GetUserById(id);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    // POST: api/User
    [HttpPost]
    public IActionResult AddOrUpdateUser([FromBody] UsersDto usersDto)
    {
        var result = _userService.AddOrUpdateUser(usersDto);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    // DELETE: api/User/5  (Soft delete beklenir)
    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        var result = _userService.DeleteUser(id);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequestDto request)
    {
        var result = _userService.Login(request);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}