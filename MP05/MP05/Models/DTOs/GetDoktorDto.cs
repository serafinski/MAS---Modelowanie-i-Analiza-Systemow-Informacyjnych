namespace MP05.Models.DTOs;

public class GetDoktorDto
{
    public int IdOsoba { get; set; }
    public string Imie { get; set; } = null!;
    public string Nazwisko { get; set; } = null!;
    public string Telefon { get; set; } = null!;
    public string Pesel { get; set; } = null!;
    public int IdDoktor { get; set; }
    public string NumerPrawaWykonywaniaZawodu { get; set; } = null!;
    
    //Lista wizyt do DTO
    public List<GetDoktorWizytaDto> GetDoktorWizytaDtos { get; set; }
}