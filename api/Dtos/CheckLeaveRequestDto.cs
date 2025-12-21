namespace api.Dtos
{
    public class CheckLeaveRequestDto
    {
        public int LeaveRequestId { get; set; }
        public int ApprovedByUserId { get; set; }
        public int ApprovedByRoleId { get; set; }
    }
}