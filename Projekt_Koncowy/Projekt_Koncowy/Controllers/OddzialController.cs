using Microsoft.AspNetCore.Mvc;
using Projekt_Koncowy.Data.DTOs;
using Projekt_Koncowy.Services;

namespace Projekt_Koncowy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OddzialController : ControllerBase
    {
        private readonly OddzialServices _oddzialServices;

        public OddzialController(OddzialServices oddzialServices)
        {
            _oddzialServices = oddzialServices;
        }

        [HttpPost("DodajOddzial")]
        public async Task<IActionResult> DodajOddzial(OddzialDodajDto oddzialDodajDto)
        {
            var nowyOddzial = await _oddzialServices.DodajOddzial(oddzialDodajDto);
            return CreatedAtAction(nameof(WyswietlOddzial), new { id = nowyOddzial.IdOddzial }, nowyOddzial);
        }
        

        [HttpDelete("UsunOddzial/{id}")]
        public async Task<IActionResult> UsunOddzial(int id)
        {
            var wynik = await _oddzialServices.UsunOddzial(id);
            if (!wynik)
            {
                return NotFound($"Oddział o ID: {id} nie istnieje!");
            }

            return Ok($"Oddział o ID: {id} zostal usuniety!");
        }

        [HttpGet("WyswietlOddzial/{id}")]
        public async Task<IActionResult> WyswietlOddzial(int id)
        {
            var oddzial = await _oddzialServices.WyswietlOddzial(id);
            if (oddzial == null)
            {
                return NotFound($"Oddział o ID: {id} nie istnieje!");
            }

            return Ok(oddzial);
        }

        [HttpGet("WyswietlWszystkieOddzialy/{idPlacowki}")]
        public async Task<ActionResult<List<OddzialyWyswietlDto>>> WyswietlWszystkieOddzialy(int idPlacowki)
        {
            var oddzialy = await _oddzialServices.WyswietlWszystkieOddzialy(idPlacowki);
            return Ok(oddzialy);
        }
    }
}