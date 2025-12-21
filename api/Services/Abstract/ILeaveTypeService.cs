using api.Models;
using api.Core.Utilities.Results.Abstract;
using api.Dtos;

public interface ILeaveTypeService
{
    IDataResult<List<LeaveTypesDto>> GetAllLeaveTypes();
}
