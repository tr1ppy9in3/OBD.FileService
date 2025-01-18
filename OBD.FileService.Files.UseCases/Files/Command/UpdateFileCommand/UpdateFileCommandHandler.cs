using MediatR;
using MNX.Application.UseCases.Results;

namespace OBD.FileService.Files.UseCases.Files.Command.UpdateFileCommand;

public class UpdateFileCommandHandler(IFileRepository fileRepository) : IRequestHandler<UpdateFileCommand, Result<Unit>>
{
    private readonly IFileRepository _fileRepository = fileRepository
        ?? throw new ArgumentNullException(nameof(fileRepository));

    public async Task<Result<Unit>> Handle(UpdateFileCommand request, CancellationToken cancellationToken)
    {
        Core.File? file = await _fileRepository.GetByIdAvailable(request.FileId, request.UserId);
        if (file is null)
            return Result<Unit>.Invalid($"Unable to find file with Id {request.FileId}");

        if (await _fileRepository.ExistsByName(request.Model.Name))
            return Result<Unit>.Invalid($"File with that name already exists!");

        file.Name = request.Model.Name;
        file.Description = request.Model.Description;

        await _fileRepository.Update(file);
        return Result<Unit>.Empty();
    }
}
