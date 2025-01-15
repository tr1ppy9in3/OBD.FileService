namespace OBD.FileService.Files.Core;

public class File
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public required string Name { get; set; }

    public required string Extension { get; set; }

    public string? Description { get ; set; }

    public bool IsFavorite { get; set; } = false;

    public List<FileVersion> Versions { get; set; } = new(0);

    public FileVersion? LastVersion => Versions.OrderByDescending(version => version.CreatedAt)
                                               .FirstOrDefault();
    public List<Tag> Tags { get; set; } = new(0);

    public long UserId { get; set; }

    public Guid? FolderId { get; set; }
}
