using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class LeaveTypeController : ControllerBase
{
    private readonly ILeaveTypeService _leaveTypeService;

    public LeaveTypeController(ILeaveTypeService leaveTypeService)
    {
        _leaveTypeService = leaveTypeService;
    }

    // GET: api/LeaveType
    [HttpGet("GetAllLeaveTypes")]
    public IActionResult GetAllLeaveTypes()
    {
        var result = _leaveTypeService.GetAllLeaveTypes();
        return result.Success ? Ok(result) : BadRequest(result);
    }
}