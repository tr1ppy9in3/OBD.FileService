using MediatR;
using MNX.Application.UseCases.Results;

namespace OBD.FileService.Files.UseCases.Files.Queries.GetFileByIdQuery;

public class GetFileByIdQueryHandler(IFileRepository fileRepository): IRequestHandler<GetFileByIdQuery, Result<Core.File>>
{
    private readonly IFileRepository _fileRepository = fileRepository
        ?? throw new ArgumentNullException(nameof(fileRepository)); 

    public async Task<Result<Core.File>> Handle(GetFileByIdQuery request, CancellationToken cancellationToken)
    {
        Core.File? file = await _fileRepository.GetByIdAvailable(request.FileId, request.UserId);
        if (file is null)
            return Result<Core.File>.Invalid($"Unable to find file with Id {request.FileId} is available to user!");

        return Result<Core.File>.Success(file);
     }
}
