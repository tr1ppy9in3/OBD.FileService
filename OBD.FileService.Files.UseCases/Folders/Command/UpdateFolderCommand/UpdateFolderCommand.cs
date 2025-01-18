
using MediatR;
using MNX.Application.UseCases.Results;

namespace OBD.FileService.Files.UseCases.Folders.Command.UpdateFolderCommand;

public record UpdateFolderCommand(Guid FolderId, FolderInputModel model, long UserId) : IRequest<Result<Unit>>;
