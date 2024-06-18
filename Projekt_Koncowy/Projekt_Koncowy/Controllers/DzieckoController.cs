using Microsoft.AspNetCore.Mvc;
using Projekt_Koncowy.Data.DTOs;
using Projekt_Koncowy.Services;

namespace Projekt_Koncowy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DzieckoController : ControllerBase
    {
        private readonly DzieckoServices _dzieckoServices;

        public DzieckoController(DzieckoServices dzieckoServices)
        {
            _dzieckoServices = dzieckoServices;
        }

        [HttpPost("DodajDziecko")]
        public async Task<ActionResult<DzieckoWyswietlDto>> DodajDziecko(DzieckoDodajDto dto)
        {
            var dziecko = await _dzieckoServices.DodajDziecko(dto);
            return CreatedAtAction(nameof(DodajDziecko), new { id = dziecko.IdPacjenta }, dziecko);
        }

        [HttpDelete("UsunDziecko/{id}")]
        public async Task<IActionResult> UsunDziecko(int id)
        {
            var result = await _dzieckoServices.UsunDziecko(id);
            if (!result)
            {
                return NotFound($"Dziecko o ID: {id} nie istnieje!");
            }
            return Ok($"Dziecko o ID: {id} zostało usunięty!");
        }
    }
}