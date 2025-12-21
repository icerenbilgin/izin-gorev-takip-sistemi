using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api.Data.Configurations
{
    public class UserRolesConfiguration : IEntityTypeConfiguration<UserRoles>
    {
        public void Configure(EntityTypeBuilder<UserRoles> builder)
        {
            builder.ToTable("UserRoles");

            builder.HasKey(r => r.UserRoleId);

            builder.Property(r => r.UserRoleId)
                .ValueGeneratedOnAdd();

            builder.Property(r => r.UserRoleName)
                .IsRequired()
                .HasMaxLength(350);

            builder.Property(r => r.IsActive)
                .HasDefaultValue(true);

             builder.HasData(
                new UserRoles { UserRoleId = 1, UserRoleName = "Admin", IsActive = true },
                new UserRoles { UserRoleId = 2, UserRoleName = "Çalışan", IsActive = true },
                new UserRoles { UserRoleId = 3, UserRoleName = "İnsan Kaynakları Sorumlusu", IsActive = true },
                new UserRoles { UserRoleId = 4, UserRoleName = "Takım Lideri", IsActive = true },
                new UserRoles { UserRoleId = 5, UserRoleName = "Yönetici", IsActive = true }
            );
        }
    }
}