using MediatR;
using AutoMapper;
using MNX.Application.UseCases.Results;

using OBD.FileService.Files.Core;
using OBD.FileService.Files.UseCases.Tags.Abstractions;
using OBD.FileService.Files.UseCases.Tags.Models;

namespace OBD.FileService.Files.UseCases.Tags.Command.CreateTagCommand;

public class CreateTagCommandHandler(ITagRepository tagRepository, IMapper mapper) : IRequestHandler<CreateTagCommand, Result<TagOutputModel>>
{
    private readonly ITagRepository _tagRepository = tagRepository
        ?? throw new ArgumentNullException(nameof(tagRepository));

    private readonly IMapper _mapper = mapper
        ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<Result<TagOutputModel>> Handle(CreateTagCommand request, CancellationToken cancellationToken)
    {
        if (await _tagRepository.ExistsByName(request.Model.Name, request.UserId))
            return Result<TagOutputModel>.Invalid($"Tag with name {request.Model.Name} already exists!");

        var tag = _mapper.Map<Tag>(request.Model);
        tag.UserId = request.UserId;

        await _tagRepository.Create(tag);
        return Result<TagOutputModel>.SuccessfullyCreated(_mapper.Map<TagOutputModel>(tag));
    }
}
