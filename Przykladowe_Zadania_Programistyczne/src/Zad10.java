// Orginalna Klasa

// public class Zad10 {
//    public void calculate() {
//        int a = 5;
//        int b = 10;
//        int sum = a + b;
//        System.out.println("Sum is: " + sum);
//    }
//}

public class Zad10 {

    // Extract Constant
    public static final int A = 5;

    // Przećwicz kilka refaktoringów/refaktoryzacji (refactoring) dostępnych w nowoczesnych IDE,
    //np. w IntelliJ IDEA, np. Safe Delete, Extract Method, Extract Constant, Extract Field, Extract
    //Parameter, Introduce Variable, Rename.

    // Extract Parameter
    public void calculate(int b) {
        int sum = getSum(A, b);
        System.out.println("Sum is: " + sum);
    }

    // Extract Method
    private static int getSum(int a, int b) {
        int sum = a + b;
        return sum;
    }

}