using MediatR;
using MNX.Application.UseCases.Results;
using OBD.FileService.Files.Core;

namespace OBD.FileService.Files.UseCases.Folders.Command.Favorites.MarkFavoriteFolderFileCommand;

public class MarkFavoriteFolderFileCommandHandler(IFolderRepository folderRepository) : IRequestHandler<MarkFavoriteFolderFileCommand, Result<Unit>>
{
    private readonly IFolderRepository _folderRepository = folderRepository
        ?? throw new ArgumentNullException(nameof(folderRepository));

    public async Task<Result<Unit>> Handle(MarkFavoriteFolderFileCommand request, CancellationToken cancellationToken)
    {
        Folder? folder = await _folderRepository.GetByIdAvailable(request.FolderId, request.UserId);
        if (folder is null)
            return Result<Unit>.Invalid($"Unable to find folder with Id {request.FolderId}");

        if (folder.IsFavorite)
            return Result<Unit>.Empty();

        folder.IsFavorite = true;
        await _folderRepository.Update(folder);

        return Result<Unit>.Empty();
    }
}
