using System.ComponentModel.DataAnnotations;

namespace Core.Models.Location;

/// <summary>
/// Модель для оновлення існуючого міста.
/// Використовується для вхідних даних від клієнта (PUT-запит).
/// </summary>
public class CityUpdateModel
{
    /// <summary>
    /// Ідентифікатор міста, яке оновлюється.
    /// </summary>
    [Required]
    public int Id { get; set; }

    /// <summary>
    /// Назва міста.
    /// </summary>
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Name { get; set; } = null!;

    /// <summary>
    /// Унікальний ідентифікатор міста для URL (slug).
    /// </summary>
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Slug { get; set; } = null!;

    /// <summary>
    /// Опис міста.
    /// </summary>
    [StringLength(5000)]
    public string Description { get; set; } = null!;

    /// <summary>
    /// Ідентифікатор країни, до якої належить місто.
    /// </summary>
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Будь ласка, оберіть дійсну країну.")]
    public int CountryId { get; set; }

    /// <summary>
    /// Посилання на зображення міста (URL).
    /// </summary>
    [StringLength(500)]
    public string ImageLink { get; set; } = null!;
}