using AutoMapper;

using MediatR;

using MNX.Application.UseCases.Results;

using OBD.FileService.Files.UseCases.Files.Command.UploadFileCommand;
using OBD.FileService.Files.UseCases.FileSystem.Models;

using System.IO;

namespace OBD.FileService.Files.UseCases.FileSystem.Commands.Object.CreateObject.CreateFileCommand;

public class CreateFileObjectCommandHandler(IMediator mediator, IMapper mapper) : IRequestHandler<CreateFileObjectCommand, Result<FileModel>>
{
    private readonly IMediator _mediator = mediator
        ?? throw new ArgumentNullException(nameof(mediator));

    private readonly IMapper _mapper = mapper
        ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<Result<FileModel>> Handle(CreateFileObjectCommand request, CancellationToken cancellationToken)
    {
        var model = _mapper.Map<Files.FileInputModel>(request.Model);

        var result = await _mediator.Send(new UploadFileCommand(request.UserId, model), cancellationToken);
        if (result.IsSuccess)
            return Result<FileModel>.Success(_mapper.Map<FileModel>(result.GetValue()));
        else
            return Result<FileModel>.Error($"Unable to create file due to issues: {string.Join(" ", result.Errors!)} ");
    }
}
