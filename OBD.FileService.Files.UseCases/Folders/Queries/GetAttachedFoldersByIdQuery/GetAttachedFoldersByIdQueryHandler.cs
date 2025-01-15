using MediatR;
using MNX.Application.UseCases.Results;
using OBD.FileService.Files.Core;

namespace OBD.FileService.Files.UseCases.Folders.Queries.GetAttachedFoldersByIdQuery;

public class GetAttachedFoldersByIdQueryHandler(IFolderRepository folderRepository) : IRequestHandler<GetAttachedFoldersByIdQuery, Result<List<Folder>>>
{
    private readonly IFolderRepository _folderRepository = folderRepository
        ?? throw new ArgumentNullException(nameof(folderRepository));

    public async Task<Result<List<Folder>>> Handle(GetAttachedFoldersByIdQuery request, CancellationToken cancellationToken)
    {
        Folder? folder = await _folderRepository.GetByIdAvailable(request.Id, request.UserId);
        if (folder is null)
            return Result<List<Folder>>.Invalid($"Unable to find folder with Id {request.Id}");

        return Result<List<Folder>>.Success(folder.AttachedFolders);
    }
}