using MediatR;
using MNX.Application.UseCases.Results;

namespace OBD.FileService.Files.UseCases.Folders.Queries.GetAttachedFoldersByIdQuery;

public record GetAttachedFoldersByIdQuery(Guid Id, long UserId) : IRequest<Result<List<Core.Folder>>>;