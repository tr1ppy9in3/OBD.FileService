using OBD.FileService.Files.UseCases.FileSystem.Models.Abstractions;
using OBD.FileService.Files.UseCases.FileSystem.Models.Enums;

namespace OBD.FileService.Files.UseCases.FileSystem.Models;

public class FolderInputModel : BaseFileSystemObjectInput
{
    public override FileSystemObjectType Type { get; set; } = FileSystemObjectType.Folder;
}
