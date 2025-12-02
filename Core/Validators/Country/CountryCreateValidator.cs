using Core.Models.Location;
using Domain;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http; // Додано для IFormFile

namespace Core.Validators.Country;

public class CountryCreateValidator : AbstractValidator<CountryCreateModel>
{
    private const long MaxFileSizeInBytes = 5 * 1024 * 1024; // 5MB

    public CountryCreateValidator(AppDbTransferContext db)
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Назва країни не може бути порожньою")
            .MaximumLength(100).WithMessage("Назва країни не може перевищувати 100 символів")
            .DependentRules(() =>
            {
                RuleFor(x => x.Name)
                    .MustAsync(async (name, cancellation) =>
                        !await db.Countries.AnyAsync(c => c.Name.ToLower() == name.ToLower().Trim(), cancellation))
                    .WithMessage("Країна з такою назвою вже існує");
            });

        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Код країни не може бути порожнім")
            .MaximumLength(10).WithMessage("Код країни не може перевищувати 10 символів");

        RuleFor(x => x.Slug)
            .NotEmpty().WithMessage("Slug країни не може бути порожнім")
            .MaximumLength(100).WithMessage("Slug країни не може перевищувати 100 символів");

        // Розширена валідація для зображення (IFormFile)
        RuleFor(x => x.Image)
            .NotNull().WithMessage("Файл зображення є обов'язковим")
            .Must(BeAValidFileSize).WithMessage("Розмір файлу не повинен перевищувати 5MB.")
            .Must(BeAValidFileType).WithMessage("Дозволені формати файлів: JPG, PNG, GIF.");
    }

    /// <summary>
    /// Перевіряє, чи розмір файлу не перевищує 5MB.
    /// </summary>
    private bool BeAValidFileSize(IFormFile file)
    {
        return file != null && file.Length <= MaxFileSizeInBytes;
    }

    /// <summary>
    /// Перевіряє, чи тип контенту файлу відповідає дозволеним форматам (JPG, PNG, GIF).
    /// </summary>
    private bool BeAValidFileType(IFormFile file)
    {
        if (file == null) return false;

        var allowedTypes = new[] { "image/jpeg", "image/png", "image/gif" };
        return allowedTypes.Contains(file.ContentType.ToLower());
    }
}