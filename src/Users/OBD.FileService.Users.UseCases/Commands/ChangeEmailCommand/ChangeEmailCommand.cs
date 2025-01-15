using MediatR;
using FluentValidation;

using MNX.Application.UseCases.Results;
using OBD.FileService.Users.UseCases.Validators;

namespace OBD.FileService.Users.UseCases.Commands.ChangeEmailCommand;

/// <summary>
/// Команда смены почты пользователя.
/// </summary>
/// <param name="userId"> Идентификатор пользователя. </param>
/// <param name="email"> Новая почта пользователя. </param>
public class ChangeEmailCommand(long userId, string email) : IRequest<Result<Unit>>
{
    /// <summary>
    /// Новый email.
    /// </summary>
    public string Email { get; set; } = email;

    /// <summary>
    /// Индетификатор пользователя, у которого меняется email.
    /// </summary>
    public long UserId { get; set; } = userId;
}

/// <summary>
/// Валидатор для команды смены почты пользователя.
/// </summary>
public class ChangeEmailCommandValidator : AbstractValidator<ChangeEmailCommand>
{
    public ChangeEmailCommandValidator(IUserRepository userRepository)
    {
        RuleFor(x => x.UserId)
            .NotNull().WithMessage("UserId is required!")
            .SetValidator(new UserExistsValidator(userRepository));

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("A valid email is required.")
            .When(x => !string.IsNullOrEmpty(x.Email)).WithMessage("Email should be a valid email address.");
    }
}
