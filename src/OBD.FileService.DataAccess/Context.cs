using Microsoft.EntityFrameworkCore;

using OBD.FileService.Users.Core.Auth;
using OBD.FileService.Users.Core;

using OBD.FileService.DataAccess.Users;
using OBD.FileService.DataAccess.Users.Auth.Cfg;
using OBD.FileService.Files.Core;
using OBD.FileService.DataAccess.Files.DTO;

namespace OBD.FileService.DataAccess;

/// <summary>
/// Контекст БД.
/// </summary>
public class Context : DbContext
{
    #region Users
    
    public DbSet<Token> Tokens { get; set; }

    public DbSet<Role> Roles { get; set; }

    public DbSet<UserRole> RoleToUsers { get; set; }

    public DbSet<BaseUser> Users { get; set; }
    
    public DbSet<AdminUser> Admins { get; set; }
    
    public DbSet<RegularUser> RegularUsers { get; set; }
    

    #endregion

    #region

    public DbSet<FileService.Files.Core.File> Files { get; set; }

    public DbSet<Folder> Folders { get; set; }
    public DbSet<FolderTag> FolderTags { get; set; }

    public DbSet<Tag> Tags { get; set; }
    public DbSet<FileTag> FileTags { get; set; }


    #endregion

    public Context(DbContextOptions<Context> option) : base(option)
    {
        //Database.EnsureDeleted();   
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TokenCfg).Assembly);
    }
}
