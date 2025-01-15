using MediatR;
using MNX.Application.UseCases.Results;

using OBD.FileService.Files.Core;

namespace OBD.FileService.Files.UseCases.Folders.Queries.GetFolderByIdQuery;

public record GetFolderByIdQuery(Guid Id, long UserId) : IRequest<Result<Folder>>;
