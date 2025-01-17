namespace OBD.FileService.Files.UseCases;

public abstract class BaseFileSystemObject
{
    public abstract FileSystemObjectType Type { get; set; }

    public required string Name { get; set; }

    public required string Path { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool IsFavorite { get; set; }
}
