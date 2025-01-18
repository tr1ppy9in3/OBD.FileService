namespace OBD.FileService.Files.UseCases.Tags.Models;

public class TagInputModel
{
    public required string Name { get; set; }

    public string? Description { get; set; } = default;
}
