using Core.Models.Location;
using Domain;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Core.Validators.City
{
    /// <summary>
    /// Валідатор для моделі CityCreateModel.
    /// Перевіряє обов'язковість, довжину, унікальність Slug та існування CountryId.
    /// </summary>
    public class CityCreateValidator : AbstractValidator<CityCreateModel>
    {
        private readonly AppDbTransferContext _context;

        public CityCreateValidator(AppDbTransferContext context)
        {
            _context = context;

            // 1. Перевірка обов'язкових полів та довжини
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Назва міста є обов'язковою.")
                .MaximumLength(100).WithMessage("Назва міста не може перевищувати 100 символів.");

            RuleFor(x => x.Slug)
                .NotEmpty().WithMessage("Slug є обов'язковим.")
                .MaximumLength(100).WithMessage("Slug не може перевищувати 100 символів.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Опис не може перевищувати 500 символів.");

            RuleFor(x => x.CountryId)
                .GreaterThan(0).WithMessage("CountryId є обов'язковим і має бути більше 0.");

            // 2. Асинхронна перевірка унікальності Slug
            RuleFor(x => x.Slug)
                .MustAsync(BeUniqueSlug).WithMessage("Місто з таким 'slug' вже існує.");

            // 3. Асинхронна перевірка існування CountryId
            RuleFor(x => x.CountryId)
                .MustAsync(CountryMustExist).WithMessage("Вказаний CountryId не існує.");
        }

        /// <summary>
        /// Перевіряє, чи є Slug унікальним серед усіх міст.
        /// </summary>
        private async Task<bool> BeUniqueSlug(string slug, CancellationToken token)
        {
            // Перевіряємо, чи існує будь-яке місто з таким же Slug
            return await _context.Cities.AllAsync(c => c.Slug != slug, token);
        }

        /// <summary>
        /// Перевіряє, чи існує країна з вказаним ID.
        /// </summary>
        private async Task<bool> CountryMustExist(int countryId, CancellationToken token)
        {
            // Перевіряємо, чи існує країна з вказаним ID
            return await _context.Countries.AnyAsync(c => c.Id == countryId, token);
        }
    }
}