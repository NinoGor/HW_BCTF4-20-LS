namespace Lecture5_HW
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region დავალება 1
            Console.WriteLine("1.");
            /*
             შექმენით ერთ განზომილებიანი ორი მასივი.
             შეავსეთ ორივე მასივი ელემენტებით.
             გააერთიანე ერთ მასივში ორივე მასივის ელემენტები.
             დაბეჭდეთ საბოლოოდ მიღებული მასივი.         
            */

            // რადგან პირობაში კონკრეტული მოთხოვნა არ არის მასივების შექმნის/შევსების გზაზე,
            // მასივის ზომებს იუზერს შემოვატანინებ და რენდომ რიცხვებით შევავსებ

            byte size1, size2;

            // ზომებზე შევზღუდავ 0 მნიშვნელობასაც (თუმცა ამის გარეშეც იმუშავებს კოდი)
            do
            {
                Console.Write("Enter size for array N1: ");
            } while (!byte.TryParse(Console.ReadLine(), out size1) || size1 == 0);
            Console.WriteLine();

            do
            {
                Console.Write("Enter size for array N2: ");
            } while (!byte.TryParse(Console.ReadLine(), out size2) || size2 == 0);
            Console.WriteLine();

            // ახლა შევქმნათ ორი 1D მასივი
            int[] arr1 = new int[size1];
            int[] arr2 = new int[size2];

            // შევავსოთ ისინი რენდომ რიცხვებით
            Random r = new Random();

            // ვთქვათ, შევავსოთ რეინჯიდან [-100, 101)
            int lBound = -100;
            int uBound = 101;

            // ბარემ ამ ციკლებითვე დავბეჭდოთ მასივის ელემენტებიც
            Console.Write("arr1 = [");
            for (int i = 0; i < arr1.Length; i++)
            {
                arr1[i] = r.Next(lBound, uBound);
                Console.Write(arr1[i] + (i < arr1.Length - 1 ? ", " : ""));
            }
            Console.WriteLine("]");

            Console.Write("arr2 = [");
            for (int i = 0; i < arr2.Length; i++)
            {
                arr2[i] = r.Next(lBound, uBound);
                Console.Write(arr2[i] + (i < arr2.Length - 1 ? ", " : ""));
            }
            Console.WriteLine("]");

            // ახლა გავაერთიანოთ ახალ მასივში ამ ორი მასივის ელემენტები
            int[] arr3 = new int[size1 + size2];
            for (int i = 0; i < arr1.Length; i++)
            {
                arr3[i] = arr1[i];
            }
            for (int i = 0; i < arr2.Length; i++)
            {
                arr3[arr1.Length + i] = arr2[i];
            }

            // ახლა დავბეჭდოთ მიღებული მასივი
            Console.Write("resultArray = [");
            for (int i = 0; i < arr3.Length; i++)
            {
                Console.Write(arr3[i] + (i < arr3.Length - 1 ? ", " : ""));
            }
            Console.WriteLine("]");
            #endregion

            #region დავალება 2
            /*
             შექმენით ინტების მასივი და შეავსეთ ელემენტებით. მაგ: 3, 5, -4, 8, 11, 1, -1, 6
             კონსოლიდან გადმოეცით რაღაც რიცხვი რომელსაც შეინახავთ targetSum ცვლადში.
             მოძებნეთ მასივში ყველა ის ორი ელემენტი რომლის ჯამიც იქნება targetSum ტოლი და ამ
             წყვილებისგან შექმენით მასივი.
             დააბრუნეთ ამ ელემენტების წყვილები კონსოლში.
            */
            Console.WriteLine("\n2.");

            // დავალება 1-ის ანალოგიურად შევქმნი და შევავსებ მასივს, ბარემ size1 ცვლადს გამოვიყენებ ხელახლა
            // ბოდიში DRY პრინციპს, ჯერ ფუნქციები არ გაგვივლია
            do
            {
                Console.Write("Enter size for array: ");
            } while (!byte.TryParse(Console.ReadLine(), out size1) || size1 == 0);
            Console.WriteLine();
            int[] arr = new int[size1];

            Console.Write("arr = [");
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = r.Next(lBound, uBound);
                Console.Write(arr[i] + (i < arr.Length - 1 ? ", " : ""));
            }
            Console.WriteLine("]");

            int targetSum;
            do
            {
                Console.Write("Enter target sum: ");
            } while (!int.TryParse(Console.ReadLine(), out targetSum));

            // გასატესტად
            //arr = [3, 5, -4, 8, 11, 1, -1, 6]; targetSum = 7;

            /*
              დავალებაში მოცემული შედეგის მიხედვით რიგს მნიშვნელობა არ აქვს, ანუ (n, m) და (m, n) ცალკე წყვილები არაა
              
              გარდა ამისა, ვფიქრობ უკეთესი გზა იქნებოდა, რომ შეგვექმნა ნებისმიერ შემთხვევაში საკმარისი ზომის 
              (სულ N(N-1)/2 წყვილი შეიძლება იყოს) მასივი და შემდეგ შეგვეძლო resize-ის გაკეთებაც.
              ასე მეტ სისწრაფეს მოვიგებდით დამატებითი მეხსიერების ხარჯზე,
              რადგან სხვა შემთხვევაში arr-ზე იტერირება დაგვჭირდება 2-ჯერ: ჯერ წყვილების რაოდენობისთვის, შემდეგ resultArray-ს შესავსებად.
              თუმცა, სავარჯიშოდ და პირობასთან უფრო სიახლოვისთვის სწორედ ამ მეორე გზას გავყვები.
            */

            int nPairs = 0;
            for (int i = 0; i < arr.Length - 1; i++)
            {
                for (int j = i + 1; j < arr.Length; j++)
                {
                    if (arr[i] + arr[j] == targetSum) nPairs++;
                }
            }

            /* resultArray-ს გაკეთების ორ ვარიანტს დავწერ, 1D და 2D ვერსიასაც.
               პრინციპში, დავალების პირობაში მკაცრად 2D მასივი არ არის მოთხოვნილი, ამიტომ პირველ ვერსიაში
               წყვილებისგან შექმნილ მასივს 1D-ს გავაკეთებ და ბეჭდვისას 2D სახეს მივცემ, როგორც შედეგშია.
            */

            #region resultArray 1D მასივით

            int[] resultArray = new int[nPairs * 2];
            int index = 0; // resultArray-სთვის, 2-2-ით გაიზრდება

            for (int i = 0; i < arr.Length - 1; i++)
            {
                for (int j = i + 1; j < arr.Length; j++)
                {
                    if (arr[i] + arr[j] == targetSum)
                    {
                        resultArray[index] = arr[i];
                        resultArray[index + 1] = arr[j];
                        index += 2;
                    }
                }
            }

            // დაბეჭდვა
            if (nPairs == 0)
            {
                Console.WriteLine($"No pairs of elements with sum {targetSum} were found.");
            }
            else
            {
                Console.Write("resultArray = [ ");
                // დავალების შედეგიდან ჩანს რომ ჯერ ბოლო წყვილი იბეჭდება და არა პირველი
                for (int i = resultArray.Length - 2; i >= 0; i -= 2)
                {
                    Console.Write($"[{resultArray[i]}, {resultArray[i + 1]}]" + (i > 0 ? ", " : ""));
                }
                Console.WriteLine(" ]");
            }

            #endregion

            #region resultArray 2D მასივით
            //int[,] resultArray = new int[nPairs, 2];
            //int index = 0;

            //for (int i = 0; i < arr.Length - 1; i++)
            //{
            //    for (int j = i + 1; j < arr.Length; j++)
            //    {
            //        if (arr[i] + arr[j] == targetSum)
            //        {
            //            resultArray[index, 0] = arr[i];
            //            resultArray[index, 1] = arr[j];
            //            index++;
            //        }
            //    }
            //}

            //// დაბეჭდვა
            //if (nPairs == 0)
            //{
            //    Console.WriteLine($"No pairs of elements with sum {targetSum} were found.");
            //}
            //else
            //{
            //    Console.Write("resultArray = [ ");
            //    // დავალების შედეგიდან ჩანს რომ ჯერ ბოლო წყვილი იბეჭდება და არა პირველი
            //    for (int i = nPairs - 1; i >= 0; i--)
            //    {
            //        Console.Write($"[{resultArray[i, 0]}, {resultArray[i, 1]}]" + (i > 0 ? ", " : ""));
            //    }
            //    Console.WriteLine(" ]");
            //}
            #endregion

            #endregion
        }
    }
}
