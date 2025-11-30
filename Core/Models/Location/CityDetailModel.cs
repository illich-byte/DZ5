using System.ComponentModel.DataAnnotations;

namespace Core.Models.Location;

/// <summary>
/// Модель для відображення повної детальної інформації про місто.
/// Використовується як DTO для GET-запиту одного об'єкта.
/// </summary>
public class CityDetailModel
{
    /// <summary>
    /// Унікальний ідентифікатор міста.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Назва міста.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Унікальний ідентифікатор міста для URL (slug).
    /// </summary>
    public string Slug { get; set; } = string.Empty;

    /// <summary>
    /// Повний опис міста.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Посилання на головне зображення міста (URL).
    /// </summary>
    public string? ImageLink { get; set; }

    /// <summary>
    /// Ідентифікатор країни, до якої належить місто.
    /// </summary>
    public int CountryId { get; set; }

    /// <summary>
    /// Назва країни (для зручності відображення без додаткового запиту).
    /// </summary>
    public string CountryName { get; set; } = string.Empty;

    // Тут можна додати інші деталі, які необхідні для сторінки міста,
    // наприклад, кількість пам'яток, список районів, чи дату створення.

    /// <summary>
    /// Дата та час останнього оновлення запису.
    /// </summary>
    public DateTime UpdatedAt { get; set; }
}