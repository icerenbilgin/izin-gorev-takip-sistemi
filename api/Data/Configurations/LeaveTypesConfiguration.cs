using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Data.Configurations
{
    public class LeaveTypesConfiguration : IEntityTypeConfiguration<LeaveTypes>
    {
        public void Configure(EntityTypeBuilder<LeaveTypes> builder)
        {
            builder.ToTable("LeaveTypes");

            builder.HasKey(t => t.LeaveTypeId);

            builder.Property(t => t.LeaveTypeId)
                .ValueGeneratedOnAdd();

            builder.Property(t => t.LeaveTypeName)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(t => t.IsActive)
                .HasDefaultValue(true);

            builder.HasData(
                new LeaveTypes { LeaveTypeId = 1, LeaveTypeName = "Yıllık İzin", IsActive = true },
                new LeaveTypes { LeaveTypeId = 2, LeaveTypeName = "Mazeret İzni", IsActive = true },
                new LeaveTypes { LeaveTypeId = 3, LeaveTypeName = "Hastalık İzni", IsActive = true },
                new LeaveTypes { LeaveTypeId = 4, LeaveTypeName = "Doğum İzni", IsActive = true },
                new LeaveTypes { LeaveTypeId = 5, LeaveTypeName = "Babalık İzni", IsActive = true },
                new LeaveTypes { LeaveTypeId = 6, LeaveTypeName = "Evlilik İzni", IsActive = true },
                new LeaveTypes { LeaveTypeId = 7, LeaveTypeName = "Cenaze İzni", IsActive = true },
                new LeaveTypes { LeaveTypeId = 8, LeaveTypeName = "Ücretsiz İzin", IsActive = true }
            );
        }
    }
}