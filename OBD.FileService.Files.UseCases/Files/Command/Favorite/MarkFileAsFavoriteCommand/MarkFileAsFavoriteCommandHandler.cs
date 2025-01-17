using MediatR;
using MNX.Application.UseCases.Results;

namespace OBD.FileService.Files.UseCases.Files.Command.Favorite.MarkFavoriteFileCommand;

public class MarkFileAsFavoriteCommandHandler(IFileRepository fileRepository) : IRequestHandler<MarkFileAsFavoriteCommand, Result<Unit>>
{
    private readonly IFileRepository _fileRepository = fileRepository
        ?? throw new ArgumentNullException(nameof(fileRepository));

    public async Task<Result<Unit>> Handle(MarkFileAsFavoriteCommand request, CancellationToken cancellationToken)
    {
        Core.File? file = await _fileRepository.GetByIdAvailable(request.FileId, request.UserId);
        if (file is null)
            return Result<Unit>.Invalid($"Unable to find fild with Id {request.FileId}");

        if (file.IsFavorite)
            return Result<Unit>.Empty();

        file.IsFavorite = true;
        await _fileRepository.Update(file);

        return Result<Unit>.Empty();
    }
}
