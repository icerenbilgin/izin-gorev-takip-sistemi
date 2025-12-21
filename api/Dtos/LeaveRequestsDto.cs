namespace api.Dtos
{
    public class LeaveRequestsDto
    {
        public int LeaveRequestId { get; set; }

        public int UserId { get; set; }
        public int LeaveTypeId { get; set; }
        public string LeaveTypeName { get; set; } = string.Empty;

        public DateTime StartedDate { get; set; }
        public DateTime FinishDate { get; set; }

        public int DayCount { get; set; }

        public bool IsActive { get; set; }
    }
}