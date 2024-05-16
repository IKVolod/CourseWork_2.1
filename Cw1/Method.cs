namespace CourseWork;

internal class Method
{
    public static int ReturnOneOrTwo()
    {
        int num;
        while (!int.TryParse(Console.ReadLine(), out num) || !(num > 0 & num < 3)) Console.WriteLine("\nВведіть коректне значення");

        return num;
    }
    
    public static int GetCorrectPositiveIntNum()
    {
        int num;
        while (!int.TryParse(Console.ReadLine(), out num) || num <= 0) Console.WriteLine("\nВведіть коректне значення");

        return num;
    }
}