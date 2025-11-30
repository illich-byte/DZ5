<<<<<<< HEAD
﻿using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Entities.Location
{
    /// <summary>
    /// Сутність, що представляє країну.
    /// Успадковує Id (int) від BaseEntity<int>.
    /// </summary>
    public class CountryEntity : BaseEntity<int>
    {
        /// <summary>
        /// Назва країни (наприклад, Україна).
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Дволітерний код країни (наприклад, UA).
        /// </summary>
        public string Code { get; set; } = null!;

        /// <summary>
        /// Унікальний ідентифікатор для URL (наприклад, ukraine).
        /// </summary>
        public string Slug { get; set; } = null!;

        /// <summary>
        /// Шлях до файлу зображення прапора.
        /// </summary>
        public string? Image { get; set; }

        // --- Навігаційні властивості ---

        /// <summary>
        /// Колекція міст, що належать цій країні.
        /// Ця властивість потрібна для зв'язку "один-до-багатьох" у Entity Framework Core.
        /// </summary>
        public virtual ICollection<CityEntity> Cities { get; set; } = new List<CityEntity>();
    }
}
=======
﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Location;

[Table("tblCountries")]
public class CountryEntity : BaseEntity<int>
{
    [StringLength(250)]
    public string Name { get; set; } = null!;
    [StringLength(10)]
    public string Code { get; set; } = null!;
    [StringLength(250)]
    public string Slug { get; set; } = null!;
    public string? Image { get; set; } 
}
>>>>>>> aea59e1b4ac8a1b0e26c6e93adb7a6774902ac26
