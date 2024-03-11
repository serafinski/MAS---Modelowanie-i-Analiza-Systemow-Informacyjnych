import java.time.LocalDateTime;
import java.time.temporal.ChronoUnit;
public class Zad9 {
    //Stwórz program obliczający liczbę dni od rozpoczęcia semestru oraz do jego zakończenia.
    //Skorzystaj z klas (np. LocalDateTime) znajdujących się w pakiecie pakiet java.time dodanych do Java 8.

    public static void main(String[] args) {
        LocalDateTime start = LocalDateTime.of(2024, 3, 1, 0, 0);
        LocalDateTime end = LocalDateTime.of(2024, 6, 28, 0, 0);
        LocalDateTime now = LocalDateTime.now();

        long daysFromStart = ChronoUnit.DAYS.between(start, now);
        long daysToEnd = ChronoUnit.DAYS.between(now, end);

        System.out.println("Teraz: " + now);
        System.out.println("Poczatek semestru: " + start);
        System.out.println("Koniec semestru: " + end);
        System.out.println("Liczba dni od rozpoczęcia semestru: " + daysFromStart);
        System.out.println("Liczba dni do zakończenia semestru: " + daysToEnd);
    }
}
