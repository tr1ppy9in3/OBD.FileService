using MediatR;
using MNX.Application.UseCases.Results;

namespace OBD.FileService.Files.UseCases.FileSystem.Commands.Favorite.MarkObjectAsFavorite;

public record MarkObjectAsFavoriteCommand(Guid ObjectId, long UserId) : IRequest<Result<Unit>>;