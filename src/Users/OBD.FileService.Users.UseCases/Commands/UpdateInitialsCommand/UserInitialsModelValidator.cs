﻿using FluentValidation;

namespace OBD.FileService.Users.UseCases.Commands.UpdateInitialsCommand;

public class UserInitialsModelValidator : AbstractValidator<UserInitialsModel>
{
    public UserInitialsModelValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(50).WithMessage("Name must be less than 50 characters.");

        RuleFor(x => x.Surname)
            .NotEmpty().WithMessage("Surname is required.")
            .MaximumLength(50).WithMessage("Surname must be less than 50 characters.");
    }
}
