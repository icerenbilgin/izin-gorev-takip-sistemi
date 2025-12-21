using api.Core.Utilities.Results.Abstract;
using api.Core.Utilities.Results.Concrete;
using api.Data;
using AutoMapper;
using api.Core.Utilities.Results;
using api.Dtos;

public class UserRoleManager : IUserRoleService
{
    private readonly AppDbContext _context;
    private readonly IUserRoleDal _userRoleDal;
    private readonly IMapper _mapper;

    public UserRoleManager(
        AppDbContext context,
        IUserRoleDal userRoleDal,
        IMapper mapper)
    {
        _context = context;
        _userRoleDal = userRoleDal;
        _mapper = mapper;
    }

    public IDataResult<List<UserRolesDto>> GetAllUserRoles()
    {
        try
        {
            var data = _userRoleDal.GetAllUserRoles();
            var dto = _mapper.Map<List<UserRolesDto>>(data);

            return new SuccessDataResult<List<UserRolesDto>>(dto);
        }
        catch (Exception ex)
        {
            return new ErrorDataResult<List<UserRolesDto>>(
                $"Kayıtlar getirilirken hata oluştu: {ex.Message}"
            );
        }
    }
}