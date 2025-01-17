using MediatR;

using OBD.FileService.Files.Core;

namespace OBD.FileService.Files.UseCases.Tags.Queries.GetAllTagsAvailableQuery;

public class GetAllTagsAvailableQueryHandler(ITagRepository tagRepository) : IStreamRequestHandler<GetAllTagsAvailableQuery, Tag>
{
    private readonly ITagRepository _tagRepository = tagRepository
        ?? throw new ArgumentNullException(nameof(tagRepository));

    public IAsyncEnumerable<Tag> Handle(GetAllTagsAvailableQuery request, CancellationToken cancellationToken)
    {
        return 
        _tagRepository.GetAllAvailable(request.UserId);
    }
}
