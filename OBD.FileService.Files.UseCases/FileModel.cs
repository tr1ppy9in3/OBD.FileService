namespace OBD.FileService.Files.UseCases;

public class FileModel : BaseFileSystemObject
{
    public override FileSystemObjectType Type { get; set; } = FileSystemObjectType.File;

    public string? Description { get; set; }
}
