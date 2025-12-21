using api.Data;
using api.DataAccess.Abstract;
using api.Dtos;
using api.Helpers;
using api.Models;
using Microsoft.EntityFrameworkCore;

public class EfUserDal : EfEntityRepositoryBase<Users, AppDbContext>, IUserDal
{
    private readonly AppDbContext _context;

    public EfUserDal(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public List<UsersDto> GetAllUsers()
    {
        var users = _context.Users
            .AsNoTracking()
            .Where(u => u.IsActive)
            .Include(u => u.UserRole)
            .Include(u => u.Department)
            .ToList();

        return users
            .Select(UsersHelper.ToDto)
            .ToList();
    }

    public Users? GetUserById(int id)
    {
        return _context.Users
            .AsNoTracking()
            .Include(u => u.UserRole)
            .Include(u => u.Department)
            .FirstOrDefault(x => x.IsActive && x.UserId == id);
    }

    public Users? GetByEmailAndPassword(string email, string password)
    {
        return _context.Users
            .AsNoTracking()
            .Include(x => x.UserRole)
            .FirstOrDefault(x =>
                x.IsActive &&
                x.Email == email &&
                x.Password == password
            );
    }
}