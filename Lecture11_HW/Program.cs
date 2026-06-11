// System.Array რომ არ გამოიყენოს Array-ს შემთხვევაში
using Array = Lecture11_HW.Array;

namespace Lecture11_HW
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region დავალება 1
            Console.WriteLine(" - Davaleba 1");
            //Console.WriteLine("\nNormal case:");
            Array arr = new Array([1, 4, -3, 4, 1, 6, 10, 7]);
            Console.WriteLine($"{arr}; Length = {arr.Length}\n");

            arr.ShowEven(); 
            arr.ShowOdd();

            // ასევე გავტესტოთ null Array-ის შემთხვევა
            //Console.WriteLine("\nArray null case:");
            //Array nullArr = new Array(null);
            //nullArr.ShowEven();
            //nullArr.ShowOdd();
            Console.WriteLine();
            #endregion

            #region დავალება 2
            Console.WriteLine(" - Davaleba 2");
            // გავაკეთოთ იგივე მასივზე რაც პირველ დავალებაშია
            Console.WriteLine($"{arr}; Length = {arr.Length}\n");
            Console.WriteLine($"Number of distinct numbers: {arr.CountDistinct()}");
            Console.WriteLine($"Number 4 appears {arr.EqualToValue(4)} times");

            // null array-ზე გატესტვა, ასევე დავალება 1-ის მასივით
            //Console.WriteLine("\nArray null case:");
            //Console.WriteLine($"Number of distinct numbers: {nullArr.CountDistinct()}");
            //Console.WriteLine($"Number 4 appears {nullArr.EqualToValue(4)} times");
            #endregion
        }
    }
}
