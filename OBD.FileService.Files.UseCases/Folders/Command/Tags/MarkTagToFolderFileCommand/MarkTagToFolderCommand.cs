using MediatR;

using MNX.Application.UseCases.Results;

namespace OBD.FileService.Files.UseCases.Folders.Command.Tags.MarkTagToFolderFileCommand;

public record MarkTagToFolderCommand(Guid FolderId, Guid TagId, long UserId) : IRequest<Result<Unit>>;
