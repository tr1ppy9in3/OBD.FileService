using MediatR;

using Microsoft.AspNetCore.Mvc;

using MNX.Application.UseCases.Results;

using OBD.FileService.Files.Core;
using OBD.FileService.Files.UseCases.Files.Command.DownloadFileCommand;
using OBD.FileService.Files.UseCases.Files.Queries.GetFileByIdQuery;

using OBD.FileService.Files.UseCases.Folders.Command.DownloadFolderCommand;
using OBD.FileService.Files.UseCases.Folders.Queries.GetFolderByIdQuery;

namespace OBD.FileService.Files.UseCases.FileSystem.Commands.Object.DownloadObjectCommand;

public class DownloadObjectCommandHandler(IMediator mediator) : IRequestHandler<DownloadObjectCommand, Result<FileContentResult>>
{
    private readonly IMediator _mediator = mediator
        ?? throw new ArgumentNullException(nameof(mediator));
    public async Task<Result<FileContentResult>> Handle(DownloadObjectCommand request, CancellationToken cancellationToken)
    {
        var fileResult = await _mediator.Send(new GetFileByIdQuery(request.Id, request.UserId), cancellationToken);
        if (fileResult.IsSuccess)
            return await HandleFile(fileResult.GetValue());


        var folderResult = await _mediator.Send(new GetFolderByIdQuery(request.Id, request.UserId), cancellationToken);
        if (folderResult.IsSuccess)
            return await HandleFolder(folderResult.GetValue());

        return Result<FileContentResult>.Error("Unable to download file!");
    }

    public async Task<Result<FileContentResult>> HandleFolder(Folder folder)
    {
        var result = await _mediator.Send(new DownloadFolderCommand(folder.Id, folder.UserId));
        if (!result.IsSuccess)
            return Result<FileContentResult>.Error("Unable to download file!");

        var resultValue = result.GetValue();

        return Result<FileContentResult>.Success(new(resultValue.Content, resultValue.MimeType) { FileDownloadName = resultValue.FileName });
    }

    public async Task<Result<FileContentResult>> HandleFile(Core.File file)
    {
        var result = await _mediator.Send(new DownloadFileCommand(file.Id, file.UserId));
        if (!result.IsSuccess)
            return Result<FileContentResult>.Error("Unable to download file!");

        var resultValue = result.GetValue();

        return Result<FileContentResult>.Success(new(resultValue.Content, resultValue.MimeType) { FileDownloadName = resultValue.FileName });
    }
}