using AutoMapper;

using MediatR;

using OBD.FileService.Files.UseCases.Files.Queries.GetAllAvailableFilesQuery;
using OBD.FileService.Files.UseCases.FileSystem.Models;
using OBD.FileService.Files.UseCases.FileSystem.Models.Abstractions;
using OBD.FileService.Files.UseCases.Folders.Queries.GetAllAvailableFolderQuery;

using System.Runtime.CompilerServices;

namespace OBD.FileService.Files.UseCases.FileSystem.Queries.GetAllFavoritesObjects;

public class GetAllFavoritesObjectsQueryHandler(IMediator mediator, IMapper mapper) : IStreamRequestHandler<GetAllFavoritesObjectsQuery, BaseFileSystemObject>
{
    private readonly IMediator _mediator = mediator
       ?? throw new ArgumentNullException(nameof(mediator));

    private readonly IMapper _mapper = mapper
        ?? throw new ArgumentNullException(nameof(mapper));

    public async IAsyncEnumerable<BaseFileSystemObject> Handle(GetAllFavoritesObjectsQuery request, [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        await foreach (var file in _mediator.CreateStream(new GetAllAvailableFilesQuery(request.UserId), cancellationToken))
        {
            if (file.IsFavorite)
                yield return _mapper.Map<FileModel>(file);
        }

        await foreach (var folder in _mediator.CreateStream(new GetAllAvailableFoldersQuery(request.UserId), cancellationToken))
        {
            if (folder.IsFavorite)
                yield return _mapper.Map<FolderModel>(folder);
        }
    }
}
