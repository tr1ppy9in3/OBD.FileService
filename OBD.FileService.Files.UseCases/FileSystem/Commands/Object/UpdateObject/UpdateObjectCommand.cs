using MediatR;

using MNX.Application.UseCases.Results;

using OBD.FileService.Files.UseCases.FileSystem.Models.Abstractions;

namespace OBD.FileService.Files.UseCases.FileSystem.Commands.Object.UpdateObject;

public record UpdateObjectCommand(Guid ObjectId, BaseFileSystemObjectInput Model,long UserId) : IRequest<Result<Unit>>;