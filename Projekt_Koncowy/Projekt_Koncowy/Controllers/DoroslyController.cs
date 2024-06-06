using Microsoft.AspNetCore.Mvc;
using Projekt_Koncowy.Data.DTOs;
using Projekt_Koncowy.Services;

namespace Projekt_Koncowy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoroslyController : ControllerBase
    {
        private readonly DoroslyServices _doroslyServices;

        public DoroslyController(DoroslyServices doroslyServices)
        {
            _doroslyServices = doroslyServices;
        }

        [HttpPost("DodajDorosly")]
        public async Task<ActionResult<DoroslyWyswietlDto>> DodajDorosly(DoroslyDodajDto dto)
        {
            var dorosly = await _doroslyServices.DodajDorosly(dto);
            return CreatedAtAction(nameof(DodajDorosly), new { id = dorosly.IdPacjenta }, dorosly);
        }

        [HttpDelete("UsunDorosly/{id}")]
        public async Task<IActionResult> UsunDorosly(int id)
        {
            var result = await _doroslyServices.UsunDorosly(id);
            if (!result)
            {
                return NotFound($"Dorosly o ID: {id} nie istnieje!");
            }
            return Ok($"Dorosly o ID: {id} został usunięty!");
        }
    }
}