using AutoMapper;

using MediatR;

using MNX.Application.UseCases.Results;

using OBD.FileService.Files.Core;
using OBD.FileService.Files.UseCases.Files.Command.UpdateFileCommand;
using OBD.FileService.Files.UseCases.Files.Queries.GetFileByIdQuery;

using OBD.FileService.Files.UseCases.Folders.Command.UpdateFolderCommand;
using OBD.FileService.Files.UseCases.Folders.Queries.GetFolderByIdQuery;

using System.Reflection;

namespace OBD.FileService.Files.UseCases.FileSystem.Commands.Object.UpdateObject;

public class UpdateObjectCommandHandler(IMediator mediator, IMapper mapper) : IRequestHandler<UpdateObjectCommand, Result<Unit>>
{
    private readonly IMediator _mediator = mediator
        ?? throw new ArgumentNullException(nameof(mediator));

    private readonly IMapper _mapper = mapper
        ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<Result<Unit>> Handle(UpdateObjectCommand request, CancellationToken cancellationToken)
    {
        var fileResult = await _mediator.Send(new GetFileByIdQuery(request.ObjectId, request.UserId), cancellationToken);
        if (fileResult.IsSuccess)
            return await HandleFile(fileResult.GetValue(), (Models.FileInputModel)request.Model );


        var folderResult = await _mediator.Send(new GetFolderByIdQuery(request.ObjectId, request.UserId), cancellationToken);
        if (folderResult.IsSuccess)
            return await HandleFolder(folderResult.GetValue(), (Models.FolderInputModel)request.Model);

        return Result<Unit>.Error("Unable to mark file as favorite!");
    }

    public async Task<Result<Unit>> HandleFolder(Folder folder, Models.FolderInputModel model)
    {
        var mapped = _mapper.Map<Folders.FolderInputModel>(model);
        return await _mediator.Send(new UpdateFolderCommand(folder.Id, mapped, folder.UserId));
    }

    public async Task<Result<Unit>> HandleFile(Core.File file, Models.FileInputModel model)
    {
        var mapped = _mapper.Map<UpdateFileCommandModel>(model);
        return await _mediator.Send(new UpdateFileCommand(file.Id, mapped, file.UserId));

    }
}
