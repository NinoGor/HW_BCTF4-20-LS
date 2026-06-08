using Lecture10_HW.Davaleba1;
using Lecture10_HW.Davaleba2;

namespace Lecture10_HW
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region დავალება 1
            Console.WriteLine("------ Davaleba 1 ------\n");

            // შევქმნათ თითო ტიპის 1 ობიექტი და გვქონდეს მათი მასივი
            MusicalInstrument[] instruments =
            {
                new Violin(),
                new Ukulele(),
                new Trombone(),
                new Cello()
            };

            // base კლასში მაქვს ToString-ის ოვერრაიდი, მაგრამ რადგანაც ამ ოთხი მეთოდის გამოყენება
            // იყო მოთხოვნილი, გამოვიყენეთ ისინი თითოეული ობიექტისთვის
            foreach (MusicalInstrument instrument in instruments)
            {
                Console.WriteLine(new string('-', 80));
                instrument.Show();
                instrument.Desc();
                instrument.History();
                instrument.Sound();
            }
            Console.WriteLine(new string('-', 80));

            Console.WriteLine();
            #endregion

            #region დავალება 2
            Console.WriteLine("\n\n------ Davaleba 2 ------\n");

            // შევქმნათ მასივი რომელშიც გვექნება თითო ტიპის თითო ობიექტი 
            Worker[] workers =
            {
                new President("John", "Doe", 5000),
                new Manager("Jane", "Doe", 3000),
                new Engineer("Alice", "Smith", 2500),
                new Security("Bob", "Bobson", 1000)
             };

            // გამოვიძახოთ ბეჭდვის ფუნქცია თითოეული მათგანისთვის
            foreach (Worker worker in workers)
            {
                worker.Print();
                Console.WriteLine();
            }
            #endregion
        }
    }
}
