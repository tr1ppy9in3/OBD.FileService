using MediatR;
using MNX.Application.UseCases.Results;

namespace OBD.FileService.Files.UseCases.Files.Command.Favorite.UnmarkFileAsFavoriteCommand;

public record UnmarkFileAsFavoriteCommand(Guid FileId, long UserId) : IRequest<Result<Unit>>;
