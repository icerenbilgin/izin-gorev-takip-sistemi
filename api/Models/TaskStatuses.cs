namespace api.Models
{
    public class TaskStatuses
    {
        public int TaskStatusId { get; set; }
        public string TaskStatusName { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        public ICollection<Tasks>? Tasks { get; set; }
    }
}
