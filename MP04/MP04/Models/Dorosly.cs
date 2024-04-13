namespace MP04.Models;

// Dziedziczenie Wieloaspektowe - uwzględnia więcej niż 1 aspekt (kryterium podziału)
public class Dorosly : KategoriaWiekowa
{
    public override void UmowWizyte()
    {
        Console.WriteLine("Dorosly - Umawiam wizytę w przychodnii");
    }
}