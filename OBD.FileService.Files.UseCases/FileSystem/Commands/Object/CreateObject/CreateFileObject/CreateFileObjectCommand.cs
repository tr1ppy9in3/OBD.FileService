using MediatR;
using MNX.Application.UseCases.Results;

using OBD.FileService.Files.UseCases.FileSystem.Commands.Object.CreateObject.CreateFileObject;
using OBD.FileService.Files.UseCases.FileSystem.Models;

namespace OBD.FileService.Files.UseCases.FileSystem.Commands.Object.CreateObject.CreateFileCommand;

public record CreateFileObjectCommand(CreateFileObjectCommandModel Model, long UserId) : IRequest<Result<FileModel>>;
