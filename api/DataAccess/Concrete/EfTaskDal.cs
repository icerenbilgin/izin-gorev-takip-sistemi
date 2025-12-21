using api.Data;
using api.DataAccess.Abstract;
using api.Dtos;
using api.Models;
using Microsoft.EntityFrameworkCore;
using api.Mapping;

namespace api.DataAccess.Concrete
{
    public class EfTaskDal : EfEntityRepositoryBase<Tasks, AppDbContext>, ITaskDal
    {
        private readonly AppDbContext _context;

        public EfTaskDal(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public List<TasksDto> GetAllTasks()
        {
            var tasks = _context.Tasks
                .AsNoTracking()
                .Where(x => x.IsActive)
                .Include(x => x.CreatedUser)
                .Include(x => x.AssignedUser)
                .Include(x => x.TaskStatus)
                .ToList();

            return tasks.Select(TasksHelper.ToDto).ToList();
        }

        public Tasks? GetTaskById(int id)
        {
            return _context.Tasks
                .AsNoTracking()
                .Include(x => x.CreatedUser)
                .Include(x => x.AssignedUser)
                .Include(x => x.TaskStatus)
                .FirstOrDefault(x => x.IsActive && x.TaskId == id);
        }

        public List<Tasks> GetTaskByUserId(int userId)
        {
            return _context.Tasks
                .AsNoTracking()
                .Include(x => x.TaskStatus)
                .Where(x => x.IsActive && x.AssignedUserId == userId)
                .ToList();
        }
    }
}