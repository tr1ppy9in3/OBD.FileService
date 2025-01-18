namespace OBD.FileService.Files.UseCases.Folders.Command.DownloadFolderCommand;

public class DownloadFolderCommandResult
{
    public byte[] Content { get; set; } = Array.Empty<byte>();

    public string FileName { get; set; } = string.Empty;

    public string MimeType { get; set; } = "application/zip";
}
