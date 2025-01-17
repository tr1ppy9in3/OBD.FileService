using MediatR;

namespace OBD.FileService.Files.UseCases.Files.Queries.GetAllAvailableFilesQuery;

public class GetAllAvailableFilesQueryHandler(IFileRepository fileRepository) : IStreamRequestHandler<GetAllAvailableFilesQuery, Core.File>
{
    private readonly IFileRepository _fileRepository = fileRepository
        ?? throw new ArgumentNullException(nameof(fileRepository));

    public IAsyncEnumerable<Core.File> Handle(GetAllAvailableFilesQuery request, CancellationToken cancellationToken)
    {
        return _fileRepository.GetAllAvailable(request.UserId);
    }
}
