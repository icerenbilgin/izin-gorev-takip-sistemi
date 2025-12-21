using api.Core.Utilities.Results.Abstract;
using api.Core.Utilities.Results.Concrete;
using api.Data;
using AutoMapper;
using api.Core.Utilities.Results;
using api.Dtos;

public class TaskStatusManager : ITaskStatusService
{
    private readonly AppDbContext _context;
    private readonly ITaskStatusDal _taskStatusDal;
    private readonly IMapper _mapper;

    public TaskStatusManager(
        AppDbContext context,
        ITaskStatusDal taskStatusDal,
        IMapper mapper)
    {
        _context = context;
        _taskStatusDal = taskStatusDal;
        _mapper = mapper;
    }

    public IDataResult<List<TaskStatusesDto>> GetAllTaskStatuses()
    {
        try
        {
            var data = _taskStatusDal.GetAllTaskStatuses();
            var dto = _mapper.Map<List<TaskStatusesDto>>(data);

            return new SuccessDataResult<List<TaskStatusesDto>>(dto);
        }
        catch (Exception ex)
        {
            return new ErrorDataResult<List<TaskStatusesDto>>(
                $"Kayıtlar getirilirken hata oluştu: {ex.Message}"
            );
        }
    }
}