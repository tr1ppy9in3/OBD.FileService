using OBD.FileService.Files.UseCases.FileSystem.Models.Enums;

using System.Text.Json.Serialization;

namespace OBD.FileService.Files.UseCases.FileSystem.Models.Abstractions;

[JsonDerivedType(typeof(FileInputModel), typeDiscriminator: "File")]
[JsonDerivedType(typeof(FolderInputModel), typeDiscriminator: "Folder")]
public abstract class BaseFileSystemObjectInput
{
    public required string Name { get; set; }

    public Guid? ParentFolderId { get; set; }

    public abstract FileSystemObjectType Type { get; set; }
}
