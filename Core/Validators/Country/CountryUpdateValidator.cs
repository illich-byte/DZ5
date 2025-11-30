using Core.Models.Location;
using Domain;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace Core.Validators.Country;

public class CountryUpdateValidator : AbstractValidator<CountryUpdateModel>
{
    public CountryUpdateValidator(AppDbTransferContext db)
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("ID країни є обов'язковим для оновлення.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Назва країни не може бути порожньою")
            .MaximumLength(100).WithMessage("Назва країни не може перевищувати 100 символів")
            .DependentRules(() =>
            {
                RuleFor(x => x.Name)
                    .MustAsync(async (model, name, cancellation) =>
                        !await db.Countries
                            .AnyAsync(c => c.Id != model.Id && c.Name.ToLower() == name.ToLower().Trim(), cancellation))
                    .WithMessage("Країна з такою назвою вже існує");
            });

        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Код країни не може бути порожнім")
            .MaximumLength(10).WithMessage("Код країни не може перевищувати 10 символів");

        RuleFor(x => x.Slug)
            .NotEmpty().WithMessage("Slug країни не може бути порожнім")
            .MaximumLength(100).WithMessage("Slug країни не може перевищувати 100 символів");

        When(x => x.Image != null, () =>
        {
            RuleFor(x => x.Image!)
                .Must(BeAValidFileSize).WithMessage("Розмір файлу не повинен перевищувати 5MB.")
                .Must(BeAValidFileType).WithMessage("Дозволені формати файлів: JPG, PNG, GIF.");
        });
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