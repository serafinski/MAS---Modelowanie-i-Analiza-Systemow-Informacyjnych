namespace Projekt_Koncowy.Data.DTOs;

public class ReceptaResponseDto
{
    public int IdRecepta { get; set; }
    public DateTime DataWystawienia { get; set; }
    public int IdPacjent { get; set; }
    // Może to być DoroslyDto, DzieckoDto lub SeniorDto
    public object Pacjent { get; set; } = null!;
    public DoktorDto Doktor { get; set; } = null!;
    public List<LekNaRecepcieWyswietlDto> LekiNaRecepcie { get; set; } = null!;
}