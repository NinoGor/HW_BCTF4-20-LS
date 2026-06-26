using Lecture15_HW.Helpers;
using Lecture15_HW.Models;
using Lecture15_HW.Services;

namespace Lecture15_HW
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GradeBook gradeBook = new();
            GradeManagerService manager = new(gradeBook);

            bool running = true;

            while (running)
            {
                MenuHelper.PrintMenu();
                Console.Write("Select an option (1-5): ");
                string choice = Console.ReadLine() ?? "";
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        manager.AddStudent(InputValidator.GetValidName(), InputValidator.GetValidGrade());
                        break;

                    case "2":
                        manager.SearchStudent(InputValidator.GetValidName());
                        break;

                    case "3":
                        manager.UpdateGrade(InputValidator.GetValidName(), InputValidator.GetValidGrade());
                        break;

                    case "4":
                        manager.PrintAllStudents();
                        break;

                    case "5":
                        running = false;
                        Console.WriteLine("Exiting...");
                        break;

                    default:
                        Console.WriteLine("Invalid selection!");
                        break;
                }
            }
        }
    }
}
