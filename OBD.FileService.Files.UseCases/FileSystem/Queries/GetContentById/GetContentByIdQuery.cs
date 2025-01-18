using MediatR;

using MNX.Application.UseCases.Results;

using OBD.FileService.Files.UseCases.FileSystem.Models.Abstractions;

namespace OBD.FileService.Files.UseCases.FileSystem.Queries.GetContentQuery;

public record GetContentByIdQuery(Guid Id, long UserId) : IRequest<Result<BaseFileSystemObject>>;
