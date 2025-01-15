using MediatR;
using FluentValidation;

using MNX.Application.UseCases.Results;
using OBD.FileService.Users.UseCases.Validators;

namespace OBD.FileService.Users.UseCases.Commands.SelfDeleteCommand;

/// <summary>
/// Команда удаления пользователя.
/// </summary>
/// <param name="Id"> Идентификатор пользователя. </param>
public sealed record class SelfDeleteCommand(long Id) : IRequest<Result<Unit>>;

/// <summary>
/// Валидатор команды удаления пользователя.
/// </summary>
public class SelfDeleteCommandValidator : AbstractValidator<SelfDeleteCommand>
{
    public SelfDeleteCommandValidator(IUserRepository userRepository)
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required!")
            .SetValidator(new UserExistsValidator(userRepository));
    }
}
