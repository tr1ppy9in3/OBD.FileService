
using Newtonsoft.Json;

using OBD.FileService.Files.Core;
using OBD.FileService.Files.UseCases.FileSystem.Models.Enums;

using System.Text.Json.Serialization;

namespace OBD.FileService.Files.UseCases.FileSystem.Models.Abstractions;

[JsonDerivedType(typeof(FileModel), typeDiscriminator: "File")]
[JsonDerivedType(typeof(FolderModel), typeDiscriminator: "Folder")]
public abstract class BaseFileSystemObject
{
    public Guid Id { get; set; }

    public abstract FileSystemObjectType Type { get; set; }

    public required string Name { get; set; }

    public long SizeInBytes { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool IsFavorite { get; set; }

    public IEnumerable<Tag> Tags { get; set; } = new List<Tag>(0);

}
