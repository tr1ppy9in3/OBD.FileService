using MediatR;
using MNX.Application.UseCases.Results;

namespace OBD.FileService.Files.UseCases.FileSystem.Commands.Object.DeleteObject;

public record DeleteObjectCommand(Guid ObjectId, long UserId) : IRequest<Result<Unit>>;
