using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TaskStatusController : ControllerBase
{
    private readonly ITaskStatusService _taskStatusService;

    public TaskStatusController(ITaskStatusService taskStatusService)
    {
        _taskStatusService = taskStatusService;
    }

    // GET: api/LeaveType
    [HttpGet("GetAllTaskStatuses")]
    public IActionResult GetAllTaskStatuses()
    {
        var result = _taskStatusService.GetAllTaskStatuses();
        return result.Success ? Ok(result) : BadRequest(result);
    }
}