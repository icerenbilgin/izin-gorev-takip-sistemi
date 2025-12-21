using api.Data;
using api.DataAccess.Abstract;
using api.Models;
using Microsoft.EntityFrameworkCore;

public class EfTaskStatusDal
    : EfEntityRepositoryBase<TaskStatuses, AppDbContext>, ITaskStatusDal
{
    private readonly AppDbContext _context;

    public EfTaskStatusDal(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public List<TaskStatuses> GetAllTaskStatuses()
    {
        return _context.TaskStatuses
            .AsNoTracking()
            .Where(x => x.IsActive)
            .ToList();
    }
}