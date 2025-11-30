using Core.Interfaces;
using Core.Models.Location;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApiTransfer.Controllers;

[Route("api/[controller]")]
[ApiController]
// ВИПРАВЛЕНО: Додано ICountryService countryService у конструктор для інжекції залежності.
public class CitiesController(ICityService cityService, ICountryService countryService)
    : ControllerBase
{
    /// <summary>
    /// [POST] Створити нове місто.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(CityItemModel), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)] // Помилки валідації
    public async Task<IActionResult> CreateCity([FromBody] CityCreateModel model)
    {
        var item = await cityService.CreateAsync(model);
        // Повертаємо 201 Created та посилання на метод GetCities (з id)
        return CreatedAtAction(nameof(GetCities), new { id = item.Id }, item);
    }

    /// <summary>
    /// [GET] Отримати список усіх міст.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<CityItemModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCities()
    {
        var list = await cityService.GetListAsync();
        return Ok(list);
    }

    /// <summary>
    /// [GET] Отримати список країн для Dropdown-меню.
    /// </summary>
    [HttpGet("dropdown")]
    [ProducesResponseType(typeof(List<CountryDropdownModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCountriesForDropdown()
    {
        // ВИПРАВЛЕНО: Виклик методу на коректному сервісі countryService
        var list = await countryService.GetCountriesForDropdownAsync();
        return Ok(list);
    }
}