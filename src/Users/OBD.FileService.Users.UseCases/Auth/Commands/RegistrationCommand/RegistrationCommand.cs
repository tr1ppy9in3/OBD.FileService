using MediatR;
using FluentValidation;

using MNX.Application.UseCases.CommandValidation;

namespace OBD.FileService.Users.UseCases.Auth.Commands.RegistrationCommand;

/// <summary>
/// Команда для регистрации пользователя.
/// </summary>
/// <param name="Login"> Логин пользователя. </param>
/// <param name="Password"> Пароль пользователя. </param>
/// <param name="Email"> Email пользователя. </param>
public sealed record class RegistrationCommand(string Login, string Password, string Email) : IValidatableCommand<Unit>;

/// <summary>
/// Валидатор команды для регистрации пользователя.
/// </summary>
public class RegistrationCommandValidator : AbstractValidator<RegistrationCommand>
{
    public RegistrationCommandValidator()
    {
        RuleFor(x => x.Login)
           .NotEmpty().NotNull().WithMessage("Логин обязателен.")
           .Length(3, 50).WithMessage("Логин должен содержать от 3 до 50 символов.")
           .Matches(@"^[a-zA-Z0-9_]+$").WithMessage("Логин может содержать только алфавитно-цифровые символы и символы подчеркивания.");

        RuleFor(x => x.Password)
           .NotEmpty().NotNull().WithMessage("Пароль обязателен.")
           .Length(8, 100).WithMessage("Пароль должен содержать от 8 до 100 символов.")
           .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d@$!%*?&]{8,}$")
           .WithMessage("Пароль должен содержать хотя бы одну заглавную букву, одну строчную букву и одну цифру.");

        RuleFor(x => x.Email)
            .NotEmpty().NotNull()
            .EmailAddress().WithMessage("A valid email is required.")
            .When(x => !string.IsNullOrEmpty(x.Email)).WithMessage("Email should be a valid email address.");
    }
}