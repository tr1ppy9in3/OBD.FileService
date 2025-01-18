using OBD.FileService.Files.UseCases.FileSystem.Models.Abstractions;
using OBD.FileService.Files.UseCases.FileSystem.Models.Enums;

namespace OBD.FileService.Files.UseCases.FileSystem.Models;

public class FolderModel : BaseFileSystemObject
{
    public override FileSystemObjectType Type { get; set; } = FileSystemObjectType.Folder;

    public IEnumerable<BaseFileSystemObject> Content { get; set; } = new List<BaseFileSystemObject>(0);
}
