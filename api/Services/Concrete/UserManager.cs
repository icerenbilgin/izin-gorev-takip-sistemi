using api.Core.Utilities.Results.Abstract;
using api.Core.Utilities.Results.Concrete;
using api.Data;
using api.DataAccess.Abstract;
using api.Dtos;
using api.Helpers;
using api.Models;
using AutoMapper;

public class UserManager : IUserService
{
    private readonly AppDbContext _context;
    private readonly IUserDal _userDal;
    private readonly IMapper _mapper;

    public UserManager(AppDbContext context, IUserDal userDal, IMapper mapper)
    {
        _context = context;
        _userDal = userDal;
        _mapper = mapper;
    }

    public IDataResult<UsersDto> AddOrUpdateUser(UsersDto usersDto)
    {
        try
        {
            var entity = UsersHelper.ToEntity(usersDto);

            if (entity.UserId == 0)
                _userDal.Add(entity);
            else
                _userDal.Update(entity);

            return new SuccessDataResult<UsersDto>(usersDto);
        }
        catch (Exception ex)
        {
            return new ErrorDataResult<UsersDto>(
                $"Kullanıcı ekleme / güncelleme sırasında hata oluştu: {ex.Message}"
            );
        }
    }

    public api.Core.Utilities.Results.Abstract.IResult DeleteUser(int id)
    {
        try
        {
            var entity = _context.Users
                .FirstOrDefault(x => x.UserId == id);

            if (entity == null)
                return new ErrorResult("Kullanıcı bulunamadı.");

            entity.IsActive = false;

            _context.Users.Update(entity);
            _context.SaveChanges();

            return new SuccessResult("Kullanıcı pasif hale getirildi.");
        }
        catch (Exception ex)
        {
            return new ErrorResult(
                $"Kullanıcı pasife alınırken hata oluştu: {ex.Message}"
            );
        }
    }

    public IDataResult<List<UsersDto>> GetAllUsers()
    {
        try
        {
            var dto = _userDal.GetAllUsers();
            return new SuccessDataResult<List<UsersDto>>(dto);
        }
        catch (Exception ex)
        {
            return new ErrorDataResult<List<UsersDto>>(
                $"Kullanıcılar getirilirken hata oluştu: {ex.Message}"
            );
        }
    }

    public IDataResult<UsersDto> GetUserById(int id)
    {
        try
        {
            var entity = _userDal.GetUserById(id);

            if (entity == null)
                return new ErrorDataResult<UsersDto>("Kullanıcı bulunamadı.");

            var dto = UsersHelper.ToDto(entity);

            return new SuccessDataResult<UsersDto>(dto);
        }
        catch (Exception ex)
        {
            return new ErrorDataResult<UsersDto>(
                $"Kullanıcı getirilirken hata oluştu: {ex.Message}"
            );
        }
    }

    public IDataResult<LoginResponseDto> Login(LoginRequestDto request)
    {
        try
        {
            var user = _userDal.GetByEmailAndPassword(request.Email, request.Password);

            if (user == null)
                return new ErrorDataResult<LoginResponseDto>("E-posta veya şifre hatalı.");

            var response = new LoginResponseDto
            {
                UserId = user.UserId,
                UserRoleId = user.UserRoleId,
                FullName = $"{user.Name} {user.LastName}",
                Email = user.Email,
                RoleName = user.UserRole?.UserRoleName ?? string.Empty
            };

            return new SuccessDataResult<LoginResponseDto>(response, "Giriş başarılı.");
        }
        catch (Exception ex)
        {
            return new ErrorDataResult<LoginResponseDto>(
                $"Giriş sırasında hata oluştu: {ex.Message}"
            );
        }
    }
}