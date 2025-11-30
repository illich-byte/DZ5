using Core.Interfaces;
using Core.Models.Location;
using Microsoft.AspNetCore.Mvc;

namespace WebApiTransfer.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CountriesController(ICountryService countryService) 
    : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetCountries()
    {
        var list = await countryService.GetListAsync();
<<<<<<< HEAD
        return Ok(list);
=======
        // Implementation to retrieve and return countries would go here.
        return Ok(list); //код 200
>>>>>>> aea59e1b4ac8a1b0e26c6e93adb7a6774902ac26
    }

    [HttpPost]
    public async Task<IActionResult> CreateCountry([FromForm] CountryCreateModel model)
    {
        var item = await countryService.CreateAsync(model);

        return CreatedAtAction(null, item);
<<<<<<< HEAD
=======
        //return Created(item); //код 201
>>>>>>> aea59e1b4ac8a1b0e26c6e93adb7a6774902ac26
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await countryService.DeleteAsync(id);
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromForm] CountryUpdateModel model)
    {
        var res = await countryService.UpdateAsync(model);
        return Ok(res);
    }
}
