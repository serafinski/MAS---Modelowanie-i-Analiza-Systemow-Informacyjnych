using Microsoft.AspNetCore.Mvc;
using Projekt_Koncowy.Data.DTOs;
using Projekt_Koncowy.Services;

namespace Projekt_Koncowy.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WizytaController : ControllerBase
    {
        private readonly WizytaServices _wizytaService;

        public WizytaController(WizytaServices wizytaServices)
        {
            _wizytaService = wizytaServices;
        }

        [HttpGet("WyswietlHistorieWizyt/{idPacjent}")]
        public async Task<ActionResult<PacjentHistoriaResponseDto>> WyswietlHistorieWizyt(int idPacjent)
        {
            var historiaWizyt = await _wizytaService.WyswietlHistorieWizytAsync(idPacjent);
            if (historiaWizyt == null)
            {
                return NotFound($"Pacjent o ID: {idPacjent} nie istnieje!");
            }
            return Ok(historiaWizyt);
        }

        [HttpGet("WyswietlWizyte/{id}")]
        public async Task<ActionResult<WizytaResponseDto>> WyswietlWizyte(int id)
        {
            var wizyta = await _wizytaService.WyswietlWizyteAsync(id);
            if (wizyta == null)
            {
                return NotFound($"Wizyta o ID: {id} nie istnieje!");
            }
            return Ok(wizyta);
        }

        [HttpPost("DodajWizyte")]
        public async Task<ActionResult<WizytaDto>> DodajWizyte([FromBody] WizytaDodajDto dto)
        {
            try
            {
                var wizytaResponse = await _wizytaService.DodajWizyteAsync(dto);
                return Ok(wizytaResponse);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("UsunWizyte/{id}")]
        public async Task<ActionResult> UsunWizyte(int id)
        {
            bool isDeleted = await _wizytaService.UsunWizyteAsync(id);
            if (!isDeleted)
            {
                return NotFound($"Wizyta o ID: {id} nie istnieje!");
            }
            return Ok($"Wizyta o ID: {id} została usunięta!");
        }
    }
}