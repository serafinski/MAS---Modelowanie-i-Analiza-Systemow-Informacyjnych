import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

public class Zad5 {
    // Wygeneruj zestaw losowych liczb z podanego zakresu. Następnie znajdź największą oraz najmniejszą spośród nich.

    int zakres;

    public Zad5(int zakres) {
        this.zakres = zakres;
    }

    public List<Integer> generate(){
        List<Integer> list = new ArrayList<>();
        for (int i = 0; i < zakres; i++) {
            list.add((int) (Math.random() * 100));
        }
        return list;
    }

    public void returnValues (List<Integer> list){
        int min = Collections.min(list);
        int max = Collections.max(list);

        System.out.println("Najmniejsza liczba to: " + min);
        System.out.println("Największa liczba to: " + max);
    }

    public static void main(String[] args) {
        Zad5 zad5 = new Zad5(10);
        List<Integer> list = zad5.generate();
        System.out.println(list);
        zad5.returnValues(list);
    }
}
