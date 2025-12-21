namespace api.Models
{
    public class LeaveRequests
    {
        public int LeaveRequestId { get; set; }

        public int UserId { get; set; }
        public Users? User { get; set; }

        public int LeaveTypeId { get; set; }
        public LeaveTypes? LeaveType { get; set; }

        public DateTime StartedDate { get; set; }
        public DateTime FinishDate { get; set; }
        public int DayCount { get; set; }

        public int? TeamLeaderId { get; set; }
        public DateTime? TeamLeaderApprovalDate { get; set; }

        public int? HrSpecialistId { get; set; }
        public DateTime? HrSpecialistApprovalDate { get; set; }

        public int? SeniorManagerId { get; set; }
        public DateTime? SeniorManagerApprovalDate { get; set; }

        public string? RejectionStatement { get; set; }
        public int? RejectingUserId { get; set; }

        public bool IsActive { get; set; } = true;
    
        public Users? TeamLeader { get; set; }
        public Users? HrSpecialist { get; set; }
        public Users? SeniorManager { get; set; }
        public Users? RejectingUser { get; set; }
    }
}