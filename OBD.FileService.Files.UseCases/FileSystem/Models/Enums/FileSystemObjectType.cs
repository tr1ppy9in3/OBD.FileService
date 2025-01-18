using System.Text.Json.Serialization;

namespace OBD.FileService.Files.UseCases.FileSystem.Models.Enums;


[JsonConverter(typeof(JsonStringEnumConverter))]
public enum FileSystemObjectType
{
    File,
    Folder
}
