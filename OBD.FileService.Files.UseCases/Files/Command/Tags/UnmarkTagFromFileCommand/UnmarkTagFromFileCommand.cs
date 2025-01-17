using MediatR;

using MNX.Application.UseCases.Results;

namespace OBD.FileService.Files.UseCases.Files.Command.Tags.UnmarkTagFromFileCommand;

public record UnmarkTagFromFileCommand(Guid TagId, Guid FileId, long UserId) : IRequest<Result<Unit>>;
