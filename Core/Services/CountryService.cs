using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Interfaces;
using Core.Models.Location;
using Domain;
using Domain.Entities.Location;
using Microsoft.EntityFrameworkCore;
<<<<<<< HEAD
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Services;

public class CountryService(AppDbTransferContext appDbContext,
=======

namespace Core.Services;

public class CountryService(AppDbTransferContext appDbContext, 
>>>>>>> aea59e1b4ac8a1b0e26c6e93adb7a6774902ac26
    IImageService imageService,
    IMapper mapper) : ICountryService
{
    public async Task<CountryItemModel> CreateAsync(CountryCreateModel model)
    {
        var entity = mapper.Map<CountryEntity>(model);
        if (model.Image != null)
        {
            entity.Image = await imageService.UploadImageAsync(model.Image);
        }
        await appDbContext.Countries.AddAsync(entity);
        await appDbContext.SaveChangesAsync();
        var item = mapper.Map<CountryItemModel>(entity);
        return item;
    }

    public async Task<List<CountryItemModel>> GetListAsync()
    {
<<<<<<< HEAD
        var list = await appDbContext.Countries
=======
        var list  = await appDbContext.Countries
>>>>>>> aea59e1b4ac8a1b0e26c6e93adb7a6774902ac26
            .ProjectTo<CountryItemModel>(mapper.ConfigurationProvider)
            .ToListAsync();
        return list;
    }

    public async Task<CountryItemModel> UpdateAsync(CountryUpdateModel model)
    {

        var entity = await appDbContext.Countries.FindAsync(model.Id);

        if (entity == null || entity.IsDeleted)
        {
            throw new Exception("Країна не знайдена");
        }

        mapper.Map(model, entity);

        if (model.Image != null)
        {
            if (!string.IsNullOrEmpty(entity.Image))
            {
                imageService.DeleteImage(entity.Image);
            }
            entity.Image = await imageService.UploadImageAsync(model.Image);
        }

<<<<<<< HEAD
=======
        //appDbContext.Countries.Update(entity);
>>>>>>> aea59e1b4ac8a1b0e26c6e93adb7a6774902ac26
        await appDbContext.SaveChangesAsync();

        var item = mapper.Map<CountryItemModel>(entity);
        return item;
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await appDbContext.Countries.FindAsync(id);

        if (entity != null)
        {
            entity.IsDeleted = true;
            await appDbContext.SaveChangesAsync();
        }
    }
<<<<<<< HEAD

    // НОВА РЕАЛІЗАЦІЯ, ЯКОЇ НЕ ВИСТАЧАЛО:
    /// <summary>
    /// Отримати список країн для випадаючого списку (тільки ID та Name).
    /// </summary>
    public async Task<List<CountryDropdownModel>> GetCountriesForDropdownAsync()
    {
        // Оптимізована вибірка даних для dropdown
        var list = await appDbContext.Countries
            .Where(c => !c.IsDeleted) // Важливо: вибираємо лише не видалені
            .Select(c => new CountryDropdownModel
            {
                Id = c.Id,
                Name = c.Name
            })
            .OrderBy(c => c.Name) // Сортування за назвою
            .ToListAsync();

        return list;
    }
}
=======
}
>>>>>>> aea59e1b4ac8a1b0e26c6e93adb7a6774902ac26
