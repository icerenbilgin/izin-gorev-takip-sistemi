using api.Models;
using api.Core.Utilities.Results.Abstract;
using api.Dtos;

public interface ILeaveRequestService
{
    IDataResult<List<LeaveRequestsDto>> GetAllLeaveRequest();
    IDataResult<List<LeaveRequestsDto>> GetLeaveRequestByUserId(int userId);
    IDataResult<LeaveRequestsDto> GetLeaveRequestById(int id);
    IDataResult<LeaveRequestsDto> AddOrUpdateLeaveRequest(LeaveRequestsDto leaveRequestsDto);
    api.Core.Utilities.Results.Abstract.IResult DeleteLeaveRequest(int id);
    IDataResult<CheckLeaveRequestDto> CheckLeaveRequest(CheckLeaveRequestDto checkLeaveRequestDto);
}
