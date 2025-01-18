using MediatR;

using MNX.Application.UseCases.Results;

using OBD.FileService.Files.Core;
using OBD.FileService.Files.UseCases.Tags.Models;

namespace OBD.FileService.Files.UseCases.Tags.Command.CreateTagCommand;

public record CreateTagCommand(TagInputModel Model, long UserId) : IRequest<Result<TagOutputModel>>;
