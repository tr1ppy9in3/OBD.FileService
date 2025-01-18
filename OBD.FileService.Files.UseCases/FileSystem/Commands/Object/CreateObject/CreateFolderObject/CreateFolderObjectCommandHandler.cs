using AutoMapper;

using MediatR;

using MNX.Application.UseCases.Results;

using OBD.FileService.Files.UseCases.FileSystem.Models;
using OBD.FileService.Files.UseCases.Folders.Command.CreateFolderCommand;

namespace OBD.FileService.Files.UseCases.FileSystem.Commands.Object.CreateObject.CreateFolderObject;

public class CreateFolderObjectCommandHandler(IMediator mediator, IMapper mapper): IRequestHandler<CreateFolderObjectCommand, Result<FolderModel>>
{
    private readonly IMediator _mediator = mediator
        ?? throw new ArgumentNullException(nameof(mediator));

    private readonly IMapper _mapper = mapper
        ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<Result<FolderModel>> Handle(CreateFolderObjectCommand request, CancellationToken cancellationToken)
    {
        var model = _mapper.Map<Folders.FolderInputModel>(request.Model);

        var result = await _mediator.Send(new CreateFolderCommand(model, request.UserId), cancellationToken);
        if (result.IsSuccess)
            return Result<FolderModel>.Success(_mapper.Map<FolderModel>(result.GetValue()));
        else
            return Result<FolderModel>.Error($"Unable to create folder due to issues: {string.Join(" ", result.Errors!)} ");
    }
}
