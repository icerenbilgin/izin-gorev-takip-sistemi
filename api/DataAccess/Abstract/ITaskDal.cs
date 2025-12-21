using api.Dtos;
using api.Models;

namespace api.DataAccess.Abstract
{
    public interface ITaskDal : IEntityRepository<Tasks>
    {
        List<TasksDto> GetAllTasks();
        Tasks? GetTaskById(int id);
         List<Tasks> GetTaskByUserId(int userId);
    }
}