using Lecture16_HW.Helpers; 

namespace Lecture16_HW
{
    internal class Program
    {
        static void Main()
        {
            Console.WriteLine("--- Testing Algorithms Class ---\n");

            // სატესტო კოლექცია - შეიცავს დუბლიკატებს და დალაგებული არაა
            List<int> numbers = new List<int> { 5, 3, 9, 1, 3, 5, 7 };
            Print(numbers, "Original numbers:");

            // 1. Where -  4-ზე დიდი რიცხვები
            var filtered = Algorithms.Where(numbers, x => x > 4);
            Print(filtered, "1. Where (x > 4):");

            // 2. OrderBy - დავალაგოთ ზრდადობით (ორიგინალი არ იცვლება)
            var ordered = Algorithms.OrderBy(numbers);
            Print(ordered, "2. OrderBy (returns new sequence):");

            // OrderByInPlace - დავალაგოთ კოლექციაშივე
            // (შევქმნათ კოპია, რომ ორიგინალი არ "გავაფუჭოთ" სხვა ტესტებისთვის)
            List<int> listToOrder = new(numbers);
            Algorithms.OrderByInPlace(listToOrder);
            Print(listToOrder, "OrderByInPlace on a copy (modifies the collection):");

            // 3. First - ავიღოთ პირველი ელემენტი
            int first = Algorithms.First(numbers);
            Console.WriteLine($"3. First: {first}");

            // First ექსეფშენის ტესტი (ცარიელ კოლექციაზე)
            try
            {
                Algorithms.First(new List<int>());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"   First (calling on an empty list): {ex.Message}");
            }

            // 4. FirstOrDefault 
            int firstOrDefault = Algorithms.FirstOrDefault(new List<int>());
            Console.WriteLine($"\n4. FirstOrDefault (on empty list): {firstOrDefault}");

            // 5. Single - ვიპოვოთ ზუსტად ერთი 9-იანი
            int single = Algorithms.Single(numbers, x => x == 9);
            Console.WriteLine($"\n5. Single (x == 9): {single}");

            // Single ექსეფშენის ტესტი (როცა ერთზე მეტია)
            try
            {
                Algorithms.Single(numbers, x => x == 5); // 5-იანი ორჯერ გვაქვს
            }
            catch (Exception ex)
            {
                Console.WriteLine($"   Single (x == 5): {ex.Message}");
            }

            // 6. SingleOrDefault - 100 სიაში არ გვაქვს, უნდა დააბრუნოს 0 (int-ის default)
            int singleOrDefault = Algorithms.SingleOrDefault(numbers, x => x == 100);
            Console.WriteLine($"\n6. SingleOrDefault (x == 100): {singleOrDefault}");

            // 7. Any - შევამოწმოთ თუ გვაქვს 7-იანი
            bool hasSeven = Algorithms.Any(numbers, x => x == 7);
            Console.WriteLine($"\n7. Any (x == 7): {hasSeven}");

            // 8. All - შევამოწმოთ ყველა რიცხვი 3-ზე მეტია თუ არა
            bool allTest = Algorithms.All(numbers, x => x > 3);
            Console.WriteLine($"\n8. All (x > 3): {allTest}");

            // 9. Count - დავითვალოთ მთლიანი რაოდენობა და ასევე პირობით (მხოლოდ 3-იანები)
            int totalCount = Algorithms.Count(numbers);
            int countOfThrees = Algorithms.Count(numbers, x => x == 3);
            Console.WriteLine($"\n9. Count (total): {totalCount}");
            Console.WriteLine($"   Count (where x == 3): {countOfThrees}");

            // 10. Distinct (ორიგინალი არ იცვლება)
            var distinct = Algorithms.Distinct(numbers);
            Print(distinct, "\n10. Distinct (returns new sequence without duplicates):");

            // DistinctInPlace - დუბლიკატების ამოშლა უშუალოდ კოლექციიდან
            List<int> listToDistinct = new List<int>(numbers);
            Algorithms.DistinctInPlace(listToDistinct);
            Print(listToDistinct, "DistinctInPlace (modifies original collection):");
        }

        // დამხმარე მეთოდი კოლექციების გამოსატანად
        private static void Print<T>(IEnumerable<T> collection, string message)
        {
            Console.WriteLine(message);
            Console.WriteLine($"[{string.Join(", ", collection)}]\n");
        }
    }
}