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
            var historiaWizyt = await _wizytaService.WyswietlHistorieWizyt(idPacjent);
            if (historiaWizyt == null)
            {
                return NotFound($"Pacjent o ID: {idPacjent} nie istnieje!");
            }
            return Ok(historiaWizyt);
        }

        [HttpGet("WyswietlWizyte/{id}")]
        public async Task<ActionResult<WizytaResponseDto>> WyswietlWizyte(int id)
        {
            var wizyta = await _wizytaService.WyswietlWizyte(id);
            if (wizyta == null)
            {
                return NotFound($"Wizyta o ID: {id} nie istnieje!");
            }
            return Ok(wizyta);
        }
        
        [HttpGet("WyswietlHistorieWizyt/{idPacjent}/{from}/{to}")]
        public async Task<ActionResult<PacjentHistoriaResponseDto>> WyswietlHistorieWizyt(int idPacjent, DateTime from, DateTime to)
        {
            var historiaWizyt = await _wizytaService.WyswietlHistorieWizyt(idPacjent, from, to);
            if (historiaWizyt == null || historiaWizyt.Wizyty.Count == 0)
            {
                return NotFound($"Brak wizyt dla pacjenta o ID: {idPacjent} w podanym zakresie czasowym od {from:yyyy-MM-dd} do {to:yyyy-MM-dd}.");
            }
            return Ok(historiaWizyt);
        }

        
        [HttpPost("DodajWizyte")]
        public async Task<ActionResult<WizytaDto>> DodajWizyte([FromBody] WizytaDodajDto dto)
        {
            try
            {
                var wizytaResponse = await _wizytaService.DodajWizyte(dto);
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
            bool isDeleted = await _wizytaService.UsunWizyte(id);
            if (!isDeleted)
            {
                return NotFound($"Wizyta o ID: {id} nie istnieje!");
            }
            return Ok($"Wizyta o ID: {id} została usunięta!");
        }
    }
}