using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Data.Configurations
{
    public class DepartmentsConfiguration : IEntityTypeConfiguration<Departments>
    {
        public void Configure(EntityTypeBuilder<Departments> builder)
        {
            builder.ToTable("Departments");

            builder.HasKey(d => d.DepartmentId);

            builder.Property(d => d.DepartmentId)
                .ValueGeneratedOnAdd();

            builder.Property(d => d.DepartmentName)
                .IsRequired()
                .HasMaxLength(350);

            builder.Property(d => d.IsActive)
                .HasDefaultValue(true);
        }
    }
}