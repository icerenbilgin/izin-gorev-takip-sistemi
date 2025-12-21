using api.Dtos;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class LeaveRequestController : ControllerBase
{
    private readonly ILeaveRequestService _leaveRequestService;

    public LeaveRequestController(ILeaveRequestService leaveRequestService)
    {
        _leaveRequestService = leaveRequestService;
    }

    // GET: api/LeaveRequest
    [HttpGet("GetAllLeaveRequest")]
    public IActionResult GetAllLeaveRequest()
    {
        var result = _leaveRequestService.GetAllLeaveRequest();
        return result.Success ? Ok(result) : BadRequest(result);
    }

    // GET: api/LeaveRequest/user/5
    [HttpGet("GetLeaveRequestByUserId/{id}")]
    public IActionResult GetLeaveRequestByUserId(int id)
    {
        var result = _leaveRequestService.GetLeaveRequestByUserId(id);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    // GET: api/LeaveRequest/5
    [HttpGet("{id}")]
    public IActionResult GetLeaveRequestById(int id)
    {
        var result = _leaveRequestService.GetLeaveRequestById(id);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    // POST: api/LeaveRequest
    [HttpPost("AddOrUpdateLeaveRequest")]
    public IActionResult AddOrUpdateLeaveRequest([FromBody] LeaveRequestsDto leaveRequestsDto)
    {
        var result = _leaveRequestService.AddOrUpdateLeaveRequest(leaveRequestsDto);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPost("CheckLeaveRequest")]
    public IActionResult CheckLeaveRequest([FromBody] CheckLeaveRequestDto checkLeaveRequestDto)
    {
        var result = _leaveRequestService.CheckLeaveRequest(checkLeaveRequestDto);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    // DELETE: api/LeaveRequest/5
    [HttpDelete("{id}")]
    public IActionResult DeleteLeaveRequest(int id)
    {
        var result = _leaveRequestService.DeleteLeaveRequest(id);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}