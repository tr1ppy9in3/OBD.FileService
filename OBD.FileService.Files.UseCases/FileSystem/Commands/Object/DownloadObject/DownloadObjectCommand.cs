using MediatR;

using Microsoft.AspNetCore.Mvc;

using MNX.Application.UseCases.Results;

namespace OBD.FileService.Files.UseCases.FileSystem.Commands.Object.DownloadObjectCommand;

public record DownloadObjectCommand(Guid Id, long UserId) : IRequest<Result<FileContentResult>>;
