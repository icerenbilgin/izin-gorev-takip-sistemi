using api.Dtos;
using api.Models;

namespace api.Mapping
{
    public static class TasksHelper
    {
        public static TasksDto ToDto(Tasks entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            return new TasksDto
            {
                TaskId = entity.TaskId,
                TaskContent = entity.TaskContent,

                CreatedUserId = entity.CreatedUserId,
                AssignedUserId = entity.AssignedUserId ?? 0,

                TaskStatusId = entity.TaskStatusId,
                
                TaskStatusName = entity.TaskStatus != null
            ? entity.TaskStatus.TaskStatusName
            : null,
                CreatedDate = entity.CreatedDate,
                AssignedDate = entity.AssignedDate,
                StartedDate = entity.StartedDate,
                FinishedDate = entity.FinishedDate,

                IsActive = entity.IsActive
            };
        }

        public static Tasks ToEntity(TasksDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (dto.CreatedUserId <= 0)
                throw new Exception("CreatedUserId zorunludur.");

            if (dto.TaskStatusId <= 0)
                throw new Exception("TaskStatusId zorunludur.");

            if (string.IsNullOrWhiteSpace(dto.TaskContent))
                throw new Exception("TaskContent zorunludur.");

            return new Tasks
            {
                TaskId = dto.TaskId ?? 0,

                TaskContent = dto.TaskContent.Trim(),

                CreatedUserId = dto.CreatedUserId,
                AssignedUserId = dto.AssignedUserId > 0 ? dto.AssignedUserId : null,

                TaskStatusId = dto.TaskStatusId,

                CreatedDate = dto.CreatedDate ?? DateTime.UtcNow,

                AssignedDate = dto.AssignedDate,
                StartedDate = dto.StartedDate,
                FinishedDate = dto.FinishedDate,

                IsActive = dto.IsActive
            };
        }
    }
}