using AutoMapper;

using MediatR;

using MNX.Application.UseCases.Results;

using OBD.FileService.Files.Core;
using OBD.FileService.Files.UseCases.Files.Queries.GetFileByIdQuery;
using OBD.FileService.Files.UseCases.FileSystem.Models;
using OBD.FileService.Files.UseCases.FileSystem.Models.Abstractions;
using OBD.FileService.Files.UseCases.Folders.Queries.GetFolderByIdQuery;

namespace OBD.FileService.Files.UseCases.FileSystem.Queries.GetContentQuery;

public class GetContentByIdQueryHandler(IMediator mediator, IMapper mapper) : IRequestHandler<GetContentByIdQuery, Result<BaseFileSystemObject>>
{
    private readonly IMediator _mediator = mediator
        ?? throw new ArgumentNullException(nameof(mediator));

    private readonly IMapper _mapper = mapper
        ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<Result<BaseFileSystemObject>> Handle(GetContentByIdQuery request, CancellationToken cancellationToken)
    {
        var fileResult = await _mediator.Send(new GetFileByIdQuery(request.Id, request.UserId), cancellationToken);
        if (fileResult.IsSuccess)
            return await HandleFile(fileResult.GetValue());
        

        var folderResult = await _mediator.Send(new GetFolderByIdQuery(request.Id, request.UserId), cancellationToken);
        if (folderResult.IsSuccess)
            return await HandleFolder(folderResult.GetValue());


        return Result<BaseFileSystemObject>.Invalid($"Unable to find object with Id {request.Id}");
    }


    public async Task<Result<BaseFileSystemObject>> HandleFolder(Folder folder)
    {
        return Result<BaseFileSystemObject>.Success(_mapper.Map<FolderModel>(folder));
    }

    public async Task<Result<BaseFileSystemObject>> HandleFile(Core.File file)
    {
        return Result<BaseFileSystemObject>.Success(_mapper.Map<FileModel>(file));
    }
}
