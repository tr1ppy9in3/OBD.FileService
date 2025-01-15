using MediatR;
using MNX.Application.UseCases.Results;

using OBD.FileService.Files.Core;

namespace OBD.FileService.Files.UseCases.Folders.Queries.GetFolderByIdQuery;

public class GetFolderByIdQueryHandler(IFolderRepository folderRepository) : IRequestHandler<GetFolderByIdQuery, Result<Folder>>
{
    private readonly IFolderRepository _folderRepository = folderRepository
        ?? throw new ArgumentNullException(nameof(folderRepository));

    public async Task<Result<Folder>> Handle(GetFolderByIdQuery request, CancellationToken cancellationToken)
    {
        Folder? folder = await _folderRepository.GetByIdAvailable(request.Id, request.UserId);
        if (folder is null)
            return Result<Folder>.Invalid($"Unable to find folder with Id {request.Id}");

        return Result<Folder>.Success(folder);
    }
}
