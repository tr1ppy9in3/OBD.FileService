using MediatR;

using MNX.Application.UseCases.Results;

using OBD.FileService.Files.Core;
using OBD.FileService.Files.UseCases.Files;

namespace OBD.FileService.Files.UseCases.Folders.Command.CreateFolderCommand;

public record CreateFolderCommand(FolderInputModel Model, long UserId) : IRequest<Result<Folder>>;