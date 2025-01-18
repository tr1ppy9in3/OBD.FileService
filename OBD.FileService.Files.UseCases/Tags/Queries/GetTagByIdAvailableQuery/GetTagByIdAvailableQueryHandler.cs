using AutoMapper;

using MediatR;

using MNX.Application.UseCases.Results;

using OBD.FileService.Files.Core;
using OBD.FileService.Files.UseCases.Tags.Abstractions;
using OBD.FileService.Files.UseCases.Tags.Models;

namespace OBD.FileService.Files.UseCases.Tags.Queries.GetTagByIdAvailableQuery;

public class GetTagByIdAvailableQueryHandler(ITagRepository tagRepository, IMapper mapper) : IRequestHandler<GetTagByIdAvailableQuery, Result<TagOutputModel>>
{
    private readonly ITagRepository _tagRepository = tagRepository
        ?? throw new ArgumentNullException(nameof(tagRepository));

    private readonly IMapper _mapper = mapper
        ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<Result<TagOutputModel>> Handle(GetTagByIdAvailableQuery request, CancellationToken cancellationToken)
    {
        Tag? tag = await _tagRepository.GetByIdAvailable(request.TagId, request.UserId);
        if (tag is null)
            return Result<TagOutputModel>.Invalid($"Unable to find tag with Id {request.TagId} available to user!");

        return Result<TagOutputModel>.Success(_mapper.Map<TagOutputModel>(tag));
    }
}
