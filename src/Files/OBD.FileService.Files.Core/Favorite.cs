namespace OBD.FileService.Files.Core;

public class Favorite
{
    public Guid Id { get; set; } = Guid.NewGuid(); 

    public Guid UserId { get; set; } 

    public Guid? FileId { get; set; } 

    public Guid? FolderId { get; set; } 

    public DateTime AddedAt { get; set; } = DateTime.UtcNow; 
}
