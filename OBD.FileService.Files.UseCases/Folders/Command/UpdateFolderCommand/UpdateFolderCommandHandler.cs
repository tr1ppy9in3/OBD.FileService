using MediatR;

using MNX.Application.UseCases.Results;

using OBD.FileService.Files.Core;

namespace OBD.FileService.Files.UseCases.Folders.Command.UpdateFolderCommand;

public class UpdateFolderCommandHandler(IFolderRepository folderRepository) : IRequestHandler<UpdateFolderCommand, Result<Unit>>
{
    private readonly IFolderRepository _folderRepository = folderRepository
        ?? throw new ArgumentNullException(nameof(folderRepository));


    public async Task<Result<Unit>> Handle(UpdateFolderCommand request, CancellationToken cancellationToken)
    {
        var folder = await _folderRepository.GetByIdAvailable(request.FolderId, request.UserId);
        if(folder is null)
            return Result<Unit>.Invalid($"Unable to find folder with Id {request.FolderId}");

        if (await _folderRepository.ExistByName(folder.Name, request.UserId))
            return Result<Unit>.Invalid("Folder with that name already exists!");

        folder.Name = request.model.Name;
        await _folderRepository.Update(folder);

        return Result<Unit>.Empty();
    }
}
