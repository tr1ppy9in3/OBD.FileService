using MediatR;
using Microsoft.AspNetCore.Mvc;
using MNX.Application.UseCases.Results;

using OBD.FileService.Files.Core;
using OBD.FileService.Files.UseCases.Files.Command.Favorite.MarkFavoriteFileCommand;
using OBD.FileService.Files.UseCases.Files.Queries.GetFileByIdQuery;
using OBD.FileService.Files.UseCases.Folders.Command.Favorites.MarkFavoriteFolderFileCommand;
using OBD.FileService.Files.UseCases.Folders.Queries.GetFolderByIdQuery;

namespace OBD.FileService.Files.UseCases.FileSystem.Commands.Favorite.MarkObjectAsFavorite;

public class MarkObjectAsFavoriteCommandHandler(IMediator mediator) : IRequestHandler<MarkObjectAsFavoriteCommand, Result<Unit>>
{
    private readonly IMediator _mediator = mediator
        ?? throw new ArgumentNullException(nameof(mediator));

    public async Task<Result<Unit>> Handle(MarkObjectAsFavoriteCommand request, CancellationToken cancellationToken)
    {
        var fileResult = await _mediator.Send(new GetFileByIdQuery(request.ObjectId, request.UserId), cancellationToken);
        if (fileResult.IsSuccess)
            return await HandleFile(fileResult.GetValue());


        var folderResult = await _mediator.Send(new GetFolderByIdQuery(request.ObjectId, request.UserId), cancellationToken);
        if (folderResult.IsSuccess)
            return await HandleFolder(folderResult.GetValue());

        return Result<Unit>.Error("Unable to mark file as favorite!");
    }

    public Task<Result<Unit>> HandleFolder(Folder folder)
        => _mediator.Send(new MarkFavoriteFolderFileCommand(folder.Id, folder.UserId));

    public Task<Result<Unit>> HandleFile(Core.File file) 
        => _mediator.Send(new MarkFileAsFavoriteCommand(file.Id, file.UserId));
}
