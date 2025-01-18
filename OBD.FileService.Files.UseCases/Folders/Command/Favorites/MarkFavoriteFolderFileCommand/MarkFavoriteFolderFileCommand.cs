using MediatR;

using MNX.Application.UseCases.Results;

namespace OBD.FileService.Files.UseCases.Folders.Command.Favorites.MarkFavoriteFolderFileCommand;

public record MarkFavoriteFolderFileCommand(Guid FolderId, long UserId) : IRequest<Result<Unit>>;
