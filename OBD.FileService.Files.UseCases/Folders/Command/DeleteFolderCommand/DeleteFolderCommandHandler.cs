using MediatR;

using MNX.Application.UseCases.Results;

using OBD.FileService.Files.Core;

namespace OBD.FileService.Files.UseCases.Folders.Command.DeleteFolderCommand;

public class DeleteFolderCommandHandler(IFolderRepository folderRepository) : IRequestHandler<DeleteFolderCommand, Result<Unit>>
{
    private readonly IFolderRepository _folderRepository = folderRepository
        ?? throw new ArgumentNullException(nameof(folderRepository));

    public async Task<Result<Unit>> Handle(DeleteFolderCommand request, CancellationToken cancellationToken)
    {
        Folder? folder = await _folderRepository.GetByIdAvailable(request.FolderId, request.UserId);
        if (folder is null)
            return Result<Unit>.Invalid($"Unable to find folder with Id {request.FolderId}");

        await _folderRepository.Delete(folder);
        return Result<Unit>.Empty();
    }
}
