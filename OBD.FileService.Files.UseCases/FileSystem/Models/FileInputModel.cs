using Microsoft.AspNetCore.Http;

using OBD.FileService.Files.UseCases.FileSystem.Models.Abstractions;
using OBD.FileService.Files.UseCases.FileSystem.Models.Enums;

namespace OBD.FileService.Files.UseCases.FileSystem.Models;

public class FileInputModel : BaseFileSystemObjectInput
{
    public override FileSystemObjectType Type { get; set; } = FileSystemObjectType.File;

    public string? Description { get; set; }
}
