using Microsoft.AspNetCore.Mvc;
using Projekt_Koncowy.Data.DTOs;
using Projekt_Koncowy.Services;

namespace Projekt_Koncowy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SeniorController : ControllerBase
    {
        private readonly SeniorServices _seniorServices;

        public SeniorController(SeniorServices seniorServices)
        {
            _seniorServices = seniorServices;
        }

        [HttpPost("DodajSenior")]
        public async Task<ActionResult<SeniorWyswietlDto>> DodajSenior(SeniorDodajDto dto)
        {
            var senior = await _seniorServices.DodajSenior(dto);
            return CreatedAtAction(nameof(DodajSenior), new { id = senior.IdPacjenta }, senior);
        }

        [HttpDelete("UsunSenior/{id}")]
        public async Task<IActionResult> UsunSenior(int id)
        {
            var result = await _seniorServices.UsunSenior(id);
            if (!result)
            {
                return NotFound($"Senior o ID: {id} nie istnieje!");
            }
            return Ok($"Senior o ID: {id} został usunięty!");
        }
    }
}