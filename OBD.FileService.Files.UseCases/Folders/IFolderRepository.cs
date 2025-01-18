using OBD.FileService.Files.Core;

namespace OBD.FileService.Files.UseCases.Folders;

public interface IFolderRepository
{
    public IAsyncEnumerable<Folder> GetAllAvailable(long userId);
    
    public Task<bool> ExistByName(string name, long userId);

    public Task<bool> ExistsById(Guid Id, long userId);


    public Task<Folder?> GetByIdAvailable(Guid id, long userId);

    public Task Create(Folder folder);

    public Task Update(Folder folder);

    public Task Delete(Folder folder);
}
