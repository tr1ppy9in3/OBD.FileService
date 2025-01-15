﻿using MediatR;
using FluentValidation;

using MNX.Application.UseCases.CommandValidation;

namespace OBD.FileService.Users.UseCases.Auth.Commands.LogoutCommand;

/// <summary>
/// Команда деавторизации.
/// </summary>
/// <param name="Token"></param>
public record class LogoutCommand(string Token) : IValidatableCommand<Unit>;

/// <summary>
/// Валидатор команды деавторизации.
/// </summary>
public class LogoutCommandValidator : AbstractValidator<LogoutCommand>
{
    public LogoutCommandValidator()
    {
        RuleFor(x => x.Token)
            .NotEmpty().WithMessage("Token is required!");
    }
}

