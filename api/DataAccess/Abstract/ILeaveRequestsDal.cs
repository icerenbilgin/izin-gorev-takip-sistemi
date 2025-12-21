using api.Models;

public interface ILeaveRequestsDal : IEntityRepository<LeaveRequests>
{
    List<LeaveRequests> GetAllLeaveRequest();
    List<LeaveRequests> GetLeaveRequestByUserId(int userId);
    LeaveRequests? GetLeaveRequestById(int id);
}