using MediatR;
using MNX.Application.UseCases.Results;

namespace OBD.FileService.Files.UseCases.Files.Command.UpdateFileCommand;

public record UpdateFileCommand(Guid FileId, UpdateFileCommandModel Model,long UserId) : IRequest<Result<Unit>>;
