import java.util.ArrayList;
import java.util.List;

public class Zad4 {
    //Napisz program zapamiętujący zmienną liczbę string’ów, a następnie usuwający losowe z nich.
    //Wyświetl na konsolę dane przed i po operacji.

    int length;
    int random = (int) (Math.random() * 10) + 1;

    public Zad4() {
        this.length = (int) (Math.random() * 10) + 1;
    }

    public String generate() {
        String result = "";
        for (int i = 0; i < length; i++) {
            // ASCII 97-122 (pomiędzy a-z)
            result += (char) (Math.random() * 26 + 97);
        }
        return result;
    }

    public List<String> generateList() {
        List<String> list = new ArrayList<>();
        for (int i = 0; i < random; i++) {
            list.add(generate());
        }
        return list;
    }

    public void removeRandom(List<String> list){
        int num = (int) (Math.random() * list.size());

        for (int i = 0; i < num; i++){
         if (!list.isEmpty()){
             list.remove((int) (Math.random() * list.size()));
         }
        }
    }

    public static void main(String[] args) {
        Zad4 zad4 =  new Zad4();

        List<String> list = zad4.generateList();
        System.out.println(list);
        zad4.removeRandom(list);
        System.out.println(list);
    }
}
