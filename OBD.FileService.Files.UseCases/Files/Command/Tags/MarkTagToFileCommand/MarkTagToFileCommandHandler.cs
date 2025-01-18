using MediatR;

using MNX.Application.UseCases.Results;

using OBD.FileService.Files.Core;
using OBD.FileService.Files.UseCases.Tags.Abstractions;

namespace OBD.FileService.Files.UseCases.Files.Command.Tags.MarkTagToFileCommand;

public class MarkTagToFileCommandHandler(ITagRepository tagRepository, IFileRepository fileRepository) : IRequestHandler<MarkTagToFileCommand, Result<Unit>>
{
    private readonly ITagRepository _tagRepository = tagRepository
        ?? throw new ArgumentNullException(nameof(tagRepository));

    private readonly IFileRepository _fileRepository = fileRepository
        ?? throw new ArgumentNullException(nameof(fileRepository));

    public async Task<Result<Unit>> Handle(MarkTagToFileCommand request, CancellationToken cancellationToken)
    {
        Tag? tag = await _tagRepository.GetByIdAvailable(request.TagId, request.UserId);
        if (tag is null)
            return Result<Unit>.Invalid($"Unable to find tag with Id {request.TagId}");

        Core.File? file = await _fileRepository.GetByIdAvailable(request.FileId, request.UserId);
        if (file is null)
            return Result<Unit>.Invalid($"Unable to find file with Id {request.FileId}");

        file.Tags.Add(tag);
        await _fileRepository.Update(file);

        return Result<Unit>.Empty();
    }
}
