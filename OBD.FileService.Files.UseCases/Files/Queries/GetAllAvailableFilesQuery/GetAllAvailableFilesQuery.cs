using MediatR;

namespace OBD.FileService.Files.UseCases.Files.Queries.GetAllAvailableFilesQuery;

public record GetAllAvailableFilesQuery(long UserId) : IStreamRequest<Core.File>;
