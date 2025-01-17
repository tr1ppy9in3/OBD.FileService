using MediatR;
using MNX.Application.UseCases.Results;

namespace OBD.FileService.Files.UseCases.Tags.Command.UpdateTagCommand;

public record UpdateTagCommand(TagModel Model, Guid TagId, long UserId) : IRequest<Result<Unit>>;
