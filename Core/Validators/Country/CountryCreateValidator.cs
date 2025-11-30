using Core.Models.Location;
using Domain;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
<<<<<<< HEAD
using Microsoft.AspNetCore.Http;
=======
>>>>>>> aea59e1b4ac8a1b0e26c6e93adb7a6774902ac26

namespace Core.Validators.Country;

public class CountryCreateValidator : AbstractValidator<CountryCreateModel>
<<<<<<< HEAD
{
=======
{ 
>>>>>>> aea59e1b4ac8a1b0e26c6e93adb7a6774902ac26
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

        RuleFor(x => x.Image)
<<<<<<< HEAD
            .NotNull().WithMessage("Файл зображення є обов'язковим")
            .Must(BeAValidFileSize).WithMessage("Розмір файлу не повинен перевищувати 5MB.")
            .Must(BeAValidFileType).WithMessage("Дозволені формати файлів: JPG, PNG, GIF.");
    }

    private bool BeAValidFileSize(IFormFile file)
    {
        return file.Length <= 5242880;
    }

    private bool BeAValidFileType(IFormFile file)
    {
        var allowedTypes = new[] { "image/jpeg", "image/png", "image/gif" };
        return allowedTypes.Contains(file.ContentType.ToLower());
    }
}
=======
            .NotEmpty().WithMessage("Файл зображення є обов'язковим");
    }
}
>>>>>>> aea59e1b4ac8a1b0e26c6e93adb7a6774902ac26
