using MediatR;

namespace OBD.FileService.Files.UseCases.Files.Queries.GetFavoritesFilesQuery;

public record GetFavoritesFilesQuery(long UserId) : IStreamRequest<Core.File>;
