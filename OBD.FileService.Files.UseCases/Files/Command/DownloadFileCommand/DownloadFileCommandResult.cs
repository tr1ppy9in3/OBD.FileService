using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBD.FileService.Files.UseCases.Files.Command.DownloadFileCommand;

public class DownloadFileCommandResult
{
    public byte[] Content { get; set; } = Array.Empty<byte>();

    public string FileName { get; set; } = string.Empty;

    public string MimeType { get; set; } = "application/octet-stream";
}