
using AutoMapper;

using MediatR;

using MNX.Application.UseCases.Results;

using OBD.FileService.Files.Core;

namespace OBD.FileService.Files.UseCases.Folders.Command.CreateFolderCommand;

public class CreateFolderCommandHandler(IFolderRepository folderRepository, IMapper mapper) : IRequestHandler<CreateFolderCommand, Result<Folder>>
{
    private readonly IFolderRepository _folderRepository = folderRepository
        ?? throw new ArgumentNullException(nameof(folderRepository));

    private readonly IMapper _mapper = mapper 
        ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<Result<Folder>> Handle(CreateFolderCommand request, CancellationToken cancellationToken)
    {
        if (await _folderRepository.ExistByName(request.Model.Name, request.UserId))
            return Result<Folder>.Invalid("Folder with that name already exists!");

        Folder folder = _mapper.Map<Folder>(request.Model);
        folder.UserId = request.UserId;
        folder.ParentFolderId = request.Model.ParentFolderId;

        await _folderRepository.Create(folder);
        return Result<Folder>.SuccessfullyCreated(folder);
    }
}
