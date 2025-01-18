using AutoMapper;

using MediatR;

using OBD.FileService.Files.Core;
using OBD.FileService.Files.UseCases.Tags.Abstractions;
using OBD.FileService.Files.UseCases.Tags.Models;

using System.Runtime.CompilerServices;

namespace OBD.FileService.Files.UseCases.Tags.Queries.GetAllTagsAvailableQuery;

public class GetAllTagsAvailableQueryHandler(ITagRepository tagRepository, IMapper mapper) : IStreamRequestHandler<GetAllTagsAvailableQuery, TagOutputModel>
{
    private readonly ITagRepository _tagRepository = tagRepository
        ?? throw new ArgumentNullException(nameof(tagRepository));
    
    private readonly IMapper _mapper = mapper
        ?? throw new ArgumentNullException(nameof(mapper)); 

    public async IAsyncEnumerable<TagOutputModel> Handle(GetAllTagsAvailableQuery request, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        await foreach (Tag tag in _tagRepository.GetAllAvailable(request.UserId))
        {
            yield return _mapper.Map<TagOutputModel>(tag);
        }
    }
}
