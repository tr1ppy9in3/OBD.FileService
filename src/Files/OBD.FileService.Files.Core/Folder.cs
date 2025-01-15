
namespace OBD.FileService.Files.Core;

public class Folder
{
    public Guid Id { get; set; }

    public required string Name { get; set; }

    public string? Description { get; set; }

    public bool IsFavorite { get; set; } = false;

    public List<Folder> AttachedFolders { get; set; } = new(0);

    public List<File> AttachedFiles { get; set; } = new(0);

    public long TotalSizeBytes => AttachedFiles.Sum(file => file.LastVersion?.SizeBytes ?? 0) + 
                                  AttachedFolders.Sum(folder => folder.TotalSizeBytes);

    public int TotalObjectCount => AttachedFiles.Count +
                                   AttachedFolders.Count;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    public List<Tag> Tags = new(0);

    public long UserId { get; set; }

    public Guid? ParentFolderId { get; set; }

    public bool IsRoot => !ParentFolderId.HasValue;
}
