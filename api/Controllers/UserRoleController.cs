using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UserRoleController : ControllerBase
{
    private readonly IUserRoleService _userRoleService;

    public UserRoleController(IUserRoleService userRoleService)
    {
        _userRoleService = userRoleService;
    }

    // GET: api/LeaveType
    [HttpGet("GetAllUserRoles")]
    public IActionResult GetAllUserRoles()
    {
        var result = _userRoleService.GetAllUserRoles();
        return result.Success ? Ok(result) : BadRequest(result);
    }
}