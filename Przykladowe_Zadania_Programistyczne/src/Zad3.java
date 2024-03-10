public class Zad3 {
    // Stwórz generator tekstów typu Lorem Ipsum.
    private static final String LOREM_IPSUM = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";

    public String generateLoremIpsum(int length){
       String[] words = LOREM_IPSUM.split(" ");
       StringBuilder loremIpsum = new StringBuilder();


       int index = 0;
       for (int i = 0; i < length;i++){
           loremIpsum.append(words[index]).append(" ");
           //pozwala na zapętlenie słów
           index = (index + 1) % words.length;
       }

       return loremIpsum.toString().trim();
    }

    public static void main(String[] args) {
        Zad3 zad3 = new Zad3();
        System.out.println(zad3.generateLoremIpsum(200));
    }
}
