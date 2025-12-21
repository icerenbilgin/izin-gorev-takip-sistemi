using api.Data;
using api.DataAccess.Abstract;
using api.Models;
using Microsoft.EntityFrameworkCore;

public class EfLeaveTypeDal
    : EfEntityRepositoryBase<LeaveTypes, AppDbContext>, ILeaveTypeDal
{
    private readonly AppDbContext _context;

    public EfLeaveTypeDal(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public List<LeaveTypes> GetAllLeaveTypes()
    {
        return _context.LeaveTypes
            .AsNoTracking()
            .Where(x => x.IsActive)
            .ToList();
    }
}