using System.ComponentModel.DataAnnotations;

namespace api.Dtos
{
    public class TasksDto
    {
        public int? TaskId { get; set; }

        [Required]
        public string TaskContent { get; set; } = string.Empty;

        [Required]
        public int CreatedUserId { get; set; }

        public int? AssignedUserId { get; set; }

        [Required]
        public int TaskStatusId { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? StartedDate { get; set; }
        public DateTime? FinishedDate { get; set; }
        public DateTime? AssignedDate { get; set; }

        public bool IsActive { get; set; } = true;
        public string? TaskStatusName { get; set; }
    }
}