using MediatR;

using OBD.FileService.Files.Core;

namespace OBD.FileService.Files.UseCases.Folders.Queries.GetAllAvailableFolderQuery;

public record class GetAllAvailableFoldersQuery(long UserId) : IStreamRequest<Folder>;
