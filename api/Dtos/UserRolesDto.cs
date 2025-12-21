namespace api.Dtos
{
    public class UserRolesDto
    {
        public int? UserRoleId { get; set; }
        public string? UserRoleName { get; set; } = string.Empty;

        public bool? IsActive { get; set; } = true;
    }
}