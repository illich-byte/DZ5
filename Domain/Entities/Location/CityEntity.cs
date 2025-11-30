using Domain.Entities; // Припускаємо, що BaseEntity знаходиться тут

namespace Domain.Entities.Location
{
    /// <summary>
    /// Сутність, що представляє місто.
    /// Успадковує Id (int) від BaseEntity<int>.
    /// </summary>
    public class CityEntity : BaseEntity<int>
    {
        /// <summary>
        /// Назва міста (наприклад, Київ).
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Унікальний ідентифікатор для URL (наприклад, kyiv).
        /// </summary>
        public string Slug { get; set; } = string.Empty;

        /// <summary>
        /// Корокткий опис міста. Може бути null.
        /// </summary>
        public string? Description { get; set; }

        // --- Зв'язки ---

        /// <summary>
        /// Зовнішній ключ для зв'язку з CountryEntity.
        /// </summary>
        public int CountryId { get; set; }

        /// <summary>
        /// Навігаційна властивість до CountryEntity.
        /// </summary>
        public virtual CountryEntity Country { get; set; } = null!;
    }
}