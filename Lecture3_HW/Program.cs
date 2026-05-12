using System.Text;

namespace Lecture3_HW
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            #region დავალება 1
            Console.WriteLine("<----- დავალება 1 ----->");
            // სისტემაში გვაქვს
            string username = "admin";
            string password = "1234";

            // მომხმარებელს შემოჰყავს ორივე მნიშვნელობა
            Console.Write("Username: ");
            string usernameInp = Console.ReadLine();
            Console.Write("Password: ");
            string passwordInp = Console.ReadLine();

            // თუ სწორია კონსოლში გამოიტანე: Welcome!, თუ არა Access denied
            if (usernameInp == username && passwordInp == password)
            {
                Console.WriteLine("Welcome!");
            }
            else
            {
                Console.WriteLine("Access denied");
            }
            #endregion

            #region დავალება 2
            Console.WriteLine("\n\n<----- დავალება 2 ----->");
            // Calculator (switch-ით)
            // მომხმარებელი შეიყვანს:
            // რიცხვი 1
            // ოპერატორი (+ - * /)
            // რიცხვი 2

            // if-ების გამოყენებით არასწორი ინპუტის შემთხვევაში დავსკიპოთ მომდევნო ოპერაციები
            Console.Write("რიცხვი 1: ");
            if (double.TryParse(Console.ReadLine(), out double num1))
            {
                Console.Write("ოპერატორი (+ - * /): ");
                if (char.TryParse(Console.ReadLine(), out char op))
                {
                    Console.Write("რიცხვი 2: ");
                    if (double.TryParse(Console.ReadLine(), out double num2))
                    {
                        switch (op)
                        {
                            case '+':
                                Console.WriteLine($"{num1} + {num2} = {num1 + num2}");
                                break;
                            case '-':
                                Console.WriteLine($"{num1} - {num2} = {num1 - num2}");
                                break;
                            case '*':
                                Console.WriteLine($"{num1} * {num2} = {num1 * num2}");
                                break;
                            case '/':
                                /* რადგან double ტიპის ცვლადები გვაქვს, 0-ზე გაყოფა exception-ს არ ისვრის,
                                   შედეგი იქნება double.PositiveInfinity ან double.NegativeInfinity.
                                   თუმცა, თუ რატომღაც გვინდა მომხმარებელს ავუკრძალოთ 0-ზე გაყოფა: */
                                // if(num2 == 0){ Console.WriteLine("შეცდომა: 0-ზე გაყოფა! Exiting..."); return; }
                                Console.WriteLine($"{num1} / {num2} = {num1 / num2}");
                                break;
                            default:
                                Console.WriteLine("შეცდომა: არასწორი სიმბოლო ოპერატორისთვის!");
                                break;
                        }
                    }
                    else Console.WriteLine("შეცდომა: რიცხვი 2 არავალიდურია!");
                }
                else Console.WriteLine("შეცდომა: ოპერატორი არავალიდურია!");
            }
            else Console.WriteLine("შეცდომა: რიცხვი 1 არავალიდურია!");

            #endregion

            #region დავალება 3
            Console.WriteLine("\n\n<----- დავალება 3 ----->");
            // მომხმარებელს შეაყვანინე ასაკი:
            Console.Write("შეიყვანეთ ასაკი: ");
            if (byte.TryParse(Console.ReadLine(), out byte age))
            {
                // რადგან byte-ი unsigned ტიპისაა, age >= 0-ის შემოწმება არ გვჭირდება
                if (age <= 12)
                {
                    Console.WriteLine("ბავშვი (0-12)");
                }
                else if (age <= 19)
                {
                    Console.WriteLine("თინეიჯერი (13-19)");
                }
                else if (age <= 64)
                {
                    Console.WriteLine("ზრდასრული (20-64)");
                }
                else
                {
                    Console.WriteLine("პენსიონერი (65+)");
                }
            }
            else
            {
                Console.WriteLine("ინპუტი არავალიდურია!");
            }
            #endregion
        }
    }
}
