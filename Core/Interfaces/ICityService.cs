using Core.Models.Location;

namespace Core.Interfaces
{
    /// <summary>
    /// Інтерфейс сервісу для управління містами.
    /// </summary>
    public interface ICityService
    {
        /// <summary>
        /// Створює нове місто.
        /// </summary>
        /// <param name="model">Дані для створення міста.</param>
        /// <returns>Створена модель міста для відображення.</returns>
        Task<CityItemModel> CreateAsync(CityCreateModel model);

        /// <summary>
        /// Отримує список усіх міст.
        /// </summary>
        /// <returns>Список моделей міст.</returns>
        Task<List<CityItemModel>> GetListAsync();

        /// <summary>
        /// Отримує місто за його ID.
        /// </summary>
        /// <param name="id">Ідентифікатор міста.</param>
        /// <returns>Модель міста або null, якщо не знайдено.</returns>
        Task<CityItemModel?> GetByIdAsync(int id);

        /// <summary>
        /// Оновлює існуюче місто.
        /// </summary>
        /// <param name="id">Ідентифікатор міста.</param>
        /// <param name="model">Дані для оновлення міста.</param>
        Task UpdateAsync(int id, CityCreateModel model);

        /// <summary>
        /// Видаляє місто за його ID.
        /// </summary>
        /// <param name="id">Ідентифікатор міста.</param>
        Task DeleteAsync(int id);
    }
}