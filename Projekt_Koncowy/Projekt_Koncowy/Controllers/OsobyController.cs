using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projekt_Koncowy.Data;

namespace Projekt_Koncowy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OsobyController : ControllerBase
    {
        private readonly MyDbContext _context;

        public OsobyController(MyDbContext context)
        {
            _context = context;
        }

        // Endpoint do wyświetlania danych konkretnej osoby
        [HttpGet("WyswietlDane/{id}")]
        public async Task<ActionResult<string>> GetOsoba(int id)
        {
            var osoba = await _context.Osoby
                .Include(o => o.Imiona)
                .Include(o => o.Adres)
                .FirstOrDefaultAsync(o => o.IdOsoba == id);

            if (osoba == null)
            {
                return NotFound($"Osoba o ID: {id} nie istnieje!");
            }

            return Ok(osoba.WyswietlDane());
        }
    }
}