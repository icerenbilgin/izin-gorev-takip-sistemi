using api.Models;

public interface ITaskStatusDal
{
    List<TaskStatuses> GetAllTaskStatuses();
}