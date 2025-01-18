namespace OBD.FileService.Files.UseCases.Folders;

public class FolderInputModel 
{
    public required Guid? ParentFolderId { get; set; }
    public required string Name { get; set; }
}
