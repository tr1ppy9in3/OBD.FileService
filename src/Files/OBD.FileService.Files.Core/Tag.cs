namespace OBD.FileService.Files.Core;

public class Tag
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public required string Name { get; set; }

    public string? Description { get; set; }
}
