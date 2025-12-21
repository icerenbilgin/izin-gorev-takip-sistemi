using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Data.Configurations
{
    public class LeaveRequestsConfiguration : IEntityTypeConfiguration<LeaveRequests>
    {
        public void Configure(EntityTypeBuilder<LeaveRequests> builder)
        {
            builder.ToTable("LeaveRequests");

            builder.HasKey(l => l.LeaveRequestId);

            builder.Property(l => l.LeaveRequestId)
                .ValueGeneratedOnAdd();

            builder.Property(l => l.RejectionStatement)
               .HasColumnType("nvarchar(max)");

            builder.Property(l => l.DayCount)
                .IsRequired();

            builder.HasOne(l => l.User)
                .WithMany(u => u.LeaveRequests)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(l => l.LeaveType)
                .WithMany(t => t.LeaveRequests)
                .HasForeignKey(l => l.LeaveTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(l => l.TeamLeader)
                .WithMany()
                .HasForeignKey(l => l.TeamLeaderId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(l => l.HrSpecialist)
                .WithMany()
                .HasForeignKey(l => l.HrSpecialistId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(l => l.SeniorManager)
                .WithMany()
                .HasForeignKey(l => l.SeniorManagerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(l => l.RejectingUser)
                .WithMany()
                .HasForeignKey(l => l.RejectingUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}