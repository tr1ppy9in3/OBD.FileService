﻿using MediatR;

using OBD.FileService.Files.Core;

namespace OBD.FileService.Files.UseCases.Folders.Queries.GetAllAvailableFolderQuery;

public class GetAllAvailableFolderQueryHandler(IFolderRepository folderRepository) : IStreamRequestHandler<GetAllAvailableFolderQuery, Folder>
{
    private readonly IFolderRepository _folderRepository = folderRepository
        ?? throw new ArgumentNullException(nameof(folderRepository));

    public IAsyncEnumerable<Folder> Handle(GetAllAvailableFolderQuery request, CancellationToken cancellationToken)
    {
        return _folderRepository.GetAllAvailable(request.UserId);
    }
}
