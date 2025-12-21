using api.Data;
using api.DataAccess.Abstract;
using api.Models;
using Microsoft.EntityFrameworkCore;

public class EfLeaveRequestsDal
    : EfEntityRepositoryBase<LeaveRequests, AppDbContext>, ILeaveRequestsDal
{
    private readonly AppDbContext _context;

    public EfLeaveRequestsDal(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public List<LeaveRequests> GetAllLeaveRequest()
    {
        return _context.LeaveRequests
            .AsNoTracking()
            .Where(x => x.IsActive)
            .Include(x => x.LeaveType)
            .ToList();
    }

    public LeaveRequests? GetLeaveRequestById(int id)
    {
        return _context.LeaveRequests
            .AsNoTracking()
            .Include(x => x.LeaveType)
            .FirstOrDefault(x => x.IsActive && x.LeaveRequestId == id);
    }

    public List<LeaveRequests> GetLeaveRequestByUserId(int userId)
    {
        return _context.LeaveRequests
            .Include(x => x.LeaveType)
            .Where(x => x.IsActive && x.UserId == userId)
            .ToList();
    }
}