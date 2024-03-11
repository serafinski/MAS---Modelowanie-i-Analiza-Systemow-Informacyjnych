public class Zad8 {
    // Przygotuj rozwiązanie modyfikujące wartość podanych liczb (np. zwiększające o 1).

    float number;
    float number2;


    public Zad8(float number, float number2) {
        this.number = number;
        this.number2 = number2;
    }

    public void opis () {
        System.out.println("Liczba: " + number);
        System.out.println("Liczba2: " + number2);
    }

    public void add1() {
        number++;
        number2++;
    }

    public void substract1() {
        number--;
        number2--;
    }

    public void multiplyby(float mul) {
        number *= mul;
        number2 *= mul;
    }

    public void divideby(float div) {
        number /= div;
        number2 /= div;
    }

    public void power(float pow) {
        number = (float) Math.pow(number, pow);
        number2 = (float) Math.pow(number2, pow);
    }

    public static void main(String[] args) {
        Zad8[] numbers = {
                new Zad8(4, 5),
                new Zad8(5, 6),
                new Zad8(6, 7)
        };

        for (Zad8 number : numbers) {
            System.out.println("Pierwotne liczby:");
            number.opis();


            System.out.println("\nDodaj 1:");
            number.add1();
            number.opis();

            System.out.println("\nOdejmij 1:");
            number.substract1();
            number.opis();

            System.out.println("\nPomnóż przez wybrana liczbe:");
            number.multiplyby(2);
            number.opis();

            System.out.println("\nPodziel przez wybrana liczbe:");
            number.divideby(2);
            number.opis();

            System.out.println("\nPodnieś do potęgi wybrana liczbe:");
            number.power(2);
            number.opis();

            System.out.println();
        }
    }
}
