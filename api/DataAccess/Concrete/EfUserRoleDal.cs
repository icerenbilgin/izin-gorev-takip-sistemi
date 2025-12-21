using api.Data;
using api.DataAccess.Abstract;
using api.Models;
using Microsoft.EntityFrameworkCore;

public class EfUserRoleDal
    : EfEntityRepositoryBase<TaskStatuses, AppDbContext>, IUserRoleDal
{
    private readonly AppDbContext _context;

    public EfUserRoleDal(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public List<UserRoles> GetAllUserRoles()
    {
        return _context.UserRoles
            .AsNoTracking()
            .Where(x => x.IsActive)
            .ToList();
    }
}