using api.Business.Abstract;
using api.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        // GET: api/Task
        [HttpGet("GetAllTasks")]
        public IActionResult GetAllTasks()
        {
            var result = _taskService.GetAllTasks();
            return result.Success ? Ok(result) : BadRequest(result);
        }

        // GET: api/Task/5
        [HttpGet("{id}")]
        public IActionResult GetTaskById(int id)
        {
            var result = _taskService.GetTaskById(id);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        // GET: api/Task/user/5
        [HttpGet("GetTaskByUserId/{userId}")]
        public IActionResult GetTaskByUserId(int userId)
        {
            var result = _taskService.GetTaskByUserId(userId);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        // POST: api/Task
        [HttpPost("AddOrUpdateTask")]
        public IActionResult AddOrUpdateTask([FromBody] TasksDto tasksDto)
        {
            var result = _taskService.AddOrUpdateTask(tasksDto);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        // DELETE: api/Task/5  (Soft delete beklenir)
        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            var result = _taskService.DeleteTask(id);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}