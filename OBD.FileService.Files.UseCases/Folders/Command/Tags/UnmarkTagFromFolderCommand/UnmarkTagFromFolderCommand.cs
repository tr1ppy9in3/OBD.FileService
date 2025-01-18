using MediatR;

using MNX.Application.UseCases.Results;

namespace OBD.FileService.Files.UseCases.Folders.Command.Tags.UnmarkTagFromFolderCommand;

public record UnmarkTagFromFolderCommand(Guid FolderId, Guid TagId, long UserId) : IRequest<Result<Unit>>;
