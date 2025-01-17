using MediatR;
using MNX.Application.UseCases.Results;

namespace OBD.FileService.Files.UseCases.Files.Command.Favorite.MarkFavoriteFileCommand;

public record MarkFileAsFavoriteCommand(Guid FileId, long UserId) : IRequest<Result<Unit>>;

