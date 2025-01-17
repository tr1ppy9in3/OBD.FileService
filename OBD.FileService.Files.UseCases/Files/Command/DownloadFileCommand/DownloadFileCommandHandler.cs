using MediatR;

using MNX.Application.UseCases.Results;

using OBD.FileService.Files.Core;

namespace OBD.FileService.Files.UseCases.Files.Command.DownloadFileCommand;

public class DownloadFileCommandHandler(IFileRepository fileRepository) : IRequestHandler<DownloadFileCommand, Result<DownloadFileCommandResult>>
{
    private readonly IFileRepository _fileRepository = fileRepository
        ?? throw new ArgumentNullException(nameof(fileRepository));

    public async Task<Result<DownloadFileCommandResult>> Handle(DownloadFileCommand request, CancellationToken cancellationToken)
    {
        Core.File? file = await _fileRepository.GetByIdAvailable(request.FileId, request.UserId);
        if (file is null)
            return Result<DownloadFileCommandResult>.Invalid($"Unable to find file with Id {request.FileId} available to user!");

        FileVersion? version = file.LastVersion;
        if (version is null)
            return Result<DownloadFileCommandResult>.Invalid($"Unable to find any version of file with Id {request.FileId}!");

        return Result<DownloadFileCommandResult>.Success(new()
        {
            Content =  version.Content,
            MimeType = GetMimeType(file.Extension),
            FileName = $"{file.Name}{file.Extension}"
        });
    }

    private static string GetMimeType(string extension)
    {
        return extension.ToLower() switch
        {
            ".txt" => "text/plain",
            ".pdf" => "application/pdf",
            ".jpg" => "image/jpeg",
            ".png" => "image/png",
            ".doc" => "application/msword",
            ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            ".xls" => "application/vnd.ms-excel",
            ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            ".zip" => "application/zip",
            ".json" => "application/json",
            _ => "application/octet-stream"
        };
    }
}
