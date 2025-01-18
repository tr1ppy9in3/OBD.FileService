using Microsoft.EntityFrameworkCore;

using OBD.FileService.Files.Core;
using OBD.FileService.Files.UseCases.Tags.Abstractions;

namespace OBD.FileService.DataAccess.Files;

public class TagRepository(Context context) : ITagRepository
{
    private readonly Context _context = context
        ?? throw new ArgumentNullException(nameof(context));

    private readonly DbSet<Tag> _tags = context.Tags;

    public IAsyncEnumerable<Tag> GetAllAvailable(long userId)
    {
        return _tags.Where(tag => tag.UserId == userId)
                    .AsAsyncEnumerable();
    }

    public Task<Tag?> GetByIdAvailable(Guid tagId, long userId)
    {
        return _tags.Where(tag => tag.Id == tagId && tag.UserId == userId)
                    .FirstOrDefaultAsync();
    }

    public Task Create(Tag tag)
    {
        _context.Add(tag);
        return _context.SaveChangesAsync();
    }

    public Task Delete(Tag tag)
    {
        _context.Remove(tag);
        return _context.SaveChangesAsync();
    }

    public Task Update(Tag tag)
    {
        _context.Update(tag);
        return _context.SaveChangesAsync();
    }

    public Task<bool> ExistsByName(string name, long UserId)
    {
        return _tags.AnyAsync(tag => tag.Name == name && tag.UserId == UserId); 
    }

    public Task<bool> ExistsById(Guid id, long UserId)
    {
        return _tags.AnyAsync(tag => tag.Id == id && tag.UserId == UserId);
    }
}
