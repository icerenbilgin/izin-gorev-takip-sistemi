using api.Models;
using api.Core.Utilities.Results.Abstract;
using api.Dtos;

public interface IUserRoleService
{
    IDataResult<List<UserRolesDto>> GetAllUserRoles();
}
