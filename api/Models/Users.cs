namespace api.Models
{
    public class Users
    {
        public int UserId { get; set; }

        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        public int UserRoleId { get; set; }
        public int? DepartmentId { get; set; }

        public UserRoles? UserRole { get; set; }
        public Departments? Department { get; set; }
        public ICollection<LeaveRequests>? LeaveRequests { get; set; }
    }
}