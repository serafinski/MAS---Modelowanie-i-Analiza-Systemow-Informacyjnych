namespace MP01;

public class Adres
{
    public required string Ulica { get; set; }
    public required string Numer { get; set; }
    public int? NumerMieszkania { get; set; }
    public required string Miasto { get; set; }
    public required string KodPocztowy { get; set; } 
}