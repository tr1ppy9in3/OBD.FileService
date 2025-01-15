using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

using OBD.FileService.Users.Core;
using OBD.FileService.Users.Core.Auth;

namespace OBD.FileService.DataAccess.Users.Cfg;

/// <summary>
/// Конфигурация для абстрактной модели User.
/// </summary>
internal class UserCfg : IEntityTypeConfiguration<BaseUser>
{
    public void Configure(EntityTypeBuilder<BaseUser> builder)
    {
        builder.HasKey(u => u.Id);

        builder.HasDiscriminator(u => u.Type)
               .HasValue<RegularUser>(UserType.Regular)
               .HasValue<AdminUser>(UserType.Admin);

        builder.HasMany(u => u.Roles)
               .WithMany(u => u.Users)
               .UsingEntity<UserRole>
               (
                    j => j.HasOne<Role>().WithMany().HasForeignKey(e => e.RoleId),
                    j => j.HasOne<BaseUser>().WithMany().HasForeignKey(e => e.UserId),
                    j =>
                    {
                        j.HasKey(e => new { e.UserId, e.RoleId });
                        j.ToTable("user_roles");
                    }
               );
    }
}
