﻿using Microsoft.AspNetCore.Mvc;
using Projekt_Koncowy.Data.DTOs;
using Projekt_Koncowy.Services;

namespace Projekt_Koncowy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReceptaController : ControllerBase
    {
        private readonly ReceptaServices _receptaService;

        public ReceptaController(ReceptaServices receptaServices)
        {
            _receptaService = receptaServices;
        }

        [HttpGet("WyswietlRecepty/{idPacjent}")]
        public async Task<ActionResult<IEnumerable<ReceptaWyswietlDto>>> WyswietlRecepty(int idPacjent)
        {
            var recepty = await _receptaService.WyswietlRecepty(idPacjent);
            return Ok(recepty);
        }

        [HttpPost("DodajRecepte")]
        public async Task<ActionResult<ReceptaResponseDto>> DodajRecepte(int idWizyta, List<LekNaRecepcieDodajDto> lekiNaRecepcie)
        {
            var recepta = await _receptaService.DodajRecepte(idWizyta, lekiNaRecepcie);
            return CreatedAtAction(nameof(WyswietlRecepty), new { idPacjent = idWizyta }, recepta);
        }



        [HttpDelete("UsunRecepte/{id}")]
        public async Task<ActionResult> UsunRecepte(int id)
        {
            var result = await _receptaService.UsunRecepte(id);
            if (!result)
            {
                return NotFound($"Recepta o ID: {id} nie istnieje!");
            }
            return Ok($"Recepta o ID: {id} zostala usunięta!");
        }
    }
}