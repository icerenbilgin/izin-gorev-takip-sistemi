using api.Core.Utilities.Results.Abstract;
using api.Core.Utilities.Results.Concrete;
using api.Data;
using api.DataAccess.Abstract;
using api.Dtos;
using api.Models;
using AutoMapper;

public class DepartmentManager : IDepartmentService
{
    private readonly AppDbContext _context;
    private readonly IDepartmentDal _departmentDal;
    private readonly IMapper _mapper;

    public DepartmentManager(AppDbContext context, IDepartmentDal departmentDal, IMapper mapper)
    {
        _context = context;
        _departmentDal = departmentDal;
        _mapper = mapper;
    }

    public IDataResult<DepartmentsDto> AddOrUpdateDepartment(DepartmentsDto departmentsDto)
    {
        try
        {
            if (departmentsDto.DepartmentId == null)
            {
                var entity = _mapper.Map<Departments>(departmentsDto);
                entity.IsActive = true;

                _context.Departments.Add(entity);
                _context.SaveChanges();

                var dto = _mapper.Map<DepartmentsDto>(entity);
                return new SuccessDataResult<DepartmentsDto>(dto, "Departman eklendi.");
            }
            else
            {
                var entity = _context.Departments
                    .FirstOrDefault(x => x.DepartmentId == departmentsDto.DepartmentId);

                if (entity == null)
                    return new ErrorDataResult<DepartmentsDto>("Departman bulunamadı.");

                _mapper.Map(departmentsDto, entity);

                _context.Departments.Update(entity);
                _context.SaveChanges();

                var dto = _mapper.Map<DepartmentsDto>(entity);
                return new SuccessDataResult<DepartmentsDto>(dto, "Departman güncellendi.");
            }
        }
        catch (Exception ex)
        {
            return new ErrorDataResult<DepartmentsDto>(
                $"Departman ekleme / güncelleme sırasında hata oluştu: {ex.Message}"
            );
        }
    }

    public api.Core.Utilities.Results.Abstract.IResult DeleteDepartment(int id)
    {
        try
        {
            var entity = _context.Departments
                .FirstOrDefault(x => x.DepartmentId == id);

            if (entity == null)
                return new ErrorResult("Departman bulunamadı.");

            entity.IsActive = false;

            _context.Departments.Update(entity);
            _context.SaveChanges();

            return new SuccessResult("Departman pasif hale getirildi.");
        }
        catch (Exception ex)
        {
            return new ErrorResult(
                $"Departman pasife alınırken hata oluştu: {ex.Message}"
            );
        }
    }

    public IDataResult<List<DepartmentsDto>> GetAllDepartments()
    {
        try
        {
            var data = _departmentDal.GetAllDepartments();
            var dto = _mapper.Map<List<DepartmentsDto>>(data);

            return new SuccessDataResult<List<DepartmentsDto>>(dto);
        }
        catch (Exception ex)
        {
            return new ErrorDataResult<List<DepartmentsDto>>(
                $"Departmanlar getirilirken hata oluştu: {ex.Message}"
            );
        }
    }

    public IDataResult<DepartmentsDto> GetDepartmentById(int id)
    {
        try
        {
            var entity = _departmentDal.GetDepartmentById(id);

            if (entity == null)
                return new ErrorDataResult<DepartmentsDto>("Departman bulunamadı.");

            var dto = _mapper.Map<DepartmentsDto>(entity);
            return new SuccessDataResult<DepartmentsDto>(dto);
        }
        catch (Exception ex)
        {
            return new ErrorDataResult<DepartmentsDto>(
                $"Departman getirilirken hata oluştu: {ex.Message}"
            );
        }
    }
}