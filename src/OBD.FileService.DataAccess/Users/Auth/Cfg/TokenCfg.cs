using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using OBD.FileService.Users.Core;
using OBD.FileService.Users.Core.Auth;

namespace OBD.FileService.DataAccess.Users.Auth.Cfg;

public class TokenCfg : IEntityTypeConfiguration<Token>
{
    public void Configure(EntityTypeBuilder<Token> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Value)
            .IsRequired()
            .HasMaxLength(512);

        builder.HasOne<BaseUser>()
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
