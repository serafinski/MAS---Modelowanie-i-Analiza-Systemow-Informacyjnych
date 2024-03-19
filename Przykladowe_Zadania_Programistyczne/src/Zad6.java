public class Zad6 {
    //Zapamiętaj dane charakteryzujące graniastosłup prawidłowy. Opracuj rozwiązanie przechowujące dane o znanej oraz zmiennej liczbie graniastosłupów.
    // Wyświetl na konsoli informacje ich dotyczące.

    // liczba boków podstawy graniastosłupa
    int n;
    // długość boku podstawy graniastosłupa
    float a;
    // wysokość graniastosłupa (np. długość krawędzi bocznej)
    float h;
    // długość promienia koła wpisanego w podstawę
    float r;


    public Zad6(int n, float a, float h, float r) {
        this.n = n;
        this.a = a;
        this.h = h;
        this.r = r;
    }

    public void opis () {
        System.out.println("Liczba boków podstawy graniastosłupa prawidłowego: " + n);
        System.out.println("Długość boku podstawy graniastosłupa prawidłowego: " + a);
        System.out.println("Wysokość graniastosłupa prawidłowego: " + h);
        System.out.println("Długość promienia koła wpisanego w podstawę graniastosłupa prawidłowego: " + r);
    }

    // objectosc
    public float v(){
        return (n * a * r * h) / 2;
    }

    // pole powierzchni bocznej
    public float m(){
        return n * a * h;
    }

    // pole powierzchni całkowitej
    public float s(){
        return (float) (n * a * (h + (a/2) * Math.tan(Math.PI/n)));
    }

    public static void main(String[] args) {
        Zad6[] graniastoslupy = {
                new Zad6(4, 5, 6, 7),
                new Zad6(5, 6, 7, 8),
                new Zad6(6, 7, 8, 9)
        };

        for (Zad6 graniastoslup : graniastoslupy) {
            graniastoslup.opis();
            System.out.println("Objętość graniastosłupa prawidłowego: " + graniastoslup.v());
            System.out.println("Pole powierzchni bocznej graniastosłupa prawidłowego: " + graniastoslup.m());
            System.out.println("Pole powierzchni całkowitej graniastosłupa prawidłowego: " + graniastoslup.s());
            System.out.println();
        }
    }
}
