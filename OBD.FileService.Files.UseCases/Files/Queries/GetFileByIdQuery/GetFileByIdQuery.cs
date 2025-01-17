using MediatR;
using MNX.Application.UseCases.Results;

using File = OBD.FileService.Files.Core.File;

namespace OBD.FileService.Files.UseCases.Files.Queries.GetFileByIdQuery;

public record GetFileByIdQuery(Guid FileId, long UserId) : IRequest<Result<File>>;
