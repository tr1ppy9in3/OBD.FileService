using Microsoft.EntityFrameworkCore;

using OBD.FileService.Files.Core;
using OBD.FileService.Files.UseCases.Folders;

namespace OBD.FileService.DataAccess.Files;

public class FolderRepository(Context context) : IFolderRepository
{
    private readonly Context _context = context;

    private readonly DbSet<Folder> _folders = context.Folders;

    public IAsyncEnumerable<Folder> GetAllAvailable(long UserId)
    {
        return _folders.Where(folder => folder.UserId == UserId)
                       .IncludeAll()
                       .OrderBy(folder => folder.Name.ToLower())
                       .AsAsyncEnumerable();
    }

    public Task<Folder?> GetByIdAvailable(Guid Id, long UserId)
    {
        return _folders.FirstOrDefaultAsync(x => x.UserId == UserId && x.Id == Id);
    }
    public Task Create(Folder folder)
    {
        _folders.Add(folder);
        return _context.SaveChangesAsync();
    }

    public Task Delete(Folder folder)
    {
        _folders.Remove(folder);
        return _context.SaveChangesAsync();
    }

    public Task Update(Folder folder)
    {
        _folders.Update(folder);
        return _context.SaveChangesAsync();

    }
}


public static class FolderQueryExtensions
{
    public static IQueryable<Folder> IncludeAll(this IQueryable<Folder> query)
    {
        return query.Include(folder => folder.AttachedFiles)
                    .Include(folder => folder.AttachedFolders)
                    .Include(folder => folder.Tags);
    }
}