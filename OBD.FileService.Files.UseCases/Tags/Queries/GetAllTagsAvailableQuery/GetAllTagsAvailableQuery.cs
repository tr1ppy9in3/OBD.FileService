using MediatR;

using OBD.FileService.Files.Core;
using OBD.FileService.Files.UseCases.Tags.Models;

namespace OBD.FileService.Files.UseCases.Tags.Queries.GetAllTagsAvailableQuery;

public record GetAllTagsAvailableQuery(long UserId) : IStreamRequest<TagOutputModel>;
