namespace OBD.FileService.Files.Core;

public class FileVersion
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public required string Tag { get; set; }

    public required string Hash { get; set; }

    public required string Content { get; set; }

    public long SizeBytes { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Guid FileId { get; set; }
}
