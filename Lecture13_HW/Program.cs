namespace Lecture13_HW
{
    internal class Program
    {
        // 10 სტუდენტის მასივი
        // დავიხმარებ წინა დავალების data.txt ფაილს
        static Student[] students =
        [
            new Student("Giorgi", "Beridze", 19, "g.beridze@email.com", "599123456", 85.3, Faculty.IT),
                new Student("Nino", "Kapanadze", 20, "nino.k@email.com", "555987654", 85.0, Faculty.Design),
                new Student("Luka", "Makharadze", 21, "johnd@gmail.com", "577321654", 92.5, Faculty.Business),
                new Student("Ani", "Dolidze", 18, "ani.doli@email.com", "591456789", 77.0, Faculty.Medicine),
                new Student("Davit", "Japaridze", 24, "d.japaridze@email.com", "551112233", 65.3, Faculty.IT),
                new Student("Mariam", "Chkheidze", 17, "m.chkheidze@email.com", "598778899", 88.7, Faculty.Business),
                new Student("Irakli", "Gelashvili", 23, "i.gelashvili@email.com", "595334455", 64.9, Faculty.IT),
                new Student("Elene", "Kvaratskhelia", 20, "elene.kvarats@email.com", "574665544", 99.3, Faculty.Medicine),
                new Student("Alex", "Meskhi", 21, "a.meskhi@email.com", "593221100", 98.8, Faculty.Design),
                new Student("Sopo", "Tsiklauri", 19, "sopho.tsik@email.com", "568445566", 83.6, Faculty.Business)
        ];
        // ამ ცვლადს გამოვიყენებ რომ გავარკვიო, მასივის resize თუ მჭირდება
        // (სტუდენტის დამატებისას გავაორმაგებ მასივის ზომას)
        static int studentCount = 10;
        // თუ სორტირებულია (რადგან ასეთი არჩევანიც გვაქ მენიუში), საუკეთესო სტუდენტის ძიებას დავაჩქარებ
        static bool isSortedGpaDesc = false;
        static void Main(string[] args)
        {
            using (Logger logger = new Logger())
            {
                while (true)
                {
                    Console.WriteLine("\n------------------ MENU ------------------");
                    Console.WriteLine("1. yvela studentis chveneba");
                    Console.WriteLine("2. sauketeso studentis povna");
                    Console.WriteLine("3. GPA-is sashualos gamotvla");
                    Console.WriteLine("4. studentis dzebna gvarit");
                    Console.WriteLine("5. studentebis dalageba GPA-is mikhedvit");
                    Console.WriteLine("6. axali studentis damateba");
                    Console.WriteLine("7. studentis washla");
                    Console.WriteLine("8. programidan gasvla");
                    Console.WriteLine("------------------------------------------");
                    Console.Write("\nChoose (1-8): ");

                    // რიცხვითი ტიპის ნაცვლად სტრინგში შევინახოთ და სვიჩის default-ით გვექნება ვალიდაცია
                    string choice = Console.ReadLine() ?? string.Empty; // null რომ არ იყოს როგორმე
                    Console.WriteLine();

                    switch (choice)
                    {
                        /* Array.Resize-ის შემთხვევაში უკვე გვქონდა ref-თან შეხება, მაგრამ, თუ არ ვცდები,
                           ჩვენ მიერ დაწერილ მეთოდებში არ გამოგვიყენებია, ამიტომ ამ ეტაპზე მოვერიდები.
                           ნაცვლად, პირდაპირ students მასივზე ვიმოქმედებ, არგუმენტად არ გადავცემ.
                         */

                        case "1": ShowAllStudents(); break;
                        case "2": FindBestStudent(); break;
                        case "3": GetAverageGPA(); break;
                        case "4": PrintStudentByLastName(); break;
                        case "5":
                            // რადგან Array.Resize-ით უკვე გვქონდა ref-თან შეხება, არ მოვერიდები მას
                            // და მასივი იქნება პარამეტრად, ნაცვლად იმისა რომ პირდაპირ მოქმედებდეს
                            SortStudentsByGPA(); break;
                        case "6": AddNewStudent(logger); break;
                        case "7": DeleteStudent(logger); break;
                        case "8": return; // დასრულდება პროგრამა
                        default: Console.WriteLine("Error: input must be in range [1-8]! Try Again..."); break;
                    }


                }
            }

        }

        static void ShowAllStudents()
        {
            if (studentCount == 0)
            {
                Console.WriteLine("Students array is empty!");
                return;
            }
            // აქ მოთხოვნილი იყო foreach-ის გამოყენება
            foreach (Student student in students)
            {
                // მხოლოდ ძირითადი ინფოს ბეჭდვა, როგორც პირობაში იყო
                if (student != null) { student.Print(); }
            }
        }
        static void FindBestStudent()
        {
            if (studentCount == 0)
            {
                Console.WriteLine("Students array is empty!");
                return;
            }

            Console.WriteLine("Best student (by GPA):");
            Student best = students[0]; // თუ სორტირებულია, ეს საუკეთესო სტუდენტია
            if (!isSortedGpaDesc)
            {
                for (int i = 1; i < studentCount; i++)
                {
                    // სიმარტივისთვის ჩავთვალოთ, რომ თუ რამდენიმე სტუდენტია უმაღლესი GPA-ით
                    // მათგან მასივში რიგით პირველის მონაცემები დაიბეჭდება
                    if (students[i].GPA > best.GPA)
                    {
                        best = students[i];
                    }
                }
            }

            best.PrintDetailed();
        }
        static void GetAverageGPA()
        {
            if (studentCount == 0)
            {
                Console.WriteLine("Students array is empty!");
                return;
            }

            double sum = 0;
            for (int i = 0; i < studentCount; i++)
            {
                sum += students[i].GPA;
            }

            Console.WriteLine($"Avg. GPA: {sum / studentCount}");
        }

        static void PrintStudentByLastName()
        {
            Console.Write("Enter a last name: ");
            string answer = (Console.ReadLine() ?? string.Empty).Trim().ToLower();
            bool found = false;

            for (int i = 0; i < studentCount; i++)
            {
                // LastName-ის set-ში Trim უკვე გვაქვს
                if (students[i].LastName.ToLower().Equals(answer))
                {
                    found = true;
                    Console.Write("Found -> ");
                    students[i].PrintDetailed();
                    // არ დავაბრეიქებ, რადგან შესაძლოა რამდენიმე სტუდენტია ამ გვარით
                }
            }

            if (!found) { Console.WriteLine($"No student found with last name: {answer}"); }
        }

        static void SortStudentsByGPA()
        {
            if (studentCount == 0)
            {
                Console.WriteLine("Students array is empty!");
                return;
            }
            Console.WriteLine("Sorting array by GPA descending...");
            // დავწეროთ bubble sort
            for (int i = 0; i < studentCount - 1; i++)
            {
                for (int j = 0; j < studentCount - i - 1; j++)
                {
                    if (students[j].GPA < students[j + 1].GPA)
                    {
                        Student temp = students[j];
                        students[j] = students[j + 1];
                        students[j + 1] = temp;
                    }
                }
            }
            isSortedGpaDesc = true;
            Console.WriteLine("Array has been sorted.");
        }

        static void AddNewStudent(Logger logger)
        {
            // ვალიდაციები მაქვს თვითონ property-ებში (ვიფიქრე, ასე ჯობდა)
            // რადგან მკაცრად არ ეწერა რომ throw-ები ამ მეთოდში უნდა მოხდეს, ასე დავტოვე
            // ვამოწმებ ყველაფერს, რაც მოთხოვნაში იყო

            Student tempStudent = new Student();

            // 1. სახელის შეყვანა (ამოწმებს Person-ის Name ფროფერთის სეტი)
            while (true)
            {
                try
                {
                    Console.Write("Enter name: ");
                    tempStudent.Name = Console.ReadLine() ?? string.Empty;
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message} Try again...");
                }
            }

            // 2. გვარის შეყვანა (ამოწმებს Person-ის LastName ფროფერთის სეტი)
            while (true)
            {
                try
                {
                    Console.Write("Enter last name: ");
                    tempStudent.LastName = Console.ReadLine() ?? string.Empty;
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ {ex.Message} სცადეთ თავიდან.");
                }
            }

            // 3. ასაკის შეყვანა (int.Parse ისვრის FormatException-ს, ხოლო Person-ის სეტერი - ArgumentOutOfRangeException-ს)
            while (true)
            {
                try
                {
                    Console.Write("ასაკი (17-120): ");
                    tempStudent.Age = int.Parse(Console.ReadLine() ?? string.Empty);
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message} Try again...");
                }
            }

            // 4. ელ-ფოსტის შეყვანა (ამოწმებს Student-ის Email ფროფერთის სეტი)
            while (true)
            {
                try
                {
                    Console.Write("Enter e-mail: ");
                    tempStudent.Email = Console.ReadLine() ?? string.Empty;
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message} Try again...");
                }
            }

            // 5. ტელეფონის შეყვანა (ამოწმებს Student-ის Phone ფროფერთის სეტი)
            while (true)
            {
                try
                {
                    Console.Write("Enter phone: ");
                    tempStudent.Phone = Console.ReadLine() ?? string.Empty;
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message} Try again...");
                }
            }

            // 6. GPA-ის შეყვანა (აქაც, double.Parse ისვრის ფორმატის ერორს, სეტერი კი - range-ისას)
            while (true)
            {
                try
                {
                    Console.Write("GPA (0-100): ");
                    tempStudent.GPA = double.Parse(Console.ReadLine() ?? string.Empty);
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message} Try again...");
                }
            }

            // 7. ფაკულტეტის შეყვანა (Enum.Parse თავად ამოწმებს ვალიდურობას)
            while (true)
            {
                try
                {
                    Console.Write("Enter faculty (IT, Business, Design, Medicine): ");
                    // case insensitive შემოწმება
                    tempStudent.Faculty = Enum.Parse<Faculty>(Console.ReadLine() ?? string.Empty, true);
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine($"Invalid faculty! Try again...");
                }
            }

            // ვექტორის მასივის გაფართოების ლოგიკა - capacity მექანიზმისავით
            if (students == null || studentCount == students.Length)
            {
                int newCapacity = (students == null || students.Length == 0) ? 4 : students.Length * 2;
                Array.Resize(ref students, newCapacity);
                logger.LogAction($"[System]: array capacity increased to {newCapacity}.");
            }

            // სტუდენტის ჩასმა მასივში და count-ის გაზრდა
            students[studentCount] = tempStudent;
            studentCount++;

            Console.WriteLine("Student has been added.");
            logger.LogAction($"Added student: {tempStudent.Name} {tempStudent.LastName} ({tempStudent.Email})");
        }

        static void DeleteStudent(Logger logger)
        {
            Console.Write("Enter student email: ");
            string searchEmail = (Console.ReadLine() ?? string.Empty).Trim().ToLower();

            int targetIndex = -1; // რომ შემდეგ shift ოპერაციები გავაკეთოთ შესაბამის ადგილებზე

            // ვეძებთ მხოლოდ აქტიურ სტუდენტებში (studentCount-მდე)
            for (int i = 0; i < studentCount; i++)
            {
                if (students[i].Email.ToLower().Equals(searchEmail))
                {
                    targetIndex = i;
                    break;
                }
            }

            // თუ ასეთი იმეილი არ მოიძებნა
            if (targetIndex == -1)
            {
                Console.WriteLine($"No student found with email: {searchEmail}");
                return;
            }

            // ამ სტუდენტს რომ "ამოვშლით", მისი შემდგომი სტუდენტები უნდა გადმოვწიოთ მარცხნივ
            for (int i = targetIndex; i < studentCount - 1; i++)
            {
                students[i] = students[i + 1];
            }

            // ბოლო ადგილი null-ად ვაქციოთ (ის ახლა "ცარიელია"),
            // მასივის ზომას მხოლოდ დამატებისას ვცვლი, სტუდენტების რეალური რაოდენობა შევამციროთ
            // warning რომ არ გვქონდეს მასივი Student?[] ტიპის უნდა გავხადოთ, თუმცა ამას ზედმეტი შემოწმებები დასჭირდება
            students[studentCount - 1] = null;
            studentCount--;

            string msg = $"Deleted student with email: {searchEmail}.";
            Console.WriteLine(msg);
            logger.LogAction(msg);
        }
    }
}

