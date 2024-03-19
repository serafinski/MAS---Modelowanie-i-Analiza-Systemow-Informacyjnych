public class Zad7 {
    // Zapamiętaj dane charakteryzujące dowolny graniastosłup. Opracuj rozwiązanie przechowujące dane o znanej oraz zmiennej liczbie graniastosłupów.
    // Wyświetl na konsoli informacje ich dotyczące.

    // pole powierzchni podstawy
    float sp;

    // wysokosc graniastosłupa
    float h;

    // pole powierzchni bocznej
    float sb;

    public Zad7(float sp, float h, float sb) {
        this.sp = sp;
        this.h = h;
        this.sb = sb;
    }

    // objectosc
    public float v(){
        return sp * h;
    }

    // pole powierzchni całkowitej
    public float s(){
        return 2*sp + sb;
    }

    public void opis () {
        System.out.println("Pole powierzchni podstawy graniastosłupa: " + sp);
        System.out.println("Wysokość graniastosłupa: " + h);
        System.out.println("Pole powierzchni bocznej graniastosłupa: " + sb);
    }

    public static void main(String[] args) {

        Zad7[] graniastoslupy = {
                new Zad7(4, 5, 6),
                new Zad7(5, 6, 7),
                new Zad7(6, 7, 8)
        };

        for (Zad7 graniastoslup : graniastoslupy) {
            graniastoslup.opis();
            System.out.println("Objętość graniastosłupa: " + graniastoslup.v());
            System.out.println("Pole powierzchni całkowitej graniastosłupa: " + graniastoslup.s());
            System.out.println();
        }
    }
}
