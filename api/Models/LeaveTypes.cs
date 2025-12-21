namespace api.Models
{
    public class LeaveTypes
    {
        public int LeaveTypeId { get; set; }
        public string LeaveTypeName { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        public ICollection<LeaveRequests>? LeaveRequests { get; set; }
    }
}
