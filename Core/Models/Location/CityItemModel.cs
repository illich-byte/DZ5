namespace Core.Models.Location;

/// <summary>
/// Модель для відображення детальної інформації про місто у списку або таблиці.
/// </summary>
public class CityItemModel
{
    /// <summary>
    /// Унікальний ідентифікатор міста.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Назва міста.
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Унікальний ідентифікатор міста для URL (slug).
    /// </summary>
    public string Slug { get; set; } = null!;

    /// <summary>
    /// Короткий опис міста.
    /// </summary>
    public string Description { get; set; } = null!;

    /// <summary>
    /// Назва країни, до якої належить місто (буде заповнено через AutoMapper).
    /// </summary>
    public string CountryName { get; set; } = null!;

    /// <summary>
    /// Посилання на зображення міста.
    /// </summary>
    public string ImageLink { get; set; } = null!;
}