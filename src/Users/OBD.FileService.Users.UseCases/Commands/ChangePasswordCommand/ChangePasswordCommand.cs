using MediatR;
using FluentValidation;

using MNX.Application.UseCases.CommandValidation;

namespace OBD.FileService.Users.UseCases.Commands.ChangePasswordCommand;

/// <summary>
/// Команда смены пароля.
/// </summary>
/// <param name="UserId"> Иденификатор пользователя. </param>
/// <param name="Password"> Новый пароль. </param>
public record ChangePasswordCommand(long UserId, string Password) : IValidatableCommand<Unit>;

/// <summary>
/// Валидатор команды смены пароля.
/// </summary>
public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator()
    {
        RuleFor(x => x.Password)
           .NotEmpty().NotNull().WithMessage("Пароль обязателен.")
           .Length(8, 100).WithMessage("Пароль должен содержать от 8 до 100 символов.")
           .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d@$!%*?&]{8,}$")
           .WithMessage("Пароль должен содержать хотя бы одну заглавную букву, одну строчную букву и одну цифру.");
    }
}