using MediatR;

using OBD.FileService.Files.UseCases.FileSystem.Models.Abstractions;

namespace OBD.FileService.Files.UseCases.FileSystem.Queries.GetRootContentQuery;
public record GetRootContentQuery(long UserId) : IStreamRequest<BaseFileSystemObject>;