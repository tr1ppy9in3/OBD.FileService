using MediatR;

using MNX.Application.UseCases.Results;

using File = OBD.FileService.Files.Core.File;

namespace OBD.FileService.Files.UseCases.Folders.Queries.GetAttachedFilesByIdQuery;

public record GetAttachedFilesByIdQuery(Guid Id, long UserId) : IRequest<Result<List<File>>>;