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
        return Ok(list);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCountry([FromForm] CountryCreateModel model)
    {
        var item = await countryService.CreateAsync(model);

        return CreatedAtAction(null, item);
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
