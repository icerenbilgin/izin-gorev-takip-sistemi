using api.Business.Abstract;
using api.Core.Utilities.Results.Abstract;
using api.Core.Utilities.Results.Concrete;
using api.Data;
using api.DataAccess.Abstract;
using api.Dtos;
using api.Helpers;
using api.Hubs;
using api.Mapping;
using api.Models;
using AutoMapper;
using Microsoft.AspNetCore.SignalR;

namespace api.Business.Concrete
{
    public class TaskManager : ITaskService
    {
        private readonly AppDbContext _context;
        private readonly ITaskDal _taskDal;
        private readonly IMapper _mapper;
        private readonly IHubContext<NotificationHub> _hubContext;

        public TaskManager(AppDbContext context, ITaskDal taskDal, IMapper mapper, IHubContext<NotificationHub> hubContext)
        {
            _context = context;
            _taskDal = taskDal;
            _mapper = mapper;
            _hubContext = hubContext;
        }

        public IDataResult<TasksDto> AddOrUpdateTask(TasksDto tasksDto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(tasksDto.TaskContent))
                    return new ErrorDataResult<TasksDto>("Task içeriği zorunludur.");

                if (tasksDto.CreatedUserId <= 0)
                    return new ErrorDataResult<TasksDto>("CreatedUserId zorunludur.");

                if (tasksDto.TaskStatusId <= 0)
                    return new ErrorDataResult<TasksDto>("TaskStatusId zorunludur.");

                if (tasksDto.TaskId == null || tasksDto.TaskId == 0)
                {
                    var entity = new Tasks
                    {
                        TaskContent = tasksDto.TaskContent.Trim(),
                        CreatedUserId = tasksDto.CreatedUserId,
                        AssignedUserId = tasksDto.AssignedUserId,
                        TaskStatusId = tasksDto.TaskStatusId,

                        CreatedDate = DateTime.UtcNow,
                        AssignedDate = tasksDto.AssignedUserId.HasValue
                            ? DateTime.UtcNow
                            : null,

                        StartedDate = tasksDto.StartedDate,
                        FinishedDate = tasksDto.FinishedDate,

                        IsActive = true
                    };

                    _context.Tasks.Add(entity);
                    _context.SaveChanges();

                    if (entity.AssignedUserId.HasValue)
                    {
                        _hubContext
                            .Clients
                            .Group(entity.AssignedUserId.Value.ToString())
                            .SendAsync(
                                "ReceiveTaskAssigned",
                                $"Yeni bir görev atandı: {entity.TaskContent}"
                            );
                    }

                    return new SuccessDataResult<TasksDto>(
                        new TasksDto
                        {
                            TaskId = entity.TaskId,
                            TaskContent = entity.TaskContent,
                            CreatedUserId = entity.CreatedUserId,
                            AssignedUserId = entity.AssignedUserId,
                            TaskStatusId = entity.TaskStatusId,
                            CreatedDate = entity.CreatedDate,
                            AssignedDate = entity.AssignedDate,
                            StartedDate = entity.StartedDate,
                            FinishedDate = entity.FinishedDate,
                            IsActive = entity.IsActive
                        },
                        "Görev eklendi."
                    );
                }
                else
                {
                    var existingEntity = _context.Tasks.FirstOrDefault(x => x.TaskId == tasksDto.TaskId && x.IsActive);

                    if (existingEntity == null)
                        return new ErrorDataResult<TasksDto>("Görev bulunamadı.");

                    existingEntity.TaskContent = tasksDto.TaskContent.Trim();
                    existingEntity.TaskStatusId = tasksDto.TaskStatusId;

                    if (existingEntity.AssignedUserId != tasksDto.AssignedUserId)
                    {
                        existingEntity.AssignedUserId = tasksDto.AssignedUserId;
                        existingEntity.AssignedDate = tasksDto.AssignedUserId.HasValue
                            ? DateTime.UtcNow
                            : null;
                    }

                    existingEntity.StartedDate = tasksDto.StartedDate;
                    existingEntity.FinishedDate = tasksDto.FinishedDate;
                    existingEntity.IsActive = tasksDto.IsActive;

                    _context.SaveChanges();

                    return new SuccessDataResult<TasksDto>(
                        new TasksDto
                        {
                            TaskId = existingEntity.TaskId,
                            TaskContent = existingEntity.TaskContent,
                            CreatedUserId = existingEntity.CreatedUserId,
                            AssignedUserId = existingEntity.AssignedUserId,
                            TaskStatusId = existingEntity.TaskStatusId,
                            CreatedDate = existingEntity.CreatedDate,
                            AssignedDate = existingEntity.AssignedDate,
                            StartedDate = existingEntity.StartedDate,
                            FinishedDate = existingEntity.FinishedDate,
                            IsActive = existingEntity.IsActive
                        },
                        "Görev güncellendi."
                    );
                }

            }
            catch (Exception ex)
            {
                return new ErrorDataResult<TasksDto>(
                    $"Görev ekleme / güncelleme sırasında hata oluştu: {ex.Message}"
                );
            }
        }

        public Core.Utilities.Results.Abstract.IResult DeleteTask(int id)
        {
            try
            {
                var entity = _context.Tasks
                    .FirstOrDefault(x => x.TaskId == id);

                if (entity == null)
                    return new ErrorResult("Görev bulunamadı.");

                entity.IsActive = false;

                _context.Tasks.Update(entity);
                _context.SaveChanges();

                return new SuccessResult("Görev pasif hale getirildi.");
            }
            catch (Exception ex)
            {
                return new ErrorResult(
                    $"Görev pasife alınırken hata oluştu: {ex.Message}"
                );
            }
        }

        public IDataResult<List<TasksDto>> GetAllTasks()
        {
            try
            {
                var dto = _taskDal.GetAllTasks();
                return new SuccessDataResult<List<TasksDto>>(dto);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<TasksDto>>(
                    $"Görevler getirilirken hata oluştu: {ex.Message}"
                );
            }
        }

        public IDataResult<TasksDto> GetTaskById(int id)
        {
            try
            {
                var entity = _taskDal.GetTaskById(id);

                if (entity == null)
                    return new ErrorDataResult<TasksDto>("Görev bulunamadı.");

                var dto = TasksHelper.ToDto(entity);
                return new SuccessDataResult<TasksDto>(dto);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<TasksDto>(
                    $"Görev getirilirken hata oluştu: {ex.Message}"
                );
            }
        }

        public IDataResult<List<TasksDto>> GetTaskByUserId(int userId)
        {
            try
            {
                var entities = _taskDal.GetTaskByUserId(userId);

                var dtoList = entities
                    .Select(TasksHelper.ToDto)
                    .ToList();

                return new SuccessDataResult<List<TasksDto>>(dtoList);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<TasksDto>>(
                    $"İzin talepleri getirilirken hata oluştu: {ex.Message}"
                );
            }
        }
    }
}

