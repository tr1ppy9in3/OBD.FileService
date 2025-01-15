using MediatR;
using System.Runtime.CompilerServices;
using MNX.Application.UseCases.Results;

using OBD.FileService.Files.Core;

namespace OBD.FileService.Files.UseCases.Folders.Queries.GetFavoritesFoldersQuery;

public class GetFavoritesFoldersQueryHandler(IFolderRepository folderRepository) : IStreamRequest<GetFavoritesFoldersQuery>
{
    private readonly IFolderRepository _folderRepository = folderRepository
        ?? throw new ArgumentNullException(nameof(folderRepository));

    public async IAsyncEnumerable<Folder> Handle(GetFavoritesFoldersQuery request, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        await foreach(Folder folder in _folderRepository.GetAllAvailable(request.UserId))
        {
            if (folder.IsFavorite)
                yield return folder;
        }
    }
}
