namespace api.Dtos
{

    public class UsersDto
    {
        public int UserId { get; set; }

        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public int UserRoleId { get; set; }
        public string UserRoleName { get; set; } = string.Empty;

        public int? DepartmentId { get; set; }
        public string DepartmentName { get; set; } = string.Empty;

        public bool IsActive { get; set; }
    }
}