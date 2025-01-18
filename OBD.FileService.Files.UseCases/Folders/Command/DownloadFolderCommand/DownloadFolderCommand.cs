using MediatR;
using MNX.Application.UseCases.Results;


namespace OBD.FileService.Files.UseCases.Folders.Command.DownloadFolderCommand;

public record DownloadFolderCommand(Guid FileId, long UserId) : IRequest<Result<DownloadFolderCommandResult>>;