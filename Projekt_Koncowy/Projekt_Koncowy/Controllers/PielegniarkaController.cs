using Microsoft.AspNetCore.Mvc;
using Projekt_Koncowy.Data.DTOs;
using Projekt_Koncowy.Services;

namespace Projekt_Koncowy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PielegniarkaController : ControllerBase
    {
        private readonly PielegniarkaServices _pielegniarkaServices;

        public PielegniarkaController(PielegniarkaServices pielegniarkaServices)
        {
            _pielegniarkaServices = pielegniarkaServices;
        }

        [HttpPost("DodajPielegniarke")]
        public async Task<ActionResult<PielegniarkaWyswietlDto>> DodajPielegniarke(PielegniarkaDodajDto dto)
        {
            var pielegniarka = await _pielegniarkaServices.DodajPielegniarke(dto);
            return CreatedAtAction(nameof(DodajPielegniarke), new { id = pielegniarka.IdPielegniarka }, pielegniarka);
        }

        [HttpDelete("UsunPielegniarke/{id}")]
        public async Task<IActionResult> UsunPielegniarke(int id)
        {
            var result = await _pielegniarkaServices.UsunPielegniarke(id);
            if (!result)
            {
                return NotFound($"Pielegniarka o ID: {id} nie istnieje!");
            }
            return Ok($"Pielegniarka o ID: {id} zostala usunieta!");
        }

        [HttpGet("WyswietlGrafik/{idPielegniarki}")]
        public async Task<IActionResult> WyswietlGrafik(int idPielegniarki)
        {
            var grafik = await _pielegniarkaServices.WyswietlGrafik(idPielegniarki);
            if (grafik == null)
            {
                return NotFound($"Pielegniarka o ID: {idPielegniarki} nie istnieje!");
            }
            return Ok(grafik);
        }
    }
}