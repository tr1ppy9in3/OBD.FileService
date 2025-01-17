using MediatR;
using MNX.Application.UseCases.Results;

namespace OBD.FileService.Files.UseCases.Files.Command.Favorite.UnmarkFileAsFavoriteCommand;

public class UnmarkFileAsFavoriteCommandHandler(IFileRepository fileRepository) : IRequestHandler<UnmarkFileAsFavoriteCommand, Result<Unit>>
{
    private readonly IFileRepository _fileRepository = fileRepository
        ?? throw new ArgumentNullException(nameof(fileRepository));

    public async Task<Result<Unit>> Handle(UnmarkFileAsFavoriteCommand request, CancellationToken cancellationToken)
    {
        Core.File? file = await _fileRepository.GetByIdAvailable(request.FileId, request.UserId);
        if (file is null)
            return Result<Unit>.Invalid($"Unable to find fild with Id {request.FileId}");

        if (!file.IsFavorite)
            return Result<Unit>.Empty();

        file.IsFavorite = false;
        await _fileRepository.Update(file);

        return Result<Unit>.Empty();
    }
}
