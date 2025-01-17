
using OBD.FileService.Files.Core;

namespace OBD.FileService.Files.UseCases.Tags;
public interface ITagRepository
{
    public IAsyncEnumerable<Tag> GetAllAvailable(long UserId);

    public Task<Tag?> GetByIdAvailable(Guid TagId, long UserId);

    public Task Create(Tag tag);

    public Task Update(Tag tag);

    public Task Delete(Tag tag);

    public Task<bool> ExistsByName(string name, long UserId);
}
