using Microsoft.EntityFrameworkCore;

using OBD.FileService.Files.Core;
using OBD.FileService.Files.UseCases.Files;

namespace OBD.FileService.DataAccess.Files;

public class FileRepository(Context context) : IFileRepository
{
    private readonly Context _context = context
        ?? throw new ArgumentNullException(nameof(context));

    private readonly DbSet<FileService.Files.Core.File> _files = context.Files;

    public IAsyncEnumerable<FileService.Files.Core.File> GetAllAvailable(long userId)
    {
        return 
        _files.Where(file => file.UserId == userId)
              .IncludeAll()
              .OrderBy(folder => folder.Name.ToLower())
              .AsAsyncEnumerable();
    }

    public Task<FileService.Files.Core.File?> GetByIdAvailable(Guid id, long userId)
    {
        return 
        _files.IncludeAll()
              .FirstOrDefaultAsync(file => file.UserId == userId && file.Id == id);
    }

    public Task Create(FileService.Files.Core.File file)
    {
        _files.Add(file);
        return _context.SaveChangesAsync();
    }

    public Task Update(FileService.Files.Core.File file)
    {
        _files.Update(file);
        return _context.SaveChangesAsync();
    }

    public Task Delete(FileService.Files.Core.File file)
    {
        _files.Remove(file);
        return _context.SaveChangesAsync();
    }

    public Task<bool> Exists(FileService.Files.Core.File fileInput)
    {
        return
       _files.AnyAsync(file => file.Name == fileInput.Name &&
                               file.Extension == fileInput.Extension);
    }

    public Task<bool> ExistsByName(string name)
    {
        return 
        _files.AnyAsync(file => file.Name == name);
    }
}

public static class FileQueryExtensions
{
    public static IQueryable<FileService.Files.Core.File> IncludeAll(this IQueryable<FileService.Files.Core.File> query)
    {
        return query.Include(file => file.Versions)
                    .Include(file => file.Tags);
    }
}