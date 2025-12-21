namespace api.Models
{
    public class Tasks
    {
        public int TaskId { get; set; }
        public string TaskContent { get; set; } = string.Empty;
        public int CreatedUserId { get; set; }        
        public int? AssignedUserId { get; set; }
        public int TaskStatusId { get; set; }
        
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? StartedDate { get; set; }
        public DateTime? FinishedDate { get; set; }
        public DateTime? AssignedDate { get; set; }

        public bool IsActive { get; set; } = true;

        public Users? CreatedUser { get; set; }
        public Users? AssignedUser { get; set; }
        public TaskStatuses? TaskStatus { get; set; }
    }
}
