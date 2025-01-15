using OBD.FileService.Files.Core;

namespace OBD.FileService.Files.UseCases.Folders;

public interface IFolderRepository
{
    public IAsyncEnumerable<Folder> GetAllAvailable(long UserId);
    
    public Task<Folder?> GetByIdAvailable(Guid Id, long UserId);

    public Task Create(Folder folder);

    public Task Update(Folder folder);

    public Task Delete(Folder folder);
}
