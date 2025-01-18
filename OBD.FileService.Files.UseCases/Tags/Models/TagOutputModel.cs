namespace OBD.FileService.Files.UseCases.Tags.Models;

public class TagOutputModel
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public string? Description { get; set; }
}
