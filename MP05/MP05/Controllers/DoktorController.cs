using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MP05.Models;
using MP05.Models.DTOs;
using MP05.Services;

namespace MP05.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoktorController : ControllerBase
    {
        //Import kontekstu
        private readonly IDoktorServices _doktorServices;

        public DoktorController(IDoktorServices doktorServices)
        {
            _doktorServices = doktorServices;
        }
        
        // Kod z dodatkowym wierszem bo lazy loading -> obejście przez DTO poniżej
        // [HttpGet("{id}")]
        // public async Task<IActionResult> Get(int id)
        // {
        //     var doktor = await _doktorServices.GetDoktor(id);
        //
        //     if (doktor == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     return Ok(doktor);
        // }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDto(int id)
        {
            var doktorDto = await _doktorServices.GetDtoDoktor(id);
        
            if (doktorDto == null)
            {
                return NotFound();
            }
        
            return Ok(doktorDto);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddDoktorDto addDoktorDto)
        {
            if (ModelState.IsValid)
            {
                var nowyDoktor = new Doktor
                {
                    Imie = addDoktorDto.Imie,
                    Nazwisko = addDoktorDto.Nazwisko,
                    Telefon = addDoktorDto.Telefon,
                    Pesel = addDoktorDto.Pesel,
                    NumerPrawaWykonywaniaZawodu = addDoktorDto.NumerPrawaWykonywaniaZawodu
                };
                
                var dodajDoktor = await _doktorServices.AddDoktor(nowyDoktor);

                return CreatedAtAction(nameof(GetDto), new { id = dodajDoktor.IdDoktor }, dodajDoktor);
            }

            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var doktor = await _doktorServices.DeleteDoktor(id);

            if (!doktor)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
