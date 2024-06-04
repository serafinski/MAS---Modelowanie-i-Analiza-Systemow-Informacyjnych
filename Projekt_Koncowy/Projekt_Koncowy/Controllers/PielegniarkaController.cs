using Microsoft.AspNetCore.Mvc;
using Projekt_Koncowy.Data.DTOs;
using Projekt_Koncowy.Services;

namespace Projekt_Koncowy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PielegniarkaController : ControllerBase
    {
        private readonly PielegniarkaServices _service;

        public PielegniarkaController(PielegniarkaServices service)
        {
            _service = service;
        }

        [HttpPost("DodajPielegniarke")]
        public async Task<IActionResult> DodajPielegniarke(DodajPielegniarkeDto pielegniarkaDto)
        {
            var result = await _service.DodajPielegniarke(pielegniarkaDto);
            return CreatedAtAction(nameof(WyswietlDane), new { id = result.IdOsoba }, result);
        }

        [HttpDelete("UsunPielegniarke/{id}")]
        public async Task<IActionResult> UsunPielegniarke(int id)
        {
            var result = await _service.UsunPielegniarke(id);
            if (!result)
            {
                return NotFound($"Pielegniarka o ID: {id} nie istnieje!");
            }
            return Ok($"Pielegniarka o ID: {id} zostala usunieta!");
        }

        [HttpGet("WyswietlDane/{id}")]
        public async Task<IActionResult> WyswietlDane(int id)
        {
            var pielegniarka = await _service.WyswietlDane(id);
            if (pielegniarka == null)
            {
                return NotFound($"Pielegniarka o ID: {id} nie istnieje!");
            }
            return Ok(pielegniarka);
        }

        [HttpGet("WyswietlGrafik/{id}")]
        public async Task<IActionResult> WyswietlGrafik(int id)
        {
            var grafik = await _service.WyswietlGrafik(id);
            if (grafik == null)
            {
                return NotFound($"Pielegniarka o ID: {id} nie istnieje!");
            }
            return Ok(grafik);
        }
    }
}