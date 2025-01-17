using MediatR;

using MNX.Application.UseCases.Results;

namespace OBD.FileService.Files.UseCases.Files.Command.Tags.MarkTagToFileCommand;

public record MarkTagToFileCommand(long UserId, Guid TagId, Guid FileId) : IRequest<Result<Unit>>;
