using api.Dtos;
using api.Models;

namespace api.Helpers
{
    public static class LeaveRequestsHelper
    {
        public static LeaveRequestsDto ToDto(LeaveRequests entity)
        {
            return new LeaveRequestsDto
            {
                LeaveRequestId = entity.LeaveRequestId,
                UserId = entity.UserId,

                LeaveTypeId = entity.LeaveTypeId,
                LeaveTypeName = entity.LeaveType?.LeaveTypeName ?? string.Empty,

                StartedDate = entity.StartedDate,
                FinishDate = entity.FinishDate,

                DayCount = (entity.FinishDate.Date - entity.StartedDate.Date).Days + 1,

                IsActive = entity.IsActive
            };
        }

        public static LeaveRequests ToEntity(LeaveRequestsDto dto)
        {
            return new LeaveRequests
            {
                LeaveRequestId = dto.LeaveRequestId,
                UserId = dto.UserId,
                LeaveTypeId = dto.LeaveTypeId,
                StartedDate = dto.StartedDate,
                FinishDate = dto.FinishDate,
                IsActive = dto.IsActive
            };
        }
    }
}