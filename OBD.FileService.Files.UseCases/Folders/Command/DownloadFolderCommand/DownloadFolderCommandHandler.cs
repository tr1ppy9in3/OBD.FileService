using MediatR;

using MNX.Application.UseCases.Results;

using OBD.FileService.Files.Core;

namespace OBD.FileService.Files.UseCases.Folders.Command.DownloadFolderCommand;

public class DownloadFolderCommandHandler(IFolderRepository folderRepository) : IRequestHandler<DownloadFolderCommand, Result<DownloadFolderCommandResult>>
{
    private readonly IFolderRepository _folderRepository = folderRepository
        ?? throw new ArgumentNullException(nameof(folderRepository));

    public async Task<Result<DownloadFolderCommandResult>> Handle(DownloadFolderCommand request, CancellationToken cancellationToken)
    {

        Folder? folder = await _folderRepository.GetByIdAvailable(request.FileId, request.UserId);
        if (folder is null)
            return Result<DownloadFolderCommandResult>.Invalid($"Unable to find folder with Id {request.FileId}");

        var result = ZipCreator.CreateZipFromFolderModel(folder);

        return Result<DownloadFolderCommandResult>.Success(new DownloadFolderCommandResult() 
        {
            Content = result,
            FileName = $"{folder.Name}.zip",
        });
    }
}
 