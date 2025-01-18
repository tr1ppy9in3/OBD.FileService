using AutoMapper;

using MediatR;

using MNX.Application.UseCases.Results;

using OBD.FileService.Files.Core;
using OBD.FileService.Files.UseCases.Tags.Abstractions;

namespace OBD.FileService.Files.UseCases.Tags.Command.UpdateTagCommand;

public class UpdateTagCommandHandler(ITagRepository tagRepository, IMapper mapper) : IRequestHandler<UpdateTagCommand, Result<Unit>>
{
    private readonly ITagRepository _tagRepository = tagRepository
        ?? throw new ArgumentNullException(nameof(tagRepository));

    private readonly IMapper _mapper = mapper
        ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<Result<Unit>> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
    {
        if (!await _tagRepository.ExistsById(request.TagId, request.UserId))
            return Result<Unit>.Invalid($"Unable to find tag with Id {request.TagId}");

        Tag tag = _mapper.Map<Tag>(request.Model);
        
        tag.Id = request.TagId;
        tag.UserId = request.UserId;

        await _tagRepository.Update(tag);
        return Result<Unit>.Empty();
    }
}
