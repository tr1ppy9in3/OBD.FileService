using MediatR;

using MNX.Application.UseCases.Results;

using OBD.FileService.Files.Core;
using OBD.FileService.Files.UseCases.Files.Command.Favorite.MarkFavoriteFileCommand;
using OBD.FileService.Files.UseCases.Files.Command.Tags.MarkTagToFileCommand;
using OBD.FileService.Files.UseCases.Files.Queries.GetFileByIdQuery;
using OBD.FileService.Files.UseCases.Folders.Command.Favorites.MarkFavoriteFolderFileCommand;
using OBD.FileService.Files.UseCases.Folders.Command.Tags.MarkTagToFolderFileCommand;
using OBD.FileService.Files.UseCases.Folders.Queries.GetFolderByIdQuery;

namespace OBD.FileService.Files.UseCases.FileSystem.Commands.Tags.AddTagToObject;

public class AddTagToObjectCommandHandler(IMediator mediator) : IRequestHandler<AddTagToObjectCommand, Result<Unit>>
{
    private readonly IMediator _mediator = mediator
        ?? throw new ArgumentNullException(nameof(mediator));

    public async Task<Result<Unit>> Handle(AddTagToObjectCommand request, CancellationToken cancellationToken)
    {
        var fileResult = await _mediator.Send(new GetFileByIdQuery(request.ObjectId, request.UserId), cancellationToken);
        if (fileResult.IsSuccess)
            return await HandleFile(fileResult.GetValue(), request.TagId);


        var folderResult = await _mediator.Send(new GetFolderByIdQuery(request.ObjectId, request.UserId), cancellationToken);
        if (folderResult.IsSuccess)
            return await HandleFolder(folderResult.GetValue(), request.TagId);

        return Result<Unit>.Error("Unable to mark file as favorite!");
    }


         public Task<Result<Unit>> HandleFolder(Folder folder, Guid TagId)
            => _mediator.Send(new MarkTagToFolderCommand(folder.Id, TagId, folder.UserId));

        public Task<Result<Unit>> HandleFile(Core.File file, Guid TagId)
            => _mediator.Send(new MarkTagToFileCommand(file.UserId, TagId, file.Id));
}

