using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Data.Configurations
{
    public class TaskStatusesConfiguration : IEntityTypeConfiguration<TaskStatuses>
    {
        public void Configure(EntityTypeBuilder<TaskStatuses> builder)
        {
            builder.ToTable("TaskStatuses");

            builder.HasKey(s => s.TaskStatusId);

            builder.Property(s => s.TaskStatusId)
                .ValueGeneratedOnAdd();

            builder.Property(s => s.TaskStatusName)
                .IsRequired()
                .HasMaxLength(350);

            builder.Property(s => s.IsActive)
                .HasDefaultValue(true);

            builder.HasData(
                new TaskStatuses { TaskStatusId = 1, TaskStatusName = "Başlanmadı", IsActive = true },
                new TaskStatuses { TaskStatusId = 2, TaskStatusName = "Devam Ediyor", IsActive = true },
                new TaskStatuses { TaskStatusId = 3, TaskStatusName = "Durduruldu", IsActive = true },
                new TaskStatuses { TaskStatusId = 4, TaskStatusName = "Tamamlandı", IsActive = true },
                new TaskStatuses { TaskStatusId = 5, TaskStatusName = "İptal Edildi", IsActive = true }
            );
        }
    }
}