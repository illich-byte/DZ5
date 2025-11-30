using Core.Models.Location;

namespace Core.Interfaces;

public interface ICountryService
{
    Task<List<CountryItemModel>> GetListAsync();

    Task<CountryItemModel> CreateAsync(CountryCreateModel model);

    Task<CountryItemModel> UpdateAsync(CountryUpdateModel model);
<<<<<<< HEAD
    Task<List<CountryDropdownModel>> GetCountriesForDropdownAsync();
    Task DeleteAsync(int id);
    
=======

    Task DeleteAsync(int id);
>>>>>>> aea59e1b4ac8a1b0e26c6e93adb7a6774902ac26
}
