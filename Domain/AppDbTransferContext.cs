using Domain.Entities.Location;
using Microsoft.EntityFrameworkCore;

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
    }
}