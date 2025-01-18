using MediatR;
using OBD.FileService.Files.UseCases.FileSystem.Models.Abstractions;

namespace OBD.FileService.Files.UseCases.FileSystem.Queries.GetAllFavoritesObjects; 

public record GetAllFavoritesObjectsQuery(long UserId) : IStreamRequest<BaseFileSystemObject>;
