using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using OBD.FileService.Files.Core;
using OBD.FileService.Users.Core;

namespace OBD.FileService.DataAccess.Files.Cfg;

internal class TagCfg : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.HasKey(tag => tag.Id);

        builder.HasOne<BaseUser>()
               .WithMany()
               .HasForeignKey(tag => tag.UserId);
    }
}
