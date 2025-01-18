using MediatR;

using MNX.Application.UseCases.Results;

using OBD.FileService.Files.UseCases.Tags.Models;

namespace OBD.FileService.Files.UseCases.Tags.Command.UpdateTagCommand;

public record UpdateTagCommand(TagInputModel Model, Guid TagId, long UserId) : IRequest<Result<Unit>>;
