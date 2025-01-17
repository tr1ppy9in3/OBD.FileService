using MediatR;
using MNX.Application.UseCases.Results;

namespace OBD.FileService.Files.UseCases.Files.Command.DeleteFileCommand;

public record DeleteFileCommand(Guid FileId, long UserId) : IRequest<Result<Unit>>;
