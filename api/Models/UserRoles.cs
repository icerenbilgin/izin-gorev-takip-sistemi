namespace api.Models
{
    public class UserRoles
    {
        public int UserRoleId { get; set; }
        public string UserRoleName { get; set; } = string.Empty;

        public bool IsActive { get; set; } = true;

        public ICollection<Users>? Users { get; set; }
    }
}