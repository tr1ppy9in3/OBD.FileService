using AutoMapper;

using MediatR;
using MNX.Application.UseCases.Results;

namespace OBD.FileService.Files.UseCases.Files.Command.UploadFileCommand;

using File = Core.File;

internal class UploadFileCommandHandler(IMapper mapper, IFileRepository fileRepository) : IRequestHandler<UploadFileCommand, Result<File>>
{
    private readonly IMapper _mapper = mapper
        ?? throw new ArgumentNullException(nameof(mapper));

    private readonly IFileRepository _fileRepository = fileRepository
        ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<Result<File>> Handle(UploadFileCommand request, CancellationToken cancellationToken)
    {
        var file = _mapper.Map<File>(request.Model);
        if (await _fileRepository.Exists(file))
        {
            return Result<File>.Invalid("File already exists!");
        }
            
        file.UserId = request.UserId;
        file.FolderId = request.Model.ParentFolderId;

        await _fileRepository.Create(file);
        return Result<File>.SuccessfullyCreated(file);
    }
}
