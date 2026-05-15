namespace Lecture4_HW
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region დავალება 1
            /* გააკეთე კონსოლიდან შემოყვანილი რიცხვის გამრავლების ტაბულის 
               ერთი ბლოკი. (ათის ნამრავლის ჩათვლით) */
            Console.WriteLine("1.");
            int num;

            // ვალიდაციისთვის დაჩაგრული do-while-ი გამოვიყენოთ 💔 სანამ იუზერი სწორ ინპუტს არ მოგვცემს
            do
            {
                Console.Write("Enter an integer: ");
            } while (!int.TryParse(Console.ReadLine(), out num));
            Console.WriteLine();

            // ახლა გამოვიტანოთ ტაბულა:
            for (int i = 1; i <= 10; i++)
            {
                Console.WriteLine($"{num} * {i} = {num * i}");
            }

            Console.WriteLine("\n------------------------------------------\n");
            #endregion

            #region დავალება 2
            /* დაწერეთ პროგრამა რომელიც გამოიტანს კონსოლში ფიფქებით შედგენილ პირამიდის ფორმას.  
               მაგალითად, ციფრი 4–ის შეყვანისას კონსოლში გამოვა შემდეგი სახის პირამიდა: 
               * 
              * * 
             * * * 
            * * * *                                                                           */
            Console.WriteLine("2.");
            byte n;
            // დავალება 1-ის ანალოგიური ვალიდაცია,
            // აქ >= 2 პირობას დავამატებ, რომ მიღებული ფორმა სამკუთხედი გამოვიდეს
            do
            {
                Console.Write("Enter an integer >= 2: ");
            } while (!(byte.TryParse(Console.ReadLine(), out n) && n >= 2));

            Console.WriteLine();

            // nested for loop-ით დაბეჭდვა:
            for (int i = 1; i <= n; i++) // სტრიქონებზე იტერაცია
            {
                // ჯერ სწორი ინდენტაციისთვის დაგვჭირდება სფეისების გამოტანა
                // ეს რაოდენობა არის (n-i), რამდენი სტრიქონიც "დარჩა" ქვემოთ, იმდენი სფეისი გვჭირდება
                for (int j = 1; j <= n - i; j++)
                {
                    Console.Write(" ");
                }
                // მერამდენე სტრიქონზეც ვართ (i), იმდენი * გვინდა, ამიტომ:
                for (int j = 1; j <= i; j++)
                {
                    // *-ს რომ სფეისი არ მივაყოლოთ მართობული სამკუთხედი გამოგვივა
                    Console.Write("* ");

                }
                //სტრიქონის ბოლოს არ დაგვავიწყდეს ახალ ხაზზე გადასვლა
                Console.WriteLine();
            }
            Console.WriteLine("\n------------------------------------------\n");
            #endregion

            #region დავალება 3
            /* დაწერეთ პროგრამა რომელიც კონსოლიდან წაკითხულ რიცხვამდე დააჯამებს 
               ყველა ლუწ რიცხვს და პასუხი გამოიტანეთ კონსოლში */

            // კვლავ ანალოგიური ვალიდაცია, მაგრამ აქ არაუარყოფითობაზე შევამოწმებ
            // (ან შეგვიძლია byte გამოვიყენოთ თუ 255-მდე შეზღუდვა დასაშვებია ჩვენთვის)
            Console.WriteLine("3.");
            do
            {
                Console.Write("Enter a non-negative integer: ");
            } while (!(int.TryParse(Console.ReadLine(), out num) && num >= 0));

            int sum = 0;

            // "რიცხვამდე"-ში თუ ჩათვლით იგულისხმება, მაშინ ნაცვლად <=-ს გამოვიყენებთ
            // რადგან 0-ის დამატება ტყუილად ზედმეტი იტერაციაა, სჯობს 2-დან დავიწყოთ, პრობლემას არ შექმნის
            for (int i = 2; i < num; i += 2)
            {
                sum += i;
            }

            Console.WriteLine("Sum of even numbers before your number: " + sum);
            Console.WriteLine("\n------------------------------------------\n");
            #endregion

            #region დავალება 4
            /* დაწერეთ პროგრამა რომელიც აირჩევს რენდომულ რიცხვს.
               მომხმარებელმა შემოიყვანოს კონსოლიდან რიცხვი მანამ არ გამოიცნობს არჩეულ რენდომულ რიცხვს.*/

            // პირობითად ავიღოთ მარტივი ლიმიტი, რომ რიცხვი გამოცნობადი იყოს
            // ან შემეძლო იუზერისთვის მეკითხა ეს მნიშვნელობაც
            Console.WriteLine("4.");
            int limit = 10;
            Random r = new Random();
            int randNum = r.Next(limit + 1);

            do
            {
                Console.Write($"Guess a number 0-{limit}: ");

                // კვლავ წინა დავალებებში გამოყენებულ num-ს გადავაწერ
                if (int.TryParse(Console.ReadLine(), out num))
                {
                    if (num == randNum)
                    {
                        Console.WriteLine($" -- Congrats! {num} was the correct guess.");
                        break;
                    }
                    else
                    {
                        Console.WriteLine(" -- Wrong guess. Try again.");
                    }
                }
                else
                {
                    Console.WriteLine($" -- Invalid input! Try again.");
                }

            } while (true);
            #endregion
        }
    }
}
