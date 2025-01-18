using MediatR;

using MNX.Application.UseCases.Results;

using OBD.FileService.Files.Core;
using OBD.FileService.Files.UseCases.Tags.Abstractions;

namespace OBD.FileService.Files.UseCases.Tags.Command.DeleteTagCommand;

public class DeleteTagCommandHandler(ITagRepository tagRepository) : IRequestHandler<DeleteTagCommand, Result<Unit>>
{
    private readonly ITagRepository _tagRepository = tagRepository
        ?? throw new ArgumentNullException(nameof(tagRepository));

    public async Task<Result<Unit>> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
    {
        Tag? tag = await _tagRepository.GetByIdAvailable(request.TagId, request.UserId);
        if (tag is null)
            return Result<Unit>.Invalid($"Unable to find tag with Id {request.TagId}");

        await _tagRepository.Delete(tag);
        return Result<Unit>.Empty();
    }
}
