using MediatR;
using MNX.Application.UseCases.Results;

using OBD.FileService.Files.UseCases.FileSystem.Models;

namespace OBD.FileService.Files.UseCases.FileSystem.Commands.Object.CreateObject.CreateFolderObject;

public record CreateFolderObjectCommand(CreateFolderObjectCommandModel Model, long UserId) : IRequest<Result<FolderModel>>;
