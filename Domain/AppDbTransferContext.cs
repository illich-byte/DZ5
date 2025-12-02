using Domain.Entities; // Для AppUser
using Domain.Entities.Location;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore; // Використовуємо IdentityDbContext
using Microsoft.EntityFrameworkCore;

namespace Domain
{
    /// <summary>
    /// Контекст бази даних для застосунку.
    /// Успадковує від IdentityDbContext для підтримки ASP.NET Identity (AppUser, IdentityRole тощо).
    /// </summary>
    public class AppDbTransferContext : IdentityDbContext<AppUser>
    {
        public AppDbTransferContext(DbContextOptions<AppDbTransferContext> options)
            : base(options)
        {
        }

        // --- Географічні сутності ---

        /// <summary>
        /// Колекція для управління країнами.
        /// </summary>
        public DbSet<CountryEntity> Countries { get; set; } = null!;

        /// <summary>
        /// Колекція для управління містами.
        /// </summary>
        public DbSet<CityEntity> Cities { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Обов'язковий виклик базового методу IdentityDbContext, 
            // який налаштовує таблиці Identity (Users, Roles, UserRoles тощо).
            base.OnModelCreating(modelBuilder);

            // Налаштування сутності AppUser (визначення полів IdentityUser)
            // Таблиця AspNetUsers вже налаштована IdentityDbContext, 
            // але ми можемо змінити її назву, якщо потрібно.
            modelBuilder.Entity<AppUser>().ToTable("Users");


            // --- Налаштування сутностей Location ---

            // Налаштування сутності CountryEntity
            modelBuilder.Entity<CountryEntity>(entity =>
            {
                entity.ToTable("Countries");

                // Унікальний індекс для Code та Slug
                entity.HasIndex(e => e.Code).IsUnique();
                entity.HasIndex(e => e.Slug).IsUnique();

                entity.Property(e => e.Code).HasMaxLength(2);
                entity.Property(e => e.Name).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Slug).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Image).HasMaxLength(255); // Шлях до файлу

                // Додавання міст, пов'язаних з країною (ЗВ'ЯЗОК 1:М)
                entity.HasMany(e => e.Cities)
                      .WithOne(c => c.Country)
                      .HasForeignKey(c => c.CountryId)
                      .OnDelete(DeleteBehavior.Restrict); // Заборона видалення країни, якщо є міста
            });

            // Налаштування сутності CityEntity
            modelBuilder.Entity<CityEntity>(entity =>
            {
                entity.ToTable("Cities");

                // Унікальний індекс для Slug
                entity.HasIndex(e => e.Slug).IsUnique();

                entity.Property(e => e.Name).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Slug).HasMaxLength(100).IsRequired();
                entity.Property(e => e.Description).HasMaxLength(500);
            });

            // --- Початкове заповнення даних (Seed Data) ---

            modelBuilder.Entity<CountryEntity>().HasData(
                new CountryEntity
                {
                    Id = 1,
                    Name = "Україна",
                    Code = "UA",
                    Slug = "ukraine",
                    Image = "https://flagcdn.com/w320/ua.png"
                },
                new CountryEntity
                {
                    Id = 2,
                    Name = "Польща",
                    Code = "PL",
                    Slug = "poland",
                    Image = "https://flagcdn.com/w320/pl.png"
                },
                new CountryEntity
                {
                    Id = 3,
                    Name = "Німеччина",
                    Code = "DE",
                    Slug = "germany",
                    Image = "https://flagcdn.com/w320/de.png"
                },
                new CountryEntity
                {
                    Id = 4,
                    Name = "Чехія",
                    Code = "CZ",
                    Slug = "czech-republic",
                    Image = "https://flagcdn.com/w320/cz.png"
                }
            );

            // --- Загальне правило видалення (як було у HEAD) ---

            // Переконайтеся, що всі зовнішні ключі, які не були явно налаштовані, 
            // мають поведінку OnDelete(DeleteBehavior.Restrict)
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                // Заборона видалення
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}