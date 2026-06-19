using System.Text.RegularExpressions;

namespace Lecture13_HW
{
    internal class Student : Person, IPrintable
    {
        private string _email = "";
        private string _phone = "";
        // decimal-ის ზედმეტი სიზუსტე, ჩემი აზრით, აქ არ დაგვჭირდება
        private double _gpa;

        // Person კლასიდან გვაქვს Name, Surname და Age property-ები
        // თუმცა, გადავფაროთ Age, ვთქვათ, სტუდენტი არის 17+ ასაკის
        public override int Age
        {
            get
            {
                return base.Age;
            }
            set
            {
                if ( value < 17 || value > 120)
                {
                    throw new ArgumentOutOfRangeException("Student age must be in rage [17, 120]");
                }
                base.Age = value;
            }
        }

        public string Email
        {
            get
            {
                return _email;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Student must provide email!");
                }
                string trimmed = value.Trim();
                // მეილისთვის მკაცრი რეგექს პატერნი მოვიძიე
                // როგორც მოთხოვნილია, რა თქმა უნდა, ისიც მოწმდება რომ @-ს შეიცავს
                if (!Regex.IsMatch(trimmed, @"[a-zA-Z0-9]+([._-][0-9a-zA-Z]+)*@[a-zA-Z0-9]+([.-][0-9a-zA-Z]+)*\.[a-zA-Z]{2,}"))
                {
                    throw new ArgumentException("Email is invalid!");
                }
                _email = trimmed;
            }
        }
        public string Phone
        {
            get
            {
                return _phone;
            }

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    // ვთქვათ ნომერი არ არის მკაცრად მოთხოვნილი
                    _phone = "Not Provided";
                    return;
                }
                string trimmed = value.Trim();
                // მოვაშოროთ შიგნიდან სფეისებიც, თუ არის
                trimmed = trimmed.Replace(" ", "");
                // ნომრისთვის მარტივი რეგექს პატერნი 
                if (!Regex.IsMatch(trimmed, @"^\+?[1-9]\d{1,14}$"))
                {
                    throw new ArgumentException("Phone number is invalid!");
                }
                _phone = trimmed;
            }
        }

        public double GPA
        {
            get
            {
                return _gpa;
            }
            set
            {
                if(value < 0 || value > 100)
                {
                    throw new ArgumentOutOfRangeException("GPA must be in the range [0, 100]");
                }
                _gpa = value;
            }
        }

        public Faculty Faculty { get; set; }

        public Student() { }
        public Student(string name, string lastName, int age, string email, string phone, double gpa, Faculty faculty)
            : base(name, lastName, age)
        {
            Email = email;
            Phone = phone;
            GPA = gpa;
            Faculty = faculty;
        }

        public void Print()
        {
            // დავბეჭდოთ იმ ფორმატით, რაც დავალების პირობაშია
            Console.WriteLine($"{Name} {LastName} | {Age} | {Faculty} | {GPA}");
        }
        public void PrintDetailed()
        {
            // აქ კი დავბეჭდოთ სრული/დეტალური ინფო
            Console.WriteLine($"{Name} {LastName} | Age: {Age} | Faculty: {Faculty}\nGPA: {GPA} | E-mail: {Email} | Phone: {Phone}");
        }

        // ოპერატორების გადატვირთვა სტუდენტების GPA-ის მიხედვით შედარებისთვის
        // რადგან arrow functions უკვე დაგვჭირდა ლექციაზე, მათ გამოვიყენებ, ვფიქრობ, უფრო წაკითხვადია 
        public static bool operator >(Student s1, Student s2) => s1.GPA > s2.GPA;
        public static bool operator <(Student s1, Student s2) => s1.GPA < s2.GPA;
        public static bool operator >=(Student s1, Student s2) => s1.GPA >= s2.GPA;
        public static bool operator <=(Student s1, Student s2) => s1.GPA <= s2.GPA;
    }
}
