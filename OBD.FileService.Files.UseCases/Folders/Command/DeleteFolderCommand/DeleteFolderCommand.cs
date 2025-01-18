using MediatR;

using MNX.Application.UseCases.Results;

namespace OBD.FileService.Files.UseCases.Folders.Command.DeleteFolderCommand;

public record DeleteFolderCommand(Guid FolderId, long UserId) : IRequest<Result<Unit>>;