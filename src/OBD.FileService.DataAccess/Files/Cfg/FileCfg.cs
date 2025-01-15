using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using OBD.FileService.DataAccess.Files.DTO;
using OBD.FileService.Files.Core;
using OBD.FileService.Users.Core;

namespace OBD.FileService.DataAccess.Files.Cfg;

using File = FileService.Files.Core.File;

public class FileCfg : IEntityTypeConfiguration<File>
{
    public void Configure(EntityTypeBuilder<File> builder)
    {
        builder.HasKey(file => file.Id);

        builder.HasMany(file => file.Versions)
               .WithOne()
               .HasForeignKey(fileVersion => fileVersion.FileId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<BaseUser>()
               .WithMany()
               .HasForeignKey(file => file.UserId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Folder>()
               .WithMany(folder => folder.AttachedFiles)
               .HasForeignKey(file => file.FolderId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(file => file.Tags)
               .WithMany()
               .UsingEntity<FileTag>
               (
                    j => j.HasOne<Tag>().WithMany().HasForeignKey(e => e.TagId),
                    j => j.HasOne<File>().WithMany().HasForeignKey(e => e.FileId),
                    j =>
                    {
                        j.HasKey(e => new { e.TagId, e.FileId });
                        j.ToTable("file_tags");
                    }
               );

        builder.HasIndex(file => file.UserId).HasDatabaseName("IX_Files_UserId");
        builder.HasIndex(file => file.FolderId).HasDatabaseName("IX_Files_FolderId");
    }
}
