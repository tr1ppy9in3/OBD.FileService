using MediatR;

using OBD.FileService.Files.Core;

namespace OBD.FileService.Files.UseCases.Tags.Queries.GetAllTagsAvailableQuery;

public record GetAllTagsAvailableQuery(long UserId) : IStreamRequest<Tag>;
