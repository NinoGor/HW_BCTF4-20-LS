using Lecture9_HW.Enums;

namespace Lecture9_HW
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 7.შექმენით Employ კლასის 8 ინსტანსი, რომლებსაც ყოველ ორს ერთიდაიგივე ქვეყანა ექნება.
            // 8.შეინახეთ ეს ობიექტები Employ[] employs = new Employ[8] ელემენტიან მასივში.

            // 7 & 8-ს გავაერთიანებ და პირდაპირ შევავსებ მასივს, 8 ცალი ზედმეტი მიმთითებელი რომ არ გვქონდეს
            Employee[] employees = new Employee[8];

            employees[0] = new Employee(
                "Giorgi",
                "Vigacadze",
                new DateTime(1995, 5, 10),
                Country.Georgia,
                Gender.Male,
                new Contact[]
                {
                    new Contact(Contacts.Phone, "+995555123456"),
                    new Contact(Contacts.Email, "giorgi@mail.com")
                });

            employees[1] = new Employee(
                "Nino",
                "Gorgiladze",
                new DateTime(2003, 10, 14),
                Country.Georgia,
                Gender.Female,
                new Contact[]
                {
                    new Contact(Contacts.Email, "nino@mail.com")
                });

            // თურმე გერმანულში გავრცელებული example გვარია Mustermann 
            employees[2] = new Employee(
                "Max",
                "Mustermann",
                new DateTime(1982, 3, 15),
                Country.Germany,
                Gender.Male,
                new Contact[]
                {
                    new Contact(Contacts.Phone, "+4915112345678")
                });

            employees[3] = new Employee(
                "Erika",
                "Mustermann",
                new DateTime(1992, 7, 20),
                Country.Germany,
                Gender.Female,
                new Contact[]
                {
                    new Contact(Contacts.Email, "erika@mustermann.de")
                });

            // ფრანგი თანამშრომლები
            employees[4] = new Employee(
                "Jean",
                "Dupont",
                new DateTime(1989, 11, 5),
                Country.France,
                Gender.Male,
                new Contact[]
                {
                     new Contact(Contacts.Phone, "+33123456789")
                });

            employees[5] = new Employee(
                "Jeanne",
                "Dupont",
                new DateTime(1993, 4, 18),
                Country.France,
                Gender.Female,
                new Contact[]
                {
                    new Contact(Contacts.Email, "jeanne.dupont@gmail.com")
                });

            // თანამშრომლები UK-დან
            employees[6] = new Employee(
                 "John",
                 "Doe",
                 new DateTime(1991, 9, 22),
                 Country.UnitedKingdom,
                 Gender.Male,
                 new Contact[]
                 {
                    new Contact(Contacts.Phone, "+44 7700 900123"),
                    new Contact(Contacts.Email, "john.doe@mail.co.uk")
                 });

            employees[7] = new Employee(
                "Jane",
                "Doe",
                new DateTime(1996, 12, 30),
                Country.UnitedKingdom, 
                Gender.Female,
                new Contact[]
                {
                    new Contact(Contacts.Phone, "+44 7700 900456"),
                    new Contact(Contacts.Email, "jane.doe@mail.co.uk")
                });

            Console.WriteLine("Employees from Georgia");
            Console.WriteLine("----------------------");
            PrintEmployeesByCountry(employees, Country.Georgia);

            Console.WriteLine("Employees from Germany");
            Console.WriteLine("----------------------");
            PrintEmployeesByCountry(employees, Country.Germany);

            Console.WriteLine("Employees from France");
            Console.WriteLine("----------------------");
            PrintEmployeesByCountry(employees, Country.France);

            Console.WriteLine("Employees from the UK");
            Console.WriteLine("----------------------");
            PrintEmployeesByCountry(employees, Country.UnitedKingdom);

        }

        /* 9.შექმენით მეთოდი გარეთ ან რამე კლასში რომელიც მიიღებს ორ პარამეტრს, Employ[] მასივს და ქვეყანას,
            გადაივლის ყოველ ელემენტზე და დაგვიბეჭდავს ისეთ Employ-იებს რომლებსაც ეგ ქვეყანა აქვთ Country
            ფროფერთიში შენახული */
        public static void PrintEmployeesByCountry(Employee[] employees, Country targetCountry)
        {
            // თუ მასივი ცარიელია ან null-ია აღარ ვაგრძელებთ
            if (employees == null || employees.Length == 0)
            {
                Console.WriteLine("Employee array is empty.");
                return;
            }

            foreach (var employee in employees)
            {
                // თუ ელემენტი თავად არის null, მას გამოვტოვებთ
                if (employee == null) continue;

                if (employee.Country == targetCountry)
                {
                    Console.WriteLine(employee);
                    Console.WriteLine();
                }
            }
        }
    }
}
