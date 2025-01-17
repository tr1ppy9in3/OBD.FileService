using MediatR;
using MNX.Application.UseCases.Results;

using OBD.FileService.Files.Core;

namespace OBD.FileService.Files.UseCases.Files.Queries.GetFileTagsByIdQuery;

public record GetFileTagsByIdQuery(Guid FileId, long UserId) : IRequest<Result<IEnumerable<Tag>>>;
