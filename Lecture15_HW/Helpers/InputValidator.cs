using System.Globalization;

namespace Lecture15_HW.Helpers
{
    internal static class InputValidator
    {
        public static string GetValidName()
        {
            while (true)
            {
                Console.Write("Enter the student's name: ");
                string name = (Console.ReadLine() ?? "").Trim();

                if (!string.IsNullOrEmpty(name))
                {
                    // Title Case-ში გადავიყვან + სერჩის დროს case sensitivity არ შეგვიშლის ხელს
                    // რატომღაც უფრო მარტივად გამოყენებადი მეთოდი არ აქვს C#-ს ამისთვის...
                    return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name.ToLower());
                }
                Console.WriteLine("Error: name is empty!");
            }
        }

        public static int GetValidGrade()
        {
            while (true)
            {
                Console.Write("Enter the score (0-100): ");
                if (int.TryParse(Console.ReadLine(), out int grade) && grade >= 0 && grade <= 100)
                {
                    return grade;
                }
                Console.WriteLine("Error: invalid score!");
            }
        }
    }
}
