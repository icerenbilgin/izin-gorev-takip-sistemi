using api.Dtos;
using api.Models;

namespace api.Helpers
{
    public static class UsersHelper
    {
        public static UsersDto ToDto(Users user)
        {
            return new UsersDto
            {
                UserId = user.UserId,
                Name = user.Name ?? string.Empty,
                LastName = user.LastName ?? string.Empty,
                FullName = $"{user.Name} {user.LastName}".Trim(),

                Email = user.Email ?? string.Empty,

                UserRoleId = user.UserRoleId,
                UserRoleName = user.UserRole?.UserRoleName ?? string.Empty,

                DepartmentId = user.DepartmentId,
                DepartmentName = user.Department?.DepartmentName ?? string.Empty,

                IsActive = user.IsActive
            };
        }

        public static Users ToEntity(UsersDto dto)
        {
            return new Users
            {
                UserId = dto.UserId,
                Name = dto.Name,
                LastName = dto.LastName,
                Email = dto.Email,
                IsActive = dto.IsActive,
                UserRoleId = dto.UserRoleId,
                DepartmentId = dto.DepartmentId
            };
        }
    }
}
