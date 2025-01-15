using MediatR;
using MNX.Application.UseCases.Results;

using OBD.FileService.Files.Core;

namespace OBD.FileService.Files.UseCases.Folders.Queries.GetAttachedFilesByIdQuery;

public class GetAttachedFilesByIdQueryHandler(IFolderRepository folderRepository) : IRequestHandler<GetAttachedFilesByIdQuery, Result<List<Core.File>>>
{
    private readonly IFolderRepository _folderRepository = folderRepository
        ?? throw new ArgumentNullException(nameof(folderRepository));

    public async Task<Result<List<Core.File>>> Handle(GetAttachedFilesByIdQuery request, CancellationToken cancellationToken)
    {
        Folder? folder = await _folderRepository.GetByIdAvailable(request.Id, request.UserId);
        if (folder is null)
            return Result<List<Core.File>>.Invalid($"Unable to find folder with Id {request.Id}");

        return Result<List<Core.File>>.Success(folder.AttachedFiles);
    }
}