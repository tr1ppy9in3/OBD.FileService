using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using OBD.FileService.DataAccess.Files.DTO;
using OBD.FileService.Files.Core;
using OBD.FileService.Users.Core;

namespace OBD.FileService.DataAccess.Files.Cfg;

public class FolderCfg : IEntityTypeConfiguration<Folder>
{
    public void Configure(EntityTypeBuilder<Folder> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasMany(folder => folder.AttachedFiles)
               .WithOne()
               .HasForeignKey(file => file.FolderId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(folder => folder.AttachedFolders)
               .WithOne()
               .HasForeignKey(folder => folder.ParentFolderId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<BaseUser>()
               .WithMany()
               .HasForeignKey(folder => folder.UserId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(file => file.Tags)
               .WithMany()
               .UsingEntity<FolderTag>
               (
                    j => j.HasOne<Tag>().WithMany().HasForeignKey(e => e.TagId),
                    j => j.HasOne<Folder>().WithMany().HasForeignKey(e => e.FolderId),
                    j =>
                    {
                        j.HasKey(e => new { e.TagId, e.FolderId });
                        j.ToTable("folder_tags");
                    }
               );

        builder.HasIndex(folder => folder.UserId).HasDatabaseName("IX_Folders_UserId");
        builder.HasIndex(folder => folder.ParentFolderId).HasDatabaseName("IX_Folders_ParentFolderId");
    }
}
