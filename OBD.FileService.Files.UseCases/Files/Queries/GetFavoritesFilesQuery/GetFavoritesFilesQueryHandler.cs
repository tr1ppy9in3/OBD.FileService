using MediatR;
using System.Runtime.CompilerServices;

namespace OBD.FileService.Files.UseCases.Files.Queries.GetFavoritesFilesQuery;

public class GetFavoritesFilesQueryHandler(IFileRepository fileRepository) : IStreamRequestHandler<GetFavoritesFilesQuery, Core.File>
{
    private readonly IFileRepository _fileRepository = fileRepository
        ?? throw new ArgumentNullException(nameof(fileRepository));

    public async IAsyncEnumerable<Core.File> Handle(GetFavoritesFilesQuery request, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        await foreach(Core.File file in _fileRepository.GetAllAvailable(request.UserId))
        {
            if (file.IsFavorite)
                yield return file;
        }
    }
}
