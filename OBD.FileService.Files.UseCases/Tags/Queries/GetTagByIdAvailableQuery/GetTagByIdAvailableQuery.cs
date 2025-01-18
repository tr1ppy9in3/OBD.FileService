using MediatR;
using MNX.Application.UseCases.Results;

using OBD.FileService.Files.Core;
using OBD.FileService.Files.UseCases.Tags.Models;

namespace OBD.FileService.Files.UseCases.Tags.Queries.GetTagByIdAvailableQuery;

public record GetTagByIdAvailableQuery(Guid TagId, long UserId) : IRequest<Result<TagOutputModel>>;