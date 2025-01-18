using FluentValidation;

using Microsoft.AspNetCore.Http;

namespace OBD.FileService.Files.UseCases.Files;

public class FileInputModel
{
    public required IFormFile Form { get; set; }

    public required Guid? ParentFolderId { get; set; }

    public string? Description { get; set; } = default;

}

public class FileInputModelValidator : AbstractValidator<FileInputModel>
{
    public FileInputModelValidator()
    {
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Описание не должно быть пустым, если указано.")
            .Length(1, 500).WithMessage("Описание должно содержать от 1 до 500 символов.");
    }
}
