using api.Dtos;
using api.Models;

namespace api.DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<Users>
    {
        List<UsersDto> GetAllUsers();
        Users? GetUserById(int id);
        Users? GetByEmailAndPassword(string email, string password);
    }
}