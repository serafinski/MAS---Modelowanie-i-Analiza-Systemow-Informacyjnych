namespace MP03.Models;

// Dziedziczenie Wieloaspektowe - uwzględnia więcej niż 1 aspekt (kryterium podziału)
public class Dziecko : KategoriaWiekowa
{
    public override void UmowWizyte()
    {
        Console.WriteLine("Dziecko - Umawiam wizytę w szkole");
    }
}