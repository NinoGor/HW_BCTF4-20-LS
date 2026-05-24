namespace Lecture6_HW
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region დავალება 1
            /* შექმენით jagged array სადაც: თითოეულ სტუდენტს აქვს სხვადასხვა რაოდენობის ქულა. 
              იპოვეთ თითოეულის საშუალო ქულა. */
            Console.WriteLine("1.");
            // ვთქვათ გვყავს 3 სტუდენტი
            // შევქმნათ jagged მასივი ახალი სინტაქსით, სხვადასხვა რაოდენობის ქულებით (მაგ., 2,3,4)
            int[][] studentScores = [[61, 86], [91, 92, 93], [71, 66, 79, 92]];

            // საშ. ქულების გამოსატანად ციკლში ცვლადიც გვეყოფა, თუ საშ. ქულების შენახვა გვსურს nStudents ზომის მასივში შეგვიძლია
            for (int i = 0; i < studentScores.Length; i++)
            {
                // გავითვალისწინოთ, რომ რომელიმე სტუდენტს შეიძლება ჯერ ქულა არ ქონდეს მიღებული
                if (studentScores[i] == null || studentScores[i].Length == 0)
                {
                    Console.WriteLine($"Student N{i + 1}: no points found.\n");
                    continue;
                }

                // ბარემ დამატებით ამ ციკლშივე გამოვიტანოთ თვითონ ქულებიც
                Console.Write($"Student N{i + 1}: ");

                int sum = 0;
                for (int j = 0; j < studentScores[i].Length; j++)
                {
                    Console.Write($"{studentScores[i][j]},"); // იყოს მძიმე ბოლო ელემენტთანაც, საშუალოს წინ
                    sum += studentScores[i][j];
                }

                // რადგან ქულები მთელ რიცხვებად ჩავთვალე, არამთელი ნაწილის არდაკარგვისთვის გამოვიყენოთ double
                double average = (double)sum / studentScores[i].Length;
                Console.WriteLine($"\nStudent N{i + 1} AVG score => {average}\n");
            }

            #endregion

            #region დავალება 2
            /* შექმენით რენდომული 4 ნიშნა პასკოდების არაი(10 წევრი). მომხმარებელს შემოაყვანინეთ კოდი 
               და თუ რომელიმეს დაემთხვა არაიში დაუბეჭდეთ “Correct” თუ არა და “Wrong”. */
            Console.WriteLine("2.");
            Random r = new Random();
            // რადგან არითმეტიკული ოპერაციები არ ტარდება პასკოდებზე, უმჯობესია სტრინგებად გვქონდეს
            string[] passcodes = new string[10];

            // 4-ნიშნა 10 პასკოდის გენერირება
            for (int i = 0; i < passcodes.Length; i++)
            {
                // საკუთარი ლოგიკით ავაწყოთ პასკოდი 4 ციფრით
                // რომელსაც ექნება 0000-დან 9999-ის ჩათვლით ნებისმიერი რენდომ მნიშვნელობა
                string temp = "";

                for (int j = 0; j < 4; j++)
                {
                    int digit = r.Next(0, 10); // [0, 10)
                    temp += digit; // რადგან იტერაციათა რაოდენობა უმნიშვნელოა StringBuilder არ დაგვჭირდება
                }

                passcodes[i] = temp;
            }

            // გასატესტად
            Console.WriteLine("Passcodes (for testing): " + string.Join(", ", passcodes));

            // იუზერს შემოვაყვანინოთ 4-ნიშნა პასკოდი, int.TryParse-ით შევამოწმოთ რომ ციფრებითაა
            string input;
            do
            {
                Console.Write("Enter a passcode of 4 digits: ");
                input = Console.ReadLine();
            } while (input.Length != 4 || !int.TryParse(input, out _));
            Console.WriteLine();

            bool isCorrect = false;

            // რადგან მასივში არაფრის მოდიფიკაცია არ ხდება, foreach გამოვიყენოთ
            foreach (string passcode in passcodes)
            {
                if (passcode == input)
                {
                    isCorrect = true;
                    break;
                }
            }
            Console.WriteLine("Result: " + (isCorrect ? "Correct" : "Wrong"));
            #endregion

            #region დავალება 3
            /* შექმენით int-ების(მათ შორის ნეგატიური რიცხვებიც) მასივი.იპოვეთ მინიმალური და მაქსიმალური 
               რიცხვები(არ გამოიყენოთ არაის მეთოდები). */
            Console.WriteLine("\n3.");

            int[] numbers = { -14, 10, 3, -5, 99, -22, 14, 7 };
            // შესამოწმებლად
            Console.WriteLine("numbers (for testing): " + string.Join(", ", numbers));

            // ნებისმიერი მასივისთვის გავითვალისწინოთ ლოგიკა
            if (numbers == null || numbers.Length == 0)
            {
                Console.WriteLine("No numbers were found!");
            }
            else
            {
                // ყველაზე მარტივია საწყის მნიშვნელობებად ავიღოთ პირველი ელემენტი
                int min = numbers[0];
                int max = numbers[0];

                for (int i = 1; i < numbers.Length; i++)
                {
                    if (numbers[i] < min)
                    {
                        min = numbers[i];
                    }
                    if (numbers[i] > max)
                    {
                        max = numbers[i];
                    }
                }

                Console.WriteLine($"Min: {min}");
                Console.WriteLine($"Max: {max}");
            }
            #endregion

            #region დავალება 4
            /* შექმენით სტრინგების მასივი და კონსოლში დაბეჭდეთ ყველა ელემენტის ყველა სიმბოლო
               (არ გამოიყენოთ არაის მეთოდები). */
            Console.WriteLine("\n4.");
            string[] strArr = ["C# ", "is a ", "general-purpose ",
                "high-level ", "programming language."];

            if (strArr == null || strArr.Length == 0)
            {
                Console.WriteLine("no letters were found.");
            }
            else
            {
                Console.WriteLine("Printing characters, added spaces and new lines for clarity:");
                // სავარჯიშოდ იყოს for ციკლი foreach-ის ნაცვლად
                for (int i = 0; i < strArr.Length; i++)
                {
                    string currentStr = strArr[i];
                    // სტრინგის სიმბოლოების მკაცრად სათითაოდ ბეჭდვა, რადგან პირობა ასე ითხოვს
                    for (int j = 0; j < currentStr.Length; j++)
                    {
                        // უკეთ საჩვენებლად სფეისებს დავამატებ
                        Console.Write($"{currentStr[j]} ");
                    }
                    // თუ გვინდა ახალი სტრინგი ახალ ხაზზე დაიწყოს
                    Console.WriteLine(); 
                }
                Console.WriteLine();
            }
            #endregion

            #region დავალება 5
            /* შექმენით იმეილების მასივი და დაადგინეთ ყველა ელემენტი თუ შეიცავს @ სიმბოლოს. 
               (არ გამოიყენოთ არაის და სტრინგის ჩაშენებული მეთოდები). */
            Console.WriteLine("5.");

            string[] emails = { "n.g@gmail.com", "araswori.com", "user@itstep.ge" };

            if (emails == null || emails.Length == 0)
            {
                Console.WriteLine("No emails were found!");
            }
            else
            {
                bool allValid = true;
                for (int i = 0; i < emails.Length; i++)
                {
                    string currentEmail = emails[i];

                    bool hasSymbol = false;

                    for (int j = 0; j < currentEmail.Length; j++)
                    {
                        if (currentEmail[j] == '@')
                        {
                            hasSymbol = true;
                            // თუ ამ მეილში ერთი @ მაინც ვიპოვეთ, შეგვიძლია ჩადგმული ციკლი უკვე გავწყვიტოთ
                            break;
                        }
                    }

                    // ბარემ სათითაოდ მეილებზეც გამოვიტანოთ ინფო
                    if (!hasSymbol)
                    {
                        allValid = false;
                        // მხოლოდ ყველას შემოწმება თუ გვინდა აქ დავაბრეიქებდით და დანარჩენ კოდს წავშლიდით
                        // break;
                        Console.WriteLine($"[{currentEmail}] does NOT contain '@'.");
                    }
                    else
                    {
                        Console.WriteLine($"[{currentEmail}] contains '@'.");
                    }
                }
                Console.Write("\nResult: ");
                Console.WriteLine(allValid ? "All emails contain '@'." : "Not all emails contain '@'.");
            }
            #endregion
        }
    }
}
