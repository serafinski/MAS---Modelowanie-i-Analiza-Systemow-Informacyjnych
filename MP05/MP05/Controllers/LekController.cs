using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MP05.Models;
using MP05.Models.DTOs;
using MP05.Services;

namespace MP05.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LekController : ControllerBase
    {
        //Import kontekstu
        private readonly ILekServices _lekServices;

        public LekController(ILekServices lekServices)
        {
            _lekServices = lekServices;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var lek = await _lekServices.GetLek(id);

            if (lek == null)
            {
                return NotFound();
            }

            return Ok(lek);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddLekDto addLekDto)
        {
            if (ModelState.IsValid)
            {
                var nowyLek = new Lek
                {
                    NazwaChemiczna = addLekDto.NazwaChemiczna,
                    MaxDawkaDzienna = addLekDto.MaxDawkaDzienna
                };

                var dodajLek = await _lekServices.AddLek(nowyLek);

                return CreatedAtAction(nameof(Get), new { id = dodajLek.IdLek }, dodajLek);
            }

            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var lek = await _lekServices.DeleteLek(id);

            if (!lek)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
