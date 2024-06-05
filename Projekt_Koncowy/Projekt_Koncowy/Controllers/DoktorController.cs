﻿using Microsoft.AspNetCore.Mvc;
using Projekt_Koncowy.Data.DTOs;
using Projekt_Koncowy.Services;

namespace Projekt_Koncowy.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoktorController : ControllerBase
    {
        private readonly DoktorServices _doktorServices;

        public DoktorController(DoktorServices doktorServices)
        {
            _doktorServices = doktorServices;
        }

        [HttpPost("DodajDoktora")]
        public async Task<ActionResult<DoktorWyswietlDto>> DodajDoktora(DoktorDodajDto doktorDodajDto)
        {
            var doktor = await _doktorServices.DodajDoktora(doktorDodajDto);
            return CreatedAtAction(nameof(DodajDoktora), new { id = doktor.IdDoktor }, doktor);
        }

        [HttpDelete("UsunDoktora/{id}")]
        public async Task<IActionResult> UsunDoktora(int id)
        {
            var result = await _doktorServices.UsunDoktora(id);
            if (!result)
            {
                return NotFound($"Doktor o ID: {id} nie istnieje!");
            }

            return Ok($"Doktor o ID: {id} zostal usuniety!");
        }
    }
}