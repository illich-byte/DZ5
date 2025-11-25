using Domain.Entities.Location;
using Microsoft.EntityFrameworkCore;

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
    }
}