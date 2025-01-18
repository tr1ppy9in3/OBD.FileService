using MediatR;
using MNX.Application.UseCases.Results;

using OBD.FileService.Files.Core;
using OBD.FileService.Files.UseCases.Files;
using OBD.FileService.Files.UseCases.Tags.Abstractions;

namespace OBD.FileService.Files.UseCases.Folders.Command.Tags.MarkTagToFolderFileCommand;

public class MarkTagToFolderCommandHandler(IFolderRepository folderRepository, ITagRepository tagRepository) : IRequestHandler<MarkTagToFolderCommand, Result<Unit>>
{
    private readonly IFolderRepository _folderRepository = folderRepository
        ?? throw new ArgumentNullException(nameof(folderRepository));

    private readonly ITagRepository _tagRepository = tagRepository
        ?? throw new ArgumentNullException(nameof(tagRepository));

    public async Task<Result<Unit>> Handle(MarkTagToFolderCommand request, CancellationToken cancellationToken)
    {
        Tag? tag = await _tagRepository.GetByIdAvailable(request.TagId, request.UserId);
        if (tag is null)
            return Result<Unit>.Invalid($"Unable to find tag with Id {request.TagId}");

        Folder? folder = await _folderRepository.GetByIdAvailable(request.FolderId, request.UserId);
        if (folder is null)
            return Result<Unit>.Invalid($"Unable to find folder with Id {request.FolderId}");

        folder.Tags.Add(tag);
        await _folderRepository.Update(folder);

        return Result<Unit>.Empty();
    }
}
