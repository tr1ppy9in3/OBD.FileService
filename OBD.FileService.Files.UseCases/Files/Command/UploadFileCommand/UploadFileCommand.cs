using FluentValidation;
using MediatR;

using MNX.Application.UseCases.CommandValidation;

namespace OBD.FileService.Files.UseCases.Files.Command.UploadFileCommand;

public record UploadFileCommand(long UserId, FileInputModel Model) : IValidatableCommand<Core.File>;

public class UploadFileCommandValidator: AbstractValidator<UploadFileCommand>
{
    public UploadFileCommandValidator()
    {
        RuleFor(x => x.Model)
            .NotNull().WithMessage("Модель обязательна!");
    }
}