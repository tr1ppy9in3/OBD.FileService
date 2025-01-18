using MediatR;
using MNX.Application.UseCases.Results;

namespace OBD.FileService.Files.UseCases.FileSystem.Commands.Favorite.UnmarkObjectAsFavorite;

public record UnmarkObjectAsFavoriteCommand(Guid ObjectId, long UserId) : IRequest<Result<Unit>>;
