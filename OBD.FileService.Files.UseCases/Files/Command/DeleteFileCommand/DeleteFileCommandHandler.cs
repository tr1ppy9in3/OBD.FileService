using MediatR;

using MNX.Application.UseCases.Results;

namespace OBD.FileService.Files.UseCases.Files.Command.DeleteFileCommand;

public class DeleteFileCommandHandler(IFileRepository fileRepository) : IRequestHandler<DeleteFileCommand, Result<Unit>>
{
    private readonly IFileRepository _fileRepository = fileRepository
        ?? throw new ArgumentNullException(nameof(fileRepository));

    public async Task<Result<Unit>> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
    {
        Core.File? file = await _fileRepository.GetByIdAvailable(request.FileId, request.UserId);
        if (file is null)
            return Result<Unit>.Invalid($"Unable to find file with Id {request.FileId}");

        await _fileRepository.Delete(file);
        return Result<Unit>.Empty();
    }
}
