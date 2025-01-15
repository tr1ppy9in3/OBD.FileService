using MediatR;

using OBD.FileService.Files.Core;

namespace OBD.FileService.Files.UseCases.Folders.Queries.GetFavoritesFoldersQuery;

public record GetFavoritesFoldersQuery(long UserId) : IStreamRequest<Folder>;
