using Domain.Entities.Location;
using Microsoft.EntityFrameworkCore;

<<<<<<< HEAD
namespace Domain
{
    /// <summary>
    /// Контекст бази даних для застосунку WebApiTransfer.
    /// </summary>
    public class AppDbTransferContext : DbContext
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
            base.OnModelCreating(modelBuilder);

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

                // Додавання міст, пов'язаних з країною
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

            // Запобігання видаленню, якщо є пов'язані об'єкти
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
=======
namespace Domain;

public class AppDbTransferContext : DbContext
{
    public AppDbTransferContext(DbContextOptions<AppDbTransferContext> options)
        : base(options)
    {
    }

    public DbSet<CountryEntity> Countries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
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
>>>>>>> aea59e1b4ac8a1b0e26c6e93adb7a6774902ac26
    }
}