using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Data.Configurations
{
    public class TasksConfiguration : IEntityTypeConfiguration<Tasks>
    {
        public void Configure(EntityTypeBuilder<Tasks> builder)
        {
            builder.ToTable("Tasks");

            builder.HasKey(t => t.TaskId);

            builder.Property(t => t.TaskId)
                .ValueGeneratedOnAdd();

            builder.Property(l => l.TaskContent)
                .IsRequired()
                .HasColumnType("nvarchar(max)");

            builder.Property(t => t.CreatedDate)
                .HasDefaultValueSql("GETDATE()");

            builder.Property(t => t.IsActive)
                .HasDefaultValue(true);

            builder.HasOne(t => t.CreatedUser)
                .WithMany()
                .HasForeignKey(t => t.CreatedUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(t => t.AssignedUser)
                .WithMany()
                .HasForeignKey(t => t.AssignedUserId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(t => t.TaskStatus)
                .WithMany(s => s.Tasks)
                .HasForeignKey(t => t.TaskStatusId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}