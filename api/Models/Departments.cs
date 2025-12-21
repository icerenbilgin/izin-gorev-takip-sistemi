namespace api.Models
{
    public class Departments
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        public ICollection<Users>? Users { get; set; }
    }
}
