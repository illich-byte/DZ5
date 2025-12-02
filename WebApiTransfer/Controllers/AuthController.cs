using Core.DTOs;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Ендпоінт для входу користувача через Google (External Login).
        /// </summary>
        /// <param name="dto">Містить Google ID Token.</param>
        /// <returns>AuthResultDto з JWT та інформацією про користувача.</returns>
        [HttpPost("google-login")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthResultDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GoogleLogin([FromBody] ExternalLoginDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.GoogleSignInAsync(dto.IdToken);

            if (result == null)
            {
                // Це може бути недійсний токен, проблема конфігурації або інша помилка
                return BadRequest(new { Message = "Неможливо виконати вхід через Google. Токен недійсний або сталася внутрішня помилка." });
            }

            return Ok(result);
        }

        // Тут можуть бути інші методи входу (логін/пароль) та реєстрації
    }
}