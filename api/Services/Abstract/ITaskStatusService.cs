using api.Models;
using api.Core.Utilities.Results.Abstract;
using api.Dtos;

public interface ITaskStatusService
{
    IDataResult<List<TaskStatusesDto>> GetAllTaskStatuses();
}
