using api.Models;
using api.Core.Utilities.Results.Abstract;
using api.Dtos;

namespace api.Business.Abstract
{
    public interface ITaskService
    {
        IDataResult<List<TasksDto>> GetAllTasks();
        IDataResult<TasksDto> GetTaskById(int id);
        IDataResult<List<TasksDto>> GetTaskByUserId(int userId);
        IDataResult<TasksDto> AddOrUpdateTask(TasksDto tasksDto);
        Core.Utilities.Results.Abstract.IResult DeleteTask(int id);
    }
}