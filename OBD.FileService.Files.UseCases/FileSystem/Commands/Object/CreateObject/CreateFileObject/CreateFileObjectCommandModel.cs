using Microsoft.AspNetCore.Http;

using OBD.FileService.Files.UseCases.FileSystem.Models.Abstractions;
using OBD.FileService.Files.UseCases.FileSystem.Models.Enums;

namespace OBD.FileService.Files.UseCases.FileSystem.Commands.Object.CreateObject.CreateFileObject;

public class CreateFileObjectCommandModel
{
    public required IFormFile Form { get; set; }

    public Guid? ParentFolderId { get; set; }

    public string? Description { get; set; }
}
