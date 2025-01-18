using MediatR;

using MNX.Application.UseCases.Results;

namespace OBD.FileService.Files.UseCases.FileSystem.Commands.Tags.AddTagToObject;
public record AddTagToObjectCommand(Guid ObjectId, Guid TagId, long UserId) : IRequest<Result<Unit>>;