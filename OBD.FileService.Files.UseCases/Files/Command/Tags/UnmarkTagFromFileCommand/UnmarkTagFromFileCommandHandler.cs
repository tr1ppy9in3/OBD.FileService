using MediatR;

using MNX.Application.UseCases.Results;

using OBD.FileService.Files.Core;
using OBD.FileService.Files.UseCases.Tags;

namespace OBD.FileService.Files.UseCases.Files.Command.Tags.UnmarkTagFromFileCommand;

public class UnmarkTagFromFileCommandHandler(IFileRepository fileRepository, ITagRepository tagRepository) : IRequestHandler<UnmarkTagFromFileCommand, Result<Unit>>
{
    private readonly IFileRepository _fileRepository = fileRepository
        ?? throw new ArgumentNullException(nameof(fileRepository));

    private readonly ITagRepository _tagRepository = tagRepository
        ?? throw new ArgumentNullException(nameof(tagRepository));

    public async Task<Result<Unit>> Handle(UnmarkTagFromFileCommand request, CancellationToken cancellationToken)
    {
        Tag? tag = await _tagRepository.GetByIdAvailable(request.TagId, request.UserId);
        if (tag is null)
            return Result<Unit>.Invalid($"Unable to find tag with Id {request.TagId}");

        Core.File? file = await _fileRepository.GetByIdAvailable(request.FileId, request.UserId);
        if (file is null)
            return Result<Unit>.Invalid($"Unable to find file with Id {request.FileId}");

        if (!file.Tags.Contains(tag))
            return Result<Unit>.Invalid($"Unable to find tag {request.TagId} in file with Id {request.FileId}");

        file.Tags.Remove(tag);
        await _fileRepository.Update(file);

        return Result<Unit>.Empty();
    }
}
