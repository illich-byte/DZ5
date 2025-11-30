using System.ComponentModel.DataAnnotations;

namespace Core.Models.Location;

/// <summary>
/// Модель для створення нового міста.
/// Використовується для вхідних даних від клієнта (POST-запит).
/// </summary>
public class CityCreateModel
{
    /// <summary>
    /// Назва міста.
    /// </summary>
    [Required(ErrorMessage = "Назва міста є обов'язковою.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Назва має бути від 2 до 100 символів.")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Унікальний ідентифікатор міста для URL (slug).
    /// </summary>
    [Required(ErrorMessage = "Slug є обов'язковим.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Slug має бути від 2 до 100 символів.")]
    public string Slug { get; set; } = string.Empty;

    /// <summary>
    /// Ідентифікатор країни, до якої належить місто.
    /// </summary>
    [Required(ErrorMessage = "ID країни є обов'язковим.")]
    [Range(1, int.MaxValue, ErrorMessage = "Будь ласка, оберіть дійсну країну.")]
    public int CountryId { get; set; }

    /// <summary>
    /// Опис міста.
    /// </summary>
    [StringLength(5000, ErrorMessage = "Опис не може перевищувати 5000 символів.")]
    public string? Description { get; set; }

    /// <summary>
    /// Посилання на зображення міста (URL).
    /// </summary>
    [StringLength(500, ErrorMessage = "Посилання на зображення не може перевищувати 500 символів.")]
    public string? ImageLink { get; set; }
}