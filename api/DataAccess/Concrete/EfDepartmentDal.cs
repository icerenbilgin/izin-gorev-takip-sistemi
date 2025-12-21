using api.Data;
using api.DataAccess.Abstract;
using api.Models;
using Microsoft.EntityFrameworkCore;

public class EfDepartmentDal
    : EfEntityRepositoryBase<Departments, AppDbContext>, IDepartmentDal
{
    private readonly AppDbContext _context;

    public EfDepartmentDal(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public List<Departments> GetAllDepartments()
    {
        return _context.Departments
            .AsNoTracking()
            .Where(x => x.IsActive)
            .ToList();
    }

    public Departments? GetDepartmentById(int id)
    {
        return _context.Departments
            .AsNoTracking()
            .FirstOrDefault(x => x.IsActive && x.DepartmentId == id);
    }
}