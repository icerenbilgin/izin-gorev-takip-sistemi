using api.Core.Utilities.Results.Abstract;
using api.Dtos;

public interface IDepartmentService
{
    IDataResult<List<DepartmentsDto>> GetAllDepartments();
    IDataResult<DepartmentsDto> GetDepartmentById(int id);
    IDataResult<DepartmentsDto> AddOrUpdateDepartment(DepartmentsDto departmentsDto);
    api.Core.Utilities.Results.Abstract.IResult DeleteDepartment(int id);
}
