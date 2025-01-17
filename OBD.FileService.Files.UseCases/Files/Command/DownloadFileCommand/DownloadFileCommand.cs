using MediatR;

using MNX.Application.UseCases.Results;

namespace OBD.FileService.Files.UseCases.Files.Command.DownloadFileCommand;

public record DownloadFileCommand(Guid FileId, long UserId) : IRequest<Result<DownloadFileCommandResult>>;
