using api.Dtos;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class DepartmentController : ControllerBase
{
    private readonly IDepartmentService _departmentService;

    public DepartmentController(IDepartmentService departmentService)
    {
        _departmentService = departmentService;
    }

    // GET: api/Department/GetAllDepartments
    [HttpGet("GetAllDepartments")]
    public IActionResult GetAllDepartments()
    {
        var result = _departmentService.GetAllDepartments();
        return result.Success ? Ok(result) : BadRequest(result);
    }

    // GET: api/Department/GetDepartmentById/5
    [HttpGet("GetDepartmentById/{id}")]
    public IActionResult GetDepartmentById(int id)
    {
        var result = _departmentService.GetDepartmentById(id);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    // POST: api/Department/AddOrUpdateDepartment
    [HttpPost("AddOrUpdateDepartment")]
    public IActionResult AddOrUpdateDepartment([FromBody] DepartmentsDto departmentsDto)
    {
        var result = _departmentService.AddOrUpdateDepartment(departmentsDto);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    // DELETE: api/Department/DeleteDepartment/5
    [HttpDelete("DeleteDepartment/{id}")]
    public IActionResult DeleteDepartment(int id)
    {
        var result = _departmentService.DeleteDepartment(id);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}