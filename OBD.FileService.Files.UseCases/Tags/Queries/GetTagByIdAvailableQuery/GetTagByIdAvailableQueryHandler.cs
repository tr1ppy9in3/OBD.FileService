using MediatR;

using MNX.Application.UseCases.Results;

using OBD.FileService.Files.Core;

namespace OBD.FileService.Files.UseCases.Tags.Queries.GetTagByIdAvailableQuery;

public class GetTagByIdAvailableQueryHandler(ITagRepository tagRepository) : IRequestHandler<GetTagByIdAvailableQuery, Result<Tag>>
{
    private readonly ITagRepository _tagRepository = tagRepository
        ?? throw new ArgumentNullException(nameof(tagRepository));

    public async Task<Result<Tag>> Handle(GetTagByIdAvailableQuery request, CancellationToken cancellationToken)
    {
        Tag? tag = await _tagRepository.GetByIdAvailable(request.TagId, request.UserId);
        if (tag is null)
            return Result<Tag>.Invalid($"Unable to find tag with Id {request.TagId} available to user!");

        return Result<Tag>.Success(tag);
    }
}
