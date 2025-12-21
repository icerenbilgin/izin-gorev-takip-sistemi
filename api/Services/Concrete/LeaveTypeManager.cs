using api.Core.Utilities.Results.Abstract;
using api.Core.Utilities.Results.Concrete;
using api.Data;
using AutoMapper;
using api.Core.Utilities.Results;
using api.Dtos;

public class LeaveTypeManager : ILeaveTypeService
{
    private readonly AppDbContext _context;
    private readonly ILeaveTypeDal _leaveTypeDal;
    private readonly IMapper _mapper;

    public LeaveTypeManager(
        AppDbContext context,
        ILeaveTypeDal leaveTypeDal,
        IMapper mapper)
    {
        _context = context;
        _leaveTypeDal = leaveTypeDal;
        _mapper = mapper;
    }
  
    public IDataResult<List<LeaveTypesDto>> GetAllLeaveTypes()
    {
        try
        {
            var data = _leaveTypeDal.GetAllLeaveTypes();
            var dto = _mapper.Map<List<LeaveTypesDto>>(data);

            return new SuccessDataResult<List<LeaveTypesDto>>(dto);
        }
        catch (Exception ex)
        {
            return new ErrorDataResult<List<LeaveTypesDto>>(
                $"Kayıtlar getirilirken hata oluştu: {ex.Message}"
            );
        }
    }

   }