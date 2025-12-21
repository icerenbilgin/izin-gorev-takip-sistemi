using api.Models;

namespace api.DataAccess.Abstract
{
    public interface IDepartmentDal : IEntityRepository<Departments>
    {
        List<Departments> GetAllDepartments();
        Departments? GetDepartmentById(int id);
    }
}