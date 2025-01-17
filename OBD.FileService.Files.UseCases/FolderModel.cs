namespace OBD.FileService.Files.UseCases;

public class FolderModel : BaseFileSystemObject
{
    public override FileSystemObjectType Type { get; set; } = FileSystemObjectType.Folder;

    public IEnumerable<BaseFileSystemObject> Objects { get; set; } = new List<BaseFileSystemObject>(0);
}
