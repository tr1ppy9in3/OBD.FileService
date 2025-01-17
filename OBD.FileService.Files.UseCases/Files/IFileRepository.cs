using File = OBD.FileService.Files.Core.File;

namespace OBD.FileService.Files.UseCases.Files;

public interface IFileRepository
{
    public IAsyncEnumerable<File> GetAllAvailable(long userId);

    public Task<File?> GetByIdAvailable(Guid id, long userId);
    
    public Task<bool> Exists(File file);

    public Task Create(File file);

    public Task Update(File file);

    public Task Delete(File file);
}
