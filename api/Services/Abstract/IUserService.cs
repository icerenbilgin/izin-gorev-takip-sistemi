using api.Core.Utilities.Results.Abstract;
using api.Dtos;

public interface IUserService
{
    IDataResult<List<UsersDto>> GetAllUsers();
    IDataResult<UsersDto> GetUserById(int id);
    IDataResult<UsersDto> AddOrUpdateUser(UsersDto usersDto);
    api.Core.Utilities.Results.Abstract.IResult DeleteUser(int id);
    IDataResult<LoginResponseDto> Login(LoginRequestDto request);
}