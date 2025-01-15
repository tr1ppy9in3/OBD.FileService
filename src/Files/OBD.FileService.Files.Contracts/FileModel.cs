namespace OBD.FileService.Files.Contracts;

public class FileModel
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
    
    public Guid FolderId { get; set; }
}
