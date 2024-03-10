import java.util.ArrayList;
import java.util.List;

public class Zad1 {
    public static void main(String[] args) {
        //Jak przechowasz wiele liczb typu int gdy wiadomo ile ich jest?
        int tablica[] = {1, 2, 3, 4, 5};

        //Jak to zrobisz gdy w trakcie działania programu będą dodawane i/lub usuwane?
        List<Integer> lista = new ArrayList<Integer>();
        lista.add(1);
        lista.add(2);
        lista.add(3);
        lista.add(4);
        lista.add(5);
        for (Integer integer : lista) {
            System.out.println(integer);
        }
        lista.remove(2);


        //Jak je wyświetlisz na konsoli?
        System.out.println("Tablica:");
        for (int j : tablica) {
            System.out.println(j);
        }

        System.out.println("Lista:");
        for (Integer integer : lista) {
            System.out.println(integer);
        }
    }
}