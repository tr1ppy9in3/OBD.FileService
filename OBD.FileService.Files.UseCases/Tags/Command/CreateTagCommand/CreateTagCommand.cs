using MediatR;
using MNX.Application.UseCases.Results;

using OBD.FileService.Files.Core;

namespace OBD.FileService.Files.UseCases.Tags.Command.CreateTagCommand;

public record CreateTagCommand(TagModel Model, long UserId) : IRequest<Result<Tag>>;
