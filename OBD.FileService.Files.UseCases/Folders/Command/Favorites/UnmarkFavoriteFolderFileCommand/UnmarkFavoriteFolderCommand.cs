using MediatR;
using MNX.Application.UseCases.Results;

namespace OBD.FileService.Files.UseCases.Folders.Command.Favorites.UnmarkFavoriteFolderFileCommand;

public record UnmarkFavoriteFolderCommand(Guid FolderId, long UserId) : IRequest<Result<Unit>>;