public class Zad2 {
    //Stwórz generator losowych string’ów dla podanej długości tekstu.
    int length;

    public Zad2(int length) {
        this.length = length;
    }

    public String generate() {
        String result = "";
        for (int i = 0; i < length; i++) {
            // ASCII 97-122 (pomiędzy a-z)
            result += (char) (Math.random() * 26 + 97);
        }
        return result;
    }

    public static void main(String[] args) {
        Zad2 zad2 = new Zad2(10);
        System.out.println(zad2.generate());
    }
}
