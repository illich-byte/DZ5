namespace Core.Models.Location
{
    /// <summary>
    /// Модель даних для елемента випадаючого списку країн.
    /// </summary>
    public class CountryDropdownModel
    {
        /// <summary>
        /// Ідентифікатор країни.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Назва країни.
        /// </summary>
        public string Name { get; set; } = null!;
    }
}