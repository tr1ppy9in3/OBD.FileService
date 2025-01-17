using MediatR;
using MNX.Application.UseCases.Results;

namespace OBD.FileService.Files.UseCases.Tags.Command.DeleteTagCommand;

public record DeleteTagCommand(Guid TagId, long UserId) : IRequest<Result<Unit>>;