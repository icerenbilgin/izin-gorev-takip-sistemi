namespace api.Dtos
{
    public class LoginResponseDto
    {
        public int UserId { get; set; }
        public int UserRoleId { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
    }
}