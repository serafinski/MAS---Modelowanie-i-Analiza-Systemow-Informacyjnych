using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MP05.Models;
using MP05.Models.DTOs;
using MP05.Services;

namespace MP05.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WizytaController : ControllerBase
    {
     
        //Import kontekstu
        private readonly IWizytaServices _wizytaServices;

        public WizytaController(IWizytaServices wizytaServices)
        {
            _wizytaServices = wizytaServices;
        }
        
        // Kod z dodatkowym wierszem bo lazy loading -> obejście przez DTO poniżej
        // [HttpGet("{id}")]
        // public async Task<IActionResult> Get(int id)
        // {
        //     var wizyta = await _wizytaServices.GetWizyta(id);
        //
        //     if (wizyta == null)
        //     {
        //         return NotFound();
        //     }
        //
        //     return Ok(wizyta);
        // }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDto(int id)
        {
            var wizytaDto = await _wizytaServices.GetDtoWizyta(id);

            if (wizytaDto == null)
            {
                return NotFound();
            }

            return Ok(wizytaDto);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddWizytaDto addWizytaDto)
        {
            if (ModelState.IsValid)
            {
                var nowaWizyta = new Wizyta
                {
                    DataWizyty = addWizytaDto.DataWizyty,
                    OpisWizyty = addWizytaDto.OpisWizyty,
                    IdDoktor = addWizytaDto.IdDoktor
                };

                var dodajWizyte = await _wizytaServices.AddWizyta(nowaWizyta);
                
                //Response DTO
                var responseDto = new WizytaResponseDto
                {
                    IdWizyta = dodajWizyte.IdWizyta,
                    DataWizyty = dodajWizyte.DataWizyty,
                    OpisWizyty = dodajWizyte.OpisWizyty,
                    IdDoktor = dodajWizyte.IdDoktor
                };

                return CreatedAtAction(nameof(GetDto), new { id = dodajWizyte.IdWizyta }, responseDto);
            }

            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var wizyta = await _wizytaServices.DeleteWizyta(id);

            if (!wizyta)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
